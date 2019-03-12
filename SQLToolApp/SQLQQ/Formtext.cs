using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using System.Net;
using System.Diagnostics;
using System.IO;

namespace SQLQQ
{
    public partial class Formtext : Form
    {
        public Formtext()
        {
            InitializeComponent();
            
        }

        private void Formtext_Load(object sender, EventArgs e)
        {
            
        }
        public void ShowListIP()
        {
            ListBox lstboxIP = new ListBox();
            string strHostName = string.Empty;
            // Lấy IP của các máy trong mạng nội bộ (LAN)...
            strHostName = Dns.GetHostName();
            // Lấy hostname sau đó lấy IP tương ứng.
            IPHostEntry ipEntry = Dns.GetHostByName(strHostName);

            IPAddress[] iparrAddr = ipEntry.AddressList;
            if (iparrAddr.Length > 0)
            {
                for (int intLoop = 0; intLoop < iparrAddr.Length; intLoop++)
                    lstboxIP.Items.Add(iparrAddr[intLoop].ToString());
            }
            //Đoạn code dưới đây liệt kê danh sách tên máy
            //Ý tưởng ở đây là lấy kết quả của lệnh DOS: net view
            //Bạn có thể tham khảo đoạn code này để lấy kết quả
            //việc thực thi các lệnh khác, chẳng hạn tree, dir,...
            //Để sử dụng cần thêm thư viện
            //using System.IO;
            //using System.Diagnostics;
            Process netUtility = new Process();
            netUtility.StartInfo.FileName = "net.exe";
            netUtility.StartInfo.CreateNoWindow = true;
            netUtility.StartInfo.Arguments = "view";
            netUtility.StartInfo.RedirectStandardOutput = true;
            netUtility.StartInfo.UseShellExecute = false;
            netUtility.StartInfo.RedirectStandardError = true;
            netUtility.Start();
            StreamReader streamReader = new StreamReader
                (netUtility.StandardOutput.BaseStream,
                netUtility.StandardOutput.CurrentEncoding);
            string line = "";
            while ((line = streamReader.ReadLine()) != null)
            {
                if (line.StartsWith("\\"))
                {
                    lstboxIP.Items.Add(line.Substring(2).Substring(0,
                        line.Substring(2).IndexOf(" ")).ToUpper());
                }
            }
            streamReader.Close();
            netUtility.WaitForExit(1000);
        }
    }
}
