namespace AutoImageTurner
{
    partial class AutoImageTurner
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoImageTurner));
            this.buttonChooseFolder = new System.Windows.Forms.Button();
            this.richTextBoxChooseFolder = new System.Windows.Forms.RichTextBox();
            this.comboBoxChooseEnding = new System.Windows.Forms.ComboBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonChooseFolder
            // 
            this.buttonChooseFolder.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonChooseFolder.Location = new System.Drawing.Point(3, 3);
            this.buttonChooseFolder.Name = "buttonChooseFolder";
            this.buttonChooseFolder.Size = new System.Drawing.Size(188, 23);
            this.buttonChooseFolder.TabIndex = 0;
            this.buttonChooseFolder.Text = "Choose folder";
            this.buttonChooseFolder.UseVisualStyleBackColor = true;
            this.buttonChooseFolder.Click += new System.EventHandler(this.buttonChooseFolder_Click);
            // 
            // richTextBoxChooseFolder
            // 
            this.richTextBoxChooseFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxChooseFolder.Location = new System.Drawing.Point(197, 3);
            this.richTextBoxChooseFolder.Name = "richTextBoxChooseFolder";
            this.richTextBoxChooseFolder.ReadOnly = true;
            this.richTextBoxChooseFolder.Size = new System.Drawing.Size(252, 154);
            this.richTextBoxChooseFolder.TabIndex = 1;
            this.richTextBoxChooseFolder.Text = "";
            // 
            // comboBoxChooseEnding
            // 
            this.comboBoxChooseEnding.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxChooseEnding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChooseEnding.FormattingEnabled = true;
            this.comboBoxChooseEnding.Items.AddRange(new object[] {
            "All images",
            "bmp",
            "gif",
            "giff",
            "jfif",
            "jpe",
            "jpeg",
            "jpg",
            "png",
            "tif",
            "tiff"});
            this.comboBoxChooseEnding.Location = new System.Drawing.Point(3, 163);
            this.comboBoxChooseEnding.Name = "comboBoxChooseEnding";
            this.comboBoxChooseEnding.Size = new System.Drawing.Size(188, 21);
            this.comboBoxChooseEnding.Sorted = true;
            this.comboBoxChooseEnding.TabIndex = 2;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(197, 163);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(100, 23);
            this.buttonStart.TabIndex = 3;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelMain.ColumnCount = 3;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanelMain.Controls.Add(this.buttonChooseFolder, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.buttonStart, 1, 1);
            this.tableLayoutPanelMain.Controls.Add(this.comboBoxChooseEnding, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.richTextBoxChooseFolder, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.comboBoxLanguage, 2, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(647, 200);
            this.tableLayoutPanelMain.TabIndex = 4;
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Location = new System.Drawing.Point(455, 3);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(159, 21);
            this.comboBoxLanguage.TabIndex = 4;
            this.comboBoxLanguage.SelectedIndexChanged += new System.EventHandler(this.comboBoxLanguage_SelectedIndexChanged);
            // 
            // AutoImageTurner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 200);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AutoImageTurner";
            this.Text = "AutoImageTurner";
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonChooseFolder;
        private System.Windows.Forms.RichTextBox richTextBoxChooseFolder;
        private System.Windows.Forms.ComboBox comboBoxChooseEnding;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
    }
}

