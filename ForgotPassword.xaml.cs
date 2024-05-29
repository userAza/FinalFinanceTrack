using MySql.Data.MySqlClient;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FinalFinanceTrack
{
    public partial class ForgotPassword : Window
    {
        public ForgotPassword()
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
                textBox.Text = "Email Address";
                textBox.Foreground = Brushes.Gray;
            }
        }

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

            var (correctAnswer1, correctAnswer2, correctAnswer3) = GetStoredAnswers(userEmail);
            if (securityQuestion1TextBox.Text != correctAnswer1 ||
                securityQuestion2TextBox.Text != correctAnswer2 ||
                securityQuestion3TextBox.Text != correctAnswer3)
            {
                MessageBox.Show("One or more answers are incorrect. Please try again.");
                return;
            }

            // Identity verification successful, open the ResetPassw window with the user email
            ResetPassw resetPasswWindow = new ResetPassw(userEmail);
            resetPasswWindow.ShowDialog(); // Use ShowDialog to make it modal

            // Close the current ForgotPassword window after opening ResetPassw
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

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
