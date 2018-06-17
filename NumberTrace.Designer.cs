namespace SystamaticDBSearch
{
    partial class NumberTrace
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
            this.label6 = new System.Windows.Forms.Label();
            this.ResultGrid = new System.Windows.Forms.DataGridView();
            this.SearchNumbers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Events = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WinningMachine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblInternalDatabaseName = new System.Windows.Forms.Label();
            this.M1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SUM_M = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.M2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.space1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.M5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.M3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label9 = new System.Windows.Forms.Label();
            this.M4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SUM_W = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtFirstNum = new System.Windows.Forms.TextBox();
            this.txtThirdNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtForthNum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSecondNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdWinningAndMachineNumbers = new System.Windows.Forms.RadioButton();
            this.rdMachineNumbersOnly = new System.Windows.Forms.RadioButton();
            this.rdWinningNumbersOnly = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchGrid = new System.Windows.Forms.DataGridView();
            this.W2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ResultGrid)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(445, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(160, 25);
            this.label6.TabIndex = 20;
            this.label6.Text = "Number Trace";
            // 
            // ResultGrid
            // 
            this.ResultGrid.AllowUserToAddRows = false;
            this.ResultGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SearchNumbers,
            this.Events,
            this.WinningMachine});
            this.ResultGrid.Location = new System.Drawing.Point(211, 442);
            this.ResultGrid.Name = "ResultGrid";
            this.ResultGrid.RowHeadersWidth = 22;
            this.ResultGrid.RowTemplate.Height = 32;
            this.ResultGrid.Size = new System.Drawing.Size(806, 171);
            this.ResultGrid.TabIndex = 19;
            // 
            // SearchNumbers
            // 
            this.SearchNumbers.DataPropertyName = "SearchCode";
            this.SearchNumbers.HeaderText = "Search Code";
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
            // lblInternalDatabaseName
            // 
            this.lblInternalDatabaseName.AutoSize = true;
            this.lblInternalDatabaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInternalDatabaseName.ForeColor = System.Drawing.Color.Red;
            this.lblInternalDatabaseName.Location = new System.Drawing.Point(214, 8);
            this.lblInternalDatabaseName.Name = "lblInternalDatabaseName";
            this.lblInternalDatabaseName.Size = new System.Drawing.Size(177, 18);
            this.lblInternalDatabaseName.TabIndex = 16;
            this.lblInternalDatabaseName.Text = "Systamatic DB Search";
            // 
            // M1
            // 
            this.M1.DataPropertyName = "M1";
            this.M1.HeaderText = "M1";
            this.M1.Name = "M1";
            this.M1.Width = 50;
            // 
            // SUM_M
            // 
            this.SUM_M.DataPropertyName = "SUM_M";
            this.SUM_M.HeaderText = "SUM";
            this.SUM_M.Name = "SUM_M";
            this.SUM_M.ReadOnly = true;
            this.SUM_M.Width = 50;
            // 
            // M2
            // 
            this.M2.DataPropertyName = "M2";
            this.M2.HeaderText = "M2";
            this.M2.Name = "M2";
            this.M2.Width = 50;
            // 
            // space1
            // 
            this.space1.HeaderText = "";
            this.space1.Name = "space1";
            this.space1.ReadOnly = true;
            this.space1.Width = 50;
            // 
            // M5
            // 
            this.M5.DataPropertyName = "M5";
            this.M5.HeaderText = "M5";
            this.M5.Name = "M5";
            this.M5.Width = 50;
            // 
            // M3
            // 
            this.M3.DataPropertyName = "M3";
            this.M3.HeaderText = "M3";
            this.M3.Name = "M3";
            this.M3.Width = 50;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(211, 423);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(146, 15);
            this.label9.TabIndex = 23;
            this.label9.Text = "Number Trace History";
            // 
            // M4
            // 
            this.M4.DataPropertyName = "M4";
            this.M4.HeaderText = "M4";
            this.M4.Name = "M4";
            this.M4.Width = 50;
            // 
            // SUM_W
            // 
            this.SUM_W.DataPropertyName = "SUM_W";
            this.SUM_W.HeaderText = "SUM";
            this.SUM_W.Name = "SUM_W";
            this.SUM_W.ReadOnly = true;
            this.SUM_W.Width = 50;
            // 
            // W5
            // 
            this.W5.DataPropertyName = "W5";
            this.W5.HeaderText = "W5";
            this.W5.Name = "W5";
            this.W5.Width = 50;
            // 
            // W4
            // 
            this.W4.DataPropertyName = "W4";
            this.W4.HeaderText = "W4";
            this.W4.Name = "W4";
            this.W4.Width = 50;
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
            this.panel3.Size = new System.Drawing.Size(188, 151);
            this.panel3.TabIndex = 9;
            // 
            // txtFirstNum
            // 
            this.txtFirstNum.Location = new System.Drawing.Point(54, 10);
            this.txtFirstNum.Name = "txtFirstNum";
            this.txtFirstNum.Size = new System.Drawing.Size(69, 20);
            this.txtFirstNum.TabIndex = 4;
            this.txtFirstNum.Validated += new System.EventHandler(this.txtFirstNum_Validated);
            // 
            // txtThirdNumber
            // 
            this.txtThirdNumber.Location = new System.Drawing.Point(54, 76);
            this.txtThirdNumber.Name = "txtThirdNumber";
            this.txtThirdNumber.ReadOnly = true;
            this.txtThirdNumber.Size = new System.Drawing.Size(69, 20);
            this.txtThirdNumber.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "RN";
            // 
            // txtForthNum
            // 
            this.txtForthNum.Location = new System.Drawing.Point(54, 112);
            this.txtForthNum.Name = "txtForthNum";
            this.txtForthNum.ReadOnly = true;
            this.txtForthNum.Size = new System.Drawing.Size(69, 20);
            this.txtForthNum.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "TCN";
            // 
            // txtSecondNumber
            // 
            this.txtSecondNumber.Location = new System.Drawing.Point(54, 43);
            this.txtSecondNumber.Name = "txtSecondNumber";
            this.txtSecondNumber.ReadOnly = true;
            this.txtSecondNumber.Size = new System.Drawing.Size(69, 20);
            this.txtSecondNumber.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "CN";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "TN";
            this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(4, 314);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 27;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(117, 314);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 26;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(6, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(201, 605);
            this.panel1.TabIndex = 17;
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
            // W3
            // 
            this.W3.DataPropertyName = "W3";
            this.W3.HeaderText = "W3";
            this.W3.Name = "W3";
            this.W3.Width = 50;
            // 
            // W1
            // 
            this.W1.DataPropertyName = "W1";
            this.W1.HeaderText = "W1";
            this.W1.Name = "W1";
            this.W1.Width = 50;
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
            this.searchGrid.Location = new System.Drawing.Point(210, 30);
            this.searchGrid.Name = "searchGrid";
            this.searchGrid.RowHeadersWidth = 25;
            this.searchGrid.Size = new System.Drawing.Size(806, 390);
            this.searchGrid.TabIndex = 18;
            this.searchGrid.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.searchGrid_CellPainting);
            // 
            // W2
            // 
            this.W2.DataPropertyName = "W2";
            this.W2.HeaderText = "W2";
            this.W2.Name = "W2";
            this.W2.Width = 50;
            // 
            // NumberTrace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 629);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ResultGrid);
            this.Controls.Add(this.lblInternalDatabaseName);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.searchGrid);
            this.Name = "NumberTrace";
            this.Text = "NumberTrace";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.NumberTrace_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ResultGrid)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView ResultGrid;
        private System.Windows.Forms.Label lblInternalDatabaseName;
        private System.Windows.Forms.DataGridViewTextBoxColumn M1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUM_M;
        private System.Windows.Forms.DataGridViewTextBoxColumn M2;
        private System.Windows.Forms.DataGridViewTextBoxColumn space1;
        private System.Windows.Forms.DataGridViewTextBoxColumn M5;
        private System.Windows.Forms.DataGridViewTextBoxColumn M3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn M4;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUM_W;
        private System.Windows.Forms.DataGridViewTextBoxColumn W5;
        private System.Windows.Forms.DataGridViewTextBoxColumn W4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtFirstNum;
        private System.Windows.Forms.TextBox txtThirdNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtForthNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSecondNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rdWinningAndMachineNumbers;
        private System.Windows.Forms.RadioButton rdMachineNumbersOnly;
        private System.Windows.Forms.RadioButton rdWinningNumbersOnly;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn W3;
        private System.Windows.Forms.DataGridViewTextBoxColumn W1;
        private System.Windows.Forms.DataGridView searchGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn W2;
        private System.Windows.Forms.DataGridViewTextBoxColumn SearchNumbers;
        private System.Windows.Forms.DataGridViewTextBoxColumn Events;
        private System.Windows.Forms.DataGridViewTextBoxColumn WinningMachine;
    }
}