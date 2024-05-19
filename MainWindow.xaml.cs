using System.Windows;

namespace FinalFinanceTrack
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the login window
            LogIn loginWindow = new LogIn();

            // Show the login window
            loginWindow.Show();

            // Close the current window (welcome page)
            this.Close();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the sign-up window
            SignUp signUpWindow = new SignUp();

            // Show the sign-up window
            signUpWindow.Show();

            // Close the current window (welcome page)
            this.Close();
        }


        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the admin login window
            AdminsLogIn adminsLoginWindow = new AdminsLogIn();

            // Show the admin login window
            adminsLoginWindow.Show();

            // Close the current window (welcome page)
            this.Close();
        }

    }
}
