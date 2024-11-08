using System.IO;
using System.Windows;
using System.Windows.Media;

namespace _oscar
{
    public partial class CategoryWindow : Window
    {
        private readonly string categoryFilePath = @"..\..\..\src\categories.txt";

        public CategoryWindow()
        {
            InitializeComponent();
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            string category = categoryTextbox.Text.Trim();

            if (!string.IsNullOrWhiteSpace(category) && category != "Add meg a kategória nevét")
            {
                // Ensure the directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(categoryFilePath));

                File.AppendAllText(categoryFilePath, category + "\n");
                MessageBox.Show("Kategória sikeresen hozzáadva!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
                categoryTextbox.Clear();
                categoryTextbox.Text = "Add meg a kategória nevét";
                categoryTextbox.Foreground = Brushes.Gray;
            }
            else
            {
                MessageBox.Show("Helyes kategórianevet adj meg.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void kategoriaTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (categoryTextbox.Text == "Add meg a kategória nevét")
            {
                categoryTextbox.Text = "";
                categoryTextbox.Foreground = Brushes.Black;
            }
        }

        private void kategoriaTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(categoryTextbox.Text))
            {
                categoryTextbox.Text = "Add meg a kategória nevét";
                categoryTextbox.Foreground = Brushes.Gray;
            }
        }
    }
}
