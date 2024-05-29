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

            // Initialize the DbManager
            dbManager = new DbManager(); 

            // Initialize with the current month
            LoadUserSavings(DateTime.Now.Month); 
        }


        private void LoadUserSavings(int month)
        {
            string email = Utility.GetCurrentUserEmail();
            int? userId = dbManager.GetUserIdByEmail(email);

            if (!userId.HasValue)
            {
                MessageBox.Show("User not found.");
                return;
            }

            CalculateSavings(userId.Value, month);
        }

        private void CalculateSavings(int userId, int month)
        {
            decimal totalIncome = DbManager.GetTotalIncomeForUser(userId, month, dbManager); // Pass the dbManager instance
            decimal totalExpenses = DbManager.GetTotalExpensesForUser(userId, month, dbManager); // Pass the dbManager instance
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
            // Navigate back to the previous page
            this.Close();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            int userId = Utility.GetCurrentUserId();
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
            if (MonthComboBox.SelectedIndex >= 0)
            {
                int selectedMonth = MonthComboBox.SelectedIndex + 1;
                LoadUserSavings(selectedMonth);
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
