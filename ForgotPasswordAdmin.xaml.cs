using System;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;

namespace FinalFinanceTrack
{
    public partial class ForgotPasswordAdmin : Window
    {
        public ForgotPasswordAdmin()
        {
            InitializeComponent();
        }

        // Event handler to remove placeholder text when the text box receives focus
        private void RemovePlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Foreground == Brushes.Gray)
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        // Event handler to add placeholder text when the text box loses focus and is empty
        private void AddPlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Email Address";
                textBox.Foreground = Brushes.Gray;
            }
        }

        // Validates the email format
        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string adminEmail = adminEmailTextBox.Text.Trim();
            if (!IsValidEmail(adminEmail))
            {
                MessageBox.Show("Please enter a valid email address.");
                return;
            }

            if (string.IsNullOrWhiteSpace(adminSecurityQuestion1TextBox.Text) ||
                string.IsNullOrWhiteSpace(adminSecurityQuestion2TextBox.Text) ||
                string.IsNullOrWhiteSpace(adminSecurityQuestion3TextBox.Text))
            {
                MessageBox.Show("Please answer all security questions.");
                return;
            }

            var (correctAnswer1, correctAnswer2, correctAnswer3) = GetStoredAnswers(adminEmail);
            if (adminSecurityQuestion1TextBox.Text != correctAnswer1 ||
                adminSecurityQuestion2TextBox.Text != correctAnswer2 ||
                adminSecurityQuestion3TextBox.Text != correctAnswer3)
            {
                MessageBox.Show("One or more answers are incorrect. Please try again.");
                return;
            }

            InsertPasswordResetRequest(adminEmail);

            // Identity verification successful, open the AdminResetPassw window with the admin email
            AdminResetPassw adminResetPasswWindow = new AdminResetPassw(adminEmail);
            adminResetPasswWindow.ShowDialog(); // Use ShowDialog to make it modal

            // Close the current ForgotPasswordAdmin window after opening AdminResetPassw
            this.Close();
        }

        private void InsertPasswordResetRequest(string email)
        {
            try
            {
                string connectionString = "server=127.0.0.1;user=root;database=budget;password=";
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    var query = "INSERT INTO password_reset_requests (Email) VALUES (@Email)";
                    var cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to insert reset request: {ex.Message}");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            // Close the ForgotPasswordAdmin window
            this.Close();
        }

        private (string, string, string) GetStoredAnswers(string email)
        {
            string connectionString = "server=127.0.0.1;user=root;database=budget;password=";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Answer1, Answer2, Answer3 FROM admin WHERE Email = @Email LIMIT 1";
                var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Email", email);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string answer1 = reader["Answer1"] as string;
                        string answer2 = reader["Answer2"] as string;
                        string answer3 = reader["Answer3"] as string;
                        return (answer1, answer2, answer3);
                    }
                }
            }
            return (null, null, null); // Return null if no data found
        }
    }
}
