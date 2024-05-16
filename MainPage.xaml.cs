using System.Windows;

namespace FinalFinanceTrack
{
    public partial class MainPage : Window
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Code to execute when the window is loaded
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            UserMan userMan = new UserMan();
            userMan.Show();
            this.Hide();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            UserListWindow userListWindow = new UserListWindow();
            userListWindow.Show();
            this.Hide();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow addUserWindow = new AddUserWindow();
            addUserWindow.Show();
            this.Hide();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            EditUserWindow editUserWindow = new EditUserWindow();
            editUserWindow.Show();
            this.Hide();
        }

        private void button_Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteUser deleteUserWindow = new DeleteUser();
            deleteUserWindow.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, RoutedEventArgs e)
        {
            // Code to execute when the text in the textbox changes
        }

        private void SETTINGS_Checked(object sender, RoutedEventArgs e)
        {
            // Code to execute when the SETTINGS radio button is checked
        }
    }
}
