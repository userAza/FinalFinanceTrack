using System.Data;
using System.Windows;

namespace FinalFinanceTrack
{
    public partial class EditUser : Window
    {
        private DbManager dbManager;
        private int userId;

        public EditUser(int userId) : this()
        {
            this.userId = userId;
            // Comment out LoadUserData call here
            // LoadUserData();
        }

        public EditUser()
        {
            InitializeComponent();
            dbManager = new DbManager();
        }

        private void LoadUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(UserIdInputTextBox.Text, out userId))
            {
                LoadUserData();
            }
            else
            {
                MessageBox.Show("Invalid user ID.");
            }
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
                //this.Close();
            }
            else
            {
                MessageBox.Show("Error saving user details.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            UserMan userMan = new UserMan(userId);
            userMan.Show();
            this.Close();
        }

     /*   private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            UserMan userMan = new UserMan();
            userMan.Show();
            this.Close();
        }*/
    }
}
