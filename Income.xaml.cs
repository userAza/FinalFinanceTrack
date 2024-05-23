using MySql.Data.MySqlClient;
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
    /// Interaction logic for Income.xaml
    /// </summary>
    public partial class Income : Window
    {
        private NavigationManager highlightbtn;

        public Income()
        {
            InitializeComponent();
            highlightbtn = new NavigationManager();
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
            DbManager dbManager = new DbManager();

            // Example fields: incomeAmount (TextBox), incomeSource (TextBox)
            /*  decimal amount;
              if (decimal.TryParse(incomeAmount.Text, out amount) && !string.IsNullOrEmpty(incomeSource.Text))
              {
                  if (dbManager.OpenConnection())
                  {
                      string query = "INSERT INTO income (Amount, Source, Date) VALUES (@Amount, @Source, @Date)";
                      MySqlCommand cmd = new MySqlCommand(query, dbManager.Connection);
                      cmd.Parameters.AddWithValue("@Amount", amount);
                      cmd.Parameters.AddWithValue("@Source", incomeSource.Text);
                      cmd.Parameters.AddWithValue("@Date", DateTime.Now);  // Or any date picker control
                      cmd.ExecuteNonQuery();
                      dbManager.CloseConnection();
                      MessageBox.Show("Income data saved successfully!");
                  }
              }
              else
              {
                  MessageBox.Show("Please check your input and try again.");
              }
          }
  */


        }

    }
}
