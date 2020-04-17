using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;

namespace RevitConnMySQL
{
    public partial class DataWindows : Form
    {
        public DataWindows()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void 显示_Click(object sender, EventArgs e)
        {
            string myConnectionString;
            myConnectionString = "server=localhost;uid=root;password=****;port=3306;database=test01";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();
            string cmd = "SELECT * FROM revitDatas";
            MySql.Data.MySqlClient.MySqlCommand mscmd = new MySql.Data.MySqlClient.MySqlCommand(cmd, conn);
            MySql.Data.MySqlClient.MySqlDataAdapter adpter = new MySql.Data.MySqlClient.MySqlDataAdapter(mscmd);
            DataTable table = new DataTable();
            adpter.Fill(table);
            dataGridView1.DataSource = table;
            conn.Close();
            conn.Dispose();
        }
    }
}
