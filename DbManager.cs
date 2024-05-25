using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Text.RegularExpressions; // Ensure this using directive is here for Regex

public class DbManager
{
    private MySqlConnection connection;
    private string connectionString = "server=127.0.0.1;user=root;database=budget;password=";

    public DbManager()
    {
        connection = new MySqlConnection(connectionString);
    }

    public bool OpenConnection()
    {
        try
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Cannot open connection: " + ex.Message);
            return false;
        }
    }

    public void CloseConnection()
    {
        try
        {
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Cannot close connection: " + ex.Message);
        }
    }

    public bool InsertUser(string firstName, string lastName, string email, string password, string answer1, string answer2, string answer3)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
            string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) ||
            string.IsNullOrWhiteSpace(answer1) || string.IsNullOrWhiteSpace(answer2) ||
            string.IsNullOrWhiteSpace(answer3))
        {
            MessageBox.Show("Please fill in all fields.");
            return false;
        }

        if (!Regex.IsMatch(email, @"\A[^@\s]+@[^@\s]+\.[^@\s]+\z"))
        {
            MessageBox.Show("Please enter a valid email address.");
            return false;
        }

        if (!OpenConnection())
            return false;

        try
        {
            string query = "INSERT INTO user (First_Name, Last_Name, Email, Password, Answer1, Answer2, Answer3) VALUES (@FirstName, @LastName, @Email, @Password, @Answer1, @Answer2, @Answer3)";
            MySqlCommand cmd = new MySqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);  // Store password as plain text
            cmd.Parameters.AddWithValue("@Answer1", answer1);
            cmd.Parameters.AddWithValue("@Answer2", answer2);
            cmd.Parameters.AddWithValue("@Answer3", answer3);

            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error during sign up: " + ex.Message);
            return false;
        }
        finally
        {
            CloseConnection();
        }
    }

    public string GetPassword(string email)
    {
        if (!OpenConnection())
            return null;

        try
        {
            string query = "SELECT Password FROM user WHERE Email = @Email";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Email", email);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return reader["Password"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error retrieving password: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }
        return null;

    }


        public bool InsertIncome(int userId, decimal amount, string month, string year)
        {
            if (!OpenConnection())
                return false;

            MySqlTransaction transaction = null;
            try
            {
                transaction = connection.BeginTransaction();

                // Insert income data into the `income` table
                string queryIncome = "INSERT INTO income (Amount, Month, Year) VALUES (@Amount, @Month, @Year);";
                MySqlCommand cmdIncome = new MySqlCommand(queryIncome, connection, transaction);
                cmdIncome.Parameters.AddWithValue("@Amount", amount);
                cmdIncome.Parameters.AddWithValue("@Month", month);
                cmdIncome.Parameters.AddWithValue("@Year", year);
                cmdIncome.ExecuteNonQuery();
                long incomeId = cmdIncome.LastInsertedId;

                // Link the new income record with the user
                string queryUserIncome = "INSERT INTO userincome (user_id, income_id) VALUES (@UserId, @IncomeId);";
                MySqlCommand cmdUserIncome = new MySqlCommand(queryUserIncome, connection, transaction);
                cmdUserIncome.Parameters.AddWithValue("@UserId", userId);
                cmdUserIncome.Parameters.AddWithValue("@IncomeId", incomeId);
                cmdUserIncome.ExecuteNonQuery();

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save income data: {ex.Message}");
                transaction?.Rollback();
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }

    public bool ExecuteQuery(string query, Dictionary<string, object> parameters)
    {
        if (!OpenConnection())
            return false;

        try
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            foreach (var param in parameters)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value);
            }
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Database operation failed: " + ex.Message);
            return false;
        }
        finally
        {
            CloseConnection();
        }
    }

    public bool UpdateUserPassword(string email, string newPassword)
    {
        if (!OpenConnection())
            return false;

        try
        {
            string query = "UPDATE user SET Password = @Password WHERE Email = @Email";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Password", newPassword);
            cmd.Parameters.AddWithValue("@Email", email);

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error updating password: " + ex.Message);
            return false;
        }
        finally
        {
            CloseConnection();
        }
    }







}
