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
    public partial class EditDatabase : Form
    {
        static DataSet myDataSet;
        public EditDatabase()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                LoadGrid();
                btnSaveUpdate.Enabled = true;
                btnSearch.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadGrid()
        { try
            {
            myDataSet = new DataSet();
            SqlClass.GetWin_Machin_DataByDBId((int)cmbDatabaseList.SelectedValue, ref myDataSet);

            //myDataSet.Tables[0].Columns.Add("space1");
            //myDataSet.Tables[0].Columns.Add("SNo");
            
            //int i = 1;
            //foreach (DataRow row in myDataSet.Tables[0].Rows)
            //{
            //    row["SNo"] = i;
            //    i++;
            //}
            
            Date.DefaultCellStyle.Format = "dd/MM/yyyy";
            searchGrid.DataSource = myDataSet.Tables[0];
            searchGrid.Columns["Id"].Visible = false;
            searchGrid.Columns["DBId"].Visible = false;
            btnSaveUpdate.Enabled = true;
            btnSearch.Enabled = true;
            
            
            }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.Message);
        }
        }

        private void btnSaveUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                myDataSet.GetChanges();
                int j = 1;
                for (int i = 0; i < myDataSet.Tables[0].Rows.Count ; i++)
                {
                    if (myDataSet.Tables[0].Rows[i].RowState != DataRowState.Deleted)
                    {
                        myDataSet.Tables[0].Rows[i]["SNo"] = j;
                        j++;
                    }
                }
                SqlClass.UpdateDatabase(ref myDataSet, (int)cmbDatabaseList.SelectedValue);
                MessageBox.Show("Updated Successfully");
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

      
        private void EditDatabase_Load(object sender, EventArgs e)
        {
            try
            {
                //cmbDatabaseList.DataSource = SqlClass.GetExternalDatabaseList();
                LoadGrid();
                //
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void searchGrid_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells["DBId"].Value = (int)cmbDatabaseList.SelectedValue;
                DateTime dt = Convert.ToDateTime(searchGrid.Rows[searchGrid.Rows.Count - 3].Cells["Date"].Value);
                int Num = Convert.ToInt32(searchGrid.Rows[searchGrid.Rows.Count - 3].Cells["SNo"].Value);
                dt = dt.AddDays(7);
                searchGrid.Rows[searchGrid.Rows.Count - 2].Cells["Date"].Value = dt;
                searchGrid.Rows[searchGrid.Rows.Count - 2].Cells["SNo"].Value = Num + 1;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Some Error occures");
            }
        }

       

        private void searchGrid_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                searchGrid.Rows[e.RowIndex].Cells["DBId"].Value = (int)cmbDatabaseList.SelectedValue;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Some Error occures");
            }
        }

        private void searchGrid_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (searchGrid.Columns[e.ColumnIndex].Name != "Date" && searchGrid.Columns[e.ColumnIndex].Name != "SUM_M" && searchGrid.Columns[e.ColumnIndex].Name != "SUM_W" && searchGrid.Columns[e.ColumnIndex].Name != "SNo")
                    if (searchGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && searchGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != DBNull.Value)
                        if (Convert.ToInt32(searchGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) > 90)
                        {
                            MessageBox.Show("Invalid Value");
                            searchGrid.CancelEdit();
                        }
                        else
                        {
                            if (e.ColumnIndex <= 9)
                            {

                                int rowIndex = e.RowIndex;
                                if (searchGrid.Rows[rowIndex].Cells["W1"].Value != null && searchGrid.Rows[rowIndex].Cells["W2"].Value != null && searchGrid.Rows[rowIndex].Cells["W3"].Value != null && searchGrid.Rows[rowIndex].Cells["W4"].Value != null && searchGrid.Rows[rowIndex].Cells["W5"].Value != null)
                                    if (searchGrid.Rows[rowIndex].Cells["W1"].Value != DBNull.Value && searchGrid.Rows[rowIndex].Cells["W2"].Value != DBNull.Value && searchGrid.Rows[rowIndex].Cells["W3"].Value != DBNull.Value && searchGrid.Rows[rowIndex].Cells["W4"].Value != DBNull.Value && searchGrid.Rows[rowIndex].Cells["W5"].Value != DBNull.Value)
                                        searchGrid.Rows[e.RowIndex].Cells["SUM_W"].Value = Convert.ToInt32(searchGrid.Rows[rowIndex].Cells["W1"].Value) + Convert.ToInt32(searchGrid.Rows[rowIndex].Cells["W2"].Value) + Convert.ToInt32(searchGrid.Rows[rowIndex].Cells["W3"].Value) + Convert.ToInt32(searchGrid.Rows[rowIndex].Cells["W4"].Value) + Convert.ToInt32(searchGrid.Rows[rowIndex].Cells["W5"].Value);
                            }
                            else
                            {
                                int rowIndex = e.RowIndex;
                                if (searchGrid.Rows[rowIndex].Cells["M1"].Value != null && searchGrid.Rows[rowIndex].Cells["M2"].Value != null && searchGrid.Rows[rowIndex].Cells["M3"].Value != null && searchGrid.Rows[rowIndex].Cells["M4"].Value != null && searchGrid.Rows[rowIndex].Cells["M5"].Value != null)
                                    if (searchGrid.Rows[rowIndex].Cells["M1"].Value != DBNull.Value && searchGrid.Rows[rowIndex].Cells["M2"].Value != DBNull.Value && searchGrid.Rows[rowIndex].Cells["M3"].Value != DBNull.Value && searchGrid.Rows[rowIndex].Cells["M4"].Value != DBNull.Value && searchGrid.Rows[rowIndex].Cells["M5"].Value != DBNull.Value)
                                        searchGrid.Rows[e.RowIndex].Cells["SUM_M"].Value = Convert.ToInt32(searchGrid.Rows[rowIndex].Cells["M1"].Value) + Convert.ToInt32(searchGrid.Rows[rowIndex].Cells["M2"].Value) + Convert.ToInt32(searchGrid.Rows[rowIndex].Cells["M3"].Value) + Convert.ToInt32(searchGrid.Rows[rowIndex].Cells["M4"].Value) + Convert.ToInt32(searchGrid.Rows[rowIndex].Cells["M5"].Value);
       
                            }

                        }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetAsExternal_Click(object sender, EventArgs e)
        {
            try
            {
                SqlClass.SetExternalDatabase((int)cmbDatabaseList.SelectedValue);
}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                LoadGrid();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            { 
                int count = searchGrid.SelectedRows.Count;
                if (count < 15 || count > 15)
                {
                    MessageBox.Show("Select exect 15  rows");
                    return;
                }
                SearchForm oSearchForm = new SearchForm();
                oSearchForm.MdiParent = this.MdiParent;
                oSearchForm.Show();
               
                int startId = (int)searchGrid.SelectedRows[count - 1].Cells["Id"].Value;
                    int endId = (int)searchGrid.SelectedRows[0].Cells["Id"].Value;
                    DataRow[] rows = myDataSet.Tables[0].Select("Id > " + (startId - 1).ToString() + " and Id < " + (endId + 1).ToString());
                    for (int i = 0; i < rows.Length; i++)
                    {
                        DataRow row = oSearchForm.dtFullcellSearch.NewRow();
                        row["Date"] = rows[i]["Date"];
                        row["W1"] = rows[i]["W1"];
                        row["W2"] = rows[i]["W2"];
                        row["W3"] = rows[i]["W3"];
                        row["W4"] = rows[i]["W4"];
                        row["W5"] = rows[i]["W5"];
                        row["SUM_W"] = rows[i]["SUM_W"];
                        row["M1"] = rows[i]["M1"];
                        row["M2"] = rows[i]["M2"];
                        row["M3"] = rows[i]["M3"];
                        row["M4"] = rows[i]["M4"];
                        row["M5"] = rows[i]["M5"];
                        row["SUM_M"] = rows[i]["SUM_M"];
                        oSearchForm.dtFullcellSearch.Rows.Add(row);
                    }

                    oSearchForm.FillSearchGrid();
                    this.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void searchGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

            try
            {
                string[] dt
                    = (e.FormattedValue.ToString().Split('/'));

                if (e.ColumnIndex == 4 && e.FormattedValue.ToString() != "")
                {
                    searchGrid.CancelEdit();
                    DateTime startdate = new DateTime(Convert.ToInt32(dt[2]), Convert.ToInt32(dt[1]), Convert.ToInt32(dt[0]));
                    searchGrid.Rows[e.RowIndex].Cells["Date"].Value = startdate;
                    if (e.RowIndex == 0)
                        ChangeAllDate(startdate);
                    //SendKeys.Send("{Escape}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Date is not in valid formate");
            }
        }

        private void searchGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbDatabaseList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void ChangeAllDate(DateTime startdate)
        {
            try
            {
                foreach (DataRow row in myDataSet.Tables[0].Rows)
                {
                    row["Date"] = startdate;
                    startdate = startdate.AddDays(7);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        

       
        
    }
}
