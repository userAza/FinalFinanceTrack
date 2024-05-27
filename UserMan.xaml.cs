using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace FinalFinanceTrack
{
    public partial class UserMan : Window
    {
        private DbManager dbManager;
        private int selectedUserId = -1;

        public UserMan()
        {
            InitializeComponent();
            dbManager = new DbManager();
            LoadUserData();
        }

        private void LoadUserData()
        {
            DataTable userData = dbManager.GetAllUsers();
            if (userData.Rows.Count > 0)
            {
                // For demonstration, just set the selectedUserId to the first user in the list
                selectedUserId = Convert.ToInt32(userData.Rows[0]["Id"]);
            }
        }

        private void AddNewUserButton_Click(object sender, RoutedEventArgs e)
        {
            CreateUser createUser = new CreateUser();
            createUser.Show();
            this.Hide();
        }

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUserId != -1)
            {
                EditUser editUser = new EditUser(selectedUserId);
                editUser.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please select a user to edit.");
            }
        }


        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUserId != -1)
            {
                DeleteUser deleteUser = new DeleteUser(selectedUserId);
                deleteUser.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please select a user to delete.");
            }
        }

        private void UserListButton_Click(object sender, RoutedEventArgs e)
        {
            UserList userList = new UserList();
            userList.Show();
            this.Hide();
        }
    }
}
