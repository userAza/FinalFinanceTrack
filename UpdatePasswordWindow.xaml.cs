﻿using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace FinalFinanceTrack
{
    public partial class UpdatePasswordWindow : Window
    {
        private DbManager dbManager;

        public UpdatePasswordWindow()
        {
            InitializeComponent();
            dbManager = new DbManager();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string email = emailTextBox.Text;
            string oldPassword = oldPasswordBox.Password;
            string newPassword = newPasswordBox.Password;
            string confirmPassword = confirmPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("All fields are required.");
                return;
            }

            if (!IsValidPassword(newPassword))
            {
                MessageBox.Show("Password must be at least 8 characters long, include at least one uppercase letter, one special character (.@§$+-*/\\<>), and one number (0-9).");
                return;
            }

            if (newPassword == confirmPassword)
            {
                if (dbManager.ValidateOldPassword(email, oldPassword))
                {
                    if (dbManager.UpdatePassword(email, newPassword))
                    {
                        MessageBox.Show("New password saved.");
                        // After the message box is closed, show the SettingsWindow
                        SettingsWindow settingsWindow = new SettingsWindow();
                        settingsWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to save the new password.");
                    }
                }
                else
                {
                    MessageBox.Show("Old password is incorrect.");
                }
            }
            else
            {
                MessageBox.Show("New password and confirmation do not match.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow(); // No need to pass userId
            settingsWindow.Show();
            this.Close();
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
    }
}
