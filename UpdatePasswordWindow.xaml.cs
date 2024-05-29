using System;
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

            if (newPassword == confirmPassword)
            {
                if (dbManager.ValidateOldPassword(email, oldPassword))
                {
                    if (dbManager.UpdatePassword(email, newPassword))
                    {
                        MessageBox.Show("New password saved.");
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
    }
}
