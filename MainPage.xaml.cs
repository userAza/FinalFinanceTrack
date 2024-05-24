using FinalFinanceTrack;
using System.Windows;

namespace FinalFinanceTrack
{
    public partial class MainPage : Window
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void UserManagementButton_Click(object sender, RoutedEventArgs e)
        {
            UserMan userMan = new UserMan();
            userMan.Show();
            this.Hide();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            
            SettingsWindow settingsPage = new SettingsWindow();
            settingsPage.Show();
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
            //to  make codeeeeee.....
        }
    }
}
