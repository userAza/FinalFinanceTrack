using System;
using System.Windows;

namespace FinalFinanceTrack
{
    public partial class ResetPasswordWindow : Window
    {
        private string adminEmail;

        public ResetPasswordWindow(string email)
        {
            InitializeComponent();
            adminEmail = email;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = newPasswordBox.Password.Trim();
            string confirmPassword = confirmPasswordBox.Password.Trim();

            if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Please enter and confirm the new password.");
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("The passwords do not match. Please re-enter your passwords.");
                return;
            }

            DbManager dbManager = new DbManager();
            bool isUpdated = dbManager.UpdateAdminPassword(adminEmail, newPassword);

            if (isUpdated)
            {
                MessageBox.Show("Your password has been reset successfully.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to reset password. Please try again.");
            }
        }
    }
}
