using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange
{
    
    static public class Global
    {
        public static string connectionString = "Data Source=ROHITH\\SQLEXPRESS;Initial Catalog=StockExchangeDB;User ID=sa;Password=data@1234;Encrypt=False";
        public static string loggedInEmail = "";
        static public UserModel userModel = new UserModel();
        static public void readCurrentUserData()
        {
            string selectQuery = @"SELECT * FROM Login WHERE Email=@Email";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(selectQuery, conn);
                cmd.Parameters.AddWithValue("@Email", loggedInEmail);
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        userModel.Id = Convert.ToInt32(reader["Id"]);
                        userModel.Email = reader["email"].ToString();
                        userModel.Password = reader["password"].ToString();
                    }
                }
                catch(Exception e) 
                {
                    Console.WriteLine(e.Message);                
                }
            }

        }
    }
}
