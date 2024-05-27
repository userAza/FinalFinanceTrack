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

        private void SelectProfilePicture_button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            if (openFileDialog.ShowDialog() == true)
            {
                profilePicturePath = openFileDialog.FileName;
                ProfilePicture_image.Source = new BitmapImage(new Uri(profilePicturePath));
            }
        }

        private void button_createuser_Click(object sender, RoutedEventArgs e)
        {
            string firstName = Firstname_textbox.Text;
            string lastName = LastName_Textbox.Text;
            string email = EmailAddress_textbox.Text;
            string password = Password_textbox.Password;

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(profilePicturePath))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            
            byte[] profilePictureData = File.ReadAllBytes(profilePicturePath);

            
            bool isSuccess = dbManager.InsertUser(firstName, lastName, email, password, profilePictureData);

            if (isSuccess)
            {
                MessageBox.Show("User created successfully!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to create user.");
            }
        }

        private void button_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
