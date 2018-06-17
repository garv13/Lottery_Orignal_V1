using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
namespace SystamaticDBSearch
{
    public partial class OddEvenSearch : Form
    {
        public OddEvenSearch()
        {
            InitializeComponent();
        }
        private static DataTable dtOddEvenInternal;
        
        private static DataTable dtOddEvenExternal;

        private static DataTable dtNumFound;

        private static DataTable dtSearch;

        private static DataTable dtSerchRes;
      
        private static DataTable dtBaseTable;

        private static DataTable dtLastRows;

        private static DataTable dtCodeInternal;

        private static DataTable dtCodeExternal;

        private static DataTable dtCodeTable;

        private static DataTable dtSearchNum;

        private static ArrayList arSymboles;

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdOddEvenSearch.Checked)
                    fillOddEvenExternal();
                else
                    CodeSearch();
                if (dtNumFound.Rows.Count == 0)
                    MessageBox.Show("No Result Found");
            }
            catch (Exception ex)
            {
            }
        }
        private void fillOddEvenExternal()
        {
            try
            {
                dtNumFound = new DataTable();
                dtNumFound.Columns.Add("DBId", Type.GetType("System.Int32"));
                dtNumFound.Columns.Add("RecNo", Type.GetType("System.Int32"));
                dtNumFound.Columns.Add("Id", Type.GetType("System.Int32"));
                dtNumFound.Columns.Add("colName");
                dtNumFound.Columns.Add("W_M");
                dtNumFound.Columns.Add("SNo", Type.GetType("System.Int32"));

                dtOddEvenExternal = dtOddEvenInternal.Clone();
                dtOddEvenExternal.Columns.Add("DBId", Type.GetType("System.Int32"));
                dtOddEvenExternal.Columns.Add("RecNo", Type.GetType("System.Int32"));
                dtOddEvenExternal.Columns.Add("Count", Type.GetType("System.Int32"));
                if (rdExternal.Checked)
                {
                    DataTable dt = SqlClass.GetExternalDatabaseList();
                    progressBar1.Value = 0;
                    progressBar1.Maximum = chkExternalList.CheckedItems.Count;
                    for (int i = 0; i < chkExternalList.CheckedItems.Count; i++)
                    {
                        progressBar1.Value += 1;
                        DataRow[] rows = dt.Select("DBName = '" + chkExternalList.CheckedItems[i].ToString() + "'");
                        int dbId = Convert.ToInt32(rows[0]["DBId"]);
                        getOddEven(dbId, false);
                    }
                    progressBar1.Value = progressBar1.Maximum;
                }
                else
                {
                    
                    int DBId = Convert.ToInt32((SqlClass.GetInternalDatabase()).Rows[0]["DBId"]);
                    getOddEven(DBId, false);
                }
                Date1.DefaultCellStyle.Format = "dd/MM/yyyy";
                dtOddEvenExternal.DefaultView.Sort = "Count desc";
                ResultGrid.DataSource = dtOddEvenExternal.DefaultView;
                progressBar1.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadDataSet()
        {
            try
            {
                if ((SqlClass.GetInternalDatabase()).Rows.Count > 0)
                {
                    dtOddEvenInternal = new DataTable();
                    dtOddEvenInternal.Columns.Add("Id", Type.GetType("System.Int32"));
                    dtOddEvenInternal.Columns.Add("SNo", Type.GetType("System.Int32"));
                    dtOddEvenInternal.Columns.Add("Date", Type.GetType("System.DateTime"));
                    dtOddEvenInternal.Columns.Add("Odd_W", Type.GetType("System.Int32"));
                    dtOddEvenInternal.Columns.Add("Even_W", Type.GetType("System.Int32"));
                    dtOddEvenInternal.Columns.Add("space");
                    dtOddEvenInternal.Columns.Add("Odd_M", Type.GetType("System.Int32"));
                    dtOddEvenInternal.Columns.Add("Even_M", Type.GetType("System.Int32"));
                    dtOddEvenInternal.Columns.Add("DBName");
                    lblInternalDatabaseName.Text = (SqlClass.GetInternalDatabase()).Rows[0]["DBName"].ToString();
                    int DBId = Convert.ToInt32((SqlClass.GetInternalDatabase()).Rows[0]["DBId"]);
                    //getOddEven(DBId,true);
                    getOddEvenMod(DBId, true);
                    Date.DefaultCellStyle.Format = "dd/MM/yyyy";
                    
                    oddEvenGrid.DataSource = dtOddEvenInternal;
                    oddEvenGrid.Columns["Id"].Visible = false;

                    dtLastRows = new DataTable();
                    dtLastRows.Columns.Add("Id", Type.GetType("System.Int32"));
                    dtLastRows.Columns.Add("SNo", Type.GetType("System.Int32"));
                    dtLastRows.Columns.Add("Odd_W", Type.GetType("System.Int32"));
                    dtLastRows.Columns.Add("Even_W", Type.GetType("System.Int32"));
                    dtLastRows.Columns.Add("DBName");
                    dtLastRows.Columns.Add("DBId", Type.GetType("System.Int32"));
                }
                else
                {
                    MessageBox.Show("Internal database is not seted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private  void getOddEven(int DBId,bool Internal)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlClass.GetWin_Machin_DataByDBId(DBId, ref ds);
                int count = ds.Tables[0].Rows.Count;
                DataTable dtTempInternal = dtOddEvenInternal.Copy();
                int Cnt = dtOddEvenInternal.Rows.Count;
                for (int cnt = 0; cnt < Cnt; cnt++)
                {
                    while ((cnt < Cnt) && (dtTempInternal.Rows[cnt]["Even_W"] == null || dtTempInternal.Rows[cnt]["Even_W"] == DBNull.Value))
                    {
                        dtTempInternal.Rows[cnt].Delete();
                        dtTempInternal.AcceptChanges();
                        Cnt = Cnt - 1;
                    }
                }
                DataTable dtTempExternal = dtOddEvenExternal.Clone();
                DataTable dtTempExternal1 = dtOddEvenExternal.Clone();
                for (int i = 0; i < count; i++)
                {
                    int cntOddW = 0;
                    int cntEvenW = 0;
                    int cntOddM = 0;
                    int cntEvenM = 0;
                    if (ds.Tables[0].Rows[i]["W1"] != null && ds.Tables[0].Rows[i]["W1"] != DBNull.Value)
                    {
                        foreach (DataColumn dc in ds.Tables[0].Columns)
                        {
                            if (dc.Caption.StartsWith("W") || dc.Caption.StartsWith("M"))
                            {
                                if (!(ds.Tables[0].Rows[i][dc] == DBNull.Value || ds.Tables[0].Rows[i][dc] == null))
                                {

                                    if (Convert.ToInt32(ds.Tables[0].Rows[i][dc]) % 2 == 0)
                                    {
                                        if (dc.Caption.StartsWith("W"))
                                            cntEvenW++;
                                        else
                                            cntEvenM++;
                                    }
                                    else
                                    {
                                        if (dc.Caption.StartsWith("W"))
                                            cntOddW++;
                                        else
                                            cntOddM++;
                                    }
                                }
                            }
                        }
                        DataRow row = dtTempExternal.NewRow();
                        row["DBId"] = ds.Tables[0].Rows[i]["DBId"];
                        row["Id"] = ds.Tables[0].Rows[i]["Id"];
                        row["Date"] = ds.Tables[0].Rows[i]["Date"];
                        row["SNo"] = ds.Tables[0].Rows[i]["SNo"];
                        row["Odd_W"] = cntOddW;
                        row["Odd_M"] = cntOddM;
                        row["Even_W"] = cntEvenW;
                        row["Even_M"] = cntEvenM;
                        row["space"] = "";
                        dtTempExternal.Rows.Add(row);

                        DataRow row1 = dtTempExternal1.NewRow();
                        row1["DBId"] = ds.Tables[0].Rows[i]["DBId"];
                        row1["Id"] = ds.Tables[0].Rows[i]["Id"];
                        row1["Date"] = ds.Tables[0].Rows[i]["Date"];
                        row1["SNo"] = ds.Tables[0].Rows[i]["SNo"];
                        row1["Odd_W"] = cntOddW;
                        row1["Odd_M"] = cntOddM;
                        row1["Even_W"] = cntEvenW;
                        row1["Even_M"] = cntEvenM;
                        dtTempExternal1.Rows.Add(row1);

                    }
                    else
                    {
                        DataRow row = dtTempExternal.NewRow();
                        row["DBId"] = ds.Tables[0].Rows[i]["DBId"];
                        row["Id"] = ds.Tables[0].Rows[i]["Id"];
                        row["Date"] = ds.Tables[0].Rows[i]["Date"];
                        row["SNo"] = ds.Tables[0].Rows[i]["SNo"];
                        dtTempExternal.Rows.Add(row);
                    }
                }
                if (rdInternal.Checked)
                {
                    progressBar1.Value = 0;
                    progressBar1.Maximum = dtTempExternal1.Rows.Count;
                }
                for (int i = 14; i < dtTempExternal1.Rows.Count; i++)
                {
                    if (rdInternal.Checked)
                    {
                        progressBar1.Value = i;
                    }
                    int matchW = 0;
                    int matchM = 0;

                    for (int j = i; j >= i - 14; j--)
                    {
                        if ((rdWinning.Checked || rdWinningMachine.Checked))
                        {
                            if (dtTempExternal1.Rows[j]["Even_W"] == DBNull.Value)
                            {
                                continue;
                            }
                            if ((int)dtTempExternal1.Rows[j]["Even_W"] == (int)dtTempInternal.Rows[j - i + 14]["Even_W"])
                            {
                                matchW++;
                                DataRow rwFound = dtNumFound.NewRow();
                                rwFound["Id"] = dtTempExternal1.Rows[j]["Id"];
                                rwFound["colName"] = "W";
                                rwFound["RecNo"] = -1;
                                rwFound["DBId"] = dtTempExternal1.Rows[j]["DBId"];
                                rwFound["SNo"] = dtTempExternal1.Rows[j]["SNo"];
                                rwFound["W_M"] = "W";
                                dtNumFound.Rows.Add(rwFound);
                            }
                        }
                        if ((rdMachine.Checked || rdWinningMachine.Checked))
                        {
                            if ((int)dtTempExternal1.Rows[j]["Even_M"] == (int)dtTempInternal.Rows[j - i + 14]["Even_M"])
                            {
                                matchM++;
                                DataRow rwFound = dtNumFound.NewRow();
                                rwFound["Id"] = dtTempExternal1.Rows[j]["Id"];
                                rwFound["colName"] = "M";
                                rwFound["RecNo"] = -1;
                                rwFound["SNo"] = dtTempExternal1.Rows[j]["SNo"];
                                rwFound["DBId"] = dtTempExternal1.Rows[j]["DBId"];
                                rwFound["W_M"] = "M";
                                dtNumFound.Rows.Add(rwFound);
                            }
                        }
                    }
                    if (matchM + matchW < 10)
                    {
                        int noDel = matchM + matchW ;
                        for (int j = 1; j <= noDel; j++)
                        {
                            dtNumFound.Rows.RemoveAt(dtNumFound.Rows.Count - 1);
                            dtNumFound.AcceptChanges();
                        }
                    }
                    else
                    {
                        int startId = Convert.ToInt32(dtTempExternal1.Rows[i - 14]["Id"])-1;
                        int endId = Convert.ToInt32(dtTempExternal1.Rows[i]["Id"]);
                        DataRow[] rows = dtTempExternal.Select("Id > " + startId.ToString() + " and Id < " + (endId + 3).ToString());
                        int RecNo = 1;
                        DataRow[] rowMaxRecNo = dtOddEvenExternal.Select(" RecNo = MAX(RecNo)");
                        if (rowMaxRecNo.Length > 0)
                            RecNo = Convert.ToInt32(rowMaxRecNo[0]["RecNo"]) + 1;
                        int k = 0;
                        foreach (DataRow r in rows)
                        {
                            k++;
                            DataRow rowExternal = dtOddEvenExternal.NewRow();
                            foreach (DataColumn dc in dtOddEvenExternal.Columns)
                            {
                                rowExternal[dc] = r[dc.Caption];
                            }
                            rowExternal["RecNo"] = RecNo;
                            rowExternal["Count"] = matchM + matchW;
                            //if (k == rows.Length - 1 || k == rows.Length - 2)
                            {
                                if((int)r["Id"] > endId)
                                  rowExternal["RecNo"] = -1;
                            }
                            dtOddEvenExternal.Rows.Add(rowExternal);
                        }
                        DataRow rowExternalBlank = dtOddEvenExternal.NewRow();
                        rowExternalBlank["RecNo"] = RecNo;
                        rowExternalBlank["Count"] = matchM + matchW;
                        //rowExternalBlank["SNo"] = -1;
                        dtOddEvenExternal.Rows.Add(rowExternalBlank);

                        DataRow[] numFound = dtNumFound.Select("RecNo = -1");
                        foreach (DataRow r in numFound)
                            r["RecNo"] = RecNo;

                        DataRow[] rowW = dtNumFound.Select("RecNo =" + RecNo.ToString() + " AND W_M = 'W'");
                    if (rowW.Length > 0)
                    {
                        DataRow[] rowEx = dtOddEvenExternal.Select("RecNo =" + RecNo.ToString());
                        rowEx[0]["DBName"] = SqlClass.GetDBNameById((int)rowEx[0]["DBId"]);
                        rowEx[1]["DBName"] =(rowW.Length).ToString() + " Draw Matches in Winning";
                    }
                    DataRow[] rowM = dtNumFound.Select("RecNo =" + RecNo.ToString() + " AND W_M = 'M'");
                    if (rowM.Length > 0)
                    {
                        DataRow[] rowEx = dtOddEvenExternal.Select("RecNo =" + RecNo.ToString());
                        rowEx[0]["DBName"] = SqlClass.GetDBNameById((int)rowEx[0]["DBId"]);
                        rowEx[2]["DBName"] = (rowM.Length).ToString() + " Draw Matches in Machine";
                    }
                    }

                }
                if (rdInternal.Checked)
                {
                    progressBar1.Value = progressBar1.Maximum;
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void OddEvenSearch_Load(object sender, EventArgs e)
        {
            fillExternalCheckListBox();
            LoadDataSet();
            dtSearchNum = SqlClass.GetSearchNum().Clone();
            dtSearchNum.Columns.Add("Sym");
            dtSearchNum.Columns.Add("Id",Type.GetType("System.Int32"));
        }

        private void ResultGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if(rdOddEvenSearch.Checked)
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    foreach (DataRow row in dtNumFound.Rows)
                    {
                        if (ResultGrid.Columns[e.ColumnIndex].DataPropertyName.EndsWith(row["colName"].ToString()))
                        {
                            if (ResultGrid.Rows[e.RowIndex].Cells["RecNo"].Value != null && ResultGrid.Rows[e.RowIndex].Cells["RecNo"].Value != DBNull.Value && ResultGrid.Rows[e.RowIndex].Cells["SNo1"].Value != null && ResultGrid.Rows[e.RowIndex].Cells["SNo1"].Value != DBNull.Value)
                           
                            {if (Convert.ToInt32(ResultGrid.Rows[e.RowIndex].Cells["RecNo"].Value) == Convert.ToInt32(row["RecNo"]) && Convert.ToInt32(ResultGrid.Rows[e.RowIndex].Cells["SNo1"].Value) == Convert.ToInt32(row["SNo"]))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                            }
                            
                            }
                        }
                    }
                    if (ResultGrid.Rows[e.RowIndex].Cells["RecNo"].Value != null && ResultGrid.Rows[e.RowIndex].Cells["RecNo"].Value != DBNull.Value)
                    if (Convert.ToInt32(ResultGrid.Rows[e.RowIndex].Cells["RecNo"].Value) == -1)
                    {
                        e.CellStyle.BackColor = Color.LightBlue;
                    }
                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toggle(bool blnCode)
        {
            ResultGrid.Visible = !blnCode;
            oddEvenGrid.Visible = !blnCode;

            searchGrid.Visible = blnCode;
            grdResult.Visible = blnCode;
            searchGrid.BringToFront();
        }

        private void rdCodeSearch_CheckedChanged(object sender, EventArgs e)
        {
            toggle(rdCodeSearch.Checked);
            if (rdCodeSearch.Checked)
             LoadCodeDataSet();
        }
        private void LoadCodeDataSet()
        {
            try
            {
                dtCodeInternal = new DataTable();
                dtCodeInternal.Columns.Add("SNo",Type.GetType("System.Int32"));
                dtCodeInternal.Columns.Add("Date",Type.GetType("System.DateTime"));
                dtCodeInternal.Columns.Add("W1");
                dtCodeInternal.Columns.Add("W2");
                dtCodeInternal.Columns.Add("W3");
                dtCodeInternal.Columns.Add("W4");
                dtCodeInternal.Columns.Add("W5");
                dtCodeInternal.Columns.Add("SUM_W");
                dtCodeInternal.Columns.Add("M1");
                dtCodeInternal.Columns.Add("M2");
                dtCodeInternal.Columns.Add("M3");
                dtCodeInternal.Columns.Add("M4");
                dtCodeInternal.Columns.Add("M5");
                dtCodeInternal.Columns.Add("SUM_M");
                lblInternalDatabaseName.Text = (SqlClass.GetInternalDatabase()).Rows[0]["DBName"].ToString();
                int DBId = Convert.ToInt32((SqlClass.GetInternalDatabase()).Rows[0]["DBId"]);
                dtCodeTable = SqlClass.GetCodeList();
                getCodeMod(DBId, true);
                
                Date2.DefaultCellStyle.Format = "dd/MM/yyyy";
                searchGrid.DataSource = dtCodeInternal;
                if (dtSerchRes != null)
                    dtSerchRes.Rows.Clear();
            }
            catch (Exception ex)
            {
            }
        }

        private void getCodeMod(int DBId, bool Internal)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlClass.GetWin_Machin_DataByDBId(DBId, ref ds);
                int count = ds.Tables[0].Rows.Count;
                dtSearch = ds.Tables[0].Clone();
                int rowCount = ds.Tables[0].Rows.Count;
                int j = 1;
                int k = 0;
                while (k != 14)
                {
                    if (ds.Tables[0].Rows[rowCount - j]["W1"] != DBNull.Value)
                    {
                        k++;
                    }
                    j++;
                }
                k = 1;
                dtCodeExternal = dtCodeInternal.Clone();
                dtCodeExternal.Columns.Add("Id",Type.GetType("System.Int32"));
                for (int i = j; i > 0; i--)
                {
                    //if (Internal)
                    {
                        DataRow rowCodeEx = dtCodeExternal.NewRow();
                        DataRow rowCodeIn = dtCodeInternal.NewRow();
                        rowCodeEx["Id"] = ds.Tables[0].Rows[count - i]["Id"];
                        
                        rowCodeIn["Date"] = ds.Tables[0].Rows[count - i]["Date"];
                        rowCodeIn["SNo"] = ds.Tables[0].Rows[count - i]["SNo"];

                        rowCodeEx["Date"] = ds.Tables[0].Rows[count - i]["Date"];
                        rowCodeEx["SNo"] = ds.Tables[0].Rows[count - i]["SNo"];

                        if (ds.Tables[0].Rows[count - i]["W1"] != DBNull.Value && ds.Tables[0].Rows[count - i]["W1"] != null)
                        {
                            rowCodeIn["W1"] = getNoCode((int)ds.Tables[0].Rows[count - i]["W1"]) == "none" ? "" : getNoCode((int)ds.Tables[0].Rows[count - i]["W1"]);
                            rowCodeEx["W1"] = getNoCode((int)ds.Tables[0].Rows[count - i]["W1"]) == "none" ? "" : getNoCode((int)ds.Tables[0].Rows[count - i]["W1"]);
                            
                            DataRow rowSearch = dtSearch.NewRow();
                            foreach (DataColumn dc in dtSearch.Columns)
                            {
                                rowSearch[dc] = ds.Tables[0].Rows[count - i][dc.Caption];
                            }
                            dtSearch.Rows.Add(rowSearch);
                        }
                        if (ds.Tables[0].Rows[count - i]["W2"] != DBNull.Value && ds.Tables[0].Rows[count - i]["W2"] != null)
                        {
                            rowCodeIn["W2"] = getNoCode((int)ds.Tables[0].Rows[count - i]["W2"]) == "none" ? "" : getNoCode((int)ds.Tables[0].Rows[count - i]["W2"]);
                            rowCodeEx["W2"] = getNoCode((int)ds.Tables[0].Rows[count - i]["W2"]) == "none" ? "" : getNoCode((int)ds.Tables[0].Rows[count - i]["W2"]);
                        
                        }
                        if (ds.Tables[0].Rows[count - i]["W3"] != DBNull.Value && ds.Tables[0].Rows[count - i]["W3"] != null)
                        {
                            rowCodeIn["W3"] = getNoCode((int)ds.Tables[0].Rows[count - i]["W3"]) == "none" ? "" : getNoCode((int)ds.Tables[0].Rows[count - i]["W3"]);
                            rowCodeEx["W3"] = getNoCode((int)ds.Tables[0].Rows[count - i]["W3"]) == "none" ? "" : getNoCode((int)ds.Tables[0].Rows[count - i]["W3"]);
                     
                        }
                        if (ds.Tables[0].Rows[count - i]["W4"] != DBNull.Value && ds.Tables[0].Rows[count - i]["W4"] != null)
                        {
                            rowCodeIn["W4"] = getNoCode((int)ds.Tables[0].Rows[count - i]["W4"]) == "none" ? "" : getNoCode((int)ds.Tables[0].Rows[count - i]["W4"]);
                            rowCodeEx["W4"] = getNoCode((int)ds.Tables[0].Rows[count - i]["W4"]) == "none" ? "" : getNoCode((int)ds.Tables[0].Rows[count - i]["W4"]);
                     
                        }
                        if (ds.Tables[0].Rows[count - i]["W5"] != DBNull.Value && ds.Tables[0].Rows[count - i]["W5"] != null)
                        {
                            rowCodeIn["W5"] = getNoCode((int)ds.Tables[0].Rows[count - i]["W5"]) == "none" ? "" : getNoCode((int)ds.Tables[0].Rows[count - i]["W5"]);
                            rowCodeEx["W5"] = getNoCode((int)ds.Tables[0].Rows[count - i]["W5"]) == "none" ? "" : getNoCode((int)ds.Tables[0].Rows[count - i]["W5"]);
                    
                        }
                        rowCodeIn["Sum_W"] = ds.Tables[0].Rows[count - i]["Sum_W"];

                        if (ds.Tables[0].Rows[count - i]["M1"] != DBNull.Value && ds.Tables[0].Rows[count - i]["M1"] != null)
                        {
                            rowCodeIn["M1"] = getNoCode((int)ds.Tables[0].Rows[count - i]["M1"]) == "none" ? "" : getNoCode((int)ds.Tables[0].Rows[count - i]["M1"]);
                            rowCodeEx["M1"] = getNoCode((int)ds.Tables[0].Rows[count - i]["M1"]) == "none" ? "" : getNoCode((int)ds.Tables[0].Rows[count - i]["M1"]);
                     
                        }
                        if (ds.Tables[0].Rows[count - i]["M2"] != DBNull.Value && ds.Tables[0].Rows[count - i]["M2"] != null)
                        {
                            rowCodeIn["M2"] = getNoCode((int)ds.Tables[0].Rows[count - i]["M2"]) == "none" ? "" : getNoCode((int)ds.Tables[0].Rows[count - i]["M2"]);
                            rowCodeEx["M2"] = getNoCode((int)ds.Tables[0].Rows[count - i]["M2"]) == "none" ? "" : getNoCode((int)ds.Tables[0].Rows[count - i]["M2"]);
                      
                        }
                        if (ds.Tables[0].Rows[count - i]["M3"] != DBNull.Value && ds.Tables[0].Rows[count - i]["M3"] != null)
                        {
                            rowCodeIn["M3"] = getNoCode((int)ds.Tables[0].Rows[count - i]["M3"]) == "none" ? "" : getNoCode((int)ds.Tables[0].Rows[count - i]["M3"]);
                            rowCodeEx["M3"] = getNoCode((int)ds.Tables[0].Rows[count - i]["M3"]) == "none" ? "" : getNoCode((int)ds.Tables[0].Rows[count - i]["M3"]);
                        }
                        if (ds.Tables[0].Rows[count - i]["M4"] != DBNull.Value && ds.Tables[0].Rows[count - i]["M4"] != null)
                        {
                            rowCodeIn["M4"] = getNoCode((int)ds.Tables[0].Rows[count - i]["M4"]) == "none" ? "" : getNoCode((int)ds.Tables[0].Rows[count - i]["M4"]);
                            rowCodeEx["M4"] = getNoCode((int)ds.Tables[0].Rows[count - i]["M4"]) == "none" ? "" : getNoCode((int)ds.Tables[0].Rows[count - i]["M4"]);

                        }
                        if (ds.Tables[0].Rows[count - i]["M5"] != DBNull.Value && ds.Tables[0].Rows[count - i]["M5"] != null)
                        {
                            rowCodeIn["M5"] = getNoCode((int)ds.Tables[0].Rows[count - i]["M5"]) == "none" ? "" : getNoCode((int)ds.Tables[0].Rows[count - i]["M5"]);
                            rowCodeEx["M5"] = getNoCode((int)ds.Tables[0].Rows[count - i]["M5"]) == "none" ? "" : getNoCode((int)ds.Tables[0].Rows[count - i]["M5"]);
                        }
                        rowCodeIn["Sum_M"] = ds.Tables[0].Rows[count - i]["Sum_M"];
                        rowCodeEx["Sum_M"] = ds.Tables[0].Rows[count - i]["Sum_M"];
                       
                        dtCodeInternal.Rows.Add(rowCodeIn);
                        if (ds.Tables[0].Rows[count - i]["W1"] != DBNull.Value && ds.Tables[0].Rows[count - i]["W1"] != null)
                        {
                            dtCodeExternal.Rows.Add(rowCodeEx);
                        }
                    }
                    
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private string getNoCode(int No)
        {
            try
            {
                string Code = "";
                if (dtCodeTable.Rows[No - 1]["RN"] != DBNull.Value)
                    if (dtCodeTable.Rows[No - 1]["RN"].ToString().Trim() != "")
                    Code = dtCodeTable.Rows[No - 1]["RN"].ToString().Trim();
                if (dtCodeTable.Rows[No - 1]["CN"] != DBNull.Value)
                    if (dtCodeTable.Rows[No - 1]["CN"].ToString().Trim() != "")
                    Code += "," + dtCodeTable.Rows[No - 1]["CN"].ToString().Trim();
                if (dtCodeTable.Rows[No - 1]["TN"] != DBNull.Value)
                    if (dtCodeTable.Rows[No - 1]["TN"].ToString().Trim() != "")
                    Code += ","+ dtCodeTable.Rows[No - 1]["TN"].ToString().Trim();
                if (dtCodeTable.Rows[No - 1]["TCN"] != DBNull.Value)
                    if (dtCodeTable.Rows[No - 1]["TCN"].ToString().Trim() != "")
                     Code += "," +  dtCodeTable.Rows[No - 1]["TCN"].ToString().Trim();
                if (Code.StartsWith(","))
                  Code = Code.Remove(0, 1);
                return Code;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "none";
            }
        }

        private void grdResult_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        { 
           
            try
            {
              
                if(rdCodeSearch.Checked)
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //progressBar1.Maximum = grdResult.Rows.Count;
                    //progressBar1.Value = e.RowIndex;
                    
                     if (grdResult.Columns[e.ColumnIndex].DataPropertyName.StartsWith("W") || grdResult.Columns[e.ColumnIndex].DataPropertyName.StartsWith("M")) 
                      foreach (DataRow row in dtNumFound.Rows)
                       {
                       
                        if (grdResult.Columns[e.ColumnIndex].DataPropertyName == row["col"].ToString())
                        {
                           
                            if (grdResult.Rows[e.RowIndex].Cells["RecNo1"].Value != null && grdResult.Rows[e.RowIndex].Cells["RecNo1"].Value != DBNull.Value && grdResult.Rows[e.RowIndex].Cells["Id1"].Value != null && grdResult.Rows[e.RowIndex].Cells["Id1"].Value != DBNull.Value)
                            {
                                if (row["Id"] == DBNull.Value || row["Id"] == null)
                                {
                                }
                                if (Convert.ToInt32(grdResult.Rows[e.RowIndex].Cells["RecNo1"].Value) == Convert.ToInt32(row["RecNo"]) && Convert.ToInt32(grdResult.Rows[e.RowIndex].Cells["Id1"].Value) == Convert.ToInt32(row["Id"]))
                                {
                                        e.CellStyle.BackColor = Color.Red;
                                }
                        }
                        }
                    }
                     if (grdResult.Rows[e.RowIndex].Cells["Id1"].Value != DBNull.Value && grdResult.Rows[e.RowIndex].Cells["Id1"].Value != null)
                         if (Convert.ToInt32(grdResult.Rows[e.RowIndex].Cells["Id1"].Value) == 0)
                    {
                        e.CellStyle.BackColor = Color.LightBlue;
                    }

                }
                //if (e.RowIndex == grdResult.Rows.Count-1)
                //    progressBar1.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message );
            }
        }

        private void getOddEvenMod(int DBId, bool Internal)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlClass.GetWin_Machin_DataByDBId(DBId, ref ds);
                int count = ds.Tables[0].Rows.Count;

                ////for (int cnt = 0; cnt < count; cnt++)
                ////{
                ////    while ((cnt < count) && (ds.Tables[0].Rows[cnt]["W1"] == null || ds.Tables[0].Rows[cnt]["W1"] == DBNull.Value))
                ////    {
                ////        ds.Tables[0].Rows[cnt].Delete();
                ////        ds.Tables[0].AcceptChanges();
                ////        count = count - 1;
                ////    }
                ////}
                int rowCount = ds.Tables[0].Rows.Count;
                int j = 1;
                int k = 0;
                while (k != 14)
                {
                    if (ds.Tables[0].Rows[rowCount - j]["W1"] != DBNull.Value)
                    {
                        k++;
                    }
                    j++;
                }
                k = 1;
                for (int i = j; i > 0; i--)
                {
                    int cntOddW = 0;
                    int cntEvenW = 0;
                    int cntOddM = 0;
                    int cntEvenM = 0;
                    if (ds.Tables[0].Rows[count - i]["W1"] != DBNull.Value && ds.Tables[0].Rows[count - i]["W1"] != null)
                    {
                        foreach (DataColumn dc in ds.Tables[0].Columns)
                        {
                            if (dc.Caption.StartsWith("W") || dc.Caption.StartsWith("M"))
                            {
                                if (Convert.ToInt32(ds.Tables[0].Rows[count - i][dc]) % 2 == 0)
                                {
                                    if (dc.Caption.StartsWith("W"))
                                        cntEvenW++;
                                    else
                                        cntEvenM++;
                                }
                                else
                                {
                                    if (dc.Caption.StartsWith("W"))
                                        cntOddW++;
                                    else
                                        cntOddM++;
                                }
                            }
                        }
                        DataRow row = dtOddEvenInternal.NewRow();
                        row["Date"] = ds.Tables[0].Rows[count - i]["Date"];
                        row["SNo"] = ds.Tables[0].Rows[count - i]["SNo"];
                        row["Id"] = ds.Tables[0].Rows[count - i]["Id"];
                        row["Odd_W"] = cntOddW;
                        row["Odd_M"] = cntOddM;
                        row["Even_W"] = cntEvenW;
                        row["Even_M"] = cntEvenM;
                        dtOddEvenInternal.Rows.Add(row);
                    }
                    else
                    {
                        DataRow row = dtOddEvenInternal.NewRow();
                        row["Date"] = ds.Tables[0].Rows[count - i]["Date"];
                        row["SNo"] = ds.Tables[0].Rows[count - i]["SNo"];
                        row["Id"] = ds.Tables[0].Rows[count - i]["Id"];
                        dtOddEvenInternal.Rows.Add(row);
                    }
                }
                    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void CodeSearch()
        {
            try
            {
                dtNumFound = new DataTable();
                dtNumFound.Columns.Add("Id", Type.GetType("System.Int32"));
                dtNumFound.Columns.Add("col");
                dtNumFound.Columns.Add("RecNo", Type.GetType("System.Int32"));
                dtNumFound.Columns.Add("Sym");

                dtSerchRes = dtSearch.Clone();
                dtSerchRes.Columns.Add("RecNo", Type.GetType("System.Int32"));
                dtSerchRes.Columns.Add("RecordId", Type.GetType("System.Int32"));
                dtSerchRes.Columns.Add("space");
                dtSerchRes.Columns.Add("MatchType");
                dtSerchRes.Columns.Add("NumHits");
                dtSerchRes.Columns.Add("NosFound", Type.GetType("System.Int32"));
                dtSerchRes.Columns.Add("tNosFound", Type.GetType("System.Int32"));
                
                if (arSymboles == null)
                {
                    arSymboles = new ArrayList();
                }
                arSymboles.Clear();

                dtSearchNum.Rows.Clear();

                int Sym = 0;
                int RowNo = 0;
                foreach (DataRow row in dtCodeExternal.Rows)
                {
                    RowNo++;
                    foreach (DataColumn col in dtCodeExternal.Columns)
                    {
                        if (col.Caption.StartsWith("W") || col.Caption.StartsWith("M"))
                            if (!(row[col] == DBNull.Value || row[col] == null))
                            {
                                string[] dValues = row[col].ToString().Trim().Split(',');
                                foreach (string dVal in dValues)
                                {
                                    DataRow rowSerNum = dtSearchNum.NewRow();
                                    rowSerNum["Id"] = row["Id"];
                                    rowSerNum["Row"] = RowNo;
                                    rowSerNum["W"] = col.Caption;
                                    rowSerNum["dValue"] = dVal.Trim();
                                    if (rdCodeSearch.Checked)
                                    {
                                        rowSerNum["Sym"] = dVal.Trim().Split('N')[1];
                                        bool flag = true;
                                        for (int i = 0; i < arSymboles.Count; i++)
                                        {
                                            if (arSymboles[i].ToString() == dVal.Trim().Split('N')[1])
                                            {
                                                flag = false;
                                            }
                                        }
                                        if (flag)
                                        {
                                            arSymboles.Add(dVal.Trim().Split('N')[1]);
                                            Sym++;
                                        }
                                    }

                                    dtSearchNum.Rows.Add(rowSerNum);
                                }
                            }
                    }
                }



                if (rdInternal.Checked)
                {
                    DataTable dtInter = SqlClass.GetInternalDatabase();
                    if (dtInter.Rows.Count > 0)
                    {

                        int dbId = Convert.ToInt32(dtInter.Rows[0]["DBId"]);
                        MakeSearch(dbId);
                    }
                    
                }
                else
                {
                    DataTable dt = SqlClass.GetExternalDatabaseList();
                    progressBar1.Value = 0;
                    progressBar1.Maximum = chkExternalList.CheckedItems.Count;
                    for (int i = 0; i < chkExternalList.CheckedItems.Count; i++)
                    {
                        progressBar1.Value += 1;
                        DataRow[] rows = dt.Select("DBName = '" + chkExternalList.CheckedItems[i].ToString() + "'");
                        int dbId = Convert.ToInt32(rows[0]["DBId"]);
                        MakeSearch(dbId);
                    }
                    progressBar1.Value = progressBar1.Maximum;
                }

                progressBar1.Value = 0;
                Date3.DefaultCellStyle.Format = "dd/MM/yyyy";
                dtSerchRes.DefaultView.Sort = "NosFound desc,tNosFound desc";
                grdResult.DataSource = dtSerchRes.DefaultView;
                grdResult.DataSource = dtSerchRes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void MakeSearch(int DBId)
        {
            DataSet ds = new DataSet();

            if (dtBaseTable != null)
                dtBaseTable.Rows.Clear();
            SqlClass.GetWin_Machin_DataByDBId(DBId, ref ds);
            dtBaseTable = ds.Tables[0].Copy();

            //DataTable dtTargetdt = dtBaseTable.Copy();
            int count = dtBaseTable.Rows.Count;

            dtBaseTable.DefaultView.RowFilter = "W1 is Not null";
            DataTable dtTargetdt = dtBaseTable.DefaultView.ToTable();

            //for (int cnt = 0; cnt < count; cnt++)
            //{
            //    while ((cnt < count) && (dtTargetdt.Rows[cnt]["W1"] == null || dtTargetdt.Rows[cnt]["W1"] == DBNull.Value))
            //    {
            //        dtTargetdt.Rows[cnt].Delete();
            //        dtTargetdt.AcceptChanges();
            //        count = count - 1;
            //    }
            //}
            GeneralCodeSearch(dtTargetdt);
        }

        private void oldGeneralCodeSearch(DataTable dtTargetdt)
        {
            try
            {
                int matchFound = 0;
                int lastMatchFound = 0;
                DataTable dtRank = new DataTable();
                dtRank.Columns.Add("startId", Type.GetType("System.Int32"));
                dtRank.Columns.Add("endId", Type.GetType("System.Int32"));
                dtRank.Columns.Add("numFound", Type.GetType("System.Int32"));
                dtRank.Columns.Add("patrnFound", Type.GetType("System.Int32"));
                dtRank.Columns.Add("RecNo", Type.GetType("System.Int32"));
                int MaxRecNo = 1;
                DataRow[] rowMax = dtSerchRes.Select("RecNo = MAX(RecNo)");
                if (rowMax.Length > 0)
                    MaxRecNo = Convert.ToInt32(rowMax[0]["RecNo"]) + 1;

                DataTable dtTemp = dtNumFound.Clone();
                if (rdInternal.Checked)
                    progressBar1.Maximum = dtTargetdt.Rows.Count;
                for (int cnt = 0; cnt < dtTargetdt.Rows.Count; cnt++)
                {
                    if (rdInternal.Checked)
                        progressBar1.Value = cnt;
                    lastMatchFound = 0;
                    matchFound = 1;
                    for (int l = 0; l < arSymboles.Count; l++)
                    {
                        int lastMatchFound1 = 1;
                        int matchFound1 = 1;
                        DataRow[] rowSearchNum = dtSearchNum.Select("Sym = '" + arSymboles[l].ToString() + "'");
                        if (rowSearchNum.Length > 0)
                        {
                            for (int i = 0; i < rowSearchNum.Length - 1; i++)
                            {
                                int rowNo1 = Convert.ToInt32(rowSearchNum[i]["Row"]) - 1;
                                int lengh1 = rowSearchNum[i]["dValue"].ToString().Trim().Length;
                                string strNo1 = rowSearchNum[i]["dValue"].ToString().Substring(0, lengh1 - 1);
                                string col1 = rowSearchNum[i]["W"].ToString();
                                if (cnt + rowNo1 < dtTargetdt.Rows.Count)
                                {
                                    if (!(dtTargetdt.Rows[cnt + rowNo1][col1] == DBNull.Value || dtTargetdt.Rows[cnt + rowNo1][col1] == null))
                                    {
                                        int No1 = Convert.ToInt32(dtTargetdt.Rows[cnt + rowNo1][col1]);

                                        matchFound = 1;
                                        matchFound1 = 1;
                                        DataRow rowNumFound = dtTemp.NewRow();
                                        rowNumFound["Id"] = (int)dtTargetdt.Rows[cnt + rowNo1]["Id"];
                                        rowNumFound["col"] = rowSearchNum[i]["W"].ToString();
                                        rowNumFound["Sym"] = l;
                                        rowNumFound["RecNo"] = MaxRecNo;
                                        dtTemp.Rows.Add(rowNumFound);

                                        for (int j = i + 1; j < rowSearchNum.Length; j++)
                                        {
                                            int lengh2 = rowSearchNum[j]["dValue"].ToString().Trim().Length;
                                            string strNo2 = rowSearchNum[j]["dValue"].ToString().Substring(0, lengh2 - 1);
                                            int rowNo2 = Convert.ToInt32(rowSearchNum[j]["Row"]) - 1;
                                            string col2 = rowSearchNum[j]["W"].ToString();
                                            if (cnt + rowNo2 < dtTargetdt.Rows.Count)
                                            {
                                                if (!(dtTargetdt.Rows[cnt + rowNo2][col2] == DBNull.Value || dtTargetdt.Rows[cnt + rowNo2][col2] == null))
                                                {
                                                    int No2 = Convert.ToInt32(dtTargetdt.Rows[cnt + rowNo2][col2]);

                                                    if (checkMatch(strNo1, strNo2, No1, No2))
                                                    {
                                                        matchFound = matchFound + 1;
                                                        matchFound1 = matchFound1 + 1;
                                                        DataRow rowNumFound1 = dtTemp.NewRow();
                                                        rowNumFound1["Id"] = (int)dtTargetdt.Rows[cnt + rowNo2]["Id"];
                                                        rowNumFound1["col"] = rowSearchNum[j]["W"].ToString();
                                                        rowNumFound1["Sym"] = l;
                                                        rowNumFound1["RecNo"] = MaxRecNo;
                                                        dtTemp.Rows.Add(rowNumFound1);
                                                    }
                                                }
                                            }
                                        }
                                        if (lastMatchFound1 < matchFound1)
                                        {

                                            if (lastMatchFound1 != 1)
                                            {
                                                for (int k = 1; k <= lastMatchFound1; k++)
                                                {
                                                    dtNumFound.Rows[dtNumFound.Rows.Count - 1].Delete();
                                                    dtNumFound.AcceptChanges();
                                                }
                                            }
                                            foreach (DataRow rowTemp in dtTemp.Rows)
                                            {
                                                DataRow rowNumFound2 = dtNumFound.NewRow();
                                                rowNumFound2["Id"] = rowTemp["Id"];
                                                rowNumFound2["col"] = rowTemp["col"];
                                                rowNumFound2["Sym"] = rowTemp["Sym"];
                                                rowNumFound2["RecNo"] = rowTemp["RecNo"];
                                                dtNumFound.Rows.Add(rowNumFound2);
                                            }
                                            dtTemp.Rows.Clear();
                                            //lastMatchFound = matchFound;
                                            lastMatchFound1 = matchFound1;
                                        }
                                        dtTemp.Rows.Clear();
                                    }
                                }
                            }//
                            if (lastMatchFound1 > 1)
                                lastMatchFound += lastMatchFound1;

                        }

                    }
                    if (lastMatchFound > 4)
                    {
                        //totalResFound += 1;
                        DataRow rowRank = dtRank.NewRow();
                        rowRank["startId"] = (int)dtTargetdt.Rows[cnt]["Id"];
                        if (cnt + 14 >= dtTargetdt.Rows.Count)
                            rowRank["endId"] = (int)dtTargetdt.Rows[dtTargetdt.Rows.Count - 1]["Id"];
                        else
                            rowRank["endId"] = (int)dtTargetdt.Rows[cnt + 14]["Id"];
                        rowRank["numFound"] = lastMatchFound;
                        rowRank["patrnFound"] = 1;
                        rowRank["RecNo"] = MaxRecNo;
                        dtRank.Rows.Add(rowRank);
                        MaxRecNo++;
                    }

                }
                ImportToResultGenCodSearch(dtRank);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private int GetTurning(int RN)
        {
            int TN = RN;
            TN = (TN % 10) * 10 + TN / 10;
            if (TN > 90)
                TN = RN;
            return TN;
        }

        private int GetCounter(int RN)
        {
            if (RN <= 45)
                return RN + 45;
            else
                return RN - 45;
        }

        private bool checkCounter(int RN, int CN)
        {
            if (CN == GetCounter(RN))
                return true;
            return false;
        }

        private bool checkTurning(int RN, int TN)
        {
            if (TN == GetTurning(RN))
                return true;
            return false;
        }

        private bool checkMatch(string strNo1, string strNo2, int No1, int No2)
        {
            try
            {
                if (strNo1 == "RN" && No1 > 45 || strNo1 == "CN" && No1 < 46)
                    return false;
                if (strNo2 == "RN" && No2 > 45 || strNo2 == "CN" && No2 < 46)
                    return false;

                int temp;
                if (strNo1 == strNo2)
                    if (No1 == No2)
                        return true;
                    else
                        return false;
                switch (strNo1)
                {
                    case "RN":
                        if (strNo2 == "CN")
                        {
                            if (No1 == GetCounter(No2))
                                return true;
                            else
                                return false;
                        }
                        else if (strNo2 == "TN")
                        {
                            if (No1 == GetTurning(No2))
                                return true;
                            else
                                return false;
                        }
                        else
                        {
                            if (No1 == GetCounter(GetTurning(No2)))
                                return true;
                            else
                                return false;
                        }
                        break;

                    case "CN":
                        if (strNo2 == "RN")
                        {
                            if (No1 == GetCounter(No2))
                                return true;
                            else
                                return false;
                        }
                        else if (strNo2 == "TN")
                        {
                            if (No1 == GetCounter(GetTurning(No2)))
                                return true;
                            else
                                return false;
                        }
                        else
                        {
                            if (No1 == GetTurning(No2))
                                return true;
                            else
                                return false;
                        }
                        break;
                    case "TN":
                        if (strNo2 == "RN")
                        {
                            if (No1 == GetTurning(No2))
                                return true;
                            else
                                return false;
                        }
                        else if (strNo2 == "CN")
                        {
                            if (No1 == GetTurning(GetCounter(No2)))
                                return true;
                            else
                                return false;
                        }
                        else
                        {
                            temp = GetTurning(GetCounter(GetTurning(No2)));
                            if (No1 == temp)
                                return true;
                            else
                                return false;
                        }
                        break;
                    default:

                        if (strNo2 == "RN")
                        {
                            temp = GetTurning(GetTurning(No2));
                        }
                        else if (strNo2 == "CN")
                        {
                            temp = GetTurning(No2);
                        }
                        else
                        {
                            temp = GetTurning(GetCounter(GetTurning(No2)));
                        }
                        if (No1 == temp)
                            return true;
                        return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void ImportToResultGenCodSearch(DataTable dtRank)
        {
            try
            {
                int id = dtSerchRes.Rows.Count;
                dtRank.DefaultView.Sort = "numFound DESC";
                foreach (DataRowView rowView in dtRank.DefaultView)
                {

                    DataRow[] rows = dtBaseTable.Select("Id > " + ((int)rowView["startId"] - 1).ToString() + " and Id < " + ((int)rowView["endId"] + 3).ToString());
                    DataRow row4 = dtSerchRes.NewRow();
                    id = id + 1;
                    row4["RecordId"] = id;
                    row4["MatchType"] = "";
                    row4["NumHits"] = "";
                    row4["NosFound"] = rowView["numFound"];
                    dtSerchRes.Rows.Add(row4);



                    DataRow row1 = dtSerchRes.NewRow();
                    id = id + 1;
                    row1["RecordId"] = id;
                    int DBId = Convert.ToInt32(rows[0]["DBId"]);
                    row1["MatchType"] = SqlClass.GetDBNameById(DBId); //"Database :";
                    row1["NumHits"] = SqlClass.GetDBNameById(DBId);
                    row1["NosFound"] = rowView["numFound"];
                    dtSerchRes.Rows.Add(row1);



                    DataRow row2 = dtSerchRes.NewRow();
                    id = id + 1;
                    row2["RecordId"] = id;
                    row2["MatchType"] = rowView["numFound"].ToString() + " Mateches Found ";
                    row2["NumHits"] = 
                    row2["NosFound"] = rowView["numFound"];
                    dtSerchRes.Rows.Add(row2);


                    for (int i = 0; i < rows.Length; i++)
                    {
                        DataRow row = dtSerchRes.NewRow();

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
                        row["SNo"] = rows[i]["SNo"];
                        row["Id"] = rows[i]["Id"].ToString();
                        id = id + 1;
                        row["RecordId"] = id;
                        row["RecNo"] = rowView["RecNo"];
                        row["NosFound"] = rowView["numFound"];
                        if ((int)rows[i]["Id"] > (int)rowView["endId"])
                        {
                                row["Id"] = 0;
                        }
                        if(i==0)
                            row["MatchType"] = rowView["expression"];
                        dtSerchRes.Rows.Add(row);
                    }

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void GeneralCodeSearch(DataTable dtTargetdt)
        {
            try
            {
                DataTable dtRank = new DataTable();
                dtRank.Columns.Add("startId", Type.GetType("System.Int32"));
                dtRank.Columns.Add("endId", Type.GetType("System.Int32"));
                dtRank.Columns.Add("numFound", Type.GetType("System.Int32"));
                dtRank.Columns.Add("patrnFound", Type.GetType("System.Int32"));
                dtRank.Columns.Add("RecNo", Type.GetType("System.Int32"));
                dtRank.Columns.Add("expression");



                int MaxRecNo = 1;
                DataRow[] rowMax = dtSerchRes.Select("RecNo = MAX(RecNo)");
                if (rowMax.Length > 0)
                    MaxRecNo = Convert.ToInt32(rowMax[0]["RecNo"]) + 1;

                DataTable dtTemp = dtNumFound.Clone();
                if (rdInternal.Checked)
                    progressBar1.Maximum = dtTargetdt.Rows.Count;

                for (int cnt = 0; cnt < dtTargetdt.Rows.Count; cnt++)
                {
                    if (rdInternal.Checked)
                        progressBar1.Value = cnt;
                    for (int l = 0; l < arSymboles.Count; l++)
                    {
                        int lastMatchFound1 = 1;
                        int total = dtNumFound.Rows.Count;
                        int matchFound1 = 1;
                        dtTemp.Rows.Clear();
                        DataRow[] rowSearchNum = dtSearchNum.Select("Sym = '" + arSymboles[l].ToString() + "'");
                        if (rowSearchNum.Length > 0)
                        {
                            int startNo = 0;
                            int startId = 0;
                            string strNo = "";
                            for (int i = 0; i < rowSearchNum.Length - 1; i++)
                            {
                                int rowNo1 = Convert.ToInt32(rowSearchNum[i]["Row"]) - 1;
                                int lengh1 = rowSearchNum[i]["dValue"].ToString().Trim().Length;
                                string strNo1 = rowSearchNum[i]["dValue"].ToString().Substring(0, lengh1 - 1);
                                string col1 = rowSearchNum[i]["W"].ToString();
                                if (cnt + rowNo1 < dtTargetdt.Rows.Count)
                                {
                                    if (!(dtTargetdt.Rows[cnt + rowNo1][col1] == DBNull.Value || dtTargetdt.Rows[cnt + rowNo1][col1] == null))
                                    {
                                        int No1 = Convert.ToInt32(dtTargetdt.Rows[cnt + rowNo1][col1]);

                                        matchFound1 = 1;
                                        int rId1 = (int)dtTargetdt.Rows[cnt + rowNo1]["Id"];
                                        string colName1 = rowSearchNum[i]["W"].ToString();
                                        DataRow[] rowDup1 = dtTemp.Select("Id = " + rId1.ToString() + " and col  = '" + colName1 + "' and Sym = '" + l.ToString() + "' and RecNo = " + MaxRecNo.ToString());
                                        if (rowDup1.Length != 0)
                                        {
                                            continue;
                                        }

                                        DataRow rowNumFound = dtTemp.NewRow();
                                        rowNumFound["Id"] = (int)dtTargetdt.Rows[cnt + rowNo1]["Id"];
                                        rowNumFound["col"] = rowSearchNum[i]["W"].ToString();
                                        rowNumFound["Sym"] = l;
                                        rowNumFound["RecNo"] = MaxRecNo;
                                        dtTemp.Rows.Add(rowNumFound);

                                        for (int j = i + 1; j < rowSearchNum.Length; j++)
                                        {
                                            int lengh2 = rowSearchNum[j]["dValue"].ToString().Trim().Length;
                                            string strNo2 = rowSearchNum[j]["dValue"].ToString().Substring(0, lengh2 - 1);
                                            int rowNo2 = Convert.ToInt32(rowSearchNum[j]["Row"]) - 1;
                                            string col2 = rowSearchNum[j]["W"].ToString();

                                            if (cnt + rowNo2 < dtTargetdt.Rows.Count)
                                            {
                                                if (!(dtTargetdt.Rows[cnt + rowNo2][col2] == DBNull.Value || dtTargetdt.Rows[cnt + rowNo2][col2] == null))
                                                {
                                                    int No2 = Convert.ToInt32(dtTargetdt.Rows[cnt + rowNo2][col2]);

                                                    if (checkMatch(strNo1, strNo2, No1, No2))
                                                    {
                                                        int rId = (int)dtTargetdt.Rows[cnt + rowNo2]["Id"];
                                                        string colName = rowSearchNum[j]["W"].ToString();
                                                        DataRow[] rowDup = dtTemp.Select("Id = " + rId.ToString() + " and col  = '" + colName + "' and Sym = '" + l.ToString() + "' and RecNo = " + MaxRecNo.ToString());
                                                        if (rowDup.Length == 0)
                                                        {
                                                            matchFound1 = matchFound1 + 1;
                                                            DataRow rowNumFound1 = dtTemp.NewRow();
                                                            rowNumFound1["Id"] = rId;
                                                            rowNumFound1["col"] = colName;
                                                            rowNumFound1["Sym"] = l;
                                                            rowNumFound1["RecNo"] = MaxRecNo;
                                                            dtTemp.Rows.Add(rowNumFound1);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        if (lastMatchFound1 < matchFound1 && matchFound1 >= 3)
                                        {
                                            startNo = No1;
                                            startId = i;
                                            strNo = strNo1;
                                            if (lastMatchFound1 != 1)
                                            {
                                                for (int k = 1; k <= lastMatchFound1; k++)
                                                {
                                                    dtNumFound.Rows[dtNumFound.Rows.Count - 1].Delete();
                                                    dtNumFound.AcceptChanges();
                                                }
                                            }
                                            foreach (DataRow rowTemp in dtTemp.Rows)
                                            {
                                                DataRow rowNumFound2 = dtNumFound.NewRow();
                                                rowNumFound2["Id"] = rowTemp["Id"];
                                                rowNumFound2["col"] = rowTemp["col"];
                                                rowNumFound2["Sym"] = rowTemp["Sym"];
                                                rowNumFound2["RecNo"] = rowTemp["RecNo"];
                                                dtNumFound.Rows.Add(rowNumFound2);
                                            }
                                            
                                            dtTemp.Rows.Clear();
                                            lastMatchFound1 = matchFound1;
                                        }
                                        dtTemp.Rows.Clear();
                                    }

                                }
                                else
                                {
                                    break;
                                }
                            }//
                            total = dtNumFound.Rows.Count - total;
                            if (total >= 3)
                            {
                                
                                //totalResFound += 1;
                                DataRow rowRank = dtRank.NewRow();
                                rowRank["startId"] = (int)dtTargetdt.Rows[cnt]["Id"];
                                if (cnt + 14 >= dtTargetdt.Rows.Count)
                                    rowRank["endId"] = (int)dtTargetdt.Rows[dtTargetdt.Rows.Count - 1]["Id"];
                                else
                                    rowRank["endId"] = (int)dtTargetdt.Rows[cnt + 14]["Id"];
                                rowRank["numFound"] = total;
                                rowRank["patrnFound"] = 1;
                                rowRank["RecNo"] = MaxRecNo;
                                string exp = strNo + " = " + startNo.ToString();
                                rowRank["expression"] = exp;
                                dtRank.Rows.Add(rowRank);
                                MaxRecNo++;
                            }
                        }


                    }


                }
                ImportToResultGenCodSearch(dtRank);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if(dtSearchNum != null)
                   dtSearchNum.Rows.Clear();

                if (dtOddEvenExternal != null)
                    dtOddEvenExternal.Rows.Clear();

                if (dtSerchRes != null)
                    dtSerchRes.Rows.Clear();

                if (arSymboles != null)
                    arSymboles.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void fillExternalCheckListBox()
        {
            try
            {
                DataTable dt = SqlClass.GetExternalDatabaseList();
                chkExternalList.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    chkExternalList.Items.Add(row["DBName"].ToString(), true);

                }
                chkExternalList.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rdInternal_CheckedChanged(object sender, EventArgs e)
        {

            chkExternalList.Enabled = !rdInternal.Checked;
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chkExternalList.Items.Count; i++)
            {
                chkExternalList.SetItemChecked(i, true);
            }
        }

        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chkExternalList.Items.Count; i++)
            {
                chkExternalList.SetItemChecked(i, false);
            }
        }

       

    }
}
