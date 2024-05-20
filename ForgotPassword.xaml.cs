using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace FinalFinanceTrack
{
    public partial class ForgotPassword : Window
    {
        public ForgotPassword()
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
            string userEmail = emailTextBox.Text.Trim();
            if (!IsValidEmail(userEmail))
            {
                MessageBox.Show("Please enter a valid email address.");
                return;
            }

            if (string.IsNullOrWhiteSpace(securityQuestion1TextBox.Text) ||
                string.IsNullOrWhiteSpace(securityQuestion2TextBox.Text) ||
                string.IsNullOrWhiteSpace(securityQuestion3TextBox.Text))
            {
                MessageBox.Show("Please answer all security questions.");
                return;
            }

            // Get the correct answers from the database
            var (correctAnswer1, correctAnswer2, correctAnswer3) = GetStoredAnswers(userEmail);

            // Check if the answers match
            if (securityQuestion1TextBox.Text != correctAnswer1 ||
                securityQuestion2TextBox.Text != correctAnswer2 ||
                securityQuestion3TextBox.Text != correctAnswer3)
            {
                MessageBox.Show("One or more answers are incorrect. Please try again.");
                return;
            }

            // If answers are correct, proceed with password reset process
            InsertPasswordResetRequest(userEmail);  // Insert the password reset request
            MessageBox.Show("Identity verified. You may proceed to reset your password.");
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



        // Event handler for the Back button click
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            // Close the ForgotPassword window
            this.Close();
        }


        private (string, string, string) GetStoredAnswers(string email)
        {
            string connectionString = "server=127.0.0.1;user=root;database=budget;password=";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Answer1, Answer2, Answer3 FROM user WHERE Email = @Email LIMIT 1";
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