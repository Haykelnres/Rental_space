using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TorgCentrApp
{
    public partial class Work : Form
    {
        private string login;
        public Work(string login)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.login = login;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Arendators1 arendators = new Arendators1();
            this.Hide();

            arendators.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Rooms backrooms = new Rooms();
            this.Hide();

            backrooms.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dogovors dogovors1 = new Dogovors(login);
            this.Hide();

            dogovors1.ShowDialog();
            this.Show();
        }
    }
}
