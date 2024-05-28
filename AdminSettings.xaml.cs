using System.Windows;

namespace FinalFinanceTrack
{
    public partial class AdminSettings : Window
    {
        private int userId;

        public AdminSettings(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage(userId);
            mainPage.Show();
            this.Close();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            LogIn loginWindow = new LogIn();
            loginWindow.Show();
            this.Close();
        }

        private void UpdatePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            DbManager dbManager = new DbManager();
            string userEmail = dbManager.GetUserEmailById(userId); // New method to get email by user ID
            if (userEmail != null)
            {
                ResetPassw resetPassw = new ResetPassw(userEmail);
                resetPassw.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to find user email. Please try again.");
            }
        }
    }
}
