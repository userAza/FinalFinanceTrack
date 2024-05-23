using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;

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

        private void GeneratePolicyDocument_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Generating policy document...");
        }

        private void TermsAndConditionsLink_Click(object sender, RoutedEventArgs e)
        {
            termsPopup.IsOpen = true;
        }

        private void PrivacyPolicyLink_Click(object sender, RoutedEventArgs e)
        {
            privacyPopup.IsOpen = true;
        }

        private void ClosePopup_Click(object sender, RoutedEventArgs e)
        {
            Button closeButton = sender as Button;
            if (closeButton != null)
            {
                Popup popup = closeButton.Tag as Popup;
                if (popup != null)
                {
                    popup.IsOpen = false;
                }
            }
        }
    }
}
