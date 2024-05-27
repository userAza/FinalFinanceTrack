using System;
using System.Data;
using System.Windows;

namespace FinalFinanceTrack
{
    public partial class UserList : Window
    {
        private DbManager dbManager;

        public UserList()
        {
            InitializeComponent();
            dbManager = new DbManager();
            LoadUserData();
        }

        private void LoadUserData()
        {
            try
            {
                DataTable userData = dbManager.GetUsers();
                if (userData != null)
                {
                    UserDataGrid.ItemsSource = userData.DefaultView;
                }
                else
                {
                    MessageBox.Show("No user data found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user data: " + ex.Message);
            }
        }

        private void button_Back_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Hide();
        }
    }
}
