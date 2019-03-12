using System.Windows.Forms;

namespace CustomFormStyle
{
    public partial class Border
    {
        ResizeType RsType;

        protected bool _resizeable;

        public bool Resizeable
        {
            get { return _resizeable; }
            set
            {
                _resizeable = value;
                if (value)
                {
                    try
                    {
                        
                        _frm.MouseMove -= _frm_MouseMove1;
                    }
                    catch { }
                    
                    _frm.MouseMove += _frm_MouseMove1;
                }
                else
                {
                    try
                    {
                        _frm.MouseMove -= _frm_MouseMove1;
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Hàm Subscribe Event MouseMove, dùng cho chức năng thay đổi kích thước form
        /// </summary>
        void _frm_MouseMove1(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (mouseisdown)
            {
                if (RsType != ResizeType.None)
                {// đang resize
                    switch (RsType)
                    {
                        case ResizeType.Right:
                            {
                                Widthprpt += e.X - MousedownLocation.X;
                                MousedownLocation.X = e.X;
                            }; break;
                        case ResizeType.BottomRight:
                            {
                                int buffWidth = _frm.Width + e.X - MousedownLocation.X;
                                int buffHeight = _frm.Height + e.Y - MousedownLocation.Y;
                                if (buffHeight<Miniumsize.Height)
                                {
                                    buffHeight = _frm.Height;
                                }
                                if (buffWidth<Miniumsize.Width)
                                {
                                    buffWidth = _frm.Width;
                                }
                                _frm.Size = new System.Drawing.Size(buffWidth, buffHeight);
                                MousedownLocation = e.Location;
                            }; break;
                        case ResizeType.Bottom:
                            {
                                Heighprpt += e.Y - MousedownLocation.Y;
                                MousedownLocation.Y = e.Y;
                            }; break;
                    }
                }
            }
            else
            {
                if (_frm.WindowState == FormWindowState.Normal)
                {// Form đang ở trạng thái bình thường - có thể resize được
                    if (e.X < Widthprpt - 5)
                    {// chuột đang ở giữa
                        if (e.Y > Heighprpt - 5)
                        {// chuột đang ở phía dưới
                            _frm.Cursor = Cursors.SizeNS;
                            RsType = ResizeType.Bottom;
                        }
                        else if (e.Y > this.Height)
                        {// chuột đang ở giữa
                            _frm.Cursor = Cursors.Default;
                            RsType = ResizeType.None;
                        }
                    }
                    else
                    { // chuột đang ở cạnh phải
                        if (e.Y < Heighprpt - 5 && e.Y > this.Height)
                        {// chuột đang ở giữa cạnh phải
                            _frm.Cursor = Cursors.SizeWE;
                            RsType = ResizeType.Right;
                        }
                        else if (e.Y > Heighprpt - 5)
                        { // chuột đang ở góc dưới bên phải
                            _frm.Cursor = Cursors.SizeNWSE;
                            RsType = ResizeType.BottomRight;
                        }
                    }
                }
            }
        }


        enum ResizeType
        {
            None,
            Right,
            Bottom,
            BottomRight,
        }
    }
}
