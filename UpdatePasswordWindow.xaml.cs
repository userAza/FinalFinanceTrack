using System;
using System.Windows;

namespace FinalFinanceTrack
{
    public partial class UpdatePasswordWindow : Window
    {
        private DbManager dbManager;
        private int userId;

        public UpdatePasswordWindow(int userId) // Accept userId as a parameter
        {
            InitializeComponent();
            dbManager = new DbManager();
            this.userId = userId; // Set the userId
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string oldPassword = oldPasswordBox.Password;
            string newPassword = newPasswordBox.Password;
            string confirmPassword = confirmPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("All fields are required.");
                return;
            }

            if (newPassword == confirmPassword)
            {
                string hashedOldPassword = Security.HashPassword(oldPassword);
                if (dbManager.ValidateOldPassword(userId, hashedOldPassword))
                {
                    string hashedNewPassword = Security.HashPassword(newPassword);
                    if (dbManager.UpdatePassword(userId, hashedNewPassword))
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
            this.Close();
        }
    }
}
