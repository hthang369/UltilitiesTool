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
using DevExpress.Xpf.Core;
using SQLAppLib;
using SQLTool.ViewModels;

namespace SQLTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        MainViewModel mainViewModel;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = mainViewModel = new MainViewModel();
        }

        private void ThemedWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //DXSplashScreen.Close();
            //LoadingDecorator loading = new LoadingDecorator();
            ////loading.spl
            //loading.Visibility = Visibility.Visible;
            //SQLApp.ShowAnimate(this.Handle, Util.FunctionList.GetAnimateWindowTime());
        }
    }
}
