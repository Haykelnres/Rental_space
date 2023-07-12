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
    public partial class Rooms : Form
    {
        DataSet backrooms;
        DataSet backroomsactual;
        DataSet backroomsactual2;
        DataSet actual;
        SqlDataAdapter adapter;
        SqlDataAdapter adapter1;
        SqlDataAdapter adapter2;


        string rOOms = "SELECT * FROM Room";
        string rOOmsavailable = ($"SELECT * FROM Room where Room_status = '{"Не в эксплуатации"}'");
        string rOOmsaunavailable = ($"SELECT * FROM Room where Room_status = '{"В эксплуатации"}'");

        string connectionString = @"Data Source=localhost;Initial Catalog=TCDB;Integrated Security=True";
        
        public Rooms()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Rooms_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(rOOms, connection);

                backrooms = new DataSet();
                actual = backrooms;
                adapter.Fill(backrooms);
                dataGridView1.DataSource = backrooms.Tables[0];
                dataGridView1.Columns[0].HeaderText = "id помещения ";
                dataGridView1.Columns[1].HeaderText = "этаж";
                dataGridView1.Columns[2].HeaderText = "число этажей";
                dataGridView1.Columns[3].HeaderText = "площадь";
                dataGridView1.Columns[4].HeaderText = "наличие туалета";
                dataGridView1.Columns[5].HeaderText = "наличие окон";
                dataGridView1.Columns[6].HeaderText = "номер по ТЦ";
                dataGridView1.Columns[7].HeaderText = "наличие wifi";
                dataGridView1.Columns[8].HeaderText = "описание";
                dataGridView1.Columns[9].HeaderText = "статус";
                connection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            { 
                connection.Open();
                adapter1 = new SqlDataAdapter(rOOmsavailable, connection);

                backroomsactual = new DataSet();
                actual = backroomsactual;
                adapter1.Fill(backroomsactual);
                dataGridView1.DataSource = backroomsactual.Tables[0];
                connection.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter2 = new SqlDataAdapter(rOOmsaunavailable, connection);

                backroomsactual2 = new DataSet();
                actual = backroomsactual2;
                adapter2.Fill(backroomsactual2);
                dataGridView1.DataSource = backroomsactual2.Tables[0];
                dataGridView1.Columns[0].HeaderText = "id помещения ";
                dataGridView1.Columns[1].HeaderText = "этаж";
                dataGridView1.Columns[2].HeaderText = "число этажей";
                dataGridView1.Columns[3].HeaderText = "площадь";
                dataGridView1.Columns[4].HeaderText = "наличие туалета";
                dataGridView1.Columns[5].HeaderText = "наличие окон";
                dataGridView1.Columns[6].HeaderText = "номер по ТЦ";
                dataGridView1.Columns[7].HeaderText = "наличие wifi";
                dataGridView1.Columns[8].HeaderText = "описание";
                dataGridView1.Columns[9].HeaderText = "статус";
                connection.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(rOOms, connection);

                backrooms = new DataSet();
                actual = backrooms;
                adapter.Fill(backrooms);
                dataGridView1.DataSource = backrooms.Tables[0];
                dataGridView1.Columns[0].HeaderText = "id помещения ";
                dataGridView1.Columns[1].HeaderText = "этаж";
                dataGridView1.Columns[2].HeaderText = "число этажей";
                dataGridView1.Columns[3].HeaderText = "площадь";
                dataGridView1.Columns[4].HeaderText = "наличие туалета";
                dataGridView1.Columns[5].HeaderText = "наличие окон";
                dataGridView1.Columns[6].HeaderText = "номер по ТЦ";
                dataGridView1.Columns[7].HeaderText = "наличие wifi";
                dataGridView1.Columns[8].HeaderText = "описание";
                dataGridView1.Columns[9].HeaderText = "статус";
                connection.Close();
            }
        }
    }
}
