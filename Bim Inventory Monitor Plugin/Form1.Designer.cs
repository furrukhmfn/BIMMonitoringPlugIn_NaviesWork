
namespace Bim_Inventory_Monitor_Plugin
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.export = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.StatusData = new System.Windows.Forms.DataGridView();
            this.SensorData1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.deletedatabasedata = new System.Windows.Forms.Button();
            this.SecondForm = new System.Windows.Forms.Button();
            this.SensorData2 = new System.Windows.Forms.Button();
            this.UpdateDatabase = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusData)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.label1.Location = new System.Drawing.Point(416, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(382, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "Bim Inventory Monitor PlugIn";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1, 86);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1347, 311);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // export
            // 
            this.export.Location = new System.Drawing.Point(14, 68);
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(250, 45);
            this.export.TabIndex = 0;
            this.export.Text = "Export Data";
            this.export.UseVisualStyleBackColor = true;
            this.export.Click += new System.EventHandler(this.export_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(250, 45);
            this.button1.TabIndex = 1;
            this.button1.Text = "Read Data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // StatusData
            // 
            this.StatusData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.StatusData.Location = new System.Drawing.Point(719, 0);
            this.StatusData.Name = "StatusData";
            this.StatusData.Size = new System.Drawing.Size(659, 210);
            this.StatusData.TabIndex = 2;
            this.StatusData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.StatusData_CellContentClick);
            // 
            // SensorData1
            // 
            this.SensorData1.Location = new System.Drawing.Point(294, 17);
            this.SensorData1.Name = "SensorData1";
            this.SensorData1.Size = new System.Drawing.Size(250, 45);
            this.SensorData1.TabIndex = 3;
            this.SensorData1.Text = "Google Sheet Data";
            this.SensorData1.UseVisualStyleBackColor = true;
            this.SensorData1.Click += new System.EventHandler(this.SensorData1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.UpdateDatabase);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.deletedatabasedata);
            this.panel1.Controls.Add(this.SecondForm);
            this.panel1.Controls.Add(this.SensorData2);
            this.panel1.Controls.Add(this.SensorData1);
            this.panel1.Controls.Add(this.StatusData);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.export);
            this.panel1.Location = new System.Drawing.Point(-2, 439);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1350, 222);
            this.panel1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(15, 171);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(250, 39);
            this.button2.TabIndex = 7;
            this.button2.Text = "Clear Data From Data Grid";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // deletedatabasedata
            // 
            this.deletedatabasedata.Location = new System.Drawing.Point(294, 119);
            this.deletedatabasedata.Name = "deletedatabasedata";
            this.deletedatabasedata.Size = new System.Drawing.Size(250, 45);
            this.deletedatabasedata.TabIndex = 6;
            this.deletedatabasedata.Text = "Delete Old Data";
            this.deletedatabasedata.UseVisualStyleBackColor = true;
            this.deletedatabasedata.Click += new System.EventHandler(this.deletedatabasedata_Click);
            // 
            // SecondForm
            // 
            this.SecondForm.Location = new System.Drawing.Point(15, 120);
            this.SecondForm.Name = "SecondForm";
            this.SecondForm.Size = new System.Drawing.Size(250, 45);
            this.SecondForm.TabIndex = 5;
            this.SecondForm.Text = "Open 2nd Form";
            this.SecondForm.UseVisualStyleBackColor = true;
            this.SecondForm.Click += new System.EventHandler(this.SecondForm_Click);
            // 
            // SensorData2
            // 
            this.SensorData2.Location = new System.Drawing.Point(294, 68);
            this.SensorData2.Name = "SensorData2";
            this.SensorData2.Size = new System.Drawing.Size(250, 45);
            this.SensorData2.TabIndex = 4;
            this.SensorData2.Text = "Database Reading";
            this.SensorData2.UseVisualStyleBackColor = true;
            this.SensorData2.Click += new System.EventHandler(this.SensorData2_Click);
            // 
            // UpdateDatabase
            // 
            this.UpdateDatabase.Location = new System.Drawing.Point(294, 170);
            this.UpdateDatabase.Name = "UpdateDatabase";
            this.UpdateDatabase.Size = new System.Drawing.Size(250, 39);
            this.UpdateDatabase.TabIndex = 8;
            this.UpdateDatabase.Text = "Update Database";
            this.UpdateDatabase.UseVisualStyleBackColor = true;
            this.UpdateDatabase.Click += new System.EventHandler(this.UpdateDatabase_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 661);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Naviswork PlugIn";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusData)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button export;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.DataGridView StatusData;
        private System.Windows.Forms.Button SensorData1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button SensorData2;
        private System.Windows.Forms.Button SecondForm;
        private System.Windows.Forms.Button deletedatabasedata;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button UpdateDatabase;
    }
}