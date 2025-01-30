using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using StockExchange.BusinessLayer;
using StockExchange.DataAccessLayer;

namespace StockExchange
{
    public class LoginPage
    {
        StockTransaction stock=new StockTransaction();
        public string Email;
        private ExchangeRepository _exchangeRepository;
        private DataBridge _dataBridge;

        public LoginPage()
        {
            _exchangeRepository = new ExchangeRepository();
            _dataBridge = new DataBridge(_exchangeRepository);
        }
        // Initialize table
        public void InitializeAllTables()
        {
            _dataBridge.CreateLoginTable();
            _dataBridge.CreateTransactionTable();
            _dataBridge.CreateTransactionTable();
        }

        // Register a new user
        public void RegisterUser()
        {
            try
            {
                string userName, password, dob, phoneNumber, email;

                // Validate Name
                do
                {
                    Console.WriteLine("Enter Name (Only alphabets, max 100 characters):");
                    userName = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(userName) || !Regex.IsMatch(userName, @"^[A-Za-z\s]{1,100}$"));

                // Validate Password
                do
                {
                    Console.WriteLine("Enter Password (Minimum 6 characters):");
                    password = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(password) || password.Length < 6);

                // Validate Date of Birth
                do
                {
                    Console.WriteLine("Enter Date of Birth (YYYY-MM-DD):");
                    dob = Console.ReadLine();
                } while (!DateTime.TryParse(dob, out _));

                // Validate Phone Number
                do
                {
                    Console.WriteLine("Enter Phone Number (10 digits):");
                    phoneNumber = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(phoneNumber) || !Regex.IsMatch(phoneNumber, @"^\d{10}$"));

                // Validate Email
                do
                {
                    Console.WriteLine("Enter Email (valid format):");
                    email = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"));

                // Check if email already exists
                string searchEmail = "SELECT COUNT(*) FROM Login WHERE Email=@Email";
                using (SqlConnection connection = new SqlConnection(Global.connectionString))
                {
                    SqlCommand cmd = new SqlCommand(searchEmail, connection);
                    cmd.Parameters.AddWithValue("@Email", email);
                    try
                    {
                        connection.Open();
                        int emailCount = (int)cmd.ExecuteScalar();
                        if (emailCount > 0)
                        {
                            Console.WriteLine("An account with this email already exists. Please choose the login option.");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return;
                    }
                }

                // Hash the password
                string hashedPassword = HashPassword(password);

                // Insert new user into the database
                _dataBridge.SaveUserRegistrationDetails(userName, dob, phoneNumber, email, hashedPassword);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

        

        // Login an existing user
        public bool LoginUser()
        {
            try
            {
                string email, password;

                // Validate Email
                do
                {
                    Console.WriteLine("Enter Email:");
                    email = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"));

                // Validate Password
                do
                {
                    Console.WriteLine("Enter Password:");
                    password = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(password));


                string storedHashedPassword = _dataBridge.ProcessLoginBasedOnPassword(email, password);
                // Verify the entered password against the stored hash
                if (VerifyPassword(password, storedHashedPassword))
                {
                    Console.WriteLine("Login Successful.");
                    Global.loggedInEmail = email;
                    stock.CompaniesInitialization();
                    stock.StockExchangeDashboard();
                    return true;
                }
                else
                {
                    Console.WriteLine("Invalid password. Please try again.");
                    return false;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }
        }

        

        // Hash a password using SHA256
        private string HashPassword(string password)
        {
            try
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    StringBuilder builder = new StringBuilder();
                    foreach (byte b in bytes)
                    {
                        builder.Append(b.ToString("x2")); // Convert to hexadecimal
                    }
                    return builder.ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Verify a password against a stored hash
        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            try
            {
                string enteredHash = HashPassword(enteredPassword);
                return enteredHash == storedHash;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
