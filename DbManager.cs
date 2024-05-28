using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

    public bool InsertUser(string firstName, string lastName, string email, string password, byte[] profilePhoto)
    {
        if (!OpenConnection())
            return false;

        try
        {
            string query = "INSERT INTO user (First_Name, Last_Name, Email, Password, profilePhoto) VALUES (@FirstName, @LastName, @Email, @Password, @profilePhoto)";
            MySqlCommand cmd = new MySqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.Parameters.AddWithValue("@ProfilePhoto", profilePhoto);

            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error during user creation: " + ex.Message);
            return false;
        }
        finally
        {
            CloseConnection();
        }
    }

    public DataTable GetAllUsers()
    {
        DataTable dataTable = new DataTable();

        if (!OpenConnection())
            return dataTable;

        try
        {
            string query = "SELECT Id, First_Name, Last_Name, Email FROM user";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dataTable);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading user data: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }

        return dataTable;
    }

    public bool DeleteUser(int userId)
    {
        if (!OpenConnection())
            return false;

        try
        {
            string query = "DELETE FROM user WHERE Id = @UserId";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserId", userId);

            int result = cmd.ExecuteNonQuery();
            return result > 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error deleting user: " + ex.Message);
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

            string queryIncome = "INSERT INTO income (Amount, Month, Year) VALUES (@Amount, @Month, @Year);";
            MySqlCommand cmdIncome = new MySqlCommand(queryIncome, connection, transaction);
            cmdIncome.Parameters.AddWithValue("@Amount", amount);
            cmdIncome.Parameters.AddWithValue("@Month", month);
            cmdIncome.Parameters.AddWithValue("@Year", year);
            cmdIncome.ExecuteNonQuery();
            long incomeId = cmdIncome.LastInsertedId;

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

    public bool UpdateUserPassword(int userId, string oldPassword, string newPassword)
    {
        if (!OpenConnection())
            return false;

        try
        {
            string query = "SELECT COUNT(*) FROM user WHERE Id = @UserId AND Password = @OldPassword";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@OldPassword", oldPassword);

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            if (count == 0)
            {
                return false;
            }

            query = "UPDATE user SET Password = @NewPassword WHERE Id = @UserId";
            cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@NewPassword", newPassword);
            cmd.Parameters.AddWithValue("@UserId", userId);

            int result = cmd.ExecuteNonQuery();
            return result > 0;
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

    public bool UpdateAdminPassword(string email, string newPassword)
    {
        if (!OpenConnection())
            return false;

        try
        {
            string query = "UPDATE admin SET Password = @Password WHERE Email = @Email";
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

    public string GetAdminPassword(string email)
    {
        if (!OpenConnection())
            return null;

        try
        {
            string query = "SELECT Password FROM admin WHERE Email = @Email";
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

    public (string, string, string) GetAdminSecurityAnswers(string email)
    {
        if (!OpenConnection())
            return (null, null, null);

        try
        {
            string query = "SELECT Answer1, Answer2, Answer3 FROM admin WHERE Email = @Email LIMIT 1";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Email", email);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    string answer1 = reader["Answer1"] as string;
                    string answer2 = reader["Answer2"] as string;
                    string answer3 = reader["Answer3"] as string;
                    return (answer1, answer2, answer3);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error retrieving security answers: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }
        return (null, null, null);
    }

    public bool DeleteUserByEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            MessageBox.Show("Please enter a valid email address.");
            return false;
        }

        if (!OpenConnection())
            return false;

        try
        {
            string query = "DELETE FROM user WHERE Email = @Email";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Email", email);

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error deleting user: " + ex.Message);
            return false;
        }
        finally
        {
            CloseConnection();
        }
    }

    public DataTable GetUsers()
    {
        DataTable dataTable = new DataTable();

        if (!OpenConnection())
            return null;

        try
        {
            string query = "SELECT * FROM user";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dataTable);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading user data: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }

        return dataTable;
    }

    public DataTable GetUserById(int userId)
    {
        DataTable dataTable = new DataTable();

        if (!OpenConnection())
            return null;

        try
        {
            string query = "SELECT * FROM user WHERE Id = @UserId";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserId", userId);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dataTable);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading user data: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }

        return dataTable;
    }

    public bool UpdateUser(int userId, string firstName, string lastName, string email)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(email))
        {
            MessageBox.Show("Please fill in all fields.");
            return false;
        }

        if (!OpenConnection())
            return false;

        try
        {
            string query = "UPDATE user SET First_Name = @FirstName, Last_Name = @LastName, Email = @Email WHERE Id = @UserId";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@UserId", userId);

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error updating user: " + ex.Message);
            return false;
        }
        finally
        {
            CloseConnection();
        }
    }

    public decimal GetTotalSavings(int year, int month)
    {
        decimal savings = 0;

        if (!OpenConnection())
            return savings;

        try
        {
            string query = "SELECT SUM(Savings) FROM History WHERE YEAR(Date) = @Year AND MONTH(Date) = @Month";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Year", year);
            cmd.Parameters.AddWithValue("@Month", month);

            object result = cmd.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                savings = Convert.ToDecimal(result);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading savings: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }

        return savings;
    }

    public List<int> GetDistinctYears()
    {
        List<int> years = new List<int>();

        if (!OpenConnection())
            return years;

        try
        {
            string query = "SELECT DISTINCT YEAR(Date) AS Year FROM History";
            MySqlCommand cmd = new MySqlCommand(query, connection);

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                years.Add(reader.GetInt32(0));
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading years: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }

        return years;
    }

    public bool ValidateOldPassword(int userId, string hashedOldPassword)
    {
        if (!OpenConnection())
            return false;

        try
        {
            string query = "SELECT COUNT(*) FROM user WHERE Id = @UserId AND Password = @Password";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@Password", hashedOldPassword);

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error validating old password: " + ex.Message);
            return false;
        }
        finally
        {
            CloseConnection();
        }
    }

    public bool UpdatePassword(int userId, string newPassword)
    {
        if (!OpenConnection())
            return false;

        try
        {
            string query = "UPDATE user SET Password = @Password WHERE Id = @UserId";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Password", newPassword);
            cmd.Parameters.AddWithValue("@UserId", userId);

            int result = cmd.ExecuteNonQuery();
            return result > 0;
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

    public bool ValidateAdminLogin(string email, string password)
    {
        if (!OpenConnection())
            return false;

        try
        {
            string query = "SELECT Password FROM admin WHERE Email = @Email";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Email", email);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    string storedPassword = reader["Password"].ToString();
                    return storedPassword == password;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error validating login: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }
        return false;
    }
}
