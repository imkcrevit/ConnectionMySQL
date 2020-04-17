using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI.Selection;
using MySql.Data.MySqlClient;

namespace RevitConnMySQL
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            var refer = uidoc.Selection.PickObjects(ObjectType.Element, "pick onjects");
            List<string> categorys = new List<string>();
            List<int> elementIds = new List<int>();
            List<string> symbolNames = new List<string>();
            foreach(Reference re in refer)
            {
                Element e = doc.GetElement(re);
                string symbolName = e.Name;
                string category = e.Category.Name;
                int elementId = e.Id.IntegerValue;
                symbolNames.Add(symbolName);
                categorys.Add(category);
                elementIds.Add(elementId);

            }
            MySQLConnection2 my = new MySQLConnection2(elementIds, symbolNames, categorys);
            DataWindows dw = new DataWindows();
            dw.ShowDialog();
            return Result.Succeeded;
        }
    }
     class MySQLConnection2
    {
        
        static string SetData(string TableName, List<int> ids, List<string> description, List<string> categorys)
        {
            string command = string.Empty;
            command += "CREATE TABLE " + TableName + "( data_id int NOT NULL AUTO_INCREMENT,data_desc char(50) NOT NULL,data_price char(50) NOT NULL,PRIMARY KEY(data_id))ENGINE=InnoDB;" + "\r\n";


            for (int i = 0; i <= ids.Count - 1; i++)
            {
                command += "INSERT INTO " + TableName + "(data_id,data_desc,data_price) VALUES(" + ids[i] + ", '" + description[i] + "' ,'" + categorys[i] + "');" + "\r\n";
                Console.WriteLine(command);
            }
            return command;
        }
        public MySQLConnection2(List<int> elementids,List<string> symbolNames,List<string> categorys)
        {
            MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=localhost;uid=root;password=****;port=3306;database=test01";
            try
            {
                conn = new MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                string command = SetData("revitDatas", elementids, symbolNames, categorys);
                MySqlCommand cmd = new MySqlCommand(command,conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                TaskDialog.Show("MysqlError",ex.Message);
            }
        }
    }
}
