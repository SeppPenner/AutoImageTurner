// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoTurnImagesTests.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   A class to test the <see cref="AutoTurnImages" /> class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AutoImageTurner.Tests;

/// <summary>
/// A class to test the <see cref="AutoTurnImages"/> class.
/// </summary>
[TestClass]
public class AutoTurnImagesTests
{
    /// <summary>
    /// The identifier of the EXIF orientation tag.
    /// </summary>
    private const int OrientationTagId = 0x0112;

    /// <summary>
    /// The value of the orientation tag of an image that needs no rotation.
    /// </summary>
    private const int OrientationNormal = 1;

    /// <summary>
    /// The test image with an orientation tag of 6, which means rotate by 90 degrees, 120 x 80 pixels.
    /// </summary>
    private static readonly string RotateImage = Path.Combine(AppContext.BaseDirectory, "TestData", "Rotate90.jpg");

    /// <summary>
    /// The test image without any orientation tag, 120 x 80 pixels.
    /// </summary>
    private static readonly string PlainImage = Path.Combine(AppContext.BaseDirectory, "TestData", "NoOrientation.jpg");

    /// <summary>
    /// The directory the images of a single test are copied to.
    /// </summary>
    private string testDirectory = string.Empty;

    /// <summary>
    /// Creates an empty directory outside of the repository for the images of the running test.
    /// </summary>
    [TestInitialize]
    public void CreateTestDirectory()
    {
        this.testDirectory = Path.Combine(Path.GetTempPath(), $"AutoImageTurner_{Guid.NewGuid():N}");
        Directory.CreateDirectory(this.testDirectory);
    }

    /// <summary>
    /// Removes the directory of the finished test.
    /// </summary>
    [TestCleanup]
    public void DeleteTestDirectory()
    {
        if (Directory.Exists(this.testDirectory))
        {
            Directory.Delete(this.testDirectory, true);
        }
    }

    /// <summary>
    /// Checks whether an image that carries an orientation tag is really rotated.
    /// </summary>
    [TestMethod]
    public void RotateRotatesAnImageWithAnOrientationTag()
    {
        var fileName = this.CopyImage(RotateImage, "image.jpg");
        var rotator = GetRotator();

        rotator.RotateImagesInFolderNoMessage(this.testDirectory, "jpg");

        Assert.AreEqual(new Size(80, 120), GetSize(fileName), "The image was not rotated.");
        Assert.AreEqual(OrientationNormal, GetOrientation(fileName), "The orientation tag was not reset.");
    }

    /// <summary>
    /// Checks whether an image without an orientation tag is left as it is.
    /// </summary>
    [TestMethod]
    public void RotateKeepsAnImageWithoutAnOrientationTag()
    {
        var fileName = this.CopyImage(PlainImage, "image.jpg");
        var rotator = GetRotator();

        rotator.RotateImagesInFolderNoMessage(this.testDirectory, "jpg");

        Assert.AreEqual(new Size(120, 80), GetSize(fileName), "The image must not be rotated.");
    }

    /// <summary>
    /// Checks whether a folder without a file of the given format is accepted silently. The rotation tool reports
    /// such a run as an error, and the run over all formats depends on that being swallowed.
    /// </summary>
    [TestMethod]
    public void RotateAcceptsAFolderWithoutAMatchingFile()
    {
        var fileName = this.CopyImage(RotateImage, "image.jpg");
        var rotator = GetRotator();

        rotator.RotateImagesInFolderNoMessage(this.testDirectory, "gif");

        Assert.AreEqual(new Size(120, 80), GetSize(fileName), "An image of another format was touched.");
    }

    /// <summary>
    /// Checks whether a file whose extension only starts with the given format is left alone.
    /// </summary>
    [TestMethod]
    public void RotateIgnoresALongerExtension()
    {
        var fileName = this.CopyImage(RotateImage, "image.jpeg");
        var rotator = GetRotator();

        rotator.RotateImagesInFolderNoMessage(this.testDirectory, "jpg");

        Assert.AreEqual(new Size(120, 80), GetSize(fileName), "A file with a longer extension was rotated.");
    }

    /// <summary>
    /// Checks whether a second run keeps the already rotated image.
    /// </summary>
    [TestMethod]
    public void RotateAppliedTwiceKeepsTheRotatedImage()
    {
        var fileName = this.CopyImage(RotateImage, "image.jpg");
        var rotator = GetRotator();

        rotator.RotateImagesInFolderNoMessage(this.testDirectory, "jpg");
        rotator.RotateImagesInFolderNoMessage(this.testDirectory, "jpg");

        Assert.AreEqual(new Size(80, 120), GetSize(fileName), "The image was rotated a second time.");
        Assert.AreEqual(OrientationNormal, GetOrientation(fileName), "The orientation tag was not reset.");
    }

    /// <summary>
    /// Checks whether a folder that does not exist ends up as an exception, which is what the form shows as an error.
    /// </summary>
    [TestMethod]
    public void RotateThrowsForAMissingFolder()
    {
        var rotator = GetRotator();
        var missingFolder = Path.Combine(this.testDirectory, "missing");

        Assert.ThrowsExactly<DirectoryNotFoundException>(() => rotator.RotateImagesInFolderNoMessage(missingFolder, "jpg"));
    }

    /// <summary>
    /// Gets a rotator that uses the language files copied beside the test assembly.
    /// </summary>
    /// <returns>A new <see cref="IAutoTurnImages"/>.</returns>
    private static IAutoTurnImages GetRotator()
    {
        return new AutoTurnImages(new LanguageManager());
    }

    /// <summary>
    /// Gets the pixel size of the given image.
    /// </summary>
    /// <param name="fileName">The file name of the image.</param>
    /// <returns>The size of the image.</returns>
    private static Size GetSize(string fileName)
    {
        using var image = Image.FromFile(fileName);
        return image.Size;
    }

    /// <summary>
    /// Gets the EXIF orientation of the given image. A missing tag counts as no rotation, the same way the rotation
    /// tool treats it.
    /// </summary>
    /// <param name="fileName">The file name of the image.</param>
    /// <returns>The value of the orientation tag.</returns>
    private static int GetOrientation(string fileName)
    {
        using var image = Image.FromFile(fileName);

        if (!image.PropertyIdList.Contains(OrientationTagId))
        {
            return OrientationNormal;
        }

        var property = image.GetPropertyItem(OrientationTagId);
        return property?.Value is null ? OrientationNormal : BitConverter.ToUInt16(property.Value, 0);
    }

    /// <summary>
    /// Copies one of the test images into the directory of the running test.
    /// </summary>
    /// <param name="sourceFileName">The file name of the test image.</param>
    /// <param name="targetFileName">The file name inside the directory of the test.</param>
    /// <returns>The full file name of the copy.</returns>
    private string CopyImage(string sourceFileName, string targetFileName)
    {
        var fileName = Path.Combine(this.testDirectory, targetFileName);
        File.Copy(sourceFileName, fileName);
        return fileName;
    }
}
