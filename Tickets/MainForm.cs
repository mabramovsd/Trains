﻿using System;
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
    public partial class MainForm : Form
    {
        public static Panel mainPanel;

        public static List<UserControl> pages = new List<UserControl>();
        public static int pagePos = -1;

        public MainForm()
        {
            InitializeComponent();
            mainPanel = panel1;

            TicketsList rf = new TicketsList();
            panel1.Controls.Clear();
            panel1.Controls.Add(rf);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegisterForm rf = new RegisterForm();
            panel1.Controls.Clear();
            panel1.Controls.Add(rf);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminForm rf = new AdminForm();
            rf.Dock = DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(rf);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Выберите пункт отправления");
                return;
            }
            else if (comboBox2.Text == "")
            {
                MessageBox.Show("Выберите пункт назначения");
                return;
            }

            string dateFrom = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string dateTo = dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd");

            string CityFrom = 
                SQLClass.Select("SELECT Id FROM cities WHERE Name = '" + comboBox1.Text + "'")[0];
            string CityTo = 
                SQLClass.Select("SELECT Id FROM cities WHERE Name = '" + comboBox2.Text + "'")[0];

            List<string> trains = SQLClass.Select(
                "SELECT runs.TrainId, trains.Name, " +
                "'" + comboBox1.Text + "', " +
                "'" + comboBox2.Text + "', " +
                " (SELECT ADDTIME(DT, TimeStart) FROM routes WHERE City = " + CityFrom + " AND trainId = runs.trainId) TimeCity1," +
                " (SELECT ADDTIME(DT, TimeStart) FROM routes WHERE City = " + CityTo + "  AND trainId = runs.trainId) TimeCity2, " +
                " runs.Id" +
                " FROM runs" +
                " JOIN trains ON trains.Id = runs.trainId" +
                " HAVING TimeCity1<TimeCity2 AND" +
                " TimeCity1 BETWEEN '" + dateFrom + "' AND '" + dateTo + "'");

            Image img = Image.FromFile("../../Pictures/TrainBtn.png");
            int x = 10;
            int y = 10;
            TrainsPanel.Controls.Clear();
            for (int i = 0; i < trains.Count; i += 7)
            {
                Label lbl = new Label();
                lbl.Text =
                    Environment.NewLine +
                    Environment.NewLine +
                    Environment.NewLine +
                    "  " + "Поезд № " + trains[i + 1] + Environment.NewLine +
                    "  " + trains[i + 2] + " - " + trains[i + 3] + Environment.NewLine +
                    "  " + "Отправление: " + Environment.NewLine +
                    "  " + trains[i + 4] + Environment.NewLine +
                    "  " + "Прибытие: " + Environment.NewLine +
                    "  " + trains[i + 5];
                lbl.Location = new Point(x, y);
                lbl.Size = new Size(200, 160);
                lbl.Font = new Font("Arial", 11);
                lbl.Tag = trains[i + 6];
                lbl.Image = img;
                lbl.Click += new EventHandler(TrainClick);
                TrainsPanel.Controls.Add(lbl);



                x += 250;
                if (x + 200 > Width)
                {
                    x = 10;
                    y += 180;
                }
            }
        }

        private void TrainClick(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            string CityFrom = SQLClass.Select("SELECT Id FROM Cities" +
                " WHERE Name = '" + comboBox1.Text + "'")[0];
            string CityTo = SQLClass.Select("SELECT Id FROM Cities" +
                " WHERE Name = '" + comboBox2.Text + "'")[0];

            OrderForm of = new OrderForm(lbl.Tag.ToString(), CityFrom, CityTo);
            of.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<string> cities = SQLClass.Select(
                "SELECT Name FROM cities ORDER BY Name");
            comboBox1.Items.AddRange(cities.ToArray());
            comboBox2.Items.AddRange(cities.ToArray());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string pass = textBox2.Text;

            string existUser = SQLClass.Select(
                "SELECT COUNT(*) FROM Users WHERE Login ='" + login + "'")[0];
            string existUserWithPass = 
                SQLClass.Select("SELECT COUNT(*) FROM Users WHERE Login = '" + login + 
                "' AND Password = '" + pass + "'")[0];

            if (existUser == "0")
            {
                MessageBox.Show("Вы не зарегистрированы!");
                return;
            }
            else if (existUserWithPass == "0")
            {
                MessageBox.Show("Неверный пароль!");
                return;
            }
            else
            {
                textBox1.Visible = false;
                textBox2.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
            }

            Program.Login = login;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pagePos--;
            panel1.Controls.Clear();
            panel1.Controls.Add(pages[pagePos]);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pagePos++;
            panel1.Controls.Clear();
            panel1.Controls.Add(pages[pagePos]);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button3.Visible = (Program.Login == "Admin");
            pictureBox1.Visible = (pagePos > 0);
            pictureBox2.Visible = (pagePos < pages.Count - 1);
        }
    }
}
