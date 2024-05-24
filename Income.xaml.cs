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



        private void PopulateMonthAndYear()
        {
            // Populate months
            string[] months = new string[] { "January", "February", "March", "April", "May", "June",
                                         "July", "August", "September", "October", "November", "December" };
            foreach (string month in months)
            {
                MonthComboBox.Items.Add(month);
            }
            MonthComboBox.SelectedIndex = DateTime.Now.Month - 1; // Set current month selected

            // Populate years from 10 years ago to the current year
            int currentYear = DateTime.Now.Year;
            for (int year = currentYear - 10; year <= currentYear; year++)
            {
                YearComboBox.Items.Add(year);
            }
            YearComboBox.SelectedItem = currentYear; // Set current year selected
        }



        // Assuming you have UI elements to capture income details such as amount, source, etc.
        private void OKButton_Click(object sender, RoutedEventArgs e)
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

            string source = CategoryComboBox.Text;
            if (string.IsNullOrEmpty(source))
            {
                MessageBox.Show("Please select a source.");
                return;
            }

            if (MonthComboBox.SelectedItem == null || YearComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select both month and year.");
                return;
            }

            string selectedMonth = MonthComboBox.SelectedItem.ToString();
            string selectedYear = YearComboBox.SelectedItem.ToString();

            DbManager dbManager = new DbManager();
            if (!dbManager.InsertIncome(userId, amount, selectedMonth, selectedYear))
            {
                MessageBox.Show("Failed to insert income data.");
            }
            else
            {
                MessageBox.Show("Income data saved successfully!");
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

    }

}

