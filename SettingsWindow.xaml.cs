using System.Windows;

namespace FinalFinanceTrack
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Overview overviewPage = new Overview();
            overviewPage.Show();
            this.Close();
        }

        private void UpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            UpdatePasswordWindow updatePasswordWindow = new UpdatePasswordWindow();
            updatePasswordWindow.Show();
            this.Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void PolicyDocument_Click(object sender, RoutedEventArgs e)
        {
            PolicyDocument policyDocumentWindow = new PolicyDocument();
            policyDocumentWindow.Show();
            this.Close();
        }

        private void AddProfilePicture_Click(object sender, RoutedEventArgs e)
        {
            AddProfilePic addProfilePicWindow = new AddProfilePic();
            addProfilePicWindow.Show();
            this.Close();
        }
    }
}
