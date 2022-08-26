﻿using System;
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
    public partial class Mainform : Form
    {
        public Mainform()
        {
            InitializeComponent();
        }
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
        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       
        private void UsersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into CustomerTbl values('" + customerid.Text + "','" + customername.Text + "','" + customerphone.Text + "')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Successfully Added");
                Con.Close();
                populate();

            }
            catch
            {

            }
        }

        private void Mainform_Load(object sender, EventArgs e)
        {
            populate();
        }

      

        private void CustomersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            customerid.Text = CustomersGV.SelectedRows[0].Cells[0].Value.ToString();
           customername.Text = CustomersGV.SelectedRows[0].Cells[1].Value.ToString();
           customerphone.Text =CustomersGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (customerid.Text == "")
            {
                MessageBox.Show("Enter TCustomer Id ");
            }
            else
            {
                Con.Open();
                string myquery = "delete from CustomerTbl where Cid='" + customerid.Text + "';";
                SqlCommand cmd = new SqlCommand(myquery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Successfully Deleted");
                Con.Close();
                populate();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update CustomerTbl set Cname='" + customername.Text + "',Cphone='" + customerphone.Text + "' where Cid= '" + customerid.Text + "'", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Successfully Updated");
                Con.Close();
                populate();

            }
            catch
            {

            }
        }
    }
}
