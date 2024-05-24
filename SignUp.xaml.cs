using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace FinalFinanceTrack
{
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void RemovePlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Foreground == Brushes.Gray)
            {
                textBox.Text = ""; // Clear the placeholder text
                textBox.Foreground = Brushes.Black; // Set text color to default (user input color)
            }
        }

        private void AddPlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = textBox.Tag?.ToString() ?? ""; // Restore the placeholder text
                textBox.Foreground = Brushes.Gray; // Set text color to indicate placeholder
            }
        }

        private void LetsGo_Click(object sender, RoutedEventArgs e)
        {
            // Check if first name is just a placeholder or is empty
            if (string.IsNullOrWhiteSpace(firstNameTextBox.Text.Trim()) || firstNameTextBox.Text.Trim() == "First Name")
            {
                MessageBox.Show("First name is required.");
                return;
            }

            // Check if last name is just a placeholder or is empty
            if (string.IsNullOrWhiteSpace(lastNameTextBox.Text.Trim()) || lastNameTextBox.Text.Trim() == "Last Name")
            {
                MessageBox.Show("Last name is required.");
                return;
            }
            if (string.IsNullOrWhiteSpace(emailTextBox.Text.Trim()))
            {
                MessageBox.Show("Email is required.");
                return;
            }

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
            if (!IsValidPassword(passwordTextBox.Text))
            {
                MessageBox.Show("Password must be at least 8 characters long, include at least one uppercase letter, one special character (.@§$+-*/\\<>), and one number (0-9).");
                return;
            }
            if (string.IsNullOrWhiteSpace(confirmPasswordTextBox.Text.Trim()))
            {
                MessageBox.Show("Confirm password is required.");
                return;
            }
            if (passwordTextBox.Text != confirmPasswordTextBox.Text)
            {
                MessageBox.Show("The passwords do not match. Please re-enter your passwords.");
                return;
            }

            if (!termsAndConditionsCheckBox.IsChecked ?? false)
            {
                MessageBox.Show("Please agree to the Terms and Conditions and the Privacy Policy.");
                return;
            }

            DbManager dbManager = new DbManager();
            if (dbManager.InsertUser(
                firstNameTextBox.Text.Trim(),
                lastNameTextBox.Text.Trim(),
                emailTextBox.Text.Trim(),
                passwordTextBox.Text.Trim(),  // Use the password directly
                securityQuestion1TextBox.Text.Trim(),
                securityQuestion2TextBox.Text.Trim(),
                securityQuestion3TextBox.Text.Trim()))
            {
                MessageBox.Show("Signup successful! Log in on the login page.");
                this.Close(); // Optionally close the sign-up window
            }
            else
            {
                MessageBox.Show("Signup failed. Please try again.");
            }
        }

        private bool IsValidPassword(string password)
        {
            var hasMinimum8Chars = new Regex(@".{8,}");
            var hasUpperCaseLetter = new Regex(@"[A-Z]+");
            var hasSpecialCharacter = new Regex(@"[.@§$+\-*/\\<>]");
            var hasNumber = new Regex(@"\d+");

            return hasMinimum8Chars.IsMatch(password) &&
                   hasUpperCaseLetter.IsMatch(password) &&
                   hasSpecialCharacter.IsMatch(password) &&
                   hasNumber.IsMatch(password);
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

 

        private void emailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!string.IsNullOrWhiteSpace(textBox.Text) && !Regex.IsMatch(textBox.Text.Trim(), @"^[^@\s]+@fintrack\.com$"))
            {
                textBox.Foreground = Brushes.Red; // Change text color to red if email format is incorrect
            }
            else
            {
                textBox.Foreground = Brushes.Black; // Reset text color to black if email format is corrected
            }
        }
        // Method to open the explanation popup when the hyperlink is clicked
        private void ExplanationLink_Click(object sender, RoutedEventArgs e)
        {
            explanationPopup.IsOpen = true;
        }

        // Method to close the explanation popup when the "Close" button is clicked

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



    }
}
