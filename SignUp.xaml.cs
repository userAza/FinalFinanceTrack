using MySql.Data.MySqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;


namespace FinalFinanceTrack
{
    public partial class SignUp : Window
    {
        private string connectionString = "server=127.0.0.1;database=budget;user=root;password=;";

        public SignUp()
        {
            InitializeComponent();
        }

        private void RemovePlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Foreground == Brushes.Gray && textBox.Text == (textBox.Tag?.ToString() ?? ""))
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
                textBox.Text = textBox.Tag?.ToString() ?? "Default Text";
                textBox.Foreground = Brushes.Gray;
            }
        }




        // Event handler for Let's Go button click



        private void LetsGo_Click(object sender, RoutedEventArgs e)
        {
            // Use Trim to ensure that fields are not only whitespace
            if (string.IsNullOrWhiteSpace(firstNameTextBox.Text.Trim()))
            {
                MessageBox.Show("First name is required.");
                return;
            }
            if (string.IsNullOrWhiteSpace(lastNameTextBox.Text.Trim()))
            {
                MessageBox.Show("Last name is required.");
                return;
            }
            if (string.IsNullOrWhiteSpace(emailTextBox.Text.Trim()))
            {
                MessageBox.Show("Email is required.");
                return;
            }

            // Validate email format specifically for FinTrack domain
            if (!Regex.IsMatch(emailTextBox.Text.Trim(), @"^[^@\s]+@fintrack\.com$"))
            {
                MessageBox.Show("Please enter a valid Fintrack email address ending with @fintrack.com.");
                return;
            }

            if (string.IsNullOrWhiteSpace(passwordTextBox.Text.Trim()))
            {
                MessageBox.Show("Password is required.");
                return;
            }
            if (string.IsNullOrWhiteSpace(confirmPasswordTextBox.Text.Trim()))
            {
                MessageBox.Show("Confirm password is required.");
                return;
            }
            if (string.IsNullOrWhiteSpace(securityQuestion1TextBox.Text.Trim()))
            {
                MessageBox.Show("Security Question 1 is required.");
                return;
            }
            if (string.IsNullOrWhiteSpace(securityQuestion2TextBox.Text.Trim()))
            {
                MessageBox.Show("Security Question 2 is required.");
                return;
            }
            if (string.IsNullOrWhiteSpace(securityQuestion3TextBox.Text.Trim()))
            {
                MessageBox.Show("Security Question 3 is required.");
                return;
            }

            if (!termsAndConditionsCheckBox.IsChecked ?? false)
            {
                MessageBox.Show("Please agree to the Terms and Conditions and the Privacy Policy.");
                return;
            }

            if (passwordTextBox.Text != confirmPasswordTextBox.Text)
            {
                MessageBox.Show("The passwords do not match. Please re-enter your passwords.");
                return;
            }

            bool success = InsertUser(
                firstNameTextBox.Text.Trim(),
                lastNameTextBox.Text.Trim(),
                emailTextBox.Text.Trim(),
                passwordTextBox.Text,
                securityQuestion1TextBox.Text.Trim(),
                securityQuestion2TextBox.Text.Trim(),
                securityQuestion3TextBox.Text.Trim()
            );

            if (success)
            {
                MessageBox.Show("Signup successful! Log in on the login page.");
                LogIn loginWindow = new LogIn();
                loginWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Signup failed. Please try again.");
            }
        }






        // Event handler for the Back button click
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            // Logic to navigate back to the MainWindow
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void LoginHyperlink_Click(object sender, RoutedEventArgs e)
        {
            LogIn loginWindow = new LogIn();
            loginWindow.Show();
            this.Close();
        }

        private void PrivacyPolicy_Click(object sender, RoutedEventArgs e)
        {
            // Show Privacy Policy content
        }

        private void TermsAndConditions_Click(object sender, RoutedEventArgs e)
        {
            // Show Terms and Conditions content
        }

        private void termsAndConditionsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // This method can be empty
        }
        private void TermsAndConditionsLink_Click(object sender, RoutedEventArgs e)
        {
            termsPopup.IsOpen = true; // Open the Terms and Conditions pop-up
        }

        private void PrivacyPolicyLink_Click(object sender, RoutedEventArgs e) // Renamed the method
        {
            privacyPopup.IsOpen = true; // Open the Privacy Policy pop-up
        }

        private void ClosePopup_Click(object sender, RoutedEventArgs e)
        {
            // Close the pop-up when the X button is clicked
            Button closeButton = sender as Button;
            if (closeButton != null)
            {
                Popup popup = closeButton.Tag as Popup;
                if (popup != null)
                {
                    popup.IsOpen = false;
                }
            }
        }

        private void ExplanationLink_Click(object sender, RoutedEventArgs e)
        {
            explanationPopup.IsOpen = true; // Open the explanation pop-up
        }

        private bool InsertUser(string firstName, string lastName, string email, string password, string answer1, string answer2, string answer3)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(answer1) || string.IsNullOrWhiteSpace(answer2) ||
                string.IsNullOrWhiteSpace(answer3))
            {
                MessageBox.Show("Please fill in all fields.");
                return false;
            }

            // Validate email format
            if (!Regex.IsMatch(email, @"\A[^@\s]+@[^@\s]+\.[^@\s]+\z"))
            {
                MessageBox.Show("Please enter a valid email address.");
                return false;
            }

            string connectionString = "server=127.0.0.1;user=root;database=budget;password=";
            MySqlConnection dbConnection = new MySqlConnection(connectionString);

            try
            {
                dbConnection.Open();

                // Get the next available ID
                int nextID = GetNextAvailableID();

                // Construct the INSERT query with the explicit ID value
                string query = "INSERT INTO user (ID, First_Name, Last_Name, Email, Password, Answer1, Answer2, Answer3) VALUES (@ID, @FirstName, @LastName, @Email, @Password, @Answer1, @Answer2, @Answer3)";
                MySqlCommand cmd = new MySqlCommand(query, dbConnection);
                cmd.Parameters.AddWithValue("@ID", nextID);  // Provide the explicit ID value
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);  // For simplicity, storing the plaintext password here.
                cmd.Parameters.AddWithValue("@Answer1", answer1);
                cmd.Parameters.AddWithValue("@Answer2", answer2);
                cmd.Parameters.AddWithValue("@Answer3", answer3);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during sign up: " + ex.Message);
                return false;
            }
            finally
            {
                if (dbConnection.State != ConnectionState.Closed)
                    dbConnection.Close();
            }
        }

        private int GetNextAvailableID()
        {
            string connectionString = "server=127.0.0.1;user=root;database=budget;password=";
            MySqlConnection dbConnection = new MySqlConnection(connectionString);

            try
            {
                dbConnection.Open();

                // Execute the query to get the maximum existing ID
                string query = "SELECT MAX(ID) FROM user";
                MySqlCommand cmd = new MySqlCommand(query, dbConnection);
                int maxID = Convert.ToInt32(cmd.ExecuteScalar());

                // Increment the maximum ID by 1 to get the next available ID
                int nextID = maxID + 1;
                return nextID;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting next available ID: " + ex.Message);
                return -1;
            }
            finally
            {
                if (dbConnection.State != ConnectionState.Closed)
                    dbConnection.Close();
            }
        }

        private void emailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

