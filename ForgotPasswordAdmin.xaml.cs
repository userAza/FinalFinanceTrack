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

namespace FinalFinanceTrack
{
    public partial class ForgotPasswordAdmin : Window
    {
        public ForgotPasswordAdmin()
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

        // Event handler for the Submit button click
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            // Logic for sending email to the entered email address
            MessageBox.Show("Email sent to the provided email address.");
        }

        // Event handler for the Back button click
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            // Close the ForgotPassword window
            this.Close();
        }
    }
}