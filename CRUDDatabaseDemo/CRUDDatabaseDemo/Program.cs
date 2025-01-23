using System;
using System.Linq;
using Microsoft.Data.SqlClient;
namespace DatabaseOperation
{
    public class Program
    {
        static string connectionString = "Data Source=ROHITH\\SQLEXPRESS;Initial Catalog=DemoDB;User ID=sa;Password=data@1234;Encrypt=False";

        static void Main(string[] args)
        {
            //ReadEmployeeData();
            DeleteEmployeeData();
        }
        public static void ReadEmployeeData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Employees";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID : {reader["Id"]} Name : {reader["Name"]} Age : {reader["Age"]} Salary : {reader["Salary"]}");
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void DeleteEmployeeData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Enter the Employee ID to Delete :");
                    int id = int.Parse(Console.ReadLine());

                    string query = "DELETE FROM Employees WHERE Id='" + id + "'";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("");
                    connection.Close();
                    //SqlDataReader reader=new SqlDataReader(query);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}