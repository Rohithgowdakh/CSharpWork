using Microsoft.Data.SqlClient;
using StockExchange.BusinessLayer;
using StockExchange.DataAccessLayer;
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
        private ExchangeRepository _exchangeRepository;
        private DataBridge _dataBridge;

        public StockTransaction()
        {
            _exchangeRepository = new ExchangeRepository();
            _dataBridge = new DataBridge(_exchangeRepository);
        }

        DateTime currentTime = DateTime.Now;
        TimeSpan marketOpen = new TimeSpan(9, 15, 0); 
        TimeSpan marketClose = new TimeSpan(18, 15, 0);

        /// <summary>
        /// Validates if the current time is within market hours (9:15 AM to 6:15 PM).
        /// </summary>
        public bool TimeValidation()
        {
            try
            {
                bool marketTime = true;
                if (marketTime)
                {
                    if (currentTime.TimeOfDay < marketOpen || currentTime.TimeOfDay > marketClose)
                    {
                        Console.WriteLine("The market is closed. You can only access this application between 9:15 AM and 6:15 PM.");
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Manages stock exchange dashboard with options to add funds, view companies, buy/sell shares, view profit/loss statement, and transaction logs, with dynamic price updates.
        /// </summary>
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
                        case "2": ViewCompanies(); break; 
                        case "3": BuyShares(); break;
                        case "4": SellShares();break;
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

        /// <summary>
        /// Initializes the companies with default prices and adds them to the _companies dictionary.
        /// </summary>
        public void CompaniesInitialization()
        {
            try
            {
                // Initialize companies with default prices
                _companies.Clear();
                _companies.Add("Sensex".ToLower(), 75000.00);
                _companies.Add("Nifty".ToLower(), 22000.00);
                _companies.Add("ICICI Bank".ToLower(), 1246.00);
                _companies.Add("Axis Bank".ToLower(), 950.00);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Starts a timer to update company prices every 2 seconds.
        /// </summary>
        private void StartPriceUpdater()
        {
            try
            {
                _timer = new System.Timers.Timer(2000);
                _timer.Elapsed += UpdatePrices;
                _timer.AutoReset = true;
                _timer.Enabled = true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Stops and disposes of the timer responsible for updating company prices.
        /// </summary>
        private void StopPriceUpdater()
        {
            try
            {
                if (_timer != null)
                {
                    _timer.Stop();
                    _timer.Dispose();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Updates the company prices every time the timer elapses. 
        /// </summary>
        private void UpdatePrices(object sender, ElapsedEventArgs e)
        {
            // Update prices dynamically
            try
            {
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
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Asks the user to enter an amount to add to their account and adds it if the amount is valid.
        /// </summary>
        private void AddFunds()
        {
            try
            {
                Console.Write("Enter the amount to add to your account: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
                {
                   _dataBridge.AddFunds(amount);
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

        /// <summary>
        /// Displays the list of companies along with their current prices.
        /// </summary>
        private void ViewCompanies()
        {
            try
            {
                if(TimeValidation())
                {
                    Console.WriteLine("\nCompany List:");
                    int i = 1;
                    foreach (KeyValuePair<string, double> item in _companies)
                    {
                        Console.WriteLine($"{i++}. Company Name: {item.Key}, Price: {item.Value:F2}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Validates market time, checks company existence, and retrieves funds to facilitate share purchase.
        /// </summary>
        private void BuyShares()
        {
            try
            {
                if (TimeValidation())
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
                        _dataBridge.GetFundsDetailBasedOnEmail(company, _companies);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"An error occurred: {e.Message}");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Validates market time, retrieves user's holdings, and processes share sell transaction for the given company.
        /// </summary>
        private void SellShares()
        {
            try
            {
                if (TimeValidation())
                {
                    try
                    {
                        Console.WriteLine("Your current holdings:");

                        _dataBridge.GetHoldingsBasedOnEmail();

                        Console.WriteLine("\nEnter company name to sell shares from:");
                        string company = Console.ReadLine().ToLower();

                        _dataBridge.ProcessSellShareTransaction(company, _companies);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"An error occurred: {e.Message}");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Fetches and displays the user's Profit/Loss statement based on their holdings and market prices.
        /// </summary>
        private void ProfitLossStatement()
        {
            try
            {
                Console.WriteLine("Fetching your Profit/Loss Statement...");
                _dataBridge.GetUserHoldingData(_companies);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }

        /// <summary>
        /// Fetches and displays the user's transaction logs from the database.
        /// </summary>
        private void TransactionLogs()
        {
            try
            {
                Console.WriteLine("\nFetching your Transaction Logs...");
                _dataBridge.GetUserTransactions();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }

        
    }
}
