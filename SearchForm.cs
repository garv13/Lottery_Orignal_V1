using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.Xml.Xsl;
using System.IO;

namespace SystamaticDBSearch
{
    public partial class SearchForm : Form
    {
        public SearchForm()
        {
            InitializeComponent();
        }

        private static DataSet dsSearch;
        private static DataTable dtSearchNum;
        private static DataTable dtSerchRes;
        private static DataTable dtBaseTable;
        private static DataTable dtNumFound;
        public DataTable dtFullcellSearch;
        private static ArrayList arSymboles;
        private static int totalResFound;
        private static DataTable dtSummary;
        private static DataTable dtLastRows;
        private static DataTable dtCodeTable;
        
        private void PrepareSearchGrid()
        {
            
            dsSearch = new DataSet();
            SqlClass.GetSearchPlaneData(ref dsSearch);
            Date.DefaultCellStyle.Format = "dd/MM/yyyy";
            searchGrid.DataSource = dsSearch.Tables[0];
            dtFullcellSearch = dsSearch.Tables[0].Clone();
            ClearSearchPanel();
            dtNumFound = new DataTable();
            dtNumFound.Columns.Add("Id");
            dtNumFound.Columns.Add("col");
            dtNumFound.Columns.Add("RecNo");
        }

        private void SearchForm_Load(object sender, EventArgs e)
        {
          
            PrepareSearchGrid();
            fillExternalCheckListBox();
            if((SqlClass.GetInternalDatabase()).Rows.Count > 0)
             lblInternalDatabaseName.Text = (SqlClass.GetInternalDatabase()).Rows[0]["DBName"].ToString();
            dtCodeTable = SqlClass.GetCodeList();
            CreateSummaryTable();
        }

        private void CreateSummaryTable()
        {
            try
            {
                dtSummary = new DataTable();
                dtSummary.Columns.Add("Id" ,Type.GetType("System.Int32"));
                dtSummary.Columns.Add("Number");
                dtSummary.Columns.Add("Description");
                dtSummary.Columns.Add("cnt", Type.GetType("System.Int32"));

                dtLastRows = new DataTable();
                dtLastRows.Columns.Add("Id");
                dtLastRows.Columns.Add("RecordId");
                dtLastRows.Columns.Add("DBId");
                dtLastRows.Columns.Add("RecNo");
                dtLastRows.Columns.Add("W1");
                dtLastRows.Columns.Add("W2");
                dtLastRows.Columns.Add("W3");
                dtLastRows.Columns.Add("W4");
                dtLastRows.Columns.Add("W5");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PrepareSummary()
        {
            int b = 0;
            try
            {
                int MaxCount = 1;
                for (int i = 0; i < dtLastRows.Rows.Count ; i++)
                {
                    
                    foreach (DataColumn dc in dtLastRows.Columns)
                    {
                        if(dc.Caption.StartsWith("W"))
                        {
                            if (!(dtLastRows.Rows[i][dc] == null || dtLastRows.Rows[i][dc] == DBNull.Value))
                            {
                                if (dtLastRows.Rows[i][dc].ToString().Trim() != "")
                                {
                                    string num = Convert.ToString(dtLastRows.Rows[i][dc]);
                                    DataRow[] rowSum = dtSummary.Select("Number = '" + num + "'");
                                    if (rowSum.Length > 0)
                                    {
                                        rowSum[0]["cnt"] = (Convert.ToInt32(rowSum[0]["cnt"]) + 1).ToString();
                                        rowSum[0]["Description"] = "Appears " + rowSum[0]["cnt"].ToString() + " times";
                                        if (MaxCount < Convert.ToInt32(rowSum[0]["cnt"]))
                                            MaxCount = Convert.ToInt32(rowSum[0]["cnt"]);
                                    }
                                    else
                                    {
                                        DataRow newSum = dtSummary.NewRow();
                                        newSum["Id"] = dtSummary.Rows.Count + 1;
                                        newSum["Number"] = dtLastRows.Rows[i][dc];
                                        newSum["cnt"] = 1;
                                        newSum["Description"] = "Appers 1 time";
                                        dtSummary.Rows.Add(newSum);
                                    }
                                }
                            }
                        }
                    }
                }
                DataTable dtPairs = new DataTable();
                dtPairs.Columns.Add("p1", Type.GetType("System.Int32"));
                dtPairs.Columns.Add("p2", Type.GetType("System.Int32"));
                for (int i = 0; i < dtSummary.Rows.Count - 1; i++)
                {
                    for (int j = i+1; j < dtSummary.Rows.Count - 1; j++)
                    {
                        DataRow row = dtPairs.NewRow();
                        row["p1"] = dtSummary.Rows[i]["Number"];
                        row["p2"] = dtSummary.Rows[j]["Number"];
                        dtPairs.Rows.Add(row);
                    }
                }
                
                foreach (DataRow rPair in dtPairs.Rows)
                {
                    int CountPair = 0;
                    //break;
                    foreach (DataRow rLast in dtLastRows.Rows)
                    {
                        bool bBreak = false;
                        foreach (DataColumn dc in dtLastRows.Columns)
                        {
                            if (dc.Caption.StartsWith("W") && (!(rLast[dc] == null || rLast[dc] == DBNull.Value)))
                            {
                               if(rLast[dc].ToString().Trim() != "" )
                                foreach (DataColumn dc1 in dtLastRows.Columns)
                                {

                                    if (dc1.Caption.StartsWith("W"))
                                    {
                                        if (!( rLast[dc1] == null || rLast[dc1] == DBNull.Value))
                                        {
                                            if (rLast[dc1].ToString().Trim() != "")
                                                if ((Convert.ToInt32(rPair["p1"]) == Convert.ToInt32(rLast[dc]) && Convert.ToInt32(rPair["p2"]) == Convert.ToInt32(rLast[dc1])) || (Convert.ToInt32(rPair["p2"]) == Convert.ToInt32(rLast[dc]) && Convert.ToInt32(rPair["p1"]) == Convert.ToInt32(rLast[dc1])))
                                                {
                                                    CountPair++;
                                                    bBreak = true;
                                                    break;
                                                }
                                        }
                                    }
                                }
                                if (bBreak)
                                    break;
                            }
                        }
                    }
                    DataRow newSum = dtSummary.NewRow();
                    newSum["Id"] = dtSummary.Rows.Count + 1;
                    newSum["Number"] = rPair["p1"].ToString() + " , " + rPair["p2"].ToString();
                    newSum["cnt"] = CountPair ;
                    newSum["Description"] = "Appers " + CountPair.ToString() + " times";
                    dtSummary.Rows.Add(newSum);

                }

                dtSummary.DefaultView.RowFilter = "cnt > 1";
                dtSummary.DefaultView.Sort = "cnt desc";
                grdSummary.DataSource = dtSummary.DefaultView;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
           
        }

        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (rdExternalDB.Checked)
            {
                panelExternalDatabse.Enabled = true;
                lblInternalDatabaseName.Visible = false;
            }
            else
            {
                panelExternalDatabse.Enabled = false;
                lblInternalDatabaseName.Visible = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
               
                PreSettingSearch();
               
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region Exact Search

        private void ExectSearch(DataTable dtTargetdt)
        {
            try
            {
                DataTable dtRankFound = new DataTable();
                dtRankFound.Columns.Add("startIndex");
                dtRankFound.Columns.Add("endIndex");
                dtRankFound.Columns.Add("numFound");
                int RecNo = 0;
                if (rdInternalDB.Checked)
                    progressBar1.Maximum = dtTargetdt.Rows.Count;
                int numFound = 0;
                for (int cnt = 0; cnt < dtTargetdt.Rows.Count; cnt++)
                {
                    if (rdInternalDB.Checked)
                        progressBar1.Value = cnt;
                    DataRow[] rowMaxRecNo = dtNumFound.Select("RecNo = MAX(RecNo)");
                    if (rowMaxRecNo.Length > 0)
                        RecNo = Convert.ToInt32(rowMaxRecNo[0]["RecNo"]) + 1;

                    numFound = 0;
                    for (int i = 0; i < dtSearchNum.Rows.Count; i++)
                    {
                        int rowNo = (int)dtSearchNum.Rows[i]["Row"] - 1;
                        if (cnt + rowNo < dtTargetdt.Rows.Count)
                            if (!(dtTargetdt.Rows[cnt + rowNo][dtSearchNum.Rows[i]["W"].ToString()] == null || dtTargetdt.Rows[cnt + rowNo][dtSearchNum.Rows[i]["W"].ToString()] == DBNull.Value))
                                if (Convert.ToInt32(dtSearchNum.Rows[i]["dValue"]) == (int)dtTargetdt.Rows[cnt + rowNo][dtSearchNum.Rows[i]["W"].ToString()])
                                {
                                    numFound = numFound + 1;
                                    DataRow rowNumFound = dtNumFound.NewRow();
                                    rowNumFound["Id"] = (int)dtTargetdt.Rows[cnt + rowNo]["Id"];
                                    rowNumFound["col"] = dtSearchNum.Rows[i]["W"].ToString();
                                    rowNumFound["RecNo"] = RecNo;
                                    dtNumFound.Rows.Add(rowNumFound);
                                }
                    }
                    if (numFound > 1 || (dtSearchNum.Rows.Count == 1 && numFound == 1))
                    {
                        totalResFound += 1;
                        int startIndex = cnt;
                        int endIndex = startIndex + 14;
                        if (endIndex >= dtTargetdt.Rows.Count)
                        {
                            endIndex = dtTargetdt.Rows.Count - 1;
                        }
                        int lastIndex = startIndex + 14;
                        if (startIndex + 16 < dtTargetdt.Rows.Count)
                            lastIndex = startIndex + 16;
                        else if (startIndex + 15 < dtTargetdt.Rows.Count)
                            lastIndex = startIndex + 15;
                        else
                            lastIndex = dtTargetdt.Rows.Count - 1;

                        int startIndexId = (int)dtTargetdt.Rows[startIndex]["Id"];
                        int endIndexId = (int)dtTargetdt.Rows[endIndex]["Id"];
                        lastIndex = (int)dtTargetdt.Rows[lastIndex]["Id"];
                        ImportToResultM(startIndexId, endIndexId, numFound, RecNo, lastIndex);
                    }
                    else
                    {
                        if (dtSearchNum.Rows.Count > 1 && numFound == 1)
                        {
                            dtNumFound.Rows.RemoveAt(dtNumFound.Rows.Count - 1);
                            dtNumFound.AcceptChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        private void ImportToResultM(int startIndex, int endIndex, int Hits, int RecNo,int lastIndex)
        {
            try
            {
                int id = dtSerchRes.Rows.Count;
                DataRow row1 = dtSerchRes.NewRow();
                id = id + 1;
                row1["Id"] = id;
                row1["MatchType"] = "";
                row1["NumHits"] = "";
                row1["NosFound"] = Hits;
                dtSerchRes.Rows.Add(row1);

                DataRow row2 = dtSerchRes.NewRow();
                id = id + 1;
                row2["Id"] = id;
                row2["MatchType"] = "Total No Found ";
                row2["NumHits"] = Hits.ToString();
                row2["NosFound"] = Hits;

                dtSerchRes.Rows.Add(row2);
                int lastId = endIndex;

                DataRow[] rows = dtBaseTable.Select("Id > " + (startIndex - 1).ToString() + " and Id < " + (lastIndex + 1).ToString());
                if (rdBottomToTop.Checked)
                {
                    rows = dtBaseTable.Select("Id > " + (lastIndex - 1).ToString() + " and Id < " + (startIndex + 1).ToString());
                    lastId = startIndex;
                }
                DataRow row3 = dtSerchRes.NewRow();
                id = id + 1;
                row3["Id"] = id;
                int DBId = Convert.ToInt32(rows[0]["DBId"]);
                row3["MatchType"] = "Database :";
                row3["NumHits"] = SqlClass.GetDBNameById(DBId);
                row3["NosFound"] = Hits;
                dtSerchRes.Rows.Add(row3);

                for (int j = 0; j < rows.Length; j++)
                {
                    int i = j;
                    //if (rdBottomToTop.Checked)
                    //    i = rows.Length - 1 - j;

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
                    row["RecordId"] = rows[i]["Id"].ToString();
                    row["RecNo"] = RecNo;
                    if (rdTopToBottom.Checked)
                    {
                        if ((int)rows[i]["Id"] != lastId && (int)rows[i]["Id"] != lastId - 1)
                        {
                            if (i == rows.Length - 1 || i == rows.Length - 2)
                            {
                                row["RecordId"] = 0;
                                if (i == rows.Length - 2)
                                {
                                    DataRow rowLast = dtLastRows.NewRow();
                                    rowLast["Id"] = id;
                                    rowLast["RecordId"] = rows[i]["Id"].ToString();
                                    rowLast["DBId"] = rows[i]["DBId"].ToString();
                                    rowLast["RecNo"] = RecNo;
                                    rowLast["W1"] = rows[i]["W1"].ToString();
                                    rowLast["W2"] = rows[i]["W2"].ToString();
                                    rowLast["W3"] = rows[i]["W3"].ToString();
                                    rowLast["W4"] = rows[i]["W4"].ToString();
                                    rowLast["W5"] = rows[i]["W5"].ToString();
                                    dtLastRows.Rows.Add(rowLast);
                                }
                            }
                        }
                    }
                    else
                    {
                        if ((int)rows[i]["Id"] != endIndex && (int)rows[i]["Id"] != endIndex + 1)
                        {
                            if (i == 0 || i == 1)
                            {
                                row["RecordId"] = 0;
                                if (i == 1)
                                {
                                    DataRow rowLast = dtLastRows.NewRow();
                                    rowLast["Id"] = id;
                                    rowLast["RecordId"] = rows[i]["Id"].ToString();
                                    rowLast["DBId"] = rows[i]["DBId"].ToString();
                                    rowLast["RecNo"] = RecNo;
                                    rowLast["W1"] = rows[i]["W1"].ToString();
                                    rowLast["W2"] = rows[i]["W2"].ToString();
                                    rowLast["W3"] = rows[i]["W3"].ToString();
                                    rowLast["W4"] = rows[i]["W4"].ToString();
                                    rowLast["W5"] = rows[i]["W5"].ToString();
                                    dtLastRows.Rows.Add(rowLast);
                                }
                            }
                        }
                    }
                    row["NosFound"] = Hits;

                    id = id + 1;

                    row["Id"] = id;

                    dtSerchRes.Rows.Add(row);


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 
        #endregion

        #region Fullcell Search

        private void FullCellSearchMOD(DataTable dtTargetdt)
        {
            try
            {

                int numFound = 0;
                int totalNumFound = 0;
                DataTable dtRank = new DataTable();
                dtRank.Columns.Add("RecNo", Type.GetType("System.Int32"));
                dtRank.Columns.Add("startId", Type.GetType("System.Int32"));
                dtRank.Columns.Add("endId", Type.GetType("System.Int32"));
                dtRank.Columns.Add("lastId", Type.GetType("System.Int32"));
                dtRank.Columns.Add("numFound", Type.GetType("System.Int32"));
                dtRank.Columns.Add("totalNumFound", Type.GetType("System.Int32"));

                DataTable dtSearchTemp = dsSearch.Tables[0].Copy();
                if (rdBottomToTop.Checked)
                    dtSearchTemp = ReversreSearchTable();
                int count = dtSearchTemp.Rows.Count;
                for (int cnt = 0; cnt < count; cnt++)
                {
                    while ((cnt < count) && (dtSearchTemp.Rows[cnt]["W1"] == null || dtSearchTemp.Rows[cnt]["W1"] == DBNull.Value))
                    {
                        dtSearchTemp.Rows[cnt].Delete();
                        dtSearchTemp.AcceptChanges();
                        count = count - 1;
                    }
                }
                if (rdInternalDB.Checked)
                {
                    progressBar1.Maximum = dtTargetdt.Rows.Count;

                }
                for (int cnt = 14; cnt < dtTargetdt.Rows.Count; )
                {
                    if (rdInternalDB.Checked)
                        progressBar1.Value = cnt;

                    numFound = 0;
                    totalNumFound = 0;
                    foreach (DataColumn dc in dtSearchTemp.Columns)
                    {
                        if (dc.Caption != "Id" && dc.Caption != "Date" && dc.Caption != "SUM_W" && dc.Caption != "SUM_M")
                        {
                            if (dtSearchTemp.Rows[14][dc] != null && dtSearchTemp.Rows[14][dc] != DBNull.Value && dtTargetdt.Rows[cnt][dc.Caption] != null && dtTargetdt.Rows[cnt][dc.Caption] != DBNull.Value)
                            {
                                if (Convert.ToInt32(dtSearchTemp.Rows[14][dc]) == (int)dtTargetdt.Rows[cnt][dc.Caption])
                                {
                                    numFound += 1;
                                    totalNumFound += 1;
                                    DataRow rowNumFound = dtNumFound.NewRow();
                                    rowNumFound["Id"] = (int)dtTargetdt.Rows[cnt]["Id"];
                                    rowNumFound["col"] = dc.Caption;
                                    rowNumFound["RecNo"] = dtRank.Rows.Count + 1;
                                    dtNumFound.Rows.Add(rowNumFound);
                                }
                                for (int i = 0; i < 14; i++)
                                {
                                    if (!(dtSearchTemp.Rows[i][dc] == null || dtSearchTemp.Rows[i][dc] == DBNull.Value || dtTargetdt.Rows[cnt - 14 + i][dc.Caption] == DBNull.Value || dtTargetdt.Rows[cnt - 14 + i][dc.Caption] == null))

                                        if (Convert.ToInt32(dtSearchTemp.Rows[i][dc]) == (int)dtTargetdt.Rows[cnt - 14 + i][dc.Caption])
                                        {
                                            totalNumFound += 1;
                                            DataRow rowNumFound = dtNumFound.NewRow();
                                            rowNumFound["Id"] = (int)dtTargetdt.Rows[cnt - 14 + i]["Id"];
                                            rowNumFound["col"] = dc.Caption;
                                            rowNumFound["RecNo"] = dtRank.Rows.Count + 1;
                                            dtNumFound.Rows.Add(rowNumFound);
                                        }
                                }
                            }
                        }
                    }
                    if (numFound == 0 || totalNumFound <= 2)
                    {
                        for (int i = 1; i <= totalNumFound; i++)
                        {
                            dtNumFound.Rows[dtNumFound.Rows.Count - 1].Delete();
                            dtNumFound.AcceptChanges();
                        }
                    }
                    if (numFound > 0 && totalNumFound > 2)
                    {
                        totalResFound += 1;
                        DataRow rowRank = dtRank.NewRow();
                        rowRank["startId"] = (int)dtTargetdt.Rows[cnt - 14]["Id"];
                        if (cnt >= dtTargetdt.Rows.Count)
                        {
                            cnt = dtTargetdt.Rows.Count - 1;
                        }
                        rowRank["endId"] = (int)dtTargetdt.Rows[cnt]["Id"];
                        
                        if(dtTargetdt.Rows.Count > cnt + 2)
                            rowRank["lastId"] = (int)dtTargetdt.Rows[cnt+2]["Id"];
                        else if (dtTargetdt.Rows.Count > cnt + 1)
                            rowRank["lastId"] = (int)dtTargetdt.Rows[cnt + 1]["Id"];
                        else
                            rowRank["lastId"] = (int)dtTargetdt.Rows[cnt]["Id"];
                        rowRank["numFound"] = numFound;
                        rowRank["totalNumFound"] = totalNumFound;
                        rowRank["RecNo"] = dtRank.Rows.Count + 1;
                        dtRank.Rows.Add(rowRank);
                        //cnt += 15;
                    }
                    //else
                    cnt++;
                }
                ImportToResultFullCellSearch(dtRank);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FullCellSearchBottomUp(DataTable dtTargetdt)
        {
            try
            {

                int numFound = 0;
                int totalNumFound = 0;
                DataTable dtRank = new DataTable();
                dtRank.Columns.Add("RecNo", Type.GetType("System.Int32"));
                dtRank.Columns.Add("startId", Type.GetType("System.Int32"));
                dtRank.Columns.Add("endId", Type.GetType("System.Int32"));
                dtRank.Columns.Add("lastId", Type.GetType("System.Int32"));
                dtRank.Columns.Add("numFound", Type.GetType("System.Int32"));
                dtRank.Columns.Add("totalNumFound", Type.GetType("System.Int32"));

                DataTable dtSearchTemp = dsSearch.Tables[0].Copy();
                if (rdBottomToTop.Checked)
                    dtSearchTemp = ReversreSearchTable();
                int count = dtSearchTemp.Rows.Count;
                for (int cnt = 0; cnt < count; cnt++)
                {
                    while ((cnt < count) && (dtSearchTemp.Rows[cnt]["W1"] == null || dtSearchTemp.Rows[cnt]["W1"] == DBNull.Value))
                    {
                        dtSearchTemp.Rows[cnt].Delete();
                        dtSearchTemp.AcceptChanges();
                        count = count - 1;
                    }
                }
                if (rdInternalDB.Checked)
                {
                    progressBar1.Maximum = dtTargetdt.Rows.Count;

                }
                for (int cnt = 14; cnt < dtTargetdt.Rows.Count; )
                {
                    if (rdInternalDB.Checked)
                        progressBar1.Value = cnt;

                    numFound = 0;
                    totalNumFound = 0;
                    foreach (DataColumn dc in dtSearchTemp.Columns)
                    {
                        if (dc.Caption != "Id" && dc.Caption != "Date" && dc.Caption != "SUM_W" && dc.Caption != "SUM_M")
                        {
                            if (dtSearchTemp.Rows[0][dc] != null && dtSearchTemp.Rows[0][dc] != DBNull.Value && dtTargetdt.Rows[cnt-14][dc.Caption] != null && dtTargetdt.Rows[cnt-14][dc.Caption] != DBNull.Value)
                            {
                                if (Convert.ToInt32(dtSearchTemp.Rows[0][dc]) == (int)dtTargetdt.Rows[cnt-14][dc.Caption])
                                {
                                    numFound += 1;
                                    totalNumFound += 1;
                                    DataRow rowNumFound = dtNumFound.NewRow();
                                    rowNumFound["Id"] = (int)dtTargetdt.Rows[cnt-14]["Id"];
                                    rowNumFound["col"] = dc.Caption;
                                    rowNumFound["RecNo"] = dtRank.Rows.Count + 1;
                                    dtNumFound.Rows.Add(rowNumFound);
                                }
                                for (int i = 1; i <= 14; i++)
                                {
                                    if (cnt - 14 + i < dtTargetdt.Rows.Count)
                                    {
                                        if (!(dtSearchTemp.Rows[i][dc] == null || dtSearchTemp.Rows[i][dc] == DBNull.Value || dtTargetdt.Rows[cnt - 14 + i][dc.Caption] == DBNull.Value || dtTargetdt.Rows[cnt - 14 + i][dc.Caption] == null))
                                        {
                                            if (Convert.ToInt32(dtSearchTemp.Rows[i][dc]) == (int)dtTargetdt.Rows[cnt - 14 + i][dc.Caption])
                                            {
                                                totalNumFound += 1;
                                                DataRow rowNumFound = dtNumFound.NewRow();
                                                rowNumFound["Id"] = (int)dtTargetdt.Rows[cnt - 14 + i]["Id"];
                                                rowNumFound["col"] = dc.Caption;
                                                rowNumFound["RecNo"] = dtRank.Rows.Count + 1;
                                                dtNumFound.Rows.Add(rowNumFound);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (numFound == 0 || totalNumFound <= 2)
                    {
                        for (int i = 1; i <= totalNumFound; i++)
                        {
                            dtNumFound.Rows[dtNumFound.Rows.Count - 1].Delete();
                            dtNumFound.AcceptChanges();
                        }
                    }
                    if (numFound > 0 && totalNumFound > 2)
                    {
                        totalResFound += 1;
                        DataRow rowRank = dtRank.NewRow();
                        rowRank["startId"] = (int)dtTargetdt.Rows[cnt - 14]["Id"];
                        if (cnt >= dtTargetdt.Rows.Count)
                        {
                            cnt = dtTargetdt.Rows.Count - 1;
                        }
                        if(cnt - 16 >= 0)
                            rowRank["lastId"] = (int)dtTargetdt.Rows[cnt - 16]["Id"];
                        else if (cnt - 15 >= 0)
                            rowRank["lastId"] = (int)dtTargetdt.Rows[cnt - 15]["Id"];
                        else
                            rowRank["lastId"] = (int)dtTargetdt.Rows[cnt - 14]["Id"];
                        rowRank["endId"] = (int)dtTargetdt.Rows[cnt]["Id"];
                        rowRank["numFound"] = numFound;
                        rowRank["totalNumFound"] = totalNumFound;
                        rowRank["RecNo"] = dtRank.Rows.Count + 1;
                        dtRank.Rows.Add(rowRank);
                        //cnt += 15;
                    }
                    //else
                    cnt++;
                }
                ImportToResultFullCellSearch(dtRank);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ImportToResultFullCellSearch(DataTable dtRank)
        {
            try
            {
                int id = dtSerchRes.Rows.Count;
                dtRank.DefaultView.Sort = "numFound DESC,totalNumFound DESC";
                foreach (DataRowView rowView in dtRank.DefaultView)
                {

                    DataRow[] rows = dtBaseTable.Select("Id > " + ((int)rowView["startId"] - 1).ToString() + " and Id < " + ((int)rowView["lastId"] + 1).ToString());
                    int LastId = (int)rowView["endId"];
                    if (rdBottomToTop.Checked)
                    {
                        rows = dtBaseTable.Select("Id > " + ((int)rowView["lastId"] - 1).ToString() + " and Id < " + ((int)rowView["endId"] + 1).ToString());
                        LastId = (int)rowView["startId"];
                    }
                        DataRow row4 = dtSerchRes.NewRow();
                    id = id + 1;
                    row4["Id"] = id;
                    row4["MatchType"] = "";
                    row4["NumHits"] = "";
                    row4["NosFound"] = rowView["numFound"];
                    row4["tNosFound"] = rowView["totalNumFound"];

                    dtSerchRes.Rows.Add(row4);

                    DataRow row3 = dtSerchRes.NewRow();
                    id = id + 1;
                    row3["Id"] = id;
                    int DBId = Convert.ToInt32(rows[0]["DBId"]);
                    row3["MatchType"] = "Database :";
                    row3["NumHits"] = SqlClass.GetDBNameById(DBId);
                    row3["NosFound"] = rowView["numFound"];
                    row3["tNosFound"] = rowView["totalNumFound"];
                    dtSerchRes.Rows.Add(row3);





                    DataRow row2 = dtSerchRes.NewRow();
                    id = id + 1;
                    row2["Id"] = id;
                    row2["MatchType"] = "Totle Num Found ";
                    row2["NumHits"] = rowView["totalNumFound"].ToString();
                    row2["NosFound"] = rowView["numFound"];
                    row2["tNosFound"] = rowView["totalNumFound"];
                    dtSerchRes.Rows.Add(row2);

                    DataRow row1 = dtSerchRes.NewRow();
                    id = id + 1;
                    row1["Id"] = id;
                    row1["MatchType"] = "Num Found in Forcast Line ";
                    row1["NumHits"] = rowView["numFound"].ToString();
                    row1["NosFound"] = rowView["numFound"];
                    row1["tNosFound"] = rowView["totalNumFound"];
                    dtSerchRes.Rows.Add(row1);

                    for (int j = 0; j < rows.Length; j++)
                    {
                        int i = j;
                        //if (rdBottomToTop.Checked)
                        //    i = rows.Length - 1 - j;

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
                        row["RecordId"] = rows[i]["Id"].ToString();
                        if (rdTopToBottom.Checked)
                        {
                            if ((int)rows[i]["Id"] != LastId && (int)rows[i]["Id"] != LastId - 1)
                            {
                                if (i == rows.Length - 1 || i == rows.Length - 2)
                                    row["RecordId"] = 0;
                                if (i == rows.Length - 2)
                                {
                                    DataRow rowLast = dtLastRows.NewRow();
                                    rowLast["Id"] = id;
                                    rowLast["RecordId"] = rows[i]["Id"].ToString();
                                    rowLast["DBId"] = rows[i]["DBId"].ToString();
                                    rowLast["RecNo"] = rowView["RecNo"];
                                    rowLast["W1"] = rows[i]["W1"].ToString();
                                    rowLast["W2"] = rows[i]["W2"].ToString();
                                    rowLast["W3"] = rows[i]["W3"].ToString();
                                    rowLast["W4"] = rows[i]["W4"].ToString();
                                    rowLast["W5"] = rows[i]["W5"].ToString();
                                    dtLastRows.Rows.Add(rowLast);
                                }
                            }
                        }
                        else
                        {
                            if ((int)rows[i]["Id"] != (int)rowView["endId"] && (int)rows[i]["Id"] != (int)rowView["endId"] + 1)
                            {
                                if (i == 0 || i == 1)
                                    row["RecordId"] = 0;
                                if (i == 1)
                                {
                                    DataRow rowLast = dtLastRows.NewRow();
                                    rowLast["Id"] = id;
                                    rowLast["RecordId"] = rows[i]["Id"].ToString();
                                    rowLast["DBId"] = rows[i]["DBId"].ToString();
                                    rowLast["RecNo"] = rowView["RecNo"];
                                    rowLast["W1"] = rows[i]["W1"].ToString();
                                    rowLast["W2"] = rows[i]["W2"].ToString();
                                    rowLast["W3"] = rows[i]["W3"].ToString();
                                    rowLast["W4"] = rows[i]["W4"].ToString();
                                    rowLast["W5"] = rows[i]["W5"].ToString();
                                    dtLastRows.Rows.Add(rowLast);
                                }
                            }
                        }
                        id = id + 1;
                        row["Id"] = id;
                        row["RecNo"] = rowView["RecNo"];

                        row["NosFound"] = rowView["numFound"];
                        row["tNosFound"] = rowView["totalNumFound"];
                        dtSerchRes.Rows.Add(row);
                    }

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 
        #endregion

        #region General Code Search

        private void GeneralCodeSearch(DataTable dtTargetdt)
        {
            try
            {
                int matchFound = 0;
                int lastMatchFound = 0;
                DataTable dtRank = new DataTable();
                dtRank.Columns.Add("startId", Type.GetType("System.Int32"));
                dtRank.Columns.Add("endId", Type.GetType("System.Int32"));
                dtRank.Columns.Add("lastId", Type.GetType("System.Int32"));
                dtRank.Columns.Add("numFound", Type.GetType("System.Int32"));
                dtRank.Columns.Add("patrnFound", Type.GetType("System.Int32"));
                dtRank.Columns.Add("RecNo", Type.GetType("System.Int32"));
                DataTable dtTemp = dtNumFound.Clone();
                if (rdInternalDB.Checked)
                    progressBar1.Maximum = dtTargetdt.Rows.Count;
                for (int cnt = 0; cnt < dtTargetdt.Rows.Count; cnt++)
                {
                    if (rdInternalDB.Checked)
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
                                        rowNumFound["RecNo"] = dtRank.Rows.Count + 1;
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
                                                        rowNumFound1["RecNo"] = dtRank.Rows.Count + 1;
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
                    if (lastMatchFound > 1)
                    {
                        //DataRow[] rowRanks = dtRank.Select("startId =" + (int)dtTargetdt.Rows[cnt]["Id"]);
                        //if (rowRanks.Length == 0)
                        //{
                        totalResFound += 1;
                        DataRow rowRank = dtRank.NewRow();
                        rowRank["startId"] = (int)dtTargetdt.Rows[cnt]["Id"];
                        if (cnt + 14 >= dtTargetdt.Rows.Count)
                            rowRank["endId"] = (int)dtTargetdt.Rows[dtTargetdt.Rows.Count - 1]["Id"];
                        else
                            rowRank["endId"] = (int)dtTargetdt.Rows[cnt + 14]["Id"];

                        if(cnt + 16 < dtTargetdt.Rows.Count )
                            rowRank["lastId"] = (int)dtTargetdt.Rows[cnt + 16]["Id"];
                        else if (cnt + 15 < dtTargetdt.Rows.Count)
                            rowRank["lastId"] = (int)dtTargetdt.Rows[cnt + 15]["Id"];
                        else
                            rowRank["lastId"] = (int)dtTargetdt.Rows[dtTargetdt.Rows.Count - 1]["Id"];

                        rowRank["numFound"] = lastMatchFound;
                        rowRank["patrnFound"] = 1;
                        rowRank["RecNo"] = dtRank.Rows.Count + 1;
                        dtRank.Rows.Add(rowRank);
                        //}
                        //else
                        //{
                        //    rowRanks[0]["numFound"] = lastMatchFound;
                        //    rowRanks[0]["patrnFound"] = Convert.ToInt32(rowRanks[0]["patrnFound"]) + 1;
                        //}

                    }




                }
                ImportToResultGenCodSearch(dtRank);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

                    DataRow[] rows = dtBaseTable.Select("Id > " + ((int)rowView["startId"] - 1).ToString() + " and Id < " + ((int)rowView["lastId"] + 1).ToString());
                    int LastId = (int)rowView["endId"];
                    if (rdBottomToTop.Checked)
                    {
                        rows = dtBaseTable.Select("Id > " + ((int)rowView["lastId"] - 1).ToString() + " and Id < " + ((int)rowView["startId"] + 1).ToString());
                        LastId = (int)rowView["startId"];
                    }
                        DataRow row4 = dtSerchRes.NewRow();
                    id = id + 1;
                    row4["Id"] = id;
                    row4["MatchType"] = "";
                    row4["NumHits"] = "";
                    dtSerchRes.Rows.Add(row4);



                    DataRow row1 = dtSerchRes.NewRow();
                    id = id + 1;
                    row1["Id"] = id;
                    row1["MatchType"] = "Database :";
                    int DBId = Convert.ToInt32(rows[0]["DBId"]);
                    row1["NumHits"] = SqlClass.GetDBNameById(DBId);
                    dtSerchRes.Rows.Add(row1);



                    DataRow row2 = dtSerchRes.NewRow();
                    id = id + 1;
                    row2["Id"] = id;
                    row2["MatchType"] = "Number Mateches :";
                    row2["NumHits"] = rowView["numFound"].ToString();
                    dtSerchRes.Rows.Add(row2);


                    for (int j = 0; j < rows.Length; j++)
                    {
                        int i = j;
                        //if (rdBottomToTop.Checked)
                        //    i = rows.Length - 1 - j;
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
                        row["RecordId"] = rows[i]["Id"].ToString();
                        if (rdTopToBottom.Checked)
                        {
                            if ((int)rows[i]["Id"] != LastId && (int)rows[i]["Id"] != LastId - 1)
                            {
                                if (i == rows.Length - 1 || i == rows.Length - 2)
                                    row["RecordId"] = 0;
                                if (i == rows.Length - 2)
                                {
                                    DataRow rowLast = dtLastRows.NewRow();
                                    rowLast["Id"] = id;
                                    rowLast["RecordId"] = rows[i]["Id"].ToString();
                                    rowLast["DBId"] = rows[i]["DBId"].ToString();
                                    rowLast["RecNo"] = RecNo;
                                    rowLast["W1"] = rows[i]["W1"].ToString();
                                    rowLast["W2"] = rows[i]["W2"].ToString();
                                    rowLast["W3"] = rows[i]["W3"].ToString();
                                    rowLast["W4"] = rows[i]["W4"].ToString();
                                    rowLast["W5"] = rows[i]["W5"].ToString();
                                    dtLastRows.Rows.Add(rowLast);
                                }
                            }
                        }
                        else
                        {
                            if ((int)rows[i]["Id"] != (int)rowView["endId"] && (int)rows[i]["Id"] != (int)rowView["endId"] + 1)
                            {
                                if (i == 0 || i == 1)
                                    row["RecordId"] = 0;
                                if (i == 1)
                                {
                                    DataRow rowLast = dtLastRows.NewRow();
                                    rowLast["Id"] = id;
                                    rowLast["RecordId"] = rows[i]["Id"].ToString();
                                    rowLast["DBId"] = rows[i]["DBId"].ToString();
                                    rowLast["RecNo"] = RecNo;
                                    rowLast["W1"] = rows[i]["W1"].ToString();
                                    rowLast["W2"] = rows[i]["W2"].ToString();
                                    rowLast["W3"] = rows[i]["W3"].ToString();
                                    rowLast["W4"] = rows[i]["W4"].ToString();
                                    rowLast["W5"] = rows[i]["W5"].ToString();
                                    dtLastRows.Rows.Add(rowLast);
                                }
                            }
                        }
                        id = id + 1;
                        row["Id"] = id;
                        row["RecNo"] = rowView["RecNo"];
                        dtSerchRes.Rows.Add(row);
                    }

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Machine Tail Search
        private void MachineTailSearchMOD(DataTable dtTargetdt)
        {
            try
            {

                int numFound = 0;
                int totalNumFound = 0;
                DataTable dtRank = new DataTable();
                dtRank.Columns.Add("startId", Type.GetType("System.Int32"));
                dtRank.Columns.Add("endId", Type.GetType("System.Int32"));
                dtRank.Columns.Add("lastId", Type.GetType("System.Int32"));
                dtRank.Columns.Add("numFound", Type.GetType("System.Int32"));
                dtRank.Columns.Add("totalNumFound", Type.GetType("System.Int32"));
                DataTable dtSearchTemp = dsSearch.Tables[0].Copy();
                if (rdBottomToTop.Checked)
                    dtSearchTemp = ReversreSearchTable();
                int count = dtSearchTemp.Rows.Count;
                for (int cnt = 0; cnt < count; cnt++)
                {
                    while ((cnt < count) && (dtSearchTemp.Rows[cnt]["W1"] == null || dtSearchTemp.Rows[cnt]["W1"] == DBNull.Value))
                    {
                        dtSearchTemp.Rows[cnt].Delete();
                        dtSearchTemp.AcceptChanges();
                        count = count - 1;
                    }
                }
                if (rdInternalDB.Checked)
                    progressBar1.Maximum = dtTargetdt.Rows.Count;
                for (int cnt = 14; cnt < dtTargetdt.Rows.Count; )
                {
                    if (rdInternalDB.Checked)
                        progressBar1.Value = cnt;
                    numFound = 0;
                    totalNumFound = 0;
                    foreach (DataColumn dc in dtSearchTemp.Columns)
                    {
                        totalNumFound = 0;
                        if (dc.Caption.StartsWith("M"))
                        {
                            //totalNumFound = 0;
                            for (int i = 14; i >= 0; i--)
                            {
                                if (!(dtSearchTemp.Rows[i][dc] == null || dtSearchTemp.Rows[i][dc] == DBNull.Value || dtTargetdt.Rows[cnt - 14 + i][dc.Caption] == DBNull.Value || dtTargetdt.Rows[cnt - 14 + i][dc.Caption] == null))
                                {
                                    if (Convert.ToInt32(dtSearchTemp.Rows[14][dc]) == (int)dtTargetdt.Rows[cnt - 14 + i][dc.Caption])
                                    {
                                        totalNumFound += 1;
                                        DataRow rowNumFound = dtNumFound.NewRow();
                                        rowNumFound["Id"] = (int)dtTargetdt.Rows[cnt - 14 + i]["Id"];
                                        rowNumFound["col"] = dc.Caption;
                                        rowNumFound["Sym"] = "R";
                                        dtNumFound.Rows.Add(rowNumFound);
                                        foreach (DataColumn dc1 in dtSearchTemp.Columns)
                                        {
                                            if (dc1.Caption.StartsWith("M"))
                                            {
                                                for (int j = 0; j < 14; j++)
                                                {
                                                    if (Convert.ToInt32(dtSearchTemp.Rows[13 - j][dc1]) == (int)dtTargetdt.Rows[cnt - 14 + i - 1 - j][dc1.Caption])
                                                    {
                                                        totalNumFound += 1;
                                                        DataRow rowNumFound1 = dtNumFound.NewRow();
                                                        rowNumFound1["Id"] = (int)dtTargetdt.Rows[cnt - 14 + i - 1 - j]["Id"];
                                                        rowNumFound1["col"] = dc1.Caption;
                                                        rowNumFound1["Sym"] = "Y";
                                                        dtNumFound.Rows.Add(rowNumFound1);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //if (cnt == 14)
                                    break;
                                }
                            }
                            if (totalNumFound > 1)
                            {
                                numFound++;
                                dtRank.Rows.Clear();
                                DataRow rowRank = dtRank.NewRow();
                                rowRank["startId"] = (int)dtTargetdt.Rows[cnt - 14]["Id"];
                                rowRank["endId"] = (int)dtTargetdt.Rows[cnt]["Id"];
                                if(cnt + 2 < dtTargetdt.Rows.Count)
                                    rowRank["lastId"] = (int)dtTargetdt.Rows[cnt+2]["Id"];
                                else if(cnt + 1 < dtTargetdt.Rows.Count)
                                    rowRank["lastId"] = (int)dtTargetdt.Rows[cnt+1]["Id"];
                                else
                                    rowRank["lastId"] = (int)dtTargetdt.Rows[cnt]["Id"];
                                rowRank["numFound"] = numFound;
                                rowRank["totalNumFound"] = totalNumFound;
                                dtRank.Rows.Add(rowRank);
                                if (totalNumFound == 71)
                                {
                                    for (int j = 2; j <= 5; j++)
                                    {
                                        DataRow rowNumFound = dtNumFound.NewRow();
                                        rowNumFound["Id"] = (int)dtTargetdt.Rows[cnt]["Id"];
                                        rowNumFound["col"] = "M" + j.ToString();
                                        rowNumFound["Sym"] = "R";
                                        dtNumFound.Rows.Add(rowNumFound);
                                    }
                                    ImportResultToMachineTail(dtRank);
                                    break;
                                }
                                ImportResultToMachineTail(dtRank);
                            }
                            else
                            {
                                if (totalNumFound == 1)
                                {
                                    dtNumFound.Rows[dtNumFound.Rows.Count - 1].Delete();
                                    dtNumFound.AcceptChanges();
                                }
                            }
                        }

                    }

                    //if (numFound == 0)
                    cnt++;
                    //else
                    //cnt = cnt + 15;

                }




            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        private void MachineTailSearchBottomUp(DataTable dtTargetdt)
        {
            try
            {

                int numFound = 0;
                int totalNumFound = 0;
                DataTable dtRank = new DataTable();
                dtRank.Columns.Add("startId", Type.GetType("System.Int32"));
                dtRank.Columns.Add("endId", Type.GetType("System.Int32"));
                dtRank.Columns.Add("lastId", Type.GetType("System.Int32"));
                dtRank.Columns.Add("numFound", Type.GetType("System.Int32"));
                dtRank.Columns.Add("totalNumFound", Type.GetType("System.Int32"));
                DataTable dtSearchTemp = dsSearch.Tables[0].Copy();
                if (rdBottomToTop.Checked)
                    dtSearchTemp = ReversreSearchTable();
                int count = dtSearchTemp.Rows.Count;
                for (int cnt = 0; cnt < count; cnt++)
                {
                    while ((cnt < count) && (dtSearchTemp.Rows[cnt]["W1"] == null || dtSearchTemp.Rows[cnt]["W1"] == DBNull.Value))
                    {
                        dtSearchTemp.Rows[cnt].Delete();
                        dtSearchTemp.AcceptChanges();
                        count = count - 1;
                    }
                }
                if (rdInternalDB.Checked)
                    progressBar1.Maximum = dtTargetdt.Rows.Count;
                for (int cnt = 14; cnt < dtTargetdt.Rows.Count; )
                {
                    if (rdInternalDB.Checked)
                        progressBar1.Value = cnt;
                    numFound = 0;
                    totalNumFound = 0;
                    foreach (DataColumn dc in dtSearchTemp.Columns)
                    {
                        totalNumFound = 0;
                        if (dc.Caption.StartsWith("M"))
                        {
                            //totalNumFound = 0;
                            for (int i = 0; i <= 14 ; i++)
                            {
                                if (!(dtSearchTemp.Rows[i][dc] == null || dtSearchTemp.Rows[i][dc] == DBNull.Value || dtTargetdt.Rows[cnt - 14 + i][dc.Caption] == DBNull.Value || dtTargetdt.Rows[cnt - 14 + i][dc.Caption] == null))
                                {
                                    if (Convert.ToInt32(dtSearchTemp.Rows[0][dc]) == (int)dtTargetdt.Rows[cnt - 14 + i][dc.Caption])
                                    {
                                        totalNumFound += 1;
                                        DataRow rowNumFound = dtNumFound.NewRow();
                                        rowNumFound["Id"] = (int)dtTargetdt.Rows[cnt - 14 + i]["Id"];
                                        rowNumFound["col"] = dc.Caption;
                                        rowNumFound["Sym"] = "R";
                                        dtNumFound.Rows.Add(rowNumFound);
                                        foreach (DataColumn dc1 in dtSearchTemp.Columns)
                                        {
                                            if (dc1.Caption.StartsWith("M"))
                                            {
                                                for (int j = 1; j <= 14; j++)
                                                {
                                                    if (Convert.ToInt32(dtSearchTemp.Rows[j][dc1]) == (int)dtTargetdt.Rows[cnt - 14 + i  + j][dc1.Caption])
                                                    {
                                                        totalNumFound += 1;
                                                        DataRow rowNumFound1 = dtNumFound.NewRow();
                                                        rowNumFound1["Id"] = (int)dtTargetdt.Rows[cnt - 14 + i + j]["Id"];
                                                        rowNumFound1["col"] = dc1.Caption;
                                                        rowNumFound1["Sym"] = "Y";
                                                        dtNumFound.Rows.Add(rowNumFound1);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //if (cnt == 14)
                                    break;
                                }
                            }
                            if (totalNumFound > 1)
                            {
                                numFound++;
                                dtRank.Rows.Clear();
                                DataRow rowRank = dtRank.NewRow();
                                rowRank["startId"] = (int)dtTargetdt.Rows[cnt - 14]["Id"];
                                rowRank["endId"] = (int)dtTargetdt.Rows[cnt]["Id"];
                                if(cnt - 16 >= 0)
                                    rowRank["lastId"] = (int)dtTargetdt.Rows[cnt - 16]["Id"];
                                else if (cnt - 15 >= 0)
                                    rowRank["lastId"] = (int)dtTargetdt.Rows[cnt - 15]["Id"];
                                else
                                    rowRank["lastId"] = (int)dtTargetdt.Rows[cnt - 14]["Id"];
                                rowRank["numFound"] = numFound;
                                rowRank["totalNumFound"] = totalNumFound;
                                dtRank.Rows.Add(rowRank);
                                if (totalNumFound == 71)
                                {
                                    for (int j = 2; j <= 5; j++)
                                    {
                                        DataRow rowNumFound = dtNumFound.NewRow();
                                        rowNumFound["Id"] = (int)dtTargetdt.Rows[cnt-14]["Id"];
                                        rowNumFound["col"] = "M" + j.ToString();
                                        rowNumFound["Sym"] = "R";
                                        dtNumFound.Rows.Add(rowNumFound);
                                    }
                                    ImportResultToMachineTail(dtRank);
                                    break;
                                }
                                ImportResultToMachineTail(dtRank);
                            }
                            else
                            {
                                if (totalNumFound == 1)
                                {
                                    dtNumFound.Rows[dtNumFound.Rows.Count - 1].Delete();
                                    dtNumFound.AcceptChanges();
                                }
                            }
                        }

                    }

                    //if (numFound == 0)
                    cnt++;
                    //else
                    //cnt = cnt + 15;

                }




            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        private void ImportResultToMachineTail(DataTable dtRank)
        {
            try
            {
                int id = dtSerchRes.Rows.Count;


                totalResFound++;
                dtRank.DefaultView.Sort = "numFound DESC,totalNumFound DESC";
                foreach (DataRowView rowView in dtRank.DefaultView)
                {

                    DataRow[] rows = dtBaseTable.Select("Id > " + ((int)rowView["startId"] - 1).ToString() + " and Id < " + ((int)rowView["lastId"] + 1).ToString());
                    int LastId = (int)rowView["endId"];
                    if (rdBottomToTop.Checked)
                    {
                        rows = dtBaseTable.Select("Id > " + ((int)rowView["lastId"] - 1).ToString() + " and Id < " + ((int)rowView["endId"] + 1).ToString());
                        LastId = (int)rowView["startId"];
                    }
                    DataRow row4 = dtSerchRes.NewRow();
                    id = id + 1;
                    row4["Id"] = id;
                    row4["MatchType"] = "";
                    row4["NumHits"] = "";
                    row4["NosFound"] = rowView["numFound"];
                    row4["tNosFound"] = rowView["totalNumFound"];
                    dtSerchRes.Rows.Add(row4);

                    DataRow row3 = dtSerchRes.NewRow();
                    id = id + 1;
                    row3["Id"] = id;
                    int DBId = Convert.ToInt32(rows[0]["DBId"]);
                    row3["MatchType"] = "Database :";
                    row3["NumHits"] = SqlClass.GetDBNameById(DBId);
                    row3["NosFound"] = rowView["numFound"];
                    row3["tNosFound"] = rowView["totalNumFound"];
                    dtSerchRes.Rows.Add(row3);





                    DataRow row2 = dtSerchRes.NewRow();
                    id = id + 1;
                    row2["Id"] = id;
                    row2["MatchType"] = "Totle Num Found ";
                    row2["NumHits"] = rowView["totalNumFound"].ToString();
                    row2["NosFound"] = rowView["numFound"];
                    row2["tNosFound"] = rowView["totalNumFound"];
                    dtSerchRes.Rows.Add(row2);
                    for (int k = 0; k < rows.Length; k++)
                    {
                        int i = k;
                        //if (rdBottomToTop.Checked)
                        //    i = rows.Length - 1 - k;
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
                        row["RecordId"] = rows[i]["Id"].ToString();
                        if (rdTopToBottom.Checked)
                        {
                            if ((int)rows[i]["Id"] != LastId && (int)rows[i]["Id"] != LastId - 1)
                            {
                                if (i == rows.Length - 1 || i == rows.Length - 2)
                                    row["RecordId"] = 0;
                                if (i == rows.Length - 2)
                                {
                                    DataRow rowLast = dtLastRows.NewRow();
                                    rowLast["Id"] = id;
                                    rowLast["RecordId"] = rows[i]["Id"].ToString();
                                    rowLast["DBId"] = rows[i]["DBId"].ToString();
                                    rowLast["RecNo"] = rowView["numFound"];
                                    rowLast["W1"] = rows[i]["W1"].ToString();
                                    rowLast["W2"] = rows[i]["W2"].ToString();
                                    rowLast["W3"] = rows[i]["W3"].ToString();
                                    rowLast["W4"] = rows[i]["W4"].ToString();
                                    rowLast["W5"] = rows[i]["W5"].ToString();
                                    dtLastRows.Rows.Add(rowLast);
                                }
                            }
                        }
                        else
                        {
                            if ((int)rows[i]["Id"] != (int)rowView["endId"] && (int)rows[i]["Id"] != (int)rowView["endId"] + 1)
                            {
                                if (i == 0 || i == 1)
                                    row["RecordId"] = 0;
                                if (i == 1)
                                {
                                    DataRow rowLast = dtLastRows.NewRow();
                                    rowLast["Id"] = id;
                                    rowLast["RecordId"] = rows[i]["Id"].ToString();
                                    rowLast["DBId"] = rows[i]["DBId"].ToString();
                                    rowLast["RecNo"] = rowView["numFound"];
                                    rowLast["W1"] = rows[i]["W1"].ToString();
                                    rowLast["W2"] = rows[i]["W2"].ToString();
                                    rowLast["W3"] = rows[i]["W3"].ToString();
                                    rowLast["W4"] = rows[i]["W4"].ToString();
                                    rowLast["W5"] = rows[i]["W5"].ToString();
                                    dtLastRows.Rows.Add(rowLast);
                                }
                            }
                        }
                        id = id + 1;
                        row["Id"] = id;
                        DataRow[] rowsdtNumFound = dtNumFound.Select("Id =" + rows[i]["Id"].ToString());
                        if (rowsdtNumFound.Length > 0)
                        {
                            for (int j = 0; j < rowsdtNumFound.Length; j++)
                            {
                                if (rowsdtNumFound[j]["RecNo"] == null || rowsdtNumFound[j]["RecNo"] == DBNull.Value)
                                    rowsdtNumFound[j]["RecNo"] = id;

                            }
                        }
                        row["NosFound"] = rowView["numFound"];
                        row["tNosFound"] = rowView["totalNumFound"];
                        dtSerchRes.Rows.Add(row);


                    }

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion



        private  void PreSettingSearch()
        {
            try
            {
                progressBar1.Value = 0;
                dtNumFound.Rows.Clear();
                totalResFound = 0;

                dsSearch.GetChanges();
                dtSummary.Rows.Clear();
                dtLastRows.Rows.Clear();
               
                dtSearchNum = SqlClass.GetSearchNum().Clone();
                if (rdCodeSearch.Checked)
                {
                    if (arSymboles == null)
                    {
                        arSymboles = new ArrayList();
                    }
                    arSymboles.Clear();

                    if(!dtSearchNum.Columns.Contains("Sym"))
                     dtSearchNum.Columns.Add("Sym");
                    if (!dtNumFound.Columns.Contains("Sym"))
                     dtNumFound.Columns.Add("Sym");
                }
                if (rdMachineTailSearch.Checked)
                {
                    if (!dtNumFound.Columns.Contains("Sym"))
                        dtNumFound.Columns.Add("Sym");
                    if (!dtNumFound.Columns.Contains("RecNo"))
                        dtNumFound.Columns.Add("RecNo");
                }
                int Sym = 0;
                DataTable dtTemp = dsSearch.Tables[0].Copy();
                if (rdBottomToTop.Checked && (rdFullCellSearch.Checked || rdMachineTailSearch.Checked))
                    dtTemp = ReversreSearchTable();

                foreach (DataRow row in dtTemp.Rows)
                {
                    foreach (DataColumn col in dtTemp.Columns)
                    {
                        if (col.Caption != "Id" && col.Caption != "Date" && col.Caption != "SUM_W" && col.Caption != "SUM_M")
                            if (!(row[col] == DBNull.Value || row[col] == null))
                            {
                                DataRow rowSerNum = dtSearchNum.NewRow();
                                rowSerNum["Row"] = row["Id"];
                                //if (col.Caption.Contains("W"))
                                rowSerNum["W"] = col.Caption;
                                if (col.Caption.Contains("M"))
                                    rowSerNum["M"] = col.Caption;
                                rowSerNum["dValue"] = row[col].ToString().Trim();
                                if (rdCodeSearch.Checked)
                                {
                                    rowSerNum["Sym"] = row[col].ToString().Trim().Split('N')[1];
                                    bool flag = true;
                                    for (int i = 0; i < arSymboles.Count ; i++)
                                    {
                                        if (arSymboles[i].ToString() == row[col].ToString().Trim().Split('N')[1])
                                        {
                                            flag = false;
                                        }
                                    }
                                    if (flag)
                                    {
                                        arSymboles.Add(row[col].ToString().Trim().Split('N')[1]);
                                        Sym++;
                                    }
                                    //rowSerNum["Sym"] = Sym;
                                }

                                dtSearchNum.Rows.Add(rowSerNum);
                            }
                    }
                }
                //SqlClass.SaveSearchNum(dtSearchNum);
                //SqlClass.UpdateSearchPlaneData(ref dsSearch);
                if (dtSearchNum.Rows.Count == 0)
                    return;
                DataSet ds = new DataSet();
                SqlClass.GetWin_Machin_DataByDBId(0,ref ds);
                dtSerchRes = ds.Tables[0].Clone();

                dtSerchRes.Columns.Add("RecNo",Type.GetType("System.Int32"));
                dtSerchRes.Columns.Add("NumHits");
                dtSerchRes.Columns.Add("MatchType");
                dtSerchRes.Columns.Add("RecordId");
                dtSerchRes.Columns.Add("NosFound",Type.GetType("System.Int32"));
                dtSerchRes.Columns.Add("tNosFound", Type.GetType("System.Int32"));
                int DBId = 0;
                if (rdInternalDB.Checked)
                {
                    DataTable dtInter = SqlClass.GetInternalDatabase();
                    
                    if (dtInter.Rows.Count > 0)
                    {
                        DBId = Convert.ToInt32(dtInter.Rows[0]["DBId"]);
                    }
                    MakeSearch(DBId);
                }
                else
                {
                    DataTable dt = SqlClass.GetExternalDatabaseList();
                    string DBIdList = "";
                    for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
                    {
                        progressBar1.Value += 100 / checkedListBox1.CheckedItems.Count;
                        DataRow[] rows = dt.Select("DBName = '" + checkedListBox1.CheckedItems[i].ToString() + "'");
                        DBIdList += "," + rows[0]["DBId"].ToString();
                        DBId = Convert.ToInt32(rows[0]["DBId"]);
                        MakeSearch(DBId);
                    }
                    progressBar1.Value = 100;
                    if (DBIdList != "")
                    {
                        DBIdList = DBIdList.Substring(1, DBIdList.Length - 1);
                       
                    }
                }

                timer1.Enabled = true;
                PrepareSummary();
                Date1.DefaultCellStyle.Format = "dd/MM/yyyy";
                dtSerchRes.DefaultView.Sort = "NosFound desc,tNosFound desc";
                ResultGrid.DataSource = dtSerchRes.DefaultView;
                ResultGrid.Columns["DBId"].Visible = false;
                ResultGrid.Columns["NosFound"].Visible = false;
                ResultGrid.Columns["RecNo"].Visible = false;
                lblResultFound.Text = "Total Result Found : " + totalResFound.ToString();

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private DataTable ReversreSearchTable()
        {
            DataTable dtreturnTable = dsSearch.Tables[0].Copy();
            try
            {
               
                int count = dtreturnTable.Rows.Count;
                for (int i = 1; i <= count / 2; i++)
                {
                    foreach (DataColumn dc in dtreturnTable.Columns)
                    {
                        if (dc.Caption != "Id")
                        {
                            object obj = dtreturnTable.Rows[i - 1][dc];
                            dtreturnTable.Rows[i - 1][dc] = dtreturnTable.Rows[count - i][dc];
                            dtreturnTable.Rows[count - i][dc] = obj;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dtreturnTable;
        }
       
        private void ReversreSearchTable(ref DataTable dtreturnTable)
        {
            //dtreturnTable = dsSearch.Tables[0].Copy();
            try
            {

                int count = dtreturnTable.Rows.Count;
                for (int i = 1; i <= count / 2; i++)
                {
                    foreach (DataColumn dc in dtreturnTable.Columns)
                    {
                        //if (dc.Caption != "Id")
                        {
                            object obj = dtreturnTable.Rows[i - 1][dc];
                            dtreturnTable.Rows[i - 1][dc] = dtreturnTable.Rows[count - i][dc];
                            dtreturnTable.Rows[count - i][dc] = obj;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //return dtreturnTable;
        }

        private void MakeSearch(int DBId)
        {
            DataSet ds = new DataSet();

            SqlClass.GetWin_Machin_DataByDBId(DBId, ref ds);
            dtBaseTable = ds.Tables[0].Copy();
            

            DataTable dtTargetdt = dtBaseTable.Copy();
            if (rdBottomToTop.Checked)
            {   if(rdExectSearch12.Checked || rdCodeSearch.Checked)
                ReversreSearchTable(ref dtTargetdt);
            }
            int count = dtBaseTable.Rows.Count;
            for (int cnt = 0; cnt < count; cnt++)
            {
                while ((cnt < count) && (dtTargetdt.Rows[cnt]["W1"] == null || dtTargetdt.Rows[cnt]["W1"] == DBNull.Value))
                {
                    dtTargetdt.Rows[cnt].Delete();
                    dtTargetdt.AcceptChanges();
                    count = count - 1;
                }
            }
            if (rdExectSearch12.Checked)
                ExectSearch(dtTargetdt);
            else if (rdFullCellSearch.Checked)
            {
                if (rdTopToBottom.Checked)
                FullCellSearchMOD(dtTargetdt);
                else
                    FullCellSearchBottomUp(dtTargetdt);
            }
            //FullCellSearch(dtTargetdt);
            else if (rdCodeSearch.Checked)
                //ExectSearchM(dtTargetdt);
                GeneralCodeSearch(dtTargetdt);
            else
            {
                if (rdTopToBottom.Checked)
                    MachineTailSearchMOD(dtTargetdt);
                else
                    MachineTailSearchBottomUp(dtTargetdt);
            }
                //MachineTailSearch(dtTargetdt);
            
        }

        private void fillExternalCheckListBox()
        {
            try
            {
                DataTable dt = SqlClass.GetExternalDatabaseList();
                checkedListBox1.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    checkedListBox1.Items.Add(row["DBName"].ToString(),true);
               
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void searchGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            searchGrid.Update();
        }

        private void searchGrid_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (rdExectSearch12.Checked || rdFullCellSearch.Checked || rdMachineTailSearch.Checked)
                {
                    if (searchGrid.Columns[e.ColumnIndex].Name != "Date" && searchGrid.Columns[e.ColumnIndex].Name != "Sum_W" && searchGrid.Columns[e.ColumnIndex].Name != "Sum_M")
                
                    if (searchGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && searchGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != DBNull.Value)
                        if (Convert.ToInt32(searchGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) > 90)
                        {
                            MessageBox.Show("Invalid Value");
                            searchGrid.CancelEdit();
                        }
                        else
                        {

                            searchGrid.Update();
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Number");
                searchGrid.CancelEdit();
            }

        }

        public void FillSearchGrid()
        {
            try
            {
                
                for (int i = 0; i < dtFullcellSearch.Rows.Count; i++)
                {
                    dsSearch.Tables[0].Rows[i]["Date"] = dtFullcellSearch.Rows[i]["Date"];
                    dsSearch.Tables[0].Rows[i]["W1"] = dtFullcellSearch.Rows[i]["W1"];
                    dsSearch.Tables[0].Rows[i]["W2"] = dtFullcellSearch.Rows[i]["W2"];
                    dsSearch.Tables[0].Rows[i]["W3"] = dtFullcellSearch.Rows[i]["W3"];
                    dsSearch.Tables[0].Rows[i]["W4"] = dtFullcellSearch.Rows[i]["W4"];
                    dsSearch.Tables[0].Rows[i]["W5"] = dtFullcellSearch.Rows[i]["W5"];
                    dsSearch.Tables[0].Rows[i]["SUM_W"] = dtFullcellSearch.Rows[i]["SUM_W"];
                    dsSearch.Tables[0].Rows[i]["M1"] = dtFullcellSearch.Rows[i]["M1"];
                    dsSearch.Tables[0].Rows[i]["M2"] = dtFullcellSearch.Rows[i]["M2"];
                    dsSearch.Tables[0].Rows[i]["M3"] = dtFullcellSearch.Rows[i]["M3"];
                    dsSearch.Tables[0].Rows[i]["M4"] = dtFullcellSearch.Rows[i]["M4"];
                    dsSearch.Tables[0].Rows[i]["M5"] = dtFullcellSearch.Rows[i]["M5"];
                    dsSearch.Tables[0].Rows[i]["SUM_M"] = dtFullcellSearch.Rows[i]["SUM_M"];
                }
                rdFullCellSearch.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ClearSearchPanel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static void ClearSearchPanel()
        {
            try
            {
                dsSearch.Tables[0].Rows.Clear();
                SqlClass.GetSearchPlaneData(ref dsSearch);

                //for (int i = 0; i < dsSearch.Tables[0].Rows.Count; i++)
                //{
                //    dsSearch.Tables[0].Rows[i]["Date"] = DBNull.Value;
                //    dsSearch.Tables[0].Rows[i]["W1"] = DBNull.Value;
                //    dsSearch.Tables[0].Rows[i]["W2"] = DBNull.Value;
                //    dsSearch.Tables[0].Rows[i]["W3"] = DBNull.Value;
                //    dsSearch.Tables[0].Rows[i]["W4"] = DBNull.Value;
                //    dsSearch.Tables[0].Rows[i]["W5"] = DBNull.Value;
                //    dsSearch.Tables[0].Rows[i]["SUM_W"] = DBNull.Value;
                //    dsSearch.Tables[0].Rows[i]["M1"] = DBNull.Value;
                //    dsSearch.Tables[0].Rows[i]["M2"] = DBNull.Value;
                //    dsSearch.Tables[0].Rows[i]["M3"] = DBNull.Value;
                //    dsSearch.Tables[0].Rows[i]["M4"] = DBNull.Value;
                //    dsSearch.Tables[0].Rows[i]["M5"] = DBNull.Value;
                //    dsSearch.Tables[0].Rows[i]["SUM_M"] = DBNull.Value;
                //}
                dsSearch.GetChanges();
                //SqlClass.UpdateSearchPlaneData(ref dsSearch);
                if(dtSerchRes != null)
                dtSerchRes.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rdCodeSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (rdCodeSearch.Checked)
            {
                ClearSearchPanel();
                searchGrid.Visible = false;
                dataGridView1.Visible = true;
                dataGridView1.Top = searchGrid.Top;
                dataGridView1.Left = searchGrid.Left - 20 ;
                dataGridView1.DataSource = dsSearch.Tables[0];
            }
            else
            {
                ClearSearchPanel();
                searchGrid.Visible = true;
                dataGridView1.Visible = false;
            }
        }

        private int GetTurning(int RN)
        {
            int TN=RN;
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

        private bool checkCounter(int RN,int CN)
        {
            if(CN == GetCounter(RN))
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
                        else if(strNo2 == "TN")
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

        private void ResultGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                    if (dtNumFound.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtNumFound.Rows)
                        {
                            if (ResultGrid.Columns[e.ColumnIndex].DataPropertyName == row["col"].ToString() && ResultGrid.Rows[e.RowIndex].Cells["RecordId"].Value != null && ResultGrid.Rows[e.RowIndex].Cells["RecordId"].Value != DBNull.Value)
                                if (Convert.ToInt32(ResultGrid.Rows[e.RowIndex].Cells["RecordId"].Value) == Convert.ToInt32(row["Id"]))
                                { 
                                    if(rdMachineTailSearch.Checked)
                                        {


                                            if (Convert.ToInt32(ResultGrid.Rows[e.RowIndex].Cells["Id"].Value) == Convert.ToInt32(row["RecNo"]))
                                            {
                                                if (row["Sym"].ToString() == "R")
                                                    e.CellStyle.BackColor = Color.Red;
                                                else
                                                    e.CellStyle.BackColor = Color.Yellow;
                                            }
                                        }
                                    else
                                    {
                                    if (Convert.ToInt32(ResultGrid.Rows[e.RowIndex].Cells["RecNo"].Value) == Convert.ToInt32(row["RecNo"]))
                                    {
                                        if (rdFullCellSearch.Checked || rdExectSearch12.Checked)
                                            e.CellStyle.BackColor = Color.Red;
                                        else if (rdCodeSearch.Checked)
                                        {

                                            int i = Convert.ToInt32(row["Sym"]);
                                            if (i == 0)
                                                e.CellStyle.BackColor = Color.Red;
                                            else if (i == 1)
                                                e.CellStyle.BackColor = Color.Blue;
                                            else if (i == 2)
                                                e.CellStyle.BackColor = Color.Green;
                                            else if (i == 3)
                                                e.CellStyle.BackColor = Color.Yellow;
                                            else if (i == 4)
                                                e.CellStyle.BackColor = Color.Brown;
                                            else if (i == 5)
                                                e.CellStyle.BackColor = Color.Pink;
                                            else if (i == 6)
                                                e.CellStyle.BackColor = Color.Violet;
                                            else if (i == 7)
                                                e.CellStyle.BackColor = Color.SteelBlue;
                                            else if (i == 8)
                                                e.CellStyle.BackColor = Color.SpringGreen;
                                            else if (i == 9)
                                                e.CellStyle.BackColor = Color.YellowGreen;
                                            else
                                                e.CellStyle.BackColor = Color.Red;

                                        }
                                        else
                                        {
                                        }
                                        
                                    }
                                }

                                }
                        }
                        if (Convert.ToInt32(ResultGrid.Rows[e.RowIndex].Cells["RecordId"].Value) == 0)
                        {
                            e.CellStyle.BackColor = Color.Aqua;
                        }
                    }
            }
            catch (Exception ex)
            {
            }
        }

        private void rdMachineTailSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (rdMachineTailSearch.Checked)
            {
               // rdInternalDB.Checked = true;
                GetlastRowsMod();
            }
        }

        private static void GetlastRows()
        {
            try
            {

                if (dsSearch.Tables[0].Rows[0]["W1"] == null || dsSearch.Tables[0].Rows[0]["W2"] == DBNull.Value)
                {
                    DataTable dtInter = SqlClass.GetInternalDatabase();
                    int DBId;
                    if (dtInter.Rows.Count > 0)
                    {
                        DBId = Convert.ToInt32(dtInter.Rows[0]["DBId"]);
                        DataSet ds = new DataSet();

                        SqlClass.GetWin_Machin_DataByDBId(DBId, ref ds);

                        int rowCount = ds.Tables[0].Rows.Count;
                       
                        for (int i = 15; i > 0; i--)
                        {
                            dsSearch.Tables[0].Rows[15 - i]["Date"] = ds.Tables[0].Rows[rowCount - i]["Date"];
                            dsSearch.Tables[0].Rows[15 - i]["W1"] = ds.Tables[0].Rows[rowCount - i]["W1"];
                            dsSearch.Tables[0].Rows[15 - i]["W2"] = ds.Tables[0].Rows[rowCount - i]["W2"];
                            dsSearch.Tables[0].Rows[15 - i]["W3"] = ds.Tables[0].Rows[rowCount - i]["W3"];
                            dsSearch.Tables[0].Rows[15 - i]["W4"] = ds.Tables[0].Rows[rowCount - i]["W4"];
                            dsSearch.Tables[0].Rows[15 - i]["W5"] = ds.Tables[0].Rows[rowCount - i]["W5"];
                            dsSearch.Tables[0].Rows[15 - i]["SUM_W"] = ds.Tables[0].Rows[rowCount - i]["SUM_W"];
                            dsSearch.Tables[0].Rows[15 - i]["M1"] = ds.Tables[0].Rows[rowCount - i]["M1"];
                            dsSearch.Tables[0].Rows[15 - i]["M2"] = ds.Tables[0].Rows[rowCount - i]["M2"];
                            dsSearch.Tables[0].Rows[15 - i]["M3"] = ds.Tables[0].Rows[rowCount - i]["M3"];
                            dsSearch.Tables[0].Rows[15 - i]["M4"] = ds.Tables[0].Rows[rowCount - i]["M4"];
                            dsSearch.Tables[0].Rows[15 - i]["M5"] = ds.Tables[0].Rows[rowCount - i]["M5"];
                            dsSearch.Tables[0].Rows[15 - i]["SUM_M"] = ds.Tables[0].Rows[rowCount - i]["SUM_M"];
                        }


                    }
                }//
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private static void GetlastRowsMod()
        {
            try
            {

                if (dsSearch.Tables[0].Rows[0]["W1"] == null || dsSearch.Tables[0].Rows[0]["W2"] == DBNull.Value)
                {
                    DataTable dtInter = SqlClass.GetInternalDatabase();
                    int DBId;
                    if (dtInter.Rows.Count > 0)
                    {
                        DBId = Convert.ToInt32(dtInter.Rows[0]["DBId"]);
                        DataSet ds = new DataSet();

                        SqlClass.GetWin_Machin_DataByDBId(DBId, ref ds);

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
                        dsSearch.Tables[0].Rows.Clear();
                        for (int i = j; i > 0; i--)
                        {
                            DataRow rowSerch = dsSearch.Tables[0].NewRow();
                            rowSerch["Id"] = k;
                            rowSerch["Date"] = ds.Tables[0].Rows[rowCount - i]["Date"];
                            rowSerch["W1"] = ds.Tables[0].Rows[rowCount - i]["W1"];
                            rowSerch["W2"] = ds.Tables[0].Rows[rowCount - i]["W2"];
                            rowSerch["W3"] = ds.Tables[0].Rows[rowCount - i]["W3"];
                            rowSerch["W4"] = ds.Tables[0].Rows[rowCount - i]["W4"];
                            rowSerch["W5"] = ds.Tables[0].Rows[rowCount - i]["W5"];
                            rowSerch["SUM_W"] = ds.Tables[0].Rows[rowCount - i]["SUM_W"];
                            rowSerch["M1"] = ds.Tables[0].Rows[rowCount - i]["M1"];
                            rowSerch["M2"] = ds.Tables[0].Rows[rowCount - i]["M2"];
                            rowSerch["M3"] = ds.Tables[0].Rows[rowCount - i]["M3"];
                            rowSerch["M4"] = ds.Tables[0].Rows[rowCount - i]["M4"];
                            rowSerch["M5"] = ds.Tables[0].Rows[rowCount - i]["M5"];
                            rowSerch["SUM_M"] = ds.Tables[0].Rows[rowCount - i]["SUM_M"];
                            k++;
                            dsSearch.Tables[0].Rows.Add(rowSerch);
                        }


                    }
                }//
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void rdFullCellSearch_CheckedChanged(object sender, EventArgs e)
        {
            //GetlastRows();
            if(rdFullCellSearch.Checked)
            GetlastRowsMod();
        }

        private void CountingWeekSearch(DataTable dtTargetdt)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ResultGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            panelSearchResult.Visible = false;
            progressBar1.Value = 0;
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private int ConvertCodeToNumber(string Code)
        {
            try
            {
                foreach (DataRow row in dtCodeTable.Rows)
                {
                    foreach (DataColumn dc in dtCodeTable.Columns)
                    {
                        if (row[dc] != DBNull.Value)
                        if (row[dc].ToString().Trim() == Code)
                        {
                            return Convert.ToInt32(row["Number"]);
                        }
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        private void rdExectSearch12_CheckedChanged(object sender, EventArgs e)
        {
            if(rdExectSearch12.Checked)
             ClearSearchPanel();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelSearchResult.Visible = true;
            panelSearchResult.BringToFront();
            progressBar1.Value = 0;
            progressBar1.Maximum = 100;
            timer1.Enabled = false;
        }

        private void btnExportToExcell_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = dtSummary.Copy();
                ds.Tables.Add(dt);
                ExportToExcel(ds);
                //WorkbookEngine.CreateWorkbook(ds);
                  
            }
            catch (Exception ex)
            {
                MessageBox.Show("Export Fail");
            }
        }

        private void ExportToExcel(DataSet ds)
        {
            string file = Application.StartupPath + "\\Report.xls";
            if (System.IO.File.Exists(file))
                System.IO.File.Delete(file);

            System.IO.FileStream fs = new FileStream(file, FileMode.Create);
            fs.Close();
            System.IO.StreamWriter sw = new StreamWriter(file);
            StringBuilder sb = new StringBuilder();
            foreach(DataRow row in ds.Tables[0].Rows)
            {
                if (Convert.ToInt32(row["cnt"]) > 1)
                {
                    foreach (DataColumn dc in ds.Tables[0].Columns)
                    {
                        if (dc.Caption == "Description" || dc.Caption == "Number")
                            sb.Append(row[dc].ToString() + "    ");
                    }
                    sb.AppendLine();
                }
            }
           sw.Write(sb);
           sw.Close();
           sw.Dispose();
           MessageBox.Show("Export Successfully");
        }

        private void panelSearchResult_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblInternalDatabaseName_Click(object sender, EventArgs e)
        {

        }
        
    }
    public class WorkbookEngine
    {
        public static void CreateWorkbook(DataSet ds)
        {
            XmlDataDocument xmlDataDoc = new XmlDataDocument(ds);
            XslTransform xt = new XslTransform();
            string file = Application.StartupPath + "\\Excel1.xls";
            StreamReader reader = new StreamReader(file);
            //StreamReader reader = new StreamReader("c:\\Report.xls");
            //StreamReader reader = new StreamReader(typeof(WorkbookEngine).Assembly.GetManifestResourceStream(typeof(WorkbookEngine), file));
            XmlTextReader xRdr = new XmlTextReader(reader);
            xt.Load(xRdr, null, null);
            StringWriter sw = new StringWriter();
            xt.Transform(xmlDataDoc, null, sw, null);
            StreamWriter myWriter = new StreamWriter(file);
            myWriter.Write(sw.ToString());
            myWriter.Close();

        }
    }
}
