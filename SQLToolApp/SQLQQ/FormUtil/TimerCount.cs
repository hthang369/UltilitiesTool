using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace SQLQQ.FormUtil
{
    public partial class TimerCount : UserControl
    {
        Stopwatch st;
        string strTime = "{0} : {1} : {2}";
        public TimerCount()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            lblShow.Text = string.Format(strTime, st.Elapsed.Hours, st.Elapsed.Minutes, st.Elapsed.Seconds);
        }

        private void TimerCount_Load(object sender, EventArgs e)
        {
            st = new Stopwatch();
            lblShow.Text = string.Format(strTime, 0, 0, 0);
        }
        public void Start()
        {
            timer.Enabled = true;
            timer.Tick += timer_Tick;
            timer.Start();
            st.Reset();
            st.Start();
        }
        public void Stop()
        {
            timer.Stop();
            st.Stop();
            lblShow.Invoke((Action)(() => lblShow.Text = string.Format(strTime, st.Elapsed.Hours, st.Elapsed.Minutes, st.Elapsed.Seconds)));
        }
    }
}
