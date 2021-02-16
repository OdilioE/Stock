using System;
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
    public partial class StockMain : Form
    {

        public StockMain()
        {
            InitializeComponent();
        }

        bool close = true;
        private void StockMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (close)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to Exit application...???", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    close = false;
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                } 
            }
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Products pro = new Products();
            pro.MdiParent = this;
            pro.StartPosition = FormStartPosition.CenterScreen;
            pro.Show();
        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stock stk = new Stock();
            stk.MdiParent = this;
            stk.StartPosition = FormStartPosition.CenterScreen;
            stk.Show();
        }

        private void productListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void productsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ReportsForm.ProductsReport2 prod = new ReportsForm.ProductsReport2();
            //ReportsForm.ProductsReport prod = new ReportsForm.ProductsReport();
            prod.MdiParent = this;
            prod.StartPosition = FormStartPosition.CenterScreen;
            prod.Show();
        }

        private void stockToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ReportsForm.StockReport prod = new ReportsForm.StockReport();
            prod.MdiParent = this;
            prod.StartPosition = FormStartPosition.CenterScreen;
            prod.Show();
        }
    }
}
