using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace FinalFinanceTrack
{
    public partial class History : Window
    {
        private DbManager dbManager;

        public History()
        {
            InitializeComponent();
            dbManager = new DbManager();
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
            decimal savings = dbManager.GetTotalSavings(year, month);

            if (savings > 0)
            {
                HistoryTextBlock.Text = $"Total savings for {month}/{year}: €{savings:N2}";
            }
            else
            {
                HistoryTextBlock.Text = $"No savings found for {month}/{year}.";
            }
        }

        private void FilterByYear()
        {
            List<int> years = dbManager.GetDistinctYears();

            if (years.Count > 0)
            {
                HistoryTextBlock.Text = "Available years:\n" + string.Join("\n", years);
            }
            else
            {
                HistoryTextBlock.Text = "No years found.";
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
