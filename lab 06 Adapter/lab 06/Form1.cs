using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab_06
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        void main()
        {
            var mysqlAdapter = new MySQLAdapter();
            var client = new Client(mysqlAdapter);
            client.ConnectToDatabase("mysql://localhost/mydatabase");
            client.QueryDatabase("SELECT * FROM mytable");
            var results = client.GetResults();

            var postgresqlAdapter = new PostgreSQLAdapter();
            client = new Client(postgresqlAdapter);
            client.ConnectToDatabase("postgresql://localhost/mydatabase");
            client.QueryDatabase("SELECT * FROM mytable");
            results = client.GetResults();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "mysql://localhost/mydatabase";
            string query = textBox1.Text;

            // підключення до бд
            IData data;
            if (connectionString.StartsWith("mysql"))
            {
                data = new MySQLAdapter();
            }
            else if (connectionString.StartsWith("postgresql"))
            {
                data = new PostgreSQLAdapter();
            }
            else
            {
                throw new Exception("Невідомий формат бази даних.");
            }
            var client = new Client(data);
            client.ConnectToDatabase(connectionString);

            //запит
            client.QueryDatabase(query);
            var results = client.GetResults();

            
            dataGridView1.DataSource = results;
        }
    }
}
