namespace SystamaticDBSearch
{
    partial class NewDatabase
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
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUnCheckAll = new System.Windows.Forms.Button();
            this.btnCheckAll = new System.Windows.Forms.Button();
            this.btnInstallSelectedFiles = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnBrowsFile = new System.Windows.Forms.Button();
            this.btnRectify = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(0, 0);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(518, 229);
            this.checkedListBox1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.btnUnCheckAll);
            this.panel1.Controls.Add(this.btnCheckAll);
            this.panel1.Controls.Add(this.checkedListBox1);
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(518, 264);
            this.panel1.TabIndex = 1;
            // 
            // btnUnCheckAll
            // 
            this.btnUnCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUnCheckAll.Location = new System.Drawing.Point(108, 235);
            this.btnUnCheckAll.Name = "btnUnCheckAll";
            this.btnUnCheckAll.Size = new System.Drawing.Size(75, 23);
            this.btnUnCheckAll.TabIndex = 2;
            this.btnUnCheckAll.Text = "Uncheck All";
            this.btnUnCheckAll.UseVisualStyleBackColor = true;
            this.btnUnCheckAll.Click += new System.EventHandler(this.btnUnCheckAll_Click);
            // 
            // btnCheckAll
            // 
            this.btnCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCheckAll.Location = new System.Drawing.Point(3, 235);
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Size = new System.Drawing.Size(75, 23);
            this.btnCheckAll.TabIndex = 1;
            this.btnCheckAll.Text = "Check All";
            this.btnCheckAll.UseVisualStyleBackColor = true;
            this.btnCheckAll.Click += new System.EventHandler(this.btnCheckAll_Click);
            // 
            // btnInstallSelectedFiles
            // 
            this.btnInstallSelectedFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnInstallSelectedFiles.Location = new System.Drawing.Point(3, 294);
            this.btnInstallSelectedFiles.Name = "btnInstallSelectedFiles";
            this.btnInstallSelectedFiles.Size = new System.Drawing.Size(159, 23);
            this.btnInstallSelectedFiles.TabIndex = 2;
            this.btnInstallSelectedFiles.Text = "Import Selected Files";
            this.btnInstallSelectedFiles.UseVisualStyleBackColor = true;
            this.btnInstallSelectedFiles.Click += new System.EventHandler(this.btnInstallSelectedFiles_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(443, 294);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // btnBrowsFile
            // 
            this.btnBrowsFile.Location = new System.Drawing.Point(3, 2);
            this.btnBrowsFile.Name = "btnBrowsFile";
            this.btnBrowsFile.Size = new System.Drawing.Size(159, 23);
            this.btnBrowsFile.TabIndex = 4;
            this.btnBrowsFile.Text = "Click here to select file";
            this.btnBrowsFile.UseVisualStyleBackColor = true;
            this.btnBrowsFile.Click += new System.EventHandler(this.btnBrowsFile_Click);
            // 
            // btnRectify
            // 
            this.btnRectify.Location = new System.Drawing.Point(390, 2);
            this.btnRectify.Name = "btnRectify";
            this.btnRectify.Size = new System.Drawing.Size(120, 23);
            this.btnRectify.TabIndex = 11;
            this.btnRectify.Text = "Rectify All Database";
            this.btnRectify.UseVisualStyleBackColor = true;
            this.btnRectify.Click += new System.EventHandler(this.btnRectify_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(169, 2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(215, 23);
            this.progressBar1.TabIndex = 12;
            // 
            // NewDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 319);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnRectify);
            this.Controls.Add(this.btnBrowsFile);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnInstallSelectedFiles);
            this.Controls.Add(this.panel1);
            this.Name = "NewDatabase";
            this.Text = "Select Excell Files To Import";
            this.Load += new System.EventHandler(this.NewDatabase_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnUnCheckAll;
        private System.Windows.Forms.Button btnCheckAll;
        private System.Windows.Forms.Button btnInstallSelectedFiles;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnBrowsFile;
        private System.Windows.Forms.Button btnRectify;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}