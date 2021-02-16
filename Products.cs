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

//modification by Odi today..... go to gibhub

namespace Stock003
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }

        private void Products_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0 ;
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                // SqlConnection con = Connection.GetConnection();

                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1DHOG7R\ODILIOE002; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False");

                //Insert logic
                con.Open();
                bool status = false;
                if (comboBox1.SelectedIndex == 0)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }

                var sqlQuery = "";

                if (IfProductExists(con, textBox1.Text))
                {
                    if (button2.Text == "Update")
                    {
                        sqlQuery = @"UPDATE [Stock002].[dbo].[Products] SET [ProductName] = '" + textBox2.Text + "', [ProductStatus] = '" + status + "' WHERE [ProductCode] = '" + textBox1.Text + "'";
                        SqlCommand cmd = new SqlCommand(sqlQuery, con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        MessageBox.Show("Product code already exists....!", "Message", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    sqlQuery = @"INSERT INTO [Stock002].[dbo].[Products]
                                ([ProductCode]
                                ,[ProductName]
                                ,[ProductStatus])
                            VALUES
                                ('" + textBox1.Text + "','" + textBox2.Text + "','" + status + "')";
                    SqlCommand cmd = new SqlCommand(sqlQuery, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }



                // Reading data on dataGrid
                LoadData();
                ResetRecord();
            } 
        }


        private bool IfProductExists(SqlConnection con, string ProductCode)
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT 1 FROM [Stock002].[dbo].[Products] WHERE [ProductCode] = '" + ProductCode + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }


        public void LoadData()
        {
            // SqlConnection con = Connection.GetConnection();

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1DHOG7R\ODILIOE002; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False");

            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT *
                                                      FROM[Stock002].[dbo].[Products]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow row in dt.Rows)                                        
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = row["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = row["ProductName"].ToString();
                if ((bool)row["ProductStatus"])
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Active";
                }
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Deactive";
                }
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            button2.Text = "Update";
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString() == "Active")
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Are you sure to delete register...?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (Validation())
                {
                    // SqlConnection con = Connection.GetConnection();

                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1DHOG7R\ODILIOE002; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False");

                    var sqlQuery = "";

                    if (IfProductExists(con, textBox1.Text))
                    {
                        con.Open();
                        sqlQuery = @"DELETE FROM [Stock002].[dbo].[Products] WHERE [ProductCode] = '" + textBox1.Text + "'";
                        SqlCommand cmd = new SqlCommand(sqlQuery, con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        MessageBox.Show("Record does not exist....!!!!!");
                    }

                    // Reading data on dataGrid
                    LoadData();
                    ResetRecord();
                } 
            }
        }

        private void ResetRecord()
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = -1;
            button2.Text = "Add";
            textBox1.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResetRecord();
        }

        private bool Validation()
        {
            bool result = false;

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "Product Code is Required !!");
            }
            else if (string.IsNullOrEmpty(textBox2.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox2, "Product Name is Required..!!");

            }
            else if (comboBox1.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(comboBox1, "Please select a product status..!");
            }
            else
            {
                result = true;
            }

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) && comboBox1.SelectedIndex > -1)
            {
                errorProvider1.Clear();
                result = true;
            }
            return result;
        }
    }
}