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

        public ResetPassw(string email) : this() // Call the default constructor to initialize components
        {
            userEmail = email; // Store email for later use (e.g., updating password in the database)
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

            // Assuming you have a method to update the password in your database
            UpdateUserPassword(userEmail, newPassword);
            MessageBox.Show("Your password has been reset successfully.");
            this.Close();
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

        private void UpdateUserPassword(string email, string newPassword)
        {
            // This method should update the password for the user in your database
            // Example implementation needed
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewPasswordTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
