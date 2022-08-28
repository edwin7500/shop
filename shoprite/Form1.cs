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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\edwin\Documents\shopdb.mdf;Integrated Security=True;Connect Timeout=30");


        private void Form1_Load(object sender, EventArgs e)
        {

        }

       

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
                passwordTb.isPassword = true;

            else
                passwordTb.isPassword = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void passwordTb_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from UserTbl where Uname = '" + unameTb.Text + "' and Upassword = '" + passwordTb.Text + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                Home home = new Home();
                home.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Username or Password");
            }
            Con.Close();
        }

       // private SqlDataAdapter SqlDataAdapter(string v, SqlConnection con)
       // {
            //throw new NotImplementedException();
        //}
    }
    
}
 