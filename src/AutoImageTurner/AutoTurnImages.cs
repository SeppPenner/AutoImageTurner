// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoTurnImages.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   A class to turn images.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AutoImageTurner
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;

    using Languages.Interfaces;

    /// <inheritdoc cref="IAutoTurnImages"/>
    /// <summary>
    /// A class to turn images.
    /// </summary>
    /// <seealso cref="IAutoTurnImages"/>
    public class AutoTurnImages : IAutoTurnImages
    {
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
            var p = new Process
            {
                StartInfo =
                {
                    FileName = "cmd.exe",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                }
            };

            p.Start();
            p.StandardInput.WriteLine("cd " + folder);
            p.StandardInput.WriteLine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "jhead") + " - autorot *." +
                                      format);
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
            var p = new Process
            {
                StartInfo =
                {
                    FileName = "cmd.exe",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                }
            };

            p.Start();
            p.StandardInput.WriteLine("cd " + folder);
            p.StandardInput.WriteLine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "jhead") + " - autorot *." +
                                      format);
        }
    }
}