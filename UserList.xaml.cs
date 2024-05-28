using System;
using System.Data;
using System.Windows;

namespace FinalFinanceTrack
{
    public partial class UserList : Window
    {
        private DbManager dbManager;
        private int userId;

        public UserList(int userId)
        {
            InitializeComponent();
            dbManager = new DbManager();
            this.userId = userId;
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
            UserMan userMan = new UserMan(userId);
            userMan.Show();
            this.Close();
        }
    }
}
