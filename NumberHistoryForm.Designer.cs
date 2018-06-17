namespace SystamaticDBSearch
{
    partial class NumberHistoryForm
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
            this.lblInternalDatabaseName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rdCodePattern = new System.Windows.Forms.RadioButton();
            this.rdSumPattern = new System.Windows.Forms.RadioButton();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.Pair3 = new System.Windows.Forms.CheckedListBox();
            this.Pair2 = new System.Windows.Forms.CheckedListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Pair1 = new System.Windows.Forms.CheckedListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtFirstNum = new System.Windows.Forms.TextBox();
            this.txtThirdNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtForthNum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSecondNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdWinningAndMachineNumbers = new System.Windows.Forms.RadioButton();
            this.rdMachineNumbersOnly = new System.Windows.Forms.RadioButton();
            this.rdWinningNumbersOnly = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
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
            this.M1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.M2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.M3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.M4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.M5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SUM_M = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResultGrid = new System.Windows.Forms.DataGridView();
            this.SearchNumbers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Events = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WinningMachine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridCountingHistory = new System.Windows.Forms.DataGridView();
            this.pairId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firstLineNo1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.secondLineNo1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firstLineNo2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.criteria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.secondLineNo2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Interval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountingWeekSpan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W_M = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResultGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCountingHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // lblInternalDatabaseName
            // 
            this.lblInternalDatabaseName.AutoSize = true;
            this.lblInternalDatabaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInternalDatabaseName.ForeColor = System.Drawing.Color.Red;
            this.lblInternalDatabaseName.Location = new System.Drawing.Point(220, 9);
            this.lblInternalDatabaseName.Name = "lblInternalDatabaseName";
            this.lblInternalDatabaseName.Size = new System.Drawing.Size(177, 18);
            this.lblInternalDatabaseName.TabIndex = 7;
            this.lblInternalDatabaseName.Text = "Systamatic DB Search";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(201, 605);
            this.panel1.TabIndex = 8;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.rdCodePattern);
            this.panel4.Controls.Add(this.rdSumPattern);
            this.panel4.Location = new System.Drawing.Point(4, 408);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(188, 62);
            this.panel4.TabIndex = 28;
            this.panel4.Visible = false;
            // 
            // rdCodePattern
            // 
            this.rdCodePattern.AutoSize = true;
            this.rdCodePattern.Location = new System.Drawing.Point(7, 32);
            this.rdCodePattern.Name = "rdCodePattern";
            this.rdCodePattern.Size = new System.Drawing.Size(87, 17);
            this.rdCodePattern.TabIndex = 1;
            this.rdCodePattern.TabStop = true;
            this.rdCodePattern.Text = "Code Pattern";
            this.rdCodePattern.UseVisualStyleBackColor = true;
            // 
            // rdSumPattern
            // 
            this.rdSumPattern.AutoSize = true;
            this.rdSumPattern.Checked = true;
            this.rdSumPattern.Location = new System.Drawing.Point(7, 4);
            this.rdSumPattern.Name = "rdSumPattern";
            this.rdSumPattern.Size = new System.Drawing.Size(83, 17);
            this.rdSumPattern.TabIndex = 0;
            this.rdSumPattern.TabStop = true;
            this.rdSumPattern.Text = "Sum Pattern";
            this.rdSumPattern.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(4, 495);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 27;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(117, 495);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 26;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.Pair3);
            this.panel5.Controls.Add(this.Pair2);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.Pair1);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Location = new System.Drawing.Point(4, 296);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(188, 105);
            this.panel5.TabIndex = 10;
            // 
            // Pair3
            // 
            this.Pair3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pair3.FormattingEnabled = true;
            this.Pair3.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.Pair3.Location = new System.Drawing.Point(131, 25);
            this.Pair3.Name = "Pair3";
            this.Pair3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Pair3.Size = new System.Drawing.Size(36, 72);
            this.Pair3.TabIndex = 33;
            // 
            // Pair2
            // 
            this.Pair2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pair2.FormattingEnabled = true;
            this.Pair2.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.Pair2.Location = new System.Drawing.Point(58, 25);
            this.Pair2.Name = "Pair2";
            this.Pair2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Pair2.Size = new System.Drawing.Size(39, 72);
            this.Pair2.TabIndex = 32;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(55, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Second Pair";
            // 
            // Pair1
            // 
            this.Pair1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pair1.FormattingEnabled = true;
            this.Pair1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.Pair1.Location = new System.Drawing.Point(4, 25);
            this.Pair1.Name = "Pair1";
            this.Pair1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Pair1.Size = new System.Drawing.Size(36, 72);
            this.Pair1.TabIndex = 31;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(128, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Third Pair";
            this.label10.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(1, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "First Pair";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txtFirstNum);
            this.panel3.Controls.Add(this.txtThirdNumber);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.txtForthNum);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.txtSecondNumber);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Location = new System.Drawing.Point(4, 136);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(188, 153);
            this.panel3.TabIndex = 9;
            // 
            // txtFirstNum
            // 
            this.txtFirstNum.Location = new System.Drawing.Point(114, 10);
            this.txtFirstNum.Name = "txtFirstNum";
            this.txtFirstNum.Size = new System.Drawing.Size(69, 20);
            this.txtFirstNum.TabIndex = 4;
            // 
            // txtThirdNumber
            // 
            this.txtThirdNumber.Location = new System.Drawing.Point(114, 76);
            this.txtThirdNumber.Name = "txtThirdNumber";
            this.txtThirdNumber.Size = new System.Drawing.Size(69, 20);
            this.txtThirdNumber.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "First Number";
            // 
            // txtForthNum
            // 
            this.txtForthNum.Location = new System.Drawing.Point(114, 112);
            this.txtForthNum.Name = "txtForthNum";
            this.txtForthNum.Size = new System.Drawing.Size(69, 20);
            this.txtForthNum.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Forth Number";
            // 
            // txtSecondNumber
            // 
            this.txtSecondNumber.Location = new System.Drawing.Point(114, 43);
            this.txtSecondNumber.Name = "txtSecondNumber";
            this.txtSecondNumber.Size = new System.Drawing.Size(69, 20);
            this.txtSecondNumber.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "Second Number";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "Third Number";
            this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rdWinningAndMachineNumbers);
            this.panel2.Controls.Add(this.rdMachineNumbersOnly);
            this.panel2.Controls.Add(this.rdWinningNumbersOnly);
            this.panel2.Location = new System.Drawing.Point(4, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(188, 89);
            this.panel2.TabIndex = 2;
            // 
            // rdWinningAndMachineNumbers
            // 
            this.rdWinningAndMachineNumbers.AutoSize = true;
            this.rdWinningAndMachineNumbers.Location = new System.Drawing.Point(6, 54);
            this.rdWinningAndMachineNumbers.Name = "rdWinningAndMachineNumbers";
            this.rdWinningAndMachineNumbers.Size = new System.Drawing.Size(175, 17);
            this.rdWinningAndMachineNumbers.TabIndex = 30;
            this.rdWinningAndMachineNumbers.TabStop = true;
            this.rdWinningAndMachineNumbers.Text = "Winning And Machine Numbers";
            this.rdWinningAndMachineNumbers.UseVisualStyleBackColor = true;
            // 
            // rdMachineNumbersOnly
            // 
            this.rdMachineNumbersOnly.AutoSize = true;
            this.rdMachineNumbersOnly.Location = new System.Drawing.Point(6, 30);
            this.rdMachineNumbersOnly.Name = "rdMachineNumbersOnly";
            this.rdMachineNumbersOnly.Size = new System.Drawing.Size(135, 17);
            this.rdMachineNumbersOnly.TabIndex = 29;
            this.rdMachineNumbersOnly.TabStop = true;
            this.rdMachineNumbersOnly.Text = "Machine Numbers Only";
            this.rdMachineNumbersOnly.UseVisualStyleBackColor = true;
            // 
            // rdWinningNumbersOnly
            // 
            this.rdWinningNumbersOnly.AutoSize = true;
            this.rdWinningNumbersOnly.Location = new System.Drawing.Point(6, 6);
            this.rdWinningNumbersOnly.Name = "rdWinningNumbersOnly";
            this.rdWinningNumbersOnly.Size = new System.Drawing.Size(133, 17);
            this.rdWinningNumbersOnly.TabIndex = 28;
            this.rdWinningNumbersOnly.TabStop = true;
            this.rdWinningNumbersOnly.Text = "Winning Numbers Only";
            this.rdWinningNumbersOnly.UseVisualStyleBackColor = true;
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
            // 
            // searchGrid
            // 
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
            this.M1,
            this.M2,
            this.M3,
            this.M4,
            this.M5,
            this.SUM_M});
            this.searchGrid.Location = new System.Drawing.Point(216, 31);
            this.searchGrid.Name = "searchGrid";
            this.searchGrid.RowHeadersWidth = 25;
            this.searchGrid.Size = new System.Drawing.Size(806, 400);
            this.searchGrid.TabIndex = 10;
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
            // ResultGrid
            // 
            this.ResultGrid.AllowUserToAddRows = false;
            this.ResultGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SearchNumbers,
            this.Events,
            this.WinningMachine});
            this.ResultGrid.Location = new System.Drawing.Point(217, 453);
            this.ResultGrid.Name = "ResultGrid";
            this.ResultGrid.RowHeadersWidth = 22;
            this.ResultGrid.RowTemplate.Height = 32;
            this.ResultGrid.Size = new System.Drawing.Size(806, 171);
            this.ResultGrid.TabIndex = 11;
            this.ResultGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ResultGrid_CellContentClick);
            // 
            // SearchNumbers
            // 
            this.SearchNumbers.DataPropertyName = "SearchNumbers";
            this.SearchNumbers.HeaderText = "Serch Numbers";
            this.SearchNumbers.Name = "SearchNumbers";
            this.SearchNumbers.Width = 120;
            // 
            // Events
            // 
            this.Events.DataPropertyName = "Events";
            this.Events.HeaderText = "Events";
            this.Events.Name = "Events";
            this.Events.Width = 550;
            // 
            // WinningMachine
            // 
            this.WinningMachine.DataPropertyName = "WinningMachine";
            this.WinningMachine.HeaderText = "Winning/Machine";
            this.WinningMachine.Name = "WinningMachine";
            this.WinningMachine.Width = 150;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(451, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(255, 25);
            this.label6.TabIndex = 12;
            this.label6.Text = "Number History Search";
            // 
            // dataGridCountingHistory
            // 
            this.dataGridCountingHistory.AllowUserToAddRows = false;
            this.dataGridCountingHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridCountingHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pairId,
            this.firstLineNo1,
            this.secondLineNo1,
            this.firstLineNo2,
            this.Operation,
            this.criteria,
            this.secondLineNo2,
            this.column1,
            this.column2,
            this.Interval,
            this.CountingWeekSpan,
            this.W_M,
            this.Id});
            this.dataGridCountingHistory.Location = new System.Drawing.Point(217, 475);
            this.dataGridCountingHistory.Name = "dataGridCountingHistory";
            this.dataGridCountingHistory.RowHeadersWidth = 22;
            this.dataGridCountingHistory.RowTemplate.Height = 35;
            this.dataGridCountingHistory.Size = new System.Drawing.Size(806, 150);
            this.dataGridCountingHistory.TabIndex = 13;
            this.dataGridCountingHistory.Visible = false;
            // 
            // pairId
            // 
            this.pairId.DataPropertyName = "pairId";
            this.pairId.HeaderText = "pairId";
            this.pairId.Name = "pairId";
            this.pairId.ReadOnly = true;
            this.pairId.Visible = false;
            // 
            // firstLineNo1
            // 
            this.firstLineNo1.DataPropertyName = "firstLineNo1";
            this.firstLineNo1.HeaderText = "firstLineNo1";
            this.firstLineNo1.Name = "firstLineNo1";
            this.firstLineNo1.ReadOnly = true;
            this.firstLineNo1.Visible = false;
            // 
            // secondLineNo1
            // 
            this.secondLineNo1.DataPropertyName = "secondLineNo1";
            this.secondLineNo1.HeaderText = "secondLineNo1";
            this.secondLineNo1.Name = "secondLineNo1";
            this.secondLineNo1.ReadOnly = true;
            this.secondLineNo1.Visible = false;
            // 
            // firstLineNo2
            // 
            this.firstLineNo2.DataPropertyName = "firstLineNo2";
            this.firstLineNo2.HeaderText = "firstLineNo2";
            this.firstLineNo2.Name = "firstLineNo2";
            this.firstLineNo2.ReadOnly = true;
            this.firstLineNo2.Visible = false;
            // 
            // Operation
            // 
            this.Operation.DataPropertyName = "Operation";
            this.Operation.HeaderText = "Method Used";
            this.Operation.Name = "Operation";
            this.Operation.ReadOnly = true;
            this.Operation.Width = 120;
            // 
            // criteria
            // 
            this.criteria.DataPropertyName = "criteria";
            this.criteria.HeaderText = "Conclusion";
            this.criteria.Name = "criteria";
            this.criteria.ReadOnly = true;
            this.criteria.Width = 750;
            // 
            // secondLineNo2
            // 
            this.secondLineNo2.DataPropertyName = "secondLineNo2";
            this.secondLineNo2.HeaderText = "secondLineNo2";
            this.secondLineNo2.Name = "secondLineNo2";
            this.secondLineNo2.ReadOnly = true;
            this.secondLineNo2.Visible = false;
            // 
            // column1
            // 
            this.column1.DataPropertyName = "column1";
            this.column1.HeaderText = "column1";
            this.column1.Name = "column1";
            this.column1.ReadOnly = true;
            this.column1.Visible = false;
            // 
            // column2
            // 
            this.column2.DataPropertyName = "column2";
            this.column2.HeaderText = "column2";
            this.column2.Name = "column2";
            this.column2.ReadOnly = true;
            this.column2.Visible = false;
            // 
            // Interval
            // 
            this.Interval.DataPropertyName = "Interval";
            this.Interval.HeaderText = "Interval";
            this.Interval.Name = "Interval";
            this.Interval.ReadOnly = true;
            this.Interval.Visible = false;
            // 
            // CountingWeekSpan
            // 
            this.CountingWeekSpan.DataPropertyName = "CountingWeekSpan";
            this.CountingWeekSpan.HeaderText = "CountingWeekSpan";
            this.CountingWeekSpan.Name = "CountingWeekSpan";
            this.CountingWeekSpan.ReadOnly = true;
            this.CountingWeekSpan.Visible = false;
            // 
            // W_M
            // 
            this.W_M.DataPropertyName = "W_M";
            this.W_M.HeaderText = "W_M";
            this.W_M.Name = "W_M";
            this.W_M.ReadOnly = true;
            this.W_M.Visible = false;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(217, 434);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 15);
            this.label9.TabIndex = 15;
            this.label9.Text = "Number History";
            // 
            // NumberHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 629);
            this.Controls.Add(this.ResultGrid);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dataGridCountingHistory);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.searchGrid);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblInternalDatabaseName);
            this.Name = "NumberHistoryForm";
            this.Text = "Number History Search";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.NumberHistoryForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResultGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCountingHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInternalDatabaseName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtThirdNumber;
        private System.Windows.Forms.TextBox txtForthNum;
        private System.Windows.Forms.TextBox txtSecondNumber;
        private System.Windows.Forms.TextBox txtFirstNum;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView searchGrid;
        private System.Windows.Forms.DataGridView ResultGrid;
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
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.RadioButton rdWinningAndMachineNumbers;
        private System.Windows.Forms.RadioButton rdMachineNumbersOnly;
        private System.Windows.Forms.RadioButton rdWinningNumbersOnly;
        private System.Windows.Forms.CheckedListBox Pair1;
        private System.Windows.Forms.CheckedListBox Pair3;
        private System.Windows.Forms.CheckedListBox Pair2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridCountingHistory;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn SearchNumbers;
        private System.Windows.Forms.DataGridViewTextBoxColumn Events;
        private System.Windows.Forms.DataGridViewTextBoxColumn WinningMachine;
        private System.Windows.Forms.DataGridViewTextBoxColumn pairId;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstLineNo1;
        private System.Windows.Forms.DataGridViewTextBoxColumn secondLineNo1;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstLineNo2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn criteria;
        private System.Windows.Forms.DataGridViewTextBoxColumn secondLineNo2;
        private System.Windows.Forms.DataGridViewTextBoxColumn column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Interval;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountingWeekSpan;
        private System.Windows.Forms.DataGridViewTextBoxColumn W_M;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton rdCodePattern;
        private System.Windows.Forms.RadioButton rdSumPattern;
    }
}