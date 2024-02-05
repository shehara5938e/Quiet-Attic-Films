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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Quiet_Attic_Films
{
    public partial class Staff : Form
    {
        //---Sql Connection
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\sheha\Documents\SE Projects\Quiet Attic Films\Database.mdf';Integrated Security=True");
        static string id = "0";

        //---clearAll() fn
        public void clearAll()
        {
            //---textbox data
            textBox1.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();

            button10.Enabled = false;
            button13.Enabled = false;
        }

        //---Refresh Staff DGV fn
        public void RefreshStaff()
        {
            string Query = "Select * From Staff";
            SqlDataAdapter adapter = new SqlDataAdapter(Query, con);
            DataSet ds = new System.Data.DataSet();
            adapter.Fill(ds, "Staff");
            dataGridView1.DataSource = ds.Tables[0];
        }

        public Staff()
        {
            InitializeComponent();
        }
                
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //Close Entire Application

            System.Windows.Forms.Application.Exit();

            //Close Entire Application
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Production btn

            this.Hide();
            Production production = new Production();
            production.Show();

            //Production btn
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Already Opened

            MessageBox.Show("This Window Is Already Opened", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Already Opened
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Client btn

            this.Hide();
            Clients clients = new Clients();
            clients.Show();

            //Client btn
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Properties btn

            this.Hide();
            Property property = new Property();
            property.Show();

            //Properties btn
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //logout btn

            this.Hide();
            Login login = new Login();
            login.Show();

            //logout btn
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //save btn//save btn//save btn

            //---textbox data
            string Name = textBox1.Text;
            string Email = textBox3.Text;
            string Phone = textBox4.Text;
            decimal Salary = decimal.Parse(textBox5.Text);

            //---combobox data
            string Type = "";
            if (comboBox2.SelectedItem != null)
            {
                Type = comboBox2.SelectedItem.ToString();
            }

            SqlCommand cmd = new SqlCommand("insert into Staff(Name, Type, Email, Phone, Salary) values('" + Name + "', '" + Type + "', '" + Email + "', '" + Phone + "', '" + Salary + "')", con);

            if (Name != "" && Type != "" && Email != "" && Phone != "")
            { 
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    clearAll();
                    MessageBox.Show("Staff Data Saved Succesfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex);
                    con.Close();
                }

                RefreshStaff();
            }
            else
            {
                MessageBox.Show("Please Provide Required Data To Proceed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //save btn//save btn//save btn
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //search btn//search btn//search btn

            string Name = textBox9.Text;
            SqlCommand cmd = new SqlCommand("select * from Staff where Name = '" + Name + "'", con);

            if (Name != "")
            {
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            id = reader["Id"].ToString();
                            //---textbox data
                            textBox1.Text = reader["Name"].ToString();
                            textBox3.Text = reader["Email"].ToString();
                            textBox4.Text = reader["Phone"].ToString();
                            textBox5.Text = reader["Salary"].ToString();

                            //---combobox data
                            comboBox2.SelectedItem = reader["Type"].ToString();

                            button10.Enabled = true;
                            button13.Enabled = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No Data Available From This Name: " + Name, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex);
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("First Enter The Name You Want To Search", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //search btn//search btn//search btn
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //update btn//update btn//update btn

            //---textbox data
            string Name = textBox1.Text;
            string Email = textBox3.Text;
            string Phone = textBox4.Text;
            string Salary = textBox5.Text;

            //---combobox data
            string Type = "";
            if (comboBox2.SelectedItem != null)
            {
                Type = comboBox2.SelectedItem.ToString();
            }

            SqlCommand cmd = new SqlCommand("update Staff Set Name = '" + Name + "', Type = '" + Type + "', Email = '" + Email + "', Phone = '" + Phone + "', Salary = '" + Salary + "' where id = '" + id + "'", con);

            if (Name != "" && Email != "" && Type != "" && Salary != "" && Phone != "")
            {
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    clearAll();
                    MessageBox.Show("Staff Data Updated Succesfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex);
                    con.Close();
                }

                RefreshStaff();
            }
            else
            {
                MessageBox.Show("Please provide the required data to proceed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //update btn//update btn//update btn
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //delete btn//delete btn//delete btn

            SqlCommand cmd = new SqlCommand("Delete from Staff where Id = '" + id + "' ", con);

            if (MessageBox.Show("Are You Sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    clearAll();
                    MessageBox.Show("Staff Data Deleted Succesfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex);
                    con.Close();
                }

                RefreshStaff();
            }

            //delete btn//delete btn//delete btn
        }

        private void button14_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void Staff_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'staff_DataSet.Staff' table. You can move, or remove it, as needed.
            this.staffTableAdapter.Fill(this.staff_DataSet.Staff);

            RefreshStaff();

            button10.Enabled = false;
            button13.Enabled = false;
        }
    }
}
