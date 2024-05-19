using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FinalFinanceTrack
{
    public partial class AdminsLogIn : Window
    {
        public AdminsLogIn()
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

        private void AddPlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox.Name.Contains("Email") ? "Email" : "Password";
                textBox.Foreground = Brushes.Gray;
            }
        }


        // Event handler for the Log In button click
        // Event handler for the Log In button click
        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            string mysqlconn = "server=127.0.0.1;user=root;database=budget;password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlconn);

            string email = adminEmailTextBox.Text;
            string password = adminPasswordTextBox.Text;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("No empty fields allowed");
                return; // Make sure to return after showing the message box.
            }
            else
            {
                try
                {
                    mySqlConnection.Open();
                    MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM admin WHERE Email = @Email AND Password = @Password", mySqlConnection);
                    mySqlCommand.Parameters.AddWithValue("@Email", email);
                    mySqlCommand.Parameters.AddWithValue("@Password", password);
                    MySqlDataReader reader = mySqlCommand.ExecuteReader();
                    if (reader.Read())
                    {
                        // Open the MainPage window after successful login
                        MainPage mainPage = new MainPage();
                        mainPage.Show();
                        this.Close(); // Close the login window, if you prefer to only have one window open at a time
                    }
                    else
                    {
                        MessageBox.Show("Invalid Login");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    mySqlConnection.Close();
                }
            }
        }



        // Event handler for the Back button click
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            // Logic to navigate back to the previous page
            // For example:
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        // Event handler for the Forgot Password hyperlink click
        private void ForgotPasswordHyperlink_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the ForgotPasswordAdmin window
            ForgotPasswordAdmin forgotPasswordAdminWindow = new ForgotPasswordAdmin();
            forgotPasswordAdminWindow.Show();
        }
    }
}
