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
                LogList.Text = "";
                StreamReader log = new StreamReader(Directory.GetCurrentDirectory() + "\\EvilProject\\log.txt");
                LogList.Text = log.ReadToEnd();
                StatusInfo.Text = "Builded successful!";
                RunPrj_Click(sender, e);
            }*/
        }

        private void BuildPrj_Click(object sender, EventArgs e)
        {
            int exit_code = -1;
            SavePrj_Click(sender, e);

            exit_code = CodeAnalyse();
            
            /*if (exit_code == 0)
                exit_code = Compile();*/

            LogList.Text = "";
            StreamReader log = new StreamReader(Directory.GetCurrentDirectory() + "\\EvilProject\\log.txt");
            LogList.Text = log.ReadToEnd();
            log.Close();

            if (exit_code == 0)
                StatusInfo.Text = "Builded successful!";
            else
                StatusInfo.Text = "Build failed!";
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
            openprj.Filter = "*.h31|*.h31";
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
            newprj.DefaultExt = "h31";
            newprj.FileName = "MainEvil";
            newprj.OverwritePrompt = true;
            newprj.Filter = "*.h31|*.h31";
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

        #region Events
        private void CodeEdit_TextChanged(object sender, EventArgs e)
        {
            //LineStat.Text = "Line: " + CodeEdit.SelectionStart;
            LineStat.Text = "Line: " + (CodeEdit.GetLineFromCharIndex(CodeEdit.GetFirstCharIndexOfCurrentLine())+1);
        }

        private void CodeEdit_MouseCaptureChanged(object sender, EventArgs e)
        {
            CodeEdit_TextChanged(sender, e);
        }

        private void CodeEdit_KeyDown(object sender, KeyEventArgs e)
        {
            CodeEdit_TextChanged(sender, e);
        }

        private void CodeEdit_KeyUp(object sender, KeyEventArgs e)
        {
            CodeEdit_TextChanged(sender, e);
        }
        #endregion

        #region hotkeys
        private void AngryCompillerDlg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N)
                NewPrj_Click(sender, e);
            if (e.Control && e.KeyCode == Keys.O)
                OpenPrj_Click(sender, e);
            if (e.Control && e.KeyCode == Keys.S && SavePrj.Enabled == true)
                SavePrj_Click(sender, e);
            if (e.KeyCode == Keys.F5 && BuildPrj.Enabled == true)
                BuildPrj_Click(sender, e);
            if (e.Control && e.KeyCode == Keys.F5 && BuildAndRunPrj.Enabled == true)
                BuildAndRunPrj_Click(sender, e);
            if (e.KeyCode == Keys.F9 && RunPrj.Enabled == true)
                RunPrj_Click(sender, e);
            if (e.KeyCode == Keys.F12)
                CMDln_Click(sender, e);
            if (e.KeyCode == Keys.F1)
                AboutCmpl_Click(sender, e);
            if (e.Control && e.KeyCode == Keys.E)
            {
                if (SavePrj.Enabled == true)
                    SavePrj_Click(sender, e);
                Close();
            }
        }
        #endregion

        #region parce and compile
        private int CodeAnalyse()
        {
            bool name = false;
            bool body = false;
            bool end = false;
            bool flg = false;
            int exit_code = 0;
            char[] smb = { ' ', ';' };
            String vars = "";
            StreamWriter log = new StreamWriter(Directory.GetCurrentDirectory() + "\\EvilProject\\log.txt");
            log.AutoFlush = true;
            log.WriteLine("=====================Code Analise=============================");

            String code_str;
            StreamReader fil = new StreamReader(fl);

            for (uint line = 1; fil.EndOfStream != true; ++line)
            {
                code_str = fil.ReadLine();

                if (code_str.Equals("") || 
                    code_str.Equals(" ") ||
                    code_str.StartsWith("\\"))
                    continue;

                #region name
                if (name == false)
                {
                    if (code_str.StartsWith("name ") == false)
                    {
                        log.Write("Error: line ");
                        log.Write(line);
                        log.WriteLine(" no directive name!");
                        name = true;
                        if (exit_code != -1)
                            exit_code = -1;
                    }

                    if (code_str.Substring(code_str.IndexOf("<")).EndsWith(">;") == false &&
                        code_str.Substring(code_str.IndexOf("<")).EndsWith("> ;") == false)
                    {
                        log.Write("Error: line ");
                        log.Write(line);
                        log.WriteLine(" wrong name!");
                        if (exit_code != -1)
                            exit_code = -1;
                    }
                    else
                    {
                        name = true;
                        continue;
                    }
                }
                #endregion
                #region body
                if (body == false)
                {
                    if (code_str.StartsWith("body data") == false ||
                        code_str.EndsWith("data") == false)
                    {
                        log.Write("Error: line ");
                        log.Write(line);
                        log.WriteLine(" no directive body!");
                        body = true;
                        if (exit_code != -1)
                            exit_code = -1;
                    }
                    else
                    {
                        body = true;
                        continue;
                    }
                }
                #endregion
                #region code
                if (name == true && body == true && fil.EndOfStream != true)
                {
                    #region integer
                    if (code_str.Contains("integer"))
                    {
                        if (code_str.StartsWith("integer ") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong directive integer!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }
                        else flg = true;

                        if (code_str.EndsWith(";") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" \";\" not found!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (code_str.Substring(8).StartsWith("_") == false ||
                            char.IsUpper(code_str.Substring(9,1), 0) == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong variable name!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }
                        else if (flg == true)
                        { 
                            vars = vars.Insert(vars.Length, code_str.Substring(8, code_str.IndexOfAny(smb, 8) - 8));
                            vars = vars.Insert(vars.Length, " ");
                        }
                        flg = false;
                    }
                    #endregion
                    #region read
                    if (code_str.Contains("read"))
                    {
                        if (code_str.StartsWith("read ") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong directive read!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (code_str.EndsWith(";") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" \";\" not found!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (code_str.Substring(5).StartsWith("_") == false ||
                            char.IsUpper(code_str.Substring(6, 1), 0) == false ||
                            vars.Contains(code_str.Substring(5, code_str.IndexOf(";") - 5)) == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong variable!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }
                    }
                    #endregion
                    #region write
                    if (code_str.Contains("write"))
                    {
                        if (code_str.StartsWith("write ") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong directive write!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (code_str.EndsWith(";") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" \";\" not found!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (code_str.Substring(6).StartsWith("_") == false ||
                            char.IsUpper(code_str.Substring(7, 1), 0) == false ||
                            vars.Contains(code_str.Substring(6, code_str.IndexOf(";") - 6)) == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong variable!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }
                    }
                    #endregion
                    /*#region if
                    if (code_str.Contains("if"))
                    {
                        log.Write("Error: line ");
                        log.Write(line);
                        log.WriteLine(" no directive body!");
                        body = true;
                        if (exit_code != -1)
                            exit_code = -1;
                    }
                    #endregion
                    #region else
                    if (code_str.Contains("else"))
                    {
                        log.Write("Error: line ");
                        log.Write(line);
                        log.WriteLine(" no directive body!");
                        body = true;
                        if (exit_code != -1)
                            exit_code = -1;
                    }
                    #endregion*/
                    #region ->
                    if (code_str.Contains("->"))
                    {
                        if (code_str.Substring(code_str.IndexOf("->") - 1, 4).StartsWith(" ") == false ||
                            code_str.Substring(code_str.IndexOf("->") - 1, 4).EndsWith(" ") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong \" -> \" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (code_str.EndsWith(";") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" \";\" not found!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (char.IsDigit(code_str.Substring(code_str.IndexOf("->") + 3, code_str.IndexOf(";") - (code_str.IndexOf("->") + 3)), 0))
                            flg = true;

                        if (code_str.Contains("integer") == false)
                        {
                            if (code_str.StartsWith("_") == false ||
                            char.IsUpper(code_str.Substring(1, 1), 0) == false ||
                            vars.Contains(code_str.Substring(0, code_str.IndexOf("->")-1)) == false)
                            {
                                log.Write("Error: line ");
                                log.Write(line);
                                log.WriteLine(" wrong variable!");
                                if (exit_code != -1)
                                    exit_code = -1;
                            }
                        }

                        if (flg == false)
                        {
                            if (code_str.Contains("integer") == true)
                            {
                                if (code_str.Substring(code_str.IndexOf("->") + 3).StartsWith("_") == false ||
                                char.IsUpper(code_str.Substring(code_str.IndexOf("->") + 4), 0) == false ||
                                code_str.Substring(code_str.IndexOf("->") + 3, code_str.IndexOf(";") - (code_str.IndexOf("->") + 3)) == code_str.Substring(code_str.IndexOf("_"), code_str.IndexOf("->") - 1 - code_str.IndexOf("_")) ||
                                vars.Contains(code_str.Substring(code_str.IndexOf("->") + 3, code_str.IndexOfAny(smb, code_str.IndexOf("->") + 3) - (code_str.IndexOf("->") + 3))) == false)
                                {
                                    log.Write("Error: line ");
                                    log.Write(line);
                                    log.WriteLine(" wrong value!");
                                    if (exit_code != -1)
                                        exit_code = -1;
                                }
                            }
                            else
                            {
                                if (code_str.Substring(code_str.IndexOf("->") + 3).StartsWith("_") == false ||
                                char.IsUpper(code_str.Substring(code_str.IndexOf("->") + 4), 0) == false ||
                                vars.Contains(code_str.Substring(code_str.IndexOf("->") + 3, code_str.IndexOfAny(smb, code_str.IndexOf("->") + 3) - (code_str.IndexOf("->") + 3))) == false)
                                {
                                    log.Write("Error: line ");
                                    log.Write(line);
                                    log.WriteLine(" wrong value!");
                                    if (exit_code != -1)
                                        exit_code = -1;
                                }
                            }
                        }
                        flg = false;
                    }
                    #endregion
                    #region *
                    if (code_str.Contains("*"))
                    {
                        if (code_str.Substring(code_str.IndexOf("*") - 1, 3).StartsWith(" ") == false ||
                            code_str.Substring(code_str.IndexOf("*") - 1, 3).EndsWith(" ") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong \" * \" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (code_str.EndsWith(";") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" \";\" not found!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (char.IsDigit(code_str.Substring(code_str.IndexOf("*") + 2, code_str.IndexOf(";") - (code_str.IndexOf("*") + 2)), 0))
                            flg = true;

                        if (code_str.Substring(code_str.LastIndexOf).StartsWith("_") == false ||
                        char.IsUpper(code_str.Substring(1, 1), 0) == false ||
                        vars.Contains(code_str.Substring(0, code_str.IndexOf("->") - 1)) == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong variable!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (flg == false)
                        {
                                if (code_str.Substring(code_str.IndexOf("*") + 2).StartsWith("_") == false ||
                                char.IsUpper(code_str.Substring(code_str.IndexOf("*") + 3), 0) == false ||
                                vars.Contains(code_str.Substring(code_str.IndexOf("*") + 2, code_str.IndexOf(";", code_str.IndexOf("*") + 2) - (code_str.IndexOf("*") + 2))) == false)
                                {
                                    log.Write("Error: line ");
                                    log.Write(line);
                                    log.WriteLine(" wrong value!");
                                    if (exit_code != -1)
                                        exit_code = -1;
                                }
                        }
                        flg = false;
                    }
                    #endregion
                    /*#region /
                    if (code_str.Contains("/"))
                    {
                        if (code_str.Substring(code_str.IndexOf("->") - 1, 4).StartsWith(" ") == false ||
                            code_str.Substring(code_str.IndexOf("->") - 1, 4).EndsWith(" ") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong \" -> \" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (char.IsDigit(code_str.Substring(code_str.IndexOf("->") + 3, code_str.IndexOf(";") - (code_str.IndexOf("->") + 3)), 0))
                            flg = true;

                        if (flg == false)
                        {
                            if ((code_str.Substring(code_str.IndexOf("->") + 3).StartsWith("_") == false ||
                            char.IsUpper(code_str.Substring(code_str.IndexOf("->") + 4), 0) == false ||
                            code_str.Substring(code_str.IndexOf("->") + 3, code_str.IndexOf(";") - (code_str.IndexOf("->") + 3)) == code_str.Substring(code_str.IndexOf("_"), code_str.IndexOf("->") - 1 - code_str.IndexOf("_"))))
                            {
                                log.Write("Error: line ");
                                log.Write(line);
                                log.WriteLine(" wrong value!");
                                if (exit_code != -1)
                                    exit_code = -1;
                            }
                        }
                        flg = false;
                    }
                    }
                    #endregion
                    #region add
                    if (code_str.Contains("add"))
                    {
                        if (code_str.Substring(code_str.IndexOf("->") - 1, 4).StartsWith(" ") == false ||
                            code_str.Substring(code_str.IndexOf("->") - 1, 4).EndsWith(" ") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong \" -> \" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (char.IsDigit(code_str.Substring(code_str.IndexOf("->") + 3, code_str.IndexOf(";") - (code_str.IndexOf("->") + 3)), 0))
                            flg = true;

                        if (flg == false)
                        {
                            if ((code_str.Substring(code_str.IndexOf("->") + 3).StartsWith("_") == false ||
                            char.IsUpper(code_str.Substring(code_str.IndexOf("->") + 4), 0) == false ||
                            code_str.Substring(code_str.IndexOf("->") + 3, code_str.IndexOf(";") - (code_str.IndexOf("->") + 3)) == code_str.Substring(code_str.IndexOf("_"), code_str.IndexOf("->") - 1 - code_str.IndexOf("_"))))
                            {
                                log.Write("Error: line ");
                                log.Write(line);
                                log.WriteLine(" wrong value!");
                                if (exit_code != -1)
                                    exit_code = -1;
                            }
                        }
                        flg = false;
                    }
                    }
                    #endregion
                    /*#region sub
                    if (code_str.Contains("sub"))
                    {
                        log.Write("Error: line ");
                        log.Write(line);
                        log.WriteLine(" no directive body!");
                        body = true;
                        if (exit_code != -1)
                            exit_code = -1;
                    }
                    #endregion
                    #region %
                    if (code_str.Contains("%"))
                    {
                        log.Write("Error: line ");
                        log.Write(line);
                        log.WriteLine(" no directive body!");
                        body = true;
                        if (exit_code != -1)
                            exit_code = -1;
                    }
                    #endregion
                    #region ==
                    if (code_str.Contains("=="))
                    {
                        log.Write("Error: line ");
                        log.Write(line);
                        log.WriteLine(" no directive body!");
                        body = true;
                        if (exit_code != -1)
                            exit_code = -1;
                    }
                    #endregion
                    #region !=
                    if (code_str.Contains("!="))
                    {
                        log.Write("Error: line ");
                        log.Write(line);
                        log.WriteLine(" no directive body!");
                        body = true;
                        if (exit_code != -1)
                            exit_code = -1;
                    }
                    #endregion
                    #region le
                    if (code_str.Contains("le"))
                    {
                        log.Write("Error: line ");
                        log.Write(line);
                        log.WriteLine(" no directive body!");
                        body = true;
                        if (exit_code != -1)
                            exit_code = -1;
                    }
                    #endregion
                    #region ge
                    if (code_str.Contains("ge"))
                    {
                        log.Write("Error: line ");
                        log.Write(line);
                        log.WriteLine(" no directive body!");
                        body = true;
                        if (exit_code != -1)
                            exit_code = -1;
                    }
                    #endregion
                    #region &
                    if (code_str.Contains("&"))
                    {
                        log.Write("Error: line ");
                        log.Write(line);
                        log.WriteLine(" no directive body!");
                        body = true;
                        if (exit_code != -1)
                            exit_code = -1;
                    }
                    #endregion
                    #region |
                    if (code_str.Contains("|"))
                    {
                        log.Write("Error: line ");
                        log.Write(line);
                        log.WriteLine(" no directive body!");
                        body = true;
                        if (exit_code != -1)
                            exit_code = -1;
                    }
                    #endregion
                    #region !
                    if (code_str.Contains("!"))
                    {
                        log.Write("Error: line ");
                        log.Write(line);
                        log.WriteLine(" no directive body!");
                        body = true;
                        if (exit_code != -1)
                            exit_code = -1;
                    }
                    #endregion*/
                }
                #endregion
                #region end
                if (end == false && fil.EndOfStream == true)
                {
                    if (code_str.StartsWith("end") == false ||
                        code_str.EndsWith("end") == false)
                    {
                        log.Write("Error: line ");
                        log.Write(line);
                        log.WriteLine(" no directive end!");
                        end = true;
                        if (exit_code != -1)
                            exit_code = -1;
                    }
                    else
                    {
                        end = true;
                        continue;
                    }
                }
                #endregion

            }
            log.WriteLine("============================================================");
            log.Close();
            fil.Close();
            return exit_code;
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
