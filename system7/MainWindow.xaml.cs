using System;
using System.Collections.Generic;
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
            string directoryPath = DirectoryPathTextBox.Text;

            if (string.IsNullOrWhiteSpace(word) || string.IsNullOrWhiteSpace(directoryPath))
            {
                MessageBox.Show("Please enter both a word and a directory path.");
                return;
            }

            ResultsListBox.Items.Clear();
            List<SearchResult> results = await Task.Run(() => SearchWordInDirectory(word, directoryPath));

            foreach (var result in results)
            {
                ResultsListBox.Items.Add($"{result.FileName}: {result.FilePath} - {result.Count} times");
            }
        }

        private List<SearchResult> SearchWordInDirectory(string word, string directoryPath)
        {
            List<SearchResult> results = new List<SearchResult>();
            try
            {
                foreach (string file in Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories))
                {
                    int count = 0;
                    try
                    {
                        string[] lines = File.ReadAllLines(file);
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
                        if (count > 0)
                        {
                            results.Add(new SearchResult
                            {
                                FileName = Path.GetFileName(file),
                                FilePath = file,
                                Count = count
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error accessing directory: {ex.Message}");
            }
            return results;
        }
    }

    public class SearchResult
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int Count { get; set; }
    }
}
