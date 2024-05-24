using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for Expenses.xaml
    /// </summary>
    public partial class Expenses : Window
    {
        private NavigationManager highlightbtn;
        public Expenses()
        {
            InitializeComponent();
            PopulateCategories();
            highlightbtn = new NavigationManager();
            highlightbtn.SetActiveButton(ExpensesButton);
        }

        private void PopulateCategories()
        {
            CategoryComboBox.Items.Add("Food");
            CategoryComboBox.Items.Add("Utilities");
            CategoryComboBox.Items.Add("Rent");
            CategoryComboBox.SelectedIndex = 0;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the previous page
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
            
            this.Close();
            
        }

        private void ExpensesButton_Click(object sender, RoutedEventArgs e)
        {
            // Stay on the current page
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

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle the OK button click event
            SaveExpense();
        }

        private void SaveExpense()
        {
            if (!decimal.TryParse(AmountInput.Text, out decimal amount))
            {
                MessageBox.Show("Please enter a valid amount", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string selectedCategory = CategoryComboBox.SelectedItem.ToString();
            DateTime selectedDate = DatePicker.SelectedDate ?? DateTime.Now;

            MessageBox.Show($"Category: {selectedCategory}\nDate: {selectedDate.ToShortDateString()}\nAmount: €{amount}", "Expense Saved");

            // Reset the input fields
           /* AmountInput.Text = string.Empty;

            if (!decimal.TryParse(AmountInput.Text, out decimal amount))
            {
                MessageBox.Show("Please enter a valid amount", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string selectedCategory = CategoryComboBox.SelectedItem.ToString();
            DateTime selectedDate = DatePicker.SelectedDate ?? DateTime.Now;
            DbManager dbManager = new DbManager();

            if (dbManager.OpenConnection())
            {
                string query = "INSERT INTO expenses (Category, Amount, Date) VALUES (@Category, @Amount, @Date)";
                MySqlCommand cmd = new MySqlCommand(query, dbManager.Connection);
                cmd.Parameters.AddWithValue("@Category", selectedCategory);
                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Parameters.AddWithValue("@Date", selectedDate);
                cmd.ExecuteNonQuery();
                dbManager.CloseConnection();
                MessageBox.Show("Expense saved successfully!");
            }
            else
            {
                MessageBox.Show("Failed to connect to the database.");
            }
*/
        }

        private void AmountInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Validate that the input is numeric
            e.Handled = !decimal.TryParse(e.Text, out _);
        }
    }
}
