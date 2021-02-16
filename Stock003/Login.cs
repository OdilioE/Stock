using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock003
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        { }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Clear();
            textBox1.Focus();
         }

        private void button2_Click(object sender, EventArgs e)
        {

            //To_Do: check username and password

             SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1DHOG7R\ODILIOE002;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
             SqlDataAdapter sda = new SqlDataAdapter(@"SELECT *
               FROM [Stock002].[dbo].[Login]
               WHERE UserName = '" +textBox1.Text+ "' and PassWord = '" + textBox2.Text +"'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                this.Hide();
                StockMain main = new StockMain();
                main.Show();
            }
            else
            {
                MessageBox.Show("Invalid UserName & Password...!", "error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                button1_Click(sender, e);
            }
        }
    }
}