using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;
// Navies Work Api Files
using Autodesk.Navisworks.Api.Timeliner;
using Autodesk.Navisworks.Api.Plugins;
using Autodesk.Navisworks.Api;
using Nw = Autodesk.Navisworks.Api;
using Tl = Autodesk.Navisworks.Api.Timeliner;
using System.Data.Entity;
using System.IO;
using Autodesk.Navisworks.Api.DocumentParts;
using Autodesk.Navisworks.Api.Data;
using System.Threading;
using IronPython.Hosting;
using System.Diagnostics;
using System.Globalization;

namespace Bim_Inventory_Monitor_Plugin
{
    public partial class Form1 : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();

        public SQLiteConnection DatabaseConncetion;
        // Adding Required Function's
        public FolderItem TasksRoot { get; }
        public SavedItemCollection DataSources { get; }
        public SavedItemCollection Tasks { get; }

        public String DumpValues()
        {

            return "0";

        }
        public Form1()
        {
            InitializeComponent();

            // Data Grid Code 
            dataGridView1.ColumnCount = 15;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "Acitivity Name";
            dataGridView1.Columns[2].Name = "Planned Model Volume";
            dataGridView1.Columns[3].Name = "Planned Cement Volume";
            dataGridView1.Columns[4].Name = "Planned Cement Kg";
            dataGridView1.Columns[5].Name = "Planned No. Of Bags";
            dataGridView1.Columns[6].Name = "Start Date";
            dataGridView1.Columns[7].Name = "End Date";
            dataGridView1.Columns[8].Name = "Current Date";
            dataGridView1.Columns[9].Name = "Remianing Stock of Cement Kg";
            dataGridView1.Columns[10].Name = "Remaining No. of Bags";
            dataGridView1.Columns[11].Name = "Material Used Today";
            dataGridView1.Columns[12].Name = "Required For Completion of Activity";
            dataGridView1.Columns[13].Name = "Required for Completion of Activity Bags";
            dataGridView1.Columns[14].Name = "Commulative Cement Used kg";
            // Other Features with this Data Grid


            // 2nd Data Grid Codes
            StatusData.ColumnCount = 2;
            StatusData.Columns[0].Name = "Error Location";
            StatusData.Columns[1].Name = "Error";
            StatusData.Columns[0].Width = 200;
            StatusData.Columns[1].Width = 459;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Document oDoc = Autodesk.Navisworks.Api.Application.ActiveDocument;
            Nw.Document doc = Nw.Application.ActiveDocument;
            Nw.DocumentParts.IDocumentTimeliner tl = doc.Timeliner;
            Tl.DocumentTimeliner tl_doc = (Tl.DocumentTimeliner)tl;
            foreach (Tl.TimelinerDataSource oDataSource in tl_doc.DataSources)
            {
                //Under Construction

            }
            foreach (Tl.TimelinerTask oTask in tl_doc.Tasks)
            {
                string TaskDisplayName = oTask.DisplayName;
                string TaskStartDate = oTask.PlannedStartDate.ToString();
                double TaskVolume = 0;
                string TaskEndDate = oTask.PlannedEndDate.ToString();
                double PlannedCementVoume = 0;
                double PlannedCementKg = 0;
                double PlannedNumberOfBags = 0;
                try
                {
                    // Using Explicit Selection
                   
                    Autodesk.Navisworks.Api.Timeliner.TimelinerSelection ModelData = oTask.Selection;
                    ModelItemCollection Hello =  ModelData.ExplicitSelection;
                    ModelItemCollection ModelItem = ModelData.GetSelectedItems(oDoc);
                    Autodesk.Navisworks.Api.BoundingBox3D BoundedArea = Hello.BoundingBox();
                    TaskVolume = (BoundedArea.Volume);
                    string[] Data = { "Task Volume - Volume", TaskVolume.ToString() };
                    StatusData.Rows.Add(Data);
                    PlannedCementVoume = (TaskVolume * (1 / 7));
                }
                catch (Exception ex)
                {
                    string[] Data = { "ModelData Collection - Volume", ex.ToString() };
                    StatusData.Rows.Add(Data);
                    // Not Required Right Now
                }
                PlannedCementKg = (PlannedCementVoume * 43);
                PlannedNumberOfBags = (PlannedCementVoume / 50);
                PlannedNumberOfBags = Math.Ceiling(PlannedNumberOfBags);
                string StartDate;
                string EndDate;
                string CurrentDate;
                string TaskVolumestr = TaskVolume.ToString();
                string PlannedCementVoumestr = PlannedCementVoume.ToString();
                string PlannedCementKgstr = PlannedCementKg.ToString();
                string PlannedNumberOfBagsstr = PlannedNumberOfBags.ToString();
                try
                {
                    DocumentDatabase database = Autodesk.Navisworks.Api.Application.ActiveDocument.Database;
                    using (NavisworksTransaction trans = database.BeginTransaction(DatabaseChangedAction.Reset))
                    {
                        // Creation of Table
                        NavisworksCommand cmd = trans.Connection.CreateCommand();
                        string sql = "CREATE TABLE IF NOT EXISTS PlugInData1(" + "id INTEGER, " + "ActivityName TEXT," + "PlannedModelVolume REAL," + "PlannedCementVolume REAL," + "PlannedCementKg REAL," + "PlannedNumberOfBags REAL," + "StartDate TEXT," + "FinishDate TEXT," + "CurrentDate TEXT," + "RemainingStock REAL," + "RemainingNumberOfBags REAL," + "MaterialUsedTodayKg REAL," + "MaterialRequiredForCompletionOfActivity REAL," + "MaterialRequiredForComletionOfActivtyBags REAL," + "CommulativeCementUsedKg REAL," + "PRIMARY KEY(" + "id AUTOINCREMENT" + "))";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        trans.Commit();
                    }
                    // Addition of  Data 
                    using (NavisworksTransaction trans = database.BeginTransaction(DatabaseChangedAction.Reset))
                    {
                        // Reading Values From the txt file
                        string Path = @"C:\Program Files\Autodesk\Navisworks Manage 2017\Plugins\Bim Inventory Monitor Plugin\GoogleSheets\Values.txt";
                        string[] FileData = File.ReadAllLines(Path);
                        int n = 0;
                        String WeightStr = "0";
                        String Datestr = "0";
                        foreach (string line in FileData)
                        {
                            if (n == 0)
                            {
                                Datestr = line;
                            }
                            else
                            {
                                WeightStr = line;
                            }
                            n++;
                        }
                        string dateString, format;
                        DateTime result;
                        CultureInfo provider = CultureInfo.InvariantCulture;
                        // Convesrion of Datestr and Weight Into Required Format
                        string format1 = "dd/MM/yyyy HH:mm:ss";
                        DateTime Date = DateTime.ParseExact(Datestr, format1,provider );
                        Double Weight = Convert.ToDouble(WeightStr);
                        Weight = Math.Abs(Weight); 
                        Double RemaningStock = 0;
                        DateTime StartDate1 = Convert.ToDateTime(TaskStartDate);
                        DateTime EndDate1 = Convert.ToDateTime(TaskEndDate);
                        int Result1 = DateTime.Compare(Date, StartDate1);
                        int Result2 = DateTime.Compare(Date, EndDate1);
                        if (Result1 > 0 && Result2 < 0)
                        {
                            RemaningStock = Weight;
                        }
                        // MaterialUsedToday
                        // Using Switch Case
                        // Getting Old 
                        NavisworksCommand cmd = trans.Connection.CreateCommand();
                        string sql = "INSERT INTO PlugInData1(ActivityName, StartDate, FinishDate, PlannedModelVolume,CurrentDate,RemainingStock)  VALUES ('" + TaskDisplayName + "','" + TaskStartDate + "','" + TaskEndDate + "','" + TaskVolume + "','" + DateTime.Now.ToString() + "','" + RemaningStock.ToString() + "')";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        trans.Commit();
                    }
                    // Reading of Data From Sqlite Database
                    using (NavisworksTransaction trans = database.BeginTransaction(DatabaseChangedAction.Reset))
                    {
                        NavisworksDataAdapter dataAdapter = new NavisworksDataAdapter("SELECT * FROM PlugInData1 WHERE ActivityName = '" + TaskDisplayName + "' ORDER BY id DESC LIMIT 2 ", database.Value);
                        NavisworksCommand cmd = trans.Connection.CreateCommand();
                        DataTable table12 = new DataTable();
                        dataAdapter.Fill(table12);
                        Double OldRemainingStock = 0;
                        // First Result Data 
                        Double RemainingStock;
                        Double RemaingBags;
                        Double MaterialUsedToday;
                        Double RequiredForCompleteionofActivityKg;
                        Double RequiredForCompleteionofActivityBags;
                        Double PlannedCementVolume;
                        string SqliteTaskName;
                        int Id = 0;
                        DataRow row123 = table12.Rows[0];
                        Id = Convert.ToInt16(row123[0]);
                        SqliteTaskName = row123[1].ToString();
                        TaskVolume = Convert.ToDouble(row123[2]);
                        PlannedCementVolume = TaskVolume * 1 / 7;
                        PlannedCementKg = (PlannedCementVolume * 43);
                        PlannedNumberOfBags = (PlannedCementVolume / 50);
                        PlannedNumberOfBags = Math.Ceiling(PlannedNumberOfBags);
                        StartDate = row123[6].ToString();
                        EndDate = row123[7].ToString();
                        CurrentDate = row123[8].ToString();
                        RemainingStock = Convert.ToDouble(row123[9]);
                        RemaingBags = RemainingStock / 50;
                        MaterialUsedToday = 0;
                        RequiredForCompleteionofActivityKg = PlannedCementKg - MaterialUsedToday;
                        RequiredForCompleteionofActivityBags = RequiredForCompleteionofActivityKg / 50;
                        if (table12.Rows.Count >1)
                        {
                            DataRow row11 = table12.Rows[0];
                            DataRow row12 = table12.Rows[1];
                            OldRemainingStock = Convert.ToDouble(row12[9]);
                            Id = Convert.ToInt16(row11[0]);
                            SqliteTaskName = row11[1].ToString();
                            TaskVolume = Convert.ToDouble(row11[2]);
                            PlannedCementVolume = TaskVolume * 1 / 7;
                            PlannedCementKg = (PlannedCementVolume * 43);
                            PlannedNumberOfBags = (PlannedCementKg / 50);
                            PlannedNumberOfBags = Math.Ceiling(PlannedNumberOfBags);
                            StartDate = row11[6].ToString();
                            EndDate = row11[7].ToString();
                            CurrentDate = row11[8].ToString();
                            RemainingStock = Convert.ToDouble(row11[9]);
                            RemaingBags = RemainingStock / 50;
                            MaterialUsedToday =   OldRemainingStock - RemainingStock;
                            RequiredForCompleteionofActivityKg = PlannedCementKg - MaterialUsedToday;
                            RequiredForCompleteionofActivityBags = RequiredForCompleteionofActivityKg / 50;
                        }


                        // Display of Data

                        NavisworksDataAdapter dataAdapter1 = new NavisworksDataAdapter("SELECT * FROM PlugInData1", database.Value);
                        NavisworksCommand cmd1 = trans.Connection.CreateCommand();
                        DataTable table1 = new DataTable();
                        dataAdapter.Fill(table1);
                        string sql1 = "SELECT * FROM PlugInData1";
                        cmd.CommandText = sql1;
                        NavisWorksDataReader sQLiteData1 = cmd.ExecuteReader();
                        trans.Commit();
                        Double CummulativeCemeentUsed = 0;
                        var row1234 = table1.Rows[0];
                        Double JustAVaraible = 0;
                        foreach(DataRow row101 in table1.Rows)
                        {
                            CummulativeCemeentUsed = JustAVaraible - Convert.ToDouble(row101[9]);
                            JustAVaraible = Convert.ToDouble(row101[9]);
                        }
                        // Under Construction
                        string[] row = { Id.ToString(), SqliteTaskName, TaskVolume.ToString(), PlannedCementVolume.ToString(), PlannedCementKg.ToString(), PlannedNumberOfBags.ToString(), StartDate, EndDate, CurrentDate, RemainingStock.ToString(), RemaingBags.ToString(), MaterialUsedToday.ToString(), RequiredForCompleteionofActivityKg.ToString(), RequiredForCompleteionofActivityBags.ToString(), CummulativeCemeentUsed.ToString() };
                        dataGridView1.Rows.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    string[] Err = { "Database Error", ex.ToString() };
                    StatusData.Rows.Add(Err);
                }
                foreach (SavedItem I in oTask.Children)
                {
                    //string ChidernData = I.DisplayName;
                    //string ChildernStartDate = I.Equals("PlannedStartDate").ToString();
                    //string ChildernEndDate = I.Equals("PlannedEndDate").ToString();
                    //string[] row1 = new string[] {ChidernData, ChildernStartDate, ChildernEndDate,"Not Specified","Under Construction"};
                    //dataGridView1.Rows.Add(row1);
                }

            }
            // Displaying Data of Sqlite in to Datagrid View
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void StatusData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SensorData1_Click(object sender, EventArgs e)
        {
            try
            {

                // Adding Process of Working
                string path = @"C:\Program Files\Autodesk\Navisworks Manage 2017\Plugins\Bim Inventory Monitor Plugin\GoogleSheets\GoogleSheetsCSharp.exe";
                Process proc = new Process();
                proc.StartInfo.FileName = path;
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.Verb = "runas";
                proc.Start();
                string[] Data = { "Senesor Data From Google Sheet", "Data Has been Updated " };
                StatusData.Rows.Add(Data);
            }
            catch (Exception ex)
            {
                string[] Data3 = { "Google Sheet - Error", ex.ToString() };
                StatusData.Rows.Add(Data3);
            }

        }

        private void SensorData2_Click(object sender, EventArgs e)
        {
            // Reading All Data From Database 
            try
            {
                // 
                Document oDoc = Autodesk.Navisworks.Api.Application.ActiveDocument;
                Nw.Document doc = Nw.Application.ActiveDocument;
                Nw.DocumentParts.IDocumentTimeliner tl = doc.Timeliner;
                Tl.DocumentTimeliner tl_doc = (Tl.DocumentTimeliner)tl;
                foreach (Tl.TimelinerDataSource oDataSource in tl_doc.DataSources)
                {
                    //Under Construction

                }
                Double CummulativeCemeentUsed = 0;
                foreach (Tl.TimelinerTask oTask in tl_doc.Tasks)
                {
                    DocumentDatabase database = Autodesk.Navisworks.Api.Application.ActiveDocument.Database;
                    using (NavisworksTransaction trans = database.BeginTransaction(DatabaseChangedAction.Reset))
                    {
                        NavisworksDataAdapter dataAdapter = new NavisworksDataAdapter("SELECT * FROM PlugInData1 WHERE ActivityName = '" + oTask.DisplayName+"'", database.Value);
                        NavisworksCommand cmd = trans.Connection.CreateCommand();
                        DataTable table = new DataTable();
                        dataAdapter.Fill(table);
                        // First Result Data 
                        double localreqiredcement;
                        Double OldRemainingStock = 0;
                        Double PlannedCementKg;
                        Double RemaingBags;
                        Double PlannedNumberOfBags;
                        Double MaterialUsedToday;
                        Double RequiredForCompleteionofActivityKg = 0 ;
                        Double RequiredForCompleteionofActivityBags;
                        Double PlannedCementVolume;
                        Double TaskVolume;
                        string StartDate;
                        string EndDate;
                        string SqliteTaskName;
                        string CurrentDate;
                        Double RemainingStock;
                        int Id;
                        int n = 0;
                        double hello = 0;
                        foreach (DataRow row11 in table.Rows)
                        {


                            Id = Convert.ToInt16(row11[0]);
                            SqliteTaskName = row11[1].ToString();
                            if (SqliteTaskName == "New Data Source (Root)" || SqliteTaskName == "Data Source (Root)" || SqliteTaskName == "New Data Source" || SqliteTaskName == "Data Source")
                            {
                                continue;
                            }
                            if (n == 0)
                            {
                                OldRemainingStock = 0;
                            }
                            else
                            {
                                DataRow Nothing = table.Rows[n-1];
                                OldRemainingStock = Convert.ToDouble(Nothing[9]);
                            }
                            TaskVolume = Convert.ToDouble(row11[2]);
                            PlannedCementVolume = TaskVolume * 1 / 7;
                            PlannedCementKg = (PlannedCementVolume * 43);
                            PlannedNumberOfBags = (PlannedCementKg / 50);
                            PlannedNumberOfBags = Math.Ceiling(PlannedNumberOfBags);
                            StartDate = row11[6].ToString();
                            EndDate = row11[7].ToString();
                            CurrentDate = row11[8].ToString();
                            RemainingStock = Convert.ToDouble(row11[9]);
                            RemaingBags = RemainingStock / 50;

                            if (n == 0)
                            {
                                MaterialUsedToday = 0;
                            }
                            else
                            {
                                MaterialUsedToday = OldRemainingStock - RemainingStock;
                            }
                            if (n == 0)
                            {
                                RequiredForCompleteionofActivityKg = PlannedCementKg;
                            }
                            else
                            {
                                RequiredForCompleteionofActivityKg = RequiredForCompleteionofActivityKg - MaterialUsedToday;
                            }
                            hello = MaterialUsedToday;
                            CummulativeCemeentUsed = CummulativeCemeentUsed + hello;
                            RequiredForCompleteionofActivityBags = RequiredForCompleteionofActivityKg / 50;
                            string[] row = { Id.ToString(), SqliteTaskName, TaskVolume.ToString(), PlannedCementVolume.ToString(), PlannedCementKg.ToString(), PlannedNumberOfBags.ToString(), StartDate, EndDate, CurrentDate, RemainingStock.ToString(), RemaingBags.ToString(), MaterialUsedToday.ToString(), RequiredForCompleteionofActivityKg.ToString(), RequiredForCompleteionofActivityBags.ToString(), CummulativeCemeentUsed.ToString() };
                            dataGridView1.Rows.Add(row);
                            n++;
                        }
                        }
                    }
            }
            catch (Exception ex)
            {
                String Data1 = "Reading Database Function ";
                string Data2 = ex.ToString();
                string[] Error = { Data1, Data2 };
                StatusData.Rows.Add(Error);
            }
        }

        private void deletedatabasedata_Click(object sender, EventArgs e)
        {
            // Function or Code to Delete All data from Plugin 
            try
            {
                DocumentDatabase database = Autodesk.Navisworks.Api.Application.ActiveDocument.Database;
                using (NavisworksTransaction trans = database.BeginTransaction(DatabaseChangedAction.Reset))
                {
                    NavisworksCommand cmd = trans.Connection.CreateCommand();
                    string sql1 = "DROP TABLE PlugInData1";
                    cmd.CommandText = sql1;
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                    string[] data = { "Delete Button", "Database Deleted Sucessfully" };
                    StatusData.Rows.Add(data);
                }
            }
            catch (Exception ex)
            {
                // Error If Found
                string[] data = { "Delete Button - Error ", ex.ToString() };
                StatusData.Rows.Add(data);

            }

        }

        private void SecondForm_Click(object sender, EventArgs e)
        {
            CummulativeForm f2 = new CummulativeForm();
            f2.ShowDialog();
        }
        private void export_Click(object sender, EventArgs e)
        {
            // Converting Datatable into Csv File 
            try
            {
                DocumentDatabase database = Autodesk.Navisworks.Api.Application.ActiveDocument.Database;
                using (NavisworksTransaction trans = database.BeginTransaction(DatabaseChangedAction.Reset))
                {
                    NavisworksDataAdapter dataAdapter = new NavisworksDataAdapter("SELECT * FROM PlugInData1", database.Value);
                    NavisworksCommand cmd = trans.Connection.CreateCommand();
                    DataTableToCsv obj = new DataTableToCsv();
                    DataTable table = new DataTable();
                    dataAdapter.Fill(table);
                    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                    app.Visible = true;
                    worksheet = workbook.Sheets["Sheet1"];
                    worksheet = workbook.ActiveSheet;
                    worksheet.Name = "Exported Data";
                    for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                    {
                        worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                    }
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                    // save the application  
                    string path = @"C:\Program Files\Autodesk\Navisworks Manage 2017\Plugins\Bim Inventory Monitor Plugin\Data.xls";
                    workbook.SaveAs(path, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    // Exit from the application  
                    app.Quit();

                }
            }
            catch (Exception ex)
            {
                string[] Err = { "Conversion into CSV", ex.ToString() };
                StatusData.Rows.Add(Err);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        private void UpdateDatabase_Click(object sender, EventArgs e)
        {
            // Updating Database 
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Updating Database Values
                    DocumentDatabase database = Autodesk.Navisworks.Api.Application.ActiveDocument.Database;
                    using (NavisworksTransaction trans = database.BeginTransaction(DatabaseChangedAction.Reset))
                    {
                        
                        // Creation of Table
                        NavisworksCommand cmd = trans.Connection.CreateCommand();
                        string sql = "UPDATE PlugInData1 SET ActivityName = '" + row.Cells[1].Value + "', StartDate = '" + row.Cells[6].Value + "', FinishDate = '" + row.Cells[7].Value + "', PlannedModelVolume = '" + row.Cells[2].Value + "',CurrentDate = '" + row.Cells[8].Value + "',RemainingStock = '" + row.Cells[9].Value + "' WHERE id = '" + row.Cells[0].Value + "'";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        trans.Commit();
                    }

                }
            }
            catch (Exception ex)
            {
                string[] Error = { "Update Database Error ", ex.ToString() };
                StatusData.Rows.Add(Error);
            }
        }
    }


    class DataTableToCsv
    {

        public StringBuilder ConvertDataTableToCsvFile(DataTable dtData)
        {
            StringBuilder data = new StringBuilder();

            //Taking the column names.
            for (int column = 0; column < dtData.Columns.Count; column++)
            {
                //Making sure that end of the line, shoould not have comma delimiter.
                if (column == dtData.Columns.Count - 1)
                    data.Append(dtData.Columns[column].ColumnName.ToString().Replace(",", ";"));
                else
                    data.Append(dtData.Columns[column].ColumnName.ToString().Replace(",", ";") + ',');
            }

            data.Append(Environment.NewLine);//New line after appending columns.

            for (int row = 0; row < dtData.Rows.Count; row++)
            {
                for (int column = 0; column < dtData.Columns.Count; column++)
                {
                    ////Making sure that end of the line, shoould not have comma delimiter.
                    if (column == dtData.Columns.Count - 1)
                        data.Append(dtData.Rows[row][column].ToString().Replace(",", ";"));
                    else
                        data.Append(dtData.Rows[row][column].ToString().Replace(",", ";") + ',');
                }

                //Making sure that end of the file, should not have a new line.
                if (row != dtData.Rows.Count - 1)
                    data.Append(Environment.NewLine);
            }
            return data;
        }

        //This method saves the data to the csv file. 
        public void SaveData(StringBuilder data, string filePath)
        {
            using (StreamWriter objWriter = new StreamWriter(filePath))
            {
                objWriter.WriteLine(data);
            }
        }
    }

}
