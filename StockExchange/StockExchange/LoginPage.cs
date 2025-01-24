using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace StockExchange
{
    public class LoginPage
    {
        StockTransaction stock=new StockTransaction();
        private string connectionString = "Data Source=ROHITH\\SQLEXPRESS;Initial Catalog=StockExchangeDB;User ID=sa;Password=data@1234;Encrypt=False";
        public string Email;
        // Initialize table
        public void InitializeLoginPage()
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

        // Register a new user
        public void RegisterUser()
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
            using (SqlConnection connection = new SqlConnection(connectionString))
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
            string insertQuery = @"INSERT INTO Login (Name, Password, Dob, PhoneNumber, Email) 
                                   VALUES (@Name, @Password, @Dob, @PhoneNumber, @Email)";
            using (SqlConnection connection = new SqlConnection(connectionString))
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
        // Login an existing user
        public bool LoginUser()
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

            // Retrieve stored hash from the database
            string selectQuery = "SELECT Password FROM Login WHERE Email=@Email";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(selectQuery, connection);
                cmd.Parameters.AddWithValue("@Email", email);

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string storedHashedPassword = reader["Password"].ToString();

                        // Verify the entered password against the stored hash
                        if (VerifyPassword(password, storedHashedPassword))
                        {
                            Console.WriteLine("Login Successful.");
                            Global.loggedInEmail = email;
                            stock.StockExchangeDashboard();
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid password. Please try again.");
                            return false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No account found with this email.");
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }
        // Hash a password using SHA256
        private string HashPassword(string password)
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
        // Verify a password against a stored hash
        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            string enteredHash = HashPassword(enteredPassword);
            return enteredHash == storedHash;
        }
    }
}
