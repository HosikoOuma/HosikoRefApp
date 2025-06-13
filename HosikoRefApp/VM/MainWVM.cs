using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using HosikoRefApp.COM;
using HosikoRefApp.CORE;
using HosikoRefApp.M;
//using HosikoRefApp.V;

namespace HosikoRefApp.VM
{
    public class MainWVM : BaseVM
    {
        public ObservableCollection<G> GL { get; set; } = new();
        public ObservableCollection<G> OL { get; set; } = new();

        private decimal tp;
        public decimal TP
        {
            get => tp;
            set { tp = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand BuyCommand { get; }

        private readonly string _p = @"./D/";

        public MainWVM()
        {
            AddCommand = new DelCom(Add, CanAdd);
            DeleteCommand = new DelCom(Delete, CanDelete);
            BuyCommand = new DelCom(Buy, () => OL.Any());

            _ = LoadDataAsync();
        }

        public G SelectedGood { get; set; }
        public G SelectedOrder { get; set; }

        private async Task LoadDataAsync()
        {
            if (!Directory.Exists(_p)) Directory.CreateDirectory(_p);

            var path = Path.Combine(_p, $"G.txt");
            if (!File.Exists(path))
            {
                File.WriteAllLines(path, HosikoRefApp.CORE.SIN.SIN.S_GL.Select(g => $"{g.N}, {g.P}"));
            }

            var data = await File.ReadAllLinesAsync(path);
            foreach (var line in data)
            {
                var parts = line.Split(", ");
                GL.Add(new G { N = parts[0], P = decimal.Parse(parts[1]) });
            }
        }

        private void Add()
        {
            if (SelectedGood == null) return;

            OL.Add(SelectedGood);
            TP += SelectedGood.P;
        }

        private bool CanAdd() => SelectedGood != null;

        private void Delete()
        {
            if (SelectedOrder == null) return;

            OL.Remove(SelectedOrder);
            TP -= SelectedOrder.P;
        }

        private bool CanDelete() => SelectedOrder != null;

        private void Buy()
        {
            var dis = HosikoRefApp.CORE.CD.CoD(TP);

            MessageBox.Show($"Скидка - {dis}%, итого - {TP - ((TP / 100) * dis)}");

            OL.Clear();
            TP = 0;
        }

    //    public static List<G> S_GL = new()
    //{
    //    new G() { N = "Смартфон салаух галаси с6", P = 369 },
    //    new G() { N = "Е***ШЙ ПОКА ХЗ ПРО 5Г УЛЬТРА", P = 699 },
    //    new G() { N = "Хими 1337 про макс плюс 6г мега омега альфа чё", P = 899 }
    //};

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged([CallerMemberName] string name = null)
        //    => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

}

