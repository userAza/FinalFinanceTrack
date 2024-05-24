using System;
using MySql.Data.MySqlClient;
using System.Windows;

namespace FinalFinanceTrack
{
    public partial class UpdatePasswordWindow : Window
    {
        private string connectionString = "server=127.0.0.1;user=root;database=budget;Password"; // Replace with your actual connection string
        private int userId=1; 

        public UpdatePasswordWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string oldPassword = oldPasswordBox.Password;
            string newPassword = newPasswordBox.Password;
            string confirmPassword = confirmPasswordBox.Password;

            if (newPassword == confirmPassword)
            {
                if (ValidateOldPassword(oldPassword))
                {
                    string hashedNewPassword = Security.HashPassword(newPassword);
                    if (UpdatePasswordInDatabase(hashedNewPassword))
                    {
                        MessageBox.Show("New password saved.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to save the new password.");
                    }
                }
                else
                {
                    MessageBox.Show("Old password is incorrect.");
                }
            }
            else
            {
                MessageBox.Show("New password and confirmation do not match.");
            }
        }

        private bool ValidateOldPassword(string oldPassword)
        {
            string hashedOldPassword = Security.HashPassword(oldPassword);
            string query = "SELECT COUNT(*) FROM Users WHERE UserId = @UserId AND Password = @Password"; // Adjust query to your schema

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@Password", hashedOldPassword);

                connection.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }

        private bool UpdatePasswordInDatabase(string newPassword)
        {
            string query = "UPDATE Users SET Password = @Password WHERE UserId = @UserId"; 

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Password", newPassword);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BackToSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
            this.Close();
        }
    }
}
