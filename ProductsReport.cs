using CrystalDecisions.CrystalReports.Engine;
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

namespace Stock003.ReportsForm
{
    public partial class ProductsReport : Form
    {
        ReportDocument cryrpt = new ReportDocument();

        public ProductsReport()
        {
            InitializeComponent();
        }

        //private void ProductsReport_Load(object sender, EventArgs e)
        //{
        //    cryrpt.Load(@"C:\Users\odili\source\repos\Stock003\Reports\Product.rpt");


        //    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1DHOG7R\ODILIOE002; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False");

        //    con.Open();
        //    DataSet dst = new DataSet();
        //    SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM [Stock002].[dbo].[Products]", con);
        //    DataTable dt = new DataTable();
        //    sda.Fill(dt);
        //    cryrpt.SetDataSource(dt);
        //    crystalReportViewer1.ReportSource = cryrpt;

        //}
    }
}