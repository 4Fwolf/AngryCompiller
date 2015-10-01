namespace AngryCompiller
{
    partial class AngryCompillerDlg
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AngryCompillerDlg));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "<File>"}, -1);
            this.CodeEdit = new System.Windows.Forms.TextBox();
            this.ToolStrp = new System.Windows.Forms.ToolStrip();
            this.NewPrj = new System.Windows.Forms.ToolStripButton();
            this.OpenPrj = new System.Windows.Forms.ToolStripButton();
            this.SavePrj = new System.Windows.Forms.ToolStripButton();
            this.BuildPrj = new System.Windows.Forms.ToolStripButton();
            this.BuildAndRunPrj = new System.Windows.Forms.ToolStripButton();
            this.RunPrj = new System.Windows.Forms.ToolStripButton();
            this.CMDln = new System.Windows.Forms.ToolStripButton();
            this.AboutCmpl = new System.Windows.Forms.ToolStripButton();
            this.StatusStrp = new System.Windows.Forms.StatusStrip();
            this.StatusInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.ListFiles = new System.Windows.Forms.ListView();
            this.column1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LogList = new System.Windows.Forms.TextBox();
            this.ToolStrp.SuspendLayout();
            this.StatusStrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // CodeEdit
            // 
            this.CodeEdit.Enabled = false;
            this.CodeEdit.Location = new System.Drawing.Point(211, 28);
            this.CodeEdit.Multiline = true;
            this.CodeEdit.Name = "CodeEdit";
            this.CodeEdit.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.CodeEdit.Size = new System.Drawing.Size(465, 251);
            this.CodeEdit.TabIndex = 0;
            // 
            // ToolStrp
            // 
            this.ToolStrp.BackColor = System.Drawing.Color.Transparent;
            this.ToolStrp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewPrj,
            this.OpenPrj,
            this.SavePrj,
            this.BuildPrj,
            this.BuildAndRunPrj,
            this.RunPrj,
            this.CMDln,
            this.AboutCmpl});
            this.ToolStrp.Location = new System.Drawing.Point(0, 0);
            this.ToolStrp.Name = "ToolStrp";
            this.ToolStrp.Size = new System.Drawing.Size(688, 25);
            this.ToolStrp.TabIndex = 2;
            this.ToolStrp.Text = "ToolStrp";
            // 
            // NewPrj
            // 
            this.NewPrj.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewPrj.Image = ((System.Drawing.Image)(resources.GetObject("NewPrj.Image")));
            this.NewPrj.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewPrj.Name = "NewPrj";
            this.NewPrj.Size = new System.Drawing.Size(23, 22);
            this.NewPrj.Text = "New";
            this.NewPrj.Click += new System.EventHandler(this.NewPrj_Click);
            // 
            // OpenPrj
            // 
            this.OpenPrj.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenPrj.Image = ((System.Drawing.Image)(resources.GetObject("OpenPrj.Image")));
            this.OpenPrj.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenPrj.Name = "OpenPrj";
            this.OpenPrj.Size = new System.Drawing.Size(23, 22);
            this.OpenPrj.Text = "Open";
            this.OpenPrj.Click += new System.EventHandler(this.OpenPrj_Click);
            // 
            // SavePrj
            // 
            this.SavePrj.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SavePrj.Enabled = false;
            this.SavePrj.Image = ((System.Drawing.Image)(resources.GetObject("SavePrj.Image")));
            this.SavePrj.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SavePrj.Name = "SavePrj";
            this.SavePrj.Size = new System.Drawing.Size(23, 22);
            this.SavePrj.Text = "Save";
            this.SavePrj.Click += new System.EventHandler(this.SavePrj_Click);
            // 
            // BuildPrj
            // 
            this.BuildPrj.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BuildPrj.Enabled = false;
            this.BuildPrj.Image = ((System.Drawing.Image)(resources.GetObject("BuildPrj.Image")));
            this.BuildPrj.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BuildPrj.Name = "BuildPrj";
            this.BuildPrj.Size = new System.Drawing.Size(23, 22);
            this.BuildPrj.Text = "Build";
            this.BuildPrj.Click += new System.EventHandler(this.BuildPrj_Click);
            // 
            // BuildAndRunPrj
            // 
            this.BuildAndRunPrj.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BuildAndRunPrj.Enabled = false;
            this.BuildAndRunPrj.Image = ((System.Drawing.Image)(resources.GetObject("BuildAndRunPrj.Image")));
            this.BuildAndRunPrj.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BuildAndRunPrj.Name = "BuildAndRunPrj";
            this.BuildAndRunPrj.Size = new System.Drawing.Size(23, 22);
            this.BuildAndRunPrj.Text = "Build and run";
            this.BuildAndRunPrj.Click += new System.EventHandler(this.BuildAndRunPrj_Click);
            // 
            // RunPrj
            // 
            this.RunPrj.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RunPrj.Enabled = false;
            this.RunPrj.Image = ((System.Drawing.Image)(resources.GetObject("RunPrj.Image")));
            this.RunPrj.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RunPrj.Name = "RunPrj";
            this.RunPrj.Size = new System.Drawing.Size(23, 22);
            this.RunPrj.Text = "Run";
            this.RunPrj.Click += new System.EventHandler(this.RunPrj_Click);
            // 
            // CMDln
            // 
            this.CMDln.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CMDln.Image = ((System.Drawing.Image)(resources.GetObject("CMDln.Image")));
            this.CMDln.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CMDln.Name = "CMDln";
            this.CMDln.Size = new System.Drawing.Size(23, 22);
            this.CMDln.Text = "CMD";
            this.CMDln.Click += new System.EventHandler(this.CMDln_Click);
            // 
            // AboutCmpl
            // 
            this.AboutCmpl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AboutCmpl.Image = ((System.Drawing.Image)(resources.GetObject("AboutCmpl.Image")));
            this.AboutCmpl.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AboutCmpl.Name = "AboutCmpl";
            this.AboutCmpl.Size = new System.Drawing.Size(23, 22);
            this.AboutCmpl.Text = "About";
            this.AboutCmpl.Click += new System.EventHandler(this.AboutCmpl_Click);
            // 
            // StatusStrp
            // 
            this.StatusStrp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusInfo});
            this.StatusStrp.Location = new System.Drawing.Point(0, 387);
            this.StatusStrp.Name = "StatusStrp";
            this.StatusStrp.Size = new System.Drawing.Size(688, 22);
            this.StatusStrp.TabIndex = 3;
            this.StatusStrp.Text = "StatusStrp";
            // 
            // StatusInfo
            // 
            this.StatusInfo.Name = "StatusInfo";
            this.StatusInfo.Size = new System.Drawing.Size(39, 17);
            this.StatusInfo.Text = "Status";
            // 
            // ListFiles
            // 
            this.ListFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column1,
            this.column2});
            this.ListFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ListFiles.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.ListFiles.Location = new System.Drawing.Point(12, 28);
            this.ListFiles.MultiSelect = false;
            this.ListFiles.Name = "ListFiles";
            this.ListFiles.Scrollable = false;
            this.ListFiles.Size = new System.Drawing.Size(193, 251);
            this.ListFiles.TabIndex = 7;
            this.ListFiles.UseCompatibleStateImageBehavior = false;
            this.ListFiles.View = System.Windows.Forms.View.Details;
            // 
            // column1
            // 
            this.column1.Text = " ";
            this.column1.Width = 19;
            // 
            // column2
            // 
            this.column2.Text = " ";
            this.column2.Width = 170;
            // 
            // LogList
            // 
            this.LogList.Enabled = false;
            this.LogList.Location = new System.Drawing.Point(12, 285);
            this.LogList.Multiline = true;
            this.LogList.Name = "LogList";
            this.LogList.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.LogList.Size = new System.Drawing.Size(664, 99);
            this.LogList.TabIndex = 8;
            // 
            // AngryCompillerDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(688, 409);
            this.Controls.Add(this.LogList);
            this.Controls.Add(this.ListFiles);
            this.Controls.Add(this.StatusStrp);
            this.Controls.Add(this.ToolStrp);
            this.Controls.Add(this.CodeEdit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AngryCompillerDlg";
            this.Text = "AngryCompiller";
            this.ToolStrp.ResumeLayout(false);
            this.ToolStrp.PerformLayout();
            this.StatusStrp.ResumeLayout(false);
            this.StatusStrp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox CodeEdit;
        private System.Windows.Forms.ToolStrip ToolStrp;
        private System.Windows.Forms.StatusStrip StatusStrp;
        private System.Windows.Forms.ToolStripButton NewPrj;
        private System.Windows.Forms.ToolStripButton OpenPrj;
        private System.Windows.Forms.ToolStripButton SavePrj;
        private System.Windows.Forms.ToolStripButton BuildPrj;
        private System.Windows.Forms.ToolStripButton BuildAndRunPrj;
        private System.Windows.Forms.ToolStripButton RunPrj;
        private System.Windows.Forms.ToolStripButton CMDln;
        private System.Windows.Forms.ToolStripButton AboutCmpl;
        private System.Windows.Forms.ToolStripStatusLabel StatusInfo;
        private System.Windows.Forms.SaveFileDialog newprj;
        private System.Windows.Forms.ListView ListFiles;
        private System.Windows.Forms.ColumnHeader column1;
        private System.Windows.Forms.ColumnHeader column2;
        private System.Windows.Forms.TextBox LogList;
    }
}

