using FinalFinanceTrack;
using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;
using System.Collections.Generic;

public class DbManager
{
    private MySqlConnection connection;
    private string connectionString = "server=127.0.0.1;user=root;database=budget;password=";
    public MySqlConnection Connection { get; private set; }

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

    public bool InsertUser(string firstName, string lastName, string email, string password, string securityQuestion1, string securityQuestion2, string securityQuestion3)
    {
        if (!OpenConnection())
            return false;

        try
        {
            string query = "INSERT INTO user (First_Name, Last_Name, Email, Password, SecurityQuestion1, SecurityQuestion2, SecurityQuestion3) " +
                           "VALUES (@FirstName, @LastName, @Email, @Password, @SecurityQuestion1, @SecurityQuestion2, @SecurityQuestion3)";
            MySqlCommand cmd = new MySqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.Parameters.AddWithValue("@SecurityQuestion1", securityQuestion1);
            cmd.Parameters.AddWithValue("@SecurityQuestion2", securityQuestion2);
            cmd.Parameters.AddWithValue("@SecurityQuestion3", securityQuestion3);

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


    public bool InsertIncome(int userId, decimal amount, string month, string year, int categoryId)
    {
        if (!OpenConnection())
            return false;

        MySqlTransaction transaction = null;
        try
        {
            transaction = connection.BeginTransaction();

            // Insert into the income table
            string queryIncome = "INSERT INTO income (Amount, Month, Year, CategoryID) VALUES (@Amount, @Month, @Year, @CategoryId);";
            MySqlCommand cmdIncome = new MySqlCommand(queryIncome, connection, transaction);
            cmdIncome.Parameters.AddWithValue("@Amount", amount);
            cmdIncome.Parameters.AddWithValue("@Month", month);
            cmdIncome.Parameters.AddWithValue("@Year", year);
            cmdIncome.Parameters.AddWithValue("@CategoryId", categoryId);
            cmdIncome.ExecuteNonQuery();
            long incomeId = cmdIncome.LastInsertedId;

            // Insert into the userincome table
            string queryUserIncome = "INSERT INTO userincome (User_ID, Income_ID) VALUES (@UserId, @IncomeId);";
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

    public int GetIncomeCategoryId(string categoryName)
    {
        if (!OpenConnection())
            return -1;

        try
        {
            string query = "SELECT ID FROM categoryincome WHERE Type = @CategoryName";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CategoryName", categoryName);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return reader.GetInt32("ID");
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error retrieving category ID: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }

        return -1; // Return an invalid ID if something went wrong
    }


    //old code: 1hr ago  29/05/2024
    /*    public bool InsertIncome(int userId, decimal amount, string month, string year)
        {
            if (!OpenConnection())
                return false;

            MySqlTransaction transaction = null;
            try
            {
                transaction = connection.BeginTransaction();

                // Insert into the income table
                string queryIncome = "INSERT INTO income (Amount, Month, Year) VALUES (@Amount, @Month, @Year);";
                MySqlCommand cmdIncome = new MySqlCommand(queryIncome, connection, transaction);
                cmdIncome.Parameters.AddWithValue("@Amount", amount);
                cmdIncome.Parameters.AddWithValue("@Month", month);
                cmdIncome.Parameters.AddWithValue("@Year", year);
                cmdIncome.ExecuteNonQuery();
                long incomeId = cmdIncome.LastInsertedId;

                // Insert into the userincome table
                string queryUserIncome = "INSERT INTO userincome (User_ID, Income_ID) VALUES (@UserId, @IncomeId);";
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
    */
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

    public bool UpdatePassword(string email, string newPassword)
    {
        if (!OpenConnection())
            return false;

        try
        {
            string query = "UPDATE user SET Password = @Password WHERE Email = @Email";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Password", newPassword);
            cmd.Parameters.AddWithValue("@Email", email);

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

    public User GetUserByEmail(string email)
    {
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM user WHERE Email = @Email";
            var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Email", email);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    User user = new User
                    {
                        Id = reader.GetInt32("Id"),
                        FirstName = reader.GetString("First_Name"),
                        LastName = reader.GetString("Last_Name"),
                        Email = reader.GetString("Email"),
                        Password = reader.GetString("Password")
                    };
                    return user;
                }
            }
        }
        return null;
    }

    public DataTable GetUserById(int userId)
    {
        DataTable dataTable = new DataTable();

        if (!OpenConnection())
            return dataTable;

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
            MessageBox.Show("Error retrieving user: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }

        return dataTable;
    }

    public int? GetUserIdByEmail(string email)
    {
        int? userId = null;
        if (!OpenConnection()) return userId;

        try
        {
            string query = "SELECT ID FROM user WHERE Email = @Email";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Email", email);

            object result = cmd.ExecuteScalar();
            if (result != null)
            {
                userId = Convert.ToInt32(result);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error retrieving user ID: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }

        return userId;
    }
    public string GetUserEmailById(int userId)
    {
        if (!OpenConnection())
            return null;

        try
        {
            string query = "SELECT Email FROM user WHERE Id = @UserId";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserId", userId);

            object result = cmd.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                return result.ToString();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error retrieving user email: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }
        return null;
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

    public bool ValidateOldPassword(string email, string oldPassword)
    {
        if (!OpenConnection())
            return false;

        try
        {
            string query = "SELECT COUNT(*) FROM user WHERE Email = @Email AND Password = @Password";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", oldPassword);

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


    public static decimal GetTotalIncomeForUser(int userId, int month, DbManager dbManager)
    {
        decimal totalIncome = 0;

        if (dbManager.OpenConnection())
        {
            try
            {
                string query = "SELECT SUM(Amount) FROM income WHERE ID = @UserId AND MONTH = @Month";
                MySqlCommand command = new MySqlCommand(query, dbManager.connection);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@Month", month);

                object result = command.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    totalIncome = Convert.ToDecimal(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving total income: " + ex.Message);
            }
            finally
            {
                dbManager.CloseConnection();
            }
        }

        return totalIncome;
    }

    public static decimal GetTotalExpensesForUser(int userId, int month, DbManager dbManager)
    {
        decimal totalExpenses = 0;

        if (dbManager.OpenConnection())
        {
            try
            {
                string query = "SELECT SUM(Amount) FROM expense WHERE ID = @UserId AND MONTH = @Month";
                MySqlCommand command = new MySqlCommand(query, dbManager.connection);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@Month", month);

                object result = command.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    totalExpenses = Convert.ToDecimal(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving total expenses: " + ex.Message);
            }
            finally
            {
                dbManager.CloseConnection();
            }
        }

        return totalExpenses;
    }

    public bool UpdateProfilePicture(int userId, byte[] imageBytes)
    {
        if (!OpenConnection())
            return false;

        try
        {
            string query = "UPDATE user SET ProfilePhoto = @ProfilePhoto WHERE Id = @UserId";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ProfilePhoto", imageBytes);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error updating profile picture: " + ex.Message);
            return false;
        }
        finally
        {
            CloseConnection();
        }
    }

    public bool DeleteProfilePicture(int userId)
    {
        if (!OpenConnection())
            return false;

        try
        {
            string query = "UPDATE user SET ProfilePhoto = NULL WHERE Id = @UserId";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error deleting profile picture: " + ex.Message);
            return false;
        }
        finally
        {
            CloseConnection();
        }
    }

    public byte[] GetProfilePicture(int userId)
    {
        if (!OpenConnection())
            return null;

        try
        {
            string query = "SELECT ProfilePhoto FROM user WHERE Id = @UserId";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserId", userId);

            object result = cmd.ExecuteScalar();
            if (result != DBNull.Value && result != null)
            {
                return (byte[])result;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error retrieving profile picture: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }
        return null;
    }

    public string GetAdminEmailById(int adminId)
    {
        if (!OpenConnection())
            return null;

        try
        {
            string query = "SELECT Email FROM admin WHERE ID = @AdminId";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@AdminId", adminId);

            object result = cmd.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                return result.ToString();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error retrieving admin email: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }
        return null;
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

            int result = cmd.ExecuteNonQuery();
            return result > 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error updating admin password: " + ex.Message);
            return false;
        }
        finally
        {
            CloseConnection();
        }
    }

    public bool ValidateOldAdminPassword(string email, string oldPassword)
    {
        if (!OpenConnection())
            return false;

        try
        {
            string query = "SELECT COUNT(*) FROM admin WHERE Email = @Email AND Password = @Password";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", oldPassword);

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error validating old admin password: " + ex.Message);
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

    public int? GetAdminIdByEmail(string email)
    {
        if (!OpenConnection())
            return null;

        try
        {
            string query = "SELECT ID FROM admin WHERE Email = @Email";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Email", email);

            object result = cmd.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                return Convert.ToInt32(result);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error retrieving admin ID: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }
        return null;
    }

    public bool InsertExpense(int userId, decimal amount, string month, string year, int categoryId)
    {
        if (!OpenConnection())
            return false;

        MySqlTransaction transaction = null;
        try
        {
            transaction = connection.BeginTransaction();

            // Insert into the expense table
            string queryExpense = "INSERT INTO expense (Amount, Month, Year, CategoryID) VALUES (@Amount, @Month, @Year, @CategoryId);";
            MySqlCommand cmdExpense = new MySqlCommand(queryExpense, connection, transaction);
            cmdExpense.Parameters.AddWithValue("@Amount", amount);
            cmdExpense.Parameters.AddWithValue("@Month", month);
            cmdExpense.Parameters.AddWithValue("@Year", year);
            cmdExpense.Parameters.AddWithValue("@CategoryId", categoryId);
            cmdExpense.ExecuteNonQuery();
            long expenseId = cmdExpense.LastInsertedId;

            // Insert into the userexpense table
            string queryUserExpense = "INSERT INTO userexpense (User_ID, Expense_ID) VALUES (@UserId, @ExpenseId);";
            MySqlCommand cmdUserExpense = new MySqlCommand(queryUserExpense, connection, transaction);
            cmdUserExpense.Parameters.AddWithValue("@UserId", userId);
            cmdUserExpense.Parameters.AddWithValue("@ExpenseId", expenseId);
            cmdUserExpense.ExecuteNonQuery();

            transaction.Commit();
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to save expense data: {ex.Message}");
            transaction?.Rollback();
            return false;
        }
        finally
        {
            CloseConnection();
        }
    }



    public int GetExpenseCategoryId(string categoryName)
    {
        if (!OpenConnection())
            return -1;

        try
        {
            string query = "SELECT ID FROM categoryexpense WHERE Type = @CategoryName";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CategoryName", categoryName);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return reader.GetInt32("ID");
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error retrieving category ID: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }

        return -1; // Return an invalid ID if something went wrong
    }

    //retrieve income data for the specific user:
    public List<IncomeRecord> GetUserIncome(int userId)
    {
        List<IncomeRecord> incomes = new List<IncomeRecord>();

        if (!OpenConnection())
            return incomes;

        try
        {
            string query = "SELECT income.ID, Amount, Month, Year, categoryincome.Type as Category FROM income " +
                           "JOIN categoryincome ON income.CategoryID = categoryincome.ID " +
                           "JOIN userincome ON income.ID = userincome.Income_ID " +
                           "WHERE userincome.User_ID = @UserId";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserId", userId);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    incomes.Add(new IncomeRecord
                    {
                        ID = reader.GetInt32("ID"),
                        Amount = reader.GetDecimal("Amount"),
                        Month = reader.GetString("Month"),
                        Year = reader.GetString("Year"),
                        Category = reader.GetString("Category")
                    });
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error retrieving user income: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }

        return incomes;
    }

    public List<IncomeRecord> GetUserIncomeByCategory(int userId, string category)
    {
        List<IncomeRecord> incomes = new List<IncomeRecord>();

        if (!OpenConnection())
            return incomes;

        try
        {
            string query = "SELECT income.ID, Amount, Month, Year, categoryincome.Type as Category FROM income " +
                           "JOIN categoryincome ON income.CategoryID = categoryincome.ID " +
                           "JOIN userincome ON income.ID = userincome.Income_ID " +
                           "WHERE userincome.User_ID = @UserId AND categoryincome.Type = @Category";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@Category", category);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    incomes.Add(new IncomeRecord
                    {
                        ID = reader.GetInt32("ID"),
                        Amount = reader.GetDecimal("Amount"),
                        Month = reader.GetString("Month"),
                        Year = reader.GetString("Year"),
                        Category = reader.GetString("Category")
                    });
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error retrieving user income by category: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }

        return incomes;
    }


    public List<ExpenseRecord> GetUserExpenses(int userId)
    {
        List<ExpenseRecord> expenses = new List<ExpenseRecord>();

        if (!OpenConnection())
            return expenses;

        try
        {
            string query = "SELECT expense.ID, Amount, Month, Year, categoryexpense.Type as Category FROM expense " +
                           "JOIN categoryexpense ON expense.CategoryID = categoryexpense.ID " +
                           "JOIN userexpense ON expense.ID = userexpense.Expense_ID " +
                           "WHERE userexpense.User_ID = @UserId";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserId", userId);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    expenses.Add(new ExpenseRecord
                    {
                        ID = reader.GetInt32("ID"),
                        Amount = reader.GetDecimal("Amount"),
                        Month = reader.GetString("Month"),
                        Year = reader.GetString("Year"),
                        Category = reader.GetString("Category")
                    });
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error retrieving user expenses: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }

        return expenses;
    }

    public List<ExpenseRecord> GetUserExpensesByCategory(int userId, string category)
    {
        List<ExpenseRecord> expenses = new List<ExpenseRecord>();

        if (!OpenConnection())
            return expenses;

        try
        {
            string query = "SELECT expense.ID, Amount, Month, Year, categoryexpense.Type as Category FROM expense " +
                           "JOIN categoryexpense ON expense.CategoryID = categoryexpense.ID " +
                           "JOIN userexpense ON expense.ID = userexpense.Expense_ID " +
                           "WHERE userexpense.User_ID = @UserId AND categoryexpense.Type = @Category";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@Category", category);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    expenses.Add(new ExpenseRecord
                    {
                        ID = reader.GetInt32("ID"),
                        Amount = reader.GetDecimal("Amount"),
                        Month = reader.GetString("Month"),
                        Year = reader.GetString("Year"),
                        Category = reader.GetString("Category")
                    });
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error retrieving user expenses by category: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }

        return expenses;
    }

    public List<string> GetIncomeCategories()
    {
        List<string> categories = new List<string>();

        if (!OpenConnection())
            return categories;

        try
        {
            string query = "SELECT Type FROM categoryincome";
            MySqlCommand cmd = new MySqlCommand(query, connection);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    categories.Add(reader.GetString("Type"));
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error retrieving income categories: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }

        return categories;
    }
    public bool DoesUserExist(int userId)
    {
        if (!OpenConnection())
            return false;

        try
        {
            string query = "SELECT COUNT(*) FROM user WHERE ID = @UserId";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserId", userId);

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error verifying user: " + ex.Message);
            return false;
        }
        finally
        {
            CloseConnection();
        }
    }

    public decimal GetTotalIncomeForUser(int userId, int month)
    {
        decimal totalIncome = 0;
        if (!OpenConnection()) return totalIncome;

        try
        {
            string query = @"
                SELECT SUM(i.Amount) 
                FROM income i
                JOIN userincome ui ON i.ID = ui.Income_Id
                WHERE ui.User_Id = @UserId AND MONTH(STR_TO_DATE(i.Month, '%M')) = @Month";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@Month", month);

            object result = cmd.ExecuteScalar();
            if (result != DBNull.Value)
            {
                totalIncome = Convert.ToDecimal(result);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error retrieving total income: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }

        return totalIncome;
    }

    public decimal GetTotalExpensesForUser(int userId, int month)
    {
        decimal totalExpenses = 0;
        if (!OpenConnection()) return totalExpenses;

        try
        {
            string query = @"
                SELECT SUM(e.Amount) 
                FROM expense e
                JOIN userexpense ue ON e.ID = ue.Expense_Id
                WHERE ue.User_Id = @UserId AND MONTH(STR_TO_DATE(e.Month, '%M')) = @Month";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@Month", month);

            object result = cmd.ExecuteScalar();
            if (result != DBNull.Value)
            {
                totalExpenses = Convert.ToDecimal(result);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error retrieving total expenses: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }

        return totalExpenses;
    }
}





