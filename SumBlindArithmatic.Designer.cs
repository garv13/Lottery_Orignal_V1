namespace SystamaticDBSearch
{
    partial class SumBlindArithmatic
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label6 = new System.Windows.Forms.Label();
            this.lblInternalDatabaseName = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rdExternal = new System.Windows.Forms.RadioButton();
            this.rdInternal = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.txtConstant = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbCode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbTruncate = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbApplyOn = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.searchGrid = new System.Windows.Forms.DataGridView();
            this.DBId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SUM_W = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.space1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.M1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.M2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.M3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.M4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.M5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SUM_M = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Database1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdSummary = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ForcastNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Database = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrawNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSummary)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(442, -1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(230, 25);
            this.label6.TabIndex = 20;
            this.label6.Text = "Sum Blind Arithmetic";
            // 
            // lblInternalDatabaseName
            // 
            this.lblInternalDatabaseName.AutoSize = true;
            this.lblInternalDatabaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInternalDatabaseName.ForeColor = System.Drawing.Color.Red;
            this.lblInternalDatabaseName.Location = new System.Drawing.Point(149, 5);
            this.lblInternalDatabaseName.Name = "lblInternalDatabaseName";
            this.lblInternalDatabaseName.Size = new System.Drawing.Size(177, 18);
            this.lblInternalDatabaseName.TabIndex = 16;
            this.lblInternalDatabaseName.Text = "Systamatic DB Search";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(75, 100);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(58, 23);
            this.btnSearch.TabIndex = 26;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(1, 100);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(62, 23);
            this.btnRefresh.TabIndex = 27;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(3, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(141, 605);
            this.panel1.TabIndex = 17;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(1, 147);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(137, 23);
            this.progressBar1.TabIndex = 30;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.rdExternal);
            this.panel3.Controls.Add(this.rdInternal);
            this.panel3.Location = new System.Drawing.Point(6, 43);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(130, 34);
            this.panel3.TabIndex = 29;
            // 
            // rdExternal
            // 
            this.rdExternal.AutoSize = true;
            this.rdExternal.Location = new System.Drawing.Point(60, 7);
            this.rdExternal.Name = "rdExternal";
            this.rdExternal.Size = new System.Drawing.Size(63, 17);
            this.rdExternal.TabIndex = 30;
            this.rdExternal.Text = "External";
            this.rdExternal.UseVisualStyleBackColor = true;
            // 
            // rdInternal
            // 
            this.rdInternal.AutoSize = true;
            this.rdInternal.Checked = true;
            this.rdInternal.Location = new System.Drawing.Point(2, 7);
            this.rdInternal.Name = "rdInternal";
            this.rdInternal.Size = new System.Drawing.Size(60, 17);
            this.rdInternal.TabIndex = 0;
            this.rdInternal.TabStop = true;
            this.rdInternal.Text = "Internal";
            this.rdInternal.UseVisualStyleBackColor = true;
            this.rdInternal.CheckedChanged += new System.EventHandler(this.rdInternal_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtConstant);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.cmbCode);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.cmbTruncate);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cmbApplyOn);
            this.panel2.Location = new System.Drawing.Point(3, 327);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(136, 147);
            this.panel2.TabIndex = 28;
            this.panel2.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 113);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(154, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Can put +ve , 0 , or -ve number";
            // 
            // txtConstant
            // 
            this.txtConstant.Location = new System.Drawing.Point(69, 86);
            this.txtConstant.Name = "txtConstant";
            this.txtConstant.Size = new System.Drawing.Size(100, 20);
            this.txtConstant.TabIndex = 7;
            this.txtConstant.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Constant";
            // 
            // cmbCode
            // 
            this.cmbCode.FormattingEnabled = true;
            this.cmbCode.Items.AddRange(new object[] {
            "RN",
            "CN",
            "TN",
            "TCN"});
            this.cmbCode.Location = new System.Drawing.Point(69, 60);
            this.cmbCode.Name = "cmbCode";
            this.cmbCode.Size = new System.Drawing.Size(100, 21);
            this.cmbCode.TabIndex = 5;
            this.cmbCode.Text = "RN";
            this.cmbCode.SelectedIndexChanged += new System.EventHandler(this.cmbCode_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Use ";
            // 
            // cmbTruncate
            // 
            this.cmbTruncate.FormattingEnabled = true;
            this.cmbTruncate.Items.AddRange(new object[] {
            "First Digit (H)",
            "Middle Digit (T)",
            "Last Digit (U)"});
            this.cmbTruncate.Location = new System.Drawing.Point(69, 34);
            this.cmbTruncate.Name = "cmbTruncate";
            this.cmbTruncate.Size = new System.Drawing.Size(100, 21);
            this.cmbTruncate.TabIndex = 3;
            this.cmbTruncate.Text = "First Digit (H)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Truncate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Apply on";
            // 
            // cmbApplyOn
            // 
            this.cmbApplyOn.FormattingEnabled = true;
            this.cmbApplyOn.Items.AddRange(new object[] {
            "WSUM",
            "MSUM"});
            this.cmbApplyOn.Location = new System.Drawing.Point(69, 8);
            this.cmbApplyOn.Name = "cmbApplyOn";
            this.cmbApplyOn.Size = new System.Drawing.Size(100, 21);
            this.cmbApplyOn.TabIndex = 0;
            this.cmbApplyOn.Text = "WSUM";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Search Option";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // searchGrid
            // 
            this.searchGrid.AllowUserToAddRows = false;
            this.searchGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.searchGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DBId,
            this.SNo,
            this.Date,
            this.W1,
            this.W2,
            this.W3,
            this.W4,
            this.W5,
            this.SUM_W,
            this.space1,
            this.M1,
            this.M2,
            this.M3,
            this.M4,
            this.M5,
            this.SUM_M,
            this.Database1});
            this.searchGrid.Location = new System.Drawing.Point(145, 27);
            this.searchGrid.Name = "searchGrid";
            this.searchGrid.RowHeadersWidth = 25;
            this.searchGrid.Size = new System.Drawing.Size(850, 381);
            this.searchGrid.TabIndex = 18;
            this.searchGrid.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.searchGrid_CellPainting);
            // 
            // DBId
            // 
            this.DBId.DataPropertyName = "DBId";
            this.DBId.HeaderText = "DBId";
            this.DBId.Name = "DBId";
            this.DBId.ReadOnly = true;
            this.DBId.Visible = false;
            // 
            // SNo
            // 
            this.SNo.DataPropertyName = "SNo";
            this.SNo.HeaderText = "";
            this.SNo.Name = "SNo";
            this.SNo.ReadOnly = true;
            this.SNo.Width = 40;
            // 
            // Date
            // 
            this.Date.DataPropertyName = "Date";
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.Width = 70;
            // 
            // W1
            // 
            this.W1.DataPropertyName = "W1";
            this.W1.HeaderText = "W1";
            this.W1.Name = "W1";
            this.W1.Width = 50;
            // 
            // W2
            // 
            this.W2.DataPropertyName = "W2";
            this.W2.HeaderText = "W2";
            this.W2.Name = "W2";
            this.W2.Width = 50;
            // 
            // W3
            // 
            this.W3.DataPropertyName = "W3";
            this.W3.HeaderText = "W3";
            this.W3.Name = "W3";
            this.W3.Width = 50;
            // 
            // W4
            // 
            this.W4.DataPropertyName = "W4";
            this.W4.HeaderText = "W4";
            this.W4.Name = "W4";
            this.W4.Width = 50;
            // 
            // W5
            // 
            this.W5.DataPropertyName = "W5";
            this.W5.HeaderText = "W5";
            this.W5.Name = "W5";
            this.W5.Width = 50;
            // 
            // SUM_W
            // 
            this.SUM_W.DataPropertyName = "SUM_W";
            this.SUM_W.HeaderText = "SUM";
            this.SUM_W.Name = "SUM_W";
            this.SUM_W.ReadOnly = true;
            this.SUM_W.Width = 50;
            // 
            // space1
            // 
            this.space1.HeaderText = "";
            this.space1.Name = "space1";
            this.space1.ReadOnly = true;
            this.space1.Width = 50;
            // 
            // M1
            // 
            this.M1.DataPropertyName = "M1";
            this.M1.HeaderText = "M1";
            this.M1.Name = "M1";
            this.M1.Width = 50;
            // 
            // M2
            // 
            this.M2.DataPropertyName = "M2";
            this.M2.HeaderText = "M2";
            this.M2.Name = "M2";
            this.M2.Width = 50;
            // 
            // M3
            // 
            this.M3.DataPropertyName = "M3";
            this.M3.HeaderText = "M3";
            this.M3.Name = "M3";
            this.M3.Width = 50;
            // 
            // M4
            // 
            this.M4.DataPropertyName = "M4";
            this.M4.HeaderText = "M4";
            this.M4.Name = "M4";
            this.M4.Width = 50;
            // 
            // M5
            // 
            this.M5.DataPropertyName = "M5";
            this.M5.HeaderText = "M5";
            this.M5.Name = "M5";
            this.M5.Width = 50;
            // 
            // SUM_M
            // 
            this.SUM_M.DataPropertyName = "SUM_M";
            this.SUM_M.HeaderText = "SUM";
            this.SUM_M.Name = "SUM_M";
            this.SUM_M.ReadOnly = true;
            this.SUM_M.Width = 50;
            // 
            // Database1
            // 
            this.Database1.DataPropertyName = "Database";
            this.Database1.HeaderText = "Database";
            this.Database1.Name = "Database1";
            // 
            // grdSummary
            // 
            this.grdSummary.AllowUserToAddRows = false;
            this.grdSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.ForcastNum,
            this.Database,
            this.DrawNo,
            this.Description,
            this.fDate});
            this.grdSummary.Location = new System.Drawing.Point(145, 438);
            this.grdSummary.Name = "grdSummary";
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdSummary.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdSummary.RowTemplate.Height = 40;
            this.grdSummary.Size = new System.Drawing.Size(850, 183);
            this.grdSummary.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(149, 415);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 18);
            this.label8.TabIndex = 22;
            this.label8.Text = "Summery";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(811, 412);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(126, 23);
            this.btnExport.TabIndex = 28;
            this.btnExport.Text = "Export To Excell";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(3, 200);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(127, 23);
            this.btnSave.TabIndex = 31;
            this.btnSave.Text = "Save Forcast Result";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            this.Id.Width = 75;
            // 
            // ForcastNum
            // 
            this.ForcastNum.DataPropertyName = "ForcastNum";
            this.ForcastNum.HeaderText = "Forcast Num";
            this.ForcastNum.Name = "ForcastNum";
            this.ForcastNum.ReadOnly = true;
            // 
            // Database
            // 
            this.Database.DataPropertyName = "Database";
            this.Database.HeaderText = "Database";
            this.Database.Name = "Database";
            this.Database.ReadOnly = true;
            this.Database.Width = 200;
            // 
            // DrawNo
            // 
            this.DrawNo.DataPropertyName = "DrawNo";
            this.DrawNo.HeaderText = "Draw No";
            this.DrawNo.Name = "DrawNo";
            this.DrawNo.ReadOnly = true;
            this.DrawNo.Width = 75;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 400;
            // 
            // fDate
            // 
            this.fDate.DataPropertyName = "fDate";
            this.fDate.HeaderText = "Date";
            this.fDate.Name = "fDate";
            this.fDate.ReadOnly = true;
            this.fDate.Visible = false;
            // 
            // SumBlindArithmatic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 629);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.grdSummary);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblInternalDatabaseName);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.searchGrid);
            this.Name = "SumBlindArithmatic";
            this.Text = "SumBlindArithmatic";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SumBlindArithmatic_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSummary)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblInternalDatabaseName;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView searchGrid;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbApplyOn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTruncate;
        private System.Windows.Forms.ComboBox cmbCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtConstant;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView grdSummary;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton rdExternal;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rdInternal;
        private System.Windows.Forms.DataGridViewTextBoxColumn DBId;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn W1;
        private System.Windows.Forms.DataGridViewTextBoxColumn W2;
        private System.Windows.Forms.DataGridViewTextBoxColumn W3;
        private System.Windows.Forms.DataGridViewTextBoxColumn W4;
        private System.Windows.Forms.DataGridViewTextBoxColumn W5;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUM_W;
        private System.Windows.Forms.DataGridViewTextBoxColumn space1;
        private System.Windows.Forms.DataGridViewTextBoxColumn M1;
        private System.Windows.Forms.DataGridViewTextBoxColumn M2;
        private System.Windows.Forms.DataGridViewTextBoxColumn M3;
        private System.Windows.Forms.DataGridViewTextBoxColumn M4;
        private System.Windows.Forms.DataGridViewTextBoxColumn M5;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUM_M;
        private System.Windows.Forms.DataGridViewTextBoxColumn Database1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ForcastNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Database;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrawNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn fDate;
    }
}