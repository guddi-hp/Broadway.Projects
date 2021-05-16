using Broadway.DesktopApp.EF;
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

namespace Broadway.DesktopApp
{
    public partial class Form1 : Form
    {
        string connectionString =
            "Data Source=DESKTOP-7RRSGE5;Initial Catalog=AdoDB;"
            + "Integrated Security=true";


        public Form1()
        {
            InitializeComponent();
            button4.Enabled = false;


            ReloadData();
        }

        private void ReadFromTable(SqlConnection connection)
        {
            // Provide the query string with a parameter placeholder.

            // Provide the query string with a parameter placeholder.
            string queryString =
                "select * from Student";

            // Create the Command and Parameter objects.//
            SqlCommand command = new SqlCommand(queryString, connection);


            try
            {
                SqlDataReader reader = command.ExecuteReader();
                List<Student> students = new List<Student>();
                while (reader.Read())
                {
                    // var readers = new string[] { reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString() };

                    //todo: need to update this 
                    students.Add(new Student
                    {
                        id = (int)reader[0],
                        name = reader[1].ToString(),
                        email = reader[2].ToString(),
                        Age = (int)reader[3]

                    });

                    // Console.WriteLine(string.Join(",", readers));
                }
                reader.Close();
                dataGridView1.DataSource = students;
                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //  Console.ReadLine();



        }

        private void InsertToTable(SqlConnection connection)
        {

            var Name = textBox1.Text;

            var Email = textBox2.Text;

            var Age = textBox3.Text;

            string queryString =
               $"Insert into Student (Name, Email,Age) values ('{Name}','{Email}','{Age}')";

            // Create the Command and Parameter objects.
            SqlCommand command = new SqlCommand(queryString, connection);
            try
            {
                var res = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //Console.ReadLine();


        }
        private void UpdateToTable(SqlConnection connection)
        {


            //var name = textBox1.Text;

            //var email = textBox2.Text;

            //var Age = textBox3.Text;
            var Name = textBox1.Text;
            var Email = textBox2.Text;
            var Age = textBox3.Text;







            //string queryString = $"update Student set email=('{mail}'),Age=('{Age}') where name=('{name}') ";\
            string querystring = $"update Student set email=('{Email}'),Age=('{Age}') where name = ('{Name}')";
            SqlCommand command = new SqlCommand(querystring, connection);

            try
            {
                var res = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            // Console.ReadLine();



        }
        private void deletefromtable(SqlConnection connection)
        {
            if (selectedId == 0)
            {
                MessageBox.Show("Id is 0, unable to find the record");
                return;
            }

            //var name= textBox1.Text;
            string querystring = $"delete from student where id=('{selectedId}')";
            SqlCommand command = new SqlCommand(querystring, connection);

            try
            {
                var res = command.ExecuteNonQuery();
                ReloadData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // console.readline();

        }

       

       

        private void Form1_Load(object sender, EventArgs e)
        {
            ReloadData();
        }

        void ReloadData()
        {
            using (SqlConnection connection =
              new SqlConnection(connectionString))
            {
                connection.Open();
                ReadFromTable(connection);

                connection.Close();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                connection.Open();
                InsertToTable(connection);

                connection.Close();
                ReloadData();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection =
             new SqlConnection(connectionString))
            {
                connection.Open();
                UpdateToTable(connection);

                connection.Close();
                ReloadData();
            }

        }
        int selectedId = 0;

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection =
             new SqlConnection(connectionString))
            {
                connection.Open();
                deletefromtable(connection);

                connection.Close();
                ReloadData();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            var selected = dataGridView1.SelectedRows;
            selectedId = (int)selected[0].Cells[0].Value;
            textBox1.Text = (string)selected[0].Cells[1].Value;
            textBox2.Text = dataGridView1.CurrentRow.Cells["Email"].Value.ToString();
            textBox3.Text=dataGridView1.CurrentRow.Cells["Age"].Value.ToString();
            button4.Enabled = true;


        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
    }

    public class student
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }
}
