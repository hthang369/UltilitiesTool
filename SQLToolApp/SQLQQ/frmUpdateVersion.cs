﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLQQ
{
    public partial class frmUpdateVersion : Form
    {
        public frmUpdateVersion()
        {
            InitializeComponent();
            this.Text += " " + Application.ProductVersion;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewFunc.Text) && string.IsNullOrEmpty(txtBug.Text)) return;
            string strContent = string.Format("[{0} - {1} - {2}]{5}1. New Function:{5}\t{3}{5}2. Fix Bug:{5}\t{4}{5}",
                dtpTime.Value.ToShortDateString(), txtTitle.Text, Application.ProductVersion, txtNewFunc.Text.Replace("\n", "\n\t"), txtBug.Text.Replace("\n", "\n\t"), Environment.NewLine);

            File.AppendAllText(Application.StartupPath + "\\Update.txt", strContent, Encoding.UTF8);
            MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                ((TextBox)sender).SelectAll();
        }
    }
}
