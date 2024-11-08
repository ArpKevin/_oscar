using System.IO;
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

namespace _oscar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    public partial class MainWindow : Window
    {
        internal List<Oscar> oscars = new();
        string csvPath = @"..\..\..\src\oscar.csv";
        private readonly string categoryFilePath = @"..\..\..\src\categories.txt";
        public MainWindow()
        {
            InitializeComponent();
            LoadCategories();

            using StreamReader sr = new(csvPath, Encoding.UTF8);
            _ = sr.ReadLine();

            while (!sr.EndOfStream)
            {
                var x = sr.ReadLine().Split("\t");
                oscars.Add(new Oscar(x[0], x[1], int.Parse(x[2]), int.Parse(x[3]), int.Parse(x[4])));
            }

            oscarListBox.ItemsSource = oscars.Select(o => new {DijazasiEv = o.Ev, Cim = o.Cim}).OrderByDescending(x => x.DijazasiEv);
        }

        private void LoadCategories()
        {
            if (File.Exists(categoryFilePath))
            {
                var categories = File.ReadAllLines(categoryFilePath);
                categoryComboBox.ItemsSource = categories;
            }
        }

        private void OpenCategoryWindow(object sender, RoutedEventArgs e)
        {
            CategoryWindow categoryWindow = new CategoryWindow();
            categoryWindow.Closed += (s, args) => LoadCategories();
            categoryWindow.ShowDialog();
        }

        private void UjfilmfelveteleButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cimTextbox.Text) ||
                string.IsNullOrWhiteSpace(evTextbox.Text) ||
                string.IsNullOrWhiteSpace(jeloltekszamaTextbox.Text) ||
                string.IsNullOrWhiteSpace(dijakszamaTextbox.Text))
            {
                MessageBox.Show("Minden mezőt ki kell tölteni!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(evTextbox.Text, out int ev) ||
                !int.TryParse(jeloltekszamaTextbox.Text, out int jeloltekSzama) ||
                !int.TryParse(dijakszamaTextbox.Text, out int dijakSzama))
            {
                MessageBox.Show("Érvényes számokat adjon meg az év és a számláló mezőkben!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string cim = cimTextbox.Text.Trim();
            string id = cim.Length >= 4 ? cim.Substring(cim.Length - 4) : cim;
            id += ev % 100;

            using StreamWriter sw = new(csvPath,true,Encoding.UTF8);
            sw.WriteLine($"{id}\t{cim}\t{ev}\t{dijakSzama}\t{jeloltekSzama}");
            

            Oscar newOscar = new Oscar(id, cim, ev, dijakSzama, jeloltekSzama);
            oscars.Add(newOscar);

            oscarListBox.ItemsSource = oscars.Select(o => new { DijazasiEv = o.Ev, Cim = o.Cim }).OrderByDescending(x => x.DijazasiEv);
        }

        private void legtobbdijatkaptaButton_Click(object sender, RoutedEventArgs e)
        {
            filmcimeLabel.Content = oscars.MaxBy(o => o.Dij).Cim;
            filmcimeLabel.Foreground = Brushes.Black;
        }

        private void keresettfilmTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (keresettfilmTextbox.Text == "Keresett film")
            {
                keresettfilmTextbox.Text = "";
                keresettfilmTextbox.Foreground = Brushes.Black;
            }
        }

        private void keresettfilmTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(keresettfilmTextbox.Text))
            {
                keresettfilmTextbox.Text = "Keresett film";
                keresettfilmTextbox.Foreground = Brushes.Gray;
            }
        }

        private void keresButton_Click(object sender, RoutedEventArgs e)
        {
            string keresettFilm = keresettfilmTextbox.Text.ToLower();

            var matchingFilms = oscars.Where(o => o.Cim.ToLower().Contains(keresettFilm)).ToList();

            if (matchingFilms.Count > 0)
            {
                var random = new Random();
                var randomFilm = matchingFilms[random.Next(matchingFilms.Count)];

                talalatKiirasaTextbox.Content = randomFilm.Cim;
                talalatKiirasaTextbox.Foreground = Brushes.Black;
            }
            else
            {
                talalatKiirasaTextbox.Content = "Sajnos nincs ilyen film a listában";
                talalatKiirasaTextbox.Foreground = Brushes.Gray;
            }
        }

        private void listazbutton_Click(object sender, RoutedEventArgs e)
        {
            string keresettSzoveg = keresettszoTextbox.Text.ToLower();
            var matchingMovies = oscars
                .Where(o => o.Cim.ToLower().Contains(keresettSzoveg))
                .Select(o => o.Cim)
                .ToList();

            listazListbox.ItemsSource = matchingMovies;
        }
        private void keresettszoTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (keresettszoTextbox.Text == "Keresett szó")
            {
                keresettszoTextbox.Text = "";
                keresettszoTextbox.Foreground = Brushes.Black;
            }
        }

        private void keresettszoTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(keresettszoTextbox.Text))
            {
                keresettszoTextbox.Text = "Keresett szó";
                keresettszoTextbox.Foreground = Brushes.Gray;
            }
        }
    }
}