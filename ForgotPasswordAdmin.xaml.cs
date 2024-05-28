using System;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;

namespace FinalFinanceTrack
{
    public partial class ForgotPasswordAdmin : Window
    {
        public ForgotPasswordAdmin()
        {
            InitializeComponent();
        }

        private void RemovePlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Foreground == Brushes.Gray)
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void AddPlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Email";
                textBox.Foreground = Brushes.Gray;
            }
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string adminEmail = adminEmailTextBox.Text.Trim();
            if (!IsValidEmail(adminEmail))
            {
                MessageBox.Show("Please enter a valid email address.");
                return;
            }

            if (string.IsNullOrWhiteSpace(adminSecurityQuestion1TextBox.Text) ||
                string.IsNullOrWhiteSpace(adminSecurityQuestion2TextBox.Text) ||
                string.IsNullOrWhiteSpace(adminSecurityQuestion3TextBox.Text))
            {
                MessageBox.Show("Please answer all security questions.");
                return;
            }

            DbManager dbManager = new DbManager();
            bool isValid = dbManager.ValidateSecurityQuestions(adminEmail,
                adminSecurityQuestion1TextBox.Text,
                adminSecurityQuestion2TextBox.Text,
                adminSecurityQuestion3TextBox.Text);

            if (!isValid)
            {
                MessageBox.Show("One or more answers are incorrect. Please try again.");
                return;
            }

            ResetPasswordWindow resetPasswordWindow = new ResetPasswordWindow(adminEmail);
            resetPasswordWindow.ShowDialog(); // Use ShowDialog to make it modal

            // Close the current ForgotPasswordAdmin window after opening ResetPasswordWindow
            this.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            // Close the ForgotPasswordAdmin window
            this.Close();
        }
    }
}
