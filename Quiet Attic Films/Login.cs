using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quiet_Attic_Films
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //Close Entire Application

            System.Windows.Forms.Application.Exit();

            //Close Entire Application
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //show hide password

            if (checkBox1.Checked == true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }

            //show hide password
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //login btn

            string Username = textBox1.Text;
            string Password = textBox2.Text;

            if (Username == "Admin" && Password == "Admin")
            {
                this.Hide();
                Production production = new Production();
                production.Show();
            }
            else if (Username == "" && Password == "")
            {
                MessageBox.Show("Please Enter User Credentials To Proceed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Invalid User Credentials.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //login btn
        }
    }
}
