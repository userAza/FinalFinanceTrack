using System.Windows.Controls;
using System.Windows;

namespace FinalFinanceTrack
{
    public partial class Savings : Window
    {
        private DbManager dbManager;

        public Savings()
        {
            InitializeComponent();
            dbManager = new DbManager();
        }

        private void LoadUserSavings(int userId, int month)
        {
            CalculateSavings(userId, month);
        }

        private void CalculateSavings(int userId, int month)
        {
            decimal totalIncome = dbManager.GetTotalIncomeForUser(userId, month);
            decimal totalExpenses = dbManager.GetTotalExpensesForUser(userId, month);
            decimal savings = totalIncome - totalExpenses;

            SavingsAmount.Text = $"€ {savings:N2}";

            // Calculate savings percentage
            decimal savingsPercentage = totalIncome != 0 ? (savings / totalIncome) * 100 : 0;
            SavingsProgressBar.Value = (double)savingsPercentage;

            // Display the savings percentage on the progress bar
            SavingsPercentage.Text = $"{savingsPercentage:N2}%";
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Overview overviewPage = new Overview();
            overviewPage.Show();
            this.Close();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            int userId = SessionUser.GetCurrentUserId();
            SettingsWindow settingsPage = new SettingsWindow(userId);
            settingsPage.Show();
            this.Hide();
        }

        private void OverviewButton_Click(object sender, RoutedEventArgs e)
        {
            Overview overviewPage = new Overview();
            overviewPage.Show();
            this.Close();
        }

        private void IncomeButton_Click(object sender, RoutedEventArgs e)
        {
            Income incomePage = new Income();
            incomePage.Show();
            this.Close();
        }

        private void ExpensesButton_Click(object sender, RoutedEventArgs e)
        {
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
            // Optional: Handle month selection if needed
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle OK button click
            if (MonthComboBox.SelectedIndex >= 0)
            {
                int selectedMonth = MonthComboBox.SelectedIndex + 1;
                int userId = SessionUser.GetCurrentUserId();
                if (userId != 0)
                {
                    LoadUserSavings(userId, selectedMonth);
                }
                else
                {
                    MessageBox.Show("User not found.");
                }
            }
        }
    }
}
