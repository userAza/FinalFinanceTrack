using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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
    /// Interaction logic for Income.xaml
    /// </summary>
    public partial class Income : Window
    {
        private NavigationManager highlightbtn;

        public Income()
        {
            InitializeComponent();
            highlightbtn = new NavigationManager();
            highlightbtn.SetActiveButton(IncomeButton);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the Overview page
            Overview overviewPage = new Overview();
            overviewPage.Show();
            this.Close();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the Settings page
            SettingsWindow settingsPage = new SettingsWindow();
            settingsPage.Show();
            this.Close();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the Overview page
            Overview overviewPage = new Overview();
            overviewPage.Show();
            this.Close();
        }

        private void IncomeButton_Click(object sender, RoutedEventArgs e)
        {
            // Stay on the current page
            highlightbtn.SetActiveButton(sender as Button);
        }

        private void ExpensesButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the Expenses page
            Expenses expensesPage = new Expenses();
            expensesPage.Show();
            this.Close();
            highlightbtn.SetActiveButton(sender as Button);
        }

        private void SavingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the Savings page
            Savings savingsPage = new Savings();
            savingsPage.Show();
            this.Close();
            highlightbtn.SetActiveButton(sender as Button);
        }

        private int GetIncomeCategoryId(string categoryName)
        {
            DbManager dbManager = new DbManager();
            return dbManager.GetIncomeCategoryId(categoryName);
        }


        private void OKButton_LinkClick(object sender, RoutedEventArgs e)
        {
            int userId = SessionUser.GetCurrentUserId(); // Ensure this static class is correctly implemented
            if (userId == 0)
            {
                MessageBox.Show("No user is currently logged in.");
                return;
            }

            if (!decimal.TryParse(IncomeAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid, positive amount.");
                return;
            }

            if (CategoryComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a category", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string selectedCategory = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            int categoryId = GetIncomeCategoryId(selectedCategory);
            DateTime selectedDate = IncomeDatePicker.SelectedDate ?? DateTime.Now;

            string query = "INSERT INTO income (Amount, Month, Year, categoryinc_ID) VALUES (@Amount, @Month, @Year, @CategoryId)";
            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    {"@Amount", amount},
                    {"@Month", selectedDate.Month.ToString()},
                    {"@Year", selectedDate.Year.ToString()},
                    {"@CategoryId", categoryId}
                };

            DbManager dbManager = new DbManager();
            if (dbManager.ExecuteQuery(query, parameters))
            {
                MessageBox.Show("Income data saved successfully!");
            }
            else
            {
                MessageBox.Show("Failed to insert income data.");
            }
        }

        private void IncomeAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!decimal.TryParse(IncomeAmount.Text, out _))
            {
                IncomeAmount.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                IncomeAmount.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void IncomeAmount_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "0.00")
            {
                textBox.Text = "";  // Clear the text when focus is gained
            }
        }

        private void IncomeAmount_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "0.00";  // Set back the placeholder text when focus is lost
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