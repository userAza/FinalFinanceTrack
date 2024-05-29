using System.Windows;
using System.Windows.Controls;

namespace FinalFinanceTrack
{
    public partial class AdminsLogIn : Window
    {
        public AdminsLogIn()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the previous window
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            string email = adminEmailTextBox.Text;
            string password = adminPasswordBox.Password;

            // Implement your login logic, e.g., checking credentials against a database
            int userId = 1; // Replace with actual userId retrieval logic

            // Navigate to MainPage with userId
            MainPage mainPage = new MainPage(userId);
            mainPage.Show();
            this.Close();
        }


        private void RemovePlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Email")
            {
                textBox.Text = "";
                textBox.Foreground = System.Windows.Media.Brushes.Black;
            }

            if (textBox == adminPasswordTextBox && adminPasswordTextBox.Visibility == Visibility.Visible)
            {
                adminPasswordTextBox.Visibility = Visibility.Collapsed;
                adminPasswordBox.Visibility = Visibility.Visible;
                adminPasswordBox.Focus();
            }
        }

        private void AddPlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Email";
                textBox.Foreground = System.Windows.Media.Brushes.Gray;
            }

            if (textBox == adminPasswordTextBox && string.IsNullOrWhiteSpace(adminPasswordBox.Password))
            {
                adminPasswordTextBox.Visibility = Visibility.Visible;
                adminPasswordBox.Visibility = Visibility.Collapsed;
            }
        }

        private void ToggleAdminPasswordVisibility_Click(object sender, RoutedEventArgs e)
        {
            if (adminPasswordBox.Visibility == Visibility.Visible)
            {
                adminPasswordTextBox.Text = adminPasswordBox.Password;
                adminPasswordBox.Visibility = Visibility.Collapsed;
                adminPasswordTextBox.Visibility = Visibility.Visible;
                toggleAdminPasswordVisibilityButton.Content = "🙈";
            }
            else
            {
                adminPasswordBox.Password = adminPasswordTextBox.Text;
                adminPasswordTextBox.Visibility = Visibility.Collapsed;
                adminPasswordBox.Visibility = Visibility.Visible;
                toggleAdminPasswordVisibilityButton.Content = "👁";
            }
        }

        private void AdminPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(adminPasswordBox.Password))
            {
                adminPasswordPlaceholder.Visibility = Visibility.Collapsed;
            }
            else
            {
                adminPasswordPlaceholder.Visibility = Visibility.Visible;
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            adminPasswordPlaceholder.Visibility = Visibility.Collapsed;
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(adminPasswordBox.Password))
            {
                adminPasswordPlaceholder.Visibility = Visibility.Visible;
            }
        }
    }
}
