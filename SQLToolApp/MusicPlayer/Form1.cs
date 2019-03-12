using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicPlayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                ExpertLib.RegistryWorker w = new ExpertLib.RegistryWorker();
                string[] a = w.GetSubKeys("Software");
                Microsoft.Win32.RegistryKey hklm = Microsoft.Win32.Registry.LocalMachine;
                hklm.OpenSubKey("Software", true).DeleteSubKey("GMC1");
                string[] lst = hklm.OpenSubKey("Software",true).GetSubKeyNames();
            }catch(Exception ex) { }
        }
    }
}
