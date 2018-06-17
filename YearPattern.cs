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
namespace SystamaticDBSearch
{
    public partial class YearPattern : Form
    {
        public YearPattern()
        {
            InitializeComponent();
        }
        private static DataSet ds;
        private static DataTable dtNumFound;
        private static DataTable dtSummary;
        private static DataTable dtSpanForcast;
        private static DataTable dtNumFoundHistory;
        private static DataTable dtSummaryHistory;
        private static bool flag = false;
        private static DataTable dtGroupSummary;
        private void YearPattern_Load(object sender, EventArgs e)
        {
            flag = false;
            LoadDataSet();
            InitialSetting();
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
                    DataSet dsTemp = new DataSet();
                    SqlClass.GetWin_Machin_DataByDBId(DBId, ref dsTemp);
                    DataTable dtTemp = new DataTable();
                    foreach (DataColumn dc in dsTemp.Tables[0].Columns)
                    {
                        dtTemp.Columns.Add(dc.Caption, dc.DataType);
                        if (dc.Caption == "SUM_W")
                        {
                            dtTemp.Columns.Add("space1");
                            dtTemp.Columns.Add("space2");
                        }
                    }
                    dtTemp.Merge(dsTemp.Tables[0]);
                    ds.Tables.Add(dtTemp);
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdInternal.Checked)
                {
                    ds.AcceptChanges();
                    if (rdSpanPattern.Checked || rdSpanCode.Checked || rdSpanMod.Checked)
                        SpanSearch();
                    //SpanPatternSearch();
                    else if (rdYearPattern.Checked)
                        YearPatternSearch();
                    else
                        GroupSummaation();
                    flag = true;
                    searchGrid.Refresh();
                }
                else
                {
                    if (rdGroupSummation.Checked)
                    {
                        GroupSummationExternal();
                        flag = true;
                        searchGrid.Columns["RecNo"].Visible = false;
                        searchGrid.Refresh();
                    }
                    if (rdYearPattern.Checked)
                    {
                        flag = true;
                        YearPatternSearch();
                    }
                }
                //LoadDataSet();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void YearPatternSearchInternal()
        {
            try
            {
                dtNumFound = new DataTable();
                dtNumFound.Columns.Add("Id");
                dtNumFound.Columns.Add("col");
                dtNumFound.Columns.Add("SNo");
                dtNumFound.Columns.Add("flag");

                dtSummary = new DataTable();
                dtSummary.Columns.Add("Id");
                dtSummary.Columns.Add("Year");
                dtSummary.Columns.Add("PairNo");
                dtSummary.Columns.Add("SearchNo");
                dtSummary.Columns.Add("Database");
                DataTable dtTargetdt = ds.Tables[0].Copy();
                int count = dtTargetdt.Rows.Count;
                for (int cnt = 0; cnt < count; cnt++)
                {
                    while ((cnt < count) && (dtTargetdt.Rows[cnt]["W1"] == null || dtTargetdt.Rows[cnt]["W1"] == DBNull.Value))
                    {
                        dtTargetdt.Rows[cnt].Delete();
                        dtTargetdt.AcceptChanges();
                        count = count - 1;
                    }
                }
                bool blnBreak = false;
                int tYear = 0;
                int sYear = 0;
                for (int i = 0; i < dtTargetdt.Rows.Count; i++)
                {
                    if (dtTargetdt.Rows[i]["Date"] != DBNull.Value)
                    {
                        //if (sYear == Convert.ToDateTime(dtTargetdt.Rows[i]["Date"]).Year)
                        //    continue;
                        sYear = Convert.ToDateTime(dtTargetdt.Rows[i]["Date"]).Year;
                    }
                    //dtNumFound.Rows.Clear();
                    if (Convert.ToInt32(dtTargetdt.Rows[i]["W1"]) > Convert.ToInt32(dtTargetdt.Rows[i]["W4"]))
                    {
                        int dif = Convert.ToInt32(dtTargetdt.Rows[i]["W1"]) - Convert.ToInt32(dtTargetdt.Rows[i]["W4"]);
                        for (int j = i+1; j < dtTargetdt.Rows.Count; j++)
                        {
                            if (dtTargetdt.Rows[j]["Date"] != DBNull.Value)
                                tYear = Convert.ToDateTime(dtTargetdt.Rows[j]["Date"]).Year;
                            
                            if (Convert.ToInt32(dtTargetdt.Rows[j]["W1"]) == dif && sYear == tYear  )
                            {
                                DataRow row = dtNumFound.NewRow();
                                row["Id"] = dtNumFound.Rows.Count + 1;
                                row["col"] = "W1";
                                row["SNo"] = dtTargetdt.Rows[j]["SNo"];
                                row["flag"] = "N";
                                dtNumFound.Rows.Add(row);

                                DataRow row1 = dtNumFound.NewRow();
                                row1["Id"] = dtNumFound.Rows.Count + 1;
                                row1["col"] = "W1";
                                row1["SNo"] = dtTargetdt.Rows[i]["SNo"];
                                row1["flag"] = "S";
                                dtNumFound.Rows.Add(row1);

                                DataRow row2 = dtNumFound.NewRow();
                                row2["Id"] = dtNumFound.Rows.Count + 1;
                                row2["col"] = "W4";
                                row2["SNo"] = dtTargetdt.Rows[i]["SNo"];
                                row2["flag"] = "S";
                                dtNumFound.Rows.Add(row2);
                                int ttYear = tYear;
                                blnBreak = false;
                                for (int k = j + 1; k < dtTargetdt.Rows.Count; k++)
                                {
                                    if (dtTargetdt.Rows[k]["Date"] != DBNull.Value)
                                        ttYear = Convert.ToDateTime(dtTargetdt.Rows[k]["Date"]).Year;
                                    foreach (DataColumn dc in dtTargetdt.Columns)
                                    {
                                        if(dc.Caption.StartsWith("W"))
                                        {
                                            if (Convert.ToInt32(dtTargetdt.Rows[k][dc]) == dif && tYear == ttYear)
                                            {
                                                DataRow row3 = dtNumFound.NewRow();
                                                row3["Id"] = dtNumFound.Rows.Count + 1;
                                                row3["col"] = dc.Caption;
                                                row3["SNo"] = dtTargetdt.Rows[k]["SNo"];
                                                row3["flag"] = "N";
                                                dtNumFound.Rows.Add(row3);
                                                blnBreak = true;
                                                DataRow rowSummary = dtSummary.NewRow();
                                                rowSummary["Id"] = 1;
                                                rowSummary["Year"] = tYear;
                                                rowSummary["Database"] = lblInternalDatabaseName.Text;
                                                rowSummary["PairNo"] =dtTargetdt.Rows[i]["SNo"].ToString() ;
                                                rowSummary["SearchNo"] = dtTargetdt.Rows[j]["SNo"].ToString() + " , " + dtTargetdt.Rows[k]["SNo"].ToString();
                                                dtSummary.Rows.Add(rowSummary);
                                                break;
                                            }
                                        }
                                    }
                                    if (blnBreak)
                                        break;
                                }
                                if (!blnBreak)
                                {
                                    dtNumFound.Rows[dtNumFound.Rows.Count - 1].Delete();
                                    dtNumFound.AcceptChanges();
                                    dtNumFound.Rows[dtNumFound.Rows.Count - 1].Delete();
                                    dtNumFound.AcceptChanges();
                                    dtNumFound.Rows[dtNumFound.Rows.Count - 1].Delete();
                                    dtNumFound.AcceptChanges();
                                }
                            }
                            if (blnBreak)
                                break;
                        }
                    }
                    //if (blnBreak)
                    //    break;
                }
                grdSummary.DataSource = dtSummary;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void YearPatternSearchInternalMachine()
        {
            try
            {
                dtNumFound = new DataTable();
                dtNumFound.Columns.Add("Id");
                dtNumFound.Columns.Add("col");
                dtNumFound.Columns.Add("SNo");
                dtNumFound.Columns.Add("flag");

                dtSummary = new DataTable();
                dtSummary.Columns.Add("Id");
                dtSummary.Columns.Add("Year");
                dtSummary.Columns.Add("PairNo");
                dtSummary.Columns.Add("SearchNo");
                dtSummary.Columns.Add("Database");
                DataTable dtTargetdt = ds.Tables[0].Copy();
                int count = dtTargetdt.Rows.Count;
                for (int cnt = 0; cnt < count; cnt++)
                {
                    while ((cnt < count) && (dtTargetdt.Rows[cnt]["W1"] == null || dtTargetdt.Rows[cnt]["W1"] == DBNull.Value))
                    {
                        dtTargetdt.Rows[cnt].Delete();
                        dtTargetdt.AcceptChanges();
                        count = count - 1;
                    }
                }
                bool blnBreak = false;
                int tYear = 0;
                int sYear = 0;
                for (int i = 0; i < dtTargetdt.Rows.Count; i++)
                {
                    if (dtTargetdt.Rows[i]["Date"] != DBNull.Value)
                    {
                        //if (sYear == Convert.ToDateTime(dtTargetdt.Rows[i]["Date"]).Year)
                        //    continue;
                        sYear = Convert.ToDateTime(dtTargetdt.Rows[i]["Date"]).Year;
                    }
                    //dtNumFound.Rows.Clear();
                    if (Convert.ToInt32(dtTargetdt.Rows[i]["M1"]) > Convert.ToInt32(dtTargetdt.Rows[i]["M4"]))
                    {
                        int dif = Convert.ToInt32(dtTargetdt.Rows[i]["M1"]) - Convert.ToInt32(dtTargetdt.Rows[i]["M4"]);
                        for (int j = i + 1; j < dtTargetdt.Rows.Count; j++)
                        {
                            if (dtTargetdt.Rows[j]["Date"] != DBNull.Value)
                                tYear = Convert.ToDateTime(dtTargetdt.Rows[j]["Date"]).Year;

                            if (Convert.ToInt32(dtTargetdt.Rows[j]["M1"]) == dif && sYear == tYear)
                            {
                                DataRow row = dtNumFound.NewRow();
                                row["Id"] = dtNumFound.Rows.Count + 1;
                                row["col"] = "M1";
                                row["SNo"] = dtTargetdt.Rows[j]["SNo"];
                                row["flag"] = "N";
                                dtNumFound.Rows.Add(row);

                                DataRow row1 = dtNumFound.NewRow();
                                row1["Id"] = dtNumFound.Rows.Count + 1;
                                row1["col"] = "M1";
                                row1["SNo"] = dtTargetdt.Rows[i]["SNo"];
                                row1["flag"] = "S";
                                dtNumFound.Rows.Add(row1);

                                DataRow row2 = dtNumFound.NewRow();
                                row2["Id"] = dtNumFound.Rows.Count + 1;
                                row2["col"] = "M4";
                                row2["SNo"] = dtTargetdt.Rows[i]["SNo"];
                                row2["flag"] = "S";
                                dtNumFound.Rows.Add(row2);
                                int ttYear = tYear;
                                blnBreak = false;
                                for (int k = j + 1; k < dtTargetdt.Rows.Count; k++)
                                {
                                    if (dtTargetdt.Rows[k]["Date"] != DBNull.Value)
                                        ttYear = Convert.ToDateTime(dtTargetdt.Rows[k]["Date"]).Year;
                                    foreach (DataColumn dc in dtTargetdt.Columns)
                                    {
                                        if (dc.Caption.StartsWith("M"))
                                        {
                                            if (Convert.ToInt32(dtTargetdt.Rows[k][dc]) == dif && tYear == ttYear)
                                            {
                                                DataRow row3 = dtNumFound.NewRow();
                                                row3["Id"] = dtNumFound.Rows.Count + 1;
                                                row3["col"] = dc.Caption;
                                                row3["SNo"] = dtTargetdt.Rows[k]["SNo"];
                                                row3["flag"] = "N";
                                                dtNumFound.Rows.Add(row3);
                                                blnBreak = true;
                                                DataRow rowSummary = dtSummary.NewRow();
                                                rowSummary["Id"] = 1;
                                                rowSummary["Year"] = tYear;
                                                rowSummary["Database"] = lblInternalDatabaseName.Text;
                                                rowSummary["PairNo"] = dtTargetdt.Rows[i]["SNo"].ToString();
                                                rowSummary["SearchNo"] = dtTargetdt.Rows[j]["SNo"].ToString() + " , " + dtTargetdt.Rows[k]["SNo"].ToString();
                                                dtSummary.Rows.Add(rowSummary);
                                                break;
                                            }
                                        }
                                    }
                                    if (blnBreak)
                                        break;
                                }
                                if (!blnBreak)
                                {
                                    dtNumFound.Rows[dtNumFound.Rows.Count - 1].Delete();
                                    dtNumFound.AcceptChanges();
                                    dtNumFound.Rows[dtNumFound.Rows.Count - 1].Delete();
                                    dtNumFound.AcceptChanges();
                                    dtNumFound.Rows[dtNumFound.Rows.Count - 1].Delete();
                                    dtNumFound.AcceptChanges();
                                }
                            }
                            if (blnBreak)
                                break;
                        }
                    }
                    //if (blnBreak)
                    //    break;
                }
                grdSummary.DataSource = dtSummary;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool checkRelation(int No1, int No2, string Rel)
        {
            return checkMatch("RN",Rel,No1,No2);
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

        private int GetTurning(int RN)
        {
            if (RN == -1)
                return -1;

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
                return -1;
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
        private void searchGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (flag)
                {
                    if (!(e.RowIndex < 0 || e.ColumnIndex < 0))
                    {
                        if (rdInternal.Checked)
                        {
                            foreach (DataRow row in dtNumFound.Rows)
                            {
                                if (searchGrid.Columns[e.ColumnIndex].DataPropertyName == row["col"].ToString())
                                {
                                    if (Convert.ToInt32(searchGrid.Rows[e.RowIndex].Cells["SNo"].Value) == Convert.ToInt32(row["SNo"]))
                                    {
                                        if (rdYearPattern.Checked || rdGroupSummation.Checked)
                                        {
                                            if (row["flag"].ToString() == "S")
                                                e.CellStyle.BackColor = Color.Yellow;
                                            else
                                                e.CellStyle.BackColor = Color.Brown;
                                        }
                                        else
                                        {
                                            if (row["flag"].ToString() == "K")
                                                e.CellStyle.BackColor = Color.DarkGreen;
                                            else if (row["flag"].ToString() == "P")
                                                e.CellStyle.BackColor = Color.Orange;
                                            else
                                                e.CellStyle.BackColor = Color.SkyBlue;
                                        }
                                    }

                                }
                            }
                        }
                        else
                        {
                            foreach (DataRow row in dtNumFound.Rows)
                            {
                                if (rdGroupSummation.Checked)
                                {
                                    if (searchGrid.Rows[e.RowIndex].Cells["SNo"].Value != null && searchGrid.Rows[e.RowIndex].Cells["SNo"].Value != DBNull.Value && searchGrid.Rows[e.RowIndex].Cells["RecNo"].Value != null && searchGrid.Rows[e.RowIndex].Cells["RecNo"].Value != DBNull.Value)
                                    {
                                        if (searchGrid.Columns[e.ColumnIndex].DataPropertyName == row["col"].ToString())
                                        {
                                            if (Convert.ToInt32(searchGrid.Rows[e.RowIndex].Cells["SNo"].Value) == Convert.ToInt32(row["SNo"]) && Convert.ToInt32(searchGrid.Rows[e.RowIndex].Cells["RecNo"].Value) == Convert.ToInt32(row["RecNo"]))
                                            {
                                                //if (rdYearPattern.Checked || rdGroupSummation.Checked)
                                                {
                                                    if (row["flag"].ToString() == "S")
                                                        e.CellStyle.BackColor = Color.Yellow;
                                                    else
                                                        e.CellStyle.BackColor = Color.Brown;
                                                }

                                            }

                                        }
                                    }
                                }
                                if (rdYearPattern.Checked)
                                {
                                    if (searchGrid.Rows[e.RowIndex].Cells["SNo"].Value != null && searchGrid.Rows[e.RowIndex].Cells["SNo"].Value != DBNull.Value && searchGrid.Rows[e.RowIndex].Cells["DBId"].Value != null && searchGrid.Rows[e.RowIndex].Cells["DBId"].Value != DBNull.Value)
                                    {
                                        if (searchGrid.Columns[e.ColumnIndex].DataPropertyName == row["col"].ToString())
                                        {
                                            if (Convert.ToInt32(searchGrid.Rows[e.RowIndex].Cells["SNo"].Value) == Convert.ToInt32(row["SNo"]) && Convert.ToInt32(searchGrid.Rows[e.RowIndex].Cells["DBId"].Value) == Convert.ToInt32(row["DBId"]))
                                            {
                                                //if (rdYearPattern.Checked || rdGroupSummation.Checked)
                                                {
                                                    if (row["flag"].ToString() == "S")
                                                        e.CellStyle.BackColor = Color.Yellow;
                                                    else
                                                        e.CellStyle.BackColor = Color.Brown;
                                                }

                                            }

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
        }

        private void rdSpanPattern_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSpanPattern.Checked)
            {
                grdSummaryGroupSummation.Visible = false;

                grdSpanSummary.Visible = true;
                grdSummary.Visible = false;
                searchGrid.Columns["space2"].Visible = false;
            }
            else
            {
                //grdSpanSummary.Visible = false;
                //grdSummary.Visible = true;
            }

        }

        private void rdGroupSummation_CheckedChanged(object sender, EventArgs e)
        {
            if (rdGroupSummation.Checked)
            {
                //space2.Visible = true;
                grdSummaryGroupSummation.Visible = true;
                searchGrid.Columns["space2"].Visible = true;
                grdSpanSummary.Visible = false;
                grdSummary.Visible = false;
                lblSummary.Visible = false;
            }
            else
            {
                grdSummaryGroupSummation.Visible = false;
                searchGrid.Columns["space2"].Visible = false;
                lblSummary.Visible = true;
            }
        }
        private void GroupSummaation()
        {
            try
            {
                dtNumFound = new DataTable();
                dtNumFound.Columns.Add("Id");
                dtNumFound.Columns.Add("col");
                dtNumFound.Columns.Add("SNo");
                dtNumFound.Columns.Add("flag");

                DataTable dtTargetdt = ds.Tables[0].Copy();
                int count = dtTargetdt.Rows.Count;
                for (int cnt = 0; cnt < count; cnt++)
                {
                    while ((cnt < count) && (dtTargetdt.Rows[cnt]["W1"] == null || dtTargetdt.Rows[cnt]["W1"] == DBNull.Value))
                    {
                        dtTargetdt.Rows[cnt].Delete();
                        dtTargetdt.AcceptChanges();
                        count = count - 1;
                    }
                }
                int noCount = 0;
                bool found1 = false;
                bool found2 = false;
                string x1="",y1="",x2="",y2="";
                dtNumFound.Rows.Clear();
                dtGroupSummary.Rows.Clear();
                DataTable dtTemp = dtNumFound.Clone();
                bool MatchFound = false;
                int No1 = 0, No2 = 0;
                progressBar1.Value = 0;
                progressBar1.Maximum = count;
                for (int i = 0; i < count-3; )
                {
                    if (i >= count - 8 && i <= count - 5)
                    {
                        i++;
                        continue;
                    }
                    progressBar1.Value = i;
                    noCount = 0;
                    found1 = false;
                    found2 = false;
                    dtTemp.Rows.Clear();
                    foreach (DataColumn dc1 in dtTargetdt.Columns)
                    {
                        found1 = false;
                        if (dc1.Caption.StartsWith("W"))
                        {
                            foreach (DataColumn dc2 in dtTargetdt.Columns)
                            {
                                if (dc2.Caption.StartsWith("W"))
                                {
                                    int[] X = new int[3];
                                    X[0] = Convert.ToInt32(dtTargetdt.Rows[i][dc1]) - Convert.ToInt32(dtTargetdt.Rows[i + 1][dc2]);
                                    X[1] = Convert.ToInt32(dtTargetdt.Rows[i][dc1]) + Convert.ToInt32(dtTargetdt.Rows[i + 1][dc2]);
                                    X[2] = -Convert.ToInt32(dtTargetdt.Rows[i][dc1]) + Convert.ToInt32(dtTargetdt.Rows[i + 1][dc2]);
                                    for (int j = 0; j < 3; j++)
                                    {

                                        foreach (DataColumn dc3 in dtTargetdt.Columns)
                                        {
                                            if (dc3.Caption.StartsWith("W"))
                                            {
                                                int[] Y = new int[3];
                                                Y[0] = Convert.ToInt32(dtTargetdt.Rows[i+1][dc2]) - Convert.ToInt32(dtTargetdt.Rows[i + 2][dc3]);
                                                Y[1] = Convert.ToInt32(dtTargetdt.Rows[i+1][dc2]) + Convert.ToInt32(dtTargetdt.Rows[i + 2][dc3]);
                                                Y[2] = -Convert.ToInt32(dtTargetdt.Rows[i+1][dc2]) + Convert.ToInt32(dtTargetdt.Rows[i + 2][dc3]);

                                                for (int k = 0; k < 3; k++)
                                                {
                                                    foreach (DataColumn dc4 in dtTargetdt.Columns)
                                                    {
                                                        if (dc4.Caption.StartsWith("W"))
                                                        {
                                                             int T2 = Convert.ToInt32(dtTargetdt.Rows[i + 2][dc3]);
                                                            int T3 = Convert.ToInt32(dtTargetdt.Rows[i + 3][dc4]);
                                                            if ((j == 0 && T3 == T2 - X[j]) || (j == 1 && T3 == X[j] - T2) || (j == 2 && T3 == T2 + X[j]))
                                                            {
                                                                foreach (DataColumn dc5 in dtTargetdt.Columns)
                                                                {
                                                                    if (dc5.Caption.StartsWith("W"))
                                                                    {
                                                                        int T4 = 0;
                                                                        if(i+4 != count)
                                                                        T4 = Convert.ToInt32(dtTargetdt.Rows[i + 4][dc5]);
                                                                        if (T4 == 0 || (k == 0 && T4 == T3 - Y[k]) || (k == 1 && T4 == Y[k] - T3) || (k == 2 && T4 == T3 + Y[k]))
                                                                        {
                                                                            if (T4 == 0)
                                                                            {
                                                                                int key = T3 - Y[k];
                                                                                if (k == 1)
                                                                                    key = Y[k] - T3;
                                                                                if (k == 2)
                                                                                    key = T3 + Y[k];
                                                                                if (key < 1 || key > 90)
                                                                                    continue;
                                                                            }

                                                                            noCount += 1;
                                                                            found1 = true;
                                                                            if (noCount == 2)
                                                                            {
                                                                                found2 = true;
                                                                                if (j == 0)
                                                                                    x2 = (-X[j]).ToString();
                                                                                else if (j == 1)
                                                                                    x2 = X[j].ToString() + "-";
                                                                                else
                                                                                    x2 = X[j].ToString();

                                                                                if (k == 0)
                                                                                    y2 = (-Y[k]).ToString();
                                                                                else if (k == 1)
                                                                                    y2 = Y[k].ToString() + "-";
                                                                                else
                                                                                    y2 = Y[k].ToString();
                                                                            }
                                                                            else
                                                                            {
                                                                                if (j == 0)
                                                                                    x1 = (-X[j]).ToString();
                                                                                else if (j == 1)
                                                                                    x1 = X[j].ToString() + "-";
                                                                                else
                                                                                    x1 = X[j].ToString();

                                                                                if (k == 0)
                                                                                    y1 = (-Y[k]).ToString();
                                                                                else if (k == 1)
                                                                                    y1 = Y[k].ToString() + "-";
                                                                                else
                                                                                    y1 = Y[k].ToString();
                                                                            }
                                                                            DataRow row1 = dtTemp .NewRow();
                                                                            row1["Id"] = dtTemp .Rows.Count + 1;
                                                                            row1["col"] = dc1.Caption;
                                                                            row1["SNo"] = dtTargetdt.Rows[i]["SNo"];
                                                                            row1["flag"] = "S";
                                                                            if (noCount==2)
                                                                                row1["flag"] = "N";
                                                                            dtTemp .Rows.Add(row1);

                                                                            DataRow row2 = dtTemp .NewRow();
                                                                            row2["Id"] = dtTemp .Rows.Count + 1;
                                                                            row2["col"] = dc2.Caption;
                                                                            row2["SNo"] = dtTargetdt.Rows[i+1]["SNo"];
                                                                            row2["flag"] = "S";
                                                                            if (noCount == 2)
                                                                                row2["flag"] = "N";
                                                                            dtTemp .Rows.Add(row2);


                                                                            DataRow row3 = dtTemp .NewRow();
                                                                            row3["Id"] = dtTemp .Rows.Count + 1;
                                                                            row3["col"] = dc3.Caption;
                                                                            row3["SNo"] = dtTargetdt.Rows[i+2]["SNo"];
                                                                            row3["flag"] = "S";
                                                                            if (noCount == 2)
                                                                                row3["flag"] = "N";
                                                                            dtTemp .Rows.Add(row3);

                                                                            DataRow row4 = dtTemp .NewRow();
                                                                            row4["Id"] = dtTemp .Rows.Count + 1;
                                                                            row4["col"] = dc4.Caption;
                                                                            row4["SNo"] = dtTargetdt.Rows[i+3]["SNo"];
                                                                            row4["flag"] = "S";
                                                                            if (noCount == 2)
                                                                                row4["flag"] = "N";
                                                                            dtTemp .Rows.Add(row4);
                                                                            if (T4 != 0)
                                                                            {
                                                                                DataRow row5 = dtTemp.NewRow();
                                                                                row5["Id"] = dtTemp.Rows.Count + 1;
                                                                                row5["col"] = dc5.Caption;
                                                                                row5["SNo"] = dtTargetdt.Rows[i + 4]["SNo"];
                                                                                row5["flag"] = "S";
                                                                                if (noCount == 2)
                                                                                    row5["flag"] = "N";
                                                                                dtTemp.Rows.Add(row5);
                                                                            }

                                                                            DataRow row6 = dtTemp .NewRow();
                                                                            row6["Id"] = dtTemp .Rows.Count + 1;
                                                                            row6["col"] = "space1";
                                                                            row6["SNo"] = dtTargetdt.Rows[i]["SNo"];
                                                                            row6["flag"] = "S";
                                                                            if (noCount == 2)
                                                                            {
                                                                                row6["flag"] = "N";
                                                                                row6["col"] = "space2";
                                                                            }
                                                                            dtTemp .Rows.Add(row6);

                                                                            DataRow row7 = dtTemp .NewRow();
                                                                            row7["Id"] = dtTemp .Rows.Count + 1;
                                                                            row7["col"] = "space1";
                                                                            row7["SNo"] = dtTargetdt.Rows[i + 1]["SNo"];
                                                                            row7["flag"] = "S";
                                                                            if (noCount == 2)
                                                                            {
                                                                                row7["col"] = "space2";
                                                                                row7["flag"] = "N";
                                                                            }
                                                                            dtTemp .Rows.Add(row7);


                                                                            DataRow row8 = dtTemp .NewRow();
                                                                            row8["Id"] = dtTemp .Rows.Count + 1;
                                                                            row8["col"] = "space1";
                                                                            row8["SNo"] = dtTargetdt.Rows[i + 2]["SNo"];
                                                                            row8["flag"] = "S";
                                                                            if (noCount == 2)
                                                                            {
                                                                                row8["flag"] = "N";
                                                                                row8["col"] = "space2";
                                                                            }
                                                                            dtTemp .Rows.Add(row8);

                                                                            DataRow row9 = dtTemp .NewRow();
                                                                            row9["Id"] = dtTemp .Rows.Count + 1;
                                                                            row9["col"] = "space1";
                                                                            row9["SNo"] = dtTargetdt.Rows[i + 3]["SNo"];
                                                                            row9["flag"] = "S";
                                                                            if (noCount == 2)
                                                                            {
                                                                                row9["flag"] = "N";
                                                                                row9["col"] = "space2";
                                                                            }
                                                                            dtTemp .Rows.Add(row9);

                                                                            if (T4 == 0)
                                                                            {
                                                                                MatchFound = true;
                                                                                if (noCount == 1)
                                                                                    No1 = T3;
                                                                                else
                                                                                    No2 = T3;
                                                                                break;
                                                                                
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                        }
                                                        if (found1)
                                                            break;
                                                    }
                                                }
                                            }
                                            if (found1)
                                                break;
                                        }
                                        if (found1)
                                            break;
                                    }

                                }
                                if (found1)
                                    break;
                            }

                        }
                        if (found2)
                            break;
                    }
                    if (found2)
                    {
                        foreach (DataRow rowTemp in dtTemp.Rows)
                        {
                            DataRow rowNumFound = dtNumFound.NewRow();
                            rowNumFound["Id"] = dtNumFound.Rows.Count + 1;
                            rowNumFound["col"] = rowTemp["col"];
                            rowNumFound["SNo"] = rowTemp["SNo"];
                            rowNumFound["flag"] = rowTemp["flag"];
                            dtNumFound.Rows.Add(rowNumFound);
                        }
                        int startSNo = Convert.ToInt32(dtTemp.Rows[0]["SNo"]);
                        int endSNo = Convert.ToInt32(dtTemp.Rows[3]["SNo"]);
                        DataRow[] rowF = ds.Tables[0].Select("SNo > " + (startSNo - 1).ToString() + " AND SNo < " + (endSNo + 1).ToString());
                        if (rowF.Length > 0)
                        {
                            int l = 0;
                            if (!ds.Tables[0].Columns.Contains("space1"))
                                ds.Tables[0].Columns.Add("space1");
                            if (!ds.Tables[0].Columns.Contains("space2"))
                                ds.Tables[0].Columns.Add("space2");
                            foreach (DataRow rowf in rowF)
                            {
                                if (rowf["W1"] != DBNull.Value)
                                {
                                    if (l == 0)
                                    {
                                        rowf["space1"] = x1;
                                        rowf["space2"] = x2;
                                        l = 1;
                                    }
                                    else
                                    {
                                        rowf["space1"] = y1;
                                        rowf["space2"] = y2;
                                        l = 0;
                                    }
                                }
                            }
                            if (MatchFound)
                            {
                                int Lucky1, Lucky2;
                                if (!y1.EndsWith("-"))
                                    Lucky1 = No1 + Convert.ToInt32(y1);
                                else
                                    Lucky1 = Convert.ToInt32(y1.Remove(y1.Length - 1)) - No1;

                                if (!y2.EndsWith("-"))
                                    Lucky2 = No2 + Convert.ToInt32(y2);
                                else
                                    Lucky2 = Convert.ToInt32(y2.Remove(y2.Length - 1)) - No2;
                                string keyNoms = y1 + " and " + y2;
                                if (y1 == y2)
                                    keyNoms = y1;
                                string pairNo = Lucky1.ToString() + " and " + Lucky2.ToString();
                                if (Lucky1 == Lucky2)
                                    pairNo = Lucky1.ToString();
                                string Nos =  No1.ToString() + " and " + No2.ToString() ;
                                if(No1 == No2)
                                    Nos =  No1.ToString();

                                string strResult = "The Key Numbers " + keyNoms + " when applied to numbers " + Nos + " produces " + pairNo + " as the expected numbers to be played in upcomming game";
                                DataRow rowGroupSummary = dtGroupSummary.NewRow();
                                rowGroupSummary["Id"] = dtGroupSummary.Rows.Count + 1;
                                rowGroupSummary["Database"] = lblInternalDatabaseName.Text;
                                rowGroupSummary["Conclusion"] = strResult;
                                DateTime dtLastDate =Convert.ToDateTime(dtTargetdt.Rows[dtTargetdt.Rows.Count - 1]["Date"]);
                                dtLastDate = dtLastDate.AddDays(7);
                                rowGroupSummary["fDate"] = dtLastDate;
                                rowGroupSummary["Numbers"] = pairNo;
                                dtGroupSummary.Rows.Add(rowGroupSummary);
                            }
                        }
                        i = i + 5;
                        //break;
                    }
                    else
                    {
                        i++;
                    }
                }
                progressBar1.Value = 0;
                if (!MatchFound)
                {
                    DataRow rowGroupSummary1 = dtGroupSummary.NewRow();
                    rowGroupSummary1["Id"] = dtGroupSummary.Rows.Count + 1;
                    rowGroupSummary1["Database"] = lblInternalDatabaseName.Text;
                    rowGroupSummary1["Conclusion"] = "No Result Found To Forcast";
                    dtGroupSummary.Rows.Add(rowGroupSummary1);
                }
                grdSummaryGroupSummation.DataSource = dtGroupSummary;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rdYearPattern_CheckedChanged(object sender, EventArgs e)
        {
            if (rdYearPattern.Checked)
            {
                grdSummaryGroupSummation.Visible = false;
                grdSpanSummary.Visible = false;
                grdSummary.Visible = true;
                searchGrid.Columns["space2"].Visible = false;
            }
            else
            {
                //grdSpanSummary.Visible = true;
                //grdSummary.Visible = false;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtNumFound != null)
                {
                    dtNumFound.Rows.Clear();
                    if(dtNumFoundHistory != null)
                    dtNumFoundHistory.Rows.Clear();
                }
                if (dtSummary != null)
                {
                    dtSummary.Rows.Clear();
                    if(dtSummaryHistory != null)
                    dtSummaryHistory.Rows.Clear();
                }
                dtGroupSummary.Rows.Clear();
                flag = false;
                rdInternal.Checked = true;
                progressBar1.Value = 0;
                LoadDataSet();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void InitialSetting()
        {
            try
            {
                dtGroupSummary = new DataTable();
                dtGroupSummary.Columns.Add("Id",Type.GetType("System.Int32"));
                dtGroupSummary.Columns.Add("Database");
                dtGroupSummary.Columns.Add("Conclusion");
                dtGroupSummary.Columns.Add("fDate");
                dtGroupSummary.Columns.Add("Numbers");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            { 
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                if (rdGroupSummation.Checked)
                {
                    dt = dtGroupSummary.Copy();
                    ds.Tables.Add(dt);
                    ExportToExcel(ds);
                }
                if (rdSpanPattern.Checked || rdSpanMod.Checked || rdSpanCode.Checked)
                {
                    dt = dtSummary.Copy();
                    ds.Tables.Add(dt);
                    ExportToExcel(ds);
                }
                if (rdYearPattern.Checked)
                {
                    dt = dtSummary.Copy();
                    ds.Tables.Add(dt);
                    ExportToExcel(ds);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Export Fail");
            }
        }

        private void ExportToExcel(DataSet ds)
        {
            try
            {
                string file = Application.StartupPath + "\\ReportGroupSummation.xls";

                if (rdSpanPattern.Checked || rdSpanMod.Checked || rdSpanCode.Checked)
                    file = Application.StartupPath + "\\ReportSpanePattern.xls";
                if(rdYearPattern.Checked)
                    file = Application.StartupPath + "\\ReportYearPattern.xls";

                if (System.IO.File.Exists(file))
                    System.IO.File.Delete(file);

                System.IO.FileStream fs = new FileStream(file, FileMode.Create);
                fs.Close();
                System.IO.StreamWriter sw = new StreamWriter(file);
                StringBuilder sb = new StringBuilder();
                if (rdGroupSummation.Checked)
                {
                    sb.Append("Database                                Conclusion");
                    sb.AppendLine();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ////if (Convert.ToInt32(row["Count"]) > 1)
                        {
                            string content = "";
                            foreach (DataColumn dc in ds.Tables[0].Columns)
                            {
                                if (!(dc.Caption == "Id"))
                                {
                                    content += row[dc].ToString();
                                    if (content.Length < 40)
                                    {
                                        int l = 40 - content.Length;
                                        for (int i = 1; i < l; i++)
                                        {
                                            content += " ";
                                        }
                                    }
                                    //sb.Append(content);
                                }
                            }
                            sb.Append(content);
                            sb.AppendLine();
                        }
                    }
                }
                if (rdSpanPattern.Checked || rdSpanMod.Checked || rdSpanCode.Checked)
                {
                     sb.Append("Database                               Key           Relation");
                    sb.AppendLine();
                    int j = 0;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ////if (Convert.ToInt32(row["Count"]) > 1)
                        {
                            string content = "";
                            if(j==0)
                             content = lblInternalDatabaseName.Text;
                            if (content.Length < 40)
                            {
                                int l = 40 - content.Length;
                                for (int i = 1; i < l; i++)
                                {
                                    content += " ";
                                }
                            }
                            foreach (DataColumn dc in ds.Tables[0].Columns)
                            {
                                if (!(dc.Caption == "Id"))
                                {
                                    if (j == 0 || dc.Caption.StartsWith("Rel"))
                                      content += row[dc].ToString();
                                    if (content.Length < 50)
                                    {
                                        int l = 50 - content.Length;
                                        for (int i = 1; i < l; i++)
                                        {
                                            content += " ";
                                        }
                                    }
                                    //sb.Append(content);
                                }
                            }
                            sb.Append(content);
                            sb.AppendLine();
                        }
                        j++;
                    }
                }
                else
                {
                    sb.Append("Year          Pair Num Found At  Search No Found At           Database");
                    sb.AppendLine();
                    int j = 0;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ////if (Convert.ToInt32(row["Count"]) > 1)
                        {
                            string content = "";
                            int lengh = 20;
                            foreach (DataColumn dc in ds.Tables[0].Columns)
                            {
                                if (!(dc.Caption == "Id"))
                                {
                                    content += row[dc].ToString();
                                    if (content.Length < lengh)
                                    {
                                        int l = lengh - content.Length;
                                        for (int i = 1; i < l; i++)
                                        {
                                            content += " ";
                                        }
                                    }
                                    lengh += 20;
                                    //sb.Append(content);
                                }
                            }
                            sb.Append(content);
                            sb.AppendLine();
                        }
                        j++;
                    }
                }
                sw.Write(sb);
                sw.Close();
                sw.Dispose();
                MessageBox.Show("Export Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Export Fail");
            }
        }

        private void rdInternal_CheckedChanged(object sender, EventArgs e)
        {
            if (rdInternal.Checked)
            {
                LoadDataSet();
            }
            else
            {
                ds.Tables[0].Rows.Clear();
                dtGroupSummary.Rows.Clear();
                if(dtNumFound != null)
                dtNumFound.Rows.Clear();
            }
        }
       
        private void GroupSummationExternal()
        {
            try
            {
                dtNumFound = new DataTable();
                dtNumFound.Columns.Add("Id");
                dtNumFound.Columns.Add("col");
                dtNumFound.Columns.Add("SNo");
                dtNumFound.Columns.Add("RecNo", Type.GetType("System.Int32"));
                dtNumFound.Columns.Add("flag");
               if(!ds.Tables[0].Columns.Contains("RecNo"))
                  ds.Tables[0].Columns.Add("RecNo", Type.GetType("System.Int32"));
               if (!ds.Tables[0].Columns.Contains("DBName"))
                   ds.Tables[0].Columns.Add("DBName");
                   dtNumFound.Rows.Clear();
                    dtGroupSummary.Rows.Clear();
                DataTable dtExterList = SqlClass.GetExternalDatabaseList();
                progressBar1.Value = 0;
                progressBar1.Maximum = dtExterList.Rows.Count;
                foreach(DataRow rowEx in dtExterList.Rows)
                {
                    progressBar1.Value = progressBar1.Value + 1;
                    int dbId = (int)rowEx["DBId"];
                    DataSet dsEx = new DataSet();
                    SqlClass.GetWin_Machin_DataByDBId(dbId,ref dsEx);
                    DataTable dtTargetdt = dsEx.Tables[0].Copy();
                    int count = dtTargetdt.Rows.Count;
                    int last4 = 0;
                    for (int cnt = count-1; cnt >= 0; cnt--)
                    {
                        while((cnt >= 0) &&( (last4 == 4 ||  (dtTargetdt.Rows[cnt]["W1"] == null || dtTargetdt.Rows[cnt]["W1"] == DBNull.Value))))
                        {
                            dtTargetdt.Rows[cnt].Delete();
                            dtTargetdt.AcceptChanges();
                            count = count - 1;
                            cnt--;
                        }
                        last4++; 

                    }
                    int noCount = 0;
                    bool found1 = false;
                    bool found2 = false;
                    string x1 = "", y1 = "", x2 = "", y2 = "";
                    
                    DataTable dtTemp = dtNumFound.Clone();
                    bool MatchFound = false;
                    int No1 = 0, No2 = 0;
                   
                    for (int i = 0; i < count - 3; )
                    {
                        
                        noCount = 0;
                        found1 = false;
                        found2 = false;
                        dtTemp.Rows.Clear();
                        foreach (DataColumn dc1 in dtTargetdt.Columns)
                        {
                            found1 = false;
                            if (dc1.Caption.StartsWith("W"))
                            {
                                foreach (DataColumn dc2 in dtTargetdt.Columns)
                                {
                                    if (dc2.Caption.StartsWith("W"))
                                    {
                                        int[] X = new int[3];
                                        X[0] = Convert.ToInt32(dtTargetdt.Rows[i][dc1]) - Convert.ToInt32(dtTargetdt.Rows[i + 1][dc2]);
                                        X[1] = Convert.ToInt32(dtTargetdt.Rows[i][dc1]) + Convert.ToInt32(dtTargetdt.Rows[i + 1][dc2]);
                                        X[2] = -Convert.ToInt32(dtTargetdt.Rows[i][dc1]) + Convert.ToInt32(dtTargetdt.Rows[i + 1][dc2]);
                                        for (int j = 0; j < 3; j++)
                                        {

                                            foreach (DataColumn dc3 in dtTargetdt.Columns)
                                            {
                                                if (dc3.Caption.StartsWith("W"))
                                                {
                                                    int[] Y = new int[3];
                                                    Y[0] = Convert.ToInt32(dtTargetdt.Rows[i + 1][dc2]) - Convert.ToInt32(dtTargetdt.Rows[i + 2][dc3]);
                                                    Y[1] = Convert.ToInt32(dtTargetdt.Rows[i + 1][dc2]) + Convert.ToInt32(dtTargetdt.Rows[i + 2][dc3]);
                                                    Y[2] = -Convert.ToInt32(dtTargetdt.Rows[i + 1][dc2]) + Convert.ToInt32(dtTargetdt.Rows[i + 2][dc3]);

                                                    for (int k = 0; k < 3; k++)
                                                    {
                                                        foreach (DataColumn dc4 in dtTargetdt.Columns)
                                                        {
                                                            if (dc4.Caption.StartsWith("W"))
                                                            {
                                                                int T2 = Convert.ToInt32(dtTargetdt.Rows[i + 2][dc3]);
                                                                int T3 = Convert.ToInt32(dtTargetdt.Rows[i + 3][dc4]);
                                                                if ((j == 0 && T3 == T2 - X[j]) || (j == 1 && T3 == X[j] - T2) || (j == 2 && T3 == T2 + X[j]))
                                                                {
                                                                    foreach (DataColumn dc5 in dtTargetdt.Columns)
                                                                    {
                                                                        if (dc5.Caption.StartsWith("W"))
                                                                        {
                                                                            int T4 = 0;
                                                                            if (i + 4 != count)
                                                                                T4 = Convert.ToInt32(dtTargetdt.Rows[i + 4][dc5]);
                                                                            if (T4 == 0 || (k == 0 && T4 == T3 - Y[k]) || (k == 1 && T4 == Y[k] - T3) || (k == 2 && T4 == T3 + Y[k]))
                                                                            {
                                                                                if (T4 == 0)
                                                                                {
                                                                                    int key = T3 - Y[k];
                                                                                    if (k == 1)
                                                                                        key = Y[k] - T3;
                                                                                    if (k == 2)
                                                                                        key = T3 + Y[k];
                                                                                    if (key < 1 || key > 90)
                                                                                        continue;
                                                                                }

                                                                                noCount += 1;
                                                                                found1 = true;
                                                                                if (noCount == 2)
                                                                                {
                                                                                    found2 = true;
                                                                                    if (j == 0)
                                                                                        x2 = (-X[j]).ToString();
                                                                                    else if (j == 1)
                                                                                        x2 = X[j].ToString() + "-";
                                                                                    else
                                                                                        x2 = X[j].ToString();

                                                                                    if (k == 0)
                                                                                        y2 = (-Y[k]).ToString();
                                                                                    else if (k == 1)
                                                                                        y2 = Y[k].ToString() + "-";
                                                                                    else
                                                                                        y2 = Y[k].ToString();
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (j == 0)
                                                                                        x1 = (-X[j]).ToString();
                                                                                    else if (j == 1)
                                                                                        x1 = X[j].ToString() + "-";
                                                                                    else
                                                                                        x1 = X[j].ToString();

                                                                                    if (k == 0)
                                                                                        y1 = (-Y[k]).ToString();
                                                                                    else if (k == 1)
                                                                                        y1 = Y[k].ToString() + "-";
                                                                                    else
                                                                                        y1 = Y[k].ToString();
                                                                                }
                                                                                DataRow row1 = dtTemp.NewRow();
                                                                                row1["Id"] = dtTemp.Rows.Count + 1;
                                                                                row1["col"] = dc1.Caption;
                                                                                row1["SNo"] = dtTargetdt.Rows[i]["SNo"];
                                                                                row1["flag"] = "S";
                                                                                if (noCount == 2)
                                                                                    row1["flag"] = "N";
                                                                                dtTemp.Rows.Add(row1);

                                                                                DataRow row2 = dtTemp.NewRow();
                                                                                row2["Id"] = dtTemp.Rows.Count + 1;
                                                                                row2["col"] = dc2.Caption;
                                                                                row2["SNo"] = dtTargetdt.Rows[i + 1]["SNo"];
                                                                                row2["flag"] = "S";
                                                                                if (noCount == 2)
                                                                                    row2["flag"] = "N";
                                                                                dtTemp.Rows.Add(row2);


                                                                                DataRow row3 = dtTemp.NewRow();
                                                                                row3["Id"] = dtTemp.Rows.Count + 1;
                                                                                row3["col"] = dc3.Caption;
                                                                                row3["SNo"] = dtTargetdt.Rows[i + 2]["SNo"];
                                                                                row3["flag"] = "S";
                                                                                if (noCount == 2)
                                                                                    row3["flag"] = "N";
                                                                                dtTemp.Rows.Add(row3);

                                                                                DataRow row4 = dtTemp.NewRow();
                                                                                row4["Id"] = dtTemp.Rows.Count + 1;
                                                                                row4["col"] = dc4.Caption;
                                                                                row4["SNo"] = dtTargetdt.Rows[i + 3]["SNo"];
                                                                                row4["flag"] = "S";
                                                                                if (noCount == 2)
                                                                                    row4["flag"] = "N";
                                                                                dtTemp.Rows.Add(row4);
                                                                                if (T4 != 0)
                                                                                {
                                                                                    DataRow row5 = dtTemp.NewRow();
                                                                                    row5["Id"] = dtTemp.Rows.Count + 1;
                                                                                    row5["col"] = dc5.Caption;
                                                                                    row5["SNo"] = dtTargetdt.Rows[i + 4]["SNo"];
                                                                                    row5["flag"] = "S";
                                                                                    if (noCount == 2)
                                                                                        row5["flag"] = "N";
                                                                                    dtTemp.Rows.Add(row5);
                                                                                }

                                                                                DataRow row6 = dtTemp.NewRow();
                                                                                row6["Id"] = dtTemp.Rows.Count + 1;
                                                                                row6["col"] = "space1";
                                                                                row6["SNo"] = dtTargetdt.Rows[i]["SNo"];
                                                                                row6["flag"] = "S";
                                                                                if (noCount == 2)
                                                                                {
                                                                                    row6["flag"] = "N";
                                                                                    row6["col"] = "space2";
                                                                                }
                                                                                dtTemp.Rows.Add(row6);

                                                                                DataRow row7 = dtTemp.NewRow();
                                                                                row7["Id"] = dtTemp.Rows.Count + 1;
                                                                                row7["col"] = "space1";
                                                                                row7["SNo"] = dtTargetdt.Rows[i + 1]["SNo"];
                                                                                row7["flag"] = "S";
                                                                                if (noCount == 2)
                                                                                {
                                                                                    row7["col"] = "space2";
                                                                                    row7["flag"] = "N";
                                                                                }
                                                                                dtTemp.Rows.Add(row7);


                                                                                DataRow row8 = dtTemp.NewRow();
                                                                                row8["Id"] = dtTemp.Rows.Count + 1;
                                                                                row8["col"] = "space1";
                                                                                row8["SNo"] = dtTargetdt.Rows[i + 2]["SNo"];
                                                                                row8["flag"] = "S";
                                                                                if (noCount == 2)
                                                                                {
                                                                                    row8["flag"] = "N";
                                                                                    row8["col"] = "space2";
                                                                                }
                                                                                dtTemp.Rows.Add(row8);

                                                                                DataRow row9 = dtTemp.NewRow();
                                                                                row9["Id"] = dtTemp.Rows.Count + 1;
                                                                                row9["col"] = "space1";
                                                                                row9["SNo"] = dtTargetdt.Rows[i + 3]["SNo"];
                                                                                row9["flag"] = "S";
                                                                                if (noCount == 2)
                                                                                {
                                                                                    row9["flag"] = "N";
                                                                                    row9["col"] = "space2";
                                                                                }
                                                                                dtTemp.Rows.Add(row9);

                                                                                if (T4 == 0)
                                                                                {
                                                                                    MatchFound = true;
                                                                                    if (noCount == 1)
                                                                                        No1 = T3;
                                                                                    else
                                                                                        No2 = T3;
                                                                                    break;

                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                            }
                                                            if (found1)
                                                                break;
                                                        }
                                                    }
                                                }
                                                if (found1)
                                                    break;
                                            }
                                            if (found1)
                                                break;
                                        }

                                    }
                                    if (found1)
                                        break;
                                }

                            }
                            if (found2)
                                break;
                        }
                        if (found2)
                        {
                            int MaxRecNo = 1;
                            DataRow[] rowMax = dtNumFound.Select("RecNo = MAX(RecNo)");
                            if (rowMax.Length > 0)
                                MaxRecNo = (int)rowMax[0]["RecNo"] + 1;
                            foreach (DataRow rowTemp in dtTemp.Rows)
                            {
                                DataRow rowNumFound = dtNumFound.NewRow();
                                rowNumFound["Id"] = dtNumFound.Rows.Count + 1;
                                rowNumFound["col"] = rowTemp["col"];
                                rowNumFound["SNo"] = rowTemp["SNo"];
                                rowNumFound["RecNo"] = MaxRecNo;
                                rowNumFound["flag"] = rowTemp["flag"];
                                dtNumFound.Rows.Add(rowNumFound);
                            }
                            foreach (DataRow rExternal in dtTargetdt.Rows)
                            {
                                DataRow rowFinal = ds.Tables[0].NewRow();
                                foreach (DataColumn dcEx in dtTargetdt.Columns)
                                {
                                    rowFinal[dcEx.Caption] = rExternal[dcEx];
                                }
                                rowFinal["RecNo"] = MaxRecNo;
                                rowFinal["DBName"] = SqlClass.GetDBNameById(dbId);
                                ds.Tables[0].Rows.Add(rowFinal);
                            }
                            DataRow rowFinalBlank = ds.Tables[0].NewRow();
                            ds.Tables[0].Rows.Add(rowFinalBlank);

                            int startSNo = Convert.ToInt32(dtTemp.Rows[0]["SNo"]);
                            int endSNo = Convert.ToInt32(dtTemp.Rows[3]["SNo"]);
                            DataRow[] rowF = ds.Tables[0].Select("DBId = "+ dbId +" AND   SNo > " + (startSNo - 1).ToString() + " AND SNo < " + (endSNo + 1).ToString());
                            if (rowF.Length > 0)
                            {
                                int l = 0;
                                if (!ds.Tables[0].Columns.Contains("space1"))
                                    ds.Tables[0].Columns.Add("space1");
                                if (!ds.Tables[0].Columns.Contains("space2"))
                                    ds.Tables[0].Columns.Add("space2");
                                foreach (DataRow rowf in rowF)
                                {
                                    if (rowf["W1"] != DBNull.Value)
                                    {
                                        if (l == 0)
                                        {
                                            rowf["space1"] = x1;
                                            rowf["space2"] = x2;
                                            l = 1;
                                        }
                                        else
                                        {
                                            rowf["space1"] = y1;
                                            rowf["space2"] = y2;
                                            l = 0;
                                        }
                                    }
                                }
                                if (MatchFound)
                                {
                                    int Lucky1, Lucky2;
                                    if (!y1.EndsWith("-"))
                                        Lucky1 = No1 + Convert.ToInt32(y1);
                                    else
                                        Lucky1 = Convert.ToInt32(y1.Remove(y1.Length - 1)) - No1;

                                    if (!y2.EndsWith("-"))
                                        Lucky2 = No2 + Convert.ToInt32(y2);
                                    else
                                        Lucky2 = Convert.ToInt32(y2.Remove(y2.Length - 1)) - No2;
                                    string keyNoms = y1 + " and " + y2;
                                    if (y1 == y2)
                                        keyNoms = y1;
                                    string pairNo = Lucky1.ToString() + " and " + Lucky2.ToString();
                                    if (Lucky1 == Lucky2)
                                        pairNo = Lucky1.ToString();
                                    string Nos = No1.ToString() + " and " + No2.ToString();
                                    if (No1 == No2)
                                        Nos = No1.ToString();

                                    string strResult = "The Key Numbers " + keyNoms + " when applied to numbers " + Nos + " produces " + pairNo + " as the expected numbers to be played in upcomming game";
                                    DataRow rowGroupSummary = dtGroupSummary.NewRow();
                                    rowGroupSummary["Id"] = dtGroupSummary.Rows.Count + 1;
                                    rowGroupSummary["Database"] = SqlClass.GetDBNameById(dbId);
                                    rowGroupSummary["Conclusion"] = strResult;
                                    DateTime dtLastDate = Convert.ToDateTime(dtTargetdt.Rows[dtTargetdt.Rows.Count - 1]["Date"]);
                                    dtLastDate = dtLastDate.AddDays(7);
                                    rowGroupSummary["fDate"] = dtLastDate;
                                    rowGroupSummary["Numbers"] = pairNo;
                                    dtGroupSummary.Rows.Add(rowGroupSummary);
                                }
                                else
                                {
                                    DataRow rowGroupSummary1 = dtGroupSummary.NewRow();
                                    rowGroupSummary1["Id"] = dtGroupSummary.Rows.Count + 1;
                                    rowGroupSummary1["Database"] = SqlClass.GetDBNameById(dbId);
                                    rowGroupSummary1["Conclusion"] = "No Result Found To Forcast";
                                    dtGroupSummary.Rows.Add(rowGroupSummary1);
                                }
                            }

                            
                        }
                       break;
                    }
                    
                }
                progressBar1.Value = 0;
                grdSummaryGroupSummation.DataSource = dtGroupSummary;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SpanPatternSearch(DataTable dtTargetdt)
        {
            int error = 0;
            try
            {


                #region r0
                ArrayList arRel = new ArrayList();
                arRel.Add("RN");
                arRel.Add("CN");
                arRel.Add("TN");
                arRel.Add("TCN");
                string[] dcColumn = { "W2", "W3", "W4", "W1", "W5" };
                ArrayList arNumbers = new ArrayList();//collectin of key numbers 
                
                progressBar1.Value = 0;
                bool bFlage = false;
                ArrayList KeyNumbers = new ArrayList();
                int RecNo = 1;
                string strKeyNumbers = "";
                int PatterLength = 15;
              progressBar1.Maximum =5 * PatterLength;
              #endregion
                foreach (string dc in dcColumn)
                {
                    
                    int keyNo;
                   arNumbers.Clear(); 
                    
                    for (int i = dtTargetdt.Rows.Count - PatterLength; i < dtTargetdt.Rows.Count; i++)
                    {
                        bFlage = false;
                        dtSummary.Rows.Clear();
                        dtNumFound.Rows.Clear();
                        #region r1
                         keyNo = Convert.ToInt32(dtTargetdt.Rows[i][dc]);
                        foreach (object oNo in arNumbers)
                        {
                            if (keyNo == Convert.ToInt32(oNo))
                            {
                              keyNo = -1;
                                break;
                            }
                        }
                        if (keyNo == -1)//No already checked
                            continue;

                        DataRow[] rowForcast = dtSpanForcast.Select("KeyNum = '" +keyNo.ToString() + "' and Col = '" + dc + "'");
                        if (rowForcast.Length > 0)
                            continue;

                        DataRow[] keyRows = dtTargetdt.Select(dc + " =" + keyNo.ToString());
                        //if (keyRows.Length < 2 || keyRows.Length > 4)//No need to check for pattern
                        //    continue;
                        if (keyRows.Length % 4 == 1)
                            continue;
                        else
                        {
                            int mod = 4;
                            if (keyRows.Length % 4 != 0)
                                mod = keyRows.Length % 4;
                            keyRows = dtTargetdt.Select(dc + " =" + keyNo.ToString() + " and SNo >= " + keyRows[keyRows.Length - mod]["SNo"].ToString());
                        }


#endregion
                        progressBar1.Value += 1;
                       
                        //for (int PatterLength = 3; PatterLength <= 7; PatterLength++)//search for each pattern length
                        //{
                           
                            for (int k = 1; k < PatterLength - 1; k++)
                            { 
                                bool relMatch = false;
                                foreach (string dc1 in dcColumn)
                                {
                                    for (int l = k + 1; l < PatterLength; l++)
                                    {
                                        #region r3
                                        //if (!relMatch)//check for arithmatic
                                        {
                                            
                                            string[] ops = { "+", "-" };
                                            foreach (string op in ops)
                                            {
                                                int[] NoList = new int[5];
                                                string[] Rels = new string[5];
                                                int ind = 0;
                                                int DrawNo0 = Convert.ToInt32(keyRows[0]["SNo"]);
                                                DataRow[] rFindIndex = dtTargetdt.Select("SNo < " + DrawNo0.ToString());
                                                DrawNo0 = rFindIndex.Length;
                                                if (DrawNo0 + l >= dtTargetdt.Rows.Count)
                                                    continue;
                                                foreach (string dc2 in dcColumn)
                                                {
                                                    int No1 = Convert.ToInt32(dtTargetdt.Rows[DrawNo0 + k][dc1]);
                                                    int No2 = Convert.ToInt32(dtTargetdt.Rows[DrawNo0 + l][dc2]);
                                                    NoList[ind] = No1 + No2;
                                                    Rels[ind] = No1.ToString() + " + " + No2.ToString() + " = " + NoList[ind].ToString() + " at DrawNo " + dtTargetdt.Rows[DrawNo0 + k]["SNo"].ToString() + " and " + dtTargetdt.Rows[DrawNo0 + l]["SNo"].ToString();
                                                    if (op == "-")
                                                    {
                                                        NoList[ind] = No1 - No2;
                                                        Rels[ind] = No1.ToString() + " - " + No2.ToString() + " = " + NoList[ind].ToString() + " at DrawNo " + dtTargetdt.Rows[DrawNo0 + k]["SNo"].ToString() + " and " + dtTargetdt.Rows[DrawNo0 + l]["SNo"].ToString();
                                                    }
                                                    ind++;
                                                }
                                                ind = 0;
                                                for (int rInd = 0; rInd < 5; rInd++)
                                                {
                                                    ArrayList oArrList = new ArrayList();
                                                    ArrayList Col1 = new ArrayList();
                                                    ArrayList Col2 = new ArrayList();
                                                    ind = 0;
                                                    string Relation = Rels[rInd] + ", ";
                                                    string skipIds = "";
                                                    foreach (DataRow row in keyRows)
                                                    {
                                                        int DrawNo = Convert.ToInt32(row["SNo"]);
                                                        DataRow[] rFindIndex1 = dtTargetdt.Select("SNo < " + DrawNo.ToString());
                                                        DrawNo = rFindIndex1.Length;
                                                        relMatch = false;
                                                        //if (DrawNo + l >= dtTargetdt.Rows.Count)
                                                        //    break;
                                                        ind++;
                                                        
                                                        if (DrawNo + l >= dtTargetdt.Rows.Count)
                                                        {
                                                            if (ind == keyRows.Length && 2 < keyRows.Length)
                                                            {
                                                                bFlage = true;
                                                                relMatch = true;
                                                                #region add new row to forcast table
                                                                if (DrawNo + k < dtTargetdt.Rows.Count)
                                                                {
                                                                    int n1 = Convert.ToInt32(dtTargetdt.Rows[DrawNo + k][dc1]);
                                                                    int n2 = NoList[rInd] - n1;
                                                                    if (op == "-")
                                                                        n2 = n1 - NoList[rInd];
                                                                    //if (n2 > 0 && n2 < 91)
                                                                    {

                                                                        int m = l - k;
                                                                        int n = dtTargetdt.Rows.Count - 1 - (DrawNo + k);
                                                                        int p = m - n;
                                                                        DateTime lastDate = Convert.ToDateTime(dtTargetdt.Rows[dtTargetdt.Rows.Count - 1]["Date"]);
                                                                        lastDate = lastDate.AddDays(7 * p);
                                                                        DataRow rForcast = dtSpanForcast.NewRow();
                                                                        rForcast["fDate"] = lastDate;
                                                                        rForcast["Numbers"] = n2;
                                                                        rForcast["KeyNum"] = keyNo;
                                                                        rForcast["Col"] = dc;
                                                                        rForcast["DBName"] = lblInternalDatabaseName.Text;
                                                                        rForcast["Method"] = "SPAN PATTERN ARITHMATIC";
                                                                        dtSpanForcast.Rows.Add(rForcast);
                                                                    }
                                                                }
                                                                #endregion
                                                            }
                                                            skipIds += (ind - 1).ToString() + ",";
                                                            break;
                                                        }
                                               

                                                        if (ind == 1)
                                                            continue;
                                                      

                                                        foreach (string dc2 in dcColumn)
                                                        {
                                                            int No1 = Convert.ToInt32(dtTargetdt.Rows[DrawNo + k][dc1]);
                                                            int No2 = Convert.ToInt32(dtTargetdt.Rows[DrawNo + l][dc2]);
                                                            int Sum = No1 + No2;
                                                            if (op == "-")
                                                                Sum = No1 - No2;
                                                            if (Sum == NoList[rInd])
                                                            {
                                                                Relation += No1.ToString() + " " + op + " " + No2.ToString() + " = " + NoList[rInd].ToString() + " at DrawNo " + dtTargetdt.Rows[DrawNo + k]["SNo"].ToString() + " and " + dtTargetdt.Rows[DrawNo + l]["SNo"].ToString() + " , ";
                                                                oArrList.Add(No1);
                                                                Col1.Add(dc1);
                                                                Col2.Add(dc2);
                                                                relMatch = true;

                                                                break;
                                                            }
                                                            foreach (object oNo1 in oArrList)
                                                            {
                                                                if (No1 == (int)oNo1)
                                                                {
                                                                    relMatch = true;
                                                                    skipIds +=  (ind-1).ToString()+ ",";
                                                                    break;
                                                                }
                                                            }
                                                            if (relMatch)
                                                                break;
                                                        }
                                                        if (!relMatch)
                                                            break;
                                                    }
                                                    if (relMatch)
                                                    {
                                                        DataRow rowSummary = dtSummary.NewRow();
                                                        rowSummary["Id"] = dtSummary.Rows.Count + 1;
                                                        rowSummary["KeyNumbers"] = keyNo;
                                                        rowSummary["Relation"] = Relation;
                                                        rowSummary["RecNo"] = RecNo;
                                                        dtSummary.Rows.Add(rowSummary);
                                                        int colId = 0;
                                                        for (int ix = 0; ix < keyRows.Length; ix++)
                                                        {
                                                            int DrawNo = Convert.ToInt32(keyRows[ix]["SNo"]);
                                                            DataRow[] rFindIndex1 = dtTargetdt.Select("SNo < " + DrawNo.ToString());
                                                            DrawNo = rFindIndex1.Length;
                                                            if (DrawNo + k >= dtTargetdt.Rows.Count)
                                                                continue;
                                                                DataRow row3 = dtNumFound.NewRow();
                                                                row3["Id"] = dtNumFound.Rows.Count + 1;
                                                                row3["col"] = dc1;
                                                                row3["SNo"] = dtTargetdt.Rows[DrawNo + k]["SNo"];//Convert.ToInt32(keyRows[ix]["SNo"]) + k;
                                                                row3["flag"] = "P";
                                                                row3["RecNo"] =RecNo;
                                                                dtNumFound.Rows.Add(row3);

                                                                if (DrawNo + l >= dtTargetdt.Rows.Count)
                                                                    continue;
                                                                if (skipIds.Contains(ix.ToString()))
                                                                    continue;
                                                                DataRow row4 = dtNumFound.NewRow();
                                                                row4["Id"] = dtNumFound.Rows.Count + 1;
                                                                if (ix == 0)
                                                                    row4["col"] = dcColumn[rInd];
                                                                else
                                                                {
                                                                    row4["col"] = Col2[colId].ToString();
                                                                    colId++;
                                                                }
                                                                row4["RecNo"] = RecNo;
                                                                row4["SNo"] = dtTargetdt.Rows[DrawNo + l]["SNo"];//Convert.ToInt32(keyRows[ix]["SNo"]) + l;
                                                                row4["flag"] = "B";
                                                                dtNumFound.Rows.Add(row4);
                                                            
                                                        }
                                                        break;
                                                    }
                                                }
                                                if (relMatch)
                                                    break;
                                            }
                                        }// 
                                        #endregion

                                        if (relMatch)
                                            break;

                                    }//end for l
                                    if (relMatch)
                                        break;
                                }//end for dc1
                                
                            }//end for k
                        //}

                        #region r4
                            if (dtNumFound.Rows.Count > 0 && bFlage)
                            {
                                for (int ix = 0; ix < keyRows.Length; ix++)
                                {
                                    DataRow row3 = dtNumFound.NewRow();
                                    row3["Id"] = dtNumFound.Rows.Count + 1;
                                    row3["col"] = dc;
                                    row3["SNo"] = Convert.ToInt32(keyRows[ix]["SNo"]);// dtTargetdt.Rows[j + k]["SNo"];
                                    row3["flag"] = "K";
                                    row3["RecNo"] = RecNo;
                                    dtNumFound.Rows.Add(row3);
                                }
                                dtNumFoundHistory.Merge(dtNumFound);
                                dtSummaryHistory.Merge(dtSummary);
                                KeyNumbers.Add(keyNo);
                                strKeyNumbers += keyNo.ToString() + " ,";
                                RecNo++;
                                //break;
                            }
                            else
                            {
                                dtSummary.Rows.Clear();
                                dtNumFound.Rows.Clear();
                            }
                            
                        #endregion

                    }//end for i
                    
                }//end for dc
                progressBar1.Value = 35;
                if (dtNumFoundHistory.Rows.Count == 0)
                    MessageBox.Show("No Match Found");
                else
                    MessageBox.Show("RESULT : "+ KeyNumbers.Count.ToString() + " key numbers found " + strKeyNumbers);
                dtSummary.Rows.Clear();
                dtNumFound.Rows.Clear();

                dtNumFoundHistory.DefaultView.RowFilter = "RecNo = MIN(RecNo)";
                dtNumFound.Merge(dtNumFoundHistory.DefaultView.ToTable());

                dtSummaryHistory.DefaultView.RowFilter = "RecNo = MIN(RecNo)";
                dtSummary.Merge(dtSummaryHistory.DefaultView.ToTable());

                grdSpanSummary.DataSource = dtSummary;
                progressBar1.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SpanPatternSearchMachine(DataTable dtTargetdt)
        {
            int error = 0;
            try
            {


                #region r0
                ArrayList arRel = new ArrayList();
                arRel.Add("RN");
                arRel.Add("CN");
                arRel.Add("TN");
                arRel.Add("TCN");
                string[] dcColumnW = { "W2", "W3", "W4", "W1", "W5" };
                string[] dcColumn = { "M2", "M3", "M4", "M1", "M5" };
                ArrayList arNumbers = new ArrayList();//collectin of key numbers 
                
                progressBar1.Value = 0;
                bool bFlage = false;
                ArrayList KeyNumbers = new ArrayList();
                int RecNo = 1;
                string strKeyNumbers = "";
                int PatterLength = 15;
                progressBar1.Maximum = 5 * PatterLength;
                #endregion
                //for (int i = dtTargetdt.Rows.Count - PatterLength; i < dtTargetdt.Rows.Count; i++)
                //{
                //    foreach (string dc in dcColumn)
                //    {
                //        dtTargetdt.Rows[i][dc] = dtTargetdt.Rows[i][dc.Replace('M', 'W')];
                //    }
                //}
                foreach (string dc in dcColumn)
                {

                    int keyNo;
                    arNumbers.Clear();
                    
                    for (int i = dtTargetdt.Rows.Count - PatterLength; i < dtTargetdt.Rows.Count; i++)
                    {
                        bFlage = false;
                        dtSummary.Rows.Clear();
                        dtNumFound.Rows.Clear();
                        #region r1
                        keyNo = Convert.ToInt32(dtTargetdt.Rows[i][dc]);
                        foreach (object oNo in arNumbers)
                        {
                            if (keyNo == Convert.ToInt32(oNo))
                            {
                                keyNo = -1;
                                break;
                            }
                        }
                        if (keyNo == -1)//No already checked
                            continue;

                        DataRow[] rowForcast = dtSpanForcast.Select("KeyNum = '" + keyNo.ToString() + "' and Col = '" + dc + "'");
                        if (rowForcast.Length > 0)
                            continue;

                        DataRow[] keyRows = dtTargetdt.Select(dc + " =" + keyNo.ToString());
                        //if (keyRows.Length < 2 || keyRows.Length > 4)//No need to check for pattern
                        //    continue;
                        if (keyRows.Length % 4 == 1)
                            continue;
                        else
                        {
                            int mod = 4;
                            if (keyRows.Length % 4 != 0)
                                mod = keyRows.Length % 4;
                            keyRows = dtTargetdt.Select(dc + " =" + keyNo.ToString() + " and SNo >= " + keyRows[keyRows.Length - mod]["SNo"].ToString());
                        }


                        #endregion
                        progressBar1.Value += 1;

                        //for (int PatterLength = 3; PatterLength <= 7; PatterLength++)//search for each pattern length
                        //{

                        for (int k = 1; k < PatterLength - 1; k++)
                        {
                            bool relMatch = false;
                            foreach (string dc1 in dcColumn)
                            {
                                for (int l = k + 1; l < PatterLength; l++)
                                {
                                    #region r3
                                    //if (!relMatch)//check for arithmatic
                                    {

                                        string[] ops = { "+", "-" };
                                        foreach (string op in ops)
                                        {
                                            int[] NoList = new int[5];
                                            string[] Rels = new string[5];
                                            int ind = 0;
                                            int DrawNo0 = Convert.ToInt32(keyRows[0]["SNo"]);
                                            DataRow[] rFindIndex = dtTargetdt.Select("SNo < " + DrawNo0.ToString());
                                            DrawNo0 = rFindIndex.Length;
                                            if (DrawNo0 + l >= dtTargetdt.Rows.Count)
                                                continue;
                                            foreach (string dc2 in dcColumnW)
                                            {
                                                int No1 = Convert.ToInt32(dtTargetdt.Rows[DrawNo0 + k][dc1]);
                                                int No2 = Convert.ToInt32(dtTargetdt.Rows[DrawNo0 + l][dc2]);
                                                NoList[ind] = No1 + No2;
                                                Rels[ind] = No1.ToString() + " + " + No2.ToString() + " = " + NoList[ind].ToString() + " at DrawNo " + dtTargetdt.Rows[DrawNo0 + k]["SNo"].ToString() + " and " + dtTargetdt.Rows[DrawNo0 + l]["SNo"].ToString();
                                                if (op == "-")
                                                {
                                                    NoList[ind] = No1 - No2;
                                                    Rels[ind] = No1.ToString() + " - " + No2.ToString() + " = " + NoList[ind].ToString() + " at DrawNo " + dtTargetdt.Rows[DrawNo0 + k]["SNo"].ToString() + " and " + dtTargetdt.Rows[DrawNo0 + l]["SNo"].ToString();
                                                }
                                                ind++;
                                            }
                                            ind = 0;
                                            for (int rInd = 0; rInd < 5; rInd++)
                                            {
                                                ArrayList oArrList = new ArrayList();
                                                ArrayList Col1 = new ArrayList();
                                                ArrayList Col2 = new ArrayList();
                                                ind = 0;
                                                string Relation = Rels[rInd] + ", ";
                                                string skipIds = "";
                                                foreach (DataRow row in keyRows)
                                                {
                                                    int DrawNo = Convert.ToInt32(row["SNo"]);
                                                    DataRow[] rFindIndex1 = dtTargetdt.Select("SNo < " + DrawNo.ToString());
                                                    DrawNo = rFindIndex1.Length;
                                                    relMatch = false;
                                                    //if (DrawNo + l >= dtTargetdt.Rows.Count)
                                                    //    break;
                                                    ind++;

                                                    if (DrawNo + l >= dtTargetdt.Rows.Count)
                                                    {
                                                        if (ind == keyRows.Length && 2 < keyRows.Length)
                                                        {
                                                            bFlage = true;
                                                            relMatch = true;
                                                            #region add new row to forcast table
                                                            if (DrawNo + k < dtTargetdt.Rows.Count)
                                                            {
                                                                int n1 = Convert.ToInt32(dtTargetdt.Rows[DrawNo + k][dc1]);
                                                                int n2 = NoList[rInd] - n1;
                                                                if (op == "-")
                                                                    n2 = n1 - NoList[rInd];
                                                                //if (n2 > 0 && n2 < 91)
                                                                {

                                                                    int m = l - k;
                                                                    int n = dtTargetdt.Rows.Count - 1 - (DrawNo + k);
                                                                    int p = m - n;
                                                                    DateTime lastDate = Convert.ToDateTime(dtTargetdt.Rows[dtTargetdt.Rows.Count - 1]["Date"]);
                                                                    lastDate = lastDate.AddDays(7 * p);
                                                                    DataRow rForcast = dtSpanForcast.NewRow();
                                                                    rForcast["fDate"] = lastDate;
                                                                    rForcast["Numbers"] = n2;
                                                                    rForcast["KeyNum"] = keyNo;
                                                                    rForcast["Col"] = dc;
                                                                    rForcast["DBName"] = lblInternalDatabaseName.Text;
                                                                    rForcast["Method"] = "SPAN PATTERN ARITHMATIC";
                                                                    dtSpanForcast.Rows.Add(rForcast);
                                                                }
                                                            }
                                                            #endregion
                                                        }
                                                        skipIds += (ind - 1).ToString() + ",";
                                                        break;
                                                    }


                                                    if (ind == 1)
                                                        continue;


                                                    foreach (string dc2 in dcColumnW)
                                                    {
                                                        int No1 = Convert.ToInt32(dtTargetdt.Rows[DrawNo + k][dc1]);
                                                        int No2 = Convert.ToInt32(dtTargetdt.Rows[DrawNo + l][dc2]);
                                                        int Sum = No1 + No2;
                                                        if (op == "-")
                                                            Sum = No1 - No2;
                                                        if (Sum == NoList[rInd])
                                                        {
                                                            Relation += No1.ToString() + " " + op + " " + No2.ToString() + " = " + NoList[rInd].ToString() + " at DrawNo " + dtTargetdt.Rows[DrawNo + k]["SNo"].ToString() + " and " + dtTargetdt.Rows[DrawNo + l]["SNo"].ToString() + " , ";
                                                            oArrList.Add(No1);
                                                            Col1.Add(dc1);
                                                            Col2.Add(dc2);
                                                            relMatch = true;

                                                            break;
                                                        }
                                                        foreach (object oNo1 in oArrList)
                                                        {
                                                            if (No1 == (int)oNo1)
                                                            {
                                                                relMatch = true;
                                                                skipIds += (ind - 1).ToString() + ",";
                                                                break;
                                                            }
                                                        }
                                                        if (relMatch)
                                                            break;
                                                    }
                                                    if (!relMatch)
                                                        break;
                                                }
                                                if (relMatch)
                                                {
                                                    DataRow rowSummary = dtSummary.NewRow();
                                                    rowSummary["Id"] = dtSummary.Rows.Count + 1;
                                                    rowSummary["KeyNumbers"] = keyNo;
                                                    rowSummary["Relation"] = Relation;
                                                    rowSummary["RecNo"] = RecNo;
                                                    dtSummary.Rows.Add(rowSummary);
                                                    int colId = 0;
                                                    for (int ix = 0; ix < keyRows.Length; ix++)
                                                    {
                                                        int DrawNo = Convert.ToInt32(keyRows[ix]["SNo"]);
                                                        DataRow[] rFindIndex1 = dtTargetdt.Select("SNo < " + DrawNo.ToString());
                                                        DrawNo = rFindIndex1.Length;
                                                        if (DrawNo + k >= dtTargetdt.Rows.Count)
                                                            continue;
                                                        DataRow row3 = dtNumFound.NewRow();
                                                        row3["Id"] = dtNumFound.Rows.Count + 1;
                                                        row3["col"] = dc1;
                                                        //if (ix == keyRows.Length - 1)
                                                        //    row3["col"] = dc1.Replace('M','W');
                                                        row3["SNo"] = dtTargetdt.Rows[DrawNo + k]["SNo"];//Convert.ToInt32(keyRows[ix]["SNo"]) + k;
                                                        row3["flag"] = "P";
                                                        row3["RecNo"] = RecNo;
                                                        dtNumFound.Rows.Add(row3);

                                                        if (DrawNo + l >= dtTargetdt.Rows.Count)
                                                            continue;
                                                        if (skipIds.Contains(ix.ToString()))
                                                            continue;
                                                        DataRow row4 = dtNumFound.NewRow();
                                                        row4["Id"] = dtNumFound.Rows.Count + 1;
                                                        if (ix == 0)
                                                            row4["col"] = dcColumnW[rInd];
                                                        else
                                                        {
                                                            row4["col"] = Col2[colId].ToString();
                                                            //if (ix == keyRows.Length - 1)
                                                            //    row4["col"] = Col2[colId].ToString().Replace('M','W');
                                                            colId++;
                                                        }
                                                        row4["RecNo"] = RecNo;
                                                        row4["SNo"] = dtTargetdt.Rows[DrawNo + l]["SNo"];//Convert.ToInt32(keyRows[ix]["SNo"]) + l;
                                                        row4["flag"] = "B";
                                                        dtNumFound.Rows.Add(row4);

                                                    }
                                                    break;
                                                }
                                            }
                                            if (relMatch)
                                                break;
                                        }
                                    }// 
                                    #endregion

                                    if (relMatch)
                                        break;

                                }//end for l
                                if (relMatch)
                                    break;
                            }//end for dc1

                        }//end for k
                        //}

                        #region r4
                        if (dtNumFound.Rows.Count > 0 && bFlage)
                        {
                            for (int ix = 0; ix < keyRows.Length; ix++)
                            {
                                DataRow row3 = dtNumFound.NewRow();
                                row3["Id"] = dtNumFound.Rows.Count + 1;
                                row3["col"] = dc;
                                //if(ix == keyRows.Length - 1)
                                //    row3["col"] = dc.Replace('M','W');
                                row3["SNo"] = Convert.ToInt32(keyRows[ix]["SNo"]);// dtTargetdt.Rows[j + k]["SNo"];
                                row3["flag"] = "K";
                                row3["RecNo"] = RecNo;
                                dtNumFound.Rows.Add(row3);
                            }
                            dtNumFoundHistory.Merge(dtNumFound);
                            dtSummaryHistory.Merge(dtSummary);
                            KeyNumbers.Add(keyNo);
                            strKeyNumbers += keyNo.ToString() + " ,";
                            RecNo++;
                            //break;
                        }
                        else
                        {
                            dtSummary.Rows.Clear();
                            dtNumFound.Rows.Clear();
                        }

                        #endregion

                    }//end for i

                }//end for dc
                progressBar1.Value = 35;
                if (dtNumFoundHistory.Rows.Count == 0)
                    MessageBox.Show("No Match Found");
                else
                    MessageBox.Show("RESULT : " + KeyNumbers.Count.ToString() + " key numbers found " + strKeyNumbers);
                dtSummary.Rows.Clear();
                dtNumFound.Rows.Clear();

                dtNumFoundHistory.DefaultView.RowFilter = "RecNo = MIN(RecNo)";
                dtNumFound.Merge(dtNumFoundHistory.DefaultView.ToTable());

                dtSummaryHistory.DefaultView.RowFilter = "RecNo = MIN(RecNo)";
                dtSummary.Merge(dtSummaryHistory.DefaultView.ToTable());

                grdSpanSummary.DataSource = dtSummary;
                progressBar1.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SpanSearch()
        {
            try
            {
                dtNumFound = new DataTable();
                dtNumFound.Columns.Add("Id");
                dtNumFound.Columns.Add("col");
                dtNumFound.Columns.Add("SNo");
                dtNumFound.Columns.Add("RecNo" , Type.GetType("System.Int32"));
                dtNumFound.Columns.Add("PatternLength");
                dtNumFound.Columns.Add("flag");

                dtSummary = new DataTable();
                dtSummary.Columns.Add("Id");
                dtSummary.Columns.Add("KeyNumbers");
                dtSummary.Columns.Add("Relation");
                dtSummary.Columns.Add("RecNo", Type.GetType("System.Int32"));
                dtSummary.Columns.Add("fDate",Type.GetType("System.DateTime"));
                dtSummary.Columns.Add("Numbers");

                dtSpanForcast = new DataTable();
                dtSpanForcast.Columns.Add("fDate");
                dtSpanForcast.Columns.Add("DBName");
                dtSpanForcast.Columns.Add("Numbers");
                dtSpanForcast.Columns.Add("Method");
                dtSpanForcast.Columns.Add("KeyNum");
                dtSpanForcast.Columns.Add("Col");

                dtSummaryHistory = dtSummary.Clone();
                dtNumFoundHistory = dtNumFound.Clone();

                DataTable dtTemp = ds.Tables[0].Copy();
                dtTemp.DefaultView.RowFilter = "W1 is Not null";
               
                DataTable dtTargetdt = dtTemp.DefaultView.ToTable();
                if (rdSpanPattern.Checked)
                {
                    if(rdWinning.Checked)
                        SpanPatternSearch(dtTargetdt);
                    else
                        SpanPatternSearchMachine(dtTargetdt);
                }
                else if (rdSpanCode.Checked)
                {
                    if (rdWinning.Checked)
                        SpanPatternSearchCode(dtTargetdt);
                    else
                        SpanPatternSearchCodeMachine(dtTargetdt);
                }
                else
                {
                    if(rdWinning.Checked)
                    SpanPatternSearchMOD(dtTargetdt);
                    else
                        SpanPatternSearchMODMachine(dtTargetdt);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearchNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdSpanPattern.Checked || rdSpanCode.Checked || rdSpanMod.Checked)
                {
                    if (dtNumFound.Rows.Count > 0)
                    {
                        int RecNo = Convert.ToInt32(dtNumFound.Rows[0]["RecNo"]) + 1;
                        dtNumFoundHistory.DefaultView.RowFilter = "RecNo = " + RecNo.ToString();
                        if ((dtNumFoundHistory.DefaultView.ToTable()).Rows.Count == 0)
                        {
                            MessageBox.Show("No more result exist");
                            return;
                        }

                        dtNumFound.Rows.Clear();
                        dtNumFound.Merge(dtNumFoundHistory.DefaultView.ToTable());

                        dtSummaryHistory.DefaultView.RowFilter = "RecNo = " + RecNo.ToString();
                        dtSummary.Rows.Clear();
                        dtSummary.Merge(dtSummaryHistory.DefaultView.ToTable());

                        grdSpanSummary.Refresh();
                        searchGrid.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("No more result exist");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void SpanPatternSearchCode(DataTable dtTargetdt)
        {
            int error = 0;
            try
            {


                #region r0
                ArrayList arRel = new ArrayList();
                arRel.Add("RN");
                arRel.Add("CN");
                arRel.Add("TN");
                arRel.Add("TCN");
                string[] dcColumn = { "W2", "W3", "W4", "W1", "W5" };
                ArrayList arNumbers = new ArrayList();//collectin of key numbers 
                
                progressBar1.Value = 0;
                bool bFlage = false;
                ArrayList KeyNumbers = new ArrayList();
                int RecNo = 1;
                string strKeyNumbers = "";
                int PatterLength = 15;
                 progressBar1.Maximum = 5 * PatterLength;
                #endregion
                foreach (string dc in dcColumn)
                {

                    int keyNo;
                    arNumbers.Clear();
                    
                    for (int i = dtTargetdt.Rows.Count - PatterLength; i < dtTargetdt.Rows.Count; i++)
                    {
                        bFlage = false;
                        dtSummary.Rows.Clear();
                        dtNumFound.Rows.Clear();
                        #region r1
                        keyNo = Convert.ToInt32(dtTargetdt.Rows[i][dc]);
                        foreach (object oNo in arNumbers)
                        {
                            if (keyNo == Convert.ToInt32(oNo))
                            {
                                keyNo = -1;
                                break;
                            }
                        }
                        if (keyNo == -1)//No already checked
                            continue;

                        DataRow[] rowForcast = dtSpanForcast.Select("KeyNum = '" + keyNo.ToString() + "' and Col = '" + dc + "'");
                        if (rowForcast.Length > 0)
                            continue;

                        DataRow[] keyRows = dtTargetdt.Select(dc + " =" + keyNo.ToString());
                        //if (keyRows.Length < 2 || keyRows.Length > 4)//No need to check for pattern
                        //    continue;
                        if (keyRows.Length % 4 == 1)
                            continue;
                        else
                        {
                            int mod = 4;
                            if (keyRows.Length % 4 != 0)
                                mod = keyRows.Length % 4;
                            keyRows = dtTargetdt.Select(dc + " =" + keyNo.ToString() + " and SNo >= " + keyRows[keyRows.Length - mod]["SNo"].ToString());
                        }


                        #endregion
                        progressBar1.Value += 1;

                        //for (int PatterLength = 3; PatterLength <= 7; PatterLength++)//search for each pattern length
                        //{
                       
                        for (int k = 1; k < PatterLength - 1; k++)
                        {
                            bool relMatch = false;
                            foreach (string dc1 in dcColumn)
                            {
                                for (int l = k + 1; l < PatterLength; l++)
                                {

                                    #region r2
                                    for (int m = 0; m < arRel.Count; m++)//check for code relationship
                                    {

                                        ArrayList oArrList = new ArrayList();//this for storin No1
                                        ArrayList Col1 = new ArrayList();
                                        ArrayList Col2 = new ArrayList();
                                        string Relation = "";
                                        string skipIds = "";
                                        int Id = 0;
                                        foreach (DataRow row in keyRows)
                                        {
                                            Id++;
                                            int DrawNo = Convert.ToInt32(row["SNo"]);
                                            DataRow[] rFindIndex = dtTargetdt.Select("SNo < " + DrawNo.ToString());
                                            DrawNo = rFindIndex.Length;
                                            relMatch = false;
                                            if (DrawNo + l >= dtTargetdt.Rows.Count)
                                            {
                                                if (Id == keyRows.Length && 2 < keyRows.Length)
                                                {
                                                    bFlage = true;
                                                    relMatch = true;
                                                    #region add new row to forcast table
                                                    if (DrawNo + k < dtTargetdt.Rows.Count)
                                                    {
                                                        int n1 = Convert.ToInt32(dtTargetdt.Rows[DrawNo + k][dc1]);
                                                        int n2 = n1;
                                                        if (arRel[m].ToString().Trim() == "CN")
                                                            n2 = GetCounter(n1);
                                                        else if (arRel[m].ToString().Trim() == "TN")
                                                            n2 = GetTurning(n1);
                                                        else if (arRel[m].ToString().Trim() == "TCN")
                                                            n2 = GetTurning(GetCounter(n1));
                                                        else
                                                            n2 = n1;
                                                        //if (n2 > 0 && n2 < 91)
                                                        {

                                                            int r = l - k;
                                                            int n = dtTargetdt.Rows.Count - 1 - (DrawNo + k);
                                                            int p = r - n;
                                                            DateTime lastDate = Convert.ToDateTime(dtTargetdt.Rows[dtTargetdt.Rows.Count - 1]["Date"]);
                                                            lastDate = lastDate.AddDays(7 * p);
                                                            DataRow rForcast = dtSpanForcast.NewRow();
                                                            rForcast["fDate"] = lastDate;
                                                            rForcast["Numbers"] = n2;
                                                            rForcast["KeyNum"] = keyNo;
                                                            rForcast["Col"] = dc;
                                                            rForcast["DBName"] = lblInternalDatabaseName.Text;
                                                            rForcast["Method"] = "SPAN PATTERN CODE";
                                                            dtSpanForcast.Rows.Add(rForcast);
                                                        }
                                                    }
                                                    #endregion
                                                }
                                                break;
                                            }


                                            foreach (string dc2 in dcColumn)
                                            {
                                                int No1 = Convert.ToInt32(dtTargetdt.Rows[DrawNo + k][dc1]);
                                                int No2 = Convert.ToInt32(dtTargetdt.Rows[DrawNo + l][dc2]);
                                                if (checkRelation(No1, No2, arRel[m].ToString()))
                                                {
                                                    relMatch = true;
                                                    Col1.Add(dc1);
                                                    Col2.Add(dc2);
                                                    oArrList.Add(No1);
                                                    Relation += No1.ToString() + " " + arRel[m].ToString() + " " + No2.ToString() + " at DarwNo " + dtTargetdt.Rows[DrawNo + k]["SNo"].ToString() + " and " + dtTargetdt.Rows[DrawNo + l]["SNo"].ToString() + " ,";
                                                    break;
                                                }
                                                foreach (object oNo1 in oArrList)
                                                {
                                                    if (No1 == (int)oNo1)
                                                    {
                                                        relMatch = true;
                                                        skipIds += (Id - 1).ToString() + ",";
                                                        break;
                                                    }
                                                }
                                                if (relMatch)
                                                    break;
                                            }
                                            if (!relMatch)//a relation should match for each row 
                                            {
                                                break;

                                            }
                                        }
                                        if (relMatch)
                                        {
                                            DataRow rowSummary = dtSummary.NewRow();
                                            rowSummary["Id"] = dtSummary.Rows.Count + 1;
                                            rowSummary["KeyNumbers"] = keyNo;
                                            rowSummary["Relation"] = Relation;
                                            rowSummary["RecNo"] = RecNo;
                                            dtSummary.Rows.Add(rowSummary);
                                            int colId = 0;
                                            for (int ix = 0; ix < keyRows.Length; ix++)
                                            {
                                                //foreach (DataRow rKey in keyRows)
                                                //{
                                                int DrawNo = Convert.ToInt32(keyRows[ix]["SNo"]);
                                                DataRow[] rFindIndex = dtTargetdt.Select("SNo < " + DrawNo.ToString());
                                                DrawNo = rFindIndex.Length;

                                                if (DrawNo + k >= dtTargetdt.Rows.Count)
                                                    continue;
                                                DataRow row3 = dtNumFound.NewRow();
                                                row3["Id"] = dtNumFound.Rows.Count + 1;
                                                row3["col"] = dc1;
                                                row3["SNo"] = dtTargetdt.Rows[DrawNo + k]["SNo"];//Convert.ToInt32(keyRows[ix]["SNo"]) + k;
                                                row3["flag"] = "P";
                                                row3["RecNo"] = RecNo;
                                                dtNumFound.Rows.Add(row3);

                                                if (DrawNo + l >= dtTargetdt.Rows.Count)
                                                    continue;
                                                if (skipIds.Contains(ix.ToString()) || colId >= Col2.Count)
                                                    continue;
                                                DataRow row4 = dtNumFound.NewRow();
                                                row4["Id"] = dtNumFound.Rows.Count + 1;
                                                row4["col"] = Col2[colId].ToString();
                                                row4["SNo"] = dtTargetdt.Rows[DrawNo + l]["SNo"];//Convert.ToInt32(keyRows[ix]["SNo"]) + l;//
                                                row4["flag"] = "B";
                                                row4["RecNo"] = RecNo;
                                                dtNumFound.Rows.Add(row4);
                                                colId++;
                                                //}
                                            }

                                            break;
                                        }
                                    }// end m
                                    #endregion
                                    
                                    if (relMatch)
                                        break;

                                }//end for l
                                if (relMatch)
                                    break;
                            }//end for dc1

                        }//end for k
                        //}

                        #region r4
                        if (dtNumFound.Rows.Count > 0 && bFlage)
                        {
                            for (int ix = 0; ix < keyRows.Length; ix++)
                            {
                                DataRow row3 = dtNumFound.NewRow();
                                row3["Id"] = dtNumFound.Rows.Count + 1;
                                row3["col"] = dc;
                                row3["SNo"] = Convert.ToInt32(keyRows[ix]["SNo"]);// dtTargetdt.Rows[j + k]["SNo"];
                                row3["flag"] = "K";
                                row3["RecNo"] = RecNo;
                                dtNumFound.Rows.Add(row3);
                            }
                            dtNumFoundHistory.Merge(dtNumFound);
                            dtSummaryHistory.Merge(dtSummary);
                            KeyNumbers.Add(keyNo);
                            strKeyNumbers += keyNo.ToString() + " ,";
                            RecNo++;
                            //break;
                        }
                        else
                        {
                            dtSummary.Rows.Clear();
                            dtNumFound.Rows.Clear();
                        }

                        #endregion

                    }//end for i

                }//end for dc
                progressBar1.Value = 35;
                if (dtNumFoundHistory.Rows.Count == 0)
                    MessageBox.Show("No Match Found");
                else
                    MessageBox.Show("RESULT : " + KeyNumbers.Count.ToString() + " key numbers found " + strKeyNumbers);
                dtSummary.Rows.Clear();
                dtNumFound.Rows.Clear();

                dtNumFoundHistory.DefaultView.RowFilter = "RecNo = MIN(RecNo)";
                dtNumFound.Merge(dtNumFoundHistory.DefaultView.ToTable());

                dtSummaryHistory.DefaultView.RowFilter = "RecNo = MIN(RecNo)";
                dtSummary.Merge(dtSummaryHistory.DefaultView.ToTable());

                grdSpanSummary.DataSource = dtSummary;
                progressBar1.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SpanPatternSearchCodeMachine(DataTable dtTargetdt)
        {
            int error = 0;
            try
            {


                #region r0
                ArrayList arRel = new ArrayList();
                arRel.Add("RN");
                arRel.Add("CN");
                arRel.Add("TN");
                arRel.Add("TCN");
                string[] dcColumn = { "M2", "M3", "M4", "M1", "M5" };
                string[] dcColumnW = { "W2", "W3", "W4", "W1", "W5" };
                ArrayList arNumbers = new ArrayList();//collectin of key numbers 
                
                progressBar1.Value = 0;
                bool bFlage = false;
                ArrayList KeyNumbers = new ArrayList();
                int RecNo = 1;
                string strKeyNumbers = "";
                int PatterLength = 15;
                progressBar1.Maximum = 5*PatterLength;
                #endregion

                //for (int i = dtTargetdt.Rows.Count - PatterLength; i < dtTargetdt.Rows.Count; i++)
                //{
                //    foreach (string dc in dcColumn)
                //    {
                //        dtTargetdt.Rows[i][dc] = dtTargetdt.Rows[i][dc.Replace('M', 'W')];
                //    }
                //}
                foreach (string dc in dcColumn)
                {

                    int keyNo;
                    arNumbers.Clear();
                    
                    for (int i = dtTargetdt.Rows.Count - PatterLength; i < dtTargetdt.Rows.Count; i++)
                    {
                        bFlage = false;
                        dtSummary.Rows.Clear();
                        dtNumFound.Rows.Clear();
                        #region r1
                        keyNo = Convert.ToInt32(dtTargetdt.Rows[i][dc]);
                        foreach (object oNo in arNumbers)
                        {
                            if (keyNo == Convert.ToInt32(oNo))
                            {
                                keyNo = -1;
                                break;
                            }
                        }
                        if (keyNo == -1)//No already checked
                            continue;

                        DataRow[] rowForcast = dtSpanForcast.Select("KeyNum = '" + keyNo.ToString() + "' and Col = '" + dc + "'");
                        if (rowForcast.Length > 0)
                            continue;

                        DataRow[] keyRows = dtTargetdt.Select(dc + " =" + keyNo.ToString());
                        //if (keyRows.Length < 2 || keyRows.Length > 4)//No need to check for pattern
                        //    continue;
                        if (keyRows.Length % 4 == 1)
                            continue;
                        else
                        {
                            int mod = 4;
                            if (keyRows.Length % 4 != 0)
                                mod = keyRows.Length % 4;
                            keyRows = dtTargetdt.Select(dc + " =" + keyNo.ToString() + " and SNo >= " + keyRows[keyRows.Length - mod]["SNo"].ToString());
                        }


                        #endregion
                        progressBar1.Value += 1;

                        //for (int PatterLength = 3; PatterLength <= 7; PatterLength++)//search for each pattern length
                        //{

                        for (int k = 1; k < PatterLength - 1; k++)
                        {
                            bool relMatch = false;
                            foreach (string dc1 in dcColumn)
                            {
                                for (int l = k + 1; l < PatterLength; l++)
                                {

                                    #region r2
                                    for (int m = 0; m < arRel.Count; m++)//check for code relationship
                                    {

                                        ArrayList oArrList = new ArrayList();//this for storin No1
                                        ArrayList Col1 = new ArrayList();
                                        ArrayList Col2 = new ArrayList();
                                        string Relation = "";
                                        string skipIds = "";
                                        int Id = 0;
                                        foreach (DataRow row in keyRows)
                                        {
                                            Id++;
                                            int DrawNo = Convert.ToInt32(row["SNo"]);
                                            DataRow[] rFindIndex = dtTargetdt.Select("SNo < " + DrawNo.ToString());
                                            DrawNo = rFindIndex.Length;
                                            relMatch = false;
                                            if (DrawNo + l >= dtTargetdt.Rows.Count)
                                            {
                                                if (Id == keyRows.Length && 2 < keyRows.Length)
                                                {
                                                    bFlage = true;
                                                    relMatch = true;
                                                    #region add new row to forcast table
                                                    if (DrawNo + k < dtTargetdt.Rows.Count)
                                                    {
                                                        int n1 = Convert.ToInt32(dtTargetdt.Rows[DrawNo + k][dc1]);
                                                        int n2 = n1;
                                                        if (arRel[m].ToString().Trim() == "CN")
                                                            n2 = GetCounter(n1);
                                                        else if (arRel[m].ToString().Trim() == "TN")
                                                            n2 = GetTurning(n1);
                                                        else if (arRel[m].ToString().Trim() == "TCN")
                                                            n2 = GetTurning(GetCounter(n1));
                                                        else
                                                            n2 = n1;
                                                        //if (n2 > 0 && n2 < 91)
                                                        {

                                                            int r = l - k;
                                                            int n = dtTargetdt.Rows.Count - 1 - (DrawNo + k);
                                                            int p = r - n;
                                                            DateTime lastDate = Convert.ToDateTime(dtTargetdt.Rows[dtTargetdt.Rows.Count - 1]["Date"]);
                                                            lastDate = lastDate.AddDays(7 * p);
                                                            DataRow rForcast = dtSpanForcast.NewRow();
                                                            rForcast["fDate"] = lastDate;
                                                            rForcast["Numbers"] = n2;
                                                            rForcast["KeyNum"] = keyNo;
                                                            rForcast["Col"] = dc;
                                                            rForcast["DBName"] = lblInternalDatabaseName.Text;
                                                            rForcast["Method"] = "SPAN PATTERN CODE";
                                                            dtSpanForcast.Rows.Add(rForcast);
                                                        }
                                                    }
                                                    #endregion
                                                }
                                                break;
                                            }


                                            foreach (string dc2 in dcColumnW)
                                            {
                                                int No1 = Convert.ToInt32(dtTargetdt.Rows[DrawNo + k][dc1]);
                                                int No2 = Convert.ToInt32(dtTargetdt.Rows[DrawNo + l][dc2]);
                                                if (checkRelation(No1, No2, arRel[m].ToString()))
                                                {
                                                    relMatch = true;
                                                    Col1.Add(dc1);
                                                    Col2.Add(dc2);
                                                    oArrList.Add(No1);
                                                    Relation += No1.ToString() + " " + arRel[m].ToString() + " " + No2.ToString() + " at DarwNo " + dtTargetdt.Rows[DrawNo + k]["SNo"].ToString() + " and " + dtTargetdt.Rows[DrawNo + l]["SNo"].ToString() + " ,";
                                                    break;
                                                }
                                                foreach (object oNo1 in oArrList)
                                                {
                                                    if (No1 == (int)oNo1)
                                                    {
                                                        relMatch = true;
                                                        skipIds += (Id - 1).ToString() + ",";
                                                        break;
                                                    }
                                                }
                                                if (relMatch)
                                                    break;
                                            }
                                            if (!relMatch)//a relation should match for each row 
                                            {
                                                break;

                                            }
                                        }
                                        if (relMatch)
                                        {
                                            DataRow rowSummary = dtSummary.NewRow();
                                            rowSummary["Id"] = dtSummary.Rows.Count + 1;
                                            rowSummary["KeyNumbers"] = keyNo;
                                            rowSummary["Relation"] = Relation;
                                            rowSummary["RecNo"] = RecNo;
                                            dtSummary.Rows.Add(rowSummary);
                                            int colId = 0;
                                            for (int ix = 0; ix < keyRows.Length; ix++)
                                            {
                                                //foreach (DataRow rKey in keyRows)
                                                //{
                                                int DrawNo = Convert.ToInt32(keyRows[ix]["SNo"]);
                                                DataRow[] rFindIndex = dtTargetdt.Select("SNo < " + DrawNo.ToString());
                                                DrawNo = rFindIndex.Length;

                                                if (DrawNo + k >= dtTargetdt.Rows.Count)
                                                    continue;
                                                DataRow row3 = dtNumFound.NewRow();
                                                row3["Id"] = dtNumFound.Rows.Count + 1;
                                                row3["col"] = dc1;
                                                //if(ix== keyRows.Length - 1)
                                                //    row3["col"] = dc1.Replace('M','W');
                                                row3["SNo"] = dtTargetdt.Rows[DrawNo + k]["SNo"];//Convert.ToInt32(keyRows[ix]["SNo"]) + k;
                                                row3["flag"] = "P";
                                                row3["RecNo"] = RecNo;
                                                dtNumFound.Rows.Add(row3);

                                                if (DrawNo + l >= dtTargetdt.Rows.Count)
                                                    continue;
                                                if (skipIds.Contains(ix.ToString()) || colId >= Col2.Count)
                                                    continue;
                                                DataRow row4 = dtNumFound.NewRow();
                                                row4["Id"] = dtNumFound.Rows.Count + 1;
                                                row4["col"] = Col2[colId].ToString();
                                                //if(ix == keyRows.Length - 1)
                                                //    row4["col"] = Col2[colId].ToString().Replace('M','W');
                                                row4["SNo"] = dtTargetdt.Rows[DrawNo + l]["SNo"];//Convert.ToInt32(keyRows[ix]["SNo"]) + l;//
                                                row4["flag"] = "B";
                                                row4["RecNo"] = RecNo;
                                                dtNumFound.Rows.Add(row4);
                                                colId++;
                                                //}
                                            }

                                            break;
                                        }
                                    }// end m
                                    #endregion

                                    if (relMatch)
                                        break;

                                }//end for l
                                if (relMatch)
                                    break;
                            }//end for dc1

                        }//end for k
                        //}

                        #region r4
                        if (dtNumFound.Rows.Count > 0 && bFlage)
                        {
                            for (int ix = 0; ix < keyRows.Length; ix++)
                            {
                                DataRow row3 = dtNumFound.NewRow();
                                row3["Id"] = dtNumFound.Rows.Count + 1;
                                row3["col"] = dc;
                                //if(ix == keyRows.Length - 1)
                                //    row3["col"] = dc.Replace('M','W');
                                row3["SNo"] = Convert.ToInt32(keyRows[ix]["SNo"]);// dtTargetdt.Rows[j + k]["SNo"];
                                row3["flag"] = "K";
                                row3["RecNo"] = RecNo;
                                dtNumFound.Rows.Add(row3);
                            }
                            dtNumFoundHistory.Merge(dtNumFound);
                            dtSummaryHistory.Merge(dtSummary);
                            KeyNumbers.Add(keyNo);
                            strKeyNumbers += keyNo.ToString() + " ,";
                            RecNo++;
                            //break;
                        }
                        else
                        {
                            dtSummary.Rows.Clear();
                            dtNumFound.Rows.Clear();
                        }

                        #endregion

                    }//end for i

                }//end for dc
                progressBar1.Value = 35;
                if (dtNumFoundHistory.Rows.Count == 0)
                    MessageBox.Show("No Match Found");
                else
                    MessageBox.Show("RESULT : " + KeyNumbers.Count.ToString() + " key numbers found " + strKeyNumbers);
                dtSummary.Rows.Clear();
                dtNumFound.Rows.Clear();

                dtNumFoundHistory.DefaultView.RowFilter = "RecNo = MIN(RecNo)";
                dtNumFound.Merge(dtNumFoundHistory.DefaultView.ToTable());

                dtSummaryHistory.DefaultView.RowFilter = "RecNo = MIN(RecNo)";
                dtSummary.Merge(dtSummaryHistory.DefaultView.ToTable());

                grdSpanSummary.DataSource = dtSummary;
                progressBar1.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SpanPatternSearchMOD(DataTable dtTargetdt)
        {
            int error = 0;
            try
            {


                #region r0
                ArrayList arRel = new ArrayList();
                arRel.Add("RN");
                arRel.Add("CN");
                arRel.Add("TN");
                arRel.Add("TCN");
                string[] dcColumn = { "W2", "W3", "W4", "W1", "W5" };
                ArrayList arNumbers = new ArrayList();//collectin of key numbers 
                
                progressBar1.Value = 0;
                bool bFlage = false;
                ArrayList KeyNumbers = new ArrayList();
                int RecNo = 1;
                string strKeyNumbers = "";
                int PatterLength = 15;
                progressBar1.Maximum = 5 * PatterLength;
                #endregion

               
                foreach (string dc in dcColumn)
                {

                    int keyNo;
                    arNumbers.Clear();

                    for (int i = dtTargetdt.Rows.Count - PatterLength; i < dtTargetdt.Rows.Count; i++)
                    {
                        bFlage = false;
                        dtSummary.Rows.Clear();
                        dtNumFound.Rows.Clear();
                        #region r1
                        keyNo = Convert.ToInt32(dtTargetdt.Rows[i][dc]);
                        foreach (object oNo in arNumbers)
                        {
                            if (keyNo == Convert.ToInt32(oNo))
                            {
                                keyNo = -1;
                                break;
                            }
                        }
                        if (keyNo == -1)//No already checked
                            continue;

                        DataRow[] rowForcast = dtSpanForcast.Select("KeyNum = '" + keyNo.ToString() + "' and Col = '" + dc + "'");
                        if (rowForcast.Length > 0)
                            continue;


                        DataRow[] keyRows = dtTargetdt.Select(dc + " =" + keyNo.ToString());
                        //if (keyRows.Length < 2 || keyRows.Length > 4)//No need to check for pattern
                        //    continue;
                        if (keyRows.Length % 4 == 1)
                            continue;
                        else
                        {
                            int mod = 4;
                            if (keyRows.Length % 4 != 0)
                                mod = keyRows.Length % 4;
                            keyRows = dtTargetdt.Select(dc + " =" + keyNo.ToString() + " and SNo >= " + keyRows[keyRows.Length - mod]["SNo"].ToString());
                        }


                        #endregion
                        progressBar1.Value += 1;

                        //for (int PatterLength = 3; PatterLength <= 7; PatterLength++)//search for each pattern length
                        //{

                        for (int k = 0; k < PatterLength - 1; k++)
                        {
                            bool relMatch = false;

                            #region reg2
                            foreach (string dc1 in dcColumn)
                            {
                                if (dc == dc1 && k == 0)
                                    continue;

                                int DrawNo0 = Convert.ToInt32(keyRows[0]["SNo"]);
                                DataRow[] rFindIndex0 = dtTargetdt.Select("SNo < " + DrawNo0.ToString());
                                DrawNo0 = rFindIndex0.Length;
                                if (DrawNo0 + k >= dtTargetdt.Rows.Count)
                                {
                                    break;

                                }
                                int Mode = Convert.ToInt32(dtTargetdt.Rows[DrawNo0 + k][dc1]);
                                int ind = 0;
                                ArrayList ColList = new ArrayList();
                                ColList.Add(dc1);
                                foreach (DataRow row in keyRows)
                                {
                                    ind++;
                                    if (ind == 1)
                                        continue;

                                    int DrawNo = Convert.ToInt32(row["SNo"]);
                                    DataRow[] rFindIndex = dtTargetdt.Select("SNo < " + DrawNo.ToString());
                                    DrawNo = rFindIndex.Length;
                                    relMatch = false;
                                    if (DrawNo + k >= dtTargetdt.Rows.Count)
                                    {
                                        if (ind == keyRows.Length && 2 < keyRows.Length)
                                        {
                                            bFlage = true;
                                            relMatch = true;
                                            #region add new row to forcast table
                                            
                                                //if (Mode > 0 && Mode < 91)
                                                {

                                                    int n =  (DrawNo + k) - ( dtTargetdt.Rows.Count - 1 );
                                                    DateTime lastDate = Convert.ToDateTime(dtTargetdt.Rows[dtTargetdt.Rows.Count - 1]["Date"]);
                                                    lastDate = lastDate.AddDays(7 * n);
                                                    DataRow rForcast = dtSpanForcast.NewRow();
                                                    rForcast["fDate"] = lastDate;
                                                    rForcast["Numbers"] = Mode;
                                                    rForcast["KeyNum"] = keyNo;
                                                    rForcast["Col"] = dc;
                                                    rForcast["DBName"] = lblInternalDatabaseName.Text;
                                                    rForcast["Method"] = "SPAN PATTERN MODE";
                                                    dtSpanForcast.Rows.Add(rForcast);
                                                }
                                            #endregion
                                        }
                                        break;
                                    }
                                    foreach (string dc2 in dcColumn)
                                    {
                                        if (dc == dc2 && k == 0)
                                            continue;
                                        int Mode1 = Convert.ToInt32(dtTargetdt.Rows[DrawNo + k][dc2]);
                                        if (Mode == Mode1)
                                        {
                                            relMatch = true;
                                            ColList.Add(dc2);
                                            break;
                                        }
                                    }
                                    if (!relMatch)
                                        break;
                                }
                                if (relMatch)
                                {
                                    DataRow rowSummary = dtSummary.NewRow();
                                    rowSummary["Id"] = dtSummary.Rows.Count + 1;
                                    rowSummary["KeyNumbers"] = keyNo;
                                    rowSummary["Relation"] = Mode.ToString() + " will be played after " + k.ToString() + " weeks";
                                    rowSummary["RecNo"] = RecNo;
                                    dtSummary.Rows.Add(rowSummary);
                                    int colId = 0;
                                    for (int ix = 0; ix < keyRows.Length; ix++)
                                    {
                                        int DrawNo = Convert.ToInt32(keyRows[ix]["SNo"]);
                                        DataRow[] rFindIndex = dtTargetdt.Select("SNo < " + DrawNo.ToString());
                                        DrawNo = rFindIndex.Length;

                                        if (DrawNo + k >= dtTargetdt.Rows.Count)
                                            continue;
                                        DataRow row3 = dtNumFound.NewRow();
                                        row3["Id"] = dtNumFound.Rows.Count + 1;
                                        row3["col"] = ColList[colId].ToString();
                                        row3["SNo"] = dtTargetdt.Rows[DrawNo + k]["SNo"];//Convert.ToInt32(keyRows[ix]["SNo"]) + k;
                                        row3["flag"] = "B";
                                        row3["RecNo"] = RecNo;
                                        dtNumFound.Rows.Add(row3);

                                        colId++;
                                        //}
                                    }

                                    //break;
                                }
                            }//dc1 
                            #endregion
                        }

                        //}

                        #region r4
                        if (dtNumFound.Rows.Count > 0 && bFlage)
                        {
                            for (int ix = 0; ix < keyRows.Length; ix++)
                            {
                                DataRow row3 = dtNumFound.NewRow();
                                row3["Id"] = dtNumFound.Rows.Count + 1;
                                row3["col"] = dc;
                                row3["SNo"] = Convert.ToInt32(keyRows[ix]["SNo"]);// dtTargetdt.Rows[j + k]["SNo"];
                                row3["flag"] = "K";
                                row3["RecNo"] = RecNo;
                                dtNumFound.Rows.Add(row3);
                            }
                            dtNumFoundHistory.Merge(dtNumFound);
                            dtSummaryHistory.Merge(dtSummary);
                            KeyNumbers.Add(keyNo);
                            strKeyNumbers += keyNo.ToString() + " ,";
                            RecNo++;
                            //break;
                        }
                        else
                        {
                            dtSummary.Rows.Clear();
                            dtNumFound.Rows.Clear();
                        }

                        #endregion

                    }//end for i

                }//end for dc
                progressBar1.Value = 35;
                if (dtNumFoundHistory.Rows.Count == 0)
                    MessageBox.Show("No Match Found");
                else
                    MessageBox.Show("RESULT : " + KeyNumbers.Count.ToString() + " key numbers found " + strKeyNumbers);
                dtSummary.Rows.Clear();
                dtNumFound.Rows.Clear();

                dtNumFoundHistory.DefaultView.RowFilter = "RecNo = MIN(RecNo)";
                dtNumFound.Merge(dtNumFoundHistory.DefaultView.ToTable());

                dtSummaryHistory.DefaultView.RowFilter = "RecNo = MIN(RecNo)";
                dtSummary.Merge(dtSummaryHistory.DefaultView.ToTable());

                grdSpanSummary.DataSource = dtSummary;
                progressBar1.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void SpanPatternSearchMODMachine(DataTable dtTargetdt)
        {
            int error = 0;
            try
            {


                #region r0
                ArrayList arRel = new ArrayList();
                arRel.Add("RN");
                arRel.Add("CN");
                arRel.Add("TN");
                arRel.Add("TCN");
                string[] dcColumn = { "M2", "M3", "M4", "M1", "M5" };
                string[] dcColumnW = { "W2", "W3", "W4", "W1", "W5" };
                ArrayList arNumbers = new ArrayList();//collectin of key numbers 
                
                progressBar1.Value = 0;
                bool bFlage = false;
                ArrayList KeyNumbers = new ArrayList();
                int RecNo = 1;
                string strKeyNumbers = "";
                int PatterLength = 15;
                progressBar1.Maximum = 5 * PatterLength;
                #endregion

                //for (int i = dtTargetdt.Rows.Count - PatterLength; i < dtTargetdt.Rows.Count; i++)
                //{
                //    foreach (string dc in dcColumn)
                //    {
                //        dtTargetdt.Rows[i][dc] = dtTargetdt.Rows[i][dc.Replace('M', 'W')];
                //    }
                //}
                foreach (string dc in dcColumn)
                {

                    int keyNo;
                    arNumbers.Clear();
                    
                    for (int i = dtTargetdt.Rows.Count - PatterLength; i < dtTargetdt.Rows.Count; i++)
                    {
                        bFlage = false;
                        dtSummary.Rows.Clear();
                        dtNumFound.Rows.Clear();
                        #region r1
                        keyNo = Convert.ToInt32(dtTargetdt.Rows[i][dc]);
                        foreach (object oNo in arNumbers)
                        {
                            if (keyNo == Convert.ToInt32(oNo))
                            {
                                keyNo = -1;
                                break;
                            }
                        }
                        if (keyNo == -1)//No already checked
                            continue;

                        DataRow[] rowForcast = dtSpanForcast.Select("KeyNum = '" + keyNo.ToString() + "' and Col = '" + dc + "'");
                        if (rowForcast.Length > 0)
                            continue;

                        DataRow[] keyRows = dtTargetdt.Select(dc + " =" + keyNo.ToString());
                        //if (keyRows.Length < 2 || keyRows.Length > 4)//No need to check for pattern
                        //    continue;
                        if (keyRows.Length % 4 == 1)
                            continue;
                        else
                        {
                            int mod = 4;
                            if (keyRows.Length % 4 != 0)
                                mod = keyRows.Length % 4;
                            keyRows = dtTargetdt.Select(dc + " =" + keyNo.ToString() + " and SNo >= " + keyRows[keyRows.Length - mod]["SNo"].ToString());
                        }


                        #endregion
                        progressBar1.Value += 1;

                        //for (int PatterLength = 3; PatterLength <= 7; PatterLength++)//search for each pattern length
                        //{
                       
                        for (int k = 0; k < PatterLength - 1; k++)
                        {
                            bool relMatch = false;

                            #region reg2
                            foreach (string dc1 in dcColumnW)
                            {
                                if (dc == dc1 && k == 0)
                                    continue;

                                int DrawNo0 = Convert.ToInt32(keyRows[0]["SNo"]);
                                DataRow[] rFindIndex0 = dtTargetdt.Select("SNo < " + DrawNo0.ToString());
                                DrawNo0 = rFindIndex0.Length;
                                if (DrawNo0 + k >= dtTargetdt.Rows.Count)
                                {
                                    break;

                                }
                                int Mode = Convert.ToInt32(dtTargetdt.Rows[DrawNo0 + k][dc1]);
                                int ind = 0;
                                ArrayList ColList = new ArrayList();
                                ColList.Add(dc1);
                                foreach (DataRow row in keyRows)
                                {
                                    ind++;
                                    if (ind == 1)
                                        continue;

                                    int DrawNo = Convert.ToInt32(row["SNo"]);
                                    DataRow[] rFindIndex = dtTargetdt.Select("SNo < " + DrawNo.ToString());
                                    DrawNo = rFindIndex.Length;
                                    relMatch = false;
                                    if (DrawNo + k >= dtTargetdt.Rows.Count)
                                    {
                                        if (ind == keyRows.Length && 2 < keyRows.Length)
                                        {
                                            bFlage = true;
                                            relMatch = true;
                                            #region add new row to forcast table

                                            //if (Mode > 0 && Mode < 91)
                                            {
                                                int n = (DrawNo + k) - (dtTargetdt.Rows.Count - 1);
                                                DateTime lastDate = Convert.ToDateTime(dtTargetdt.Rows[dtTargetdt.Rows.Count - 1]["Date"]);
                                                lastDate = lastDate.AddDays(7 * n);
                                                DataRow rForcast = dtSpanForcast.NewRow();
                                                rForcast["fDate"] = lastDate;
                                                rForcast["Numbers"] = Mode;
                                                rForcast["KeyNum"] = keyNo;
                                                rForcast["Col"] = dc;
                                                rForcast["DBName"] = lblInternalDatabaseName.Text;
                                                rForcast["Method"] = "SPAN PATTERN MODE";
                                                dtSpanForcast.Rows.Add(rForcast);
                                            }
                                            #endregion
                                        }
                                        break;
                                    }
                                    foreach (string dc2 in dcColumnW)
                                    {
                                        if (dc == dc2 && k == 0)
                                            continue;
                                        int Mode1 = Convert.ToInt32(dtTargetdt.Rows[DrawNo + k][dc2]);
                                        if (Mode == Mode1)
                                        {
                                            relMatch = true;
                                            ColList.Add(dc2);
                                            break;
                                        }
                                    }
                                    if (!relMatch)
                                        break;
                                }
                                if (relMatch)
                                {
                                    DataRow rowSummary = dtSummary.NewRow();
                                    rowSummary["Id"] = dtSummary.Rows.Count + 1;
                                    rowSummary["KeyNumbers"] = keyNo;
                                    rowSummary["Relation"] = Mode.ToString() + " will be played after " + k.ToString() + " weeks";
                                    rowSummary["RecNo"] = RecNo;
                                    dtSummary.Rows.Add(rowSummary);
                                    int colId = 0;
                                    for (int ix = 0; ix < keyRows.Length; ix++)
                                    {
                                        int DrawNo = Convert.ToInt32(keyRows[ix]["SNo"]);
                                        DataRow[] rFindIndex = dtTargetdt.Select("SNo < " + DrawNo.ToString());
                                        DrawNo = rFindIndex.Length;

                                        if (DrawNo + k >= dtTargetdt.Rows.Count)
                                            continue;
                                        DataRow row3 = dtNumFound.NewRow();
                                        row3["Id"] = dtNumFound.Rows.Count + 1;
                                        row3["col"] = ColList[colId].ToString();
                                       //if(ix == keyRows.Length - 1)
                                       //    row3["col"] = ColList[colId].ToString().Replace('M','W');
                                        row3["SNo"] = dtTargetdt.Rows[DrawNo + k]["SNo"];//Convert.ToInt32(keyRows[ix]["SNo"]) + k;
                                        row3["flag"] = "B";
                                        row3["RecNo"] = RecNo;
                                        dtNumFound.Rows.Add(row3);

                                        colId++;
                                        //}
                                    }

                                    //break;
                                }
                            }//dc1 
                            #endregion
                        }
                                                        
                        //}

                        #region r4
                        if (dtNumFound.Rows.Count > 0 && bFlage)
                        {
                            for (int ix = 0; ix < keyRows.Length; ix++)
                            {
                                DataRow row3 = dtNumFound.NewRow();
                                row3["Id"] = dtNumFound.Rows.Count + 1;
                                row3["col"] = dc;
                               //if(ix == keyRows.Length -1)
                               //    row3["col"] = dc.Replace('M','W');
                                row3["SNo"] = Convert.ToInt32(keyRows[ix]["SNo"]);// dtTargetdt.Rows[j + k]["SNo"];
                                row3["flag"] = "K";
                                row3["RecNo"] = RecNo;
                                dtNumFound.Rows.Add(row3);
                            }
                            dtNumFoundHistory.Merge(dtNumFound);
                            dtSummaryHistory.Merge(dtSummary);
                            KeyNumbers.Add(keyNo);
                            strKeyNumbers += keyNo.ToString() + " ,";
                            RecNo++;
                            //break;
                        }
                        else
                        {
                            dtSummary.Rows.Clear();
                            dtNumFound.Rows.Clear();
                        }

                        #endregion

                    }//end for i

                }//end for dc
                progressBar1.Value = 35;
                if (dtNumFoundHistory.Rows.Count == 0)
                    MessageBox.Show("No Match Found");
                else
                    MessageBox.Show("RESULT : " + KeyNumbers.Count.ToString() + " key numbers found " + strKeyNumbers);
                dtSummary.Rows.Clear();
                dtNumFound.Rows.Clear();

                dtNumFoundHistory.DefaultView.RowFilter = "RecNo = MIN(RecNo)";
                dtNumFound.Merge(dtNumFoundHistory.DefaultView.ToTable());

                dtSummaryHistory.DefaultView.RowFilter = "RecNo = MIN(RecNo)";
                dtSummary.Merge(dtSummaryHistory.DefaultView.ToTable());

                grdSpanSummary.DataSource = dtSummary;
                progressBar1.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rdSpanCode_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSpanCode.Checked)
            {
                grdSummaryGroupSummation.Visible = false;

                grdSpanSummary.Visible = true;
                grdSummary.Visible = false;
                searchGrid.Columns["space2"].Visible = false;
            }
        }

        private void rdSpanMod_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSpanMod.Checked)
            {
                grdSummaryGroupSummation.Visible = false;

                grdSpanSummary.Visible = true;
                grdSummary.Visible = false;
                searchGrid.Columns["space2"].Visible = false;
            }
        }

        private void YearPatternSearchExternal()
        {
            try
            {
                dtNumFound = new DataTable();
                dtNumFound.Columns.Add("Id");
                dtNumFound.Columns.Add("col");
                dtNumFound.Columns.Add("SNo");
                dtNumFound.Columns.Add("DBId");
                dtNumFound.Columns.Add("flag");

                dtSummary = new DataTable();
                dtSummary.Columns.Add("Id");
                dtSummary.Columns.Add("Year");
                dtSummary.Columns.Add("PairNo");
                dtSummary.Columns.Add("SearchNo");
                dtSummary.Columns.Add("Database");

                DataTable dtExterList = SqlClass.GetExternalDatabaseList();
                foreach (DataRow rowEx in dtExterList.Rows)
                {
                    int DBId =Convert.ToInt32(rowEx["DBId"]);
                    DataTable dtTemp = SqlClass.GetLastYearData(DBId);
                    dtTemp.DefaultView.RowFilter = "W1 is NOT null ";
                    DataTable dtTargetdt = dtTemp.DefaultView.ToTable();
                    int count = dtTargetdt.Rows.Count;
                   
                    bool blnBreak = false;
                    int tYear = 0;
                    int sYear = 0;
                    bool found = false;
                    dtTargetdt.Columns.Add("Database");
                    if(dtTargetdt.Rows.Count != 0)
                      dtTargetdt.Rows[0]["Database"] = SqlClass.GetDBNameById(DBId);
                    for (int i = 0; i < dtTargetdt.Rows.Count; i++)
                    {
                        if (dtTargetdt.Rows[i]["Date"] != DBNull.Value)
                        {
                            //if (sYear == Convert.ToDateTime(dtTargetdt.Rows[i]["Date"]).Year)
                            //    continue;
                            sYear = Convert.ToDateTime(dtTargetdt.Rows[i]["Date"]).Year;
                        }
                        //dtNumFound.Rows.Clear();
                        if (Convert.ToInt32(dtTargetdt.Rows[i]["W1"]) > Convert.ToInt32(dtTargetdt.Rows[i]["W4"]))
                        {
                            int dif = Convert.ToInt32(dtTargetdt.Rows[i]["W1"]) - Convert.ToInt32(dtTargetdt.Rows[i]["W4"]);
                            for (int j = i + 1; j < dtTargetdt.Rows.Count; j++)
                            {
                                if (dtTargetdt.Rows[j]["Date"] != DBNull.Value)
                                    tYear = Convert.ToDateTime(dtTargetdt.Rows[j]["Date"]).Year;

                                if (Convert.ToInt32(dtTargetdt.Rows[j]["W1"]) == dif && sYear == tYear)
                                {
                                    DataRow row = dtNumFound.NewRow();
                                    row["Id"] = dtNumFound.Rows.Count + 1;
                                    row["col"] = "W1";
                                    row["SNo"] = dtTargetdt.Rows[j]["SNo"];
                                    row["flag"] = "N";
                                    row["DBId"] = DBId;
                                    dtNumFound.Rows.Add(row);

                                    DataRow row1 = dtNumFound.NewRow();
                                    row1["Id"] = dtNumFound.Rows.Count + 1;
                                    row1["col"] = "W1";
                                    row1["SNo"] = dtTargetdt.Rows[i]["SNo"];
                                    row1["flag"] = "S";
                                    row1["DBId"] = DBId;
                                    dtNumFound.Rows.Add(row1);

                                    DataRow row2 = dtNumFound.NewRow();
                                    row2["Id"] = dtNumFound.Rows.Count + 1;
                                    row2["col"] = "W4";
                                    row2["SNo"] = dtTargetdt.Rows[i]["SNo"];
                                    row2["flag"] = "S";
                                    row2["DBId"] = DBId;
                                    dtNumFound.Rows.Add(row2);
                                    int ttYear = tYear;
                                    blnBreak = false;
                                    for (int k = j + 1; k < dtTargetdt.Rows.Count; k++)
                                    {
                                        if (dtTargetdt.Rows[k]["Date"] != DBNull.Value)
                                            ttYear = Convert.ToDateTime(dtTargetdt.Rows[k]["Date"]).Year;
                                        foreach (DataColumn dc in dtTargetdt.Columns)
                                        {
                                            if (dc.Caption.StartsWith("W"))
                                            {
                                                if (Convert.ToInt32(dtTargetdt.Rows[k][dc]) == dif && tYear == ttYear)
                                                {
                                                    found = true;
                                                    DataRow row3 = dtNumFound.NewRow();
                                                    row3["Id"] = dtNumFound.Rows.Count + 1;
                                                    row3["col"] = dc.Caption;
                                                    row3["SNo"] = dtTargetdt.Rows[k]["SNo"];
                                                    row3["flag"] = "N";
                                                    row3["DBId"] = DBId;
                                                    dtNumFound.Rows.Add(row3);
                                                    blnBreak = true;
                                                    DataRow rowSummary = dtSummary.NewRow();
                                                    rowSummary["Id"] = 1;
                                                    rowSummary["Database"] = SqlClass.GetDBNameById(DBId);
                                                    rowSummary["Year"] = tYear;
                                                    rowSummary["PairNo"] = dtTargetdt.Rows[i]["SNo"].ToString();
                                                    rowSummary["SearchNo"] = dtTargetdt.Rows[j]["SNo"].ToString() + " , " + dtTargetdt.Rows[k]["SNo"].ToString();
                                                    dtSummary.Rows.Add(rowSummary);
                                                    break;
                                                }
                                            }
                                        }
                                        if (blnBreak)
                                            break;
                                    }
                                    if (!blnBreak)
                                    {
                                        dtNumFound.Rows[dtNumFound.Rows.Count - 1].Delete();
                                        dtNumFound.AcceptChanges();
                                        dtNumFound.Rows[dtNumFound.Rows.Count - 1].Delete();
                                        dtNumFound.AcceptChanges();
                                        dtNumFound.Rows[dtNumFound.Rows.Count - 1].Delete();
                                        dtNumFound.AcceptChanges();
                                    }
                                }
                                if (blnBreak)
                                    break;
                            }
                        }
                        //if (blnBreak)
                        //    break;
                    }
                    if (found)
                    {
                        ds.Tables[0].Merge(dtTargetdt);
                        DataRow rowBlank = ds.Tables[0].NewRow();
                        rowBlank["DBId"] = DBId;
                        ds.Tables[0].Rows.Add(rowBlank);
                    }
                }
                grdSummary.DataSource = dtSummary;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void YearPatternSearchExternalMachine()
        {
            try
            {
                dtNumFound = new DataTable();
                dtNumFound.Columns.Add("Id");
                dtNumFound.Columns.Add("col");
                dtNumFound.Columns.Add("SNo");
                dtNumFound.Columns.Add("DBId");
                dtNumFound.Columns.Add("flag");

                dtSummary = new DataTable();
                dtSummary.Columns.Add("Id");
                dtSummary.Columns.Add("Year");
                dtSummary.Columns.Add("PairNo");
                dtSummary.Columns.Add("SearchNo");
                dtSummary.Columns.Add("Database");

                DataTable dtExterList = SqlClass.GetExternalDatabaseList();
                foreach (DataRow rowEx in dtExterList.Rows)
                {
                    int DBId = Convert.ToInt32(rowEx["DBId"]);
                    DataTable dtTemp = SqlClass.GetLastYearData(DBId);
                    dtTemp.DefaultView.RowFilter = "M1 is NOT null ";
                    DataTable dtTargetdt = dtTemp.DefaultView.ToTable();
                    int count = dtTargetdt.Rows.Count;

                    bool blnBreak = false;
                    int tYear = 0;
                    int sYear = 0;
                    bool found = false;
                    dtTargetdt.Columns.Add("Database");
                    if (dtTargetdt.Rows.Count != 0)
                        dtTargetdt.Rows[0]["Database"] = SqlClass.GetDBNameById(DBId);
                    for (int i = 0; i < dtTargetdt.Rows.Count; i++)
                    {
                        if (dtTargetdt.Rows[i]["Date"] != DBNull.Value)
                        {
                            //if (sYear == Convert.ToDateTime(dtTargetdt.Rows[i]["Date"]).Year)
                            //    continue;
                            sYear = Convert.ToDateTime(dtTargetdt.Rows[i]["Date"]).Year;
                        }
                        //dtNumFound.Rows.Clear();
                        if (Convert.ToInt32(dtTargetdt.Rows[i]["M1"]) > Convert.ToInt32(dtTargetdt.Rows[i]["M4"]))
                        {
                            int dif = Convert.ToInt32(dtTargetdt.Rows[i]["M1"]) - Convert.ToInt32(dtTargetdt.Rows[i]["M4"]);
                            for (int j = i + 1; j < dtTargetdt.Rows.Count; j++)
                            {
                                if (dtTargetdt.Rows[j]["Date"] != DBNull.Value)
                                    tYear = Convert.ToDateTime(dtTargetdt.Rows[j]["Date"]).Year;

                                if (Convert.ToInt32(dtTargetdt.Rows[j]["M1"]) == dif && sYear == tYear)
                                {
                                    DataRow row = dtNumFound.NewRow();
                                    row["Id"] = dtNumFound.Rows.Count + 1;
                                    row["col"] = "M1";
                                    row["SNo"] = dtTargetdt.Rows[j]["SNo"];
                                    row["flag"] = "N";
                                    row["DBId"] = DBId;
                                    dtNumFound.Rows.Add(row);

                                    DataRow row1 = dtNumFound.NewRow();
                                    row1["Id"] = dtNumFound.Rows.Count + 1;
                                    row1["col"] = "M1";
                                    row1["SNo"] = dtTargetdt.Rows[i]["SNo"];
                                    row1["flag"] = "S";
                                    row1["DBId"] = DBId;
                                    dtNumFound.Rows.Add(row1);

                                    DataRow row2 = dtNumFound.NewRow();
                                    row2["Id"] = dtNumFound.Rows.Count + 1;
                                    row2["col"] = "M4";
                                    row2["SNo"] = dtTargetdt.Rows[i]["SNo"];
                                    row2["flag"] = "S";
                                    row2["DBId"] = DBId;
                                    dtNumFound.Rows.Add(row2);
                                    int ttYear = tYear;
                                    blnBreak = false;
                                    for (int k = j + 1; k < dtTargetdt.Rows.Count; k++)
                                    {
                                        if (dtTargetdt.Rows[k]["Date"] != DBNull.Value)
                                            ttYear = Convert.ToDateTime(dtTargetdt.Rows[k]["Date"]).Year;
                                        foreach (DataColumn dc in dtTargetdt.Columns)
                                        {
                                            if (dc.Caption.StartsWith("M"))
                                            {
                                                if (Convert.ToInt32(dtTargetdt.Rows[k][dc]) == dif && tYear == ttYear)
                                                {
                                                    found = true;
                                                    DataRow row3 = dtNumFound.NewRow();
                                                    row3["Id"] = dtNumFound.Rows.Count + 1;
                                                    row3["col"] = dc.Caption;
                                                    row3["SNo"] = dtTargetdt.Rows[k]["SNo"];
                                                    row3["flag"] = "N";
                                                    row3["DBId"] = DBId;
                                                    dtNumFound.Rows.Add(row3);
                                                    blnBreak = true;
                                                    DataRow rowSummary = dtSummary.NewRow();
                                                    rowSummary["Id"] = 1;
                                                    rowSummary["Database"] = SqlClass.GetDBNameById(DBId);
                                                    rowSummary["Year"] = tYear;
                                                    rowSummary["PairNo"] = dtTargetdt.Rows[i]["SNo"].ToString();
                                                    rowSummary["SearchNo"] = dtTargetdt.Rows[j]["SNo"].ToString() + " , " + dtTargetdt.Rows[k]["SNo"].ToString();
                                                    dtSummary.Rows.Add(rowSummary);
                                                    break;
                                                }
                                            }
                                        }
                                        if (blnBreak)
                                            break;
                                    }
                                    if (!blnBreak)
                                    {
                                        dtNumFound.Rows[dtNumFound.Rows.Count - 1].Delete();
                                        dtNumFound.AcceptChanges();
                                        dtNumFound.Rows[dtNumFound.Rows.Count - 1].Delete();
                                        dtNumFound.AcceptChanges();
                                        dtNumFound.Rows[dtNumFound.Rows.Count - 1].Delete();
                                        dtNumFound.AcceptChanges();
                                    }
                                }
                                if (blnBreak)
                                    break;
                            }
                        }
                        //if (blnBreak)
                        //    break;
                    }
                    if (found)
                    {
                        ds.Tables[0].Merge(dtTargetdt);
                        DataRow rowBlank = ds.Tables[0].NewRow();
                        rowBlank["DBId"] = DBId;
                        ds.Tables[0].Rows.Add(rowBlank);
                    }
                }
                grdSummary.DataSource = dtSummary;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void YearPatternSearch()
        {
            try
            {
                if (rdInternal.Checked)
                {
                    if (rdWinning.Checked)
                        YearPatternSearchInternal();
                    else
                        YearPatternSearchInternalMachine();
                }
                else
                {
                    if(rdWinning.Checked)
                       YearPatternSearchExternal();
                    else
                        YearPatternSearchExternalMachine();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnSaveResult_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtForcast = new DataTable();
                dtForcast.Columns.Add("fDate");
                dtForcast.Columns.Add("DBName");
                dtForcast.Columns.Add("Numbers");
                dtForcast.Columns.Add("Method");

                #region Group Summation
                if (rdGroupSummation.Checked)
                {
                    if (dtGroupSummary != null)
                    {
                        if (dtGroupSummary.Rows.Count > 0)
                        {
                            if (rdInternal.Checked)
                            {
                                if (dtGroupSummary.Rows[0]["fDate"] != DBNull.Value)
                                {
                                    DataRow rForcast = dtForcast.NewRow();
                                    rForcast["fDate"] = dtGroupSummary.Rows[0]["fDate"];
                                    rForcast["DBName"] = dtGroupSummary.Rows[0]["Database"];
                                    rForcast["Numbers"] = dtGroupSummary.Rows[0]["Numbers"];
                                    rForcast["Method"] = "GROUP SUMMATION";
                                    dtForcast.Rows.Add(rForcast);
                                    SqlClass.SaveForCastResult(dtForcast);
                                }
                            }
                            else
                            {
                                foreach (DataRow rSum in dtGroupSummary.Rows)
                                {
                                    if (rSum["fDate"] != null)
                                    {
                                        DataRow rForcast = dtForcast.NewRow();
                                        rForcast["fDate"] = rSum["fDate"];
                                        rForcast["DBName"] = rSum["Database"];
                                        rForcast["Numbers"] = rSum["Numbers"];
                                        rForcast["Method"] = "GROUP SUMMATION";
                                        dtForcast.Rows.Add(rForcast);
                                    }
                                }
                                SqlClass.SaveForCastResult(dtForcast);
                            }
                        }
                    }
                }// 
                #endregion

                #region Span Search
                if (rdSpanPattern.Checked || rdSpanCode.Checked || rdSpanMod.Checked)
                {
                    if (dtSpanForcast != null)
                    {
                        DataTable dtTemp = dtSpanForcast.Clone();
                        int KeyNo = 0;
                        string Col = "a";
                        foreach (DataRow rForcast in dtSpanForcast.Rows)
                        {
                            if(KeyNo == Convert.ToInt32(rForcast["KeyNum"]) && Col == rForcast["Col"].ToString())
                                continue;
                            else
                            {
                                KeyNo = Convert.ToInt32(rForcast["KeyNum"]);
                                Col = rForcast["Col"].ToString();
                                dtSpanForcast.DefaultView.RowFilter = "KeyNum = '" + KeyNo.ToString() + "' and Col = '" + Col + "'";
                                DataTable dtTemp1 = dtSpanForcast.DefaultView.ToTable();
                                DateTime fDate = new DateTime(1900, 1, 1);
                                
                                foreach (DataRow rTemp1 in dtTemp1.Rows)
                                {
                                    DataRow[] rowTemp = dtTemp.Select("fDate = '" + rTemp1["fDate"].ToString() + "' and " + "KeyNum = '" + rTemp1["KeyNum"].ToString() + "' and Col = '" + rTemp1["Col"].ToString() + "'");
                                    if (rowTemp.Length > 0)
                                        continue;
                                    fDate = Convert.ToDateTime(rTemp1["fDate"]);
                                    DataRow[] cuplRows = dtTemp1.Select("fDate = '" + rTemp1["fDate"].ToString() + "'");
                                    bool flag = true;
                                    string Nums = "";
                                    foreach (DataRow rCup in cuplRows)
                                    {
                                        if (Convert.ToInt32(rCup["Numbers"]) > 90 || Convert.ToInt32(rCup["Numbers"]) < 1)
                                        {
                                            flag = false;
                                            break;
                                        }
                                        if (Nums == "")
                                            Nums = rCup["Numbers"].ToString();
                                        else
                                            Nums += "," + rCup["Numbers"].ToString();
                                    }
                                    if (flag)
                                    {
                                        DataRow rTemp = dtTemp.NewRow();
                                        rTemp["Numbers"] = Nums + " played for key num " + KeyNo.ToString();
                                        rTemp["fDate"] = fDate;
                                        rTemp["DBName"] = rForcast["DBName"].ToString();
                                        rTemp["Method"] = rForcast["Method"].ToString();
                                        rTemp["KeyNum"] = KeyNo;
                                        rTemp["Col"] = rForcast["Col"].ToString();
                                        dtTemp.Rows.Add(rTemp);
                                    }
                                }
                            }

                        }
                        SqlClass.SaveForCastResult(dtTemp);
                    }
                }
                #endregion

                MessageBox.Show("Saved Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save");
            }
        }
    }
}
