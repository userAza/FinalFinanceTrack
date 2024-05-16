using System.Windows;

namespace FinalFinanceTrack
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the Overview page
            // OverviewWindow overviewWindow = new OverviewWindow();
            // overviewWindow.Show();
            // this.Close();

            MessageBox.Show("Return to Overview page");
        }

        private void UpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            UpdatePasswordWindow updatePasswordWindow = new UpdatePasswordWindow();
            updatePasswordWindow.Show();
            this.Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the Welcome page
            // WelcomeWindow welcomeWindow = new WelcomeWindow();
            // welcomeWindow.Show();
            // this.Close();

            MessageBox.Show("Logout and return to Welcome page");
        }

        private void GeneratePolicyDocument_Click(object sender, RoutedEventArgs e)
        {
            // Generate and display policy document
            // For demonstration purposes, we'll just show a message
            MessageBox.Show("Generating policy document...");
        }
    }
}
