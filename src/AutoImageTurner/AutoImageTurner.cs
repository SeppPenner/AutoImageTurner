// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoImageTurner.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The auto image turner form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AutoImageTurner
{
    using System;
    using System.Windows.Forms;

    using Languages.Implementation;
    using Languages.Interfaces;

    /// <summary>
    /// The auto image turner form.
    /// </summary>
    public partial class AutoImageTurner : Form
    {
        /// <summary>
        /// The language manager.
        /// </summary>
        private readonly ILanguageManager languageManager = new LanguageManager();

        /// <summary>
        /// The rotator.
        /// </summary>
        private IAutoTurnImages rotator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoImageTurner"/> class.
        /// </summary>
        public AutoImageTurner()
        {
            this.InitializeComponent();
            this.SetVersionAndName();
            this.InitializeLanguageManager();
            this.LoadLanguagesToCombo();
        }

        /// <summary>
        /// Initializes the language manager.
        /// </summary>
        private void InitializeLanguageManager()
        {
            this.languageManager.SetCurrentLanguage("de-DE");
            this.languageManager.OnLanguageChanged += this.OnLanguageChanged;
            this.rotator = new AutoTurnImages(this.languageManager);
        }

        /// <summary>
        /// Loads the languages to the combo box.
        /// </summary>
        private void LoadLanguagesToCombo()
        {
            foreach (var lang in this.languageManager.GetLanguages())
            {
                this.comboBoxLanguage.Items.Add(lang.Name);
            }

            this.comboBoxLanguage.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the selected language combo box index changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void ComboBoxLanguageSelectedIndexChanged(object sender, EventArgs e)
        {
            this.languageManager.SetCurrentLanguageFromName(this.comboBoxLanguage.SelectedItem.ToString());
        }

        /// <summary>
        /// Handles the language changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        private void OnLanguageChanged(object sender, EventArgs eventArgs)
        {
            this.buttonChooseFolder.Text = this.languageManager.GetCurrentLanguage().GetWord("ChooseFolder");
        }

        /// <summary>
        /// Sets the version name.
        /// </summary>
        private void SetVersionAndName()
        {
            this.Text = Application.ProductName + @" " + Application.ProductVersion;
        }

        /// <summary>
        /// Handles the choose folder click event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void ButtonChooseFolderClick(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();
            
            if (result == DialogResult.OK)
            {
                this.richTextBoxChooseFolder.Text = dialog.SelectedPath;
            }
        }

        /// <summary>
        /// Handles the start button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void ButtonStartClick(object sender, EventArgs e)
        {
            if (!this.StartAllowed())
            {
                return;
            }

            // ReSharper disable once PossibleNullReferenceException
            if (this.comboBoxChooseEnding.SelectedItem.ToString().Equals("All images"))
            {
                foreach (var s in this.comboBoxChooseEnding.Items)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    if (s.ToString().Equals("All images"))
                    {
                        continue;
                    }

                    this.RotateAllKindsofImages(s.ToString());
                }
            }
            else
            {
                this.RotateImagesNormal();
            }
        }

        /// <summary>
        /// Rotate all kind of images.
        /// </summary>
        /// <param name="itemName">The item name.</param>
        private void RotateAllKindsofImages(string itemName)
        {
            if (itemName.Equals("tiff"))
            {
                this.RotateAllKindsofImagesNormal(itemName);
            }
            else
            {
                this.RotateAllKindsofImagesNoMessage(itemName);
            }
        }

        /// <summary>
        /// Rotates all kind of images.
        /// </summary>
        /// <param name="itemName">The item name.</param>
        private void RotateAllKindsofImagesNormal(string itemName)
        {
            try
            {
                this.rotator.RotateImagesInFolder(this.richTextBoxChooseFolder.Text, itemName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.StackTrace, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Rotates all kind of images without messages.
        /// </summary>
        /// <param name="itemName">The item name.</param>
        private void RotateAllKindsofImagesNoMessage(string itemName)
        {
            try
            {
                this.rotator.RotateImagesInFolderNoMessage(this.richTextBoxChooseFolder.Text, itemName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.StackTrace, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Rotates the images.
        /// </summary>
        private void RotateImagesNormal()
        {
            try
            {
                this.rotator.RotateImagesInFolder(this.richTextBoxChooseFolder.Text, this.comboBoxChooseEnding.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.StackTrace, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Checks whether starting is allowed or not.
        /// </summary>
        /// <returns>A value indicating whether the start is allowed or not.</returns>
        private bool StartAllowed()
        {
            if (this.comboBoxChooseEnding.SelectedIndex == -1)
            {
                MessageBox.Show(
                    this.languageManager.GetCurrentLanguage().GetWord("NoEndingSelectedText"),
                    this.languageManager.GetCurrentLanguage().GetWord("NoEndingSelectedCaption"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(this.richTextBoxChooseFolder.Text))
            {
                return true;
            }

            MessageBox.Show(
                this.languageManager.GetCurrentLanguage().GetWord("NoFolderSelectedText"),
                this.languageManager.GetCurrentLanguage().GetWord("NoFolderSelectedCaption"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            return false;
        }
    }
}
