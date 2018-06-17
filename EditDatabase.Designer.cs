namespace SystamaticDBSearch
{
    partial class EditDatabase
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
            this.lbl = new System.Windows.Forms.Label();
            this.cmbDatabaseList = new System.Windows.Forms.ComboBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSaveUpdate = new System.Windows.Forms.Button();
            this.btnSetAsExternal = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.searchGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(24, 23);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(86, 13);
            this.lbl.TabIndex = 0;
            this.lbl.Text = "Select Database";
            // 
            // cmbDatabaseList
            // 
            this.cmbDatabaseList.DisplayMember = "DBName";
            this.cmbDatabaseList.FormattingEnabled = true;
            this.cmbDatabaseList.Location = new System.Drawing.Point(130, 20);
            this.cmbDatabaseList.Name = "cmbDatabaseList";
            this.cmbDatabaseList.Size = new System.Drawing.Size(212, 21);
            this.cmbDatabaseList.TabIndex = 1;
            this.cmbDatabaseList.ValueMember = "DBId";
            this.cmbDatabaseList.SelectedIndexChanged += new System.EventHandler(this.cmbDatabaseList_SelectedIndexChanged);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(348, 20);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSaveUpdate
            // 
            this.btnSaveUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveUpdate.Enabled = false;
            this.btnSaveUpdate.Location = new System.Drawing.Point(6, 450);
            this.btnSaveUpdate.Name = "btnSaveUpdate";
            this.btnSaveUpdate.Size = new System.Drawing.Size(118, 23);
            this.btnSaveUpdate.TabIndex = 4;
            this.btnSaveUpdate.Text = "Update Changes";
            this.btnSaveUpdate.UseVisualStyleBackColor = true;
            this.btnSaveUpdate.Click += new System.EventHandler(this.btnSaveUpdate_Click);
            // 
            // btnSetAsExternal
            // 
            this.btnSetAsExternal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetAsExternal.Location = new System.Drawing.Point(668, 450);
            this.btnSetAsExternal.Name = "btnSetAsExternal";
            this.btnSetAsExternal.Size = new System.Drawing.Size(156, 23);
            this.btnSetAsExternal.TabIndex = 6;
            this.btnSetAsExternal.Text = "Set As External Database";
            this.btnSetAsExternal.UseVisualStyleBackColor = true;
            this.btnSetAsExternal.Visible = false;
            this.btnSetAsExternal.Click += new System.EventHandler(this.btnSetAsExternal_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(221, 450);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(67, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(130, 450);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
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
            this.searchGrid.Location = new System.Drawing.Point(12, 60);
            this.searchGrid.Name = "searchGrid";
            this.searchGrid.Size = new System.Drawing.Size(821, 384);
            this.searchGrid.TabIndex = 9;
            this.searchGrid.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.searchGrid_UserAddedRow);
            this.searchGrid.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.searchGrid_CellValidated);
            this.searchGrid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.searchGrid_CellValidating);
            this.searchGrid.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.searchGrid_RowValidated);
            this.searchGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.searchGrid_CellContentClick);
            // 
            // SNo
            // 
            this.SNo.DataPropertyName = "SNo";
            this.SNo.HeaderText = "Event";
            this.SNo.Name = "SNo";
            this.SNo.Width = 50;
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
            // EditDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 485);
            this.Controls.Add(this.searchGrid);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSetAsExternal);
            this.Controls.Add(this.btnSaveUpdate);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.cmbDatabaseList);
            this.Controls.Add(this.lbl);
            this.Name = "EditDatabase";
            this.Text = "Edit Database";
            this.Load += new System.EventHandler(this.EditDatabase_Load);
            ((System.ComponentModel.ISupportInitialize)(this.searchGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveUpdate;
        public System.Windows.Forms.ComboBox cmbDatabaseList;
        public System.Windows.Forms.Button btnLoad;
        public System.Windows.Forms.Button btnSetAsExternal;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView searchGrid;
        public System.Windows.Forms.Label lbl;
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
    }
}