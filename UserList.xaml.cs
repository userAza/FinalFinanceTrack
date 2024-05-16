using System.Windows;

namespace FinalFinanceTrack
{
    public partial class UserList : Window
    {
        public UserList()
        {
            InitializeComponent();
        }

        private void button_Back_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Hide();
        }
    }
}
