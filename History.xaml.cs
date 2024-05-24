using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using MySql.Data.MySqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace FinalFinanceTrack
{
    public partial class History : Window
    {
        private string connectionString = "server=127.0.0.1;user=root;database=budget;Password="; 
        public History()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
            Overview overviewPage = new Overview();
            overviewPage.Show();
            this.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            
            string searchText = SearchTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(searchText))
            {
                string[] parts = searchText.Split(' ');
                if (parts.Length == 2 && int.TryParse(parts[0], out int year) && int.TryParse(parts[1], out int month))
                {
                    DisplaySavings(year, month);
                }
                else
                {
                    MessageBox.Show("Please enter a valid year and month (e.g., 2023 05).");
                }
            }
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            
            FilterByYear();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            
            SaveHistoryAsPdf();
        }

        private void DisplaySavings(int year, int month)
        {
            string query = "SELECT SUM(Savings) FROM History WHERE YEAR(Date) = @Year AND MONTH(Date) = @Month";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Year", year);
                command.Parameters.AddWithValue("@Month", month);

                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    decimal savings = Convert.ToDecimal(result);
                    HistoryTextBlock.Text = $"Total savings for {month}/{year}: €{savings:N2}";
                }
                else
                {
                    HistoryTextBlock.Text = $"No savings found for {month}/{year}.";
                }
            }
        }

        private void FilterByYear()
        {
            string query = "SELECT DISTINCT YEAR(Date) AS Year FROM History";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                List<int> years = new List<int>();
                while (reader.Read())
                {
                    years.Add(reader.GetInt32(0));
                }

                if (years.Count > 0)
                {
                    HistoryTextBlock.Text = "Available years:\n" + string.Join("\n", years);
                }
                else
                {
                    HistoryTextBlock.Text = "No years found.";
                }
            }
        }

        private void SaveHistoryAsPdf()
        {
            string pdfPath = "History.pdf";
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(pdfPath, FileMode.Create));
            document.Open();

            Paragraph paragraph = new Paragraph(HistoryTextBlock.Text);
            document.Add(paragraph);

            document.Close();

            MessageBox.Show($"History saved as PDF: {pdfPath}");
        }
    }
}
