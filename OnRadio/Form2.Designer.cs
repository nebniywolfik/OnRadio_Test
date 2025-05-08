namespace OnRaDiO
{
    partial class About
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.TextBoxDescription = new System.Windows.Forms.TextBox();
            this.TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.LogoPictureBox = new System.Windows.Forms.PictureBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.LabelCopyright = new System.Windows.Forms.Label();
            this.LabelVersion = new System.Windows.Forms.Label();
            this.LabelProductName = new System.Windows.Forms.Label();
            this.LabelCompanyName = new System.Windows.Forms.Label();
            this.TableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // TextBoxDescription
            // 
            this.TextBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBoxDescription.Location = new System.Drawing.Point(6, 217);
            this.TextBoxDescription.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.TextBoxDescription.Multiline = true;
            this.TextBoxDescription.Name = "TextBoxDescription";
            this.TextBoxDescription.ReadOnly = true;
            this.TextBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextBoxDescription.Size = new System.Drawing.Size(254, 51);
            this.TextBoxDescription.TabIndex = 0;
            this.TextBoxDescription.TabStop = false;
            this.TextBoxDescription.Text = "Описание:\r\n\r\n(Во время выполнения текст подписи будет заменен информацией о сборк" +
    "е приложения.\r\nНастроить эту информацию можно в области ";
            // 
            // TableLayoutPanel
            // 
            this.TableLayoutPanel.ColumnCount = 1;
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.16254F));
            this.TableLayoutPanel.Controls.Add(this.LogoPictureBox, 0, 0);
            this.TableLayoutPanel.Controls.Add(this.OKButton, 0, 6);
            this.TableLayoutPanel.Controls.Add(this.TextBoxDescription, 0, 5);
            this.TableLayoutPanel.Controls.Add(this.LabelCopyright, 0, 3);
            this.TableLayoutPanel.Controls.Add(this.LabelVersion, 0, 2);
            this.TableLayoutPanel.Controls.Add(this.LabelProductName, 0, 1);
            this.TableLayoutPanel.Controls.Add(this.LabelCompanyName, 0, 4);
            this.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.TableLayoutPanel.Name = "TableLayoutPanel";
            this.TableLayoutPanel.RowCount = 7;
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.01987F));
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.216495F));
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.560137F));
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.90378F));
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.271523F));
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.86093F));
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.TableLayoutPanel.Size = new System.Drawing.Size(263, 311);
            this.TableLayoutPanel.TabIndex = 1;
            // 
            // LogoPictureBox
            // 
            this.LogoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("LogoPictureBox.Image")));
            this.LogoPictureBox.Location = new System.Drawing.Point(3, 3);
            this.LogoPictureBox.Name = "LogoPictureBox";
            this.LogoPictureBox.Size = new System.Drawing.Size(257, 123);
            this.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LogoPictureBox.TabIndex = 0;
            this.LogoPictureBox.TabStop = false;
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.OKButton.Location = new System.Drawing.Point(185, 285);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 0;
            this.OKButton.Text = "&ОК";
            // 
            // LabelCopyright
            // 
            this.LabelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelCopyright.Location = new System.Drawing.Point(6, 168);
            this.LabelCopyright.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.LabelCopyright.MaximumSize = new System.Drawing.Size(0, 17);
            this.LabelCopyright.Name = "LabelCopyright";
            this.LabelCopyright.Size = new System.Drawing.Size(254, 17);
            this.LabelCopyright.TabIndex = 0;
            this.LabelCopyright.Text = "Авторские права";
            this.LabelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LabelVersion
            // 
            this.LabelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelVersion.Location = new System.Drawing.Point(6, 148);
            this.LabelVersion.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.LabelVersion.MaximumSize = new System.Drawing.Size(0, 17);
            this.LabelVersion.Name = "LabelVersion";
            this.LabelVersion.Size = new System.Drawing.Size(254, 17);
            this.LabelVersion.TabIndex = 0;
            this.LabelVersion.Text = "Версия";
            this.LabelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LabelProductName
            // 
            this.LabelProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelProductName.Location = new System.Drawing.Point(6, 129);
            this.LabelProductName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.LabelProductName.MaximumSize = new System.Drawing.Size(0, 17);
            this.LabelProductName.Name = "LabelProductName";
            this.LabelProductName.Size = new System.Drawing.Size(254, 17);
            this.LabelProductName.TabIndex = 0;
            this.LabelProductName.Text = "Имя продукта";
            this.LabelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LabelCompanyName
            // 
            this.LabelCompanyName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelCompanyName.Location = new System.Drawing.Point(6, 189);
            this.LabelCompanyName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.LabelCompanyName.MaximumSize = new System.Drawing.Size(0, 17);
            this.LabelCompanyName.Name = "LabelCompanyName";
            this.LabelCompanyName.Size = new System.Drawing.Size(254, 17);
            this.LabelCompanyName.TabIndex = 0;
            this.LabelCompanyName.Text = "Название организации";
            this.LabelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(263, 311);
            this.ControlBox = false;
            this.Controls.Add(this.TableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About the program";
            this.TableLayoutPanel.ResumeLayout(false);
            this.TableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TextBox TextBoxDescription;
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel;
        internal System.Windows.Forms.PictureBox LogoPictureBox;
        internal System.Windows.Forms.Button OKButton;
        internal System.Windows.Forms.Label LabelCopyright;
        internal System.Windows.Forms.Label LabelVersion;
        internal System.Windows.Forms.Label LabelProductName;
        internal System.Windows.Forms.Label LabelCompanyName;
    }
}