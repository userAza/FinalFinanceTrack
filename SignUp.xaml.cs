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
            UpdatePasswordPlaceholder();
            UpdateConfirmPasswordPlaceholder();
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
            if (textBox != null && string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = textBox.Tag?.ToString() ?? "";
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void LetsGo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(firstNameTextBox.Text.Trim()) || firstNameTextBox.Text.Trim() == "First Name")
            {
                MessageBox.Show("First name is required.");
                return;
            }

            if (string.IsNullOrWhiteSpace(lastNameTextBox.Text.Trim()) || lastNameTextBox.Text.Trim() == "Last Name")
            {
                MessageBox.Show("Last name is required.");
                return;
            }

            if (string.IsNullOrWhiteSpace(emailTextBox.Text.Trim()) || emailTextBox.Text.Trim() == "Email must be ending with @fintrack.com")
            {
                MessageBox.Show("Email is required.");
                return;
            }

            if (!Regex.IsMatch(emailTextBox.Text.Trim(), @"^[^@\s]+@fintrack\.com$"))
            {
                MessageBox.Show("Please enter a valid Fintrack email address ending with @fintrack.com.");
                return;
            }

            string password = passwordBox.Visibility == Visibility.Visible ? passwordBox.Password : passwordTextBox.Text;

            if (string.IsNullOrWhiteSpace(password) || password == "Password")
            {
                MessageBox.Show("Password is required.");
                return;
            }

            if (!IsValidPassword(password))
            {
                MessageBox.Show("Password must be at least 8 characters long, include at least one uppercase letter, one special character (.@§$+-*/\\<>), and one number (0-9).");
                return;
            }

            string confirmPassword = confirmPasswordBox.Visibility == Visibility.Visible ? confirmPasswordBox.Password : confirmPasswordTextBox.Text;

            if (string.IsNullOrWhiteSpace(confirmPassword) || confirmPassword == "Confirm Password")
            {
                MessageBox.Show("Confirm password is required.");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("The passwords do not match. Please re-enter your passwords.");
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

            // Assuming profilePicture is fetched from a file picker or another source as byte array
            byte[] profilePicture = GetProfilePicture();

            DbManager dbManager = new DbManager();
            if (dbManager.InsertUser(
                firstNameTextBox.Text.Trim(),
                lastNameTextBox.Text.Trim(),
                emailTextBox.Text.Trim(),
                password,
                profilePicture))
            {
                MessageBox.Show("Signup successful! Log in on the login page.");

                // Navigate to the LogIn window
                LogIn loginWindow = new LogIn();
                loginWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Signup failed. Please try again.");
            }
        }

        private byte[] GetProfilePicture()
        {
            // Implement the logic to fetch the profile picture as a byte array.
            // This could involve opening a file dialog, reading the selected file, and converting it to a byte array.
            // For the sake of example, let's assume we return a dummy byte array.
            return new byte[0]; // Replace with actual implementation
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

        private void Back_Click(object sender, RoutedEventArgs e)
        {
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
        }

        private void TermsAndConditions_Click(object sender, RoutedEventArgs e)
        {
        }

        private void termsAndConditionsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void TermsAndConditionsLink_Click(object sender, RoutedEventArgs e)
        {
            termsPopup.IsOpen = true;
        }

        private void PrivacyPolicyLink_Click(object sender, RoutedEventArgs e)
        {
            privacyPopup.IsOpen = true;
        }

        private void emailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!string.IsNullOrWhiteSpace(textBox.Text) && !Regex.IsMatch(textBox.Text.Trim(), @"^[^@\s]+@fintrack\.com$"))
            {
                textBox.Foreground = Brushes.Red;
            }
            else
            {
                textBox.Foreground = Brushes.Black;
            }
        }

        private void ExplanationLink_Click(object sender, RoutedEventArgs e)
        {
            explanationPopup.IsOpen = true;
        }

        private void ClosePopup_Click(object sender, RoutedEventArgs e)
        {
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

        private void TogglePasswordVisibility_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Visibility == Visibility.Visible)
            {
                passwordBox.Visibility = Visibility.Collapsed;
                passwordTextBox.Text = passwordBox.Password;
                passwordTextBox.Visibility = Visibility.Visible;
                togglePasswordVisibilityButton.Content = "👁";
                UpdatePasswordPlaceholder();
            }
            else
            {
                passwordTextBox.Visibility = Visibility.Collapsed;
                passwordBox.Password = passwordTextBox.Text;
                passwordBox.Visibility = Visibility.Visible;
                togglePasswordVisibilityButton.Content = "👁️";
                UpdatePasswordPlaceholder();
            }
        }

        private void ToggleConfirmPasswordVisibility_Click(object sender, RoutedEventArgs e)
        {
            if (confirmPasswordBox.Visibility == Visibility.Visible)
            {
                confirmPasswordBox.Visibility = Visibility.Collapsed;
                confirmPasswordTextBox.Text = confirmPasswordBox.Password;
                confirmPasswordTextBox.Visibility = Visibility.Visible;
                toggleConfirmPasswordVisibilityButton.Content = "👁";
                UpdateConfirmPasswordPlaceholder();
            }
            else
            {
                confirmPasswordTextBox.Visibility = Visibility.Collapsed;
                confirmPasswordBox.Password = confirmPasswordTextBox.Text;
                confirmPasswordBox.Visibility = Visibility.Visible;
                toggleConfirmPasswordVisibilityButton.Content = "👁️";
                UpdateConfirmPasswordPlaceholder();
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            UpdatePasswordPlaceholder();
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordPlaceholder.Visibility = Visibility.Collapsed;
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdatePasswordPlaceholder();
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            UpdateConfirmPasswordPlaceholder();
        }

        private void ConfirmPasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            confirmPasswordPlaceholder.Visibility = Visibility.Collapsed;
        }

        private void ConfirmPasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateConfirmPasswordPlaceholder();
        }

        private void UpdatePasswordPlaceholder()
        {
            if (string.IsNullOrEmpty(passwordBox.Password) && passwordBox.Visibility == Visibility.Visible)
            {
                passwordPlaceholder.Visibility = Visibility.Visible;
            }
            else
            {
                passwordPlaceholder.Visibility = Visibility.Collapsed;
            }
        }

        private void UpdateConfirmPasswordPlaceholder()
        {
            if (string.IsNullOrEmpty(confirmPasswordBox.Password) && confirmPasswordBox.Visibility == Visibility.Visible)
            {
                confirmPasswordPlaceholder.Visibility = Visibility.Visible;
            }
            else
            {
                confirmPasswordPlaceholder.Visibility = Visibility.Collapsed;
            }
        }
    }
}
