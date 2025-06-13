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
//using HosikoRefApp.M;

namespace HosikoRefApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string _p = @"./D/";
        public ObservableCollection<G> OL {  get; set; }
        public ObservableCollection<G> GL {  get; set; }
        public decimal TP {  get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DefDat();
            Bind();
        }
        public void Bind()
        {
            tPTBR.Text = TP.ToString();
            gCB.ItemsSource = GL;
            oLB.ItemsSource = OL;
        }
        private async void DefDat()
        {
            GL = new();
            OL = new();
            TP = 0;
            if (!Directory.Exists(_p))
            {
                Directory.CreateDirectory(_p);
            }
            var path = System.IO.Path.Combine(_p, $"G.txt");
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            using (StreamWriter r = new(path, false))
            {
                foreach (var good in S_GL)
                {
                    await r.WriteLineAsync($"{good.N}, {good.P}");
                }
            }
            using (StreamReader rx = new(path))
            {
                var data = await rx.ReadToEndAsync();
                if (!string.IsNullOrEmpty(data))
                {
                    var sd = data.Split("\r\n");
                    sd = sd.Take(sd.Length - 1).ToArray();
                    foreach (var good in sd)
                    {
                        var sg = good.Split(", ");
                        GL.Add(new G() { N = sg[0], P = decimal.Parse(sg[1]) });
                    }
                }
            }
        }
        public class G
        {
            public string N { get; set; }
            public decimal P { get; set; }
        }

        public static List<G> S_GL = new()
        {
            new G() { N = "Смартфон салаух галаси с6", P = 369 }, new G() { N = "Е***ШЙ ПОКА ХЗ ПРО 5Г УЛЬТРА", P = 699}, new G() { N = "Хими 1337 про макс плюс 6г мега омега альфа чё", P = 899}
        };
        private void delBTN_Click(object sender, RoutedEventArgs e)
        {
            if (oLB.SelectedItem != null)
            {
                tPTBR.Text = (decimal.Parse(tPTBR.Text) - (oLB.SelectedItem as G).P).ToString();
                TP -= (oLB.SelectedItem as G).P;
                OL.Remove(oLB.SelectedItem as G);
            }
            else
            {
                MessageBox.Show("Выбери товар для удаления");
            }
        }
        private void buyBTN_Click(object sender, RoutedEventArgs e)
        {
            var dis = TP switch
            {
                < 1000 => 0,
                >= 1000 and < 5000 => 5,
                >= 5000 => 10
            };
            MessageBox.Show($"Скидка - {dis}%, итого - {TP - ((TP / 100) * dis)}");
            OL = new();
            oLB.ItemsSource = OL;
            oLB.SelectedItem = null;
            TP = 0;
            tPTBR.Text = TP.ToString();
        }
        private void addBTN_Click(object sender, RoutedEventArgs e)
        {
            if (gCB.SelectedItem != null)
            {
                OL.Add(gCB.SelectedItem as G);
                tPTBR.Text = (decimal.Parse(tPTBR.Text) + (gCB.SelectedItem as G).P).ToString();
                TP += (gCB.SelectedItem as G).P;
            }
            else
            {
                MessageBox.Show("Выбери товар для добавления");
            }
        }
    }
}