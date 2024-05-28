using System.Windows;

namespace FinalFinanceTrack
{
    public partial class UpdatePasswAdmin : Window
    {
        private int userId;
        private DbManager dbManager;

        public UpdatePasswAdmin(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            dbManager = new DbManager();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string oldPassword = oldPasswordBox.Password;
            string newPassword = newPasswordBox.Password;
            string confirmPassword = confirmPasswordBox.Password;

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New passwords do not match.");
                return;
            }

            if (dbManager.UpdateUserPassword(userId, oldPassword, newPassword))
            {
                MessageBox.Show("Password updated successfully.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to update password. Please make sure the old password is correct.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BackToSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            AdminSettings adminSettings = new AdminSettings();
            adminSettings.Show();
            this.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AdminSettings adminSettings = new AdminSettings();
            adminSettings.Show();
            this.Close();
        }
    }
}
