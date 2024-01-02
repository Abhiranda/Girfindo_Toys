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
    public partial class Employee_Compenent : Form
    {
        public Employee_Compenent() 
        {
            InitializeComponent();
            dataGridView_empData.SelectionChanged += dataGridView_empData_SelectionChanged;
            DataGridViewSalary();
        }


        private void pictureBox3_Click(object sender, EventArgs e)
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

                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM employeeData", con);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView_empData.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while refreshing the DataGridView: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        Employee_compenent ec = new Employee_compenent();

        private bool userInputFormatCheck()
        {
            bool invalidInput = false;

            if (!ec.textOnly(txt_fName.Text.Trim()))
            {
                errorProvider1.SetError(txt_fName, "Name can only contain letters");
                invalidInput = true;
            }

            if (!ec.textOnly(text_ln.Text.Trim()))
            {
                errorProvider1.SetError(text_ln, "Name can only contain letters");
                invalidInput = true;
            }
            return invalidInput;
        }

        private void dataGridView_empData_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView_empData.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView_empData.SelectedRows[0];

                txt_empId.Text = selectedRow.Cells["empId"].Value.ToString();
                txt_fName.Text = selectedRow.Cells["firstName"].Value.ToString();
                text_ln.Text = selectedRow.Cells["lastName"].Value.ToString();
                text_or.Text = selectedRow.Cells["Overtime_Rate"].Value.ToString();
                text_E.Text = selectedRow.Cells["email"].Value.ToString();
                text_ms.Text = selectedRow.Cells["salary"].Value.ToString();
                text_la.Text = selectedRow.Cells["leavesAllowed"].Value.ToString();
                text_A.Text = selectedRow.Cells["allowances"].Value.ToString();
                text_H.Text = selectedRow.Cells["holidays"].Value.ToString();

            }
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();

            if (userInputFormatCheck())
            {
                MessageBox.Show("Invalid Input Formats", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else
            {

                try
                {
                    using (SqlConnection con = Database_Connection.GetConnection())
                    {
                        con.Open();

                        SqlCommand cmd = new SqlCommand("INSERT INTO employeeData (empId,firstName,lastName,Overtime_Rate, email, salary, leavesAllowed, allowances, holidays) " +
                            "VALUES (@empId, @firstName, @lastName, @Overtime_Rate, @email, @salary, @leavesAllowed, @allowances, @holidays)", con);
                        cmd.Parameters.AddWithValue("@empId", int.Parse(txt_empId.Text));
                        cmd.Parameters.AddWithValue("@firstName", txt_fName.Text);
                        cmd.Parameters.AddWithValue("@lastName", text_ln.Text);
                        cmd.Parameters.AddWithValue("@Overtime_Rate", decimal.Parse(text_or.Text));
                        cmd.Parameters.AddWithValue("@email", text_E.Text);
                        cmd.Parameters.AddWithValue("@salary", decimal.Parse(text_ms.Text));
                        cmd.Parameters.AddWithValue("@leavesAllowed", int.Parse(text_la.Text));
                        cmd.Parameters.AddWithValue("@allowances", decimal.Parse(text_A.Text));
                        cmd.Parameters.AddWithValue("@holidays", text_H.Text);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Record saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();

            if (userInputFormatCheck())
            {
                MessageBox.Show("Invalid Input Formats", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else
            {

                try
                {
                    using (SqlConnection con = Database_Connection.GetConnection())
                    {
                        con.Open();

                        SqlCommand cmd = new SqlCommand("UPDATE employeeData SET firstName = @firstName, lastName = @lastName, Overtime_Rate = @Overtime_Rate, " +
                            "email = @email, salary = @salary, leavesAllowed = @leavesAllowed, allowances = @allowances, " +
                            "holidays = @holidays WHERE empId = @empId", con);

                        cmd.Parameters.AddWithValue("@empId", int.Parse(txt_empId.Text));
                        cmd.Parameters.AddWithValue("@firstName", txt_fName.Text);
                        cmd.Parameters.AddWithValue("@lastName", text_ln.Text);
                        cmd.Parameters.AddWithValue("@Overtime_Rate", decimal.Parse(text_or.Text));
                        cmd.Parameters.AddWithValue("@email", text_E.Text);
                        cmd.Parameters.AddWithValue("@salary", decimal.Parse(text_ms.Text));
                        cmd.Parameters.AddWithValue("@leavesAllowed", int.Parse(text_la.Text));
                        cmd.Parameters.AddWithValue("@allowances", decimal.Parse(text_A.Text));
                        cmd.Parameters.AddWithValue("@holidays", text_H.Text);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No record found with the provided Employee ID.", "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            txt_empId.Clear();
            txt_fName.Clear();
            text_ln.Clear();
            text_or.Clear();
            text_E.Clear();
            text_ms.Clear();
            text_la.Clear();
            text_A.Clear();
            text_H.Clear();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Database_Connection.GetConnection())
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM employeeData WHERE empId = @empId", con);
                    cmd.Parameters.AddWithValue("@empId", int.Parse(txt_empId.Text));

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No record found with the provided Employee ID.", "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
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
                if (!int.TryParse(txt_search.Text, out int empIdToSearch))
                {
                    MessageBox.Show("Please enter a valid Employee ID for searching.", "Invalid Employee ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection con = Database_Connection.GetConnection())
                {
                    con.Open();

                    SqlCommand searchCmd = new SqlCommand("SELECT * FROM employeeData WHERE empId = @empId", con);
                    searchCmd.Parameters.AddWithValue("@empId", empIdToSearch);

                    SqlDataAdapter adapter = new SqlDataAdapter(searchCmd);
                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView_empData.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("No record found with the provided Employee ID.", "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dataGridView_empData.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Database_Connection.GetConnection())
                {
                    con.Open();

                    SqlCommand latestEmpIdCmd = new SqlCommand("SELECT MAX(empId) FROM employeeData", con);
                    object result = latestEmpIdCmd.ExecuteScalar();

                    int latestEmpId = result != null && result != DBNull.Value ? (int)result : 0;
                    int newEmpId = latestEmpId + 1;

                    txt_empId.Text = newEmpId.ToString();
                    txt_fName.Clear();
                    text_ln.Clear();
                    text_or.Clear();
                    text_E.Clear();
                    text_ms.Clear();
                    text_la.Clear();
                    text_A.Clear();
                    text_H.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
