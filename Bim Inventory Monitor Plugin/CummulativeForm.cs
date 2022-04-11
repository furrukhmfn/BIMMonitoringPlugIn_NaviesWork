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


namespace Bim_Inventory_Monitor_Plugin
{
    public partial class CummulativeForm : Form
    {
        public CummulativeForm()
        {
            InitializeComponent();
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "Activites";
            dataGridView1.Columns[1].Name = "Volume";
            dataGridView1.Columns[2].Name = "Remaining Stock";
            dataGridView1.Columns[3].Name = "Required Stock";

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

            private void CummulativeForm_Load(object sender, EventArgs e)
        {
            // Adding Values to The List Box 
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
                CummulativeBox.Items.Add(TaskDisplayName, CheckState.Unchecked);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Activites = "";
            Double Volume = 0;
            Double RemainingStockCement = 0;
            Double RequiredVolume = 0;
            // Checking  And Retriving Data
            try
            {
                foreach (int indexChecked in CummulativeBox.CheckedIndices)
                {
                    string ActName = CummulativeBox.Items[indexChecked].ToString();
                    DocumentDatabase database = Autodesk.Navisworks.Api.Application.ActiveDocument.Database;
                    using (NavisworksTransaction trans = database.BeginTransaction(DatabaseChangedAction.Reset))
                    {
                        NavisworksDataAdapter dataAdapter = new NavisworksDataAdapter("SELECT * FROM PlugInData1 WHERE ActivityName = '" + ActName + "' ORDER BY 'id' DESC LIMIT 1 ", database.Value);
                        NavisworksCommand cmd = trans.Connection.CreateCommand();
                        DataTable table = new DataTable();
                        dataAdapter.Fill(table);
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
                        DataRow row123 = table.Rows[0];
                        Id = Convert.ToInt16(row123[0]);
                        SqliteTaskName = row123[1].ToString();
                        Double TaskVolume = Convert.ToDouble(row123[2]);
                        PlannedCementVolume = TaskVolume * 1 / 7;
                        Double PlannedCementKg = (PlannedCementVolume * 43);
                        RemainingStock = Convert.ToDouble(row123[9]);
                        RemaingBags = RemainingStock / 50;
                        MaterialUsedToday = RemainingStock;
                        RequiredForCompleteionofActivityKg = PlannedCementKg - MaterialUsedToday;
                        RequiredForCompleteionofActivityBags = RequiredForCompleteionofActivityKg / 50;
                        Volume = Volume + TaskVolume;
                        RequiredVolume = RequiredVolume + RequiredForCompleteionofActivityKg;
                        RemainingStockCement = RemainingStockCement + RemainingStock;
                    }
                    Activites = Activites + " , " + ActName;
                }
            }
            catch (Exception ex)
            {
                // Printing Exception
                string[] Data1 = { "Database Loading - Error ", ex.ToString(), "", "" };
                dataGridView1.Rows.Add(Data1);
            }
            string[] Data = { Activites, Volume.ToString(), RemainingStockCement.ToString(), RequiredVolume.ToString() };
            dataGridView1.Rows.Add(Data);
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
