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
        public Income()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the Overview page
            this.Close();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the Settings page
            Settings settingsPage = new Settings();
            settingsPage.Show();
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
            // Navigate to the Savings page
            Savings savingsPage = new Savings();
            savingsPage.Show();
            this.Close();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle the OK button click event
            // Save income details or perform necessary actions
        }
    }

}
