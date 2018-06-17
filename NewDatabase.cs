using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using SystamaticDBSearch;
namespace SystamaticDBSearch
{
    public partial class NewDatabase : Form
    {
        public NewDatabase()
        {
            InitializeComponent();
        }

        private void NewDatabase_Load(object sender, EventArgs e)
        {
            
            
        }
        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count ; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        private void btnUnCheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void btnInstallSelectedFiles_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkedListBox1.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Select atleast one file");
                    return;
                }
                progressBar1.Value = 0;
                progressBar1.Maximum = checkedListBox1.CheckedItems.Count;
                for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
                {
                    string fullName = checkedListBox1.CheckedItems[i].ToString();
                    ImportDB(fullName.Split('>')[1].Trim(), fullName.Split(',')[0].Trim());
                    progressBar1.Value += 1;
                }
                MessageBox.Show("Imported Successfully");
                progressBar1.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ImportDB(string fileName,string dataBasename)
        {
            try
            {

                string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                      "Data Source=" + fileName + ";" +
                      "Extended Properties=Excel 8.0;";

                Microsoft.Office.Interop.Excel.Application app;
                app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook workBook = app.Workbooks.Open(fileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                string SheetName = ((Microsoft.Office.Interop.Excel.Worksheet)workBook.Sheets[1]).Name;
                OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM [" + SheetName + "$]", strConn);
                DataSet myDataSet = new DataSet();
                myCommand.Fill(myDataSet, "ExcelInfo");
                workBook.Save();
                app.Application.Quit();
                SqlClass.ImportDatabase(myDataSet, dataBasename);
            }
            catch (Exception ex)
            {
                throw ex;
                           }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if(e.Value != null)
                if (e.Value.ToString() == "48")
                {
                    e.CellStyle.BackColor = Color.Blue;
              
                }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string Dbname = Microsoft.VisualBasic.Interaction.InputBox("Enter Database Name","Systamatic Database Search",openFileDialog1.SafeFileName.Split('.')[0],  300, 300);
            checkedListBox1.Items.Add(Dbname + " , file name ->" + openFileDialog1.FileName);
        }

        private void btnBrowsFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void btnRectify_Click(object sender, EventArgs e)
        {
            try
            {
               
                DataTable dt = SqlClass.GetExternalDatabaseList();
                int DBId;
                progressBar1.Value = 0;
                progressBar1.Maximum = dt.Rows.Count + 1;
                foreach (DataRow rowEx in dt.Rows)
                { 
                    DataSet myDataSet = new DataSet();
                    DBId = Convert.ToInt32(rowEx["DBId"]);
                    SqlClass.GetWin_Machin_DataByDBId(DBId, ref myDataSet);
                    for (int i = 0; i < myDataSet.Tables[0].Rows.Count - 1; i++)
                    {
                        myDataSet.Tables[0].Rows[i]["SNo"] = i + 1;
                    }
                    SqlClass.UpdateDatabase(ref myDataSet, DBId);
                    progressBar1.Value += 1;
                }
                dt = SqlClass.GetInternalDatabase();
                if (dt.Rows.Count > 0)
                {
                    DataSet myDataSet = new DataSet();
                    DBId = Convert.ToInt32(dt.Rows[0]["DBId"]);
                    SqlClass.GetWin_Machin_DataByDBId(DBId, ref myDataSet);
                    for (int i = 0; i < myDataSet.Tables[0].Rows.Count; i++)
                    {
                        myDataSet.Tables[0].Rows[i]["SNo"] = i + 1;
                    }
                    SqlClass.UpdateDatabase(ref myDataSet, DBId);
                    progressBar1.Value = progressBar1.Maximum;
                }
                MessageBox.Show("Update Successfully");
                progressBar1.Value = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
    }
}
