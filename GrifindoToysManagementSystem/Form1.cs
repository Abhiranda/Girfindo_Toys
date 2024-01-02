using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GrifindoToysManagementSystem.Login;

namespace GrifindoToysManagementSystem
{
    public partial class Form1 : Form
    {
        public Form1() 
        {
            InitializeComponent();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Login ll = new Login();
            ll.Show();
            this.Hide();
        }
       

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            User_Regestration ur = new User_Regestration();
            ur.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox4.Enabled = true;

            if (Employee.userType.ToLower() == "user")
            {
                pictureBox4.Enabled = false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Employee_Compenent ec = new Employee_Compenent();
            ec.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Setting_Compenent sc = new Setting_Compenent();
            sc.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Salary_Compenent sc2 = new Salary_Compenent();
            sc2.Show();
            this.Hide();
        }
    }
}
