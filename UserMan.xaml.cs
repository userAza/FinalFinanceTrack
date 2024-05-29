using System.Windows;

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
            CreateUser user = new CreateUser();
            user.Show();
        }
        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteUser deleteUser = new DeleteUser(userId);
            deleteUser.Show();
            this.Close();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage(userId);
            mainPage.Show();
            this.Close();
        }

    }
}
