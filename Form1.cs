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

namespace UsersForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //SQL Connection
        // SqlConnection conn = new SqlConnection(@"Data Source=localhost\sqlexpress;Initial Catalog=Users;Integrated Security=True");

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            //calling out of authentication script
            authentication_login();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            input_username.Clear(); 
            input_password.Clear();

            input_username.Focus();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Do you want to exit","Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(res == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                this.Show();
            }
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Hide();
        }

        private void authentication_login()
        {
            String username, password;
            username = input_username.Text; 
            password = input_password.Text;

            if(String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                MessageBox.Show("All fields are reuiqred");
                return;
            }

            using (SqlConnection sql =  new SqlConnection(@"Data Source=localhost\sqlexpress;Initial Catalog=Users;Integrated Security=True"))
            {
                try
                {
                    sql.Open();

                    String query = "SELECT COUNT(*) FROM User_info WHERE Username = @username AND Password = @password";
                    SqlCommand cmd = new SqlCommand(query, sql);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    int user = (int)cmd.ExecuteScalar();
                    if(user > 0)
                    {
                        MessageBox.Show("Login Successfull");
                        Menuform menuform = new Menuform();
                        menuform.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Username or Password, Please try again!");
                    }

                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error" + ex.Message);
                }

            }

        }
    }
}
