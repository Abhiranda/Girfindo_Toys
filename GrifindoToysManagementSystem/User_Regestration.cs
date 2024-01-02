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
    public partial class User_Regestration : Form
    {
        public User_Regestration()
        {
            InitializeComponent();
            DataGridViewSalary();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void DataGridViewSalary()
        {
            try
            {
                using (SqlConnection con = Database_Connection.GetConnection())
                {
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM loginUser", con);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView_userData.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while refreshing the DataGridView: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_userId.Text) || string.IsNullOrWhiteSpace(txt_conPassword.Text))
                {
                    MessageBox.Show("fill the User ID and password.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txt_password.Text != txt_conPassword.Text)
                {
                    MessageBox.Show("Password not match. Please confirm your password.", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_conPassword.Clear();
                    txt_conPassword.Clear();
                    txt_conPassword.Focus();
                    return;
                }

                if (!int.TryParse(txt_userId.Text, out int userId))
                {
                    MessageBox.Show("User ID must be a valid integer.", "Invalid User ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_userId.Clear();
                    txt_userId.Focus();
                    return;
                }

                string userType = radio_admin.Checked ? "Admin" : "User";

                using (SqlConnection con = Database_Connection.GetConnection())
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO loginUser (userId, password, userType) VALUES (@userId, @password, @userType)", con);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@password", txt_conPassword.Text);
                    cmd.Parameters.AddWithValue("@userType", userType);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Successfully Added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_userId.Text) || string.IsNullOrWhiteSpace(txt_conPassword.Text))
                {
                    MessageBox.Show("fill in both User ID and password.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txt_password.Text != txt_conPassword.Text)
                {
                    MessageBox.Show("Passwords not match. Please confirm your password.", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_conPassword.Clear();
                    txt_conPassword.Clear();
                    txt_conPassword.Focus();
                    return;
                }

                if (!int.TryParse(txt_userId.Text, out int userId))
                {
                    MessageBox.Show("User ID must be a valid integer.", "Invalid User ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_userId.Clear();
                    txt_userId.Focus();
                    return;
                }

                string userType = radio_admin.Checked ? "Admin" : "User";

                using (SqlConnection con = Database_Connection.GetConnection())
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE loginUser SET password = @password, userType = @userType WHERE userId = @userId", con);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@password", txt_conPassword.Text);
                    cmd.Parameters.AddWithValue("@userType", userType);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No record found with the provided User ID.", "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_userId.Text))
                {
                    MessageBox.Show("fill in the User ID field.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txt_userId.Text, out int userId))
                {
                    MessageBox.Show("User ID must be a valid integer.", "Invalid User ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_userId.Clear();
                    txt_userId.Focus();
                    return;
                }

                using (SqlConnection con = Database_Connection.GetConnection())
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM loginUser WHERE userId = @userId", con);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No record found with the provided User ID.", "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            txt_userId.Clear();
            txt_password.Clear();
            txt_conPassword.Clear();
            radio_admin.Checked = false;
            radio_user.Checked = false;
        }

        private void btn_newAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Database_Connection.GetConnection())
                {
                    con.Open();

                    SqlCommand latestUserIdCmd = new SqlCommand("SELECT MAX(userId) FROM loginUser", con);
                    object result = latestUserIdCmd.ExecuteScalar();

                    int latestUserId = result != null && result != DBNull.Value ? (int)result : 0;

                    int newUserId = latestUserId + 1;

                    txt_userId.Text = newUserId.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picture_search_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txt_search.Text, out int userIdToSearch))
                {
                    MessageBox.Show("Please enter a valid User ID for searching.", "Invalid User ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection con = Database_Connection.GetConnection())
                {
                    con.Open();

                    SqlCommand searchCmd = new SqlCommand("SELECT * FROM loginUser WHERE userId = @userId", con);
                    searchCmd.Parameters.AddWithValue("@userId", userIdToSearch);

                    SqlDataAdapter adapter = new SqlDataAdapter(searchCmd);
                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView_userData.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("No record found with the provided User ID.", "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dataGridView_userData.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
