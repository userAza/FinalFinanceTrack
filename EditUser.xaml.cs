using System.Windows;

namespace FinalFinanceTrack
{
    public partial class EditUser : Window
    {
        public EditUser()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement the functionality to save the edited user details
            // For example:
            // string userId = UserIdTextBox.Text;
            // string userName = UserNameTextBox.Text;
            // string email = EmailTextBox.Text;
            // Save the details to the database or perform the necessary operations

            MessageBox.Show("User details saved successfully!");

            // Close the window after saving
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the window without saving
            this.Close();
        }
    }
}
