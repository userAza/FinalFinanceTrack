using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace FinTrackWpf
{
    public class PasswordResetRequest
    {
        public int RequestID { get; set; }
        public string Email { get; set; }
        public DateTime RequestTime { get; set; }
        public string Status { get; set; }
    }

    public partial class AdminPasswordRequest : Window
    {
        public ObservableCollection<PasswordResetRequest> Requests { get; set; }

        public AdminPasswordRequest()
        {
            InitializeComponent();
            LoadRequests();
        }


        private void LoadRequests()
        {
            var newRequests = new ObservableCollection<PasswordResetRequest>();

            string connectionString = "server=127.0.0.1;user=root;database=budget;password=";
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var cmd = new MySqlCommand("SELECT RequestID, Email, RequestTime, Status FROM password_reset_requests", connection);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("No data found in password_reset_requests.");
                            return;
                        }

                        while (reader.Read())
                        {
                            var req = new PasswordResetRequest
                            {
                                RequestID = reader.GetInt32("RequestID"),
                                Email = reader["Email"].ToString(), // Using ToString() for safety
                                RequestTime = reader.GetDateTime("RequestTime"),
                                Status = reader.GetString("Status")
                            };
                            newRequests.Add(req);
                            MessageBox.Show("Loaded: " + req.Email); // Debug message for each loaded entry
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }

            Application.Current.Dispatcher.Invoke(() => {
                Requests = newRequests;
                RequestsDataGrid.ItemsSource = Requests;
                RequestsDataGrid.Items.Refresh(); // Force refresh to ensure UI updates
            });
        }







        private void ApproveRequest_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = RequestsDataGrid.SelectedItem as PasswordResetRequest;
            if (selectedRequest != null)
            {
                UpdateRequestStatus(selectedRequest.RequestID, "Completed");
                MessageBox.Show("Request Approved. User password is reset.");
            }
        }

        private void DenyRequest_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = RequestsDataGrid.SelectedItem as PasswordResetRequest;
            if (selectedRequest != null)
            {
                UpdateRequestStatus(selectedRequest.RequestID, "Denied");
                MessageBox.Show("Request Denied.");
            }
        }

        private void UpdateRequestStatus(int requestId, string newStatus)
        {
            string connectionString = "server=127.0.0.1;user=root;database=budget;password=";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var cmd = new MySqlCommand("UPDATE password_reset_requests SET Status = @Status WHERE RequestID = @RequestID", connection);
                cmd.Parameters.AddWithValue("@Status", newStatus);
                cmd.Parameters.AddWithValue("@RequestID", requestId);
                cmd.ExecuteNonQuery();
            }
            LoadRequests(); // Refresh the Requests list to reflect the change

        }


        private void ResetUserPassword(string email)
        {
            string newPassword = GenerateNewPassword();  // This method generates a new, secure password
            string connectionString = "server=127.0.0.1;user=root;database=budget;password=";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var cmd = new MySqlCommand("UPDATE users SET Password = @Password WHERE Email = @Email", connection);
                cmd.Parameters.AddWithValue("@Password", newPassword);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.ExecuteNonQuery();
            }
            NotifyUserOfNewPassword(email, newPassword);  // Call this method to handle in-app notification
        }

        private string GenerateNewPassword()
        {
            // Generate a new password, which could be a random string
            // For simplicity, let's use a fixed example (not secure)
            return "newPassword123";  // You should implement a better random password generator
        }

        private void NotifyUserOfNewPassword(string email, string newPassword)
        {
            // Assuming you have a 'notifications' table where you can store messages for users
            string connectionString = "server=127.0.0.1;user=root;database=budget;password=";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var cmd = new MySqlCommand("INSERT INTO notifications (Email, Message) VALUES (@Email, @Message)", connection);
                string message = "Your new password is: " + newPassword + ". Please change it upon login.";
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Message", message);
                cmd.ExecuteNonQuery();
            }
        }


    }
}
