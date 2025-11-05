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

        private void btnVásárlás_Click(object sender, RoutedEventArgs e)
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

            txtNev.Clear();
            txtEmail.Clear();
            txtJegyekSzama.Clear();
            cmbEloadas.SelectedIndex = 0;
        }
    }
}