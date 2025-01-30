using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.DataAccessLayer
{
    public class ExchangeRepository
    {
        public void SaveUserRegistrationDetails(string userName, string dob, string phoneNumber, string email, string hashedPassword)
        {
            string insertQuery = @"INSERT INTO Login (Name, Password, Dob, PhoneNumber, Email) 
                                   VALUES (@Name, @Password, @Dob, @PhoneNumber, @Email)";
            using (SqlConnection connection = new SqlConnection(Global.connectionString))
            {
                SqlCommand cmd = new SqlCommand(insertQuery, connection);
                cmd.Parameters.AddWithValue("@Name", userName);
                cmd.Parameters.AddWithValue("@Password", hashedPassword);
                cmd.Parameters.AddWithValue("@Dob", DateTime.Parse(dob));
                cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                cmd.Parameters.AddWithValue("@Email", email);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Registration Successful.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        /// <summary>
        /// Checks and creates the "Login" table with user details if it doesn't exist.
        /// </summary>
        public void CreateLoginTable()
        {
            try
            {
                string createTableQuery = @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Login' AND xtype='U')
                CREATE TABLE Login (
                Id INT IDENTITY(1,1) PRIMARY KEY,
                Name NVARCHAR(100),
                Password NVARCHAR(255),
                Dob DATE,
                PhoneNumber NVARCHAR(15),
                Email NVARCHAR(100),
                Funds DECIMAL(18, 2) DEFAULT 0
            );";
                using (SqlConnection connection = new SqlConnection(Global.connectionString))
                {
                    SqlCommand cmd = new SqlCommand(createTableQuery, connection);
                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Checks and creates the "UserHoldings" table to store user shareholding details if it doesn't exist.
        /// </summary>
        public void CreateUserHoldingsTable()
        {
            try
            {
                string createTableQuery = @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='UserHoldings' AND xtype='U')
                CREATE TABLE UserHoldings (
                    HoldingID INT IDENTITY(1,1) PRIMARY KEY,
                    Email NVARCHAR(100) NOT NULL,
                    CompanyName NVARCHAR(100) NOT NULL,
                    ShareCount INT NOT NULL,
                    AveragePrice DECIMAL(18, 2) NOT NULL
                );";

                using (SqlConnection connection = new SqlConnection(Global.connectionString))
                {
                    SqlCommand cmd = new SqlCommand(createTableQuery, connection);
                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Checks and creates the "Transactions" table to store transaction details, including shares bought or sold, if it doesn't exist.
        /// </summary>
        public void CreateTransactionTable()
        {
            try
            {
                string createTableQuery = @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Transactions' AND xtype='U')
                CREATE TABLE Transactions (
                    TransactionID INT IDENTITY(1,1) PRIMARY KEY,
                    DateTime DATETIME NOT NULL,
                    CompanyName NVARCHAR(100) NOT NULL,
                    ShareCount INT NOT NULL,
                    LTP DECIMAL(18, 2) NOT NULL,
                    Email NVARCHAR(100) NOT NULL,
                    Type NVARCHAR(10) NOT NULL 
                );";


                using (SqlConnection connection = new SqlConnection(Global.connectionString))
                {
                    SqlCommand cmd = new SqlCommand(createTableQuery, connection);
                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Adds the specified amount to the logged-in user's funds in the "Login" table.
        /// </summary>
        public void AddUsersFunds(decimal amount)
        {
            try
            {
                string updateFundsQuery = "UPDATE Login SET Funds = Funds + @Amount WHERE Email = @Email";
                using (SqlConnection connection = new SqlConnection(Global.connectionString))
                {
                    SqlCommand cmd = new SqlCommand(updateFundsQuery, connection);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@Email", Global.loggedInEmail);

                    try
                    {
                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Successfully added {amount:C} to your account.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to add funds. Please check the email and try again.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Retrieves the user's available funds from the "Login" table based on the provided email.
        /// </summary>
        public int GetUserFunds(string email)
        {
            try
            {
                string query = "SELECT Funds FROM Login WHERE Email = @Email";
                using (SqlConnection connection = new SqlConnection(Global.connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Email", email);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return Convert.ToInt32(reader["Funds"]);
                        }
                    }
                }
                return -1;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Retrieves and displays the logged-in user's shareholdings from the "UserHoldings" table.
        /// </summary>
        public void GetHoldingsBasedOnEmail()
        {
            try
            {
                string selectUserHoldingsQuery = @"
                    SELECT CompanyName, ShareCount, AveragePrice 
                    FROM UserHoldings 
                    WHERE Email = @Email";

                using (SqlConnection connection = new SqlConnection(Global.connectionString))
                {
                    SqlCommand selectUserHoldingsCmd = new SqlCommand(selectUserHoldingsQuery, connection);
                    selectUserHoldingsCmd.Parameters.AddWithValue("@Email", Global.loggedInEmail);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = selectUserHoldingsCmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    string companyName = reader["CompanyName"].ToString();
                                    int shareCount = Convert.ToInt32(reader["ShareCount"]);
                                    double averagePrice = Convert.ToDouble(reader["AveragePrice"]);
                                    Console.WriteLine($"Company: {companyName}, Shares Owned: {shareCount}, Average Price: {averagePrice}");
                                }
                                reader.Close();
                            }
                            else
                            {
                                Console.WriteLine("You do not have any holdings yet.");
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred while fetching holdings: {ex.Message}");
                        return;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Processes the selling of shares for the logged-in user, updates holdings, funds, and transaction details.
        /// </summary>
        public void ProcessSellShareTransaction(string company, Dictionary<string, double> companies)
        {
            try
            {
                string checkHoldingsQuery = @"
                    SELECT ShareCount, AveragePrice 
                    FROM UserHoldings 
                    WHERE CompanyName = @CompanyName AND Email = @Email";

                using (SqlConnection connection = new SqlConnection(Global.connectionString))
                {
                    SqlCommand checkHoldingsCmd = new SqlCommand(checkHoldingsQuery, connection);
                    checkHoldingsCmd.Parameters.AddWithValue("@CompanyName", company);
                    checkHoldingsCmd.Parameters.AddWithValue("@Email", Global.loggedInEmail);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = checkHoldingsCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int sharesOwned = Convert.ToInt32(reader["ShareCount"]);
                                double averagePrice = Convert.ToDouble(reader["AveragePrice"]);
                                reader.Close();

                                Console.Write("Enter number of shares to sell: ");
                                if (int.TryParse(Console.ReadLine(), out int sharesToSell) && sharesToSell > 0 && sharesToSell <= sharesOwned)
                                {
                                    if (!companies.ContainsKey(company))
                                    {
                                        Console.WriteLine("The given company is not available in the market.");
                                        return;
                                    }
                                    double currentSharePrice = companies[company];
                                    double totalRevenue = sharesToSell * currentSharePrice;
                                    using (SqlTransaction transaction = connection.BeginTransaction())
                                    {
                                        try
                                        {
                                            UpdateFundsBasedOnSell(connection, totalRevenue, transaction);
                                            UpdateHoldingsBasedOnSell(company, connection, sharesToSell, transaction);
                                            DeleteUserHoldings(company, connection, transaction);
                                            InsertTransactionDetails(company, sharesToSell, currentSharePrice, "Sell");
                                            transaction.Commit();

                                            Console.WriteLine($"You successfully sold {sharesToSell} shares of {company} at {currentSharePrice:F2} per share. Total Revenue: {totalRevenue:F2}");
                                        }
                                        catch (Exception ex)
                                        {
                                            transaction.Rollback();
                                            Console.WriteLine($"An error occurred while processing the sale: {ex.Message}");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid number of shares to sell. Please enter a valid number.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("You do not own any shares of this company.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred while processing your request: {ex.Message}");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Deletes user holdings from the "UserHoldings" table if the share count reaches zero.
        /// </summary>
        private void DeleteUserHoldings(string company, SqlConnection connection, SqlTransaction transaction)
        {
            try
            {
                string deleteHoldingsQuery = @"
                                        DELETE FROM UserHoldings 
                                        WHERE ShareCount = 0 AND CompanyName = @CompanyName AND Email = @Email";
                SqlCommand deleteHoldingsCmd = new SqlCommand(deleteHoldingsQuery, connection, transaction);
                deleteHoldingsCmd.Parameters.AddWithValue("@CompanyName", company);
                deleteHoldingsCmd.Parameters.AddWithValue("@Email", Global.loggedInEmail);
                deleteHoldingsCmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Updates the user's holdings by reducing the share count after a sell transaction.
        /// </summary>
        private void UpdateHoldingsBasedOnSell(string company, SqlConnection connection, int sharesToSell, SqlTransaction transaction)
        {
            try
            {
                string updateHoldingsQuery = @"
                                        UPDATE UserHoldings 
                                        SET ShareCount = ShareCount - @ShareCount 
                                        WHERE CompanyName = @CompanyName AND Email = @Email";
                SqlCommand updateHoldingsCmd = new SqlCommand(updateHoldingsQuery, connection, transaction);
                updateHoldingsCmd.Parameters.AddWithValue("@ShareCount", sharesToSell);
                updateHoldingsCmd.Parameters.AddWithValue("@CompanyName", company);
                updateHoldingsCmd.Parameters.AddWithValue("@Email", Global.loggedInEmail);
                updateHoldingsCmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Updates the logged-in user's funds by adding the revenue from a sell transaction.
        /// </summary>
        private void UpdateFundsBasedOnSell(SqlConnection connection, double totalRevenue, SqlTransaction transaction)
        {
            try
            {
                string updateFundsQuery = "UPDATE Login SET Funds = Funds + @TotalRevenue WHERE Email = @Email";
                SqlCommand updateFundsCmd = new SqlCommand(updateFundsQuery, connection, transaction);
                updateFundsCmd.Parameters.AddWithValue("@TotalRevenue", totalRevenue);
                updateFundsCmd.Parameters.AddWithValue("@Email", Global.loggedInEmail);
                updateFundsCmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Inserts a new transaction record into the "Transactions" table for a buy or sell operation.
        /// </summary>
        private void InsertTransactionDetails(string company, int sharesToSell, double currentSharePrice, string type)
        {
            try
            {
                string insertTransactionQuery = @"
                    INSERT INTO Transactions (DateTime, CompanyName, ShareCount, LTP, Email, Type) 
                    VALUES (@DateTime, @CompanyName, @ShareCount, @LTP, @Email, @Type)";

                using (SqlConnection connection = new SqlConnection(Global.connectionString))
                {
                    SqlCommand insertTransactionCmd = new SqlCommand(insertTransactionQuery, connection);
                    connection.Open();
                    insertTransactionCmd.Parameters.AddWithValue("@DateTime", DateTime.Now);
                    insertTransactionCmd.Parameters.AddWithValue("@CompanyName", company);
                    insertTransactionCmd.Parameters.AddWithValue("@ShareCount", sharesToSell);
                    insertTransactionCmd.Parameters.AddWithValue("@LTP", currentSharePrice);
                    insertTransactionCmd.Parameters.AddWithValue("@Email", Global.loggedInEmail);
                    insertTransactionCmd.Parameters.AddWithValue("@Type", type);
                    insertTransactionCmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Retrieves and displays the logged-in user's current funds and sets share count details.
        /// </summary>
        public void GetFundsDetailBasedOnEmail(string company, Dictionary<string, double> companies)
        {
            try
            {
                string selectFundsQuery = "SELECT Funds FROM Login WHERE Email = @Email";
                using (SqlConnection connection = new SqlConnection(Global.connectionString))
                {
                    SqlCommand selectFundsCmd = new SqlCommand(selectFundsQuery, connection);
                    selectFundsCmd.Parameters.AddWithValue("@Email", Global.loggedInEmail);
                    try
                    {
                        connection.Open();

                        // Check user funds
                        using (SqlDataReader reader = selectFundsCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int funds = Convert.ToInt32(reader["Funds"]);
                                Console.WriteLine($"Your Current Balance is :{funds:F2}");
                                reader.Close();
                            }
                        }
                    }
                    catch (Exception e) { Console.WriteLine(e.Message); }

                    SetSharesCount(company, companies, connection, selectFundsCmd);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Processes share purchase based on user input, available funds, and existing holdings.
        /// Updates funds, modifies holdings, and records the transaction.
        /// </summary>
        private void SetSharesCount(string company, Dictionary<string, double> companies, SqlConnection connection, SqlCommand selectFundsCmd)
        {
            try
            {
                Console.Write("Enter number of shares to buy: ");
                if (int.TryParse(Console.ReadLine(), out int shares) && shares > 0)
                {
                    double sharePrice = companies[company.ToLower()];
                    double totalCost = shares * sharePrice;

                    string selectHoldingsQuery = @"
                            SELECT ISNULL(ShareCount, 0) AS TotalShares, ISNULL(AveragePrice, 0) AS AveragePrice 
                            FROM UserHoldings 
                            WHERE CompanyName = @CompanyName AND Email = @Email";


                    SqlCommand selectHoldingsCmd = new SqlCommand(selectHoldingsQuery, connection);
                    selectHoldingsCmd.Parameters.AddWithValue("@CompanyName", company);
                    selectHoldingsCmd.Parameters.AddWithValue("@Email", Global.loggedInEmail);

                    try
                    {
                        using (SqlDataReader reader = selectFundsCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int funds = Convert.ToInt32(reader["Funds"]);
                                reader.Close();

                                if (totalCost <= funds)
                                {
                                    UpdateFundsBasedOnBuy(connection, totalCost);

                                    using (SqlDataReader holdingsReader = selectHoldingsCmd.ExecuteReader())
                                    {
                                        if (holdingsReader.Read())
                                        {
                                            int existingShares = Convert.ToInt32(holdingsReader["TotalShares"]);
                                            double existingAveragePrice = Convert.ToDouble(holdingsReader["AveragePrice"]);
                                            holdingsReader.Close();

                                            double newAveragePrice = ((existingShares * existingAveragePrice) + totalCost) / (existingShares + shares);
                                            UpdateHoldingsBasedOnBuyShares(company, connection, shares, newAveragePrice);
                                        }
                                        else
                                        {
                                            holdingsReader.Close();
                                            // Insert new holdings record
                                            InsertUserHoldings(company, shares, sharePrice);

                                        }
                                    }
                                    // Record the transaction
                                    InsertTransactionDetails(company, shares, sharePrice, "Buy");
                                    Console.WriteLine($"You successfully bought {shares} shares of {company}.");
                                }
                                else
                                {
                                    Console.WriteLine("Insufficient funds. Please top-up money.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("User not found. Please check your login.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }

                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a positive number of shares.");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Updates the user's holdings by adding purchased shares and updating the average price.
        /// </summary>
        private void UpdateHoldingsBasedOnBuyShares(string company, SqlConnection connection, int shares, double newAveragePrice)
        {
            try
            {
                string updateHoldingsQuery = @"
                            UPDATE UserHoldings 
                            SET ShareCount = ShareCount + @ShareCount, 
                                AveragePrice = @NewAveragePrice 
                            WHERE CompanyName = @CompanyName AND Email = @Email";
                SqlCommand updateHoldingsCmd = new SqlCommand(updateHoldingsQuery, connection);
                updateHoldingsCmd.Parameters.AddWithValue("@CompanyName", company);
                updateHoldingsCmd.Parameters.AddWithValue("@ShareCount", shares);
                updateHoldingsCmd.Parameters.AddWithValue("@NewAveragePrice", newAveragePrice);
                updateHoldingsCmd.Parameters.AddWithValue("@Email", Global.loggedInEmail);
                updateHoldingsCmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Updates the user's funds by deducting the total cost of the share purchase.
        /// </summary>
        private void UpdateFundsBasedOnBuy(SqlConnection connection, double totalCost)
        {
            try
            {
                string updateFundsQuery = "UPDATE Login SET Funds = Funds - @TotalCost WHERE Email = @Email";
                SqlCommand updateFundsCmd = new SqlCommand(updateFundsQuery, connection);
                updateFundsCmd.Parameters.AddWithValue("@Email", Global.loggedInEmail);
                updateFundsCmd.Parameters.AddWithValue("@TotalCost", totalCost);
                updateFundsCmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Inserts a new record of user holdings for the specified company, shares, and average price.
        /// </summary>
        private void InsertUserHoldings(string company, int shares, double sharePrice)
        {
            try
            {
                string insertHoldingsQuery = @"
                            INSERT INTO UserHoldings (Email, CompanyName, ShareCount, AveragePrice) 
                            VALUES (@Email, @CompanyName, @ShareCount, @AveragePrice)";
                using (SqlConnection connection = new SqlConnection(Global.connectionString))
                {
                    SqlCommand insertTransactionCmd = new SqlCommand(insertHoldingsQuery, connection);
                    connection.Open();
                    SqlCommand insertHoldingsCmd = new SqlCommand(insertHoldingsQuery, connection);
                    insertHoldingsCmd.Parameters.AddWithValue("@Email", Global.loggedInEmail);
                    insertHoldingsCmd.Parameters.AddWithValue("@CompanyName", company);
                    insertHoldingsCmd.Parameters.AddWithValue("@ShareCount", shares);
                    insertHoldingsCmd.Parameters.AddWithValue("@AveragePrice", sharePrice);
                    insertHoldingsCmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Fetches and displays the user's profit or loss statement based on their holdings and current market prices.
        /// </summary>
        public void GetUserHoldingData(Dictionary<string, double> companies)
        {
            try
            {
                string selectUserHoldingsQuery = @"
                    SELECT CompanyName, ShareCount, AveragePrice 
                    FROM UserHoldings 
                    WHERE Email = @Email";

                using (SqlConnection connection = new SqlConnection(Global.connectionString))
                {
                    SqlCommand selectUserHoldingsCmd = new SqlCommand(selectUserHoldingsQuery, connection);
                    selectUserHoldingsCmd.Parameters.AddWithValue("@Email", Global.loggedInEmail);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = selectUserHoldingsCmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                Console.WriteLine("\n--- Profit/Loss Statement ---");
                                Console.WriteLine($"{"Company",-20} {"Shares",-10} {"Avg Price",-12} {"Current Price",-15} {"Profit/Loss"}");

                                while (reader.Read())
                                {
                                    string companyName = reader["CompanyName"].ToString();
                                    int shareCount = Convert.ToInt32(reader["ShareCount"]);
                                    double averagePrice = Convert.ToDouble(reader["AveragePrice"]);

                                    // Fetch the current price of the company from the market (_companies dictionary)
                                    if (companies.TryGetValue(companyName.ToLower(), out double currentPrice))
                                    {
                                        // Calculate profit or loss
                                        double totalCost = shareCount * averagePrice;
                                        double currentValue = shareCount * currentPrice;
                                        double profitLoss = currentValue - totalCost;

                                        if (profitLoss > 0)
                                        {
                                            Console.BackgroundColor = ConsoleColor.Green;
                                            Console.ForegroundColor = ConsoleColor.White;
                                        }
                                        else if (profitLoss < 0)
                                        {
                                            Console.BackgroundColor = ConsoleColor.Red;
                                            Console.ForegroundColor = ConsoleColor.White;
                                        }
                                        
                                        Console.WriteLine($"{companyName,-20} {shareCount,-10} {averagePrice,-12:0.00} {currentPrice,-15:0.00} {profitLoss:+0.00;-0.00}");
                                        Console.ResetColor();

                                    }
                                    else
                                    {
                                        Console.WriteLine($"Market price for {companyName} not available.");
                                    }
                                }
                                Console.WriteLine("\n--- End of Statement ---");
                            }
                            else
                            {
                                Console.WriteLine("You do not have any holdings.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred while fetching your Profit/Loss Statement: {ex.Message}");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Fetches and displays the user's transaction history, including details like date, company, share count, price, and transaction type.
        /// </summary>
        public void GetUserTransactions()
        {
            try
            {
                string selectTransactionLogsQuery = @"
                    SELECT DateTime, CompanyName, ShareCount, LTP, Type
                    FROM Transactions
                    WHERE Email = @Email
                    ORDER BY DateTime DESC";

                using (SqlConnection connection = new SqlConnection(Global.connectionString))
                {
                    SqlCommand selectTransactionLogsCmd = new SqlCommand(selectTransactionLogsQuery, connection);
                    selectTransactionLogsCmd.Parameters.AddWithValue("@Email", Global.loggedInEmail);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = selectTransactionLogsCmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                Console.WriteLine("\n--- Transaction Logs ---");
                                Console.WriteLine($"{"Date",-20} {"Company",-20} {"Shares",-10} {"LTP",-10} {"Type",-10}");

                                while (reader.Read())
                                {
                                    string dateTime = Convert.ToDateTime(reader["DateTime"]).ToString("yyyy-MM-dd HH:mm:ss");
                                    string companyName = reader["CompanyName"].ToString();
                                    int shareCount = Convert.ToInt32(reader["ShareCount"]);
                                    double ltp = Convert.ToDouble(reader["LTP"]);
                                    string type = reader["Type"].ToString();

                                    // Display the transaction details
                                    Console.WriteLine($"{dateTime,-20} {companyName,-20} {shareCount,-10} {ltp,-10:0.00} {type,-10}");
                                }
                                Console.WriteLine("\n--- End of Logs ---");
                            }
                            else
                            {
                                Console.WriteLine("No transaction history found.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred while fetching transaction logs: {ex.Message}");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Verifies the password for a given email by querying the Login table.
        /// </summary>
        public string ProcessLoginBasedOnPassword(string email, string password)
        {
            try
            {
                string selectQuery = "SELECT Password FROM Login WHERE Email=@Email";
                using (SqlConnection connection = new SqlConnection(Global.connectionString))
                {
                    SqlCommand cmd = new SqlCommand(selectQuery, connection);
                    cmd.Parameters.AddWithValue("@Email", email);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            return reader["Password"].ToString();
                        }
                        else
                        {
                            return "No account found with this email.";
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return "No account found with this email.";
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
