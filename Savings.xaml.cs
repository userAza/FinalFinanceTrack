using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FinalFinanceTrack
{
    /// <summary>
    /// Interaction logic for Savings.xaml
    /// </summary>
    public partial class Savings : Window
    {
        public Savings()
        {
            InitializeComponent();
            CalculateSavings();
        }

        private void CalculateSavings()
        {
            // Assuming that you have a method to get the total income and expenses
            decimal totalIncome = GetTotalIncome();
            decimal totalExpenses = GetTotalExpenses();
            decimal savings = totalIncome - totalExpenses;

            SavingsAmount.Text = $"€ {savings:N2}";

            // Calculate savings percentage
            decimal savingsPercentage = totalIncome != 0 ? (savings / totalIncome) * 100 : 0;
            SavingsProgressBar.Value = (double)savingsPercentage;
        }

        private decimal GetTotalIncome()
        {
            // Replace with actual logic to retrieve total income
            // This is a placeholder
            return 1000.00m;
        }

        private decimal GetTotalExpenses()
        {
            // Replace with actual logic to retrieve total expenses
            // This is a placeholder
            return 400.00m;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the previous page
            this.Close();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the Settings page
            SettingsWindow settingsPage = new SettingsWindow();
            settingsPage.Show();
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
    }
}
