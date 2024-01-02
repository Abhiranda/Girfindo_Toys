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
    public partial class Salary_Compenent : Form
    { 
        public Salary_Compenent()
        {
            InitializeComponent();
            DataGridViewSalary();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        Setting_compenent sc1 = new Setting_compenent();

        private bool userInputFormatCheck()
        {
            bool invalidInput = false;

            if (!sc1.numbersOnly(txt_settingid.Text.Trim()))
            {
                errorProvider1.SetError(txt_settingid, "Setting ID can only contain number");
                invalidInput = true;
            }         

            return invalidInput;
        }

        private void button_add_Click(object sender, EventArgs e)
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
                    int recordId = int.Parse(text_recordid.Text);
                    int settingId = int.Parse(txt_settingid.Text);

                    decimal salary = decimal.Parse(text_monthlysalaery.Text);
                    decimal OTRate = decimal.Parse(text_overtimerate.Text);
                    decimal allowances = decimal.Parse(text_allownce.Text);
                    decimal taxRate = decimal.Parse(text_govermenttaxrate.Text);
                    int salCyDateRange = int.Parse(text_salaryCycleDataRange.Text);
                    int absentDays = int.Parse(text_absendday.Text);
                    decimal OTHours = decimal.Parse(text_overtimehours.Text);

                    decimal basePay = salary + allowances + (OTRate * OTHours);
                    decimal OTPay = OTRate * OTHours;
                    decimal calculatedTax = (basePay * taxRate) / 100;
                    decimal noPay = ((salary + allowances) / salCyDateRange) * absentDays;
                    decimal grossPay = (basePay - (noPay + calculatedTax));

                    using (SqlConnection con = Database_Connection.GetConnection())
                    {
                        con.Open();

                        SqlCommand cmd = new SqlCommand("INSERT INTO salary (recordId, settingId, basePay, OTPay, tax, noPay, grossPay) " +
                            "VALUES (@recordId, @settingId, @basePay, @OTPay, @tax, @noPay, @grossPay)", con);

                        cmd.Parameters.AddWithValue("@recordId", recordId);
                        cmd.Parameters.AddWithValue("@settingId", settingId);
                        cmd.Parameters.AddWithValue("@basePay", basePay);
                        cmd.Parameters.AddWithValue("@OTPay", OTPay);
                        cmd.Parameters.AddWithValue("@tax", calculatedTax);
                        cmd.Parameters.AddWithValue("@noPay", noPay);
                        cmd.Parameters.AddWithValue("@grossPay", grossPay);

                        cmd.ExecuteNonQuery();

                        DataGridViewSalary();

                        MessageBox.Show("Record saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DataGridViewSalary()
        {
            try
            {
                using (SqlConnection con = Database_Connection.GetConnection())
                {
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM salary", con);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView_salary.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while refreshing the DataGridView: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    int recordId = int.Parse(text_recordid.Text);
                    int settingId = int.Parse(txt_settingid.Text);

                    decimal salary = decimal.Parse(text_monthlysalaery.Text);
                    decimal OTRate = decimal.Parse(text_overtimerate.Text);
                    decimal allowances = decimal.Parse(text_allownce.Text);
                    decimal taxRate = decimal.Parse(text_govermenttaxrate.Text);
                    int salCyDateRange = int.Parse(text_salaryCycleDataRange.Text);
                    int absentDays = int.Parse(text_absendday.Text);
                    decimal OTHours = decimal.Parse(text_overtimehours.Text);

                    decimal basePay = salary + allowances + (OTRate * OTHours);
                    decimal OTPay = OTRate * OTHours;
                    decimal calculatedTax = (basePay * taxRate) / 100;
                    decimal noPay = ((salary + allowances) / salCyDateRange) * absentDays;
                    decimal grossPay = (basePay - (noPay + calculatedTax));

                    using (SqlConnection con = Database_Connection.GetConnection())
                    {
                        con.Open();

                        SqlCommand cmd = new SqlCommand("UPDATE salary SET settingId = @settingId, basePay = @basePay, OTPay = @OTPay, " +
                            "tax = @tax, noPay = @noPay, grossPay = @grossPay WHERE recordId = @recordId", con);

                        cmd.Parameters.AddWithValue("@recordId", recordId);
                        cmd.Parameters.AddWithValue("@settingId", settingId);
                        cmd.Parameters.AddWithValue("@basePay", basePay);
                        cmd.Parameters.AddWithValue("@OTPay", OTPay);
                        cmd.Parameters.AddWithValue("@tax", calculatedTax);
                        cmd.Parameters.AddWithValue("@noPay", noPay);
                        cmd.Parameters.AddWithValue("@grossPay", grossPay);

                        cmd.ExecuteNonQuery();

                        DataGridViewSalary();

                        MessageBox.Show("Record updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            text_recordid.Clear();
            txt_settingid.Clear();
            text_employeeid.Clear();
            text_salaryCycleDataRange.Clear();
            text_monthlysalaery.Clear();
            text_overtimerate.Clear();
            text_allownce.Clear();
            text_absendday.Clear();
            text_leavestaken.Clear();
            text_overtimehours.Clear();
            text_govermenttaxrate.Clear();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                int recordId = int.Parse(text_recordid.Text);

                using (SqlConnection con = Database_Connection.GetConnection())
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM salary WHERE recordId = @recordId", con);
                    cmd.Parameters.AddWithValue("@recordId", recordId);

                    cmd.ExecuteNonQuery();

                    DataGridViewSalary();

                    MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (!int.TryParse(txt_search.Text, out int recIdToSearch))
                {
                    MessageBox.Show("Please enter a valid Record ID for searching.", "Invalid Record ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection con = Database_Connection.GetConnection())
                {
                    con.Open();

                    SqlCommand searchCmd = new SqlCommand("SELECT * FROM salary WHERE recordId = @recordId", con);
                    searchCmd.Parameters.AddWithValue("@recordId", recIdToSearch);

                    SqlDataAdapter adapter = new SqlDataAdapter(searchCmd);
                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView_salary.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("No record found with the provided Record ID.", "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dataGridView_salary.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Database_Connection.GetConnection())
                {
                    con.Open();

                    SqlCommand latestRecordIdCmd = new SqlCommand("SELECT MAX(recordId) FROM salary", con);
                    object result = latestRecordIdCmd.ExecuteScalar();

                    int latestRecordId = result != null && result != DBNull.Value ? (int)result : 0;
                    int newRecordId = latestRecordId + 1;

                    text_recordid.Text = newRecordId.ToString();
                    txt_settingid.Clear();
                    text_employeeid.Clear();
                    text_absendday.Clear();
                    text_leavestaken.Clear();
                    text_overtimehours.Clear();
                    text_govermenttaxrate.Clear();
                    text_allownce.Clear();
                    text_salaryCycleDataRange.Clear();
                    text_overtimerate.Clear();
                    text_monthlysalaery.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_search_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txt_settingid.Text, out int settingIdToSearch))
                {
                    MessageBox.Show("Please enter a valid Setting ID for searching.", "Invalid Setting ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection con = Database_Connection.GetConnection())
                {
                    con.Open();

                    SqlCommand searchCmd = new SqlCommand("SELECT * FROM setting WHERE settingId = @settingId", con);
                    searchCmd.Parameters.AddWithValue("@settingId", settingIdToSearch);

                    SqlDataReader reader = searchCmd.ExecuteReader();

                    if (reader.Read())
                    {
                        text_absendday.Text = reader["absentDays"].ToString();
                        text_leavestaken.Text = reader["leavesTaken"].ToString();
                        text_overtimehours.Text = reader["OTHours"].ToString();
                        text_govermenttaxrate.Text = reader["taxRate"].ToString();
                        text_salaryCycleDataRange.Text = reader["sal_cy_date_range"].ToString();
                        text_employeeid.Text = reader["empId"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No record found with the provided Setting ID.", "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        text_absendday.Clear();
                        text_leavestaken.Clear();
                        text_overtimehours.Clear();
                        text_govermenttaxrate.Clear();
                        text_salaryCycleDataRange.Clear();
                        text_employeeid.Clear();
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_2search_Click(object sender, EventArgs e)
        {
            try
            {
                int empIdToSearch = 0;

                if (!int.TryParse(text_employeeid.Text, out empIdToSearch))
                {
                    MessageBox.Show("Please enter a valid Employee ID for searching.", "Invalid Employee ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection con = Database_Connection.GetConnection())
                {
                    con.Open();

                    SqlCommand empSearchCmd = new SqlCommand("SELECT * FROM employeeData WHERE empId = @empId", con);
                    empSearchCmd.Parameters.AddWithValue("@empId", empIdToSearch);

                    SqlDataReader empReader = empSearchCmd.ExecuteReader();

                    if (empReader.Read())
                    {
                        text_monthlysalaery.Text = empReader["salary"].ToString();
                        text_overtimerate.Text = empReader["Overtime_Rate"].ToString();
                        text_allownce.Text = empReader["allowances"].ToString();
                        text_employeeid.Text = empReader["empId"].ToString();

                        empReader.Close();
                    }
                    else
                    {
                        MessageBox.Show("No record found with the provided Employee ID.", "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        text_monthlysalaery.Clear();
                        text_overtimerate.Clear();
                        text_allownce.Clear();
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
