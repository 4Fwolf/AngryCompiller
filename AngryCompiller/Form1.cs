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
            {
                StatusInfo.Text = "Builded successful!";
                //RunPrj_Click(sender, e);
            }
            else
                StatusInfo.Text = "Build failed!";
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
            #region flags
            bool name = false;
            bool body = false;
            bool end = false;
            bool flg = false;
            bool if_op = false;
            bool else_op = false;
            #endregion
            #region vars
            uint []blk_op = {0,0};
            uint ln = 0;
            int exit_code = 0;
            char[] smb = { ' ', ';' };
            String vars = "";
            #endregion
            StreamWriter log = new StreamWriter(Directory.GetCurrentDirectory() + "\\EvilProject\\log.txt");
            log.AutoFlush = true;
            log.WriteLine("=====================Code Analise=============================");

            String code_str;
            StreamReader fil = new StreamReader(fl);
            #region parse
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
                    #region if
                    if (code_str.Contains("if(") || code_str.Contains("if ("))
                    {
                        if (code_str.StartsWith("if(") == false && code_str.StartsWith("if (") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" missing \" if \" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (code_str.EndsWith(")") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" \")\" not found!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }
                        ln = line;
                        if_op = true;
                    }
                    #endregion
                    #region else
                    if (code_str.Contains("else"))
                    {
                        if (code_str.StartsWith("else") == false || if_op == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" missing \" else \" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (code_str.EndsWith("else") == false || if_op == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" missing \" else \" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }
                        ln = line;
                        else_op = true;
                    }
                    #endregion
                    #region {}
                    if (code_str.Contains("{"))
                    {
                        if(code_str.Length > 1)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" missing \" { \" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (blk_op[0] > 0 && blk_op[1] == 0)
                        {
                            log.Write("Error: line ");
                            log.Write(line-2);
                            log.WriteLine(" missing \" } \" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }
                        ++blk_op[0];
                        blk_op[1] = 0;

                        if (line - ln > 2)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" missing \" { \" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (code_str.StartsWith("{") == false || (if_op == false && else_op == false))
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" missing \" { \" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (code_str.EndsWith("{") == false || (if_op == false && else_op == false))
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" missing \" { \" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }
                    }

                    if (code_str.Contains("}"))
                    {
                        if (code_str.Length > 1)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" missing \" } \" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (code_str.StartsWith("}") == false || blk_op[0] < 1 || blk_op[0] > 1)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" missing \" } \" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (code_str.EndsWith("}") == false || blk_op[0] < 1 || blk_op[0] > 1)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" missing \" } \" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        --blk_op[0];
                        blk_op[1] = 1;
                    }

                    /*if ((blk_op[0] > 0 && blk_op[1] == 0) && fil.EndOfStream)
                    {
                        log.Write("Error: line ");
                        log.Write(ln+1);
                        log.WriteLine(" missing \" { \" operator!");
                        if (exit_code != -1)
                            exit_code = -1;
                    }*/
                    #endregion
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

                        if (char.IsUpper(code_str.Substring(code_str.IndexOf("_", 3), code_str.IndexOf("*") - 1 - code_str.IndexOf("_", 3)), 1) == false ||
                        vars.Contains(code_str.Substring(code_str.IndexOf("_", 3), code_str.IndexOf("*") - 1 - code_str.IndexOf("_", 3))) == false)
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
                    #region /
                    if (code_str.Contains("/"))
                    {
                        if (code_str.Substring(code_str.IndexOf("/") - 1, 3).StartsWith(" ") == false ||
                            code_str.Substring(code_str.IndexOf("/") - 1, 3).EndsWith(" ") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong \" / \" operator!");
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

                        if (char.IsDigit(code_str.Substring(code_str.IndexOf("/") + 2, code_str.IndexOf(";") - (code_str.IndexOf("/") + 2)), 0))
                            flg = true;

                        if (char.IsUpper(code_str.Substring(code_str.IndexOf("_", 3), code_str.IndexOf("/") - 1 - code_str.IndexOf("_", 3)), 1) == false ||
                        vars.Contains(code_str.Substring(code_str.IndexOf("_", 3), code_str.IndexOf("/") - 1 - code_str.IndexOf("_", 3))) == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong variable!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (flg == false)
                        {
                            if (code_str.Substring(code_str.IndexOf("/") + 2).StartsWith("_") == false ||
                            char.IsUpper(code_str.Substring(code_str.IndexOf("/") + 3), 0) == false ||
                            vars.Contains(code_str.Substring(code_str.IndexOf("/") + 2, code_str.IndexOf(";", code_str.IndexOf("/") + 2) - (code_str.IndexOf("/") + 2))) == false)
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
                    #region add
                    if (code_str.Contains("add"))
                    {
                        if (code_str.StartsWith("integer") == false)
                        {
                            if (code_str.Substring(code_str.IndexOf("add") - 1, 5).StartsWith(" ") == false ||
                                code_str.Substring(code_str.IndexOf("add") - 1, 5).EndsWith(" ") == false)
                            {
                                log.Write("Error: line ");
                                log.Write(line);
                                log.WriteLine(" wrong \" add \" operator!");
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

                            if (char.IsDigit(code_str.Substring(code_str.IndexOf("add") + 4, code_str.IndexOf(";") - (code_str.IndexOf("add") + 4)), 0))
                                flg = true;

                            if (char.IsUpper(code_str.Substring(code_str.IndexOf("_", 3), code_str.IndexOf("add") - 1 - code_str.IndexOf("_", 3)), 1) == false ||
                            vars.Contains(code_str.Substring(code_str.IndexOf("_", 3), code_str.IndexOf("add") - 1 - code_str.IndexOf("_", 3))) == false)
                            {
                                log.Write("Error: line ");
                                log.Write(line);
                                log.WriteLine(" wrong variable!");
                                if (exit_code != -1)
                                    exit_code = -1;
                            }

                            if (flg == false)
                            {
                                if (code_str.Substring(code_str.IndexOf("add") + 4).StartsWith("_") == false ||
                                char.IsUpper(code_str.Substring(code_str.IndexOf("add") + 5), 0) == false ||
                                vars.Contains(code_str.Substring(code_str.IndexOf("add") + 4, code_str.IndexOf(";", code_str.IndexOf("add") + 4) - (code_str.IndexOf("add") + 4))) == false)
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
                    #region sub
                    if (code_str.Contains("sub"))
                    {
                        if (code_str.StartsWith("integer") == false)
                        {
                            if (code_str.Substring(code_str.IndexOf("sub") - 1, 5).StartsWith(" ") == false ||
                                code_str.Substring(code_str.IndexOf("sub") - 1, 5).EndsWith(" ") == false)
                            {
                                log.Write("Error: line ");
                                log.Write(line);
                                log.WriteLine(" wrong \" sub \" operator!");
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

                            if (char.IsDigit(code_str.Substring(code_str.IndexOf("sub") + 4, code_str.IndexOf(";") - (code_str.IndexOf("sub") + 4)), 0))
                                flg = true;

                            if (char.IsUpper(code_str.Substring(code_str.IndexOf("_", 3), code_str.IndexOf("sub") - 1 - code_str.IndexOf("_", 3)), 1) == false ||
                            vars.Contains(code_str.Substring(code_str.IndexOf("_", 3), code_str.IndexOf("sub") - 1 - code_str.IndexOf("_", 3))) == false)
                            {
                                log.Write("Error: line ");
                                log.Write(line);
                                log.WriteLine(" wrong variable!");
                                if (exit_code != -1)
                                    exit_code = -1;
                            }

                            if (flg == false)
                            {
                                if (code_str.Substring(code_str.IndexOf("sub") + 4).StartsWith("_") == false ||
                                char.IsUpper(code_str.Substring(code_str.IndexOf("sub") + 5), 0) == false ||
                                vars.Contains(code_str.Substring(code_str.IndexOf("sub") + 4, code_str.IndexOf(";", code_str.IndexOf("sub") + 4) - (code_str.IndexOf("sub") + 4))) == false)
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
                    #region %
                    if (code_str.Contains("%"))
                    {
                        if (code_str.Substring(code_str.IndexOf("%") - 1, 3).StartsWith(" ") == false ||
                            code_str.Substring(code_str.IndexOf("%") - 1, 3).EndsWith(" ") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong \" % \" operator!");
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

                        if (char.IsDigit(code_str.Substring(code_str.IndexOf("%") + 2, code_str.IndexOf(";") - (code_str.IndexOf("%") + 2)), 0))
                            flg = true;

                        if (char.IsUpper(code_str.Substring(code_str.IndexOf("_", 3), code_str.IndexOf("%") - 1 - code_str.IndexOf("_", 3)), 1) == false ||
                        vars.Contains(code_str.Substring(code_str.IndexOf("_", 3), code_str.IndexOf("%") - 1 - code_str.IndexOf("_", 3))) == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong variable!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (flg == false)
                        {
                            if (code_str.Substring(code_str.IndexOf("%") + 2).StartsWith("_") == false ||
                            char.IsUpper(code_str.Substring(code_str.IndexOf("%") + 3), 0) == false ||
                            vars.Contains(code_str.Substring(code_str.IndexOf("%") + 2, code_str.IndexOf(";", code_str.IndexOf("%") + 2) - (code_str.IndexOf("%") + 2))) == false)
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
                    #region == 
                    if (code_str.Contains("=="))
                    {
                        if (code_str.Contains("if(") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong \" == \" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (code_str.Substring(code_str.IndexOf("==") - 1, 4).StartsWith(" ") == false ||
                            code_str.Substring(code_str.IndexOf("==") - 1, 4).EndsWith(" ") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong \" == \" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (char.IsDigit(code_str.Substring(code_str.IndexOf("==") + 3, code_str.IndexOf(")") - (code_str.IndexOf("==") + 3)), 0))
                            flg = true;

                        if (char.IsUpper(code_str.Substring(code_str.IndexOf("_", 3), code_str.IndexOf("==") - 1 - code_str.IndexOf("_", 3)), 1) == false ||
                        vars.Contains(code_str.Substring(code_str.IndexOf("_", 3), code_str.IndexOf("==") - 1 - code_str.IndexOf("_", 3))) == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong variable!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (flg == false)
                        {
                            if (code_str.Substring(code_str.IndexOf("==") + 3).StartsWith("_") == false ||
                            char.IsUpper(code_str.Substring(code_str.IndexOf("==") + 4), 0) == false ||
                            vars.Contains(code_str.Substring(code_str.IndexOf("==") + 3, code_str.IndexOf(")", code_str.IndexOf("==") + 3) - (code_str.IndexOf("==") + 3))) == false)
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
                    #region !=
                    if (code_str.Contains("!="))
                    {
                        if (code_str.Contains("if(") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong \" != \" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (code_str.Substring(code_str.IndexOf("!=") - 1, 4).StartsWith(" ") == false ||
                            code_str.Substring(code_str.IndexOf("!=") - 1, 4).EndsWith(" ") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong \" != \" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (char.IsDigit(code_str.Substring(code_str.IndexOf("!=") + 3, code_str.IndexOf(")") - (code_str.IndexOf("!=") + 3)), 0))
                            flg = true;

                        if (char.IsUpper(code_str.Substring(code_str.IndexOf("_", 3), code_str.IndexOf("!=") - 1 - code_str.IndexOf("_", 3)), 1) == false ||
                        vars.Contains(code_str.Substring(code_str.IndexOf("_", 3), code_str.IndexOf("!=") - 1 - code_str.IndexOf("_", 3))) == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong variable!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (flg == false)
                        {
                            if (code_str.Substring(code_str.IndexOf("!=") + 3).StartsWith("_") == false ||
                            char.IsUpper(code_str.Substring(code_str.IndexOf("!=") + 4), 0) == false ||
                            vars.Contains(code_str.Substring(code_str.IndexOf("!=") + 3, code_str.IndexOf(")", code_str.IndexOf("!=") + 3) - (code_str.IndexOf("!=") + 3))) == false)
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
                    #region le
                    if (code_str.Contains("le"))
                    {
                        if (code_str.StartsWith("integer") == false)
                        {
                            if (code_str.Contains("if(") == false)
                            {
                                log.Write("Error: line ");
                                log.Write(line);
                                log.WriteLine(" wrong \" le \" operator!");
                                if (exit_code != -1)
                                    exit_code = -1;
                            }

                            if (code_str.Substring(code_str.IndexOf("le") - 1, 4).StartsWith(" ") == false ||
                                code_str.Substring(code_str.IndexOf("le") - 1, 4).EndsWith(" ") == false)
                            {
                                log.Write("Error: line ");
                                log.Write(line);
                                log.WriteLine(" wrong \" le \" operator!");
                                if (exit_code != -1)
                                    exit_code = -1;
                            }

                            if (char.IsDigit(code_str.Substring(code_str.IndexOf("le") + 3, code_str.IndexOf(")") - (code_str.IndexOf("le") + 3)), 0))
                                flg = true;

                            if (char.IsUpper(code_str.Substring(code_str.IndexOf("_", 3), code_str.IndexOf("le") - 1 - code_str.IndexOf("_", 3)), 1) == false ||
                            vars.Contains(code_str.Substring(code_str.IndexOf("_", 3), code_str.IndexOf("le") - 1 - code_str.IndexOf("_", 3))) == false)
                            {
                                log.Write("Error: line ");
                                log.Write(line);
                                log.WriteLine(" wrong variable!");
                                if (exit_code != -1)
                                    exit_code = -1;
                            }

                            if (flg == false)
                            {
                                if (code_str.Substring(code_str.IndexOf("le") + 3).StartsWith("_") == false ||
                                char.IsUpper(code_str.Substring(code_str.IndexOf("le") + 4), 0) == false ||
                                vars.Contains(code_str.Substring(code_str.IndexOf("le") + 3, code_str.IndexOf(")", code_str.IndexOf("le") + 3) - (code_str.IndexOf("le") + 3))) == false)
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
                    #region ge
                    if (code_str.Contains("ge"))
                    {
                        if (code_str.StartsWith("integer") == false)
                        {
                            if (code_str.Substring(code_str.IndexOf("ge") - 1, 4).StartsWith(" ") == false ||
                                code_str.Substring(code_str.IndexOf("ge") - 1, 4).EndsWith(" ") == false)
                            {
                                log.Write("Error: line ");
                                log.Write(line);
                                log.WriteLine(" wrong \" ge \" operator!");
                                if (exit_code != -1)
                                    exit_code = -1;
                            }

                            if (code_str.Contains("if(") == false)
                            {
                                log.Write("Error: line ");
                                log.Write(line);
                                log.WriteLine(" wrong \" ge \" operator!");
                                if (exit_code != -1)
                                    exit_code = -1;
                            }

                            if (char.IsDigit(code_str.Substring(code_str.IndexOf("ge") + 3, code_str.IndexOf(")") - (code_str.IndexOf("ge") + 3)), 0))
                                flg = true;

                            if (char.IsUpper(code_str.Substring(code_str.IndexOf("_", 3), code_str.IndexOf("ge") - 1 - code_str.IndexOf("_", 3)), 1) == false ||
                            vars.Contains(code_str.Substring(code_str.IndexOf("_", 3), code_str.IndexOf("ge") - 1 - code_str.IndexOf("_", 3))) == false)
                            {
                                log.Write("Error: line ");
                                log.Write(line);
                                log.WriteLine(" wrong variable!");
                                if (exit_code != -1)
                                    exit_code = -1;
                            }

                            if (flg == false)
                            {
                                if (code_str.Substring(code_str.IndexOf("ge") + 3).StartsWith("_") == false ||
                                char.IsUpper(code_str.Substring(code_str.IndexOf("ge") + 4), 0) == false ||
                                vars.Contains(code_str.Substring(code_str.IndexOf("ge") + 3, code_str.IndexOf(")", code_str.IndexOf("ge") + 3) - (code_str.IndexOf("ge") + 3))) == false)
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
                    #region &
                    if (code_str.Contains("&"))
                    {
                        if (code_str.Contains("if(") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong \" & \" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (code_str.Substring(code_str.IndexOf("&") - 1, 3).StartsWith(" ") == false ||
                            code_str.Substring(code_str.IndexOf("&") - 1, 3).EndsWith(" ") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong \" & \" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (char.IsDigit(code_str.Substring(code_str.IndexOf("&") + 2, code_str.IndexOf(")") - (code_str.IndexOf("&") + 2)), 0))
                            flg = true;

                        if (char.IsUpper(code_str.Substring(code_str.IndexOf("_", 3), code_str.IndexOf("&") - 1 - code_str.IndexOf("_", 3)), 1) == false ||
                        vars.Contains(code_str.Substring(code_str.IndexOf("_", 3), code_str.IndexOf("&") - 1 - code_str.IndexOf("_", 3))) == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong variable!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (flg == false)
                        {
                            if (code_str.Substring(code_str.IndexOf("&") + 2).StartsWith("_") == false ||
                            char.IsUpper(code_str.Substring(code_str.IndexOf("&") + 3), 0) == false ||
                            vars.Contains(code_str.Substring(code_str.IndexOf("&") + 2, code_str.IndexOf(")", code_str.IndexOf("&") + 2) - (code_str.IndexOf("&") + 2))) == false)
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
                    #region |
                    if (code_str.Contains("|"))
                    {
                        if (code_str.Contains("if(") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong \" | \" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (code_str.Substring(code_str.IndexOf("|") - 1, 3).StartsWith(" ") == false ||
                            code_str.Substring(code_str.IndexOf("|") - 1, 3).EndsWith(" ") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong \" | \" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (char.IsDigit(code_str.Substring(code_str.IndexOf("|") + 2, code_str.IndexOf(")") - (code_str.IndexOf("|") + 2)), 0))
                            flg = true;

                        if (char.IsUpper(code_str.Substring(code_str.IndexOf("_", 3), code_str.IndexOf("|") - 1 - code_str.IndexOf("_", 3)), 1) == false ||
                        vars.Contains(code_str.Substring(code_str.IndexOf("_", 3), code_str.IndexOf("|") - 1 - code_str.IndexOf("_", 3))) == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong variable!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (flg == false)
                        {
                            if (code_str.Substring(code_str.IndexOf("|") + 2).StartsWith("_") == false ||
                            char.IsUpper(code_str.Substring(code_str.IndexOf("|") + 3), 0) == false ||
                            vars.Contains(code_str.Substring(code_str.IndexOf("|") + 2, code_str.IndexOf(")", code_str.IndexOf("|") + 2) - (code_str.IndexOf("|") + 2))) == false)
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
                    #region !
                    if (code_str.Contains("!"))
                    {
                        if (code_str.Contains("if(") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong \" ! \" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (code_str.Substring(code_str.IndexOf("!") - 1, 3).StartsWith(" ") == false ||
                            code_str.Substring(code_str.IndexOf("!") - 1, 3).EndsWith(" ") == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong \" ! \" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (char.IsDigit(code_str.Substring(code_str.IndexOf("!") + 2, code_str.IndexOf(")") - (code_str.IndexOf("!") + 2)), 0))
                            flg = true;

                        if (char.IsUpper(code_str.Substring(code_str.IndexOf("_", 3), code_str.IndexOf("!") - 1 - code_str.IndexOf("_", 3)), 1) == false ||
                        vars.Contains(code_str.Substring(code_str.IndexOf("_", 3), code_str.IndexOf("!") - 1 - code_str.IndexOf("_", 3))) == false)
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong variable!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }

                        if (flg == false)
                        {
                            if (code_str.Substring(code_str.IndexOf("!") + 2).StartsWith("_") == false ||
                            char.IsUpper(code_str.Substring(code_str.IndexOf("!") + 3), 0) == false ||
                            vars.Contains(code_str.Substring(code_str.IndexOf("!") + 2, code_str.IndexOf(")", code_str.IndexOf("!") + 2) - (code_str.IndexOf("!") + 2))) == false)
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
            #region {} (last)
            if ((blk_op[0] > 0 && blk_op[1] == 0) && fil.EndOfStream)
            {
                log.Write("Error: line ");
                log.Write(ln + 1);
                log.WriteLine(" missing \" { \" operator!");
                if (exit_code != -1)
                    exit_code = -1;
            }
            #endregion
            #endregion

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
