using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.Entity;

namespace Bim_Inventory_Monitor_Plugin
{
    class database
    {
        public SQLiteConnection DatabaseConncetion;
        
        public void database_checking()
        {
            DatabaseConncetion = new SQLiteConnection("Data Source = PlugInDatabase.db");
            if (File.Exists("./PlugInDatabase.db"))
            {
                SQLiteConnection.CreateFile("PlugInDatabase.db");
            }
        }
    }
}
