﻿using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace FinalFinanceTrack
{
    public partial class ResetPassw : Window
    {
        private string userEmail;

        public ResetPassw()
        {
            InitializeComponent();
        }

        public ResetPassw(string email) : this()
        {
            userEmail = email;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = NewPasswordTextBox.Text;
            string confirmPassword = ConfirmPasswordTextBox.Text;

            if (!IsValidPassword(newPassword))
            {
                MessageBox.Show("Password must be at least 8 characters long, include at least one uppercase letter, one number, and one special character (.@§$+-*/\\<>).");
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("The passwords do not match. Please re-enter your passwords.");
                return;
            }

            DbManager dbManager = new DbManager();
            if (dbManager.UpdatePassword(userEmail, newPassword))
            {
                MessageBox.Show("Your password has been reset successfully.");
                LogIn loginWindow = new LogIn();
                loginWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to reset password. Please try again.");
            }
        }

        private bool IsValidPassword(string password)
        {
            var hasMinimum8Chars = new Regex(@".{8,}");
            var hasUpperCaseLetter = new Regex(@"[A-Z]+");
            var hasNumber = new Regex(@"\d+");
            var hasSpecialCharacter = new Regex(@"[.@§$+\-*/\\<>]");

            return hasMinimum8Chars.IsMatch(password) &&
                   hasUpperCaseLetter.IsMatch(password) &&
                   hasNumber.IsMatch(password) &&
                   hasSpecialCharacter.IsMatch(password);
        }

        private void NewPasswordTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Handle text changed event
        }
    }
}

