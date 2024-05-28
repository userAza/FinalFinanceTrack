﻿using System.Windows;

namespace FinalFinanceTrack
{
    public partial class UserMan : Window
    {
        private int userId;

        public UserMan()
        {
            InitializeComponent();
        }

        public UserMan(int userId) : this()
        {
            this.userId = userId;
        }

        private void UserListButton_Click(object sender, RoutedEventArgs e)
        {
            UserList userList = new UserList(userId);
            userList.Show();
            this.Close();
        }

        private void AddNewUserButton_Click(object sender, RoutedEventArgs e)
        {
            CreateUser newuser = new CreateUser();
            newuser.Show();
            this.Close();
        }

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
           

            EditUser editUser = new EditUser(userId);
            editUser.Show();
            this.Close();
        }

        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            DbManager dbManager = new DbManager();
            bool success = dbManager.InsertUser(firstName, lastName, email, password);

            if (success)
            {
                MessageBox.Show("User created successfully.");
                // Clear input fields and hide the Add User Panel
                FirstNameTextBox.Clear();
                LastNameTextBox.Clear();
                EmailTextBox.Clear();
                PasswordBox.Clear();
                AddUserPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Failed to create user.");
            }
        }
        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteUser deleteUser = new DeleteUser(userId);
            deleteUser.Show();
            this.Close();
        }


    }
}
