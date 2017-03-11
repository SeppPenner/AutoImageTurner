using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Languages.Interfaces;

namespace AutoImageTurner
{
    public class AutoTurnImages : IAutoTurnImages
    {
        private readonly ILanguageManager _lm;

        public AutoTurnImages(ILanguageManager languageManager)
        {
            _lm = languageManager;
        }

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
            MessageBox.Show(_lm.GetCurrentLanguage().GetWord("AutorotateFinishedText"),
                _lm.GetCurrentLanguage().GetWord("AutorotateFinishedCaption"), MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

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