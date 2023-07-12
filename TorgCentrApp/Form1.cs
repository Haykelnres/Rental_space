using System.Data.SqlClient;
using System.Data;
using System;


namespace TorgCentrApp
{
    
    public partial class Form1 : Form
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=TCDB;Integrated Security=True";
        string a;
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var login = textBox1.Text;
                var password = textBox2.Text;
                
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                
                string querystring = ($"select Worker_id, Worker_name, Worker_log, Worker_password from Worker where Worker_log = '{login}' and Worker_password = '{password}'");
                string idforwork = ($"select Worker_id from Worker where Worker_log = '{login}'");
                connection.Open();
                SqlCommand cmd = new SqlCommand(querystring, connection);
                adapter.SelectCommand = cmd;
                adapter.Fill(table);
                if (table.Rows.Count == 1)
                {
                    Work work = new Work(login);
                    connection.Close();
                    this.Hide();
                    work.ShowDialog();
                    this.Show();
                    

                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            textBox2.MaxLength = 20;
            textBox1.MaxLength = 10;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
    }
}