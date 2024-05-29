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
        private int userId;

        public History()
        {
            InitializeComponent();
            dbManager = new DbManager();
            userId = Utility.GetCurrentUserId();
            LoadUserHistory();
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

        private void LoadUserHistory()
        {
            List<IncomeRecord> incomes = dbManager.GetUserIncome(userId);
            List<ExpenseRecord> expenses = dbManager.GetUserExpenses(userId);
            DisplayHistory(incomes, expenses);
        }

        private void DisplayHistory(List<IncomeRecord> incomes, List<ExpenseRecord> expenses)
        {
            HistoryTextBlock.Text = "User Income and Expense History:\n";

            HistoryTextBlock.Text += "Incomes:\n";
            foreach (var income in incomes)
            {
                HistoryTextBlock.Text += $"{income.Month}/{income.Year} - {income.Category}: €{income.Amount:N2}\n";
            }

            HistoryTextBlock.Text += "\nExpenses:\n";
            foreach (var expense in expenses)
            {
                HistoryTextBlock.Text += $"{expense.Month}/{expense.Year} - {expense.Category}: €{expense.Amount:N2}\n";
            }
        }

        private void FilterByCategory(string category)
        {
            List<IncomeRecord> incomes = dbManager.GetUserIncomeByCategory(userId, category);
            List<ExpenseRecord> expenses = dbManager.GetUserExpensesByCategory(userId, category);
            DisplayHistory(incomes, expenses);
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


public class IncomeRecord
    {
        public int ID { get; set; }
        public decimal Amount { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Category { get; set; }
    }

    public class ExpenseRecord
    {
        public int ID { get; set; }
        public decimal Amount { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Category { get; set; }
    }

