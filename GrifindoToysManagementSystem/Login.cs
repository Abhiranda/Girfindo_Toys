using GrifindoToysManagementSystem.System_Par;
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

namespace GrifindoToysManagementSystem
{
    public partial class Login : Form
    { 
        public Login()
        {
            InitializeComponent();
        }
        internal class Employee
        {
            public static string userType = "";
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Database_Connection.GetConnection())
                {
                    con.Open();
                    string query = "SELECT * FROM loginUser WHERE userId = @userId AND password = @password";
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);

                    if (int.TryParse(text_userid.Text.Trim(), out int userId))
                    {
                        sda.SelectCommand.Parameters.AddWithValue("@userId", userId);
                        sda.SelectCommand.Parameters.AddWithValue("@password", text_password.Text.Trim());

                        DataTable dataTable = new DataTable();
                        sda.Fill(dataTable);

                        if (dataTable.Rows.Count == 1)
                        {
                            Employee.userType = dataTable.Rows[0][2].ToString();
                            MessageBox.Show("Login Successfuly.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Form1 f1 = new Form1();
                            f1.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            text_userid.Clear();
                            text_password.Clear();
                            text_userid.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("User ID must be a valid.", "Invalid User ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        text_userid.Clear();
                        text_userid.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            text_userid.Clear();
            text_password.Clear();

            text_userid.Focus();
        }

        private void checkBox_ShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            bool showPasswords = checkBox_ShowPassword.Checked;

            text_password.UseSystemPasswordChar = !showPasswords;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
