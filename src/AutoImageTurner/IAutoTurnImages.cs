// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAutoTurnImages.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   An interface to turn images.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AutoImageTurner
{
    /// <summary>
    /// An interface to turn images.
    /// </summary>
    public interface IAutoTurnImages
    {
        /// <summary>
        /// Rotates the images in a folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="format">The format.</param>
        void RotateImagesInFolder(string folder, string format);

        /// <summary>
        /// Rotates the images in a folder and doesn't show messages.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="format">The format.</param>
        void RotateImagesInFolderNoMessage(string folder, string format);
    }
}