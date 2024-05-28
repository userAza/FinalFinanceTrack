using System;
using System.Windows;
using System.Windows.Controls;

namespace FinalFinanceTrack
{
    public partial class Savings : Window
    {
        public Savings()
        {
            InitializeComponent();
            CalculateSavings(DateTime.Now.Month); // Initialize with the current month
        }

        private void CalculateSavings(int month)
        {
            // Assuming that you have methods to get the total income and expenses for a given month
            decimal totalIncome = GetTotalIncome(month);
            decimal totalExpenses = GetTotalExpenses(month);
            decimal savings = totalIncome - totalExpenses;

            SavingsAmount.Text = $"€ {savings:N2}";

            // Calculate savings percentage
            decimal savingsPercentage = totalIncome != 0 ? (savings / totalIncome) * 100 : 0;
            SavingsProgressBar.Value = (double)savingsPercentage;
        }

        private decimal GetTotalIncome(int month)
        {
            // Replace with actual logic to retrieve total income for the given month
            // This is a placeholder
            return month switch
            {
                1 => 1000.00m,
                2 => 1100.00m,
                3 => 1200.00m,
                4 => 1300.00m,
                5 => 1400.00m,
                6 => 1500.00m,
                7 => 1600.00m,
                8 => 1700.00m,
                9 => 1800.00m,
                10 => 1900.00m,
                11 => 2000.00m,
                12 => 2100.00m,
                _ => 1000.00m,
            };
        }

        private decimal GetTotalExpenses(int month)
        {
            // Replace with actual logic to retrieve total expenses for the given month
            // This is a placeholder
            return month switch
            {
                1 => 400.00m,
                2 => 500.00m,
                3 => 600.00m,
                4 => 700.00m,
                5 => 800.00m,
                6 => 900.00m,
                7 => 1000.00m,
                8 => 1100.00m,
                9 => 1200.00m,
                10 => 1300.00m,
                11 => 1400.00m,
                12 => 1500.00m,
                _ => 400.00m,
            };
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the previous page
            this.Close();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the Settings page
            int userId = Utility.GetCurrentUserId();
            SettingsWindow settingsPage = new SettingsWindow(userId);
            settingsPage.Show();
            this.Hide();
        }

        private void OverviewButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the Overview page
            Overview overviewPage = new Overview();
            overviewPage.Show();
            this.Close();
        }

        private void IncomeButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the Income page
            Income incomePage = new Income();
            incomePage.Show();
            this.Close();
        }

        private void ExpensesButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the Expenses page
            Expenses expensesPage = new Expenses();
            expensesPage.Show();
            this.Close();
        }

        private void SavingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Stay on the current page
        }

        private void MonthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MonthComboBox.SelectedIndex >= 0)
            {
                int selectedMonth = MonthComboBox.SelectedIndex + 1;
                CalculateSavings(selectedMonth);
            }
        }
        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            History historyPage = new History();
            historyPage.Show();
            this.Close();
        }

    }
}
