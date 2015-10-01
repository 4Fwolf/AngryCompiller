using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace AngryCompiller
{
    public partial class AngryCompillerDlg : Form
    {
        public AngryCompillerDlg()
        {
            InitializeComponent();
        }

        String fl;
        #region tools
        private void AboutCmpl_Click(object sender, EventArgs e)
        {
            AboutEvil dlg = new AboutEvil();
            dlg.Show();
        }

        private void CMDln_Click(object sender, EventArgs e)
        {
            ProcessStartInfo psiOpt = new ProcessStartInfo(@"cmd.exe");
            Process procCommand = Process.Start(psiOpt);
        }

        private void RunPrj_Click(object sender, EventArgs e)
        {
            //ProcessStartInfo psiOpt = new ProcessStartInfo(@"cmd.exe");
            //Process procCommand = Process.Start(psiOpt);
        }

        private void BuildAndRunPrj_Click(object sender, EventArgs e)
        {
            /*int exit_code = -1;
            SavePrj_Click(sender, e);
            String code_str;
            StreamReader fil = new StreamReader(fl);
            
            for(uint line = 1; fil.EndOfStream != true; ++line)
            {
                code_str = fil.ReadLine();
                exit_code = SyntaxAnalyse(code_str, line);
                if (exit_code > 0) break;
            }

            if (exit_code == 0)
            for(uint line = 1; fil.EndOfStream != true; ++line)
            {
                code_str = fil.ReadLine();
                exit_code = LogicAnalyse(code_str, line);
                if (exit_code > 0) break;
            }

            if (exit_code == 0)
            for(uint line = 1; fil.EndOfStream != true; ++line)
            {
                code_str = fil.ReadLine();
                exit_code = Compile(code_str, line);
                if (exit_code > 0) break;
            }

            if (exit_code == 0)
            {
                StreamReader log = new StreamReader(Directory.GetCurrentDirectory() + "\\EvilProject\\log.txt");
                LogList.Text = log.ReadToEnd();
                StatusInfo.Text = "Builded successful!";
                RunPrj_Click(sender, e);
            }*/
        }

        private void BuildPrj_Click(object sender, EventArgs e)
        {
            /*int exit_code = -1;
            SavePrj_Click(sender, e);
            String code_str;
            StreamReader fil = new StreamReader(fl);
            
            for(uint line = 1; fil.EndOfStream != true; ++line)
            {
                code_str = fil.ReadLine();
                exit_code = SyntaxAnalyse(code_str, line);
                if (exit_code > 0) break;
            }

            if (exit_code == 0)
            for(uint line = 1; fil.EndOfStream != true; ++line)
            {
                code_str = fil.ReadLine();
                exit_code = LogicAnalyse(code_str, line);
                if (exit_code > 0) break;
            }

            if (exit_code == 0)
            for(uint line = 1; fil.EndOfStream != true; ++line)
            {
                code_str = fil.ReadLine();
                exit_code = Compile(code_str, line);
                if (exit_code > 0) break;
            }

            if (exit_code == 0)
            {
                StreamReader log = new StreamReader(Directory.GetCurrentDirectory() + "\\EvilProject\\log.txt");
                LogList.Text = log.ReadToEnd();
                StatusInfo.Text = "Builded successful!";
            }*/
        }

        private void SavePrj_Click(object sender, EventArgs e)
        {
            StreamWriter file = new StreamWriter(fl);

            file.Write(CodeEdit.Text);
            file.Close();

            StatusInfo.Text = "Saved!";
        }

        private void OpenPrj_Click(object sender, EventArgs e)
        {
            OpenFileDialog openprj = new OpenFileDialog();
            openprj.Filter = "*.h5|*.h5";
            openprj.RestoreDirectory = true;
            openprj.InitialDirectory = Directory.GetCurrentDirectory() + "\\EvilProject";
            if (openprj.ShowDialog() == DialogResult.OK)
            {
                StreamReader fil = new StreamReader(openprj.FileName);
                CodeEdit.Text = fil.ReadToEnd();
                //StreamWriter file = new StreamWriter(openprj.FileName);
                //file.Close();

                fl = openprj.FileName;
                CodeEdit.Enabled = true;
                SavePrj.Enabled = true;
                BuildPrj.Enabled = true;
                BuildAndRunPrj.Enabled = true;
                RunPrj.Enabled = true;
                ListFiles.Items.Clear();

                var imageList = new ImageList();
                imageList.Images.Add("itemImageKey", RunPrj.Image);
                ListFiles.SmallImageList = imageList;
                ListViewItem item = new ListViewItem();
                item.ImageKey = "itemImageKey";
                item.SubItems.Add(fl.Substring(fl.LastIndexOf("\\")+1));
                ListFiles.Items.Add(item);
                fil.Close();

                StatusInfo.Text = "Readed!";
            }
        }

        private void NewPrj_Click(object sender, EventArgs e)
        {
            CodeEdit.Text = "";
            SaveFileDialog newprj = new SaveFileDialog();
            newprj.AddExtension = true;
            newprj.DefaultExt = "h5";
            newprj.FileName = "MainEvil";
            newprj.OverwritePrompt = true;
            newprj.Filter = "*.h5|*.h5";
            newprj.RestoreDirectory = true;
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\EvilProject");
            newprj.InitialDirectory = Directory.GetCurrentDirectory() + "\\EvilProject";
            if (newprj.ShowDialog() == DialogResult.OK)
            {
                StreamWriter file = new StreamWriter(newprj.FileName);
                file.Close();
            }
            if (File.Exists(newprj.FileName))
            {
                fl = newprj.FileName;
                CodeEdit.Enabled = true;
                SavePrj.Enabled = true;
                BuildPrj.Enabled = true;
                BuildAndRunPrj.Enabled = true;
                RunPrj.Enabled = true;
                ListFiles.Items.Clear();

                var imageList = new ImageList();
                imageList.Images.Add("itemImageKey", RunPrj.Image);
                ListFiles.SmallImageList = imageList;
                ListViewItem item = new ListViewItem();
                item.ImageKey = "itemImageKey";
                item.SubItems.Add(fl.Substring(fl.LastIndexOf("\\")+1));
                ListFiles.Items.Add(item);

                StatusInfo.Text = "Created!";
            }
        }
        #endregion

        #region parce and compile
        private int SyntaxAnalyse(String code_str, uint line)
        {
            StreamWriter log = new StreamWriter(Directory.GetCurrentDirectory() + "\\EvilProject\\log.txt");
            log.AutoFlush = true;


            return 0;
        }

        private int LogicAnalyse(String code_str, uint line)
        {
            StreamWriter log = new StreamWriter(Directory.GetCurrentDirectory() + "\\EvilProject\\log.txt");
            log.AutoFlush = true;

            return 0;
        }

        private int Compile(String code_str, uint line)
        {
            StreamWriter log = new StreamWriter(Directory.GetCurrentDirectory() + "\\EvilProject\\log.txt");
            log.AutoFlush = true;
            return 0;
        }
        #endregion
    }
}
