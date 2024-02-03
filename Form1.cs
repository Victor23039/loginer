using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp22
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
        }
        public class DBHelper
        {
            private string server = "";
            private int port = 0;
            private string username = "";
            private string password = "";
            private string database = "";

            MySqlConnection mySqlConnection = null;

            public DBHelper(string server = "localhost",
                            int port = 8889,
                            string username = "root",
                            string password = "root",
                            string database = "mysql01")
            {
                mySqlConnection = new MySqlConnection($"server={server};port={port};username={username};password={password};database={database}");
            }

            public MySqlConnection getMySqlConnection()
            {
                return mySqlConnection;
            }
        }
        
        private void button1_Click(object sender, EventArgs e, MySqlConnection mySqlConnection)
        {

            string name = textBox1.Text;
            string email = textBox2.Text;
            string password = textBox3.Text;

            MySqlCommand command = new MySqlCommand(
                $"SELECT * " +
                $"FROM users" +
                $"WHERE email = @email");
            command.Parameters.AddWithValue("@email", email);
            int count = (int)command.ExecuteScalar();

            if (textBox1 == null || textBox2 == null || textBox3 == null)
            {
                MessageBox.Show("Помилка");
                return;
            }
           
            else if (count > 0)
                 {
                    MessageBox.Show("Користувач з такою електронною поштою вже існує");
                    return;
                 }
            else
            {
                MySqlCommand command = new MySqlCommand(
                    $"INSERT INTO" +
                    $" users (name, email, password) " +
                    $"VALUES (@name, @email, @password)", mySqlConnection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                command.ExecuteNonQuery();

                MessageBox.Show("Реєстрування пройшло успішно!");
                return;
            }
        }
    }
}
