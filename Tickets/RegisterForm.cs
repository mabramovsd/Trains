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
    public partial class RegisterForm : UserControl
    {
        public RegisterForm()
        {
            InitializeComponent();
            if (MainForm.pages.Count > MainForm.pagePos + 1)
                MainForm.pages.RemoveRange(MainForm.pagePos + 1, MainForm.pages.Count - MainForm.pagePos - 1);
            MainForm.pages.Add(this);
            MainForm.pagePos++;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string registered = SQLClass.Select(
                "SELECT COUNT(*) FROM Users WHERE Login = '" + loginTB.Text + "'")[0];

            if (registered != "0")
            {
                MessageBox.Show("Вы уже зарегистрированы!");
                return;
            }

            SQLClass.Insert("INSERT INTO Users(Login, Name, Password) VALUES(" + 
                "'" + loginTB.Text + "', '" + fioTB.Text + "', '" + passTB.Text + "')");
            MessageBox.Show("Теперь можно входить в систему");

            //Close();
        }
    }
}
