using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace StockExchange
{
    public class StockTransaction
    {
        // Dictionary to store company names and their respective prices
        Dictionary<string, double> _companies = new Dictionary<string, double>();
        private Random _random = new Random();
        private System.Timers.Timer _timer;
        private string connectionString = Global.connectionString;

        public void InitializeTransactionTable()
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


            using (SqlConnection connection = new SqlConnection(connectionString))
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
        public void InitializeUserHoldingsTable()
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

            using (SqlConnection connection = new SqlConnection(connectionString))
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


        public void StockExchangeDashboard()
        {
            try
            {
                // Initialize companies and start the timer for dynamic price changes
                CompaniesInitialization();
                StartPriceUpdater();

                bool exit = false;
                while (!exit)
                {
                    Console.WriteLine("\nStock Exchange Menu");
                    Console.WriteLine("1. Add Funds\n2. View Companies & Prices\n3. Buy Shares\n4. Sell Shares\n5. Profit/Loss Statement\n6. Transaction Logs\n7. Logout");
                    Console.Write("Enter your choice: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1": AddFunds(); break;
                        case "2": ViewCompanies(); break; // Only display prices
                        case "3": BuyShares(); break;
                        case "4":SellShares();break;
                        case "5": ProfitLossStatement(); break;
                        case "6": TransactionLogs(); break;
                        case "7":
                            Console.WriteLine("Logging out...");
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid input! Please choose a valid option.");
                            break;
                    }
                }

                // Stop the timer when exiting the application
                StopPriceUpdater();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void CompaniesInitialization()
        {
            try
            {
                // Initialize companies with default prices
                _companies.Clear();
                _companies.Add("Sensex".ToLower(), 1000.00);
                _companies.Add("Nifty".ToLower(), 1500.00);
                _companies.Add("ICICI Bank".ToLower(), 1100.00);
                _companies.Add("Axis Bank".ToLower(), 5000.00);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void StartPriceUpdater()
        {
            // Set up a timer to update company prices every 2 seconds
            _timer = new System.Timers.Timer(2000);
            _timer.Elapsed += UpdatePrices;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void StopPriceUpdater()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
            }
        }

        private void UpdatePrices(object sender, ElapsedEventArgs e)
        {
            // Update prices dynamically
            foreach (var company in _companies.Keys.ToList())
            {
                if (company == "sensex" || company == "nifty")
                {
                    // Slight random price change for Sensex and Nifty
                    _companies[company] += (_random.NextDouble() < 0.5 ? -0.20 : 0.20);
                }
                else
                {
                    // Random price change for other companies
                    _companies[company] += (_random.NextDouble() < 0.5 ? -1.00 : 1.00);
                }

                _companies[company] = Math.Max(0, _companies[company]); // Ensure no negative prices
            }
        }

        private void AddFunds()
        {
            try
            {
                Console.Write("Enter the amount to add to your account: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
                {
                    string updateFundsQuery = "UPDATE Login SET Funds = Funds + @Amount WHERE Email = @Email";

                    using (SqlConnection connection = new SqlConnection(connectionString))
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
                else
                {
                    Console.WriteLine("Invalid amount. Please enter a positive value.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void ViewCompanies()
        {
            try
            {
                Console.WriteLine("\nCompany List:");
                int i = 1;
                foreach (KeyValuePair<string, double> item in _companies)
                {
                    Console.WriteLine($"{i++}. Company Name: {item.Key}, Price: {item.Value:F2}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void BuyShares()
        {
            try
            {
                Console.WriteLine("Enter company name: ");
                string company = Console.ReadLine();
                if (!_companies.ContainsKey(company.ToLower()))
                {
                    Console.WriteLine($"The given company, {company}, is not available in the market.");
                    return;
                }
                string selectFundsQuery = "SELECT Funds FROM Login WHERE Email = @Email";
                using (SqlConnection connection = new SqlConnection(connectionString))
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
                    
                    Console.Write("Enter number of shares to buy: ");
                    if (int.TryParse(Console.ReadLine(), out int shares) && shares > 0)
                    {
                        double sharePrice = _companies[company.ToLower()];
                        double totalCost = shares * sharePrice;

                        string updateFundsQuery = "UPDATE Login SET Funds = Funds - @TotalCost WHERE Email = @Email";
                        string selectHoldingsQuery = @"
                            SELECT ISNULL(ShareCount, 0) AS TotalShares, ISNULL(AveragePrice, 0) AS AveragePrice 
                            FROM UserHoldings 
                            WHERE CompanyName = @CompanyName AND Email = @Email";
                        string updateHoldingsQuery = @"
                            UPDATE UserHoldings 
                            SET ShareCount = ShareCount + @ShareCount, 
                                AveragePrice = @NewAveragePrice 
                            WHERE CompanyName = @CompanyName AND Email = @Email";
                        string insertHoldingsQuery = @"
                            INSERT INTO UserHoldings (Email, CompanyName, ShareCount, AveragePrice) 
                            VALUES (@Email, @CompanyName, @ShareCount, @AveragePrice)";
                        string insertTransactionQuery = @"
                            INSERT INTO Transactions (DateTime, CompanyName, ShareCount, LTP, Email, Type) 
                            VALUES (@DateTime, @CompanyName, @ShareCount, @LTP, @Email, @Type)";

                        SqlCommand updateFundsCmd = new SqlCommand(updateFundsQuery, connection);
                        updateFundsCmd.Parameters.AddWithValue("@Email", Global.loggedInEmail);
                        updateFundsCmd.Parameters.AddWithValue("@TotalCost", totalCost);

                        SqlCommand selectHoldingsCmd = new SqlCommand(selectHoldingsQuery, connection);
                        selectHoldingsCmd.Parameters.AddWithValue("@CompanyName", company);
                        selectHoldingsCmd.Parameters.AddWithValue("@Email", Global.loggedInEmail);

                        SqlCommand updateHoldingsCmd = new SqlCommand(updateHoldingsQuery, connection);
                        SqlCommand insertHoldingsCmd = new SqlCommand(insertHoldingsQuery, connection);

                        SqlCommand insertTransactionCmd = new SqlCommand(insertTransactionQuery, connection);
                        insertTransactionCmd.Parameters.AddWithValue("@DateTime", DateTime.Now);
                        insertTransactionCmd.Parameters.AddWithValue("@CompanyName", company);
                        insertTransactionCmd.Parameters.AddWithValue("@ShareCount", shares);
                        insertTransactionCmd.Parameters.AddWithValue("@LTP", sharePrice);
                        insertTransactionCmd.Parameters.AddWithValue("@Email", Global.loggedInEmail);
                        insertTransactionCmd.Parameters.AddWithValue("@Type", "Buy");

                        try
                        {
                            // Check user funds
                            using (SqlDataReader reader = selectFundsCmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    int funds = Convert.ToInt32(reader["Funds"]);
                                    reader.Close();

                                    if (totalCost <= funds)
                                    {
                                        // Update user funds
                                        updateFundsCmd.ExecuteNonQuery();

                                        // Check if user already holds shares of the company
                                        using (SqlDataReader holdingsReader = selectHoldingsCmd.ExecuteReader())
                                        {
                                            if (holdingsReader.Read())
                                            {
                                                int existingShares = Convert.ToInt32(holdingsReader["TotalShares"]);
                                                double existingAveragePrice = Convert.ToDouble(holdingsReader["AveragePrice"]);
                                                holdingsReader.Close();

                                                // Calculate the new average price
                                                double newAveragePrice = ((existingShares * existingAveragePrice) + totalCost) / (existingShares + shares);

                                                // Add @CompanyName parameter
                                                updateHoldingsCmd.Parameters.AddWithValue("@CompanyName", company);

                                                // Add @ShareCount and @NewAveragePrice parameters
                                                updateHoldingsCmd.Parameters.AddWithValue("@ShareCount", shares);
                                                updateHoldingsCmd.Parameters.AddWithValue("@NewAveragePrice", newAveragePrice);

                                                // Execute the command
                                                updateHoldingsCmd.ExecuteNonQuery();

                                            }
                                            else
                                            {
                                                holdingsReader.Close();

                                                // Insert new holdings record
                                                insertHoldingsCmd.Parameters.AddWithValue("@Email", Global.loggedInEmail);
                                                insertHoldingsCmd.Parameters.AddWithValue("@CompanyName", company);
                                                insertHoldingsCmd.Parameters.AddWithValue("@ShareCount", shares);
                                                insertHoldingsCmd.Parameters.AddWithValue("@AveragePrice", sharePrice);
                                                insertHoldingsCmd.ExecuteNonQuery();
                                            }
                                        }

                                        // Record the transaction
                                        insertTransactionCmd.ExecuteNonQuery();
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
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }

        private void SellShares()
        {
            try
            {
                Console.WriteLine("Your current holdings:");

                string selectUserHoldingsQuery = @"
                    SELECT CompanyName, ShareCount, AveragePrice 
                    FROM UserHoldings 
                    WHERE Email = @Email";

                using (SqlConnection connection = new SqlConnection(connectionString))
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

                Console.WriteLine("\nEnter company name to sell shares from:");
                string company = Console.ReadLine().ToLower();

                string checkHoldingsQuery = @"
                    SELECT ShareCount, AveragePrice 
                    FROM UserHoldings 
                    WHERE CompanyName = @CompanyName AND Email = @Email";
                string insertTransactionQuery = @"
                    INSERT INTO Transactions (DateTime, CompanyName, ShareCount, LTP, Email, Type) 
                    VALUES (@DateTime, @CompanyName, @ShareCount, @LTP, @Email, @Type)";
                using (SqlConnection connection = new SqlConnection(connectionString))
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
                                    if (!_companies.ContainsKey(company))
                                    {
                                        Console.WriteLine("The given company is not available in the market.");
                                        return;
                                    }

                                    double currentSharePrice = _companies[company];
                                    double totalRevenue = sharesToSell * currentSharePrice;

                                    string updateFundsQuery = "UPDATE Login SET Funds = Funds + @TotalRevenue WHERE Email = @Email";
                                    string updateHoldingsQuery = @"
                                        UPDATE UserHoldings 
                                        SET ShareCount = ShareCount - @ShareCount 
                                        WHERE CompanyName = @CompanyName AND Email = @Email";

                                    string deleteHoldingsQuery = @"
                                        DELETE FROM UserHoldings 
                                        WHERE ShareCount = 0 AND CompanyName = @CompanyName AND Email = @Email";

                                    using (SqlTransaction transaction = connection.BeginTransaction())
                                    {
                                        try
                                        {
                                            SqlCommand updateFundsCmd = new SqlCommand(updateFundsQuery, connection, transaction);
                                            updateFundsCmd.Parameters.AddWithValue("@TotalRevenue", totalRevenue);
                                            updateFundsCmd.Parameters.AddWithValue("@Email", Global.loggedInEmail);
                                            updateFundsCmd.ExecuteNonQuery();

                                            SqlCommand updateHoldingsCmd = new SqlCommand(updateHoldingsQuery, connection, transaction);
                                            updateHoldingsCmd.Parameters.AddWithValue("@ShareCount", sharesToSell);
                                            updateHoldingsCmd.Parameters.AddWithValue("@CompanyName", company); // Added parameter for company
                                            updateHoldingsCmd.Parameters.AddWithValue("@Email", Global.loggedInEmail);
                                            updateHoldingsCmd.ExecuteNonQuery();

                                            SqlCommand deleteHoldingsCmd = new SqlCommand(deleteHoldingsQuery, connection, transaction);
                                            deleteHoldingsCmd.Parameters.AddWithValue("@CompanyName", company); // Added parameter for company
                                            deleteHoldingsCmd.Parameters.AddWithValue("@Email", Global.loggedInEmail);
                                            deleteHoldingsCmd.ExecuteNonQuery();

                                            SqlCommand insertTransactionCmd = new SqlCommand(insertTransactionQuery, connection, transaction);
                                            insertTransactionCmd.Parameters.AddWithValue("@DateTime", DateTime.Now);
                                            insertTransactionCmd.Parameters.AddWithValue("@CompanyName", company); // Added parameter for company
                                            insertTransactionCmd.Parameters.AddWithValue("@ShareCount", sharesToSell);
                                            insertTransactionCmd.Parameters.AddWithValue("@LTP", currentSharePrice);
                                            insertTransactionCmd.Parameters.AddWithValue("@Email", Global.loggedInEmail);
                                            insertTransactionCmd.Parameters.AddWithValue("@Type", "Sell");
                                            insertTransactionCmd.ExecuteNonQuery();
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
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }

        private void ProfitLossStatement()
        {
            try
            {
                Console.WriteLine("Fetching your Profit/Loss Statement...");

                // Query to fetch the user's holdings
                string selectUserHoldingsQuery = @"
                    SELECT CompanyName, ShareCount, AveragePrice 
                    FROM UserHoldings 
                    WHERE Email = @Email";

                using (SqlConnection connection = new SqlConnection(connectionString))
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
                                    if (_companies.TryGetValue(companyName.ToLower(), out double currentPrice))
                                    {
                                        // Calculate profit or loss
                                        double totalCost = shareCount * averagePrice;
                                        double currentValue = shareCount * currentPrice;
                                        double profitLoss = currentValue - totalCost;

                                        // Display the statement
                                        Console.WriteLine($"{companyName,-20} {shareCount,-10} {averagePrice,-12:0.00} {currentPrice,-15:0.00} {profitLoss:+0.00;-0.00}");
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
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }
        private void TransactionLogs()
        {
            try
            {
                Console.WriteLine("\nFetching your Transaction Logs...");

                // Query to fetch the user's transaction history
                string selectTransactionLogsQuery = @"
                    SELECT DateTime, CompanyName, ShareCount, LTP, Type
                    FROM Transactions
                    WHERE Email = @Email
                    ORDER BY DateTime DESC";

                using (SqlConnection connection = new SqlConnection(connectionString))
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
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }

    }
}
