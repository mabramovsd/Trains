using System;
using System.Data.Common;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Tickets
{
    public partial class RouteForm : Form
    {
        public RouteForm()
        {
            InitializeComponent();

            int y = 0;
            System.Collections.Generic.List<string> cities = SQLClass.Select(
                "SELECT City, TimeStart FROM `routes` WHERE TrainId = 7 ORDER BY TimeStart");
            
            for(int i = 0; i < cities.Count;i += 2)
            {
                Label lbl = new Label();
                lbl.Location = new Point(0, y);
                lbl.Size = new Size(300, 30);
                string city = 
                    SQLClass.Select("SELECT Name FROM cities WHERE Id = " + cities[i])[0];

                lbl.Text = city + " " + cities[i+1];
                Controls.Add(lbl);
                y += 30;
            }
        }

        private void RouteForm_Load(object sender, EventArgs e)
        {

        }
    }
}
