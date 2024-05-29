using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FinalFinanceTrack
{
    public partial class CreateUser : Window
    {
        private DbManager dbManager;
        private string profilePicturePath;

        public CreateUser()
        {
            InitializeComponent();
            dbManager = new DbManager();
        }

        private void label5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Example: Showing a password hint when the label is clicked
            MessageBox.Show("Enter a strong password.");
        }

        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Example: Live validation for first name input
            if (string.IsNullOrWhiteSpace(Firstname_textbox.Text))
            {
                Firstname_textbox.Background = new SolidColorBrush(System.Windows.Media.Colors.LightPink);
            }
            else
            {
                Firstname_textbox.Background = new SolidColorBrush(System.Windows.Media.Colors.White);
            }
        }

        private void button_createuser_Click(object sender, RoutedEventArgs e)
        {
            string firstName = Firstname_textbox.Text;
            string lastName = LastName_Textbox.Text;
            string email = EmailAddress_textbox.Text;
            string password = Password_textbox.Password;
            string securityQuestion1 = SecurityQuestion1_textbox.Text;
            string securityQuestion2 = SecurityQuestion2_textbox.Text;
            string securityQuestion3 = SecurityQuestion3_textbox.Text;

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(securityQuestion1) || string.IsNullOrWhiteSpace(securityQuestion2) || string.IsNullOrWhiteSpace(securityQuestion3))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            bool isSuccess = dbManager.InsertUser(firstName, lastName, email, password, securityQuestion1, securityQuestion2, securityQuestion3);

            if (isSuccess)
            {
                MessageBox.Show("User created successfully!");
                //this.Close();
            }
            else
            {
                MessageBox.Show("Failed to create user.");
            }
        }

        private void button_Exit_Click(object sender, RoutedEventArgs e)
        {
            UserMan userMan = new UserMan();
            userMan.Show();
            this.Close();
        }
    }
}
