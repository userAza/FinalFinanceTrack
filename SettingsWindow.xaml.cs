using System.Windows;

namespace FinalFinanceTrack
{
    public partial class SettingsWindow : Window
    {
        private int? userId;

        public SettingsWindow(int? userId = null)
        {
            InitializeComponent();
            this.userId = userId;

            // Use the userId as needed, check for null if necessary
            if (userId.HasValue)
            {
                // Handle operations that require userId
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Overview overviewPage = new Overview();
            overviewPage.Show();
            this.Close();
        }

        private void UpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            UpdatePasswordWindow updatePasswordWindow = new UpdatePasswordWindow(); // No userId needed
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
            if (userId.HasValue)
            {
                AddProfilePic addProfilePicWindow = new AddProfilePic(userId.Value, "SettingsWindow");
                addProfilePicWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("User ID is required for this operation.");
            }
        }

    }
}
