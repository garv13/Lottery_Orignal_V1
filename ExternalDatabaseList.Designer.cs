namespace SystamaticDBSearch
{
    partial class ExternalDatabaseList
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSetInternalDatabase = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.btnSetInternalDatabase);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.checkedListBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(460, 264);
            this.panel1.TabIndex = 2;
            // 
            // btnSetInternalDatabase
            // 
            this.btnSetInternalDatabase.Location = new System.Drawing.Point(299, 235);
            this.btnSetInternalDatabase.Name = "btnSetInternalDatabase";
            this.btnSetInternalDatabase.Size = new System.Drawing.Size(158, 23);
            this.btnSetInternalDatabase.TabIndex = 2;
            this.btnSetInternalDatabase.Text = "Set As Internal Database";
            this.btnSetInternalDatabase.UseVisualStyleBackColor = true;
            this.btnSetInternalDatabase.Visible = false;
            this.btnSetInternalDatabase.Click += new System.EventHandler(this.btnSetInternalDatabase_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(3, 235);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(158, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete Selected Database";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(0, 0);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(460, 229);
            this.checkedListBox1.TabIndex = 0;
            // 
            // ExternalDatabaseList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 265);
            this.Controls.Add(this.panel1);
            this.Name = "ExternalDatabaseList";
            this.Text = "External Database List";
            this.Load += new System.EventHandler(this.ExternalDatabaseList_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnSetInternalDatabase;
    }
}