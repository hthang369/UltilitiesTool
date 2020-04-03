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
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using SQLTool.ViewModels;

namespace SQLTool.Views
{
    /// <summary>
    /// Interaction logic for ResultView.xaml
    /// </summary>
    public partial class ResultView : UserControl
    {
        public ResultView()
        {
            InitializeComponent();
        }

        private void DgvDataResult_AutoGeneratingColumn(object sender, DevExpress.Xpf.Grid.AutoGeneratingColumnEventArgs e)
        {
            if(e.Column.FieldType.Equals(typeof(String)))
                e.Column.AutoFilterCriteria = DevExpress.Data.Filtering.Helpers.ClauseType.Contains;
            if (e.Column.FieldName == "id")
            {
                e.Column.Fixed = DevExpress.Xpf.Grid.FixedStyle.Left;
                //e.Column.BestFit
            }
        }
    }
}
