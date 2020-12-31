using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tickets
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SQLClass.conn = new MySql.Data.MySqlClient.MySqlConnection();
            SQLClass.conn.ConnectionString =
                "Server=localhost;Database=trains;port=3306;User Id=root";
            SQLClass.conn.Open();
            Application.Run(new MainForm());

            SQLClass.conn.Close();
        }

        public static string Login = "Mikki";
    }
}
