using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace FinalFinanceTrack
{
    public partial class UpdatePasswAdmin : Window
    {
        private DbManager dbManager;
        private int userId;

        public UpdatePasswAdmin(int userId)
        {
            InitializeComponent();
            dbManager = new DbManager();
            this.userId = userId;
        }

        private void AdminOkButton_Click(object sender, RoutedEventArgs e)
        {
            string email = adminEmailTextBox.Text;
            string oldPassword = adminOldPasswordBox.Password;
            string newPassword = adminNewPasswordBox.Password;
            string confirmPassword = adminConfirmPasswordBox.Password;

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

            string dbEmail = dbManager.GetAdminEmailById(userId);
            if (dbEmail != email)
            {
                MessageBox.Show("Email does not match.");
                return;
            }

            if (newPassword == confirmPassword)
            {
                if (dbManager.ValidateOldPassword(email, oldPassword))
                {
                    if (dbManager.UpdatePassword(email, newPassword))
                    {
                        MessageBox.Show("New password saved.");
                        AdminSettings adminSettingsWindow = new AdminSettings(userId);
                        adminSettingsWindow.Show();
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

        private void AdminCancelButton_Click(object sender, RoutedEventArgs e)
        {
            AdminSettings adminSettingsWindow = new AdminSettings(userId); // Return to AdminSettings
            adminSettingsWindow.Show();
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
