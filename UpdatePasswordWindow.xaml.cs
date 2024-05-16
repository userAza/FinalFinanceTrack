using System.Windows;

namespace FinalFinanceTrack
{
    public partial class UpdatePasswordWindow : Window
    {
        public UpdatePasswordWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string oldPassword = oldPasswordBox.Password;
            string newPassword = newPasswordBox.Password;
            string confirmPassword = confirmPasswordBox.Password;

            if (newPassword == confirmPassword)
            {
                // Assuming password validation and updating logic is implemented
                MessageBox.Show("New password saved or created.");
                // Code to save the new password goes here
            }
            else
            {
                MessageBox.Show("New password and confirmation do not match.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BackToSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow(); // Assuming you have a SettingsWindow class
            settingsWindow.Show();
            this.Close();
        }
    }
}
