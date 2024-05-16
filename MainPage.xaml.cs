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
            // Implement the functionality for Reports button
        }

        private void UserManagementButton_Click(object sender, RoutedEventArgs e)
        {
            UserMan userMan = new UserMan();
            userMan.Show();
            this.Hide();
        }

        private void ClickMeButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement the functionality for Click Me button
        }

        private void SettingsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // Implement the functionality for Settings radio button
        }
    }
}
