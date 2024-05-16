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
            // Equivalent to Form2_Load
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            UserMan f1 = new UserMan();
            f1.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, RoutedEventArgs e)
        {
            // Your text changed logic here
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            UserList f6 = new UserList();
            f6.Show();
            this.Hide();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow f4 = new AddUserWindow();
            f4.Show();
            this.Hide();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            EditUserWindow f5 = new EditUserWindow();
            f5.Show();
            this.Hide();
        }

        private void button_Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteUser f5 = new DeleteUser();
            f5.Show();
            this.Hide();
        }
    }
}
