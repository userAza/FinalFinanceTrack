using System;
using System.Windows;

namespace FinalFinanceTrack
{
    public partial class UpdatePasswordWindow : Window
    {
        private DbManager dbManager;
        private int userId = 1; // Replace with the actual user ID

        public UpdatePasswordWindow()
        {
            InitializeComponent();
            dbManager = new DbManager();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string oldPassword = oldPasswordBox.Password;
            string newPassword = newPasswordBox.Password;
            string confirmPassword = confirmPasswordBox.Password;

            if (newPassword == confirmPassword)
            {
                string hashedOldPassword = Security.HashPassword(oldPassword);
                if (dbManager.ValidateOldPassword(userId, hashedOldPassword))
                {
                    string hashedNewPassword = Security.HashPassword(newPassword);
                    if (dbManager.UpdatePassword(userId, hashedNewPassword))
                    {
                        MessageBox.Show("New password saved.");
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
            this.Close();
        }

        private void BackToSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
            this.Close();
        }
    }
}
