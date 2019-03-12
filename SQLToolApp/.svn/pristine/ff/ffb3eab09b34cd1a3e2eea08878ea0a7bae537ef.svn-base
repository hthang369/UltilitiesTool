using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomFormStyle
{
    public partial class CustomForm : Form
    {
        public Border border;
        public CustomForm()
        {
            InitializeComponent();
        }
        public bool isEnableOpacity
        {
            set { border.isEnableOpacityTrackbar = value; }
        }
        public bool isVisibleOpacity
        {
            set { border.isVisibleOpacityTrackbar = value; }
        }
        public string Title
        {
            set { border.TitleForm = value; }
        }
        private void CustomForm_Load(object sender, EventArgs e)
        {
            // bạn có thể thay đổi dòng này để có các hiệu ứng màu sắc khác nhau.
            border = new Border(this, Color.FromArgb(64, 241, 64), Color.White, true, true);
        }
    }
}
