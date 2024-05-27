using System.Windows;

namespace FinalFinanceTrack
{
    public partial class DeleteUser : Window
    {
        private int userId;
        private DbManager dbManager;

        public DeleteUser(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            dbManager = new DbManager();
        }

        

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            bool isDeleted = dbManager.DeleteUser(userId);
            if (isDeleted)
            {
                MessageBox.Show("User deleted successfully!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to delete user.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
