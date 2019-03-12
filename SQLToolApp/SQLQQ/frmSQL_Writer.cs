using System;
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
    public partial class frmSQL_Writer : Form
    {
        public frmSQL_Writer()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "Bin File|*.bin";
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(saveFile.FileName, FileMode.OpenOrCreate);
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write(txtSql.Text);
                    bw.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Bin File|*.bin";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(open.FileName, FileMode.Open);
                    BinaryReader br = new BinaryReader(fs);
                    txtSql.Text = br.ReadString();
                    br.Close();
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
