using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FinalFinanceTrack
{
    public partial class AdminsLogIn : Window
    {
        private DbManager dbManager;

        public AdminsLogIn()
        {
            InitializeComponent();
            dbManager = new DbManager();
            UpdatePasswordPlaceholder();
        }

        private void RemovePlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Foreground == Brushes.Gray)
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void AddPlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox.Name.Contains("Email") ? "Email" : "Password";
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void ToggleAdminPasswordVisibility_Click(object sender, RoutedEventArgs e)
        {
            if (adminPasswordBox.Visibility == Visibility.Visible)
            {
                adminPasswordBox.Visibility = Visibility.Collapsed;
                adminPasswordTextBox.Text = adminPasswordBox.Password;
                adminPasswordTextBox.Visibility = Visibility.Visible;
                toggleAdminPasswordVisibilityButton.Content = "👁";
                UpdatePasswordPlaceholder();
            }
            else
            {
                adminPasswordTextBox.Visibility = Visibility.Collapsed;
                adminPasswordBox.Password = adminPasswordTextBox.Text;
                adminPasswordBox.Visibility = Visibility.Visible;
                toggleAdminPasswordVisibilityButton.Content = "👁️";
                UpdatePasswordPlaceholder();
            }
        }

        private void AdminPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            UpdatePasswordPlaceholder();
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            adminPasswordPlaceholder.Visibility = Visibility.Collapsed;
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdatePasswordPlaceholder();
        }

        private void UpdatePasswordPlaceholder()
        {
            if (string.IsNullOrEmpty(adminPasswordBox.Password) && adminPasswordBox.Visibility == Visibility.Visible)
            {
                adminPasswordPlaceholder.Visibility = Visibility.Visible;
            }
            else
            {
                adminPasswordPlaceholder.Visibility = Visibility.Collapsed;
            }
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            string email = adminEmailTextBox.Text;
            string password = adminPasswordBox.Visibility == Visibility.Visible ? adminPasswordBox.Password : adminPasswordTextBox.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("No empty fields allowed");
                return;
            }

            string storedPassword = dbManager.GetAdminPassword(email);
            if (storedPassword != null && password == storedPassword)
            {
                MainPage mainPage = new MainPage();
                mainPage.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Login");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ForgotPasswordHyperlink_Click(object sender, RoutedEventArgs e)
        {
            ForgotPasswordAdmin forgotPasswordAdminWindow = new ForgotPasswordAdmin();
            forgotPasswordAdminWindow.Show();
        }
    }
}
