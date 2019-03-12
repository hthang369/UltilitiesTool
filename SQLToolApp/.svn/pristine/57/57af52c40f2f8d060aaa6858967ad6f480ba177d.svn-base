using System.Drawing;
using System.Windows.Forms;

namespace CustomFormStyle
{
    public partial class Border
    {
        void InitializeComponent(Color backcolor, Color textcolor)
        {
            // panel
            BackColor = backcolor;
            Size = new System.Drawing.Size(_frm.Width, 26);
            Location = new Point(0, 0);
            //Dock = DockStyle.Top;
            //this.MouseMove += Border_MouseMove;

            // closebox
            CloseBox.Image = Properties.Resources.close1;
            CloseBox.Size = new Size(34, Height);
            CloseBox.Location = new Point(Width - CloseBox.Width, 0);
            CloseBox.SizeMode = PictureBoxSizeMode.StretchImage;
            CloseBox.MouseEnter += CloseBox_MouseEnter;
            CloseBox.MouseLeave += CloseBox_MouseLeave;
            CloseBox.MouseClick += CloseBox_MouseClick;

            //miniBox
            MiniBox.Image = Properties.Resources.mini1;
            MiniBox.Size = new Size(34, Height);
            MiniBox.Location = new Point(Width - MiniBox.Width * 3, 0);
            MiniBox.SizeMode = PictureBoxSizeMode.StretchImage;
            MiniBox.MouseEnter += MiniBox_MouseEnter;
            MiniBox.MouseLeave += MiniBox_MouseLeave;
            MiniBox.MouseClick += MiniBox_MouseClick;

            // normnalbox
            NormnalBox.Image = Properties.Resources.normnal1;
            NormnalBox.Size = new Size(34, Height);
            NormnalBox.Location = new Point(Width - NormnalBox.Width * 2, 0);
            NormnalBox.SizeMode = PictureBoxSizeMode.StretchImage;
            NormnalBox.MouseEnter += NormnalBox_MouseEnter;
            NormnalBox.MouseLeave += NormnalBox_MouseLeave;
            NormnalBox.MouseClick += NormnalBox_MouseClick;

            // tractbar
            OpacityTrackbar.Minimum = 10;
            OpacityTrackbar.Maximum = 100;
            OpacityTrackbar.Size = new System.Drawing.Size(106, 45);
            OpacityTrackbar.Location = new Point(MiniBox.Left - OpacityTrackbar.Width - 5, 2);
            OpacityTrackbar.Value = 100;
            OpacityTrackbar.TickStyle = System.Windows.Forms.TickStyle.None;
            _frm.Opacity = OpacityTrackbar.Value / 100D;
            OpacityTrackbar.Scroll += Opacitytractbar_Scroll;

            // label title
            Title.AutoEllipsis = true;
            Title.AutoSize = false;
            Title.Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Title.ForeColor = textcolor;
            Title.Location = new Point(0, 0);
            Title.Size = new Size(OpacityTrackbar.Left, Height);
            Title.Text = _frm.Text;

            //Timer1
            Timer1.Interval = 3;
            Timer1.Tick += Timer1_Tick;

            //Timer2
            Timer2.Interval = 3;
            Timer2.Tick += Timer2_Tick;

            // Form
            _frm.Padding = new Padding(0, Height, 0, 0);
            _frm.MouseDown += _frm_MouseDown;
            _frm.MouseUp += _frm_MouseUp;
        }

        protected Timer Timer1 = new Timer();
        protected Timer Timer2 = new Timer();
        protected TrackBar OpacityTrackbar = new TrackBar();
        protected Label Title = new Label();
        protected PictureBox MiniBox = new PictureBox();
        protected PictureBox NormnalBox = new PictureBox();
        protected PictureBox CloseBox = new PictureBox();
        System.Windows.Forms.Form _frm;

        System.Drawing.Point MousedownLocation;
        bool mouseisdown = false;
    }
}
