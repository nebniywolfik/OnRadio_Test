namespace OnRadio
{
    partial class AboutBox1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox1));
            this.LogoPictureBox = new System.Windows.Forms.PictureBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.TextBoxDescription = new System.Windows.Forms.TextBox();
            this.LabelCopyright = new System.Windows.Forms.Label();
            this.LabelVersion = new System.Windows.Forms.Label();
            this.LabelProductName = new System.Windows.Forms.Label();
            this.LabelCompanyName = new System.Windows.Forms.Label();
            this.TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
            this.TableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LogoPictureBox
            // 
            resources.ApplyResources(this.LogoPictureBox, "LogoPictureBox");
            this.LogoPictureBox.Name = "LogoPictureBox";
            this.LogoPictureBox.TabStop = false;
            // 
            // OKButton
            // 
            resources.ApplyResources(this.OKButton, "OKButton");
            this.OKButton.BackColor = System.Drawing.SystemColors.Control;
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.OKButton.Name = "OKButton";
            this.OKButton.UseVisualStyleBackColor = false;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click_1);
            // 
            // TextBoxDescription
            // 
            this.TextBoxDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.TextBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.TextBoxDescription, "TextBoxDescription");
            this.TextBoxDescription.Name = "TextBoxDescription";
            this.TextBoxDescription.ReadOnly = true;
            this.TextBoxDescription.TabStop = false;
            // 
            // LabelCopyright
            // 
            resources.ApplyResources(this.LabelCopyright, "LabelCopyright");
            this.LabelCopyright.Name = "LabelCopyright";
            // 
            // LabelVersion
            // 
            resources.ApplyResources(this.LabelVersion, "LabelVersion");
            this.LabelVersion.Name = "LabelVersion";
            // 
            // LabelProductName
            // 
            resources.ApplyResources(this.LabelProductName, "LabelProductName");
            this.LabelProductName.Name = "LabelProductName";
            // 
            // LabelCompanyName
            // 
            resources.ApplyResources(this.LabelCompanyName, "LabelCompanyName");
            this.LabelCompanyName.Name = "LabelCompanyName";
            // 
            // TableLayoutPanel
            // 
            resources.ApplyResources(this.TableLayoutPanel, "TableLayoutPanel");
            this.TableLayoutPanel.Controls.Add(this.LogoPictureBox, 0, 0);
            this.TableLayoutPanel.Controls.Add(this.OKButton, 0, 6);
            this.TableLayoutPanel.Controls.Add(this.TextBoxDescription, 0, 5);
            this.TableLayoutPanel.Controls.Add(this.LabelCopyright, 0, 3);
            this.TableLayoutPanel.Controls.Add(this.LabelVersion, 0, 2);
            this.TableLayoutPanel.Controls.Add(this.LabelProductName, 0, 1);
            this.TableLayoutPanel.Controls.Add(this.LabelCompanyName, 0, 4);
            this.TableLayoutPanel.Name = "TableLayoutPanel";
            // 
            // AboutBox1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Controls.Add(this.TableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
            this.TableLayoutPanel.ResumeLayout(false);
            this.TableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.PictureBox LogoPictureBox;
        internal System.Windows.Forms.Button OKButton;
        internal System.Windows.Forms.TextBox TextBoxDescription;
        internal System.Windows.Forms.Label LabelCopyright;
        internal System.Windows.Forms.Label LabelVersion;
        internal System.Windows.Forms.Label LabelProductName;
        internal System.Windows.Forms.Label LabelCompanyName;
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel;
    }
}
