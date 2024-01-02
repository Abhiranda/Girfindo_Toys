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
    public partial class Setting_Compenent : Form
    { 
        public Setting_Compenent()
        {
            InitializeComponent();
            dataGridView_settingData.SelectionChanged += dataGridView_settingData_SelectionChanged;
            DataGridViewSalary();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }


        Setting_compenent sc = new Setting_compenent();

        private bool userInputFormatCheck()
        {
            bool invalidInput = false;

            if (!sc.numbersOnly(txt_dataranage.Text.Trim()))
            {
                errorProvider1.SetError(txt_dataranage, "Can only contain numbers");
                invalidInput = true;
            }

            if (!sc.numbersOnly(text_oh.Text.Trim()))
            {
                errorProvider1.SetError(text_oh, "Can only contain numbers");
                invalidInput = true;
            }

            if (!sc.numbersOnly(text_gtaxrate.Text.Trim()))
            {
                errorProvider1.SetError(text_gtaxrate, "Can only contain numbers");
                invalidInput = true;
            }

            return invalidInput;
        }

        private void DataGridViewSalary()
        {
            try
            {
                using (SqlConnection con = Database_Connection.GetConnection())
                {
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM setting", con);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView_settingData.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while refreshing the DataGridView: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_settingData_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView_settingData.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView_settingData.SelectedRows[0];

                txt_settingid.Text = selectedRow.Cells["settingId"].Value.ToString();
                empid.Text = selectedRow.Cells["empId"].Value.ToString();
                txt_dataranage.Text = selectedRow.Cells["sal_cy_date_range"].Value.ToString();
                text_sd.Text = selectedRow.Cells["sal_cy_st_date"].Value.ToString();
                text_ed.Text = selectedRow.Cells["sal_cy_end_date"].Value.ToString();
                text_ad.Text = selectedRow.Cells["absentDays"].Value.ToString();
                text_leavesT.Text = selectedRow.Cells["leavesTaken"].Value.ToString();
                text_oh.Text = selectedRow.Cells["OTHours"].Value.ToString();
                text_gtaxrate.Text = selectedRow.Cells["taxRate"].Value.ToString();
            }
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
                    using (SqlConnection con = Database_Connection.GetConnection())
                    {
                        con.Open();

                        SqlCommand cmd = new SqlCommand("INSERT INTO setting (settingId, empId, sal_cy_date_range, sal_cy_st_date, sal_cy_end_date, absentDays, leavesTaken, OTHours, taxRate) " +
                            "VALUES (@settingId, @empId, @sal_cy_date_range, @sal_cy_st_date, @sal_cy_end_date, @absentDays, @leavesTaken, @OTHours, @taxRate)", con);

                        cmd.Parameters.AddWithValue("@settingId", int.Parse(txt_settingid.Text));
                        cmd.Parameters.AddWithValue("@empId", int.Parse(empid.Text));
                        cmd.Parameters.AddWithValue("@sal_cy_date_range", int.Parse(txt_dataranage.Text));
                        cmd.Parameters.AddWithValue("@sal_cy_st_date", DateTime.Parse(text_sd.Text));
                        cmd.Parameters.AddWithValue("@sal_cy_end_date", DateTime.Parse(text_ed.Text));
                        cmd.Parameters.AddWithValue("@absentDays", text_ad.Text);
                        cmd.Parameters.AddWithValue("@leavesTaken", text_leavesT.Text);
                        cmd.Parameters.AddWithValue("@OTHours", text_oh.Text);
                        cmd.Parameters.AddWithValue("@taxRate", int.Parse(text_gtaxrate.Text));

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

                        SqlCommand cmd = new SqlCommand("UPDATE setting SET empId = @empId, sal_cy_date_range = @sal_cy_date_range, " +
                            "sal_cy_st_date = @sal_cy_st_date, sal_cy_end_date = @sal_cy_end_date, absentDays = @absentDays, " +
                            "leavesTaken = @leavesTaken, OTHours = @OTHours, taxRate = @taxRate WHERE settingId = @settingId", con);

                        cmd.Parameters.AddWithValue("@settingId", int.Parse(txt_settingid.Text));
                        cmd.Parameters.AddWithValue("@empId", int.Parse(empid.Text));
                        cmd.Parameters.AddWithValue("@sal_cy_date_range", int.Parse(txt_dataranage.Text));
                        cmd.Parameters.AddWithValue("@sal_cy_st_date", DateTime.Parse(text_sd.Text));
                        cmd.Parameters.AddWithValue("@sal_cy_end_date", DateTime.Parse(text_ed.Text));
                        cmd.Parameters.AddWithValue("@absentDays", text_ad.Text);
                        cmd.Parameters.AddWithValue("@leavesTaken", text_leavesT.Text);
                        cmd.Parameters.AddWithValue("@OTHours", text_oh.Text);
                        cmd.Parameters.AddWithValue("@taxRate", int.Parse(text_gtaxrate.Text));

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No record found with the provided Setting ID.", "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            txt_settingid.Clear();
            empid.Clear();
            txt_dataranage.Clear();
            text_sd.Clear();
            text_ed.Clear();
            text_ad.Clear();
            text_leavesT.Clear();
            text_oh.Clear();
            text_gtaxrate.Clear();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Database_Connection.GetConnection())
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM setting WHERE settingId = @settingId", con);
                    cmd.Parameters.AddWithValue("@settingId", int.Parse(txt_settingid.Text));

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No record found with the provided Setting ID.", "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (!int.TryParse(txt_search.Text, out int settingIdToSearch))
                {
                    MessageBox.Show("Please enter a valid Setting ID for searching.", "Invalid Setting ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection con = Database_Connection.GetConnection())
                {
                    con.Open();

                    SqlCommand searchCmd = new SqlCommand("SELECT * FROM setting WHERE settingId = @settingId", con);
                    searchCmd.Parameters.AddWithValue("@settingId", settingIdToSearch);

                    SqlDataAdapter adapter = new SqlDataAdapter(searchCmd);
                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView_settingData.DataSource = dt;
                        DataRow row = dt.Rows[0];
                        txt_settingid.Text = row["settingId"].ToString();
                        empid.Text = row["empId"].ToString();
                        txt_dataranage.Text = row["sal_cy_date_range"].ToString();
                        text_sd.Text = row["sal_cy_st_date"].ToString();
                        text_ed.Text = row["sal_cy_end_date"].ToString();
                        text_ad.Text = row["absentDays"].ToString();
                        text_leavesT.Text = row["leavesTaken"].ToString();
                        text_oh.Text = row["OTHours"].ToString();
                        text_gtaxrate.Text = row["taxRate"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No record found with the provided Setting ID.", "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dataGridView_settingData.DataSource = null;
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

                    SqlCommand latestSettingIdCmd = new SqlCommand("SELECT MAX(settingId) FROM setting", con);
                    object result = latestSettingIdCmd.ExecuteScalar();

                    int latestSettingId = result != null && result != DBNull.Value ? (int)result : 0;
                    int newSettingId = latestSettingId + 1;

                    txt_settingid.Text = newSettingId.ToString();
                    empid.Clear();
                    txt_dataranage.Clear();
                    text_sd.Clear();
                    text_ed.Clear();
                    text_ad.Clear();
                    text_leavesT.Clear();
                    text_oh.Clear();
                    text_gtaxrate.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            text_sd.Text = dateTimePicker1.Value.ToString("yyyy-MM-dd");
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            text_ed.Text = dateTimePicker2.Value.ToString("yyyy-MM-dd");

        }

        private void Setting_Compenent_Load(object sender, EventArgs e)
        {

        }
    }
}
