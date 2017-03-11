using System;
using System.Windows.Forms;
using Languages.Implementation;
using Languages.Interfaces;

namespace AutoImageTurner
{
    public partial class AutoImageTurner : Form
    {
        private readonly ILanguageManager _lm = new LanguageManager();
        private IAutoTurnImages _rotator;

        public AutoImageTurner()
        {
            InitializeComponent();
            SetVersionAndName();
            InitializeLanguageManager();
            LoadLanguagesToCombo();
        }

        private void InitializeLanguageManager()
        {
            _lm.SetCurrentLanguage("de-DE");
            _lm.OnLanguageChanged += OnLanguageChanged;
            _rotator = new AutoTurnImages(_lm);
        }

        private void LoadLanguagesToCombo()
        {
            foreach (var lang in _lm.GetLanguages())
                comboBoxLanguage.Items.Add(lang.Name);
            comboBoxLanguage.SelectedIndex = 0;
        }

        private void comboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxLanguage.SelectedItem.ToString())
            {
                case "Deutsch":
                    _lm.SetCurrentLanguage("de-DE");
                    break;
                case "English (US)":
                    _lm.SetCurrentLanguage("en-US");
                    break;
            }
        }

        private void OnLanguageChanged(object sender, EventArgs eventArgs)
        {
            buttonChooseFolder.Text = _lm.GetCurrentLanguage().GetWord("ChooseFolder");
        }

        private void SetVersionAndName()
        {
            Text = Application.ProductName + @" " + Application.ProductVersion;
        }

        private void buttonChooseFolder_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
                richTextBoxChooseFolder.Text = dialog.SelectedPath;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (!StartAllowed()) return;
            if (comboBoxChooseEnding.SelectedItem.ToString().Equals("All images"))
                foreach (var s in comboBoxChooseEnding.Items)
                {
                    if (s.ToString().Equals("All images")) continue;
                    RotateAllKindsofImages(s.ToString());
                }
            else
                RotateImagesNormal();
        }

        private void RotateAllKindsofImages(string itemName)
        {
            if (itemName.Equals("tiff"))
                RotateAllKindsofImagesNormal(itemName);
            else
                RotateAllKindsofImagesNoMessage(itemName);
        }

        private void RotateAllKindsofImagesNormal(string itemName)
        {
            try
            {
                _rotator.RotateImagesInFolder(richTextBoxChooseFolder.Text, itemName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.StackTrace, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RotateAllKindsofImagesNoMessage(string itemName)
        {
            try
            {
                _rotator.RotateImagesInFolderNoMessage(richTextBoxChooseFolder.Text, itemName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.StackTrace, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RotateImagesNormal()
        {
            try
            {
                _rotator.RotateImagesInFolder(richTextBoxChooseFolder.Text, comboBoxChooseEnding.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.StackTrace, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool StartAllowed()
        {
            if (comboBoxChooseEnding.SelectedIndex == -1)
            {
                MessageBox.Show(_lm.GetCurrentLanguage().GetWord("NoEndingSelectedText"),
                    _lm.GetCurrentLanguage().GetWord("NoEndingSelectedCaption"), MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(richTextBoxChooseFolder.Text)) return true;
            MessageBox.Show(_lm.GetCurrentLanguage().GetWord("NoFolderSelectedText"),
                _lm.GetCurrentLanguage().GetWord("NoFolderSelectedCaption"), MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            return false;
        }
    }
}