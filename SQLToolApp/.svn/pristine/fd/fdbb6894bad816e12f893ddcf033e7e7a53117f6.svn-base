using System;
using System.Windows.Forms;

namespace CustomFormStyle
{
    public partial class Border
    {
        // Close Box
        void CloseBox_MouseEnter(object sender, EventArgs e)
        {
            CloseBox.Image = Properties.Resources.close2;
        }

        void CloseBox_MouseLeave(object sender, EventArgs e)
        {
            CloseBox.Image = Properties.Resources.close1;
        }

        void CloseBox_MouseClick(object sender, MouseEventArgs e)
        {
            _frm.Close();
        }

        // MiniBox
        void MiniBox_MouseEnter(object sender, EventArgs e)
        {
            MiniBox.Image = Properties.Resources.mini2;
        }

        void MiniBox_MouseLeave(object sender, EventArgs e)
        {
            MiniBox.Image = Properties.Resources.mini1;
        }

        void MiniBox_MouseClick(object sender, MouseEventArgs e)
        {
            _frm.WindowState = FormWindowState.Minimized;
        }

        //NormnalBox
        void NormnalBox_MouseEnter(object sender, EventArgs e)
        {
            NormnalBox.Image = Properties.Resources.normnal2;
        }

        void NormnalBox_MouseLeave(object sender, EventArgs e)
        {
            NormnalBox.Image = Properties.Resources.normnal1;
        }

        void NormnalBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (_frm.WindowState == FormWindowState.Maximized) _frm.WindowState = FormWindowState.Normal;
            else _frm.WindowState = FormWindowState.Maximized;
        }

        //TrackBar
        void Opacitytractbar_Scroll(object sender, EventArgs e)
        {
            _frm.Opacity = OpacityTrackbar.Value / 100D;
        }

        // Timer1
        void Timer1_Tick(object sender, EventArgs e)
        {
            if (Top < 0)
            {
                Top++;
            }
            else
            {
                Timer1.Stop();
            }
        }

        //Timer2
        void Timer2_Tick(object sender, EventArgs e)
        {
            if (Bottom > 4)
            {
                Top--;
            }
            else
            {
                Timer2.Stop();
            }
        }
    }
}
