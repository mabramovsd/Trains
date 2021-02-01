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
    public partial class OrderForm : UserControl
    {
        string RunId;
        string CityFrom;
        string CityTo;
        public OrderForm(string runId, string cityFrom, string cityTo)
        {
            InitializeComponent();
            if (MainForm.pages.Count > MainForm.pagePos + 1)
                MainForm.pages.RemoveRange(MainForm.pagePos + 1, MainForm.pages.Count - MainForm.pagePos - 1);
            MainForm.pages.Add(this);
            MainForm.pagePos++;

            RunId = runId;
            CityFrom = cityFrom;
            CityTo = cityTo;

            int x = 50;
            int y = 80;
            List<string> trainData = SQLClass.Select("SELECT Places FROM Trains" +
                " WHERE Id = (SELECT TrainId FROM Runs WHERE Id = " + RunId + ")");
            for (int i = 1; i <= Convert.ToInt32(trainData[0]); i++)
            {
                Button btn = new Button();
                btn.Location = new Point(x, y);
                btn.Size = new Size(50, 30);
                btn.Text = i.ToString();
                string disabled = SQLClass.Select("SELECT COUNT(*) FROM Orders" +
                    " WHERE RunId = " + RunId + " AND Place = " + i.ToString())[0];
                btn.Enabled = (disabled == "0");
                btn.Click += new EventHandler(MakeOrder);

                Controls.Add(btn);

                x += 100;
                if (x + 100 >= Width)
                {
                    x = 50;
                    y += 50;
                }
            }
        }

        void MakeOrder(object sender, EventArgs e)
        {
            if (Program.Login == "")
            {
                MessageBox.Show("Вы не вошли в систему!");
                return;
            }

            Button btn = (Button)sender;
            SQLClass.Insert("INSERT INTO Orders(Login, RunId, Place, CityFrom, CityTo)" +
                " VALUES('" + Program.Login + "', " + RunId + ", " + btn.Text + ", " + CityFrom + ", " + CityTo + ")");
            MessageBox.Show("Сделано");
            btn.Enabled = false;
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
