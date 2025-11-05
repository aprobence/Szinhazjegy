using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Színházjegy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int jegyar = 3500;
        private const string filePath = "vasarlasok.txt";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnVasarlas_Click(object sender, RoutedEventArgs e)
        {
            string nev = txtNev.Text.Trim();
            string email = txtEmail.Text.Trim();
            string eloadas = (cmbEloadas.SelectedItem as ComboBoxItem)?.Content.ToString();
            string strJegyekSzama = txtJegyekSzama.Text.Trim();

            if (string.IsNullOrEmpty(nev) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(eloadas) || string.IsNullOrEmpty(strJegyekSzama))
            {
                MessageBox.Show("Kérem, töltse ki az összes mezőt!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(strJegyekSzama, out int jegyekSzama) || jegyekSzama <= 0)
            {
                MessageBox.Show("Kérem, érvényes jegyszámot adjon meg!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int osszeg = jegyekSzama * jegyar;

            MessageBox.Show($"Kedves {nev}!\n\nAz Ön által választott előadás: {eloadas}\nJegyek száma: {jegyekSzama}\nFizetendő összeg: {osszeg} Ft\n\nKöszönjük a vásárlást!", "Vásárlás sikeres", MessageBoxButton.OK, MessageBoxImage.Information);

            try
            {
                string sor = $"{nev};{email};{eloadas};{jegyekSzama};{osszeg}";
                File.AppendAllText(filePath, sor + Environment.NewLine, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt a fájl mentése közben:\n{ex.Message}", "Fájl hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            txtNev.Clear();
            txtEmail.Clear();
            txtJegyekSzama.Clear();
            cmbEloadas.SelectedIndex = 0;
        }

        private void Betoltes()
        {
            lstVasarlasok.Items.Clear();
            if (!File.Exists(filePath))
                return;
            var lines = File.ReadAllLines(filePath, Encoding.UTF8);
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length == 5)
                {
                    string nev = parts[0];
                    string email = parts[1];
                    string eloadas = parts[2];
                    string jegyekSzama = parts[3];
                    string osszeg = parts[4];
                    lstVasarlasok.Items.Add($"{nev} | {email} | {eloadas} | {jegyekSzama} db | {osszeg} Ft");
                }
            }
        }

        private void btnBetoltes_Click(object sender, RoutedEventArgs e)
        {
            Betoltes();
            MessageBox.Show("Vásárlások sikeresen betöltve!", "Betöltés", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}