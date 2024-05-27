using System.Data;
using System.Windows;

namespace FinalFinanceTrack
{
    public partial class EditUser : Window
    {
        private DbManager dbManager;
        private int userId;

        public EditUser(int userId)
        {
            InitializeComponent();
            dbManager = new DbManager();
            this.userId = userId;
            LoadUserData();
        }

        private void LoadUserData()
        {
            DataTable userData = dbManager.GetUserById(userId);

            if (userData != null && userData.Rows.Count > 0)
            {
                DataRow row = userData.Rows[0];
                UserIdTextBox.Text = row["Id"].ToString();
                FirstNameTextBox.Text = row["First_Name"].ToString();
                LastNameTextBox.Text = row["Last_Name"].ToString();
                EmailTextBox.Text = row["Email"].ToString();
            }
            else
            {
                MessageBox.Show("User not found.");
                this.Close();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string email = EmailTextBox.Text;

            bool isUpdated = dbManager.UpdateUser(userId, firstName, lastName, email);

            if (isUpdated)
            {
                MessageBox.Show("User details saved successfully!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Error saving user details.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
