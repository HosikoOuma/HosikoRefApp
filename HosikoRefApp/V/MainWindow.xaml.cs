using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HosikoRefApp.M;
using HosikoRefApp.COM;
using HosikoRefApp.CORE;
using HosikoRefApp.M;
using HosikoRefApp.VM;

namespace HosikoRefApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWVM vm;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm = new();
            //DefDat();
            //Bind();
        }

    }
}