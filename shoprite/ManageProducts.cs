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
    public partial class ManageProducts : Form
    {
        public ManageProducts()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\edwin\Documents\shopdb.mdf;Integrated Security=True;Connect Timeout=30");

        void populate()
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

       

        void filterbycategory()
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
        void fillcategory()
        {
            string query = "select * from CategoryTbl";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;      
            try {
                Con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("CatName", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                CatCombo.ValueMember = "CatName";
                CatCombo.DataSource = dt;
                SearchCombo.ValueMember = "CatName";
                SearchCombo.DataSource = dt;

                Con.Close();
            }
            catch
            {

            }
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            try  
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into ProductTbl values('" + ProdidTb.Text + "','" + ProdNameTb.Text + "','" + QtyTb.Text + "','" + PriceTb.Text + "','"+DescriptionTb.Text+"','"+CatCombo.SelectedValue.ToString()+"')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Successfully Added");
                Con.Close();
                populate();

            }
            catch
            {

            }
        }

        private void ManageProducts_Load(object sender, EventArgs e)
        {
             fillcategory();
            populate();

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (ProdidTb.Text == "")
            {
                MessageBox.Show("Enter The product Id ");
            }
            else
            {
                Con.Open();
                string myquery = "delete from ProductTbl where Prodid=" + ProdidTb.Text + ";";
                SqlCommand cmd = new SqlCommand(myquery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Successfully Deleted");
                Con.Close();
                populate();

               
            }
         
        }

        private void ProductGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdidTb.Text =ProductGV.SelectedRows[0].Cells[0].Value.ToString();
            ProdNameTb.Text = ProductGV.SelectedRows[0].Cells[1].Value.ToString();
            QtyTb.Text = ProductGV.SelectedRows[0].Cells[2].Value.ToString();
            PriceTb.Text = ProductGV.SelectedRows[0].Cells[3].Value.ToString();
            DescriptionTb.Text = ProductGV.SelectedRows[0].Cells[4].Value.ToString();
            CatCombo.SelectedValue = ProductGV.SelectedRows[0].Cells[5].Value.ToString();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update ProductTbl set ProName='" + ProdNameTb.Text + "',ProQty='" + QtyTb.Text + "',ProPrice='" + PriceTb.Text + "',ProDesc='" + DescriptionTb.Text + "',ProdCat='" + CatCombo.SelectedValue.ToString() + "'where ProId='" + ProdidTb.Text + "'", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Successfully Updated");
                Con.Close();
                populate();

            }
            catch
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            filterbycategory();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            populate();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }
    }
}

