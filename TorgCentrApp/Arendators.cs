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

namespace TorgCentrApp
{
    public partial class Arendators1 : Form
    {
        DataSet for_Arendators;
        DataSet actual;
        SqlDataAdapter adapter;

        string Areners = "SELECT * FROM Arendator";
        string Areners1 = "SELECT * FROM Arendator";
        string connectionString = @"Data Source=localhost;Initial Catalog=TCDB;Integrated Security=True";
        public void refresh()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                adapter = new SqlDataAdapter(Areners1, connection);
                for_Arendators = new DataSet();
                adapter.Fill(for_Arendators);
                dataGridView1.DataSource = for_Arendators.Tables[0];
                dataGridView1.Columns[0].HeaderText = "id арендатора ";
                dataGridView1.Columns[1].HeaderText = "ФИО арендатора";
                dataGridView1.Columns[2].HeaderText = "телефон арендатора";
                dataGridView1.Columns[3].HeaderText = "серия, номер паспорта";
                dataGridView1.Columns[4].HeaderText = "название фирмы";
                


                adapter = new SqlDataAdapter(Areners, connection);
                for_Arendators = new DataSet();
                adapter.Fill(for_Arendators);
                dataGridView1.DataSource = for_Arendators.Tables[0];
                dataGridView1.Columns[0].HeaderText = "id арендатора ";
                dataGridView1.Columns[1].HeaderText = "ФИО арендатора";
                dataGridView1.Columns[2].HeaderText = "телефон арендатора";
                dataGridView1.Columns[3].HeaderText = "серия, номер паспорта";
                dataGridView1.Columns[4].HeaderText = "название фирмы";
            }
        }
        public Arendators1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            
        }
        

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Arendators_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(Areners, connection);

                for_Arendators = new DataSet();
                actual = for_Arendators;
                adapter.Fill(for_Arendators);
                dataGridView1.DataSource = for_Arendators.Tables[0];
                dataGridView1.Columns[0].HeaderText = "id арендатора ";
                dataGridView1.Columns[1].HeaderText = "ФИО арендатора";
                dataGridView1.Columns[2].HeaderText = "телефон арендатора";
                dataGridView1.Columns[3].HeaderText = "серия, номер паспорта";
                dataGridView1.Columns[4].HeaderText = "название фирмы";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if(textBox1.Text != "" & textBox2.Text != "" & textBox3.Text != "" &textBox4.Text != "")
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand($"INSERT INTO Arendator (Arendator_name ,Arendator_phone ,Arendator_passport ,Arendator_firm_name) VALUES ('{textBox1.Text}', '{textBox2.Text}', '{textBox3.Text}', '{textBox4.Text}')", connection);
                    command.ExecuteNonQuery();
                    refresh();
                }
                else
                {
                    MessageBox.Show("Все поля должны быть заполнены");
                }
                
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
        string id;
        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try 
                {
                    connection.Open();
                    id = dataGridView1.SelectedRows[0].Cells["Arendator_id"].Value.ToString();
                    SqlCommand SQLDLETE = new SqlCommand("DELETE FROM Arendator where Arendator_id =" + id, connection);
                    SQLDLETE.ExecuteNonQuery();
                    refresh();
                } catch (Exception ex) { MessageBox.Show("Нельзя удалять действующего арендатора"); }
                
            }
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                id = dataGridView1.SelectedRows[0].Cells["Arendator_id"].Value.ToString();
                if (textBox1.Text != "")
                {

                    SqlCommand SQLCHANGE = new SqlCommand($"UPDATE Arendator SET Arendator_name = '{textBox1.Text}' where Arendator_id = {id} ", connection);

                    SQLCHANGE.ExecuteNonQuery();
                }
                
                if (textBox2.Text != "")
                {

                    SqlCommand SQLCHANGE = new SqlCommand($"UPDATE Arendator SET Arendator_phone = '{textBox2.Text}' where Arendator_id = {id} ", connection);

                    SQLCHANGE.ExecuteNonQuery();
                }
                if (textBox3.Text != "")
                {

                    SqlCommand SQLCHANGE = new SqlCommand($"UPDATE Arendator SET Arendator_passport = '{textBox3.Text}' where Arendator_id = {id} ", connection);

                    SQLCHANGE.ExecuteNonQuery();
                }
                if (textBox4.Text != "")
                {

                    SqlCommand SQLCHANGE = new SqlCommand($"UPDATE Arendator SET Arendator_firm_name = '{textBox4.Text}' where Arendator_id = {id} ", connection);

                    SQLCHANGE.ExecuteNonQuery();
                }
                refresh();
            }
        }
    }
}
