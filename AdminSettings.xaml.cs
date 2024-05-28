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
            // Navigate to the reset password window
            ResetPassw resetPassw = new ResetPassw(userId);
            resetPassw.Show();
            this.Close();
        }
    }
}
