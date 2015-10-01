namespace AngryCompiller
{
    partial class AboutEvil
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutEvil));
            this.logo_of_evil = new System.Windows.Forms.PictureBox();
            this.ProductName = new System.Windows.Forms.Label();
            this.version_of_evil = new System.Windows.Forms.Label();
            this.copyright_of_evil = new System.Windows.Forms.Label();
            this.mail = new System.Windows.Forms.LinkLabel();
            this.okButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.logo_of_evil)).BeginInit();
            this.SuspendLayout();
            // 
            // logo_of_evil
            // 
            this.logo_of_evil.Dock = System.Windows.Forms.DockStyle.Left;
            this.logo_of_evil.ErrorImage = ((System.Drawing.Image)(resources.GetObject("logo_of_evil.ErrorImage")));
            this.logo_of_evil.Image = ((System.Drawing.Image)(resources.GetObject("logo_of_evil.Image")));
            this.logo_of_evil.InitialImage = ((System.Drawing.Image)(resources.GetObject("logo_of_evil.InitialImage")));
            this.logo_of_evil.Location = new System.Drawing.Point(10, 9);
            this.logo_of_evil.Name = "logo_of_evil";
            this.logo_of_evil.Size = new System.Drawing.Size(90, 130);
            this.logo_of_evil.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logo_of_evil.TabIndex = 27;
            this.logo_of_evil.TabStop = false;
            this.logo_of_evil.Click += new System.EventHandler(this.logo_of_evil_Click);
            // 
            // ProductName
            // 
            this.ProductName.AutoSize = true;
            this.ProductName.Location = new System.Drawing.Point(120, 9);
            this.ProductName.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0);
            this.ProductName.MaximumSize = new System.Drawing.Size(0, 17);
            this.ProductName.Name = "ProductName";
            this.ProductName.Size = new System.Drawing.Size(142, 13);
            this.ProductName.TabIndex = 28;
            this.ProductName.Text = "Product: AngryCompiller";
            this.ProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // version_of_evil
            // 
            this.version_of_evil.AutoSize = true;
            this.version_of_evil.Location = new System.Drawing.Point(120, 33);
            this.version_of_evil.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0);
            this.version_of_evil.MaximumSize = new System.Drawing.Size(0, 17);
            this.version_of_evil.Name = "version_of_evil";
            this.version_of_evil.Size = new System.Drawing.Size(101, 13);
            this.version_of_evil.TabIndex = 26;
            this.version_of_evil.Text = "Version: X.X.X.X";
            this.version_of_evil.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // copyright_of_evil
            // 
            this.copyright_of_evil.AutoSize = true;
            this.copyright_of_evil.Location = new System.Drawing.Point(120, 56);
            this.copyright_of_evil.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0);
            this.copyright_of_evil.MaximumSize = new System.Drawing.Size(0, 17);
            this.copyright_of_evil.Name = "copyright_of_evil";
            this.copyright_of_evil.Size = new System.Drawing.Size(83, 13);
            this.copyright_of_evil.TabIndex = 29;
            this.copyright_of_evil.Text = "Author: IICuX";
            this.copyright_of_evil.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mail
            // 
            this.mail.ActiveLinkColor = System.Drawing.Color.Silver;
            this.mail.AutoSize = true;
            this.mail.LinkColor = System.Drawing.Color.White;
            this.mail.Location = new System.Drawing.Point(120, 82);
            this.mail.Name = "mail";
            this.mail.Size = new System.Drawing.Size(153, 13);
            this.mail.TabIndex = 31;
            this.mail.TabStop = true;
            this.mail.Text = "sasha.halchin@gmail.com";
            this.mail.VisitedLinkColor = System.Drawing.Color.White;
            this.mail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.mail_LinkClicked);
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Location = new System.Drawing.Point(230, 113);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(98, 23);
            this.okButton.TabIndex = 30;
            this.okButton.Text = "&ОК";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // AboutEvil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(341, 148);
            this.Controls.Add(this.logo_of_evil);
            this.Controls.Add(this.ProductName);
            this.Controls.Add(this.version_of_evil);
            this.Controls.Add(this.copyright_of_evil);
            this.Controls.Add(this.mail);
            this.Controls.Add(this.okButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutEvil";
            this.Padding = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About Share Evil";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AboutEvil_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.logo_of_evil)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox logo_of_evil;
        private System.Windows.Forms.Label ProductName;
        private System.Windows.Forms.Label version_of_evil;
        private System.Windows.Forms.Label copyright_of_evil;
        private System.Windows.Forms.LinkLabel mail;
        private System.Windows.Forms.Button okButton;

    }
}
