using System;
using System.Drawing;
using System.Windows.Forms;

namespace CustomFormStyle
{
    public partial class Border
    {

        Size Miniumsize
        {
            get { return new Size(40+OpacityTrackbar.Width + MiniBox.Width * 3, this.Height * 2); }
        }

        int Widthprpt
        {
            get { return _frm.Width; }
            set
            {
                if (value > Miniumsize.Width)
                {
                    _frm.Width = value;
                }
            }
        }


        /// <summary>
        /// Lấy hoặc đặt màu của vền form.
        /// </summary>
        public Color BorderColor
        {
            get { return this.BackColor; }
            set
            {
                this.BackColor = value;
                _frm.CreateGraphics().DrawRectangle(new Pen(Title.BackColor), new Rectangle(0, 0, _frm.Width - 1, _frm.Height - 1));
            }
        }

        /// <summary>
        /// Lấy hoặc đặt màu của tên form được hiển thị trên thanh title.
        /// </summary>
        public Color TitleColor
        {
            get { return Title.ForeColor; }
            set
            {
                Title.ForeColor = value;
            }
        }

        int Heighprpt
        {
            get { return _frm.Height; }
            set
            {
                if (value > Miniumsize.Height)
                {
                    _frm.Height = value;
                }
            }
        }
        public bool isEnableOpacityTrackbar
        {
            set { OpacityTrackbar.Enabled = value; }
        }

        public bool isVisibleOpacityTrackbar
        {
            set { OpacityTrackbar.Visible = value; }
        }

        public string TitleForm
        {
            set { Title.Text = value; }
            get { return Title.Text; }
        }

        /// <summary>
        /// Tạo một thể hiện mới với các tham số được truyền vào
        /// </summary>
        /// <param name="frm">Form chứa Border này</param>
        /// <param name="backcolor">Màu nền của Border</param>
        /// <param name="textcolor">Màu chữ tiêu đề của Border</param>
        /// <param name="textsize">Kích thước chữ tiêu đề của Border</param>
        /// <param name="backgroundMoveable">Chỉ định khả năng di chuyển form bằng background</param>
        /// <param name="resizeable">Chỉ định khả năng thay đổi kích thước của form</param>
        public Border(Form frm, Color backcolor, Color textcolor, bool backgroundMoveable, bool resizeable)
        {
            _frm = frm;
            _frm.FormBorderStyle = FormBorderStyle.None;
            BackgroundMoveable = backgroundMoveable;
            Resizeable = resizeable;
            
            AddFormMoveableControls(this, Title);
            
            InitializeComponent(backcolor, textcolor);

            // add event to control
            frm.MouseMove += delegate(object sender, MouseEventArgs e)
            {
                if (e.Y < Height / 2)
                {
                    Timer1.Start();
                    Timer2.Stop();
                }
                else
                {
                    Timer2.Start();
                    Timer1.Stop();
                }
            };

            frm.SizeChanged += delegate(object sender, EventArgs e)
            {
                this.Width = frm.Width;
                CloseBox.Location = new Point(Width - CloseBox.Width, 0);
                NormnalBox.Location = new Point(Width - NormnalBox.Width * 2, 0);
                MiniBox.Location = new Point(Width - MiniBox.Width * 3, 0);
                OpacityTrackbar.Location = new Point(MiniBox.Left - OpacityTrackbar.Width - 5, 2);
                Title.Width = OpacityTrackbar.Left;
                frm.Refresh();
            };

            frm.Paint += delegate(object sender, PaintEventArgs e)
            {
                e.Graphics.DrawRectangle(new Pen(Title.BackColor), new Rectangle(0, 0, frm.Width - 1, frm.Height - 1));
            };

            Controls.Add(Title);
            Controls.Add(OpacityTrackbar);
            Controls.Add(MiniBox);
            Controls.Add(NormnalBox);
            Controls.Add(CloseBox);
            frm.Controls.Add(this);
            this.BringToFront();
        }
    }
}
