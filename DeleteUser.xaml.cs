using System;
using System.Windows;
using FinalFinanceTrack;
using MySql.Data.MySqlClient;
using static Org.BouncyCastle.Bcpg.Attr.ImageAttrib;

namespace FinalFinanceTrack
{
    public partial class DeleteUser: Window
    {
        public DeleteUser()
        {
            InitializeComponent();
        }

        string myConnectionString = "server=localhost;database=budget;uid=root;pwd=root;";

        private void back_text_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Hide();
        }

        private void Delete_text_Click(object sender, RoutedEventArgs e)
        {
            using (MySqlConnection cnn = new MySqlConnection(myConnectionString))
            {
                try
                {
                    cnn.Open();

                    string emailAddress = email_textbox.Text; // Assuming you have a textbox for email address input

                    string query = "DELETE FROM User WHERE Email = @Email";

                    MySqlCommand cmd = new MySqlCommand(query, cnn);

                    cmd.Parameters.AddWithValue("@Email", emailAddress);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User deleted successfully!");
                    }
                    else
                    {
                        MessageBox.Show("No user found with the provided email address.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
