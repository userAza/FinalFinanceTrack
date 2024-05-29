using System.Windows;

namespace FinalFinanceTrack
{
    public partial class AdminSettings : Window
    {
        private int? userId;

        public AdminSettings(int? userId = null)
        {
            InitializeComponent();
            this.userId = userId;

            if (!userId.HasValue)
            {
                MessageBox.Show("User ID is not set.");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage(userId.GetValueOrDefault());
            mainPage.Show();
            this.Close();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            AdminsLogIn adminLoginWindow = new AdminsLogIn();
            adminLoginWindow.Show();
            this.Close();
        }

        private void UpdatePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (userId.HasValue)
            {
                UpdatePasswAdmin updatePasswAdminWindow = new UpdatePasswAdmin(userId.Value);
                updatePasswAdminWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("User ID is not set. Cannot update password.");
            }
        }
    }
}
