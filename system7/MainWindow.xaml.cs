using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace system7
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string word = WordTextBox.Text;
            string filePath = FilePathTextBox.Text;

            if (string.IsNullOrWhiteSpace(word) || string.IsNullOrWhiteSpace(filePath))
            {
                MessageBox.Show("Please enter both a word and a file path.");
                return;
            }

            ResultTextBlock.Text = "Searching...";
            int count = await Task.Run(() => CountWordInFile(word, filePath));
            ResultTextBlock.Text = $"The word '{word}' appears {count} times in the file.";
        }

        private int CountWordInFile(string word, string filePath)
        {
            int count = 0;
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] words = line.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string fileWord in words)
                    {
                        if (string.Equals(fileWord, word, StringComparison.OrdinalIgnoreCase))
                        {
                            count++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading file: {ex.Message}");
            }
            return count;
        }
    }
}
