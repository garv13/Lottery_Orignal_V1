namespace SystamaticDBSearch
{
    partial class YearPattern
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label6 = new System.Windows.Forms.Label();
            this.lblInternalDatabaseName = new System.Windows.Forms.Label();
            this.searchGrid = new System.Windows.Forms.DataGridView();
            this.SNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SUM_W = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.space1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.space2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.M1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.M2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.M3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.M4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.M5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SUM_M = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DBId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblSummary = new System.Windows.Forms.Label();
            this.grdSummary = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sum1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sum2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Database1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdSpanSummary = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spanRecNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fsDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fsNumbers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdSpanMod = new System.Windows.Forms.RadioButton();
            this.rdSpanCode = new System.Windows.Forms.RadioButton();
            this.btnSearchNext = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.rdGroupSummation = new System.Windows.Forms.RadioButton();
            this.rdSpanPattern = new System.Windows.Forms.RadioButton();
            this.rdYearPattern = new System.Windows.Forms.RadioButton();
            this.grdSummaryGroupSummation = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Database = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Conclusion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Numbers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnExport = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdExternal = new System.Windows.Forms.RadioButton();
            this.rdInternal = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rdMachine = new System.Windows.Forms.RadioButton();
            this.rdWinning = new System.Windows.Forms.RadioButton();
            this.btnSaveResult = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.searchGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSpanSummary)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSummaryGroupSummation)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(380, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 25);
            this.label6.TabIndex = 14;
            this.label6.Text = "  Search";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // lblInternalDatabaseName
            // 
            this.lblInternalDatabaseName.AutoSize = true;
            this.lblInternalDatabaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInternalDatabaseName.ForeColor = System.Drawing.Color.Red;
            this.lblInternalDatabaseName.Location = new System.Drawing.Point(203, 9);
            this.lblInternalDatabaseName.Name = "lblInternalDatabaseName";
            this.lblInternalDatabaseName.Size = new System.Drawing.Size(177, 18);
            this.lblInternalDatabaseName.TabIndex = 13;
            this.lblInternalDatabaseName.Text = "Systamatic DB Search";
            // 
            // searchGrid
            // 
            this.searchGrid.AllowUserToAddRows = false;
            this.searchGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.searchGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SNo,
            this.Date,
            this.W1,
            this.W2,
            this.W3,
            this.W4,
            this.W5,
            this.SUM_W,
            this.space1,
            this.space2,
            this.M1,
            this.M2,
            this.M3,
            this.M4,
            this.M5,
            this.SUM_M,
            this.RecNo,
            this.DBId});
            this.searchGrid.Location = new System.Drawing.Point(94, 63);
            this.searchGrid.Name = "searchGrid";
            this.searchGrid.RowHeadersWidth = 25;
            this.searchGrid.Size = new System.Drawing.Size(890, 401);
            this.searchGrid.TabIndex = 15;
            this.searchGrid.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.searchGrid_CellPainting);
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
            this.space1.DataPropertyName = "space1";
            this.space1.HeaderText = "";
            this.space1.Name = "space1";
            this.space1.ReadOnly = true;
            this.space1.Width = 50;
            // 
            // space2
            // 
            this.space2.DataPropertyName = "space2";
            this.space2.HeaderText = "";
            this.space2.Name = "space2";
            this.space2.ReadOnly = true;
            this.space2.Visible = false;
            this.space2.Width = 50;
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
            // RecNo
            // 
            this.RecNo.DataPropertyName = "RecNo";
            this.RecNo.HeaderText = "RecNo";
            this.RecNo.Name = "RecNo";
            this.RecNo.ReadOnly = true;
            this.RecNo.Visible = false;
            // 
            // DBId
            // 
            this.DBId.DataPropertyName = "DBId";
            this.DBId.HeaderText = "DBId";
            this.DBId.Name = "DBId";
            this.DBId.ReadOnly = true;
            this.DBId.Visible = false;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(644, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(60, 23);
            this.btnSearch.TabIndex = 28;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblSummary
            // 
            this.lblSummary.AutoSize = true;
            this.lblSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummary.Location = new System.Drawing.Point(115, 467);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(79, 18);
            this.lblSummary.TabIndex = 30;
            this.lblSummary.Text = "Summery";
            // 
            // grdSummary
            // 
            this.grdSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Year,
            this.Sum1,
            this.Sum2,
            this.Database1});
            this.grdSummary.Location = new System.Drawing.Point(114, 495);
            this.grdSummary.Name = "grdSummary";
            this.grdSummary.Size = new System.Drawing.Size(750, 129);
            this.grdSummary.TabIndex = 29;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // Year
            // 
            this.Year.DataPropertyName = "Year";
            this.Year.HeaderText = "Year";
            this.Year.Name = "Year";
            this.Year.ReadOnly = true;
            // 
            // Sum1
            // 
            this.Sum1.DataPropertyName = "PairNo";
            this.Sum1.HeaderText = "Pair found at Draw";
            this.Sum1.Name = "Sum1";
            this.Sum1.ReadOnly = true;
            this.Sum1.Width = 200;
            // 
            // Sum2
            // 
            this.Sum2.DataPropertyName = "SearchNo";
            this.Sum2.HeaderText = "Search No found at Draw";
            this.Sum2.Name = "Sum2";
            this.Sum2.ReadOnly = true;
            this.Sum2.Width = 200;
            // 
            // Database1
            // 
            this.Database1.DataPropertyName = "Database";
            this.Database1.HeaderText = "Database";
            this.Database1.Name = "Database1";
            this.Database1.ReadOnly = true;
            this.Database1.Width = 200;
            // 
            // grdSpanSummary
            // 
            this.grdSpanSummary.AllowUserToAddRows = false;
            this.grdSpanSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSpanSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.spanRecNo,
            this.fsDate,
            this.fsNumbers});
            this.grdSpanSummary.Location = new System.Drawing.Point(114, 495);
            this.grdSpanSummary.Name = "grdSpanSummary";
            this.grdSpanSummary.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdSpanSummary.RowTemplate.Height = 40;
            this.grdSpanSummary.Size = new System.Drawing.Size(806, 129);
            this.grdSpanSummary.TabIndex = 31;
            this.grdSpanSummary.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "KeyNumbers";
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn2.HeaderText = "Key Numbers";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Relation";
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn3.HeaderText = "Relation";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 550;
            // 
            // spanRecNo
            // 
            this.spanRecNo.DataPropertyName = "RecNo";
            this.spanRecNo.HeaderText = "RecNo";
            this.spanRecNo.Name = "spanRecNo";
            this.spanRecNo.Visible = false;
            // 
            // fsDate
            // 
            this.fsDate.DataPropertyName = "fDate";
            this.fsDate.HeaderText = "Date";
            this.fsDate.Name = "fsDate";
            this.fsDate.ReadOnly = true;
            this.fsDate.Visible = false;
            // 
            // fsNumbers
            // 
            this.fsNumbers.DataPropertyName = "Numbers";
            this.fsNumbers.HeaderText = "Numbers";
            this.fsNumbers.Name = "fsNumbers";
            this.fsNumbers.ReadOnly = true;
            this.fsNumbers.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rdSpanMod);
            this.panel1.Controls.Add(this.rdSpanCode);
            this.panel1.Controls.Add(this.btnSearchNext);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.rdGroupSummation);
            this.panel1.Controls.Add(this.rdSpanPattern);
            this.panel1.Controls.Add(this.rdYearPattern);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Location = new System.Drawing.Point(94, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(853, 27);
            this.panel1.TabIndex = 32;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // rdSpanMod
            // 
            this.rdSpanMod.AutoSize = true;
            this.rdSpanMod.Location = new System.Drawing.Point(508, 4);
            this.rdSpanMod.Name = "rdSpanMod";
            this.rdSpanMod.Size = new System.Drawing.Size(117, 17);
            this.rdSpanMod.TabIndex = 39;
            this.rdSpanMod.TabStop = true;
            this.rdSpanMod.Text = "Span Pattern Mode";
            this.rdSpanMod.UseVisualStyleBackColor = true;
            this.rdSpanMod.CheckedChanged += new System.EventHandler(this.rdSpanMod_CheckedChanged);
            // 
            // rdSpanCode
            // 
            this.rdSpanCode.AutoSize = true;
            this.rdSpanCode.Location = new System.Drawing.Point(372, 4);
            this.rdSpanCode.Name = "rdSpanCode";
            this.rdSpanCode.Size = new System.Drawing.Size(115, 17);
            this.rdSpanCode.TabIndex = 38;
            this.rdSpanCode.TabStop = true;
            this.rdSpanCode.Text = "Span Pattern Code";
            this.rdSpanCode.UseVisualStyleBackColor = true;
            this.rdSpanCode.CheckedChanged += new System.EventHandler(this.rdSpanCode_CheckedChanged);
            // 
            // btnSearchNext
            // 
            this.btnSearchNext.Location = new System.Drawing.Point(708, 0);
            this.btnSearchNext.Name = "btnSearchNext";
            this.btnSearchNext.Size = new System.Drawing.Size(75, 23);
            this.btnSearchNext.TabIndex = 37;
            this.btnSearchNext.Text = "Search Next";
            this.btnSearchNext.UseVisualStyleBackColor = true;
            this.btnSearchNext.Click += new System.EventHandler(this.btnSearchNext_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(788, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(60, 23);
            this.btnRefresh.TabIndex = 32;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // rdGroupSummation
            // 
            this.rdGroupSummation.AutoSize = true;
            this.rdGroupSummation.Location = new System.Drawing.Point(119, 4);
            this.rdGroupSummation.Name = "rdGroupSummation";
            this.rdGroupSummation.Size = new System.Drawing.Size(109, 17);
            this.rdGroupSummation.TabIndex = 31;
            this.rdGroupSummation.TabStop = true;
            this.rdGroupSummation.Text = "Group Summation";
            this.rdGroupSummation.UseVisualStyleBackColor = true;
            this.rdGroupSummation.CheckedChanged += new System.EventHandler(this.rdGroupSummation_CheckedChanged);
            // 
            // rdSpanPattern
            // 
            this.rdSpanPattern.AutoSize = true;
            this.rdSpanPattern.Location = new System.Drawing.Point(235, 4);
            this.rdSpanPattern.Name = "rdSpanPattern";
            this.rdSpanPattern.Size = new System.Drawing.Size(136, 17);
            this.rdSpanPattern.TabIndex = 30;
            this.rdSpanPattern.TabStop = true;
            this.rdSpanPattern.Text = "Span Pattern Arithmatic";
            this.rdSpanPattern.UseVisualStyleBackColor = true;
            this.rdSpanPattern.CheckedChanged += new System.EventHandler(this.rdSpanPattern_CheckedChanged);
            // 
            // rdYearPattern
            // 
            this.rdYearPattern.AutoSize = true;
            this.rdYearPattern.Checked = true;
            this.rdYearPattern.Location = new System.Drawing.Point(29, 4);
            this.rdYearPattern.Name = "rdYearPattern";
            this.rdYearPattern.Size = new System.Drawing.Size(84, 17);
            this.rdYearPattern.TabIndex = 29;
            this.rdYearPattern.TabStop = true;
            this.rdYearPattern.Text = "Year Pattern";
            this.rdYearPattern.UseVisualStyleBackColor = true;
            this.rdYearPattern.CheckedChanged += new System.EventHandler(this.rdYearPattern_CheckedChanged);
            // 
            // grdSummaryGroupSummation
            // 
            this.grdSummaryGroupSummation.AllowUserToAddRows = false;
            this.grdSummaryGroupSummation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSummaryGroupSummation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.Database,
            this.Conclusion,
            this.fDate,
            this.Numbers});
            this.grdSummaryGroupSummation.Location = new System.Drawing.Point(114, 495);
            this.grdSummaryGroupSummation.Name = "grdSummaryGroupSummation";
            this.grdSummaryGroupSummation.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdSummaryGroupSummation.RowTemplate.Height = 50;
            this.grdSummaryGroupSummation.Size = new System.Drawing.Size(702, 129);
            this.grdSummaryGroupSummation.TabIndex = 33;
            this.grdSummaryGroupSummation.Visible = false;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn4.HeaderText = "Id";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // Database
            // 
            this.Database.DataPropertyName = "Database";
            this.Database.HeaderText = "Database";
            this.Database.Name = "Database";
            this.Database.ReadOnly = true;
            this.Database.Width = 200;
            // 
            // Conclusion
            // 
            this.Conclusion.DataPropertyName = "Conclusion";
            this.Conclusion.HeaderText = "Conclusion";
            this.Conclusion.Name = "Conclusion";
            this.Conclusion.ReadOnly = true;
            this.Conclusion.Width = 430;
            // 
            // fDate
            // 
            this.fDate.DataPropertyName = "fDate";
            this.fDate.HeaderText = "Date";
            this.fDate.Name = "fDate";
            this.fDate.ReadOnly = true;
            this.fDate.Visible = false;
            // 
            // Numbers
            // 
            this.Numbers.DataPropertyName = "Numbers";
            this.Numbers.HeaderText = "Numbers";
            this.Numbers.Name = "Numbers";
            this.Numbers.ReadOnly = true;
            this.Numbers.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(313, 468);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(318, 23);
            this.progressBar1.TabIndex = 34;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(833, 468);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(112, 23);
            this.btnExport.TabIndex = 35;
            this.btnExport.Text = "Export Result";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rdExternal);
            this.panel2.Controls.Add(this.rdInternal);
            this.panel2.Location = new System.Drawing.Point(696, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(218, 23);
            this.panel2.TabIndex = 36;
            // 
            // rdExternal
            // 
            this.rdExternal.AutoSize = true;
            this.rdExternal.Location = new System.Drawing.Point(139, 2);
            this.rdExternal.Name = "rdExternal";
            this.rdExternal.Size = new System.Drawing.Size(63, 17);
            this.rdExternal.TabIndex = 1;
            this.rdExternal.Text = "External";
            this.rdExternal.UseVisualStyleBackColor = true;
            // 
            // rdInternal
            // 
            this.rdInternal.AutoSize = true;
            this.rdInternal.Checked = true;
            this.rdInternal.Location = new System.Drawing.Point(25, 1);
            this.rdInternal.Name = "rdInternal";
            this.rdInternal.Size = new System.Drawing.Size(60, 17);
            this.rdInternal.TabIndex = 0;
            this.rdInternal.TabStop = true;
            this.rdInternal.Text = "Internal";
            this.rdInternal.UseVisualStyleBackColor = true;
            this.rdInternal.CheckedChanged += new System.EventHandler(this.rdInternal_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.rdMachine);
            this.panel3.Controls.Add(this.rdWinning);
            this.panel3.Location = new System.Drawing.Point(526, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(164, 23);
            this.panel3.TabIndex = 37;
            // 
            // rdMachine
            // 
            this.rdMachine.AutoSize = true;
            this.rdMachine.Location = new System.Drawing.Point(79, 2);
            this.rdMachine.Name = "rdMachine";
            this.rdMachine.Size = new System.Drawing.Size(66, 17);
            this.rdMachine.TabIndex = 1;
            this.rdMachine.Text = "Machine";
            this.rdMachine.UseVisualStyleBackColor = true;
            // 
            // rdWinning
            // 
            this.rdWinning.AutoSize = true;
            this.rdWinning.Checked = true;
            this.rdWinning.Location = new System.Drawing.Point(11, 1);
            this.rdWinning.Name = "rdWinning";
            this.rdWinning.Size = new System.Drawing.Size(58, 17);
            this.rdWinning.TabIndex = 0;
            this.rdWinning.TabStop = true;
            this.rdWinning.Text = "Wining";
            this.rdWinning.UseVisualStyleBackColor = true;
            // 
            // btnSaveResult
            // 
            this.btnSaveResult.Location = new System.Drawing.Point(637, 468);
            this.btnSaveResult.Name = "btnSaveResult";
            this.btnSaveResult.Size = new System.Drawing.Size(190, 23);
            this.btnSaveResult.TabIndex = 38;
            this.btnSaveResult.Text = "Save Forcast Result";
            this.btnSaveResult.UseVisualStyleBackColor = true;
            this.btnSaveResult.Click += new System.EventHandler(this.btnSaveResult_Click);
            // 
            // YearPattern
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 629);
            this.Controls.Add(this.grdSpanSummary);
            this.Controls.Add(this.btnSaveResult);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.searchGrid);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblInternalDatabaseName);
            this.Controls.Add(this.grdSummaryGroupSummation);
            this.Controls.Add(this.grdSummary);
            this.Name = "YearPattern";
            this.Text = "YearPattern";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.YearPattern_Load);
            ((System.ComponentModel.ISupportInitialize)(this.searchGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSpanSummary)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSummaryGroupSummation)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblInternalDatabaseName;
        private System.Windows.Forms.DataGridView searchGrid;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.DataGridView grdSummary;
        private System.Windows.Forms.DataGridView grdSpanSummary;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdSpanPattern;
        private System.Windows.Forms.RadioButton rdYearPattern;
        private System.Windows.Forms.RadioButton rdGroupSummation;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView grdSummaryGroupSummation;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rdExternal;
        private System.Windows.Forms.RadioButton rdInternal;
        private System.Windows.Forms.Button btnSearchNext;
        private System.Windows.Forms.RadioButton rdSpanMod;
        private System.Windows.Forms.RadioButton rdSpanCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Year;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sum1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sum2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Database1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn W1;
        private System.Windows.Forms.DataGridViewTextBoxColumn W2;
        private System.Windows.Forms.DataGridViewTextBoxColumn W3;
        private System.Windows.Forms.DataGridViewTextBoxColumn W4;
        private System.Windows.Forms.DataGridViewTextBoxColumn W5;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUM_W;
        private System.Windows.Forms.DataGridViewTextBoxColumn space1;
        private System.Windows.Forms.DataGridViewTextBoxColumn space2;
        private System.Windows.Forms.DataGridViewTextBoxColumn M1;
        private System.Windows.Forms.DataGridViewTextBoxColumn M2;
        private System.Windows.Forms.DataGridViewTextBoxColumn M3;
        private System.Windows.Forms.DataGridViewTextBoxColumn M4;
        private System.Windows.Forms.DataGridViewTextBoxColumn M5;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUM_M;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DBId;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rdMachine;
        private System.Windows.Forms.RadioButton rdWinning;
        private System.Windows.Forms.Button btnSaveResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Database;
        private System.Windows.Forms.DataGridViewTextBoxColumn Conclusion;
        private System.Windows.Forms.DataGridViewTextBoxColumn fDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Numbers;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn spanRecNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn fsDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn fsNumbers;
    }
}