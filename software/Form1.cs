namespace software
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)

        {
            if(checkBox1.Checked == false)
                passwordt.UseSystemPasswordChar = true;
            else
                passwordt.UseSystemPasswordChar = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void uname_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}