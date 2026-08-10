// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoTurnImages.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   A class to turn images.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AutoImageTurner;

/// <inheritdoc cref="IAutoTurnImages"/>
/// <summary>
/// A class to turn images.
/// </summary>
/// <seealso cref="IAutoTurnImages"/>
public class AutoTurnImages : IAutoTurnImages
{
    /// <summary>
    /// The file name of the tool that reads the orientation tag and rotates the images.
    /// </summary>
    private const string JheadFileName = "jhead.exe";

    /// <summary>
    /// The language manager.
    /// </summary>
    private readonly ILanguageManager languageManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoTurnImages"/> class.
    /// </summary>
    /// <param name="languageManager">The language manager.</param>
    public AutoTurnImages(ILanguageManager languageManager)
    {
        this.languageManager = languageManager;
    }

    /// <inheritdoc cref="IAutoTurnImages"/>
    /// <summary>
    /// Rotates the images in a folder.
    /// </summary>
    /// <param name="folder">The folder.</param>
    /// <param name="format">The format.</param>
    /// <seealso cref="IAutoTurnImages"/>
    public void RotateImagesInFolder(string folder, string format)
    {
        RunAutoRotate(folder, format);

        MessageBox.Show(
            this.languageManager.GetCurrentLanguage().GetWord("AutorotateFinishedText"),
            this.languageManager.GetCurrentLanguage().GetWord("AutorotateFinishedCaption"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    /// <inheritdoc cref="IAutoTurnImages"/>
    /// <summary>
    /// Rotates the images in a folder and doesn't show messages.
    /// </summary>
    /// <param name="folder">The folder.</param>
    /// <param name="format">The format.</param>
    /// <seealso cref="IAutoTurnImages"/>
    public void RotateImagesInFolderNoMessage(string folder, string format)
    {
        RunAutoRotate(folder, format);
    }

    /// <summary>
    /// Runs the rotation tool on all files of the given format in the given folder and waits for it to finish.
    /// </summary>
    /// <param name="folder">The folder.</param>
    /// <param name="format">The format.</param>
    /// <exception cref="InvalidOperationException">Thrown if the rotation tool could not be started or failed.</exception>
    private static void RunAutoRotate(string folder, string format)
    {
        // The tool treats a pattern without any match as an error, so folders without such files are skipped.
        if (!HasFilesOfFormat(folder, format))
        {
            return;
        }

        var toolDirectory = AppContext.BaseDirectory;
        var startInfo = new ProcessStartInfo
        {
            FileName = Path.Combine(toolDirectory, JheadFileName),
            WorkingDirectory = folder,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        startInfo.ArgumentList.Add("-autorot");
        startInfo.ArgumentList.Add($"*.{format}");

        // The tool runs jpegtran for the rotation itself and looks it up in the path. The working directory is the
        // image folder, so the folder holding both tools has to be added to the path of the child process.
        startInfo.Environment["PATH"] = startInfo.Environment.TryGetValue("PATH", out var path) && !string.IsNullOrWhiteSpace(path)
            ? $"{toolDirectory}{Path.PathSeparator}{path}"
            : toolDirectory;

        using var process = Process.Start(startInfo) ?? throw new InvalidOperationException($"The process {startInfo.FileName} could not be started.");
        var outputReader = process.StandardOutput.ReadToEndAsync();
        var errorReader = process.StandardError.ReadToEndAsync();
        process.WaitForExit();

        if (process.ExitCode == 0)
        {
            return;
        }

        var error = errorReader.GetAwaiter().GetResult();
        var output = outputReader.GetAwaiter().GetResult();
        var details = string.IsNullOrWhiteSpace(error) ? output : error;
        throw new InvalidOperationException($"{JheadFileName} exited with code {process.ExitCode}.{Environment.NewLine}{details.Trim()}");
    }

    /// <summary>
    /// Checks whether the given folder contains at least one file of the given format.
    /// </summary>
    /// <param name="folder">The folder.</param>
    /// <param name="format">The format.</param>
    /// <returns>A value indicating whether the folder contains such a file or not.</returns>
    private static bool HasFilesOfFormat(string folder, string format)
    {
        // The search pattern of the file system also matches longer extensions, so the ending is compared directly.
        return Directory.EnumerateFiles(folder).Any(file => file.EndsWith($".{format}", StringComparison.OrdinalIgnoreCase));
    }
}
