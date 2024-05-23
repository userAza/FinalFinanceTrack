using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace FinalFinanceTrack
{
    public partial class UserList : Window
    {
        private string connectionString = "server=127.0.0.1;user=root;database=budget;User="; 

        public UserList()
        {
            InitializeComponent();
            LoadUserData();
        }

        private void LoadUserData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Users", conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    UserDataGrid.ItemsSource = dataTable.DefaultView;
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
