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
        static string SetData(string TableName,List<int> ids,List<string> description,List<double> price)
        {
            string command = string.Empty;
            command += "CREATE TABLE " + TableName + "( data_id int NOT NULL AUTO_INCREMENT,data_desc char(50) NOT NULL,data_price double NOT NULL,PRIMARY KEY(data_id))ENGINE=InnoDB;"+"\r\n";
            

            for (int i = 0; i <= ids.Count - 1; i++)
            {
                command += "INSERT INTO " + TableName + "(data_id,data_desc,data_price) VALUES(" + ids[i] + ", '" + description[i] + "' ," + price[i] + ");"+"\r\n";
                Console.WriteLine(command);
            }
            return command;
        }
        
        static void Main(string[] args)
        {
            MySqlConnection conn;
            string myConnectionString;
            //创建数据库
            //CreatNewDatabase("localhost", "root", "****", "test02");
            //创建数据表
            //CreatNewDataTable("localhost", "root", "****", "test01","Tables03");
            string command = SetData("Tables03", new List<int>() { 111, 112, 113 }, new List<string>() { "fir", "sec", "third" }, new List<double>() { 1.00, 2.01, 3.04 });
            //Console.WriteLine(command);
            myConnectionString = "server=localhost;uid=root;password=****;port=3306;database=test01";
            try
            {
                conn = new MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = command + "SELECT * FROM Tables03";
                //cmd.CommandType = System.Data.CommandType.TableDirect;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("{0}-->{1}",reader.GetString("data_id"),reader.GetString("data_desc"));
                }
                conn.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Done, ");
  
            Console.ReadLine();
        }
    }
}
