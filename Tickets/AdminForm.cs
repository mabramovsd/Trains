using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tickets
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminTrainsForm atf = new AdminTrainsForm();
            atf.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminBookingForm atf = new AdminBookingForm();
            atf.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminRunsForm atf = new AdminRunsForm();
            atf.Show();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            AdminUsersForm atf = new AdminUsersForm();
            atf.Show();
        }
    }
}
