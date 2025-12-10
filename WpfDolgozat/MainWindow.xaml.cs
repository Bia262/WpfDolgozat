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
using System.IO;

namespace WpfDolgozat
{
    public class Dolgozat
    {
        public String Nev {  get; set; }
        public int Eletkor { get; set; }
        public int Pontszan { get; set; }

        public Dolgozat(String nev, int eletkor, int pontszam)
        {
            Nev = nev;
            Eletkor = eletkor;
            Pontszan = pontszam;
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
                string[] darabok = s.Split(";");
                string neve = darabok[0];
                int eletkora = Convert.ToInt32(darabok[1]);
                int pontszama = Convert.ToInt32(darabok[2]);
                dolgozatok.Add(new Dolgozat(neve, eletkora, pontszama));
                
            }
            dataGrid.ItemsSource = dolgozatok;
        }
        private void hozzaadas (object sender, EventArgs e)
        {
            int eletkora;
            int pontszama;
            if(nev.Text.Length > 0 
                && int.TryParse(eletkor.Text,out eletkora)
                &&eletkora>6&& int.TryParse(pontszam.Text,out pontszama) 
                && pontszama>=0
                && pontszama <=100)
            {
                dolgozatok.Add(new Dolgozat(nev.Text,eletkora,pontszama));
                dataGrid.ItemsSource= dolgozatok;
                dataGrid.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Hibás");
            }
        }
    }
}