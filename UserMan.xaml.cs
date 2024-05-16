using System.Windows;

namespace FinalFinanceTrack
{
    public partial class UserMan : Window
    {
        public UserMan()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Equivalent to Form1_Load
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Hide();
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
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

        private void ViewUserListButton_Click(object sender, RoutedEventArgs e)
        {
            UserList userList = new UserList();
            userList.Show();
            this.Hide();
        }
    }
}
