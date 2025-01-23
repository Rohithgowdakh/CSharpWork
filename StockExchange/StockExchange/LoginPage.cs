using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
namespace StockExchange
{
    public class LoginPage
    {
        private string connectionString = "Data Source=ROHITH\\SQLEXPRESS;Initial Catalog=StockExchangeDB;User ID=sa;Password=data@1234;Encrypt=False";
        public void InitializeLoginPage()
        {
            string createTableQuery = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Login' AND xtype='U')
            CREATE TABLE Login (
                Id INT IDENTITY(1,1) PRIMARY KEY,
                Name NVARCHAR(100),
                Password NVARCHAR(100),
                Dob DATE,
                PhoneNumber NVARCHAR(15),
                Email NVARCHAR(100)
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
                    Console.WriteLine($"Error : {ex.Message}");
                }
            }
        }
        public void RegisterUser()
        {
            Console.WriteLine("Enter Name :");
            string userName = Console.ReadLine();
            Console.WriteLine("Enter Password :");
            string password = Console.ReadLine();
            Console.WriteLine("Enter Date of Birth (YYYY-MM-DD):");
            string dob = Console.ReadLine();
            Console.WriteLine("Enter Phone Number :");
            string phoneNumber = Console.ReadLine();
            Console.WriteLine("Enter Email :");
            string email = Console.ReadLine();

            string insertQuery = $"INSERT INTO Login (Name, Password, Dob, PhoneNumber, Email) VALUES ('" + userName + "','" + password + "','" + DateTime.Parse(dob) + "','" + phoneNumber + "','" + email + "')";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(insertQuery, connection);
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
        public bool LoginUser()
        {
            Console.WriteLine("Enter Email :");
            string email = Console.ReadLine();
            Console.WriteLine("Enter Password :");
            string password = Console.ReadLine();

            string selectQuery = "SELECT * FROM Login WHERE Email = '" +email+ "' AND Password = '"+password+"'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd=new SqlCommand(selectQuery, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        Console.WriteLine("Login Successful.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid credential, please try again.");
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
    }
}
