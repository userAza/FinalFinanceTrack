using System;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace FinalFinanceTrack
{
    public partial class AdminResetPassw : Window
    {
        private string adminEmail;

        public AdminResetPassw(string email)
        {
            InitializeComponent();
            adminEmail = email;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = AdminNewPasswordTextBox.Text;
            string confirmPassword = AdminConfirmPasswordTextBox.Text;

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

            // Update the admin password
            if (UpdateAdminPassword(adminEmail, newPassword))
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

        private bool UpdateAdminPassword(string email, string newPassword)
        {
            try
            {
                string connectionString = "server=127.0.0.1;user=root;database=budget;password=";
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    var query = "UPDATE admin SET Password = @Password WHERE Email = @Email";
                    var cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Password", newPassword);
                    cmd.Parameters.AddWithValue("@Email", email);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update password: {ex.Message}");
                return false;
            }
        }
    }
}
