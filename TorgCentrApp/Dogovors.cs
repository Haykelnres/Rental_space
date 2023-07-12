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
    public partial class Dogovors : Form
    {
        private string login;
        DataSet actual;
        DataSet actual1;
        SqlDataAdapter adapter;
        SqlDataAdapter adapter1;
        
        string Dogvor = "SELECT * FROM Dogovor";
        string Dogvor1 = "SELECT * FROM Dogovor";
        
        string Dogvordel = "SELECT * FROM Dogovor";
        string oleCommand = ($"SELECT Room_id from Room where Room_status = '{"Не в эксплуатации"}'");
        string oleCommand2 = "SELECT Arendator_id from Arendator ";
        string connectionString = @"Data Source=localhost;Initial Catalog=TCDB;Integrated Security=True";

        public void refresh()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(Dogvor, connection);
                actual1 = new DataSet();
                adapter.Fill(actual1);
                dataGridView1.DataSource = actual1.Tables[0];
                


                adapter = new SqlDataAdapter(Dogvor1, connection);
                actual1 = new DataSet();
                adapter.Fill(actual1);
                dataGridView1.DataSource = actual1.Tables[0];
                
                connection.Close();
            }
        }

        public Dogovors(string login)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.login = login;

            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Dogovors_Load(object sender, EventArgs e)
        {

            

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(Dogvor, connection);
                actual1 = new DataSet();
                adapter.Fill(actual1);
                dataGridView1.DataSource = actual1.Tables[0];
                dataGridView1.Columns[0].HeaderText = "id договора ";
                dataGridView1.Columns[1].HeaderText = "дата создания договора";
                dataGridView1.Columns[2].HeaderText = "дата окончания договора";
                dataGridView1.Columns[3].HeaderText = "цена";
                dataGridView1.Columns[4].HeaderText = "id арендатора";
                dataGridView1.Columns[5].HeaderText = "id комнаты";
                dataGridView1.Columns[6].HeaderText = "тип оплаты";
                dataGridView1.Columns[7].HeaderText = "статус договора";
                dataGridView1.Columns[8].HeaderText = "id работника-составителя";

                //заполнение комбобокса для room id
                SqlCommand cmd = new SqlCommand(oleCommand, connection);
                SqlDataReader DR = cmd.ExecuteReader();

                while (DR.Read())
                {
                    comboBox2.Items.Add(DR[0]);

                }
                DR.Close();
                //заполнение комбобокса для arendator id
                SqlCommand cmd1 = new SqlCommand(oleCommand2, connection);
                SqlDataReader DR2 = cmd1.ExecuteReader();

                while (DR2.Read())
                {
                    
                    comboBox1.Items.Add(DR2[0]);

                }
                DR2.Close();
                comboBox3.Items.Add("Наличный");
                comboBox3.Items.Add("Безналичный");
                comboBox4.Items.Add("Активен");
                comboBox4.Items.Add("Не активен");
                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DateTime now = DateTime.Now;
                
                string idfrw = "";
                connection.Open();
                string datenow = now.ToString("d");
                string idforwork = ($"select Worker_id from Worker where Worker_log = '{login}'");
                SqlCommand cmd = new SqlCommand(idforwork, connection);
                SqlDataReader DR = cmd.ExecuteReader();
                while (DR.Read())
                {
                    idfrw = (DR[0]).ToString();

                }
                DR.Close();
                string oledCommand = ($"SELECT Room_status from Room where Room_id = '{comboBox2.Text}'");
                string result = "";
                SqlCommand cmdd = new SqlCommand(oledCommand, connection);
                SqlDataReader DRd = cmdd.ExecuteReader();
                while (DRd.Read())
                {
                    result = (DRd[0]).ToString();

                }
                DRd.Close();
                if ((comboBox1.Text != "") & (comboBox2.Text != "") & (comboBox3.Text != "") & (comboBox4.Text != "") & (textBox1.Text != "") & (textBox2.Text != ""))
                {
                    if ((result.ToString()) == "В эксплуатации")
                    {
                        MessageBox.Show("Помещение уже занято.");
                    }
                    else
                    {
                        SqlCommand command = new SqlCommand($"INSERT INTO Dogovor (Dogovor_create ,Dogovor_end ,Dogovor_cost ,Arendator_id,Room_id,Dogovor_payment,Dogovor_status, Worker_id) VALUES ('{datenow}', '{textBox1.Text}', '{textBox2.Text}', '{comboBox1.Text}','{comboBox2.Text}','{comboBox3.Text}','{comboBox4.Text}', '{idfrw}')", connection);
                        command.ExecuteNonQuery();
                        
                        SqlCommand command1 = new SqlCommand($"UPDATE Room set Room_status = '{"В эксплуатации"}' where Room_id = '{comboBox2.Text}'", connection);
                        command1.ExecuteNonQuery();
                        connection.Close();
                    }

                    //                                     

                    refresh();
                }
                else
                {
                    MessageBox.Show("Все поля должны быть заполнены");
                }
                
            
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string id;
                connection.Open();

                var index = dataGridView1.SelectedCells[0].RowIndex;
                var IdRoomerForDelete = dataGridView1[0, index].Value;
                //id = dataGridView1.SelectedRows[0].Cells["Dogovor_id"].Value.ToString();

                
                SqlCommand command1 = new SqlCommand($"UPDATE Room set Room_status = '{"Не в эксплуатации"}' where Room_id = (select Room_id from Dogovor where Dogovor_id={IdRoomerForDelete})", connection);
                command1.ExecuteNonQuery();
                SqlCommand SQLDLETE = new SqlCommand("DELETE FROM Dogovor where Dogovor_id =" + IdRoomerForDelete, connection);
                
                SQLDLETE.ExecuteNonQuery();
                
                refresh();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                connection.Open();

                var index = dataGridView1.SelectedCells[0].RowIndex;
                var IdRoomerForDelete = dataGridView1[0, index].Value;
                


                SqlCommand command1 = new SqlCommand($"UPDATE Room set Room_status = '{"Не в эксплуатации"}' where Room_id = (select Room_id from Dogovor where Dogovor_id={IdRoomerForDelete})", connection);
                command1.ExecuteNonQuery();
                SqlCommand SQLDLETE = new SqlCommand($"UPDATE Dogovor set Dogovor_status = '{"Не активен"}' where Dogovor_id = {IdRoomerForDelete}", connection); 
                SQLDLETE.ExecuteNonQuery();

                refresh();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();

                var index = dataGridView1.SelectedCells[0].RowIndex;
                var IdRoomerForDelete = dataGridView1[0, index].Value;

                string oledCommand = ($"SELECT Room_status from Room where Room_id = (select Room_id from Dogovor where Dogovor_id={IdRoomerForDelete})");
                string result = "";
                SqlCommand cmd = new SqlCommand(oledCommand, connection);
                SqlDataReader DR = cmd.ExecuteReader();
                while (DR.Read())
                {
                    result = (DR[0]).ToString();

                }
                DR.Close();
                if ((result.ToString()) == "В эксплуатации") 
                {
                    MessageBox.Show("Помещение уже занято, необходимо создать новый договор");
                }
                else
                {
                    SqlCommand command1 = new SqlCommand($"UPDATE Room set Room_status = '{"В эксплуатации"}' where Room_id = (select Room_id from Dogovor where Dogovor_id={IdRoomerForDelete})", connection);
                    command1.ExecuteNonQuery();
                    SqlCommand SQLDLETE = new SqlCommand($"UPDATE Dogovor set Dogovor_status = '{"Активен"}'where Dogovor_id = {IdRoomerForDelete}", connection);
                    SQLDLETE.ExecuteNonQuery();

                    refresh();
                }
                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string Dogvorsearch = $"select * from Dogovor where Dogovor_id = '{textBox3.Text}'";
                connection.Open();
                adapter1 = new SqlDataAdapter(Dogvorsearch, connection);
                actual = new DataSet();
                adapter1.Fill(actual);
                dataGridView1.DataSource = actual.Tables[0];
                dataGridView1.Columns[0].HeaderText = "id договора ";
                dataGridView1.Columns[1].HeaderText = "дата создания договора";
                dataGridView1.Columns[2].HeaderText = "дата окончания договора";
                dataGridView1.Columns[3].HeaderText = "цена";
                dataGridView1.Columns[4].HeaderText = "id арендатора";
                dataGridView1.Columns[5].HeaderText = "id комнаты";
                dataGridView1.Columns[6].HeaderText = "тип оплаты";
                dataGridView1.Columns[7].HeaderText = "статус договора";
                dataGridView1.Columns[8].HeaderText = "id работника-составителя";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(Dogvor, connection);
                actual1 = new DataSet();
                adapter.Fill(actual1);
                dataGridView1.DataSource = actual1.Tables[0];
                dataGridView1.Columns[0].HeaderText = "id договора ";
                dataGridView1.Columns[1].HeaderText = "дата создания договора";
                dataGridView1.Columns[2].HeaderText = "дата окончания договора";
                dataGridView1.Columns[3].HeaderText = "цена";
                dataGridView1.Columns[4].HeaderText = "id арендатора";
                dataGridView1.Columns[5].HeaderText = "id комнаты";
                dataGridView1.Columns[6].HeaderText = "тип оплаты";
                dataGridView1.Columns[7].HeaderText = "статус договора";
                dataGridView1.Columns[8].HeaderText = "id работника-составителя";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string oleCommand = ($"SELECT * from Dogovor where Dogovor_status = '{"Активен"}'");
                connection.Open();
                adapter = new SqlDataAdapter(oleCommand, connection);
                actual1 = new DataSet();
                adapter.Fill(actual1);
                dataGridView1.DataSource = actual1.Tables[0];
                dataGridView1.Columns[0].HeaderText = "id договора ";
                dataGridView1.Columns[1].HeaderText = "дата создания договора";
                dataGridView1.Columns[2].HeaderText = "дата окончания договора";
                dataGridView1.Columns[3].HeaderText = "цена";
                dataGridView1.Columns[4].HeaderText = "id арендатора";
                dataGridView1.Columns[5].HeaderText = "id комнаты";
                dataGridView1.Columns[6].HeaderText = "тип оплаты";
                dataGridView1.Columns[7].HeaderText = "статус договора";
                dataGridView1.Columns[8].HeaderText = "id работника-составителя";
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string oleCommand1 = ($"SELECT * from Dogovor where Dogovor_status = '{"Не активен"}'");
                connection.Open();
                adapter = new SqlDataAdapter(oleCommand1, connection);
                actual1 = new DataSet();
                adapter.Fill(actual1);
                dataGridView1.DataSource = actual1.Tables[0];
                dataGridView1.Columns[0].HeaderText = "id договора ";
                dataGridView1.Columns[1].HeaderText = "дата создания договора";
                dataGridView1.Columns[2].HeaderText = "дата окончания договора";
                dataGridView1.Columns[3].HeaderText = "цена";
                dataGridView1.Columns[4].HeaderText = "id арендатора";
                dataGridView1.Columns[5].HeaderText = "id комнаты";
                dataGridView1.Columns[6].HeaderText = "тип оплаты";
                dataGridView1.Columns[7].HeaderText = "статус договора";
                dataGridView1.Columns[8].HeaderText = "id работника-составителя";
            }
        }
    }
}
