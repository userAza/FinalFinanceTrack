using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace FinalFinanceTrack
{
    public partial class PolicyDocument : Window
    {
        public PolicyDocument()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
            this.Close();
        }

        private void TermsHyperlink_Click(object sender, RoutedEventArgs e)
        {
            termsPopup.IsOpen = true;
        }

        private void PrivacyHyperlink_Click(object sender, RoutedEventArgs e)
        {
            privacyPopup.IsOpen = true;
        }

        private void ClosePopup_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var popup = button.Tag as Popup;
                if (popup != null)
                {
                    popup.IsOpen = false;
                }
            }
        }
    }
}
