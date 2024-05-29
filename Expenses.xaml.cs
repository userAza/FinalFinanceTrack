using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FinalFinanceTrack
{
    public partial class Expenses : Window
    {
        private NavigationManager highlightbtn;

        public Expenses()
        {
            InitializeComponent();
            PopulateCategories();
            highlightbtn = new NavigationManager();
            if (ExpensesButton != null)
                highlightbtn.SetActiveButton(ExpensesButton);
        }

        private void PopulateCategories()
        {
            CategoryComboBox.Items.Add(new ComboBoxItem { Content = "Food" });
            CategoryComboBox.Items.Add(new ComboBoxItem { Content = "Utilities" });
            CategoryComboBox.Items.Add(new ComboBoxItem { Content = "Rent" });
            CategoryComboBox.SelectedIndex = 0;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the previous page or close the current window
            Overview overviewPage = new Overview();
            overviewPage.Show();
            this.Close();
        }
        private int GetCurrentUserId()
        {
            // Implement this method to retrieve the current user's ID.
            // This is just a placeholder implementation.
            return SessionUser.GetCurrentUserId();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            int userId = GetCurrentUserId();
            SettingsWindow settingsPage = new SettingsWindow(userId);
            settingsPage.Show();
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
            // Optional: This button might simply highlight, indicating you're on the current page
        }

        private void SavingsButton_Click(object sender, RoutedEventArgs e)
        {
            Savings savingsPage = new Savings();
            savingsPage.Show();
            this.Close();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            SaveExpense();
        }

        private void SaveExpense()
        {
            int userId = GetCurrentUserId();

            if (!decimal.TryParse(AmountInput.Text, out decimal amount))
            {
                MessageBox.Show("Please enter a valid amount", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (CategoryComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a category", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string selectedCategory = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            DateTime selectedDate = DatePicker.SelectedDate ?? DateTime.Now;

            DbManager dbManager = new DbManager();
            int categoryId = dbManager.GetExpenseCategoryId(selectedCategory); // Get the category ID

            if (categoryId == -1)
            {
                MessageBox.Show("Invalid category selected.");
                return;
            }

            if (dbManager.InsertExpense(userId, amount, selectedDate.ToString("MMMM"), selectedDate.Year.ToString(), categoryId))
            {
                MessageBox.Show("Expense saved successfully!");
            }
            else
            {
                MessageBox.Show("Failed to save expense.");
            }
        }

        private void AmountInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !decimal.TryParse(e.Text, out _); // Ensure only numeric input
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            History historyPage = new History();
            historyPage.Show();
            this.Close();
        }
    }
}
