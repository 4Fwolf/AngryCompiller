﻿using System;
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
        StreamWriter log;
        String name_file = "";
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
            ProcessStartInfo psiOpt = new ProcessStartInfo(Directory.GetCurrentDirectory() + "\\EvilProject\\" + name_file.Substring(0, name_file.IndexOf(".")) + ".exe");
            psiOpt.WorkingDirectory = Directory.GetCurrentDirectory() + "\\EvilProject\\";
            Process procCommand = Process.Start(psiOpt);
            procCommand.WaitForExit();
        }
        private void BuildAndRunPrj_Click(object sender, EventArgs e)
        {
            log = new StreamWriter(Directory.GetCurrentDirectory() + "\\EvilProject\\log.txt");
            int exit_code = -1;
            SavePrj_Click(sender, e);

            exit_code = CodeAnalyse();

            if (exit_code == 0)
                exit_code = Compile();
            log.Close();

            LogList.Text = "";
            StreamReader logR = new StreamReader(Directory.GetCurrentDirectory() + "\\EvilProject\\log.txt");
            LogList.Text = logR.ReadToEnd();
            logR.Close();

            if (exit_code == 0)
            {
                StatusInfo.Text = "Builded successful!";
                RunPrj_Click(sender, e);
            }
            else
                StatusInfo.Text = "Build failed!";
        }
        private void BuildPrj_Click(object sender, EventArgs e)
        {
            log = new StreamWriter(Directory.GetCurrentDirectory() + "\\EvilProject\\log.txt");
            int exit_code = -1;
            SavePrj_Click(sender, e);

            exit_code = CodeAnalyse();
            
            if (exit_code == 0)
                exit_code = Compile();
            log.Close();

            LogList.Text = "";
            StreamReader logR = new StreamReader(Directory.GetCurrentDirectory() + "\\EvilProject\\log.txt");
            LogList.Text = logR.ReadToEnd();
            logR.Close();

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
        String vars = "";
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
            int idx = 0;
            #endregion
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
                        else
                        {
                            if (code_str.Substring(8).StartsWith("_") == false ||
                                char.IsUpper(code_str.Substring(9, 1), 0) == false)
                            {
                                log.Write("Error: line ");
                                log.Write(line);
                                log.WriteLine(" uknown variable name!");
                                if (exit_code != -1)
                                    exit_code = -1;
                            }
                            else if (flg == true)
                            {
                                vars = vars.Insert(vars.Length, code_str.Substring(8, code_str.IndexOfAny(smb, 8) - 8));
                                vars = vars.Insert(vars.Length, " ");
                            }
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
                        else
                        {
                            if (code_str.Substring(5).StartsWith("_") == false ||
                                char.IsUpper(code_str.Substring(6, 1), 0) == false ||
                                vars.Contains(code_str.Substring(5, code_str.IndexOf(";") - 5)) == false)
                            {
                                log.Write("Error: line ");
                                log.Write(line);
                                log.WriteLine(" uknown variable!");
                                if (exit_code != -1)
                                    exit_code = -1;
                            }
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
                        else
                        {
                            if (code_str.Substring(6).StartsWith("_") == false ||
                                char.IsUpper(code_str.Substring(7, 1), 0) == false ||
                                vars.Contains(code_str.Substring(6, code_str.IndexOf(";") - 6)) == false)
                            {
                                log.Write("Error: line ");
                                log.Write(line);
                                log.WriteLine(" uknown variable!");
                                if (exit_code != -1)
                                    exit_code = -1;
                            }
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
                        #region Main logic
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
                        else
                        {
                        #endregion

                            if (char.IsDigit(code_str.Substring(code_str.IndexOf("->") + 3, code_str.IndexOf(";") - (code_str.IndexOf("->") + 3)), 0))
                                flg = true;
                            #region LV
                            if (code_str.Contains("integer") == false)
                            {
                                if (code_str.StartsWith("_") == false ||
                                char.IsUpper(code_str.Substring(1, 1), 0) == false ||
                                vars.Contains(code_str.Substring(0, code_str.IndexOf("->") - 1)) == false)
                                {
                                    log.Write("Error: line ");
                                    log.Write(line);
                                    log.WriteLine(" uknown variable!");
                                    if (exit_code != -1)
                                        exit_code = -1;
                                }
                            }
                            #endregion

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
                        }
                        flg = false;
                    }
                    #endregion
                    #region *
                    if (code_str.Contains("*"))
                    {
                        #region Main logic
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
                        else
                        {
                        #endregion

                            String []SplStr = code_str.Split(' ');
                           
                            for (int i = 0; i < SplStr.Length; ++i)
                            {
                                if (SplStr[i] == "*")
                                {
                                    idx = i;
                                    break;
                                }
                            }
                            #region 2 vars
                            if (char.IsDigit(SplStr[idx - 1], 0) == false &&
                                char.IsDigit(SplStr[idx + 1], 0) == false)
                            {
                                flg = true;
                                if (SplStr[idx - 1].StartsWith("_") == false ||
                                    char.IsUpper(SplStr[idx - 1], 1) == false ||
                                    vars.Contains(SplStr[idx - 1]) == false)
                                {
                                    log.Write("Error: line ");
                                    log.Write(line);
                                    log.WriteLine(" uknown variable!");
                                    if (exit_code != -1)
                                        exit_code = -1;
                                }

                                if (SplStr[idx + 1].EndsWith(";") == false)
                                {
                                    if (SplStr[idx + 1].StartsWith("_") == false ||
                                        char.IsUpper(SplStr[idx + 1], 1) == false ||
                                        vars.Contains(SplStr[idx + 1]) == false)
                                    {
                                        log.Write("Error: line ");
                                        log.Write(line);
                                        log.WriteLine(" uknown variable!");
                                        if (exit_code != -1)
                                            exit_code = -1;
                                    }
                                }
                                else
                                {
                                    if (SplStr[idx + 1].StartsWith("_") == false ||
                                        char.IsUpper(SplStr[idx + 1], 1) == false ||
                                        vars.Contains(SplStr[idx + 1].Substring(0, SplStr[idx + 1].Length - 1)) == false)
                                    {
                                        log.Write("Error: line ");
                                        log.Write(line);
                                        log.WriteLine(" uknown variable!");
                                        if (exit_code != -1)
                                            exit_code = -1;
                                    }
                                }
                            }
                            #endregion
                            #region 1 var
                            if ((char.IsDigit(SplStr[idx - 1], 0) == false ||
                                char.IsDigit(SplStr[idx + 1], 0) == false) &&
                                flg == false)
                            {
                                if (char.IsDigit(SplStr[idx - 1], 0) == false)
                                {
                                    if (SplStr[idx - 1].StartsWith("_") == false ||
                                    char.IsUpper(SplStr[idx - 1], 1) == false ||
                                    vars.Contains(SplStr[idx - 1]) == false)
                                    {
                                        log.Write("Error: line ");
                                        log.Write(line);
                                        log.WriteLine(" uknown variable!");
                                        if (exit_code != -1)
                                            exit_code = -1;
                                    }
                                }

                                if (char.IsDigit(SplStr[idx + 1], 0) == false)
                                {
                                    if (SplStr[idx + 1].EndsWith(";") == false)
                                    {
                                        if (SplStr[idx + 1].StartsWith("_") == false ||
                                            char.IsUpper(SplStr[idx + 1], 1) == false ||
                                            vars.Contains(SplStr[idx + 1]) == false)
                                        {
                                            log.Write("Error: line ");
                                            log.Write(line);
                                            log.WriteLine(" uknown variable!");
                                            if (exit_code != -1)
                                                exit_code = -1;
                                        }
                                    }
                                    else
                                    {
                                        if (SplStr[idx + 1].StartsWith("_") == false ||
                                            char.IsUpper(SplStr[idx + 1], 1) == false ||
                                            vars.Contains(SplStr[idx + 1].Substring(0, SplStr[idx + 1].Length - 1)) == false)
                                        {
                                            log.Write("Error: line ");
                                            log.Write(line);
                                            log.WriteLine(" uknown variable!");
                                            if (exit_code != -1)
                                                exit_code = -1;
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                        flg = false;
                    }
                    #endregion
                    #region /
                    if (code_str.Contains("/"))
                    {
                        #region Main logic
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
                        else
                        {
                        #endregion

                            String[] SplStr = code_str.Split(' ');

                            for (int i = 0; i < SplStr.Length; ++i)
                            {
                                if (SplStr[i] == "/")
                                {
                                    idx = i;
                                    break;
                                }
                            }
                            #region 2 vars
                            if (char.IsDigit(SplStr[idx - 1], 0) == false &&
                                char.IsDigit(SplStr[idx + 1], 0) == false)
                            {
                                flg = true;
                                if (SplStr[idx - 1].StartsWith("_") == false ||
                                    char.IsUpper(SplStr[idx - 1], 1) == false ||
                                    vars.Contains(SplStr[idx - 1]) == false)
                                {
                                    log.Write("Error: line ");
                                    log.Write(line);
                                    log.WriteLine(" uknown variable!");
                                    if (exit_code != -1)
                                        exit_code = -1;
                                }

                                if (SplStr[idx + 1].EndsWith(";") == false)
                                {
                                    if (SplStr[idx + 1].StartsWith("_") == false ||
                                        char.IsUpper(SplStr[idx + 1], 1) == false ||
                                        vars.Contains(SplStr[idx + 1]) == false)
                                    {
                                        log.Write("Error: line ");
                                        log.Write(line);
                                        log.WriteLine(" uknown variable!");
                                        if (exit_code != -1)
                                            exit_code = -1;
                                    }
                                }
                                else
                                {
                                    if (SplStr[idx + 1].StartsWith("_") == false ||
                                        char.IsUpper(SplStr[idx + 1], 1) == false ||
                                        vars.Contains(SplStr[idx + 1].Substring(0, SplStr[idx + 1].Length - 1)) == false)
                                    {
                                        log.Write("Error: line ");
                                        log.Write(line);
                                        log.WriteLine(" uknown variable!");
                                        if (exit_code != -1)
                                            exit_code = -1;
                                    }
                                }
                            }
                            #endregion
                            #region 1 var
                            if ((char.IsDigit(SplStr[idx - 1], 0) == false ||
                                char.IsDigit(SplStr[idx + 1], 0) == false) &&
                                flg == false)
                            {
                                if (char.IsDigit(SplStr[idx - 1], 0) == false)
                                {
                                    if (SplStr[idx - 1].StartsWith("_") == false ||
                                    char.IsUpper(SplStr[idx - 1], 1) == false ||
                                    vars.Contains(SplStr[idx - 1]) == false)
                                    {
                                        log.Write("Error: line ");
                                        log.Write(line);
                                        log.WriteLine(" uknown variable!");
                                        if (exit_code != -1)
                                            exit_code = -1;
                                    }
                                }

                                if (char.IsDigit(SplStr[idx + 1], 0) == false)
                                {
                                    if (SplStr[idx + 1].EndsWith(";") == false)
                                    {
                                        if (SplStr[idx + 1].StartsWith("_") == false ||
                                            char.IsUpper(SplStr[idx + 1], 1) == false ||
                                            vars.Contains(SplStr[idx + 1]) == false)
                                        {
                                            log.Write("Error: line ");
                                            log.Write(line);
                                            log.WriteLine(" uknown variable!");
                                            if (exit_code != -1)
                                                exit_code = -1;
                                        }
                                    }
                                    else
                                    {
                                        if (SplStr[idx + 1].StartsWith("_") == false ||
                                            char.IsUpper(SplStr[idx + 1], 1) == false ||
                                            vars.Contains(SplStr[idx + 1].Substring(0, SplStr[idx + 1].Length - 1)) == false)
                                        {
                                            log.Write("Error: line ");
                                            log.Write(line);
                                            log.WriteLine(" uknown variable!");
                                            if (exit_code != -1)
                                                exit_code = -1;
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                        flg = false;
                    }
                    #endregion
                    #region add
                    if (code_str.Contains("add"))
                    {
                        if (code_str.StartsWith("integer") == false)
                        {
                            #region Main logic
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
                            else
                            {
                            #endregion
                                String[] SplStr = code_str.Split(' ');

                                for (int i = 0; i < SplStr.Length; ++i)
                                {
                                    if (SplStr[i] == "add")
                                    {
                                        idx = i;
                                        break;
                                    }
                                }
                                #region 2 vars
                                if (char.IsDigit(SplStr[idx - 1], 0) == false &&
                                    char.IsDigit(SplStr[idx + 1], 0) == false)
                                {
                                    flg = true;
                                    if (SplStr[idx - 1].StartsWith("_") == false ||
                                        char.IsUpper(SplStr[idx - 1], 1) == false ||
                                        vars.Contains(SplStr[idx - 1]) == false)
                                    {
                                        log.Write("Error: line ");
                                        log.Write(line);
                                        log.WriteLine(" uknown variable!");
                                        if (exit_code != -1)
                                            exit_code = -1;
                                    }

                                    if (SplStr[idx + 1].EndsWith(";") == false)
                                    {
                                        if (SplStr[idx + 1].StartsWith("_") == false ||
                                            char.IsUpper(SplStr[idx + 1], 1) == false ||
                                            vars.Contains(SplStr[idx + 1]) == false)
                                        {
                                            log.Write("Error: line ");
                                            log.Write(line);
                                            log.WriteLine(" uknown variable!");
                                            if (exit_code != -1)
                                                exit_code = -1;
                                        }
                                    }
                                    else
                                    {
                                        if (SplStr[idx + 1].StartsWith("_") == false ||
                                            char.IsUpper(SplStr[idx + 1], 1) == false ||
                                            vars.Contains(SplStr[idx + 1].Substring(0, SplStr[idx + 1].Length - 1)) == false)
                                        {
                                            log.Write("Error: line ");
                                            log.Write(line);
                                            log.WriteLine(" uknown variable!");
                                            if (exit_code != -1)
                                                exit_code = -1;
                                        }
                                    }
                                }
                                #endregion
                                #region 1 var
                                if ((char.IsDigit(SplStr[idx - 1], 0) == false ||
                                    char.IsDigit(SplStr[idx + 1], 0) == false) &&
                                    flg == false)
                                {
                                    if (char.IsDigit(SplStr[idx - 1], 0) == false)
                                    {
                                        if (SplStr[idx - 1].StartsWith("_") == false ||
                                        char.IsUpper(SplStr[idx - 1], 1) == false ||
                                        vars.Contains(SplStr[idx - 1]) == false)
                                        {
                                            log.Write("Error: line ");
                                            log.Write(line);
                                            log.WriteLine(" uknown variable!");
                                            if (exit_code != -1)
                                                exit_code = -1;
                                        }
                                    }

                                    if (char.IsDigit(SplStr[idx + 1], 0) == false)
                                    {
                                        if (SplStr[idx + 1].EndsWith(";") == false)
                                        {
                                            if (SplStr[idx + 1].StartsWith("_") == false ||
                                                char.IsUpper(SplStr[idx + 1], 1) == false ||
                                                vars.Contains(SplStr[idx + 1]) == false)
                                            {
                                                log.Write("Error: line ");
                                                log.Write(line);
                                                log.WriteLine(" uknown variable!");
                                                if (exit_code != -1)
                                                    exit_code = -1;
                                            }
                                        }
                                        else
                                        {
                                            if (SplStr[idx + 1].StartsWith("_") == false ||
                                                char.IsUpper(SplStr[idx + 1], 1) == false ||
                                                vars.Contains(SplStr[idx + 1].Substring(0, SplStr[idx + 1].Length - 1)) == false)
                                            {
                                                log.Write("Error: line ");
                                                log.Write(line);
                                                log.WriteLine(" uknown variable!");
                                                if (exit_code != -1)
                                                    exit_code = -1;
                                            }
                                        }
                                    }
                                }
                                #endregion
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
                            #region Main logic
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
                            else
                            {
                            #endregion
                                String[] SplStr = code_str.Split(' ');

                                for (int i = 0; i < SplStr.Length; ++i)
                                {
                                    if (SplStr[i] == "sub")
                                    {
                                        idx = i;
                                        break;
                                    }
                                }
                                #region 2 vars
                                if (char.IsDigit(SplStr[idx - 1], 0) == false &&
                                    char.IsDigit(SplStr[idx + 1], 0) == false)
                                {
                                    flg = true;
                                    if (SplStr[idx - 1].StartsWith("_") == false ||
                                        char.IsUpper(SplStr[idx - 1], 1) == false ||
                                        vars.Contains(SplStr[idx - 1]) == false)
                                    {
                                        log.Write("Error: line ");
                                        log.Write(line);
                                        log.WriteLine(" uknown variable!");
                                        if (exit_code != -1)
                                            exit_code = -1;
                                    }

                                    if (SplStr[idx + 1].EndsWith(";") == false)
                                    {
                                        if (SplStr[idx + 1].StartsWith("_") == false ||
                                            char.IsUpper(SplStr[idx + 1], 1) == false ||
                                            vars.Contains(SplStr[idx + 1]) == false)
                                        {
                                            log.Write("Error: line ");
                                            log.Write(line);
                                            log.WriteLine(" uknown variable!");
                                            if (exit_code != -1)
                                                exit_code = -1;
                                        }
                                    }
                                    else
                                    {
                                        if (SplStr[idx + 1].StartsWith("_") == false ||
                                            char.IsUpper(SplStr[idx + 1], 1) == false ||
                                            vars.Contains(SplStr[idx + 1].Substring(0, SplStr[idx + 1].Length - 1)) == false)
                                        {
                                            log.Write("Error: line ");
                                            log.Write(line);
                                            log.WriteLine(" uknown variable!");
                                            if (exit_code != -1)
                                                exit_code = -1;
                                        }
                                    }
                                }
                                #endregion
                                #region 1 var
                                if ((char.IsDigit(SplStr[idx - 1], 0) == false ||
                                    char.IsDigit(SplStr[idx + 1], 0) == false) &&
                                    flg == false)
                                {
                                    if (char.IsDigit(SplStr[idx - 1], 0) == false)
                                    {
                                        if (SplStr[idx - 1].StartsWith("_") == false ||
                                        char.IsUpper(SplStr[idx - 1], 1) == false ||
                                        vars.Contains(SplStr[idx - 1]) == false)
                                        {
                                            log.Write("Error: line ");
                                            log.Write(line);
                                            log.WriteLine(" uknown variable!");
                                            if (exit_code != -1)
                                                exit_code = -1;
                                        }
                                    }

                                    if (char.IsDigit(SplStr[idx + 1], 0) == false)
                                    {
                                        if (SplStr[idx + 1].EndsWith(";") == false)
                                        {
                                            if (SplStr[idx + 1].StartsWith("_") == false ||
                                                char.IsUpper(SplStr[idx + 1], 1) == false ||
                                                vars.Contains(SplStr[idx + 1]) == false)
                                            {
                                                log.Write("Error: line ");
                                                log.Write(line);
                                                log.WriteLine(" uknown variable!");
                                                if (exit_code != -1)
                                                    exit_code = -1;
                                            }
                                        }
                                        else
                                        {
                                            if (SplStr[idx + 1].StartsWith("_") == false ||
                                                char.IsUpper(SplStr[idx + 1], 1) == false ||
                                                vars.Contains(SplStr[idx + 1].Substring(0, SplStr[idx + 1].Length - 1)) == false)
                                            {
                                                log.Write("Error: line ");
                                                log.Write(line);
                                                log.WriteLine(" uknown variable!");
                                                if (exit_code != -1)
                                                    exit_code = -1;
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                            flg = false;
                        }
                    }
                    #endregion
                    #region %
                    if (code_str.Contains("%"))
                    {
                        #region Main logic
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
                        else
                        {
                        #endregion
                            String[] SplStr = code_str.Split(' ');

                            for (int i = 0; i < SplStr.Length; ++i)
                            {
                                if (SplStr[i] == "%")
                                {
                                    idx = i;
                                    break;
                                }
                            }
                            #region 2 vars
                            if (char.IsDigit(SplStr[idx - 1], 0) == false &&
                                char.IsDigit(SplStr[idx + 1], 0) == false)
                            {
                                flg = true;
                                if (SplStr[idx - 1].StartsWith("_") == false ||
                                    char.IsUpper(SplStr[idx - 1], 1) == false ||
                                    vars.Contains(SplStr[idx - 1]) == false)
                                {
                                    log.Write("Error: line ");
                                    log.Write(line);
                                    log.WriteLine(" uknown variable!");
                                    if (exit_code != -1)
                                        exit_code = -1;
                                }

                                if (SplStr[idx + 1].EndsWith(";") == false)
                                {
                                    if (SplStr[idx + 1].StartsWith("_") == false ||
                                        char.IsUpper(SplStr[idx + 1], 1) == false ||
                                        vars.Contains(SplStr[idx + 1]) == false)
                                    {
                                        log.Write("Error: line ");
                                        log.Write(line);
                                        log.WriteLine(" uknown variable!");
                                        if (exit_code != -1)
                                            exit_code = -1;
                                    }
                                }
                                else
                                {
                                    if (SplStr[idx + 1].StartsWith("_") == false ||
                                        char.IsUpper(SplStr[idx + 1], 1) == false ||
                                        vars.Contains(SplStr[idx + 1].Substring(0, SplStr[idx + 1].Length - 1)) == false)
                                    {
                                        log.Write("Error: line ");
                                        log.Write(line);
                                        log.WriteLine(" uknown variable!");
                                        if (exit_code != -1)
                                            exit_code = -1;
                                    }
                                }
                            }
                            #endregion
                            #region 1 var
                            if ((char.IsDigit(SplStr[idx - 1], 0) == false ||
                                char.IsDigit(SplStr[idx + 1], 0) == false) &&
                                flg == false)
                            {
                                if (char.IsDigit(SplStr[idx - 1], 0) == false)
                                {
                                    if (SplStr[idx - 1].StartsWith("_") == false ||
                                    char.IsUpper(SplStr[idx - 1], 1) == false ||
                                    vars.Contains(SplStr[idx - 1]) == false)
                                    {
                                        log.Write("Error: line ");
                                        log.Write(line);
                                        log.WriteLine(" uknown variable!");
                                        if (exit_code != -1)
                                            exit_code = -1;
                                    }
                                }

                                if (char.IsDigit(SplStr[idx + 1], 0) == false)
                                {
                                    if (SplStr[idx + 1].EndsWith(";") == false)
                                    {
                                        if (SplStr[idx + 1].StartsWith("_") == false ||
                                            char.IsUpper(SplStr[idx + 1], 1) == false ||
                                            vars.Contains(SplStr[idx + 1]) == false)
                                        {
                                            log.Write("Error: line ");
                                            log.Write(line);
                                            log.WriteLine(" uknown variable!");
                                            if (exit_code != -1)
                                                exit_code = -1;
                                        }
                                    }
                                    else
                                    {
                                        if (SplStr[idx + 1].StartsWith("_") == false ||
                                            char.IsUpper(SplStr[idx + 1], 1) == false ||
                                            vars.Contains(SplStr[idx + 1].Substring(0, SplStr[idx + 1].Length - 1)) == false)
                                        {
                                            log.Write("Error: line ");
                                            log.Write(line);
                                            log.WriteLine(" uknown variable!");
                                            if (exit_code != -1)
                                                exit_code = -1;
                                        }
                                    }
                                }
                            }
                            #endregion
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

                        String[] SplStr = code_str.Split(' ');

                        for (int i = 0; i < SplStr.Length; ++i)
                        {
                            if (SplStr[i] == "==")
                            {
                                idx = i;
                                break;
                            }
                        }
                        #region 2 vars
                        if (char.IsDigit(SplStr[idx - 1], 0) == false &&
                            char.IsDigit(SplStr[idx + 1], 0) == false)
                        {
                            flg = true;
                            if (SplStr[idx - 1].StartsWith("_") == false ||
                                char.IsUpper(SplStr[idx - 1], 1) == false ||
                                vars.Contains(SplStr[idx - 1]) == false)
                            {
                                log.Write("Error: line ");
                                log.Write(line);
                                log.WriteLine(" uknown variable!");
                                if (exit_code != -1)
                                    exit_code = -1;
                            }

                            if (SplStr[idx + 1].EndsWith(")") == false)
                            {
                                if (SplStr[idx + 1].StartsWith("_") == false ||
                                    char.IsUpper(SplStr[idx + 1], 1) == false ||
                                    vars.Contains(SplStr[idx + 1]) == false)
                                {
                                    log.Write("Error: line ");
                                    log.Write(line);
                                    log.WriteLine(" uknown variable!");
                                    if (exit_code != -1)
                                        exit_code = -1;
                                }
                            }
                            else
                            {
                                if (SplStr[idx + 1].StartsWith("_") == false ||
                                    char.IsUpper(SplStr[idx + 1], 1) == false ||
                                    vars.Contains(SplStr[idx + 1].Substring(0, SplStr[idx + 1].Length - 1)) == false)
                                {
                                    log.Write("Error: line ");
                                    log.Write(line);
                                    log.WriteLine(" uknown variable!");
                                    if (exit_code != -1)
                                        exit_code = -1;
                                }
                            }
                        }
                        #endregion
                        #region 1 var
                        if ((char.IsDigit(SplStr[idx - 1], 0) == false ||
                            char.IsDigit(SplStr[idx + 1], 0) == false) &&
                            flg == false)
                        {
                            if (char.IsDigit(SplStr[idx - 1], 0) == false)
                            {
                                if (SplStr[idx - 1].StartsWith("_") == false ||
                                char.IsUpper(SplStr[idx - 1], 1) == false ||
                                vars.Contains(SplStr[idx - 1]) == false)
                                {
                                    log.Write("Error: line ");
                                    log.Write(line);
                                    log.WriteLine(" uknown variable!");
                                    if (exit_code != -1)
                                        exit_code = -1;
                                }
                            }

                            if (char.IsDigit(SplStr[idx + 1], 0) == false)
                            {
                                if (SplStr[idx + 1].EndsWith(")") == false)
                                {
                                    if (SplStr[idx + 1].StartsWith("_") == false ||
                                        char.IsUpper(SplStr[idx + 1], 1) == false ||
                                        vars.Contains(SplStr[idx + 1]) == false)
                                    {
                                        log.Write("Error: line ");
                                        log.Write(line);
                                        log.WriteLine(" uknown variable!");
                                        if (exit_code != -1)
                                            exit_code = -1;
                                    }
                                }
                                else
                                {
                                    if (SplStr[idx + 1].StartsWith("_") == false ||
                                        char.IsUpper(SplStr[idx + 1], 1) == false ||
                                        vars.Contains(SplStr[idx + 1].Substring(0, SplStr[idx + 1].Length - 1)) == false)
                                    {
                                        log.Write("Error: line ");
                                        log.Write(line);
                                        log.WriteLine(" uknown variable!");
                                        if (exit_code != -1)
                                            exit_code = -1;
                                    }
                                }
                            }
                        }
                        #endregion
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

                        String[] SplStr = code_str.Split(' ');

                        for (int i = 0; i < SplStr.Length; ++i)
                        {
                            if (SplStr[i] == "!=")
                            {
                                idx = i;
                                break;
                            }
                        }
                        #region 2 vars
                        if (char.IsDigit(SplStr[idx - 1], 0) == false &&
                            char.IsDigit(SplStr[idx + 1], 0) == false)
                        {
                            flg = true;
                            if (SplStr[idx - 1].StartsWith("_") == false ||
                                char.IsUpper(SplStr[idx - 1], 1) == false ||
                                vars.Contains(SplStr[idx - 1]) == false)
                            {
                                log.Write("Error: line ");
                                log.Write(line);
                                log.WriteLine(" uknown variable!");
                                if (exit_code != -1)
                                    exit_code = -1;
                            }

                            if (SplStr[idx + 1].EndsWith(")") == false)
                            {
                                if (SplStr[idx + 1].StartsWith("_") == false ||
                                    char.IsUpper(SplStr[idx + 1], 1) == false ||
                                    vars.Contains(SplStr[idx + 1]) == false)
                                {
                                    log.Write("Error: line ");
                                    log.Write(line);
                                    log.WriteLine(" uknown variable!");
                                    if (exit_code != -1)
                                        exit_code = -1;
                                }
                            }
                            else
                            {
                                if (SplStr[idx + 1].StartsWith("_") == false ||
                                    char.IsUpper(SplStr[idx + 1], 1) == false ||
                                    vars.Contains(SplStr[idx + 1].Substring(0, SplStr[idx + 1].Length - 1)) == false)
                                {
                                    log.Write("Error: line ");
                                    log.Write(line);
                                    log.WriteLine(" uknown variable!");
                                    if (exit_code != -1)
                                        exit_code = -1;
                                }
                            }
                        }
                        #endregion
                        #region 1 var
                        if ((char.IsDigit(SplStr[idx - 1], 0) == false ||
                            char.IsDigit(SplStr[idx + 1], 0) == false) &&
                            flg == false)
                        {
                            if (char.IsDigit(SplStr[idx - 1], 0) == false)
                            {
                                if (SplStr[idx - 1].StartsWith("_") == false ||
                                char.IsUpper(SplStr[idx - 1], 1) == false ||
                                vars.Contains(SplStr[idx - 1]) == false)
                                {
                                    log.Write("Error: line ");
                                    log.Write(line);
                                    log.WriteLine(" uknown variable!");
                                    if (exit_code != -1)
                                        exit_code = -1;
                                }
                            }

                            if (char.IsDigit(SplStr[idx + 1], 0) == false)
                            {
                                if (SplStr[idx + 1].EndsWith(")") == false)
                                {
                                    if (SplStr[idx + 1].StartsWith("_") == false ||
                                        char.IsUpper(SplStr[idx + 1], 1) == false ||
                                        vars.Contains(SplStr[idx + 1]) == false)
                                    {
                                        log.Write("Error: line ");
                                        log.Write(line);
                                        log.WriteLine(" uknown variable!");
                                        if (exit_code != -1)
                                            exit_code = -1;
                                    }
                                }
                                else
                                {
                                    if (SplStr[idx + 1].StartsWith("_") == false ||
                                        char.IsUpper(SplStr[idx + 1], 1) == false ||
                                        vars.Contains(SplStr[idx + 1].Substring(0, SplStr[idx + 1].Length - 1)) == false)
                                    {
                                        log.Write("Error: line ");
                                        log.Write(line);
                                        log.WriteLine(" uknown variable!");
                                        if (exit_code != -1)
                                            exit_code = -1;
                                    }
                                }
                            }
                        }
                        #endregion
                        flg = false;
                    }
                    #endregion
                    #region le
                    if (code_str.Contains("le"))
                    {
                        if (!code_str.StartsWith("integer"))
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

                            String[] SplStr = code_str.Split(' ');

                            for (int i = 0; i < SplStr.Length; ++i)
                            {
                                if (SplStr[i] == "le")
                                {
                                    idx = i;
                                    break;
                                }
                            }
                            #region 2 vars
                            if (char.IsDigit(SplStr[idx - 1], 0) == false &&
                                char.IsDigit(SplStr[idx + 1], 0) == false)
                            {
                                flg = true;
                                if (SplStr[idx - 1].StartsWith("_") == false ||
                                    char.IsUpper(SplStr[idx - 1], 1) == false ||
                                    vars.Contains(SplStr[idx - 1]) == false)
                                {
                                    log.Write("Error: line ");
                                    log.Write(line);
                                    log.WriteLine(" uknown variable!");
                                    if (exit_code != -1)
                                        exit_code = -1;
                                }

                                if (SplStr[idx + 1].EndsWith(")") == false)
                                {
                                    if (SplStr[idx + 1].StartsWith("_") == false ||
                                        char.IsUpper(SplStr[idx + 1], 1) == false ||
                                        vars.Contains(SplStr[idx + 1]) == false)
                                    {
                                        log.Write("Error: line ");
                                        log.Write(line);
                                        log.WriteLine(" uknown variable!");
                                        if (exit_code != -1)
                                            exit_code = -1;
                                    }
                                }
                                else
                                {
                                    if (SplStr[idx + 1].StartsWith("_") == false ||
                                        char.IsUpper(SplStr[idx + 1], 1) == false ||
                                        vars.Contains(SplStr[idx + 1].Substring(0, SplStr[idx + 1].Length - 1)) == false)
                                    {
                                        log.Write("Error: line ");
                                        log.Write(line);
                                        log.WriteLine(" uknown variable!");
                                        if (exit_code != -1)
                                            exit_code = -1;
                                    }
                                }
                            }
                            #endregion
                            #region 1 var
                            if ((char.IsDigit(SplStr[idx - 1], 0) == false ||
                                char.IsDigit(SplStr[idx + 1], 0) == false) &&
                                flg == false)
                            {
                                if (char.IsDigit(SplStr[idx - 1], 0) == false)
                                {
                                    if (SplStr[idx - 1].StartsWith("_") == false ||
                                    char.IsUpper(SplStr[idx - 1], 1) == false ||
                                    vars.Contains(SplStr[idx - 1]) == false)
                                    {
                                        log.Write("Error: line ");
                                        log.Write(line);
                                        log.WriteLine(" uknown variable!");
                                        if (exit_code != -1)
                                            exit_code = -1;
                                    }
                                }

                                if (char.IsDigit(SplStr[idx + 1], 0) == false)
                                {
                                    if (SplStr[idx + 1].EndsWith(")") == false)
                                    {
                                        if (SplStr[idx + 1].StartsWith("_") == false ||
                                            char.IsUpper(SplStr[idx + 1], 1) == false ||
                                            vars.Contains(SplStr[idx + 1]) == false)
                                        {
                                            log.Write("Error: line ");
                                            log.Write(line);
                                            log.WriteLine(" uknown variable!");
                                            if (exit_code != -1)
                                                exit_code = -1;
                                        }
                                    }
                                    else
                                    {
                                        if (SplStr[idx + 1].StartsWith("_") == false ||
                                            char.IsUpper(SplStr[idx + 1], 1) == false ||
                                            vars.Contains(SplStr[idx + 1].Substring(0, SplStr[idx + 1].Length - 1)) == false)
                                        {
                                            log.Write("Error: line ");
                                            log.Write(line);
                                            log.WriteLine(" uknown variable!");
                                            if (exit_code != -1)
                                                exit_code = -1;
                                        }
                                    }
                                }
                            }
                            #endregion
                            flg = false;
                        }
                    }
                    #endregion
                    #region ge
                    if (code_str.Contains("ge"))
                    {
                        if (!code_str.StartsWith("integer"))
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

                            String[] SplStr = code_str.Split(' ');

                            for (int i = 0; i < SplStr.Length; ++i)
                            {
                                if (SplStr[i] == "ge")
                                {
                                    idx = i;
                                    break;
                                }
                            }
                            #region 2 vars
                            if (char.IsDigit(SplStr[idx - 1], 0) == false &&
                                char.IsDigit(SplStr[idx + 1], 0) == false)
                            {
                                flg = true;
                                if (SplStr[idx - 1].Substring(SplStr[idx - 1].IndexOf("(") + 1).StartsWith("_") == false ||
                                    char.IsUpper(SplStr[idx - 1].Substring(SplStr[idx - 1].IndexOf("(") + 1), 1) == false ||
                                    vars.Contains(SplStr[idx - 1].Substring(SplStr[idx - 1].IndexOf("(") + 1)) == false)
                                {
                                    log.Write("Error: line ");
                                    log.Write(line);
                                    log.WriteLine(" uknown variable!");
                                    if (exit_code != -1)
                                        exit_code = -1;
                                }

                                if (SplStr[idx + 1].EndsWith(")") == false)
                                {
                                    if (SplStr[idx + 1].Substring(SplStr[idx + 1].IndexOf("(") + 1).StartsWith("_") == false ||
                                        char.IsUpper(SplStr[idx + 1].Substring(SplStr[idx + 1].IndexOf("(") + 1), 1) == false ||
                                        vars.Contains(SplStr[idx + 1].Substring(SplStr[idx + 1].IndexOf("(") + 1)) == false)
                                    {
                                        log.Write("Error: line ");
                                        log.Write(line);
                                        log.WriteLine(" uknown variable!");
                                        if (exit_code != -1)
                                            exit_code = -1;
                                    }
                                }
                                else
                                {
                                    if (SplStr[idx + 1].Substring(SplStr[idx + 1].IndexOf("(") + 1).StartsWith("_") == false ||
                                        char.IsUpper(SplStr[idx + 1].Substring(SplStr[idx + 1].IndexOf("(") + 1), 1) == false ||
                                        vars.Contains(SplStr[idx + 1].Substring(0, SplStr[idx + 1].Length - 1)) == false)
                                    {
                                        log.Write("Error: line ");
                                        log.Write(line);
                                        log.WriteLine(" uknown variable!");
                                        if (exit_code != -1)
                                            exit_code = -1;
                                    }
                                }
                            }
                            #endregion
                            #region 1 var
                            if ((char.IsDigit(SplStr[idx - 1], 0) == false ||
                                char.IsDigit(SplStr[idx + 1], 0) == false) &&
                                flg == false)
                            {
                                if (char.IsDigit(SplStr[idx - 1], 0) == false)
                                {
                                    if (SplStr[idx - 1].StartsWith("_") == false ||
                                    char.IsUpper(SplStr[idx - 1], 1) == false ||
                                    vars.Contains(SplStr[idx - 1]) == false)
                                    {
                                        log.Write("Error: line ");
                                        log.Write(line);
                                        log.WriteLine(" uknown variable!");
                                        if (exit_code != -1)
                                            exit_code = -1;
                                    }
                                }

                                if (char.IsDigit(SplStr[idx + 1], 0) == false)
                                {
                                    if (SplStr[idx + 1].EndsWith(")") == false)
                                    {
                                        if (SplStr[idx + 1].StartsWith("_") == false ||
                                            char.IsUpper(SplStr[idx + 1], 1) == false ||
                                            vars.Contains(SplStr[idx + 1]) == false)
                                        {
                                            log.Write("Error: line ");
                                            log.Write(line);
                                            log.WriteLine(" uknown variable!");
                                            if (exit_code != -1)
                                                exit_code = -1;
                                        }
                                    }
                                    else
                                    {
                                        if (SplStr[idx + 1].StartsWith("_") == false ||
                                            char.IsUpper(SplStr[idx + 1], 1) == false ||
                                            vars.Contains(SplStr[idx + 1].Substring(0, SplStr[idx + 1].Length - 1)) == false)
                                        {
                                            log.Write("Error: line ");
                                            log.Write(line);
                                            log.WriteLine(" uknown variable!");
                                            if (exit_code != -1)
                                                exit_code = -1;
                                        }
                                    }
                                }
                            }
                            #endregion
                            flg = false;
                        }
                    }
                    #endregion
                    #region &
                    if (code_str.Contains("&"))
                    {
                        #region Main logic
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
                        #endregion
                        String[] SplStr = code_str.Substring(code_str.IndexOf("if(") + 3, code_str.LastIndexOf(")") - (code_str.IndexOf("if(") + 3)).Split(' ');

                        for (int i = 0; i < SplStr.Length; ++i)
                        {
                            if (SplStr[i] == "&")
                            {
                                idx = i;
                                break;
                            }
                        }
                        if (!SplStr[0].StartsWith("(") || !SplStr[idx - 1].EndsWith(")") ||
                            !SplStr[idx + 1].StartsWith("(") || !SplStr[SplStr.Length - 1].EndsWith(")"))
                        {
                                log.Write("Error: line ");
                                log.Write(line);
                                log.WriteLine(" wrong \"&\" operator!");
                                if (exit_code != -1)
                                    exit_code = -1;
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

                        String[] SplStr = code_str.Substring(code_str.IndexOf("if(") + 3, code_str.LastIndexOf(")") - (code_str.IndexOf("if(") + 3)).Split(' ');

                        for (int i = 0; i < SplStr.Length; ++i)
                        {
                            if (SplStr[i] == "&")
                            {
                                idx = i;
                                break;
                            }
                        }
                        if (!SplStr[0].StartsWith("(") || !SplStr[idx - 1].EndsWith(")") ||
                            !SplStr[idx + 1].StartsWith("(") || !SplStr[SplStr.Length - 1].EndsWith(")"))
                        {
                            log.Write("Error: line ");
                            log.Write(line);
                            log.WriteLine(" wrong \"&\" operator!");
                            if (exit_code != -1)
                                exit_code = -1;
                        }
                        flg = false;
                    }
                    #endregion
                    #region !
                    if (code_str.Contains("!") && !code_str.Contains("!="))
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
                            log.WriteLine(" uknown variable!");
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
            fil.Close();
            return exit_code;
        }
        public String operators(String str, bool[] regs)
        {
            String reg = "";
            String[] SplStr = str.Split(' ');
            int idx = 0;
            #region *
            if (str.Contains("*"))
            {
                for (int i = 0; i < SplStr.Length; ++i)
                {
                    if (SplStr[i] == "*")
                    {
                        if (regs[1] == false ||
                            SplStr[i + 1] == "ebx" ||
                            SplStr[i - 1] == "ebx")
                        {
                            #region ebx
                            idx = i;
                            asm.WriteLine("mov eax," + SplStr[idx - 1]);
                            asm.WriteLine("mov ebx," + SplStr[idx + 1]);
                            asm.WriteLine("imul ebx");
                            asm.WriteLine("mov ebx,eax");
                            regs[1] = true;
                            SplStr[idx] = "0";
                            SplStr[idx + 1] = "0";
                            SplStr[idx - 1] = "ebx";
                            for (int j = 0; j < SplStr.Length; ++j)
                            {
                                if (SplStr[i] == "0")
                                {
                                    for (int k = i; k < SplStr.Length; ++k)
                                        if (k + 2 < SplStr.Length)
                                            SplStr[k] = SplStr[k + 2];
                                }
                            }
                            SplStr[SplStr.Length - 1] = "0";
                            SplStr[SplStr.Length - 2] = "0";
                            String[] nwSt = new String[SplStr.Length - 2];
                            nwSt = SplStr;
                            SplStr = new String[nwSt.Length];
                            SplStr = nwSt;
                            #endregion
                        }
                        else if (regs[2] == false ||
                                 SplStr[i + 1] == "ecx" ||
                                 SplStr[i - 1] == "ecx")
                        {
                            #region ecx
                            idx = i;
                            asm.WriteLine("mov eax," + SplStr[idx - 1]);
                            asm.WriteLine("mov ecx," + SplStr[idx + 1]);
                            asm.WriteLine("imul ecx");
                            asm.WriteLine("mov ecx,eax");
                            regs[2] = true;
                            SplStr[idx] = "0";
                            SplStr[idx + 1] = "0";
                            SplStr[idx - 1] = "ecx";
                            for (int j = 0; j < SplStr.Length; ++j)
                            {
                                if (SplStr[i] == "0")
                                {
                                    for (int k = i; k < SplStr.Length; ++k)
                                        if (k + 2 < SplStr.Length)
                                            SplStr[k] = SplStr[k + 2];
                                }
                            }
                            SplStr[SplStr.Length - 1] = "0";
                            SplStr[SplStr.Length - 2] = "0";
                            String[] nwSt = new String[SplStr.Length - 2];
                            nwSt = SplStr;
                            SplStr = new String[nwSt.Length];
                            SplStr = nwSt;
                            #endregion
                        }
                        else
                        {
                            #region no regs
                            MessageBox.Show("No regs!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw new Exception();
                            #endregion
                        }
                    }
                }
                idx = 0;
            }
            #endregion
            #region /
            if (str.Contains("/"))
            {
                for (int i = 0; i < SplStr.Length; ++i)
                {
                    if (SplStr[i] == "/")
                    {
                        if (regs[1] == false ||
                            SplStr[i + 1] == "ebx" ||
                            SplStr[i - 1] == "ebx")
                        {
                            #region ebx
                            idx = i;
                            asm.WriteLine("mov eax," + SplStr[idx - 1]);
                            asm.WriteLine("mov ebx," + SplStr[idx + 1]);
                            asm.WriteLine("idiv ebx");
                            asm.WriteLine("mov ebx,eax");
                            regs[1] = true;
                            SplStr[idx] = "0";
                            SplStr[idx + 1] = "0";
                            SplStr[idx - 1] = "ebx";
                            for (int j = 0; j < SplStr.Length; ++j)
                            {
                                if (SplStr[i] == "0")
                                {
                                    for (int k = i; k < SplStr.Length; ++k)
                                        if (k + 2 < SplStr.Length)
                                            SplStr[k] = SplStr[k + 2];
                                }
                            }
                            SplStr[SplStr.Length - 1] = "0";
                            SplStr[SplStr.Length - 2] = "0";
                            String[] nwSt = new String[SplStr.Length - 2];
                            nwSt = SplStr;
                            SplStr = new String[nwSt.Length];
                            SplStr = nwSt;
                            #endregion
                        }
                        else if (regs[2] == false ||
                                 SplStr[i + 1] == "ecx" ||
                                 SplStr[i - 1] == "ecx")
                        {
                            #region ecx
                            idx = i;
                            asm.WriteLine("mov eax," + SplStr[idx - 1]);
                            asm.WriteLine("mov ecx," + SplStr[idx + 1]);
                            asm.WriteLine("idiv ecx");
                            asm.WriteLine("mov ecx,eax");
                            regs[2] = true;
                            SplStr[idx] = "0";
                            SplStr[idx + 1] = "0";
                            SplStr[idx - 1] = "ecx";
                            for (int j = 0; j < SplStr.Length; ++j)
                            {
                                if (SplStr[i] == "0")
                                {
                                    for (int k = i; k < SplStr.Length; ++k)
                                        if (k + 2 < SplStr.Length)
                                            SplStr[k] = SplStr[k + 2];
                                }
                            }
                            SplStr[SplStr.Length - 1] = "0";
                            SplStr[SplStr.Length - 2] = "0";
                            String[] nwSt = new String[SplStr.Length - 2];
                            nwSt = SplStr;
                            SplStr = new String[nwSt.Length];
                            SplStr = nwSt;
                            #endregion
                        }
                        else
                        {
                            #region no regs
                            MessageBox.Show("No regs!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw new Exception();
                            #endregion
                        }
                    }
                }
                idx = 0;
            }
            #endregion
            #region add
            if (str.Contains("add"))
            {
                for (int i = 0; i < SplStr.Length; ++i)
                {
                    if (SplStr[i] == "add")
                    {
                        if (regs[1] == false ||
                            SplStr[i + 1] == "ebx" ||
                            SplStr[i - 1] == "ebx")
                        {
                            #region ebx
                            idx = i;
                            asm.WriteLine("mov eax," + SplStr[idx - 1]);
                            asm.WriteLine("mov ebx," + SplStr[idx + 1]);
                            asm.WriteLine("add eax,ebx");
                            asm.WriteLine("mov ebx,eax");
                            regs[1] = true;
                            SplStr[idx] = "0";
                            SplStr[idx + 1] = "0";
                            SplStr[idx - 1] = "ebx";
                            for (int j = 0; j < SplStr.Length; ++j)
                            {
                                if (SplStr[i] == "0")
                                {
                                    for (int k = i; k < SplStr.Length; ++k)
                                        if (k + 2 < SplStr.Length)
                                            SplStr[k] = SplStr[k + 2];
                                }
                            }
                            SplStr[SplStr.Length - 1] = "0";
                            SplStr[SplStr.Length - 2] = "0";
                            String[] nwSt = new String[SplStr.Length - 2];
                            nwSt = SplStr;
                            SplStr = new String[nwSt.Length];
                            SplStr = nwSt;
                            #endregion
                        }
                        else if (regs[2] == false ||
                                 SplStr[i + 1] == "ecx" ||
                                 SplStr[i - 1] == "ecx")
                        {
                            #region ecx
                            idx = i;
                            asm.WriteLine("mov eax," + SplStr[idx - 1]);
                            asm.WriteLine("mov ecx," + SplStr[idx + 1]);
                            asm.WriteLine("add eax,ecx");
                            asm.WriteLine("mov ecx,eax");
                            regs[2] = true;
                            SplStr[idx] = "0";
                            SplStr[idx + 1] = "0";
                            SplStr[idx - 1] = "ecx";
                            for (int j = 0; j < SplStr.Length; ++j)
                            {
                                if (SplStr[i] == "0")
                                {
                                    for (int k = i; k < SplStr.Length; ++k)
                                        if (k + 2 < SplStr.Length)
                                            SplStr[k] = SplStr[k + 2];
                                }
                            }
                            SplStr[SplStr.Length - 1] = "0";
                            SplStr[SplStr.Length - 2] = "0";
                            String[] nwSt = new String[SplStr.Length - 2];
                            nwSt = SplStr;
                            SplStr = new String[nwSt.Length];
                            SplStr = nwSt;
                            #endregion
                        }
                        else
                        {
                            #region no regs
                            MessageBox.Show("No regs!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw new Exception();
                            #endregion
                        }
                    }
                }
                idx = 0;
            }
            #endregion
            #region sub
            if (str.Contains("sub"))
            {
                for (int i = 0; i < SplStr.Length; ++i)
                {
                    if (SplStr[i] == "sub")
                    {
                        if (regs[1] == false ||
                            SplStr[i + 1] == "ebx" ||
                            SplStr[i - 1] == "ebx")
                        {
                            #region ebx
                            idx = i;
                            asm.WriteLine("mov eax," + SplStr[idx - 1]);
                            asm.WriteLine("mov ebx," + SplStr[idx + 1]);
                            asm.WriteLine("sub eax,ebx");
                            asm.WriteLine("mov ebx,eax");
                            regs[1] = true;
                            SplStr[idx] = "0";
                            SplStr[idx + 1] = "0";
                            SplStr[idx - 1] = "ebx";
                            for (int j = 0; j < SplStr.Length; ++j)
                            {
                                if (SplStr[i] == "0")
                                {
                                    for (int k = i; k < SplStr.Length; ++k)
                                        if (k + 2 < SplStr.Length)
                                            SplStr[k] = SplStr[k + 2];
                                }
                            }
                            SplStr[SplStr.Length - 1] = "0";
                            SplStr[SplStr.Length - 2] = "0";
                            String[] nwSt = new String[SplStr.Length - 2];
                            nwSt = SplStr;
                            SplStr = new String[nwSt.Length];
                            SplStr = nwSt;
                            #endregion
                        }
                        else if (regs[2] == false ||
                                 SplStr[i + 1] == "ecx" ||
                                 SplStr[i - 1] == "ecx")
                        {
                            #region ecx
                            idx = i;
                            asm.WriteLine("mov eax," + SplStr[idx - 1]);
                            asm.WriteLine("mov ecx," + SplStr[idx + 1]);
                            asm.WriteLine("sub eax,ecx");
                            asm.WriteLine("mov ecx,eax");
                            regs[2] = true;
                            SplStr[idx] = "0";
                            SplStr[idx + 1] = "0";
                            SplStr[idx - 1] = "ecx";
                            for (int j = 0; j < SplStr.Length; ++j)
                            {
                                if (SplStr[i] == "0")
                                {
                                    for (int k = i; k < SplStr.Length; ++k)
                                        if (k + 2 < SplStr.Length)
                                            SplStr[k] = SplStr[k + 2];
                                }
                            }
                            SplStr[SplStr.Length - 1] = "0";
                            SplStr[SplStr.Length - 2] = "0";
                            String[] nwSt = new String[SplStr.Length - 2];
                            nwSt = SplStr;
                            SplStr = new String[nwSt.Length];
                            SplStr = nwSt;
                            #endregion
                        }
                        else
                        {
                            #region no regs
                            MessageBox.Show("No regs!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw new Exception();
                            #endregion
                        }
                    }
                }
                idx = 0;
            }
            #endregion
            #region %
            if (str.Contains("%"))
            {
                for (int i = 0; i < SplStr.Length; ++i)
                {
                    if (SplStr[i] == "%")
                    {
                        if (regs[1] == false ||
                            SplStr[i + 1] == "ebx" ||
                            SplStr[i - 1] == "ebx")
                        {
                            #region ebx
                            idx = i;
                            asm.WriteLine("mov eax," + SplStr[idx - 1]);
                            asm.WriteLine("mov ebx," + SplStr[idx + 1]);
                            asm.WriteLine("idiv ebx");
                            asm.WriteLine("mov ebx,edx");
                            regs[1] = true;
                            SplStr[idx] = "0";
                            SplStr[idx + 1] = "0";
                            SplStr[idx - 1] = "ebx";
                            for (int j = 0; j < SplStr.Length; ++j)
                            {
                                if (SplStr[i] == "0")
                                {
                                    for (int k = i; k < SplStr.Length; ++k)
                                        if (k + 2 < SplStr.Length)
                                            SplStr[k] = SplStr[k + 2];
                                }
                            }
                            SplStr[SplStr.Length - 1] = "0";
                            SplStr[SplStr.Length - 2] = "0";
                            String[] nwSt = new String[SplStr.Length - 2];
                            nwSt = SplStr;
                            SplStr = new String[nwSt.Length];
                            SplStr = nwSt;
                            #endregion
                        }
                        else if (regs[2] == false ||
                                 SplStr[i + 1] == "ecx" ||
                                 SplStr[i - 1] == "ecx")
                        {
                            #region ecx
                            idx = i;
                            asm.WriteLine("mov eax," + SplStr[idx - 1]);
                            asm.WriteLine("mov ecx," + SplStr[idx + 1]);
                            asm.WriteLine("idiv ecx");
                            asm.WriteLine("mov ecx,edx");
                            regs[2] = true;
                            SplStr[idx] = "0";
                            SplStr[idx + 1] = "0";
                            SplStr[idx - 1] = "ecx";
                            for (int j = 0; j < SplStr.Length; ++j)
                            {
                                if (SplStr[i] == "0")
                                {
                                    for (int k = i; k < SplStr.Length; ++k)
                                        if (k + 2 < SplStr.Length)
                                            SplStr[k] = SplStr[k + 2];
                                }
                            }
                            SplStr[SplStr.Length - 1] = "0";
                            SplStr[SplStr.Length - 2] = "0";
                            String[] nwSt = new String[SplStr.Length - 2];
                            nwSt = SplStr;
                            SplStr = new String[nwSt.Length];
                            SplStr = nwSt;
                            #endregion
                        }
                        else
                        {
                            #region no regs
                            MessageBox.Show("No regs!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw new Exception();
                            #endregion
                        }
                    }
                }
                idx = 0;
            }
            #endregion
            #region ==
            if (str.Contains("=="))
            {
                reg = str;
                return reg;
            }
            #endregion
            #region !=
            if (str.Contains("!="))
            {
                reg = str;
                return reg;
            }
            #endregion
            #region le
            if (str.Contains("le"))
            {
                reg = "<" + str.Substring(str.LastIndexOf(" "));
                return reg;
            }
            #endregion
            #region ge
            if (str.Contains("ge"))
            {
                reg = ">" + str.Substring(str.LastIndexOf(" "));
                return reg;
            }
            #endregion
            #region &
            if (str.Contains("&"))
            {
                reg = str;
            }
            #endregion
            #region |
            if (str.Contains("|"))
            {
                reg = str;
            }
            #endregion
            #region !
            if (str.Contains("!"))
            {
                reg = str;
            }
            #endregion
            reg = SplStr[0];

            return reg;
        }
        StreamWriter asm;
        private int Compile()
        {
            #region flags
            bool name = false;
            bool body = false;
            bool end = false;
            bool flg = false;
            bool if_op = false;
            bool else_op = false;
            bool[] regs = { false, false, false, false };
            #endregion
            #region vars
            uint[] blk_op = { 0, 0 };
            uint ln = 0;
            int exit_code = 0;
            char[] smb = { ' ', ';' };
            #endregion
            log.AutoFlush = true;
            log.WriteLine("=====================Compile==============================");
            if (File.Exists(Directory.GetCurrentDirectory() + "\\EvilProject\\code.asm"))
                File.Delete(Directory.GetCurrentDirectory() + "\\EvilProject\\code.asm");
            String code_str;
            StreamReader fil = new StreamReader(fl);
            asm = new StreamWriter(Directory.GetCurrentDirectory() + "\\EvilProject\\code.asm");
            asm.AutoFlush = true;
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
                    name_file = code_str.Substring(code_str.IndexOf("<") + 1, code_str.IndexOf(">") - (code_str.IndexOf("<") + 1)) +".asm";
                    if (File.Exists(Directory.GetCurrentDirectory() + "\\EvilProject\\code.asm"))
                        name = true;
                    continue;
                }
                #endregion
                #region body
                if (body == false)
                {
                    asm.WriteLine(".686");
                    asm.WriteLine(".model flat, stdcall ");
                    asm.WriteLine("option casemap: none");
                    asm.WriteLine("");
                    asm.WriteLine("include \\masm32\\include\\windows.inc");
                    asm.WriteLine("include \\masm32\\macros\\macros.asm");
                    asm.WriteLine("include \\masm32\\include\\kernel32.inc ");
                    asm.WriteLine("include \\masm32\\include\\masm32.inc ");
                    asm.WriteLine("include \\masm32\\include\\user32.inc ");
                    asm.WriteLine("include \\masm32\\include\\gdi32.inc ");
                    asm.WriteLine("include \\masm32\\include\\msvcrt.inc");
                    asm.WriteLine("");
                    asm.WriteLine("includelib \\masm32\\lib\\kernel32.lib ");
                    asm.WriteLine("includelib \\masm32\\lib\\masm32.lib ");
                    asm.WriteLine("includelib \\masm32\\lib\\gdi32.lib ");
                    asm.WriteLine("includelib \\masm32\\lib\\user32.lib");
                    asm.WriteLine("includelib \\masm32\\lib\\msvcrt.lib");
                    asm.WriteLine("");
                    asm.WriteLine(".data");
                    asm.WriteLine("format1 db \"%d\",10,13");

                    asm.WriteLine("tmp dd 0");
                    for (uint i = (uint)vars.IndexOf("_"); i <= vars.LastIndexOf("_"); i = (uint)vars.IndexOf("_", (int)i+1))
                    {
                        asm.WriteLine(vars.Substring((int)i, vars.IndexOf(" ", (int)i) - (int)i) + " dd 0");
                    }
                    asm.WriteLine("");
                    asm.WriteLine(".code");
                    asm.WriteLine("start:");
                    body = true;
                    continue;
                }
                #endregion
                #region code
                if (name == true && body == true && fil.EndOfStream != true)
                {
                    #region ->
                    if (code_str.Contains("->"))
                    {
                        if(code_str.StartsWith("integer "))
                        {
                            asm.Write("mov ");
                            asm.Write(code_str.Substring(code_str.IndexOf("_"), code_str.IndexOf(" ", code_str.IndexOf("_")) - code_str.IndexOf("_")));
                            if (char.IsDigit(code_str.Substring(code_str.IndexOf("->") + 3, code_str.IndexOf(";") - (code_str.IndexOf("->") + 3)), 0))
                            {
                                int.Parse(code_str.Substring(code_str.IndexOf("->") + 3, code_str.IndexOf(";") - (code_str.IndexOf("->") + 3)));
                                asm.WriteLine("," + code_str.Substring(code_str.IndexOf("->") + 3, code_str.IndexOf(";") - (code_str.IndexOf("->") + 3)));
                            }
                            else
                                asm.WriteLine("," + code_str.Substring(code_str.IndexOf("->") + 3, code_str.IndexOf(";") - (code_str.IndexOf("->") + 3)));
                        }
                        else
                        {
                            String reg = "";
                            reg = operators(code_str.Substring(code_str.IndexOf("-> ") + 3, code_str.IndexOf(";") - (code_str.IndexOf("-> ") + 3)), regs);
                            asm.WriteLine("mov " + code_str.Substring(0, code_str.IndexOf(" ->")) + "," + reg);
                        }
                    }
                    #endregion
                    #region read
                    if (code_str.Contains("read"))
                    {
                        asm.WriteLine("invoke  crt_scanf,ADDR format1,ADDR " + code_str.Substring(code_str.IndexOf("_"), code_str.IndexOf(";") - code_str.IndexOf("_")));
                    }
                    #endregion
                    #region write
                    if (code_str.Contains("write"))
                    {
                        asm.WriteLine("invoke  crt_printf,ADDR format1," + code_str.Substring(code_str.IndexOf("_"), code_str.IndexOf(";") - code_str.IndexOf("_")));
                    }
                    #endregion
                    #region if
                    if (code_str.Contains("if(") || code_str.Contains("if ("))
                    {
                        String nw_cd = ".";
                        if (regs[0] == false)
                        {
                            regs[0] = true;
                            asm.WriteLine("mov eax," + code_str.Substring(code_str.IndexOf("_"), code_str.IndexOf(" ") - code_str.IndexOf("_")));
                            nw_cd = nw_cd.Insert(nw_cd.Length, code_str.Substring(0, code_str.IndexOf("(")) + " ");
                            nw_cd = nw_cd.Insert(nw_cd.Length, " eax ");
                            nw_cd = nw_cd.Insert(nw_cd.Length, operators(code_str.Substring(code_str.IndexOf(" ", code_str.IndexOf("_")) + 1, code_str.IndexOf(")") - (code_str.IndexOf(" ", code_str.IndexOf("_")) + 1)), regs));
                            asm.WriteLine(nw_cd);
                        }

                        else if (regs[1] == false)
                        {
                            regs[1] = true;
                            asm.WriteLine("mov ebx," + code_str.Substring(code_str.IndexOf("_"), code_str.IndexOf(" ") - code_str.IndexOf("_")));
                            nw_cd = nw_cd.Insert(nw_cd.Length, code_str.Substring(0, code_str.IndexOf("(")));
                            nw_cd = nw_cd.Insert(nw_cd.Length, " ebx ");
                            nw_cd = nw_cd.Insert(nw_cd.Length, operators(code_str.Substring(code_str.IndexOf(" ", code_str.IndexOf("_")) + 1, code_str.IndexOf(")") - (code_str.IndexOf(" ", code_str.IndexOf("_")) + 1)), regs));
                            asm.WriteLine(nw_cd);
                        }

                        else if (regs[2] == false)
                        {
                            regs[2] = true;
                            asm.WriteLine("mov ecx," + code_str.Substring(code_str.IndexOf("_"), code_str.IndexOf(" ") - code_str.IndexOf("_")));
                            nw_cd = nw_cd.Insert(nw_cd.Length, code_str.Substring(0, code_str.IndexOf("(")));
                            nw_cd = nw_cd.Insert(nw_cd.Length, " ecx ");
                            nw_cd = nw_cd.Insert(nw_cd.Length, operators(code_str.Substring(code_str.IndexOf(" ", code_str.IndexOf("_")) + 1, code_str.IndexOf(")") - (code_str.IndexOf(" ", code_str.IndexOf("_")) + 1)), regs));
                            asm.WriteLine(nw_cd);
                        }

                        else if (regs[3] == false)
                        {
                            regs[3] = true;
                            asm.WriteLine("mov edx," + code_str.Substring(code_str.IndexOf("_"), code_str.IndexOf(" ") - code_str.IndexOf("_")));
                            nw_cd = nw_cd.Insert(nw_cd.Length, code_str.Substring(0, code_str.IndexOf("(")));
                            nw_cd = nw_cd.Insert(nw_cd.Length, " edx ");
                            nw_cd = nw_cd.Insert(nw_cd.Length, operators(code_str.Substring(code_str.IndexOf(" ", code_str.IndexOf("_")) + 1, code_str.IndexOf(")") - (code_str.IndexOf(" ", code_str.IndexOf("_")) + 1)), regs));
                            asm.WriteLine(nw_cd);
                        }
                        if_op = true;
                    }
                    #endregion
                    #region else
                    if (code_str.Contains("else"))
                    {
                        String nw_cd = ".";
                        nw_cd = nw_cd.Insert(nw_cd.Length, code_str);
                        asm.WriteLine(nw_cd);
                        else_op = true;
                    }
                    #endregion
                    #region {}
                    if(code_str.Contains("}"))
                    {
                        if (File.ReadAllText(fl).Contains("else") && else_op == false)
                            continue;
                        else
                            asm.WriteLine(".endif");
                    }
                    #endregion
                }
                #endregion
                #region end
                if (end == false && fil.EndOfStream == true)
                {
                    asm.WriteLine("invoke  ExitProcess,0");
                    asm.WriteLine("END start");
                    end = true;
                    continue;
                }
                #endregion
            }
            #endregion
            vars = "";
            asm.Close();
            if (File.Exists(Directory.GetCurrentDirectory() + "\\EvilProject\\" + name_file))
                File.Delete(Directory.GetCurrentDirectory() + "\\EvilProject\\" + name_file);
            File.Move(Directory.GetCurrentDirectory() + "\\EvilProject\\code.asm", Directory.GetCurrentDirectory() + "\\EvilProject\\" + name_file);
            fil.Close();
            #region assembly
            ProcessStartInfo psiOpt = new ProcessStartInfo(Directory.GetCurrentDirectory() + "\\EvilProject\\" + @"bldall", name_file.Substring(0, name_file.IndexOf(".")));
            psiOpt.WorkingDirectory = Directory.GetCurrentDirectory() + "\\EvilProject\\";
            Process procCommand = Process.Start(psiOpt);
            procCommand.WaitForExit();

            if (File.Exists(Directory.GetCurrentDirectory() + "\\EvilProject\\" + name_file.Substring(0, name_file.IndexOf(".")) + ".obj") == false)
            {
                log.WriteLine(name_file.Substring(0, name_file.IndexOf(".")) + ".obj" + " exists");
                exit_code = -1;
            }
            if (File.Exists(Directory.GetCurrentDirectory() + "\\EvilProject\\" + name_file.Substring(0, name_file.IndexOf(".")) + ".exe") == false)
            {
                log.WriteLine(name_file.Substring(0, name_file.IndexOf(".")) + ".exe" + " exists");
                exit_code = -1;
            }
            #endregion
            log.WriteLine("============================================================");
            return exit_code;
        }
        #endregion
    }
}
