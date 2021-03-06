﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace AngryCompiller
{
    partial class AboutEvil : Form
    {
        public AboutEvil()
        {
            InitializeComponent();
            this.Text = String.Format("О программе {0}", AssemblyTitle);
            this.ProductName.Text = AssemblyProduct;
            this.version_of_evil.Text = String.Format("Версия {0}", AssemblyVersion);
            this.copyright_of_evil.Text = AssemblyCopyright;
            ProductName.Text = "Product: " + AssemblyTitle;
            version_of_evil.Text = "Version: " + AssemblyVersion;

        }

        #region Методы доступа к атрибутам сборки

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void AboutEvil_MouseDown(object sender, MouseEventArgs e)
        {
            this.Capture = false;
            var msg = Message.Create(this.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref msg);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void logo_of_evil_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Don't click me, motherfucker!!!");
        }

        private void mail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://mailto:sasha.halchin@gmail.com");
        }
    }
}
