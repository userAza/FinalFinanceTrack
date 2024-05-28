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

        private void BackToMainPage_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage(userId);
            mainPage.Show();
            this.Close();
        }

        private void UpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            UpdatePasswAdmin updatePasswordWindow = new UpdatePasswAdmin(userId);
            updatePasswordWindow.Show();
            this.Close();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
