using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Data.OleDb;

namespace SystamaticDBSearch
{
    public partial class NumberHistoryForm : Form
    {
        public NumberHistoryForm()
        {
            InitializeComponent();
        }

        private static DataTable dtSearch;
        private static DataSet ds;
        private static DataTable dtNumFound;
        private static DataTable dtSummary;
        private static bool flag = false;
        private static DataTable dtCountingWeek;
        private static DataTable dtCodeTable;
        private void NumberHistoryForm_Load(object sender, EventArgs e)
        {
            flag = false;
            LoadDataSet();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            flag = true;
            //prepareCountingWeekTable();
            fillSerchingNumbers();
            NumHistorySearch();
            LoadDataSet();
        }
        private void LoadDataSet()
        {
            try
            {
                if ((SqlClass.GetInternalDatabase()).Rows.Count > 0)
                {
                    lblInternalDatabaseName.Text = (SqlClass.GetInternalDatabase()).Rows[0]["DBName"].ToString();
                    int DBId = Convert.ToInt32((SqlClass.GetInternalDatabase()).Rows[0]["DBId"]);
                    ds = new DataSet();
                    SqlClass.GetWin_Machin_DataByDBId(DBId, ref ds);
                    
                    Date.DefaultCellStyle.Format = "dd/MM/yyyy";
                    searchGrid.DataSource = ds.Tables[0];
                    searchGrid.Columns["Id"].Visible = false;
                    searchGrid.Columns["DBId"].Visible = false;
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void fillSerchingNumbers()
        {
            try
            {
                int f1 = 0, f2 = 0, f3 = 0, f4 = 0;
                
                if (txtFirstNum.Text != "")
                    f1 = Convert.ToInt32(txtFirstNum.Text);

                if ( txtSecondNumber.Text != "")
                    f2 = Convert.ToInt32(txtSecondNumber.Text);

                if (txtThirdNumber.Text != "")
                    f3 = Convert.ToInt32(txtThirdNumber.Text);

                if (txtForthNum.Text != "")
                    f4 = Convert.ToInt32(txtForthNum.Text);


                dtSearch = new DataTable();
                dtSearch.Columns.Add("N1");
                dtSearch.Columns.Add("N2");
                dtSearch.Columns.Add("N3");
                dtSearch.Columns.Add("N4");
                dtSearch.Columns.Add("Id",Type.GetType("System.Int32"));
                if (f1 != 0)
                {
                    DataRow row1 = dtSearch.NewRow();
                    row1["N1"] = f1;
                    row1["N2"] = 0;
                    row1["N3"] = 0;
                    row1["N4"] = 0;
                    row1["Id"] = dtSearch.Rows.Count + 1;
                    dtSearch.Rows.Add(row1);
                }
                if (f2 != 0)
                {
                    DataRow row2 = dtSearch.NewRow();
                    row2["N1"] = 0;
                    row2["N2"] = f2;
                    row2["N3"] = 0;
                    row2["N4"] = 0;
                    row2["Id"] = dtSearch.Rows.Count + 1;
                    dtSearch.Rows.Add(row2);
                }

                if (f3 != 0)
                {
                    DataRow row3 = dtSearch.NewRow();
                    row3["N1"] = 0;
                    row3["N2"] = 0;
                    row3["N3"] =f3;
                    row3["N4"] = 0;
                    row3["Id"] = dtSearch.Rows.Count + 1;
                    dtSearch.Rows.Add(row3);
                }

                if (f4 != 0)
                {
                    DataRow row4 = dtSearch.NewRow();
                    row4["N1"] = 0;
                    row4["N2"] = 0;
                    row4["N3"] = 0;
                    row4["N4"] = f4;
                    row4["Id"] = dtSearch.Rows.Count + 1;
                    dtSearch.Rows.Add(row4);
                }

                if (Pair1.CheckedItems.Count > 1)
                {
                    DataRow row = dtSearch.NewRow();
                    row["N1"] = 0;
                    row["N2"] = 0;
                    row["N3"] = 0;
                    row["N4"] = 0;
                    for (int i = 0; i < Pair1.CheckedItems.Count ; i++)
                    {
                        if(Pair1.CheckedItems[i].ToString() == "1")
                            row["N1"] = f1;
                        else if (Pair1.CheckedItems[i].ToString() == "2")
                            row["N2"] = f2;
                        else if (Pair1.CheckedItems[i].ToString() == "3")
                            row["N3"] = f3;
                        else
                            row["N4"] = f4;
                    }
                    bool flag = true;
                    for (int i1 = 0; i1 <= dtSearch.Rows.Count - 1; i1++)
                    {
                        if ((Convert.ToInt32(dtSearch.Rows[i1]["N1"]) == Convert.ToInt32(row["N1"]) && Convert.ToInt32(dtSearch.Rows[i1]["N2"]) == Convert.ToInt32(row["N2"]) && Convert.ToInt32(dtSearch.Rows[i1]["N3"]) == Convert.ToInt32(row["N3"]) && Convert.ToInt32(dtSearch.Rows[i1]["N4"]) == Convert.ToInt32(row["N4"])))
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        row["Id"] = dtSearch.Rows.Count + 1;
                        dtSearch.Rows.Add(row);
                    }
                    
                }
                if (Pair2.CheckedItems.Count > 1)
                {
                    DataRow row = dtSearch.NewRow();
                    row["N1"] = 0;
                    row["N2"] = 0;
                    row["N3"] = 0;
                    row["N4"] = 0;
                    for (int i = 0; i < Pair2.CheckedItems.Count ; i++)
                    {
                        if (Pair2.CheckedItems[i].ToString() == "1")
                            row["N1"] = f1;
                        else if (Pair2.CheckedItems[i].ToString() == "2")
                            row["N2"] = f2;
                        else if (Pair2.CheckedItems[i].ToString() == "3")
                            row["N3"] = f3;
                        else
                            row["N4"] = f4;
                    }
                    bool flag = true;
                    for (int i1 = 0; i1 <= dtSearch.Rows.Count - 1; i1++)
                    {
                        if ((Convert.ToInt32(dtSearch.Rows[i1]["N1"]) == Convert.ToInt32(row["N1"]) && Convert.ToInt32(dtSearch.Rows[i1]["N2"]) == Convert.ToInt32(row["N2"]) && Convert.ToInt32(dtSearch.Rows[i1]["N3"]) == Convert.ToInt32(row["N3"]) && Convert.ToInt32(dtSearch.Rows[i1]["N4"]) == Convert.ToInt32(row["N4"])))
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        row["Id"] = dtSearch.Rows.Count + 1;
                        dtSearch.Rows.Add(row);
                    }
                }
                if (Pair3.CheckedItems.Count > 1)
                {
                    DataRow row = dtSearch.NewRow();
                    row["N1"] = 0;
                    row["N2"] = 0;
                    row["N3"] = 0;
                    row["N4"] = 0;
                    for (int i = 0; i < Pair3.CheckedItems.Count; i++)
                    {
                        if (Pair3.CheckedItems[i].ToString() == "1")
                            row["N1"] = f1;
                        else if (Pair3.CheckedItems[i].ToString() == "2")
                            row["N2"] = f2;
                        else if (Pair3.CheckedItems[i].ToString() == "3")
                            row["N3"] = f3;
                        else
                            row["N4"] = f4;
                    }
                    bool flag = true;
                    for (int i1 = 0; i1 <= dtSearch.Rows.Count - 1; i1++)
                    {
                        if ((Convert.ToInt32(dtSearch.Rows[i1]["N1"]) == Convert.ToInt32(row["N1"]) && Convert.ToInt32(dtSearch.Rows[i1]["N2"]) == Convert.ToInt32(row["N2"]) && Convert.ToInt32(dtSearch.Rows[i1]["N3"]) == Convert.ToInt32(row["N3"]) && Convert.ToInt32(dtSearch.Rows[i1]["N4"]) == Convert.ToInt32(row["N4"])))
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        row["Id"] = dtSearch.Rows.Count + 1;
                        dtSearch.Rows.Add(row);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void NumHistorySearch()
        {
            try
            {
                dtNumFound = new DataTable();
                dtNumFound.Columns.Add("Id",Type.GetType("System.Int32"));
                dtNumFound.Columns.Add("SNo", Type.GetType("System.Int32"));
                dtNumFound.Columns.Add("Col");

                dtSummary = new DataTable();
                dtSummary.Columns.Add("SearchNumbers");
                dtSummary.Columns.Add("Events");
                dtSummary.Columns.Add("WinningMachine");

                for (int i = 0; i < dtSearch.Rows.Count; i++)
                {
                    ArrayList oArrayList = new ArrayList();
                    foreach (DataColumn dc in dtSearch.Columns)
                    {
                        if (!(dc.Caption == "Id"))
                        {
                            if (Convert.ToInt32(dtSearch.Rows[i][dc]) != 0)
                            {
                                oArrayList.Add(dtSearch.Rows[i][dc]);
                            }

                        }
                    }
                    ArrayList arrEventW = new ArrayList();
                    ArrayList arrEventM = new ArrayList();
                    string colName = "W";
                        if (rdMachineNumbersOnly.Checked)
                            colName = "M";
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        
                        
                        ArrayList colList = new ArrayList();
                        int cnt = 0;
                        foreach (DataColumn dc1 in ds.Tables[0].Columns)
                        {
                            if (dc1.Caption.StartsWith(colName))
                            {
                                for (int k = 0; k < oArrayList.Count; k++)
                                {
                                    if (ds.Tables[0].Rows[j][dc1] != null && ds.Tables[0].Rows[j][dc1] != DBNull.Value)
                                        if (Convert.ToInt32(oArrayList[k]) == Convert.ToInt32(ds.Tables[0].Rows[j][dc1]))
                                    {
                                        colList.Add(dc1.Caption);
                                        cnt++; 
                                    }
                                }
                            }
                        }
                        if (cnt == oArrayList.Count && cnt != 0)
                        {
                            for (int l = 0; l < oArrayList.Count; l++)
                            {
                                DataRow rowNum = dtNumFound.NewRow();
                                rowNum["SNo"] = (int)ds.Tables[0].Rows[j]["SNo"];
                                rowNum["Col"] = colList[l].ToString();
                                rowNum["Id"] = (int)dtSearch.Rows[i]["Id"];
                                dtNumFound.Rows.Add(rowNum);
                            }
                            if(rdWinningNumbersOnly.Checked || rdWinningAndMachineNumbers.Checked)
                              arrEventW.Add(ds.Tables[0].Rows[j]["SNo"]);
                            else
                                arrEventM.Add(ds.Tables[0].Rows[j]["SNo"]);

                        }
                        if (rdWinningAndMachineNumbers.Checked)
                        {
                            colList.Clear();
                            cnt = 0;
                            foreach (DataColumn dc1 in ds.Tables[0].Columns)
                            {
                                if (dc1.Caption.StartsWith("M"))
                                {
                                    for (int k = 0; k < oArrayList.Count; k++)
                                    {
                                        if (ds.Tables[0].Rows[j][dc1] != null && ds.Tables[0].Rows[j][dc1] != DBNull.Value)
                                            if (Convert.ToInt32(oArrayList[k]) == Convert.ToInt32(ds.Tables[0].Rows[j][dc1]))
                                        {
                                            colList.Add(dc1.Caption);
                                            cnt++;
                                        }
                                    }
                                }
                            }
                            if (cnt >= oArrayList.Count && cnt != 0)
                            {
                                for (int l = 0; l < oArrayList.Count; l++)
                                {
                                    DataRow rowNum = dtNumFound.NewRow();
                                    rowNum["SNo"] = (int)ds.Tables[0].Rows[j]["SNo"];
                                    rowNum["Col"] = colList[l].ToString();
                                    rowNum["Id"] = (int)dtSearch.Rows[i]["Id"];
                                    dtNumFound.Rows.Add(rowNum);
                                }
                                arrEventM.Add(ds.Tables[0].Rows[j]["SNo"]);
                            }
                        }
                    }
                    DataRow[] rowdtNums = dtNumFound.Select("Id = " + dtSearch.Rows[i]["Id"].ToString());
                    if (rowdtNums.Length > 0)
                    {
                        DataRow rowSum = dtSummary.NewRow();
                        DataRow rowSum1 = dtSummary.NewRow();
                        DataRow rowSum2 = dtSummary.NewRow();
                        string SearchNumbers = oArrayList[0].ToString();
                        for(int m = 1; m < oArrayList.Count; m++)
                        {
                            SearchNumbers +=" & " + oArrayList[m].ToString();
                        }
                        rowSum["SearchNumbers"] = SearchNumbers;
                        //if (arrEventW.Count > 1 && oArrayList.Count > 1 )
                        //{
                        //    DataRow[] rows = dtCountingWeek.Select("pairId = " + dtSearch.Rows[i]["Id"].ToString() + " AND W_M ='" +colName + "'" );
                        //    if (rows.Length == 0)
                        //    {
                        //        if (rdSumPattern.Checked)
                        //        {
                        //            if (!VerticalArithmatic(Convert.ToInt32(arrEventW[0]), Convert.ToInt32(arrEventW[1]), (int)dtSearch.Rows[i]["Id"], colName, oArrayList))
                        //                HorizontalArithmatic(Convert.ToInt32(arrEventW[0]), Convert.ToInt32(arrEventW[1]), (int)dtSearch.Rows[i]["Id"], colName, oArrayList);
                        //        }
                        //        else
                        //            if (!codePatternSearchV(Convert.ToInt32(arrEventW[0]), Convert.ToInt32(arrEventW[1]), (int)dtSearch.Rows[i]["Id"], colName, oArrayList))
                        //                codePatternSearchH(Convert.ToInt32(arrEventW[0]), Convert.ToInt32(arrEventW[1]), (int)dtSearch.Rows[i]["Id"], colName, oArrayList);
                                
                        //    }
                        //}
                        //if (arrEventM.Count > 1 && oArrayList.Count > 1)
                        //{
                        //    //DataRow[] rows = dtCountingWeek.Select("pairId = " + dtSearch.Rows[i]["Id"].ToString() + " AND W_M ='M'");
                        //    //if (rows.Length == 0)
                        //    //{
                        //    //    if (rdSumPattern.Checked)
                        //    //    {
                        //    //        if (!VerticalArithmatic(Convert.ToInt32(arrEventM[0]), Convert.ToInt32(arrEventM[1]), (int)dtSearch.Rows[i]["Id"], "M", oArrayList))
                        //    //            HorizontalArithmatic(Convert.ToInt32(arrEventM[0]), Convert.ToInt32(arrEventM[1]), (int)dtSearch.Rows[i]["Id"], "M", oArrayList);
                        //    //    }
                        //    //    else
                        //    //        if (!codePatternSearchV(Convert.ToInt32(arrEventW[0]), Convert.ToInt32(arrEventM[1]), (int)dtSearch.Rows[i]["Id"], "M", oArrayList))
                        //    //            codePatternSearchH(Convert.ToInt32(arrEventW[0]), Convert.ToInt32(arrEventM[1]), (int)dtSearch.Rows[i]["Id"], "M", oArrayList);
                                
                        //    //}
                        //}
                        for (int n = 0; n < arrEventW.Count ; n++)
                        {
                            rowSum1["Events"] += arrEventW[n].ToString() + ",";
                        }
                        for (int n = 0; n < arrEventM.Count ; n++)
                        {
                            rowSum2["Events"] += arrEventM[n].ToString() + ",";
                        }
                        if (rdMachineNumbersOnly.Checked)
                        {
                            rowSum["Events"] = rowSum2["Events"];
                            rowSum["WinningMachine"] = "Find in machine no only";
                            dtSummary.Rows.Add(rowSum);
                        }
                        else if (rdWinningNumbersOnly.Checked)
                        {
                            rowSum["Events"] = rowSum1["Events"];
                            rowSum["WinningMachine"] = "Find in winning no only";
                            dtSummary.Rows.Add(rowSum);
                        }
                        else
                        {
                            rowSum["Events"] = rowSum1["Events"].ToString() + rowSum2["Events"].ToString();
                            rowSum["WinningMachine"] = "Both in winning and machine";
                            rowSum1["WinningMachine"] = "Find in winning no only";
                            rowSum2["WinningMachine"] = "Find in machine no only";
                            dtSummary.Rows.Add(rowSum);
                            dtSummary.Rows.Add(rowSum1);
                            dtSummary.Rows.Add(rowSum2);
                        }
                    }
                    DataRow rowSum3 = dtSummary.NewRow();
                    dtSummary.Rows.Add(rowSum3);
                }
                ResultGrid.DataSource = dtSummary;
                //dataGridCountingHistory.DataSource = dtCountingWeek;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ResultGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void searchGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                if (flag)
                {
                    foreach (DataRow row in dtNumFound.Rows)
                        {
                            if (searchGrid.Columns[e.ColumnIndex].DataPropertyName == row["Col"].ToString() )
                            {
                                if (Convert.ToInt32(searchGrid.Rows[e.RowIndex].Cells["SNo"].Value) == Convert.ToInt32(row["SNo"]))
                                {
                                    e.CellStyle.BackColor = Color.Yellow;
                                }

                            }
                        }
                    //foreach (DataRow row in dtCountingWeek.Rows)
                    //{
                    //    int first1 = (int)row["firstLineNo1"];
                    //    int sec1 = (int)row["secondLineNo1"];
                    //    int first2 = (int)row["firstLineNo2"];
                    //    int sec2 = (int)row["secondLineNo2"];
                    //    string col1 = row["column1"].ToString();
                    //    string col2 = row["column2"].ToString();
                    //    if (searchGrid.Columns[e.ColumnIndex].DataPropertyName == col1)
                    //    {
                    //        if (Convert.ToInt32(searchGrid.Rows[e.RowIndex].Cells["SNo"].Value) == first1 || Convert.ToInt32(searchGrid.Rows[e.RowIndex].Cells["SNo"].Value) == first2)
                    //        {
                    //            if (Convert.ToInt32(row["Id"])==1)
                    //              e.CellStyle.BackColor = Color.Brown;
                    //           else if (Convert.ToInt32(row["Id"]) == 2)
                    //                e.CellStyle.BackColor = Color.Green;
                    //            else if (Convert.ToInt32(row["Id"]) == 3)
                    //                e.CellStyle.BackColor = Color.Violet;
                    //            else if (Convert.ToInt32(row["Id"] )== 4)
                    //                e.CellStyle.BackColor = Color.SkyBlue;
                    //            else if (Convert.ToInt32(row["Id"]) == 5)
                    //                e.CellStyle.BackColor = Color.Snow;
                    //            else
                    //                e.CellStyle.BackColor = Color.Salmon;
                    //        }
                    //    }
                    //    if (searchGrid.Columns[e.ColumnIndex].DataPropertyName == col2)
                    //    {
                    //        if (Convert.ToInt32(searchGrid.Rows[e.RowIndex].Cells["SNo"].Value) == sec1 || Convert.ToInt32(searchGrid.Rows[e.RowIndex].Cells["SNo"].Value) == sec2)
                    //        {

                    //            if (Convert.ToInt32(row["Id"]) == 1)
                    //                e.CellStyle.BackColor = Color.Brown;
                    //            else if (Convert.ToInt32(row["Id"]) == 2)
                    //                e.CellStyle.BackColor = Color.Green;
                    //            else if (Convert.ToInt32(row["Id"]) == 3)
                    //                e.CellStyle.BackColor = Color.Violet;
                    //            else if (Convert.ToInt32(row["Id"]) == 4)
                    //                e.CellStyle.BackColor = Color.SkyBlue;
                    //            else if (Convert.ToInt32(row["Id"]) == 5)
                    //                e.CellStyle.BackColor = Color.Snow;
                    //            else
                    //                e.CellStyle.BackColor = Color.Salmon;
                    //        }
                    //    }
                    //}//
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Refresh()
        {
            try
            {
                //saveResult();
                flag = false;
                LoadDataSet();
                for (int i = 0; i < 4; i++)
                {
                    Pair1.SetItemChecked(i, false);
                    Pair2.SetItemChecked(i, false);
                    Pair3.SetItemChecked(i, false);
                }
                if (dtSummary != null)
                    dtSummary.Rows.Clear();
                if (dtCountingWeek != null)
                    dtCountingWeek.Rows.Clear();
                txtFirstNum.Text = "";
                txtSecondNumber.Text = "";
                txtThirdNumber.Text = "";
                txtForthNum.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool VerticalArithmatic(int firstDraw, int secondDraw, int pairId, string colName,ArrayList nums)
        {
            try
            {
                int temFirstdraw = firstDraw;
                int tempSecondDraw = secondDraw;
                DataTable dtTargetdt = ds.Tables[0].Copy();
                int count = dtTargetdt.Rows.Count;
                string noList = "";
                for (int k = 0; k < nums.Count; k++)
                {
                    noList += nums[k].ToString() + " ,";
                }
                for (int cnt = 0; cnt < count; cnt++)
                {
                    while ((cnt < count) && (dtTargetdt.Rows[cnt]["W1"] == null || dtTargetdt.Rows[cnt]["W1"] == DBNull.Value))
                    {
                        dtTargetdt.Rows[cnt].Delete();
                        dtTargetdt.AcceptChanges();
                        count = count - 1;
                        if(cnt + 1 < temFirstdraw)
                        {
                            temFirstdraw--;
                            firstDraw--;
                        }
                        if(cnt + 1 < tempSecondDraw)
                        {
                            tempSecondDraw--;
                            secondDraw--;
                        }
                    }
                }
               // DataRow[] r = dtTargetdt.Select("SNo > " + (firstDraw + 1).ToString() + "AND SNo < " + (secondDraw + 1).ToString());
                int countingWeekLength = secondDraw - firstDraw - 1;
                int countingWeekLengthTemp = countingWeekLength;
                if (firstDraw < countingWeekLength)
                {
                    countingWeekLengthTemp = firstDraw - 1;
                }
                for (int i = 1; i <= countingWeekLengthTemp; i++)
                {
                    for (int j = i + 1; j <= countingWeekLengthTemp+1; j++)
                    {
                        foreach (DataColumn dc1 in ds.Tables[0].Columns)
                        {
                            foreach (DataColumn dc2 in ds.Tables[0].Columns)
                            {
                                if (dc1.Caption.StartsWith(colName) && dc2.Caption.StartsWith(colName))
                                {
                                    //if(ds.Tables[0].Rows[firstDraw - i][dc1] != null && ds.Tables[0].Rows[firstDraw - i][dc1] != DBNull.Value && ds.Tables[0].Rows[firstDraw - j][dc1] != null && ds.Tables[0].Rows[firstDraw - j][dc1] != DBNull.Value )
                                    int firstSum = (int)ds.Tables[0].Rows[firstDraw - i][dc1] + (int)ds.Tables[0].Rows[firstDraw - j][dc2];
                                    int secondSum = (int)ds.Tables[0].Rows[secondDraw - i][dc1] + (int)ds.Tables[0].Rows[secondDraw - j][dc2];
                                    if (firstSum == secondSum)
                                    {
                                        DataRow rowCounting = dtCountingWeek.NewRow();
                                        rowCounting["pairId"] = pairId;
                                        rowCounting["firstLineNo1"] = (int)ds.Tables[0].Rows[firstDraw - i]["SNo"];
                                        rowCounting["secondLineNo1"] = (int)ds.Tables[0].Rows[firstDraw - j]["SNo"];
                                        rowCounting["firstLineNo2"] = (int)ds.Tables[0].Rows[secondDraw - i]["SNo"];
                                        rowCounting["secondLineNo2"] = (int)ds.Tables[0].Rows[secondDraw - j]["SNo"];
                                        rowCounting["column1"] = dc1.Caption;
                                        rowCounting["column2"] = dc2.Caption;
                                        string strW_M = " in Winning Numbers";
                                        if(colName == "M")
                                            strW_M = " in Machine Numbers";
                                        rowCounting["criteria"] = noList + " is expected after every " + countingWeekLength.ToString() + " weeks " +  strW_M + " with counting week span of " + j.ToString() + " where by " + dc1.Caption + " + " + dc2.Caption + " = " + secondSum.ToString() ;
                                        rowCounting["Interval"] = countingWeekLength;
                                        rowCounting["CountingWeekSpan"] = j;
                                        rowCounting["W_M"] = colName;
                                        rowCounting["Id"] = dtCountingWeek.Rows.Count + 1;
                                        rowCounting["Operation"] = "Vertical Arithmatic";
                                        dtCountingWeek.Rows.Add(rowCounting);
                                       
                                        return true;
                                    }
                                    int firstDiff = (int)ds.Tables[0].Rows[firstDraw - i][dc1] - (int)ds.Tables[0].Rows[firstDraw - j][dc2];
                                    int secondDiff = (int)ds.Tables[0].Rows[secondDraw - i][dc1] - (int)ds.Tables[0].Rows[secondDraw - j][dc2];
                                    if (firstDiff == secondDiff)
                                    {
                                        string strW_M = " in Winning Numbers";
                                        if (colName == "M")
                                            strW_M = " in Machine Numbers";
                                   
                                        DataRow rowCounting = dtCountingWeek.NewRow();
                                        rowCounting["pairId"] = pairId;
                                        rowCounting["firstLineNo1"] = (int)ds.Tables[0].Rows[firstDraw - i]["SNo"];
                                        rowCounting["secondLineNo1"] = (int)ds.Tables[0].Rows[firstDraw - j]["SNo"];
                                        rowCounting["firstLineNo2"] = (int)ds.Tables[0].Rows[secondDraw - i]["SNo"];
                                        rowCounting["secondLineNo2"] = (int)ds.Tables[0].Rows[secondDraw - j]["SNo"];
                                        rowCounting["column1"] = dc1.Caption;
                                        rowCounting["column2"] = dc2.Caption;
                                        rowCounting["criteria"] = noList + " is expected after every " + countingWeekLength.ToString() + " weeks " + strW_M + " with counting week span of " + j.ToString() + " where by " + dc1.Caption + " - " + dc2.Caption + " = " + secondDiff.ToString() ;
                                        rowCounting["Interval"] = countingWeekLength;
                                        rowCounting["CountingWeekSpan"] = j;
                                        rowCounting["W_M"] = colName;
                                        rowCounting["Id"] = dtCountingWeek.Rows.Count + 1;
                                        rowCounting["Operation"] = "Vertical Arithmatic";
                                        dtCountingWeek.Rows.Add(rowCounting);
                                        return true;
                                    }
                                }
                            }
                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        private void prepareCountingWeekTable()
        {
            try
            {
                dtCountingWeek = new DataTable();
                dtCountingWeek.Columns.Add("pairId", Type.GetType("System.Int32"));
                dtCountingWeek.Columns.Add("firstLineNo1", Type.GetType("System.Int32"));
                dtCountingWeek.Columns.Add("secondLineNo1", Type.GetType("System.Int32"));
                dtCountingWeek.Columns.Add("firstLineNo2", Type.GetType("System.Int32"));
                dtCountingWeek.Columns.Add("secondLineNo2", Type.GetType("System.Int32")); 
                dtCountingWeek.Columns.Add("column1");
                dtCountingWeek.Columns.Add("column2");
                dtCountingWeek.Columns.Add("criteria");
                dtCountingWeek.Columns.Add("W_M");
                dtCountingWeek.Columns.Add("Id");
                dtCountingWeek.Columns.Add("Operation");
                dtCountingWeek.Columns.Add("Interval", Type.GetType("System.Int32"));
                dtCountingWeek.Columns.Add("CountingWeekSpan", Type.GetType("System.Int32")); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool HorizontalArithmatic(int firstDraw, int secondDraw, int pairId, string colName, ArrayList nums)
        {
            try
            {
                int temFirstdraw = firstDraw;
                int tempSecondDraw = secondDraw;
                DataTable dtTargetdt = ds.Tables[0].Copy();
                int count = dtTargetdt.Rows.Count;
                string noList = "";
                for (int k = 0; k < nums.Count; k++)
                {
                    noList += nums[k].ToString() + " ,";
                }
                for (int cnt = 0; cnt < count; cnt++)
                {
                    while ((cnt < count) && (dtTargetdt.Rows[cnt]["W1"] == null || dtTargetdt.Rows[cnt]["W1"] == DBNull.Value))
                    {
                        dtTargetdt.Rows[cnt].Delete();
                        dtTargetdt.AcceptChanges();
                        count = count - 1;
                        if (cnt + 1 < temFirstdraw)
                        {
                            temFirstdraw--;
                            firstDraw--;
                        }
                        if (cnt + 1 < tempSecondDraw)
                        {
                            tempSecondDraw--;
                            secondDraw--;
                        }
                    }
                }
                // DataRow[] r = dtTargetdt.Select("SNo > " + (firstDraw + 1).ToString() + "AND SNo < " + (secondDraw + 1).ToString());
                int countingWeekLength = secondDraw - firstDraw;
                int countingWeekLengthTemp = countingWeekLength;
                if (firstDraw < countingWeekLength)
                {
                    countingWeekLengthTemp = firstDraw ;
                }
                for (int i = 1; i <= countingWeekLengthTemp; i++)
                {
                     foreach (DataColumn dc1 in ds.Tables[0].Columns)
                        {
                            foreach (DataColumn dc2 in ds.Tables[0].Columns)
                            {
                                if (dc1.Caption.StartsWith(colName) && dc2.Caption.StartsWith(colName) && dc1.Caption != dc2.Caption)
                                { 
                                    int firstSum = (int)ds.Tables[0].Rows[firstDraw - i][dc1] + (int)ds.Tables[0].Rows[firstDraw - i][dc2];
                                    int secondSum = (int)ds.Tables[0].Rows[secondDraw - i][dc1] + (int)ds.Tables[0].Rows[secondDraw - i][dc2];
                                    if (firstSum == secondSum)
                                    {
                                        DataRow rowCounting = dtCountingWeek.NewRow();
                                        string strW_M = " in Winning Numbers";
                                        if (colName == "M")
                                            strW_M = " in Machine Numbers";
                                        rowCounting["pairId"] = pairId;
                                        rowCounting["firstLineNo1"] = (int)ds.Tables[0].Rows[firstDraw - i]["SNo"];
                                        rowCounting["secondLineNo1"] = (int)ds.Tables[0].Rows[firstDraw - i]["SNo"];
                                        rowCounting["firstLineNo2"] = (int)ds.Tables[0].Rows[secondDraw - i]["SNo"];
                                        rowCounting["secondLineNo2"] = (int)ds.Tables[0].Rows[secondDraw - i]["SNo"];
                                        rowCounting["column1"] = dc1.Caption;
                                        rowCounting["column2"] = dc2.Caption;
                                        rowCounting["criteria"] = noList + " is expected after every " + countingWeekLength.ToString() + " weeks " + strW_M + " with counting week span of " + i.ToString() + " where by " + dc1.Caption + " + " + dc2.Caption + " = " + secondSum.ToString() + " in the same draw";
                                        rowCounting["Interval"] = countingWeekLength;
                                        rowCounting["CountingWeekSpan"] = i;
                                        rowCounting["W_M"] = colName;
                                        rowCounting["Id"] = dtCountingWeek.Rows.Count + 1;
                                        rowCounting["Operation"] = "Horizontal Arithmatic";
                                        dtCountingWeek.Rows.Add(rowCounting);
                                        return true;
                                    }
                                    int firstDiff = (int)ds.Tables[0].Rows[firstDraw - i][dc1] - (int)ds.Tables[0].Rows[firstDraw - i][dc2];
                                    int secondDiff = (int)ds.Tables[0].Rows[secondDraw - i][dc1] - (int)ds.Tables[0].Rows[secondDraw - i][dc2];
                                    if (firstDiff == secondDiff)
                                    {
                                        string strW_M = " in Winning Numbers";
                                        if (colName == "M")
                                            strW_M = " in Machine Numbers";
                                        DataRow rowCounting = dtCountingWeek.NewRow();
                                        rowCounting["pairId"] = pairId;
                                        rowCounting["firstLineNo1"] = (int)ds.Tables[0].Rows[firstDraw - i]["SNo"];
                                        rowCounting["secondLineNo1"] = (int)ds.Tables[0].Rows[firstDraw - i]["SNo"];
                                        rowCounting["firstLineNo2"] = (int)ds.Tables[0].Rows[secondDraw - i]["SNo"];
                                        rowCounting["secondLineNo2"] = (int)ds.Tables[0].Rows[secondDraw - i]["SNo"];
                                        rowCounting["column1"] = dc1.Caption;
                                        rowCounting["column2"] = dc2.Caption;
                                        rowCounting["criteria"] = noList + " is expected after every " + countingWeekLength.ToString() + " weeks " + strW_M + "  with counting week span of " + i.ToString() + " where by " + dc1.Caption + " - " + dc2.Caption + " = " + secondDiff.ToString()+" in the same draw" ;
                                        rowCounting["Interval"] = countingWeekLength;
                                        rowCounting["CountingWeekSpan"] = i;
                                        rowCounting["W_M"] = colName;
                                        rowCounting["Id"] = dtCountingWeek.Rows.Count + 1;
                                        rowCounting["Operation"] = "Horizontal Arithmatic";
                                        dtCountingWeek.Rows.Add(rowCounting);
                                        return true;
                                    }
                                }
                            }

                        }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        private void saveResult()
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                         "Data Source=Result.xls;" +
                         "Extended Properties=Excel 8.0;";
            File.Create("Result.xls");
            Microsoft.Office.Interop.Excel.Application app;
            app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workBook = app.Workbooks.Open(Application.StartupPath + "\\Result.xls", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM [Result$]", strConn);
            DataSet myDataSet = new DataSet();
            myCommand.Fill(myDataSet);
            OleDbCommandBuilder cb = new OleDbCommandBuilder(myCommand);
            foreach(DataRow row in dtCountingWeek.Rows)
            {
                DataRow rowexcel = myDataSet.Tables[0].NewRow();
                rowexcel[0] = row["Operation"].ToString();
                rowexcel[1] = row["criteria"].ToString();
                dtCountingWeek.Rows.Add(rowexcel);
            }
            myDataSet.GetChanges();
            myCommand.Update(myDataSet);

        }

        private bool codePatternSearchV(int firstDraw, int secondDraw, int pairId, string colName, ArrayList nums)
        {
            try
            {
                fillCodetable();
                int temFirstdraw = firstDraw;
                int tempSecondDraw = secondDraw;
                DataTable dtTargetdt = ds.Tables[0].Copy();
                int count = dtTargetdt.Rows.Count;
                string noList = "";
                for (int k = 0; k < nums.Count; k++)
                {
                    noList += nums[k].ToString() + " ,";
                }
                for (int cnt = 0; cnt < count; cnt++)
                {
                    while ((cnt < count) && (dtTargetdt.Rows[cnt]["W1"] == null || dtTargetdt.Rows[cnt]["W1"] == DBNull.Value))
                    {
                        dtTargetdt.Rows[cnt].Delete();
                        dtTargetdt.AcceptChanges();
                        count = count - 1;
                        if (cnt + 1 < temFirstdraw)
                        {
                            temFirstdraw--;
                            firstDraw--;
                        }
                        if (cnt + 1 < tempSecondDraw)
                        {
                            tempSecondDraw--;
                            secondDraw--;
                        }
                    }
                }
                // DataRow[] r = dtTargetdt.Select("SNo > " + (firstDraw + 1).ToString() + "AND SNo < " + (secondDraw + 1).ToString());
                int countingWeekLength = secondDraw - firstDraw-1;
                int countingWeekLengthTemp = countingWeekLength;
                if (firstDraw < countingWeekLength)
                {
                    countingWeekLengthTemp = firstDraw - 1;
                }
                for (int i = 1; i <= countingWeekLengthTemp; i++)
                {
                    for (int j = i + 1; j <= countingWeekLengthTemp+1; j++)
                    {
                        foreach (DataColumn dc1 in ds.Tables[0].Columns)
                        {
                            foreach (DataColumn dc2 in ds.Tables[0].Columns)
                            {
                                if (dc1.Caption.StartsWith(colName) && dc2.Caption.StartsWith(colName))
                                {
                                    //if(ds.Tables[0].Rows[firstDraw - i][dc1] != null && ds.Tables[0].Rows[firstDraw - i][dc1] != DBNull.Value && ds.Tables[0].Rows[firstDraw - j][dc1] != null && ds.Tables[0].Rows[firstDraw - j][dc1] != DBNull.Value )
                                    int firstNo1 = (int)ds.Tables[0].Rows[firstDraw - i][dc1];
                                    int secNo1 = (int)ds.Tables[0].Rows[firstDraw - j][dc2];
                                    
                                    int firstNo2 = (int)ds.Tables[0].Rows[secondDraw - i][dc1] ;
                                    int secNo2 = (int)ds.Tables[0].Rows[secondDraw - j][dc2];
                                   
                                   
                                    foreach (DataRow rw in dtCodeTable.Rows)
                                    {
                                        if (checkMatch(rw["Code1"].ToString(), rw["Code2"].ToString(), firstNo1, secNo1))
                                        {
                                            if (checkMatch(rw["Code1"].ToString(), rw["Code2"].ToString(), firstNo2, secNo2))
                                            {
                                         DataRow rowCounting = dtCountingWeek.NewRow();
                                        rowCounting["pairId"] = pairId;
                                        rowCounting["firstLineNo1"] = (int)ds.Tables[0].Rows[firstDraw - i]["SNo"];
                                        rowCounting["secondLineNo1"] = (int)ds.Tables[0].Rows[firstDraw - j]["SNo"];
                                        rowCounting["firstLineNo2"] = (int)ds.Tables[0].Rows[secondDraw - i]["SNo"];
                                        rowCounting["secondLineNo2"] = (int)ds.Tables[0].Rows[secondDraw - j]["SNo"];
                                        rowCounting["column1"] = dc1.Caption;
                                        rowCounting["column2"] = dc2.Caption;
                                        string strW_M = " in Winning Numbers";
                                        if (colName == "M")
                                            strW_M = " in Machine Numbers";
                                        rowCounting["criteria"] = noList + " is expected after every " + countingWeekLength.ToString() + " weeks " + strW_M + " with counting week span of " + j.ToString() + " where by " + dc2.Caption + " and " + dc1.Caption + " are related as  " + rw["Code2"].ToString() + " and " + rw["Code1"].ToString();
                                        rowCounting["Interval"] = countingWeekLength;
                                        rowCounting["CountingWeekSpan"] = j;
                                        rowCounting["W_M"] = colName;
                                        rowCounting["Id"] = dtCountingWeek.Rows.Count + 1;
                                        rowCounting["Operation"] = "Code pattern search";
                                        dtCountingWeek.Rows.Add(rowCounting);
                                                return true;
                                            }
                                        }

                                    }
                                    
                                }
                            }

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        private bool codePatternSearchH(int firstDraw, int secondDraw, int pairId, string colName, ArrayList nums)
        {
            try
            {
                int temFirstdraw = firstDraw;
                int tempSecondDraw = secondDraw;
                DataTable dtTargetdt = ds.Tables[0].Copy();
                int count = dtTargetdt.Rows.Count;
                string noList = "";
                for (int k = 0; k < nums.Count; k++)
                {
                    noList += nums[k].ToString() + " ,";
                }
                for (int cnt = 0; cnt < count; cnt++)
                {
                    while ((cnt < count) && (dtTargetdt.Rows[cnt]["W1"] == null || dtTargetdt.Rows[cnt]["W1"] == DBNull.Value))
                    {
                        dtTargetdt.Rows[cnt].Delete();
                        dtTargetdt.AcceptChanges();
                        count = count - 1;
                        if (cnt + 1 < temFirstdraw)
                        {
                            temFirstdraw--;
                            firstDraw--;
                        }
                        if (cnt + 1 < tempSecondDraw)
                        {
                            tempSecondDraw--;
                            secondDraw--;
                        }
                    }
                }
                // DataRow[] r = dtTargetdt.Select("SNo > " + (firstDraw + 1).ToString() + "AND SNo < " + (secondDraw + 1).ToString());
                int countingWeekLength = secondDraw - firstDraw;
                int countingWeekLengthTemp = countingWeekLength;
                if (firstDraw < countingWeekLength)
                {
                    countingWeekLengthTemp = firstDraw ;
                }
                for (int i = 1; i <= countingWeekLengthTemp; i++)
                {
                    foreach (DataColumn dc1 in ds.Tables[0].Columns)
                    {
                        foreach (DataColumn dc2 in ds.Tables[0].Columns)
                        {
                            if (dc1.Caption.StartsWith(colName) && dc2.Caption.StartsWith(colName) && dc1.Caption != dc2.Caption)
                            {
                                int firstNo1 = (int)ds.Tables[0].Rows[firstDraw - i][dc1] ;
                                int secNo1 = (int)ds.Tables[0].Rows[firstDraw - i][dc2];
                                int firstNo2 = (int)ds.Tables[0].Rows[secondDraw - i][dc1];
                                int secNo2 = (int)ds.Tables[0].Rows[secondDraw - i][dc2];
                                foreach (DataRow rw in dtCodeTable.Rows)
                                    {
                                        if (checkMatch(rw["Code1"].ToString(), rw["Code2"].ToString(), firstNo1, secNo1))
                                        {
                                            if (checkMatch(rw["Code1"].ToString(), rw["Code2"].ToString(), firstNo2, secNo2))
                                            {
                                           
                                    DataRow rowCounting = dtCountingWeek.NewRow();
                                    string strW_M = " in Winning Numbers";
                                    if (colName == "M")
                                        strW_M = " in Machine Numbers";
                                    rowCounting["pairId"] = pairId;
                                    rowCounting["firstLineNo1"] = (int)ds.Tables[0].Rows[firstDraw - i]["SNo"];
                                    rowCounting["secondLineNo1"] = (int)ds.Tables[0].Rows[firstDraw - i]["SNo"];
                                    rowCounting["firstLineNo2"] = (int)ds.Tables[0].Rows[secondDraw - i]["SNo"];
                                    rowCounting["secondLineNo2"] = (int)ds.Tables[0].Rows[secondDraw - i]["SNo"];
                                    rowCounting["column1"] = dc1.Caption;
                                    rowCounting["column2"] = dc2.Caption;
                                    rowCounting["criteria"] = noList + " is expected after every " + countingWeekLength.ToString() + " weeks " + strW_M + " with counting week span of " + i.ToString() + " where by " + dc1.Caption + " + " + dc2.Caption + "  are releted as " + rw["Code1"].ToString() + " and " + rw["Code2"].ToString(); 
                                    rowCounting["Interval"] = countingWeekLength;
                                    rowCounting["CountingWeekSpan"] = i;
                                    rowCounting["W_M"] = colName;
                                    rowCounting["Id"] = dtCountingWeek.Rows.Count + 1;
                                    rowCounting["Operation"] = "Code pattern search";
                                    dtCountingWeek.Rows.Add(rowCounting);
                                    return true;
                                            }
                                        }
                                }
                               
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        private void fillCodetable()
        {
            try
            {
                dtCodeTable = new DataTable();
                dtCodeTable.Columns.Add("Id", Type.GetType("System.Int32"));
                dtCodeTable.Columns.Add("Code1");
                dtCodeTable.Columns.Add("Code2");


                DataRow row1 = dtCodeTable.NewRow();
                row1["Code1"] = "RN";
                row1["Code2"] = "RN";
                row1["Id"] = 1;
                dtCodeTable.Rows.Add(row1);


                DataRow row2 = dtCodeTable.NewRow();
                row2["Code1"] = "RN";
                row2["Code2"] = "CN";
                row2["Id"] = 2;
                dtCodeTable.Rows.Add(row2);

                DataRow row3 = dtCodeTable.NewRow();
                row3["Code1"] = "RN";
                row3["Code2"] = "TN";
                row3["Id"] = 3;
                dtCodeTable.Rows.Add(row3);

                DataRow row4 = dtCodeTable.NewRow();
                row4["Code1"] = "RN";
                row4["Code2"] = "TCN";
                row4["Id"] = 4;
                dtCodeTable.Rows.Add(row4);

                DataRow row5 = dtCodeTable.NewRow();
                row5["Code1"] = "CN";
                row5["Code2"] = "TN";
                row5["Id"] = 5;
                dtCodeTable.Rows.Add(row5);

                DataRow row6 = dtCodeTable.NewRow();
                row5["Code1"] = "CN";
                row5["Code2"] = "TCN";
                row5["Id"] = 6;
                dtCodeTable.Rows.Add(row6);

                DataRow row7 = dtCodeTable.NewRow();
                row7["Code1"] = "TN";
                row7["Code2"] = "CN";
                row7["Id"] = 7;
                dtCodeTable.Rows.Add(row7);

                DataRow row8 = dtCodeTable.NewRow();
                row8["Code1"] = "TN";
                row8["Code2"] = "TCN";
                row8["Id"] =8;
                dtCodeTable.Rows.Add(row8);

                DataRow row9 = dtCodeTable.NewRow();
                row9["Code1"] = "TCN";
                row9["Code2"] = "CN";
                row9["Id"] = 9;
                dtCodeTable.Rows.Add(row9);

                DataRow row10 = dtCodeTable.NewRow();
                row10["Code1"] = "TCN";
                row10["Code2"] = "TN";
                row10["Id"] = 10;
                dtCodeTable.Rows.Add(row10);


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

    }
}
