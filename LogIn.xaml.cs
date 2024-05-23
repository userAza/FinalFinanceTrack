using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
                textBox.Text = textBox.Name == "emailTextBox" ? "Email" : "Password";
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string email = emailTextBox.Text;
            string inputPassword = passwordTextBox.Text;

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

        // Method to validate user login in your Login class
        private bool ValidateLogin(string email, string inputPassword)
        {
            DbManager dbManager = new DbManager();
            string storedHash = dbManager.GetHashedPassword(email);

            if (storedHash != null && Security.HashPassword(inputPassword) == storedHash)
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
    }
}
