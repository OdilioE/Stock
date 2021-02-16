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
    public partial class Stock : Form
    {
        private DataGridView dgview;
        private DataGridViewTextBoxColumn dgviewcol1;
        private DataGridViewTextBoxColumn dgviewcol2;

        public Stock()
        {
            InitializeComponent();
        }

        private void Stock_Load(object sender, EventArgs e)
        {
            this.ActiveControl = dateTimePicker1;
            comboBox1.SelectedIndex = 0;
            LoadData();
            Search();
        }

        //private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if(e.KeyCode == Keys.Enter)
        //    {
        //        textBox1.Focus();
        //    }
        //}

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(textBox1.Text.Length > 0)
                {
                    /*
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1DHOG7R\ODILIOE002; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False");
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT ProductName FROM [Stock002].[dbo].[Products] WHERE [ProductCode] = '" + textBox1.Text + "'", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        textBox2.Text = dt.Rows[0][0].ToString();
                        textBox3.Focus();
                    }
                    else
                    {
                        textBox2.Text = "";
                    }*/

                    //Here we are going to show product name to the small dataGridView:

                    //textBox1.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                    //textBox2.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();

                    if(dgview.Rows.Count > 0)
                    {
                        textBox1.Text = dgview.SelectedRows[0].Cells[0].Value.ToString();
                        textBox2.Text = dgview.SelectedRows[0].Cells[1].Value.ToString();
                        this.dgview.Visible = false;
                        textBox3.Focus();
                    }
                    else
                    {
                        this.dgview.Visible = false;
                    }

                    textBox3.Focus();

                //}
                //else
                //{
                //    textBox1.Focus();
                }
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(textBox2.Text.Length > 0)
                {
                    textBox3.Focus();
                }
                else
                {
                    textBox2.Focus();
                }
            }
        }

        bool change = true;
        
        private void proCode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (change)
            {
                change = false;
                textBox1.Text = dgview.SelectedRows[0].Cells[0].Value.ToString();
                textBox2.Text = dgview.SelectedRows[0].Cells[1].Value.ToString();
                this.dgview.Visible = false;
                textBox3.Focus();
                change = true;
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if(textBox3.Text.Length > 0)
                {
                    comboBox1.Focus();
                }
                else
                {
                    textBox3.Focus();
                }
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if(comboBox1.SelectedIndex != -1)
                {
                    button1.Focus();
                }
                else
                {
                    comboBox1.Focus();
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        public void LoadData()
        {
            // SqlConnection con = Connection.GetConnection();

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1DHOG7R\ODILIOE002; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False");

            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT *
                                                      FROM[Stock002].[dbo].[Stock]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells["dgSno"].Value = n + 1;
                dataGridView1.Rows[n].Cells["dgProCode"].Value = row["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells["dgProName"].Value = row["ProductName"].ToString();
                dataGridView1.Rows[n].Cells["dgQuantity"].Value = row["Quantity"].ToString();
                dataGridView1.Rows[n].Cells["dgDate"].Value = Convert.ToDateTime(row["TransDate"].ToString()).ToString("MM/dd/yyyy");
                if ((bool)row["ProductStatus"])
                {
                    dataGridView1.Rows[n].Cells["dgStatus"].Value = "Active";
                }
                else
                {
                    dataGridView1.Rows[n].Cells["dgStatus"].Value = "Deactive";
                }
            }

            if(dataGridView1.Rows.Count > 0)
            {
                label8.Text = dataGridView1.Rows.Count.ToString();
                float totQty = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    totQty += float.Parse(dataGridView1.Rows[i].Cells["dgQuantity"].Value.ToString());
                    label9.Text = totQty.ToString();
                }
            }
            else
            {
                label8.Text = "0";
                label9.Text = "0";
            }
        }

        private void ResetRecords()
        {
            dateTimePicker1.Value = DateTime.Now;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.SelectedIndex = -1;
            button1.Text = "Add";
            dateTimePicker1.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResetRecords();
        }

        private bool Validation()
        {
            bool result = false;
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "Product code is required...!");
            }
            else
            {
                if (string.IsNullOrEmpty(textBox2.Text))
                {
                    errorProvider1.Clear();
                    errorProvider1.SetError(textBox2, "Product Name is required....!");
                }
                else
                {
                    if (string.IsNullOrEmpty(textBox3.Text))
                    {
                        errorProvider1.Clear();
                        errorProvider1.SetError(textBox3, "Quantity is needed here...!");
                    }
                    else
                    {
                        if(comboBox1.SelectedIndex == -1)
                        {
                            errorProvider1.Clear();
                            errorProvider1.SetError(comboBox1, "Select status please...");
                        }
                        else
                        {
                            errorProvider1.Clear();
                            result = true;
                        }
                    }
                }
            }
            return result;
        }
        
        private bool IfProductsExists(SqlConnection con, string productCode)
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT 1 FROM [Stock002].[dbo].[Stock] WHERE[ProductCode] = '" + productCode + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void button1_Click(object sender, EventArgs e)
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

                if (IfProductsExists(con, textBox1.Text))
                {
                        sqlQuery = @"UPDATE [Stock002].[dbo].[Stock] SET [ProductName] = '" + textBox2.Text + "', [TransDate] = '" +dateTimePicker1.Value.ToString("MM/dd/yyyy")+ "', [Quantity] = '" +textBox3.Text+ "', [ProductStatus] = '" + status + "' WHERE [ProductCode] = '" + textBox1.Text + "'";
                }
                else
                {
                    sqlQuery = @"INSERT INTO [Stock002].[dbo].[Stock]
                                ([ProductCode]
                                ,[ProductName]
                                ,[TransDate]
                                ,[Quantity]
                                ,[ProductStatus])
                            VALUES
                                ('" + textBox1.Text + "','" + textBox2.Text + "','" +dateTimePicker1.Value.ToString("MM/dd/yyyy")+ "','" +textBox3.Text+ "','" + status + "')";
                }

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();
                // Reading data on dataGrid
                LoadData();
                MessageBox.Show("Record Saved Successfully...!");
                ResetRecords();
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            button1.Text = "Update";
            textBox1.Text = dataGridView1.SelectedRows[0].Cells["dgProCode"].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells["dgProName"].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells["dgQuantity"].Value.ToString();
            dateTimePicker1.Text = DateTime.Parse(dataGridView1.SelectedRows[0].Cells["dgDate"].Value.ToString()).ToString("MM/dd/yyyy");
            if(dataGridView1.SelectedRows[0].Cells["dgStatus"].Value.ToString() == "Active")
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to delete register...?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (Validation())
                {
                    // SqlConnection con = Connection.GetConnection();

                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1DHOG7R\ODILIOE002; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False");

                    var sqlQuery = "";

                    if (IfProductsExists(con, textBox1.Text))
                    {
                        con.Open();
                        sqlQuery = @"DELETE FROM [Stock002].[dbo].[Stock] WHERE [ProductCode] = '" + textBox1.Text + "'";
                        SqlCommand cmd = new SqlCommand(sqlQuery, con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Record Deleted Successfully.!!!!!");
                    }
                    else
                    {
                        MessageBox.Show("Record does not exist....!!!!!");
                    }

                    // Reading data on dataGrid
                    LoadData();
                    ResetRecords();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                if (button1.Text == "Add")
                {
                    this.dgview.Visible = true;
                    dgview.BringToFront();
                    Search(105, 105, 430, 200, "Pro Code, Pro Name", "100,0");
                    this.dgview.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.proCode_MouseDoubleClick);

                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1DHOG7R\ODILIOE002; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False");
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("Select ProductCode, ProductName From [Stock002].[dbo].[Products] WHERE [ProductCode] Like '" + textBox1.Text + "%'", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    //dataGridView2.DataSource = dt; 
                    dgview.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        int n = dgview.Rows.Add();
                        dgview.Rows[n].Cells[0].Value = row["ProductCode"].ToString();
                        dgview.Rows[n].Cells[1].Value = row["ProductName"].ToString();
                    } 
                }
            }
            else
            {
                dgview.Visible = false;
            }
        }



        void Search()
        {
            dgview = new DataGridView();
            dgviewcol1 = new DataGridViewTextBoxColumn();
            dgviewcol2 = new DataGridViewTextBoxColumn();
            this.dgview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dgviewcol1, this.dgviewcol2 });
            this.dgview.Name = "dgview";
            dgview.Visible = false;
            this.dgviewcol1.Visible = false;
            this.dgviewcol2.Visible = false;
            this.dgview.AllowUserToAddRows = false;
            this.dgview.RowHeadersVisible = false;
            this.dgview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            //this.dgview.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgview_KeyDown);

            this.Controls.Add(dgview);
            this.dgview.ReadOnly = true;
            dgview.BringToFront();
        }

        //Two Columns
        void Search(int LX, int LY, int DW, int DH, string ColName, string ColSize)
        {
            this.dgview.Location = new System.Drawing.Point(LX, LY);
            this.dgview.Size = new System.Drawing.Size(DW, DH);

            string[] ClSize = ColSize.Split(',');
            //size
            for(int i = 0; i < ClSize.Length; i++)
            {
                if(int.Parse(ClSize[i]) != 0)
                {
                    dgview.Columns[i].Width = int.Parse(ClSize[i]);
                }
                else
                {
                    dgview.Columns[i].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            //name
            string[] ClName = ColName.Split(',');
            for(int i = 0; i < ClName.Length; i++)
            {
                this.dgview.Columns[i].HeaderText = ClName[i];
                this.dgview.Columns[i].Visible = true;
            }
        }

        private void dateTimePicker1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                textBox1.Focus();
            }
        }
    }
}