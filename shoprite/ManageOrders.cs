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


namespace shoprite
{
    public partial class ManageOrders : Form
    {
        public ManageOrders()
        {
            InitializeComponent();
        }



        DataTable table = new DataTable();

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\edwin\Documents\shopdb.mdf;Integrated Security=True;Connect Timeout=30");

        void populate()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from CustomerTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                CustomersGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }

        void populateproducts()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from ProductTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }

        void fillcategory()
        {
            string query = "select * from CategoryTbl";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            try
            {
                Con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("CatName", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                //CatCombo.ValueMember = "CatName";
                //CatCombo.DataSource = dt;
                SearchCombo.ValueMember = "CatName";
                SearchCombo.DataSource = dt;
                Con.Close();
            }
            catch
            {

            }
        }
        void updateproduct()

        {
           

            int id = Convert.ToInt32(ProductGV.SelectedRows[0].Cells[0].Value.ToString());
            int newQty = stock - Convert.ToInt32(QtyTb.Text);
            if (newQty < 0)
            {
                MessageBox.Show("Operation Failed");
            }
            else
           {
                Con.Open();
                string query = "update ProductTbl set ProQty = " + newQty + "where ProId" + id + ";";   
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                populateproducts();
            }
        }
        int num = 0;
        int uprice, totprice, qty;
        string product;
        

        private void ManageOrders_Load(object sender, EventArgs e)
        {
            populate();
            populateproducts();
            fillcategory();
            table.Columns.Add("Num",typeof(int));
            table.Columns.Add("Product", typeof(String));  
            table.Columns.Add("Quantity", typeof(int));
            table.Columns.Add("UPrice", typeof(int));
            table.Columns.Add("TotPrice", typeof(int));

            OrderGV.DataSource = table;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        int flag = 0;
        int stock;
        private void ProductGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            product = ProductGV.SelectedRows[0].Cells[1].Value.ToString();
            //qty = Convert.ToInt32(QtyTb.Text);
            stock = Convert.ToInt32(ProductGV.SelectedRows[0].Cells[2].Value.ToString());
            uprice = Convert.ToInt32(ProductGV.SelectedRows[0].Cells[3].Value.ToString());
            //totprice = qty * uprice;
            flag = 1;



        }
      

        private void button1_Click(object sender, EventArgs e)
        {
            int sum = 0;
            if (QtyTb.Text == "")
                MessageBox.Show("Enter The Quantity Of Products");
            else if(flag == 0)
            {
                MessageBox.Show("Selects the Products");
            }
           else if (Convert.ToInt32(QtyTb.Text) > stock)
            {
                MessageBox.Show("Not Enough Stock Available");
            }
            else
            {

                num = num + 1;
                qty = Convert.ToInt32(QtyTb.Text);
                totprice = qty * uprice;
                table.Rows.Add(num,product,qty,uprice,totprice);
                OrderGV.DataSource = table;
                flag = 0;
                
            }

            sum = sum + totprice;
            TotAmount.Text = sum.ToString();
            //updateproduct();
        }

        private void CustomersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Cid.Text = CustomersGV.SelectedRows[0].Cells[0].Value.ToString();
            CustName.Text = CustomersGV.SelectedRows[0].Cells[1].Value.ToString();


        }

        private void OrderGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (OrderIdTb.Text == "" || Cid.Text =="" || CustName.Text =="" || TotAmount.Text =="" )
            {
                MessageBox.Show("Fill The data Correctly");
            }
            else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into OrdersTbl values(" + OrderIdTb.Text + "," + Cid.Text + ",'" + CustName.Text + "','" + orderdate.Text + "'," + TotAmount.Text + ")", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Order Added Successfully ");
                Con.Close();
                //populate();
                try
                {
                   

                }
                catch
                {

                }
            }
        }

        private void bunifuDatepicker1_onValueChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ViewOrders view = new ViewOrders();
            view.Show();

        }

        private void OrderIdTb_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void SearchCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string Myquery = "select * from ProductTbl  where prodCat = '" + SearchCombo.SelectedValue.ToString() + "'";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }
    }
}
