using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace ConnectionMySQL
{
    class Connection
    {
        private static void DisplayData(System.Data.DataTable table)
        {
            foreach(System.Data.DataRow row in table.Rows)
            {
                foreach(System.Data.DataColumn col in table.Columns)
                {
                    Console.WriteLine("{0}={1}", col.ColumnName, row[col]);
                }
                Console.WriteLine("======================");
            }
        }
        static void CreatNewDatabase(string dbSource,string dbUID,string dbpwd,string dbName)
        {
            MySqlConnection con = new MySqlConnection("server=" + dbSource + ";uid=" + dbUID + ";password=" + dbpwd + ";");
            MySqlCommand cmd = new MySqlCommand("CREAT SCHEMA" + dbName, con);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            
        }
        static void CreatNewDataTable(string dbSource, string dbUID, string dbPwd, string dbName,string tbName)
        {
            MySqlConnection con = new MySqlConnection("server=" + dbSource + ";Persist Security Info=yes;uid=" + dbUID + ";password=" + dbPwd + ";");
            string newTableCMD = " use " + dbName + ";create TABLE " + tbName + "(vend_id int not null auto_increment,vend_name char(50) not null,primary key(vend_id))engine = innodb;";
            MySqlCommand cmd = new MySqlCommand(newTableCMD, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        static void Main(string[] args)
        {
            MySqlConnection conn;
            string myConnectionString;
            //创建数据库
            //CreatNewDatabase("localhost", "root", "X.517469", "test02");
            //创建数据表
            //CreatNewDataTable("localhost", "root", "X.517469", "test01","Tables02");
            myConnectionString = "server=localhost;uid=root;password=X.517469;port=3306;database=test01";
            try
            {
                conn = new MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Tables02 ";
                //cmd.CommandType = System.Data.CommandType.TableDirect;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("{0}-->{1}",reader.GetString("prod_name"),reader.GetString("prod_id"));
                }
                conn.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Done, ");
            double a = -2.055624156;
            Console.ReadLine();
        }
    }
}
