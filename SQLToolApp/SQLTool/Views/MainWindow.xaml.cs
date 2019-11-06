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
            DataContext = mainViewModel = new MainViewModel(this);
            
        }

        private void ThemedWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //DXSplashScreen.Close();
            //LoadingDecorator loading = new LoadingDecorator();
            ////loading.spl
            //loading.Visibility = Visibility.Visible;
            //SQLApp.ShowAnimate(this.Handle, Util.FunctionList.GetAnimateWindowTime());
            //InputGesture gesture = new KeyGesture(Key.D1, ModifierKeys.Alt, "ALT+D1");
            //string hex = "637E5FC15EZ8831";
            //hex = "A4167B221DFC2D3";
            //System.Security.Cryptography.SymmetricAlgorithm algorithm = System.Security.Cryptography.DES.Create();
            //System.Security.Cryptography.DESCryptoServiceProvider desCrypto = new System.Security.Cryptography.DESCryptoServiceProvider();
            //algorithm.Padding = System.Security.Cryptography.PaddingMode.Zeros;
            //System.Security.Cryptography.ICryptoTransform transform = algorithm.CreateDecryptor();
            //System.IO.MemoryStream stream = new System.IO.MemoryStream();
            //System.Security.Cryptography.CryptoStream stream2 = new System.Security.Cryptography.CryptoStream(stream, transform, System.Security.Cryptography.CryptoStreamMode.Write);
            //byte[] bytes = new ASCIIEncoding().GetBytes(hex);
            //stream2.Write(bytes, 0, bytes.Length);
            //stream2.FlushFinalBlock();
            //byte[] buffer = stream.ToArray();
            //stream2.Close();


            InputBinding inputBinding = new KeyBinding(mainViewModel.KeyBindingCommand, Key.D1, ModifierKeys.Alt);
            inputBinding.CommandParameter = "Alt+D1";
            InputBindings.Add(inputBinding);

            inputBinding = new KeyBinding(mainViewModel.KeyBindingCommand, Key.Enter, ModifierKeys.None);
            inputBinding.CommandParameter = "None+Enter";
            lstFunction.InputBindings.Add(inputBinding);
        }

        private void ThemedWindow_KeyDown(object sender, KeyEventArgs e)
        {
            mainViewModel.KeyActionCommand(sender, e);
        }
    }
}
