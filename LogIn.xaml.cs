using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FinalFinanceTrack;
using MySql.Data.MySqlClient;

namespace FinTrackWpf
{
    public partial class LogIn : Window
    {
        public LogIn()
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
                textBox.Text = textBox.Name == "emailTextBox" ? "Email" : "Password";
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string mysqlconn = "server=127.0.0.1;user=root;database=budget;password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlconn);

            string email = emailTextBox.Text;
            string password = passwordTextBox.Text;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("No empty fields allowed");
            }
            else
            {
                try
                {
                    mySqlConnection.Open();
                    MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM user WHERE Email = @Email AND Password = @Password", mySqlConnection);
                    mySqlCommand.Parameters.AddWithValue("@Email", email);
                    mySqlCommand.Parameters.AddWithValue("@Password", password);
                    MySqlDataReader reader = mySqlCommand.ExecuteReader();
                    if (reader.Read())
                    {
                        MessageBox.Show("Login success");
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

        private void ForgotPasswordHyperlink_Click(object sender, RoutedEventArgs e)
        {
            ForgotPassword forgotPasswordWindow = new ForgotPassword();
            forgotPasswordWindow.Show();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void SignUpHyperlink_Click(object sender, RoutedEventArgs e)
        {
            SignUp signUpWindow = new SignUp();
            signUpWindow.Show();
            this.Close();
        }

    }
}
