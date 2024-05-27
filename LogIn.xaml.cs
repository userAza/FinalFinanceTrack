using System.Windows;
using System.Windows.Controls;

namespace FinalFinanceTrack
{
    public partial class LogIn : Window
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void RemovePlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Foreground == System.Windows.Media.Brushes.Gray)
            {
                textBox.Text = "";
                textBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void AddPlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox.Name == "emailTextBox" ? "Email" : "Password";
                textBox.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private void TogglePasswordVisibility_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Visibility == Visibility.Visible)
            {
                passwordTextBox.Text = passwordBox.Password;
                passwordBox.Visibility = Visibility.Collapsed;
                passwordTextBox.Visibility = Visibility.Visible;
                togglePasswordVisibilityButton.Content = "👁";
            }
            else
            {
                passwordBox.Password = passwordTextBox.Text;
                passwordBox.Visibility = Visibility.Visible;
                passwordTextBox.Visibility = Visibility.Collapsed;
                togglePasswordVisibilityButton.Content = "👁️";
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string email = emailTextBox.Text;
            string inputPassword = passwordBox.Visibility == Visibility.Visible ? passwordBox.Password : passwordTextBox.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(inputPassword))
            {
                MessageBox.Show("No empty fields allowed");
                return;
            }

            if (ValidateLogin(email, inputPassword))
            {
                Overview overviewWindow = new Overview();
                overviewWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Login");
            }
        }

        private bool ValidateLogin(string email, string inputPassword)
        {
            DbManager dbManager = new DbManager();
            string storedPassword = dbManager.GetPassword(email);

            if (storedPassword != null && inputPassword == storedPassword)
            {
                return true;
            }
            return false;
        }

        private void ForgotPasswordHyperlink_Click(object sender, RoutedEventArgs e)
        {
            ForgotPassword forgotPasswordWindow = new ForgotPassword();
            forgotPasswordWindow.Show();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void SignUpHyperlink_Click(object sender, RoutedEventArgs e)
        {
            SignUp signUpWindow = new SignUp();
            signUpWindow.Show();
            this.Close();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Visibility == Visibility.Visible)
            {
                passwordTextBox.Text = passwordBox.Password;
            }
        }
    }
}
