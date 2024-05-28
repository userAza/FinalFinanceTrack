using System.Windows;

namespace FinalFinanceTrack
{
    public partial class DeleteUser : Window
    {
        private DbManager dbManager;
        private int userId;

        public DeleteUser(int userId)
        {
            InitializeComponent();
            dbManager = new DbManager();
            this.userId = userId;
        }

        private void Delete_text_Click(object sender, RoutedEventArgs e)
        {
            string email = email_textbox.Text.Trim();
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please enter a valid email address.");
                return;
            }

            bool isDeleted = dbManager.DeleteUserByEmail(email);
            if (isDeleted)
            {
                MessageBox.Show("User deleted successfully!");
                //this.Close();
            }
            else
            {
                MessageBox.Show("Failed to delete user.");
            }
        }

        private void back_text_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            UserMan userMan = new UserMan(userId);
            userMan.Show();
            this.Close();
        }
    }
}
