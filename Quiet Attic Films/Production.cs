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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Quiet_Attic_Films
{
    public partial class Production : Form
    {
        //---Sql Connection
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\sheha\Documents\SE Projects\Quiet Attic Films\Database.mdf';Integrated Security=True");
        static string id = "0";
        static string Staff;

        //---clearAll() fn
        void clearAll()
        {
            //---textbox data
            textBox1.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox2.Clear();

            //---combobox data
            comboBox2.SelectedIndex = -1;

            button10.Enabled = false;
            button13.Enabled = false;
        }

        //---Refresh Staff Budget DGV fn
        public void RefreshStaffBudget()
        {
            string Query = "Select * From Staff_Budget";
            SqlDataAdapter adapter = new SqlDataAdapter(Query, con);
            DataSet ds = new System.Data.DataSet();
            adapter.Fill(ds, "Staff_Budget");
            dataGridView2.DataSource = ds.Tables[0];
        }

        //---Refresh Production fn
        public void RefreshProduction()
        {
            string Query = "Select * From Production";
            SqlDataAdapter adapter = new SqlDataAdapter(Query, con);
            DataSet ds = new System.Data.DataSet();
            adapter.Fill(ds, "Production");
            dataGridView1.DataSource = ds.Tables[0];
        }

        public Production()
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
            //Already Opened

            MessageBox.Show("This Window Is Already Opened", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Already Opened
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Staff btn

            this.Hide();
            Staff staff = new Staff();
            staff.Show();

            //Staff btn
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

        private void Production_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'production_DataSet.Production' table. You can move, or remove it, as needed.
            this.productionTableAdapter.Fill(this.production_DataSet.Production);
            // TODO: This line of code loads data into the 'clients_DataSet.Clients' table. You can move, or remove it, as needed.
            this.clientsTableAdapter.Fill(this.clients_DataSet.Clients);
            // TODO: This line of code loads data into the 'staffBudget_DataSet.Staff_Budget' table. You can move, or remove it, as needed.
            this.staff_BudgetTableAdapter.Fill(this.staffBudget_DataSet.Staff_Budget);

            button10.Enabled = false;
            button13.Enabled = false;

            RefreshStaffBudget();

        }

        //--------------------------------------------------Staff Budget Calculator (Built in)
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Select Type//Select Type//Select Type

            string Type = comboBox1.SelectedItem.ToString();
            SqlCommand cmd = new SqlCommand("select * from Staff where Type = '" + Type + "'", con);

            if (Type != "")
            {
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //---textbox data
                            textBox12.Text = reader["Salary"].ToString();                            
                        }
                    }
                    else
                    {
                        MessageBox.Show("No Data Available From This Type: " + Type, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex);
                    con.Close();
                }                                
            }

            //Select Type//Select Type//Select Type
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Add btn//Add btn//Add btn

            float Cost = float.Parse(textBox12.Text);
            int Count = int.Parse(textBox4.Text);

            float total = Cost * Count;

            string Type = "";
            if (comboBox1.SelectedItem != null)
            {
                Type = comboBox1.SelectedItem.ToString();
            }

            SqlCommand cmd = new SqlCommand("insert into Staff_Budget(Type, Count, Cost) values('" + Type + "', '" + Count + "', '" + total + "')", con);

            if (Cost != 0.00 && Count != 0 && Type != "")
            {
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex);
                    con.Close();
                }

                textBox4.Clear();
                RefreshStaffBudget();
            }
            else
            {
                MessageBox.Show("Please Provide Required Data To Proceed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //total box

            con.Open();

            string query = "SELECT SUM(Cost) FROM Staff_Budget";

            SqlCommand command = new SqlCommand(query, con);
            object totalAmount = command.ExecuteScalar();

            textBox5.Text = totalAmount.ToString();

            con.Close();

            //total box

            //Add btn//Add btn//Add btn
        }
                
        private void button11_Click(object sender, EventArgs e)
        {
            //delete btn//delete btn//delete btn

            SqlCommand cmd = new SqlCommand("Delete from Staff_Budget", con);

            if (MessageBox.Show("Are You Sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                                        
                    MessageBox.Show("Data Cleared Succesfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex);
                    con.Close();
                }

                textBox8.Clear();
                textBox12.Clear();
                textBox4.Clear();
                textBox5.Clear();
                comboBox1.SelectedIndex = -1;

                RefreshStaffBudget();
            }

            //delete btn//delete btn//delete btn
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //Finalize btn

            con.Open();

            string query = "SELECT SUM(Cost) FROM Staff_Budget";

            SqlCommand command = new SqlCommand(query, con);
            object totalAmount = command.ExecuteScalar();

            textBox8.Text = totalAmount.ToString();
            MessageBox.Show("Data Finalized", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            con.Close();

            textBox12.Clear();
            textBox4.Clear();
            textBox5.Clear();
            

            RefreshStaffBudget();

            //Finalize btn
        }
        //--------------------------------------------------Staff Budget Calculator (Built in)

        private void button12_Click(object sender, EventArgs e)
        {
            //save btn//save btn//save btn

            //---textbox data
            string Name = textBox1.Text;
            int Duration = int.Parse(textBox2.Text);
            string Type = textBox3.Text;
            string Properties = textBox10.Text;
            string Locations = textBox11.Text;
            float Staff_Budget = float.Parse(textBox8.Text);

            //Staff data
            SqlCommand cmd6 = new SqlCommand("select * from Staff_Budget", con);

            string Type5;
            string Count5;

            try
            {
                con.Open();
                SqlDataReader reader = cmd6.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Type5 = reader["Type"].ToString();
                        Count5 = reader["Count"].ToString();

                        Staff = Type5 + " " + Count5;
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
                con.Close();
            }

            //---combobox data
            string Client = "";
            if (comboBox2.SelectedItem != null)
            {
                Client = comboBox2.SelectedItem.ToString();
            }


            SqlCommand cmd = new SqlCommand("insert into Production(Name, Client, Duration, Type, Properties, Locations, Staff, Staff_Budget) values('" + Name + "', '" + Client + "', '" + Duration + "', '" + Type + "', '" + Properties + "', '" + Locations + "', '" + Staff + "','" + Staff_Budget + "')", con);


            if (Name != "" && Client != "" && Type != "" && Locations != "")
            {
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    clearAll();
                    MessageBox.Show("Production Data Saved Succesfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex);
                    con.Close();
                }

                RefreshProduction();
            }
            else
            {
                MessageBox.Show("Please Provide Required Data To Proceed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //Budget clear

            SqlCommand cmd1 = new SqlCommand("Delete from Staff_Budget", con);

            try
            {
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
                con.Close();
            }

            RefreshStaffBudget();

            //Budget clear

            //save btn//save btn//save btn
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //search btn//search btn//search btn

            string Name = textBox9.Text;
            SqlCommand cmd = new SqlCommand("select * from Production where Name = '" + Name + "'", con);

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
                            textBox2.Text = reader["Duration"].ToString();
                            textBox3.Text = reader["Type"].ToString();
                            textBox10.Text = reader["Properties"].ToString();
                            textBox11.Text = reader["Locations"].ToString();
                            textBox8.Text = reader["Staff_Budget"].ToString();

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
            int Duration = int.Parse(textBox2.Text);
            string Type = textBox3.Text;
            string Properties = textBox10.Text;
            string Locations = textBox11.Text;
            float Staff_Budget = float.Parse(textBox8.Text);

            //---combobox data
            string Client = "";
            if (comboBox2.SelectedItem != null)
            {
                Client = comboBox2.SelectedItem.ToString();
            }

            SqlCommand cmd = new SqlCommand("update Production Set Name = '" + Name + "', Duration = '" + Duration + "', Type = '" + Type + "', Properties = '" + Properties + "', Locations = '" + Locations + "' , '" + Staff_Budget + "' where id = '" + id + "'", con);

            if (Name != "" && Client != "" && Type != "" && Locations != "")
            {
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    clearAll();
                    MessageBox.Show("Production Data Updated Succesfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex);
                    con.Close();
                }

                RefreshProduction();
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

            SqlCommand cmd = new SqlCommand("Delete from Production where Id = '" + id + "' ", con);

            if (MessageBox.Show("Are You Sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    clearAll();
                    MessageBox.Show("Production Data Deleted Succesfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex);
                    con.Close();
                }

                RefreshProduction();
            }

            //delete btn//delete btn//delete btn
        }
    }
}
