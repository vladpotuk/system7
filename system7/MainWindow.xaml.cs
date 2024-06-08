using System;
using System.Collections.Generic;
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

        private async void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(LimitTextBox.Text, out int limit) && limit >= 0)
            {
                FibonacciListBox.Items.Clear();
                List<int> fibonacciNumbers = await Task.Run(() => GenerateFibonacci(limit));
                foreach (int number in fibonacciNumbers)
                {
                    FibonacciListBox.Items.Add(number);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid non-negative integer.");
            }
        }

        private List<int> GenerateFibonacci(int limit)
        {
            List<int> fibonacciNumbers = new List<int>();
            int a = 0, b = 1;
            fibonacciNumbers.Add(a);

            while (b <= limit)
            {
                fibonacciNumbers.Add(b);
                int temp = a + b;
                a = b;
                b = temp;
            }

            return fibonacciNumbers;
        }
    }
}
