using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SQLTool.Views
{
    /// <summary>
    /// Interaction logic for ResultData.xaml
    /// </summary>
    public partial class ResultData : UserControl
    {
        public ResultData()
        {
            InitializeComponent();
        }

        private void DgcResultData_ItemsSourceChanged(object sender, DevExpress.Xpf.Grid.ItemsSourceChangedEventArgs e)
        {
            (sender as DevExpress.Xpf.Grid.GridControl).Columns[0].Fixed = DevExpress.Xpf.Grid.FixedStyle.Left;
        }
    }
}
