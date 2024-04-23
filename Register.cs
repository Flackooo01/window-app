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

namespace UsersForm
{
    public partial class Register : Form
    {

        public Register()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            input_username.Clear();
            input_firstname.Clear(); 
            input_lastname.Clear();    
            input_password.Clear(); 
            input_cpassword.Clear();
            input_email.Clear();

            input_firstname.Focus();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            //calling out of authentication script
            authentication_register();
        }

        private void authentication_register()
        {
         String firstname, lastname, username, email, password, cpassword;

            firstname = input_firstname.Text;
            lastname = input_lastname.Text;
            username = input_username.Text;
            email = input_email.Text;
            password = input_password.Text;
            cpassword = input_cpassword.Text;

            if(String.IsNullOrEmpty(firstname) || String.IsNullOrEmpty(lastname) || String.IsNullOrEmpty(username) || String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password) || String.IsNullOrEmpty(cpassword))
            {
                MessageBox.Show("All fields are required");
                return;
            }

            using (SqlConnection sql = new SqlConnection(@"Data Source=localhost\sqlexpress;Initial Catalog=Users;Integrated Security=True"))
            {
                try
                {
                    sql.Open();
                    String res = "SELECT COUNT(*) FROM User_info WHERE Username = @username";
                    String query = "INSERT INTO User_info (Firstname, Lastname, Username, Email, Password, Confirm_Password)" +
                        "VALUES(@firstname,@lastname,@username,@email,@password,@cpassword)";
                    if(password == cpassword)
                    {
                        using(SqlCommand cmd = new SqlCommand(res, sql))
                        {
                            cmd.Parameters.AddWithValue("@username", username);
                            int users = (int)cmd.ExecuteScalar();
                            if(users > 0)
                            {
                                MessageBox.Show("Username is Already Exist/Taken");
                            }
                            else
                            {
                                using(SqlCommand insertsql = new SqlCommand(query, sql))
                                {
                                    insertsql.Parameters.AddWithValue("@firstname", firstname);
                                    insertsql.Parameters.AddWithValue("@lastname", lastname);
                                    insertsql.Parameters.AddWithValue("@username", username);
                                    insertsql.Parameters.AddWithValue("@email", email);
                                    insertsql.Parameters.AddWithValue("@password", password);
                                    insertsql.Parameters.AddWithValue("@cpassword", cpassword);

                                    int rowAffected = insertsql.ExecuteNonQuery();

                                    if(rowAffected > 0)
                                    {
                                        MessageBox.Show("Registered Successfull");
                                        Menuform menuform = new Menuform();
                                        menuform.Show();
                                        this.Hide();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Password does not Match!");
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show("Error" + ex.Message);
                }
            }
        }
    }
}
