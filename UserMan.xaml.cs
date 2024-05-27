using System.Windows;

namespace FinalFinanceTrack
{
    public partial class UserMan : Window
    {
        public UserMan()
        {
            InitializeComponent();
        }
        private void AddNewUserButton_Click(object sender, RoutedEventArgs e)
        {
            CreateUser createUser = new CreateUser();
            createUser.Show();
            this.Hide();
        }

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            EditUser editUser = new EditUser();
            editUser.Show();
            this.Hide();
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteUser deleteUser = new DeleteUser();
            deleteUser.Show();
            this.Hide();
        }

        private void UserListButton_Click(object sender, RoutedEventArgs e)
        {
            UserList userList = new UserList();
            userList.Show();
            this.Hide();
        }
         private void UserManagementButton_Click(object sender, RoutedEventArgs e)
        {
            UserMan userMan = new UserMan();
            userMan.Show();
            this.Hide();
        }
    }
}
