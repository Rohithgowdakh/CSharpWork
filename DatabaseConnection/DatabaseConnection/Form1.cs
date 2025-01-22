using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace DatabaseConnection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void employeesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.employeesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.demoDBDataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'demoDBDataSet.Employees' table. You can move, or remove it, as needed.
            //this.employeesTableAdapter.Fill(this.demoDBDataSet.Employees);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Address of SQL server and database
            //string connectionString = "Data Source=ROHITH\\SQLEXPRESS;Initial Catalog=DemoDB;Integrated Security=True;Trust Server Certificate=True";
            string connectionString = "Data Source=ROHITH\\SQLEXPRESS;Initial Catalog=DemoDB;User ID=sa;Password=data@1234;Encrypt=False";


            //Establish connection
            SqlConnection conn = new SqlConnection(connectionString);
            
            //Open connection
            conn.Open();

            //Prepare query
            string id =idTextBox.Text;
            string name=nameTextBox.Text;
            string age = ageTextBox.Text;
            string salary = salaryTextBox.Text;
            string query= $"INSERT INTO Employees(Id, Name, Age, Salary) VALUES('"+id+"', '"+name+ "', '"+age+"', '"+salary+"')";

            //Execute query
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();

            //Close Connection
            conn.Close();
            MessageBox.Show("Data has been saved");
        }
        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
