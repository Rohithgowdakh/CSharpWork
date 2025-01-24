using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange
{
    public class StockTransaction
    {
        Dictionary<string, double> _companies=new Dictionary<string, double>();
        private string connectionString = "Data Source=ROHITH\\SQLEXPRESS;Initial Catalog=StockExchangeDB;User ID=sa;Password=data@1234;Encrypt=False";
        public void CompaniesInitialization()
        {
            _companies.Add("Sensex".ToLower(), 1000.00);
            _companies.Add("Nifty", 1500.00);
            _companies.Add("ICICI Bank", 1100.00);
            _companies.Add("Axis Bank", 5000.00);
        }
        public void StockExchangeDashboard()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nStock Exchange Menu");
                Console.WriteLine("1. Add Funds\n2. View Companies & Prices\n3. Buy Shares\n4. Sell Shares\n5. Profit/Loss Statement\n6. Transaction Logs\n7. Logout");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":AddFunds();break;
                    case "2":CompaniesInitialization();
                             ViewCompanies();break;
                    case "3":BuyShares();break;
                    //case "4":SellShares();break;
                    //case "5":ProfitLossStatement();break;
                    //case "6":TransactionLogs();break;
                    case "7":Console.WriteLine("Logging out...");
                             exit = true;

                             break;
                    default:Console.WriteLine("Invalid input! Please choose a valid option.");
                            break;
                }
            }
        }
        private void AddFunds()
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
        private void ViewCompanies()
        {
            Console.WriteLine("Company List:");
            int i = 1;
            foreach (KeyValuePair<string,double> item in _companies)
            {
                Console.WriteLine($"{i++}. Company Name :{item.Key} Prize : {item.Value:F2}");
            }
        }
        private void BuyShares()
        {
            Console.WriteLine("Enter company name: ");
            string company = Console.ReadLine();
            bool companyExists = false;

            foreach (KeyValuePair<string, double> item in _companies)
            {
                if (company == item.Key)
                {
                    companyExists = true;
                    break;
                }
            }

            if (!companyExists)
            {
                Console.WriteLine($"The given company, {company}, is not available in the market.");return;
            }
            Console.Write("Enter number of shares to buy: ");
            if (int.TryParse(Console.ReadLine(), out int shares) && shares > 0)
            {
                double sharePrize = shares * _companies[company];
                string selectFundsQuery = "SELECT Funds FROM Login WHERE Email = @Email";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(selectFundsQuery, connection);
                    cmd.Parameters.AddWithValue("@Email", Global.loggedInEmail);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            int funds =Convert.ToInt32( reader["Funds"]);
                            if (sharePrize > funds)
                            {
                                Console.WriteLine($"You successfully bought {shares} shares of {company}.");
                            }
                            else
                            {
                                Console.WriteLine("Insufficient funds, Please top-up money");
                            }
                            
                        }

                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message ); }    
                }
            
            }
            else
            {
                Console.WriteLine("Invalid input! Please enter a positive number of shares.");
            }
        }


    }

}

