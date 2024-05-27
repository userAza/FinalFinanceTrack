using System;
using System.Windows;

namespace FinalFinanceTrack
{
    public partial class DeleteUser : Window
    {
        private DbManager dbManager;

        public DeleteUser()
        {
            InitializeComponent();
            dbManager = new DbManager();
        }

        private void back_text_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Hide();
        }

        private void Delete_text_Click(object sender, RoutedEventArgs e)
        {
            string emailAddress = email_textbox.Text;

            bool isDeleted = dbManager.DeleteUserByEmail(emailAddress);

            if (isDeleted)
            {
                MessageBox.Show("User deleted successfully!");
            }
            else
            {
                MessageBox.Show("No user found with the provided email address.");
            }
        }
    }
}
