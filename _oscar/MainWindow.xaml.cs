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
        public MainWindow()
        {
            InitializeComponent();

            using StreamReader sr = new(@"..\..\..\src\oscar.csv");
            _ = sr.ReadLine();

            while (!sr.EndOfStream) oscars.Add(new Oscar(sr.ReadLine()));

            oscarListBox.ItemsSource = oscars;
        }
    }
}