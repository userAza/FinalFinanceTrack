using System;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace FinalFinanceTrack
{
    public partial class AdminResetPassw : Window
    {
        private string adminEmail;
        private DbManager dbManager;

        public AdminResetPassw(string email)
        {
            InitializeComponent();
            adminEmail = email;
            dbManager = new DbManager();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string oldPassword = AdminOldPasswordTextBox.Text.Trim();
            string newPassword = AdminNewPasswordTextBox.Text.Trim();
            string confirmPassword = AdminConfirmPasswordTextBox.Text.Trim();

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

            if (!dbManager.ValidateAdminPassword(adminEmail.Trim(), oldPassword))
            {
                MessageBox.Show("The old password is incorrect. Please try again.");
                return;
            }

            if (dbManager.UpdateAdminPassword(adminEmail.Trim(), newPassword))
            {
                MessageBox.Show("Your password has been reset successfully.");
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

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the AdminsLogIn window
            AdminsLogIn adminsLogIn = new AdminsLogIn();
            adminsLogIn.Show();
            this.Close();
        }
    }
}
