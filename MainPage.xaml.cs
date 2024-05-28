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
            UserMan userMan = new UserMan();
            userMan.Show();
            this.Hide();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            AdminSettings adminSettings = new AdminSettings(userId);
            adminSettings.Show();
            this.Close();
        }

        private void OverviewButton_Click(object sender, RoutedEventArgs e)
        {
            Overview overviewPage = new Overview();
            overviewPage.Show();
            this.Close();
        }

        private void ClickMeButton_Click(object sender, RoutedEventArgs e)
        {
            // Placeholder for additional code
        }
    }
}
