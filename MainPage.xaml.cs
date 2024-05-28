using System.Windows;

namespace FinalFinanceTrack
{
    public partial class MainPage : Window
    {
        private int userId;

        public MainPage(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void UserManagementButton_Click(object sender, RoutedEventArgs e)
        {
            UserMan userManagement = new UserMan(userId);
            userManagement.Show();
            this.Close();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            AdminSettings adminSettings = new AdminSettings(userId);
            adminSettings.Show();
            this.Close();
        }
    }
}
