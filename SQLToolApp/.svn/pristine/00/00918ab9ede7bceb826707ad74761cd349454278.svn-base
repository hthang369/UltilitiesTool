using System.Windows.Forms;

namespace CustomFormStyle
{
    public partial class Border : Panel
    {
        protected bool _backgroundmoveable;

        public bool BackgroundMoveable
        {
            get { return _backgroundmoveable; }
            set
            {
                _backgroundmoveable = value;
                    if (value)
                    {
                        try
                        {
                            _frm.MouseMove -= _frm_MouseMove;
                        }
                        catch { }
                        _frm.MouseMove += _frm_MouseMove;
                    }
                    else
                    {
                        _frm.MouseMove -= _frm_MouseMove;
                    }
            }
        }
        public void AddFormMoveableControls(params System.Windows.Forms.Control[] control)
        {
            foreach (var ctl in control)
            {
                ctl.MouseDown += _frm_MouseDown;
                ctl.MouseMove += _frm_MouseMove;
                ctl.MouseUp += _frm_MouseUp;
            }
        }

        void _frm_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mouseisdown = false;
        }

        void _frm_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mouseisdown = true;
            MousedownLocation = e.Location;
        }

        /// <summary>
        /// Event Mousemove dùng cho chức năng di chuyển form
        /// </summary>
        void _frm_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (mouseisdown && _frm.Cursor == Cursors.Default)
            {
                _frm.Top += e.Y - MousedownLocation.Y;
                _frm.Left += e.X - MousedownLocation.X;
            }
        }
    }
}
