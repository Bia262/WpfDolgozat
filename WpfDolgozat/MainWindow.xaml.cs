using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace dolgozatok
{

    public class Dolgozat
    {
        public string Nev { get; set; }
        public int Eletkor { get; set; }
        public int Pontszam { get; set; }

        public Dolgozat(string nev, int eletkor, int pontszam)
        {
            Nev = nev;
            Eletkor = eletkor;
            Pontszam = pontszam;
        }
    }
    public partial class MainWindow : Window
    {

        public List<Dolgozat> dolgozatok = new List<Dolgozat>();

        public MainWindow()
        {
            InitializeComponent();

            var sorok = File.ReadAllLines("dolgozatok.txt").Skip(1);
            foreach (string s in sorok)
            {
                string[] darabok = s.Split(';');
                string neve = darabok[0];
                int eletkora = int.Parse(darabok[1]);
                int pontszama = int.Parse(darabok[2]);
                dolgozatok.Add(new Dolgozat(neve, eletkora, pontszama));
            }
            dataGrid.ItemsSource = dolgozatok;
        }

        private void hozzaadas(object sender, RoutedEventArgs e)
        {
            int eletkora;
            int pontszama;
            if (nev.Text.Length > 0
                && int.TryParse(eletkor.Text, out eletkora)
                && eletkora > 6
                && int.TryParse(pontszam.Text, out pontszama)
                && pontszama >= 0
                && pontszama <= 100)
            {
                dolgozatok.Add(new Dolgozat(nev.Text, eletkora, pontszama));
                dataGrid.ItemsSource = dolgozatok;
                dataGrid.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Hibás adatbevitel!");
            }
        }

        private void mentes(object sender, RoutedEventArgs e)
        {
            string fajlba = "";
            foreach (var i in dolgozatok)
            {
                fajlba += i.Nev + ";" + i.Eletkor + ";" + i.Pontszam + "\n";
            }
            try
            {
                File.WriteAllText("dolgozatok.txt", fajlba);
                MessageBox.Show($"Sikeres Mentés!", $"Infó", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show("Hiba a fájl mentése során: " + ae.Message);
            }
            catch (IOException ioe)
            {
                MessageBox.Show("Hiba a fájl mentése során: " + ioe.Message);
            }
        }
    }
}