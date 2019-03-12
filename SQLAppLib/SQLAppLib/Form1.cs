using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace SQLAppLib
{
    public partial class frmWaiting : Form
    {
        Image img;
        Timer time;
        Graphics g;
        int Xscr, Yscr, x, y;
        Rectangle rec;
        Random rand;
        string strStartupPath;
        string[] strFiles;
        const int WM_NCLBUTTONDOWN = 0xA1;
        const int HT_CAPTION = 0x2;
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.Z)
            {
                int idx = rand.Next(1, strFiles.Count());
                string[] files = strFiles.Where(x => x.StartsWith(strStartupPath + "\\" + idx.ToString().PadLeft(3, '0'))).ToArray();
                if (files.Length > 0)
                    LoadImage(files.First());
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        public frmWaiting()
        {
            InitializeComponent();
            time = new Timer();
            time.Interval = 100;
            time.Tick += time_Tick;
            x = 0;
            y = 0;
            rand = new Random();
            strStartupPath = Application.StartupPath + "\\Data";
            strFiles = Directory.GetFiles(strStartupPath);
            int idx = rand.Next(1, strFiles.Count());
            string[] files = strFiles.Where(x => x.StartsWith(strStartupPath + "\\"+idx.ToString().PadLeft(3, '0'))).ToArray();
            if (files.Length > 0)
                LoadImage(files.First());
        }

        private void time_Tick(object sender, EventArgs e)
        {
            if (x < (img.Width - Xscr)) x += Xscr;
            else
            {
                x = 0;
                if (y < (img.Height - Yscr)) y += Yscr;
                else y = 0;
            }
            Bitmap map = new Bitmap(Xscr, Yscr);
            g = Graphics.FromImage(map);
            g.DrawImage(img, rec, x, y, Xscr, Yscr, GraphicsUnit.Pixel);
            pictureBox1.Image = map;
        }

        private void LoadImage(string strPath)
        {
            img = Image.FromFile(strPath);
            FileInfo info = new FileInfo(strPath);
            string[] arrSize = info.Name.Replace(info.Extension, "").Split('_');
            Xscr = img.Width / Convert.ToInt32(arrSize[2]);
            Yscr = img.Height / Convert.ToInt32(arrSize[1]);
            Bitmap map = new Bitmap(Xscr, Yscr);
            rec = new Rectangle(0, 0, Xscr, Yscr);
            g = Graphics.FromImage(map);
            g.DrawImage(img, rec, x, y, Xscr, Yscr, GraphicsUnit.Pixel);
            pictureBox1.Size = new Size(Xscr, Yscr);
            this.Size = new Size(Xscr+10, Yscr+40);
            pictureBox1.Image = map;
            time.Enabled = true;
        }

    }
}
