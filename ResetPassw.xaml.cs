using System.Windows;
using System.Text.RegularExpressions;

namespace FinalFinanceTrack
{
    public partial class ResetPassw : Window
    {
        private string userEmail;

        public ResetPassw()
        {
            InitializeComponent();
        }

        public ResetPassw(string email) : this()
        {
            userEmail = email;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = NewPasswordTextBox.Text;
            string confirmPassword = ConfirmPasswordTextBox.Text;

            if (!IsValidPassword(newPassword))
            {
                MessageBox.Show("Password must be at least 8 characters long, include at least one uppercase letter, one number, and one special character (.@§$+-*/\\<>).");
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("The passwords do not match. Please re-enter your passwords.");
                return;
            }

            // Update the user password
            DbManager dbManager = new DbManager();
            User user = dbManager.GetUserByEmail(userEmail);
            if (user != null && dbManager.UpdatePassword(user.Id, newPassword))
            {
                MessageBox.Show("Your password has been reset successfully.");

                // Navigate to the login page
                LogIn loginWindow = new LogIn();
                loginWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to reset password. Please try again.");
            }
        }

        private bool IsValidPassword(string password)
        {
            var hasMinimum8Chars = new Regex(@".{8,}");
            var hasUpperCaseLetter = new Regex(@"[A-Z]+");
            var hasNumber = new Regex(@"\d+");
            var hasSpecialCharacter = new Regex(@"[.@§$+\-*/\\<>]");

            return hasMinimum8Chars.IsMatch(password) &&
                   hasUpperCaseLetter.IsMatch(password) &&
                   hasNumber.IsMatch(password) &&
                   hasSpecialCharacter.IsMatch(password);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Handle button click event
        }

        private void NewPasswordTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Handle text changed event
        }
    }
}
