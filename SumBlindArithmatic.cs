using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SystamaticDBSearch
{
    public partial class SumBlindArithmatic : Form
    {
        public SumBlindArithmatic()
        {
            InitializeComponent();
        }
        private static DataTable dtNumFound;
        private void SumBlindArithmatic_Load(object sender, EventArgs e)
        {
            LoadDataSet();
        }
        private static DataSet ds;
        private static DataTable dtSummary;
        private bool flag1 = false;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            flag1 = false;
            Search();
            searchGrid.Refresh();
            //LoadDataSet();
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

        private void oldSearch()
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
                int start = 0;
                int found = 0;
                for (int i = 0; i < dtTargetdt.Rows.Count; i++)
                {
                    int sum = Convert.ToInt32(dtTargetdt.Rows[i]["Sum_W"]);
                    if(cmbApplyOn.SelectedIndex == 1)
                        sum = Convert.ToInt32(dtTargetdt.Rows[i]["Sum_M"]);
                    int num = GetNumber(sum);
                    for (int j = start; j < dtTargetdt.Rows.Count; j++)
                    {
                        bool flag = false;
                        foreach (DataColumn dc in dtTargetdt.Columns)
                        {
                            if (dc.Caption.StartsWith("W"))
                            {
                                if (num == Convert.ToInt32(dtTargetdt.Rows[j][dc]))
                                {
                                    found++;
                                    flag = true;
                                    DataRow row = dtNumFound.NewRow();
                                    row["Id"] = dtNumFound.Rows.Count + 1;
                                    row["col"] = dc.Caption;
                                    row["SNo"] = dtTargetdt.Rows[j]["SNo"];
                                    row["flag"] = "N";
                                    dtNumFound.Rows.Add(row);
                                    
                                    DataRow row1 = dtNumFound.NewRow();
                                    row1["Id"] = dtNumFound.Rows.Count + 1;
                                    row1["col"] = "SUM_W";
                                    if(cmbApplyOn.SelectedIndex == 1)
                                        row1["col"] = "SUM_M";
                                    row1["SNo"] = dtTargetdt.Rows[i]["SNo"];
                                    row1["flag"] = "S";
                                    dtNumFound.Rows.Add(row1);

                                    start = j + 1;

                                    break;
                                }
                            }
                        }
                        if (flag)
                        {
                            break;
                        }
                    }
                    if (start <= i)
                    {
                        start = i + 1;
                    }
                    if (found > 1)
                    {
                        flag1 = true;
                        dtSummary = new DataTable();
                        dtSummary.Columns.Add("Id");
                        dtSummary.Columns.Add("Sum1");
                        dtSummary.Columns.Add("Sum2");
                        dtSummary.Columns.Add("No1");
                        dtSummary.Columns.Add("No2");
                        DataRow rowSummary = dtSummary.NewRow();
                        rowSummary["Id"] = 1;
                        rowSummary["Sum1"] = dtNumFound.Rows[1]["SNo"].ToString();
                        rowSummary["Sum2"] = dtNumFound.Rows[3]["SNo"].ToString();
                        rowSummary["No1"] = dtNumFound.Rows[0]["SNo"].ToString();
                        rowSummary["No2"] = dtNumFound.Rows[2]["SNo"].ToString();
                        dtSummary.Rows.Add(rowSummary);
                        grdSummary.DataSource = dtSummary;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int GetNumber(int SUM)
        {
            int num = 0;
            try
            {
                
                if (cmbTruncate.SelectedIndex == 0)
                {
                    num = SUM % 100;
                }
                else if (cmbTruncate.SelectedIndex == 1)
                {
                    num = SUM % 10 + (SUM / 100) * 10;
                }
                else
                {
                    num = SUM / 10;
                }
                if (cmbCode.SelectedIndex == 0)
                {
                    if (num + Convert.ToInt32(txtConstant.Text) >= 1 && num + Convert.ToInt32(txtConstant.Text) <= 90)
                        return num + Convert.ToInt32(txtConstant.Text);
                    else
                        return num;
                }
                else if (cmbCode.SelectedIndex == 1)
                {
                    if (GetCounter(num) + Convert.ToInt32(txtConstant.Text) >= 1 && GetCounter(num) + Convert.ToInt32(txtConstant.Text) <= 90)
                        return GetCounter(num) + Convert.ToInt32(txtConstant.Text);
                    else
                        GetCounter(num);
                }
                else if (cmbCode.SelectedIndex == 2)
                {
                    if (1 <= GetTurning(num) + Convert.ToInt32(txtConstant.Text) && 90 >= GetTurning(num) + Convert.ToInt32(txtConstant.Text))
                        return GetTurning(num) + Convert.ToInt32(txtConstant.Text);
                    else
                        return GetTurning(num);
                }
                else
                {
                    if (GetTurning(GetCounter(num)) + Convert.ToInt32(txtConstant.Text) >= 1 && GetTurning(GetCounter(num)) + Convert.ToInt32(txtConstant.Text) <= 90)
                        return GetTurning(GetCounter(num)) + Convert.ToInt32(txtConstant.Text);
                    else
                        GetTurning(GetCounter(num));
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return num;
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

        private void cmbCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void searchGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (flag1)
                {
                    if (!(e.RowIndex < 0 || e.ColumnIndex < 0))
                    {
                        foreach (DataRow row in dtNumFound.Rows)
                        {
                            if (searchGrid.Columns[e.ColumnIndex].DataPropertyName == row["col"].ToString())
                            {
                                if (Convert.ToInt32(searchGrid.Rows[e.RowIndex].Cells["SNo"].Value) == Convert.ToInt32(row["SNo"]))
                                {
                                    if (rdInternal.Checked || (Convert.ToInt32(searchGrid.Rows[e.RowIndex].Cells["DBId"].Value) == Convert.ToInt32(row["DBId"])))
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            flag1 = false;
            LoadDataSet();
            if (dtSummary != null)
                dtSummary.Rows.Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Search()
        {
            try
            {
                
                dtNumFound = new DataTable();
                dtNumFound.Columns.Add("Id");
                dtNumFound.Columns.Add("col");
                dtNumFound.Columns.Add("SNo");
                dtNumFound.Columns.Add("flag");
                dtNumFound.Columns.Add("DBId",Type.GetType("System.Int32"));

                dtSummary = new DataTable();
                dtSummary.Columns.Add("Id",Type.GetType("System.Int32"));
                dtSummary.Columns.Add("ForcastNum");
                dtSummary.Columns.Add("Database");
                dtSummary.Columns.Add("DrawNo");
                dtSummary.Columns.Add("Description");
                dtSummary.Columns.Add("fDate",Type.GetType("System.DateTime"));

                if (rdInternal.Checked)
                {
                    DataTable dtTemp = ds.Tables[0].Copy();
                    dtTemp.DefaultView.RowFilter = "W1 is Not null ";
                    DataTable dtTargetdt = dtTemp.DefaultView.ToTable();
                    SearchInternal(dtTargetdt);
                }
                else
                {
                    if (!ds.Tables[0].Columns.Contains("Database"))
                    {
                        ds.Tables[0].Columns.Add("Database");
                    }
                    DataTable dtExternal = SqlClass.GetExternalDatabaseList();
                    progressBar1.Maximum = dtExternal.Rows.Count;
                    progressBar1.Value = 0;
                    foreach (DataRow rEx in dtExternal.Rows)
                    {
                        progressBar1.Value += 1;
                        int DBId = Convert.ToInt32(rEx["DBId"]);
                        DataSet dsEx = new DataSet();
                        SqlClass.GetWin_Machin_DataByDBId(DBId, ref dsEx);
                        DataTable dtTargetdt = dsEx.Tables[0].Copy();
                        int count = dtTargetdt.Rows.Count;
                        int last3 = 0;
                        for (int cnt = count - 1; cnt >= 0; cnt--)
                        {
                            while ((cnt >= 0) && ((last3 == 3 || (dtTargetdt.Rows[cnt]["W1"] == null || dtTargetdt.Rows[cnt]["W1"] == DBNull.Value))))
                            {
                                dtTargetdt.Rows[cnt].Delete();
                                dtTargetdt.AcceptChanges();
                                count = count - 1;
                                cnt--;
                            }
                            last3++;
                        }
                        dtTargetdt.Columns.Add("Database");
                        if (dtTargetdt.Rows.Count != 0)
                        { dtTargetdt.Rows[0]["Database"] = SqlClass.GetDBNameById(Convert.ToInt32(dtTargetdt.Rows[0]["DBId"]));
                        SearchExternal(dtTargetdt);
                        }
                       
                    }
                    progressBar1.Value = 0;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchInternal(DataTable dtTarget)
        {
            try
            {
                string[] cols = {"W1","W2","W3","W4","W5"};
                string[] sums = {"SUM_W","SUM_M" };
                int[] digits = { 0, 1 ,2};
                string[] codes = { "RN", "CN","TN","TCN" };
                int totalrelationMatch = 0;
                int sumlast = -1;
                progressBar1.Value = 0;
                progressBar1.Maximum = dtTarget.Rows.Count - 2;
                for (int cnt = 0; cnt < dtTarget.Rows.Count - 2; )
                {
                    progressBar1.Value += 1;
                    int relMatch = 0;
                    foreach (string sum in sums)
                    {
                        
                        foreach (int dig in digits)
                        {
                            if (dtTarget.Rows[cnt][sum].ToString().Length < 3)
                                break;
                            int exectSUM = Convert.ToInt32(dtTarget.Rows[cnt][sum].ToString().Remove(dig, 1));
                            foreach (string code in codes)
                            {
                                int SUM = exectSUM;
                                if ((SUM > 45 && code == "CN") || GetCode(SUM, code) == -1)
                                    continue;
                                SUM = GetCode(SUM, code);
                                foreach (string col in cols)
                                {
                                    int N =Convert.ToInt32(dtTarget.Rows[cnt+1][col]);
 
                                    int[] X = new int[3];
                                    X[0] = N - SUM;
                                    X[1] = SUM - N;
                                    X[2] = SUM + N;

                                    for (int x = 0; x < 3; x++)
                                    {
                                        for (int cnt1 = 1; cnt1 <= 4; cnt1++)
                                        {
                                            bool bBreak = true;
                                            if (cnt + cnt1 + 1 < dtTarget.Rows.Count)
                                            {
                                                if (dtTarget.Rows[cnt + cnt1][sum].ToString().Length < 3)
                                                    break;
                                                int SUM1 = Convert.ToInt32(dtTarget.Rows[cnt + cnt1][sum].ToString().Remove(dig, 1));
                                                if ((SUM1 > 45 && code == "CN") || GetCode(SUM1, code) == -1)
                                                    break;
                                                SUM1 = GetCode(SUM1, code);
                                                foreach (string col1 in cols)
                                                {
                                                    int N1 = Convert.ToInt32(dtTarget.Rows[cnt + cnt1+1][col1]);
                                                    if ((x == 0 && N1 - SUM1 == X[x]) || (x == 1 && SUM1 - N1 == X[x]) || (x == 2 && N1 + SUM1 == X[x]))
                                                    {
                                                        if (cnt == dtTarget.Rows.Count - 3)
                                                        {
                                                             sumlast = Convert.ToInt32(dtTarget.Rows[cnt + 2][sum].ToString().Remove(dig, 1));
                                                            if ((sumlast > 45 && code == "CN") || GetCode(sumlast, code) == -1)
                                                                continue;
                                                            sumlast = GetCode(sumlast, code);
                                                            if (x == 0)
                                                                sumlast = X[x] + sumlast;
                                                            else if(x == 1)
                                                                sumlast =sumlast - X[x];
                                                            else
                                                                sumlast = X[x] - sumlast;
                                                            if (sumlast < 1 || sumlast > 90)
                                                                continue;
                                                        }
                                                        bBreak = false;
                                                        relMatch++;
                                                        if (relMatch == 1)
                                                        {
                                                            DataRow row = dtNumFound.NewRow();
                                                            row["Id"] = dtNumFound.Rows.Count + 1;
                                                            row["col"] = col;
                                                            row["SNo"] = dtTarget.Rows[cnt  + 1]["SNo"];
                                                            row["flag"] = "N";
                                                            dtNumFound.Rows.Add(row);

                                                            DataRow row1 = dtNumFound.NewRow();
                                                            row1["Id"] = dtNumFound.Rows.Count + 1;
                                                            row1["col"] = sum;
                                                            row1["SNo"] = dtTarget.Rows[cnt]["SNo"];
                                                            row1["flag"] = "S";
                                                            dtNumFound.Rows.Add(row1);

                                                        }

                                                        DataRow row2 = dtNumFound.NewRow();
                                                        row2["Id"] = dtNumFound.Rows.Count + 1;
                                                        row2["col"] = col1;
                                                        row2["SNo"] = dtTarget.Rows[cnt + cnt1 + 1]["SNo"];
                                                        row2["flag"] = "N";
                                                        dtNumFound.Rows.Add(row2);

                                                        DataRow row3 = dtNumFound.NewRow();
                                                        row3["Id"] = dtNumFound.Rows.Count + 1;
                                                        row3["col"] = sum;
                                                        row3["SNo"] = dtTarget.Rows[cnt + cnt1]["SNo"];
                                                        row3["flag"] = "S";
                                                        dtNumFound.Rows.Add(row3);

                                                        break;
                                                    }
                                                }//col1

                                            }
                                            if (bBreak)
                                            {
                                                break;
                                            }
                                        }//cnt1
                                        if (relMatch > 1)
                                        {
                                            DataRow rowSummary = dtSummary.NewRow();
                                            rowSummary["Id"] = dtSummary.Rows.Count + 1;
                                            rowSummary["Database"] = lblInternalDatabaseName.Text;
                                            rowSummary["DrawNo"] =dtTarget.Rows[cnt]["SNo"].ToString();
                                            string constant = X[x].ToString();
                                            if (x == 2)
                                                constant += " -";
                                            rowSummary["Description"] = "Applied on " + sum + " truncating digit at position " + (dig + 1).ToString() + " taking its " + code + " and using the constant " + constant;
                                            dtSummary.Rows.Add(rowSummary);
                                            break;
                                        }
                                        else
                                        {
                                            if (relMatch != 0 && cnt != dtTarget.Rows.Count - 3)
                                            {
                                                for (int i = 0; i < (relMatch + 1) * 2; i++)
                                                {
                                                    dtNumFound.Rows[dtNumFound.Rows.Count - 1].Delete();
                                                    dtNumFound.AcceptChanges();
                                                }
                                                relMatch = 0;
                                            }
                                            if (relMatch > 0 && cnt == dtTarget.Rows.Count - 3)
                                            {
                                                DataRow rowSummary = dtSummary.NewRow();
                                                rowSummary["Id"] = dtSummary.Rows.Count + 1;
                                                rowSummary["Database"] = lblInternalDatabaseName.Text;
                                                rowSummary["DrawNo"]  =dtTarget.Rows[cnt]["SNo"].ToString();
                                                string constant = X[x].ToString();
                                                if (x == 2)
                                                    constant += " -";
                                                rowSummary["Description"] = "Applied on " + sum + " truncating digit at position " + (dig + 1).ToString() + " taking its " + code + " and using the constant " + constant;
                                                dtSummary.Rows.Add(rowSummary);
                                                break;
                                            }
                                        }
                                    }//x
                                    if (relMatch > 0)
                                        break;
                                }//col
                                if (relMatch > 0)
                                    break;
                            }//code
                            if (relMatch > 0)
                                break;
                        }//dig
                        if (relMatch > 0)
                            break;
                    }//sum
                    cnt += relMatch + 1;
                    if (cnt == dtTarget.Rows.Count-2 && relMatch != 0)
                        cnt = dtTarget.Rows.Count - 3;
                    if (relMatch > 0)
                    {
                        totalrelationMatch++;
                    }
                }//cnt
                if (totalrelationMatch > 0)
                    flag1 = true;
                if (sumlast == -1)
                    MessageBox.Show("No result found to forcast");
                else
                {
                    DateTime dtLastDate = Convert.ToDateTime(dtTarget.Rows[dtTarget.Rows.Count-1]["Date"]);
                    dtLastDate = dtLastDate.AddDays(7);
                    foreach (DataRow row in dtSummary.Rows)
                    {
                        row["fDate"] = dtLastDate;
                        row["ForcastNum"] = sumlast;
                    }
                    MessageBox.Show(sumlast.ToString() + " is expected to be played in up coming game");
                }
                progressBar1.Value = 0;
                grdSummary.DataSource = dtSummary;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int GetCode(int No, string Code)
        {
            if (Code == "RN")
                return No;
            else if (Code == "CN")
                return GetCounter(No);
            else if (Code == "TN")
                return GetTurning(No);
            else
            {
                if (No > 45)
                    return -1;
                return GetTurning(GetCounter(No));
            }
        }

        private void SearchExternal(DataTable dtTarget)
        {
           
                try
            {
                string[] cols = {"W1","W2","W3","W4","W5"};
                string[] sums = {"SUM_W","SUM_M" };
                int[] digits = { 0, 1 ,2};
                string[] codes = { "RN", "CN","TN","TCN" };
                int totalrelationMatch = 0;
                int sumlast = -1;
                for (int cnt = 0; cnt < dtTarget.Rows.Count - 2; )
                {
                   
                    int relMatch = 0;
                    foreach (string sum in sums)
                    {
                        
                        foreach (int dig in digits)
                        {
                            if (dtTarget.Rows[cnt][sum].ToString().Length < 3)
                                break;
                            int exectSUM = Convert.ToInt32(dtTarget.Rows[cnt][sum].ToString().Remove(dig, 1));
                            foreach (string code in codes)
                            {
                                int SUM = exectSUM;
                                if ((SUM > 45 && code == "CN") || GetCode(SUM, code) == -1)
                                    continue;
                                SUM = GetCode(SUM, code);
                                foreach (string col in cols)
                                {
                                    int N =Convert.ToInt32(dtTarget.Rows[cnt+1][col]);
 
                                    int[] X = new int[3];
                                    X[0] = N - SUM;
                                    X[1] = SUM - N;
                                    X[2] = SUM + N;

                                    for (int x = 0; x < 3; x++)
                                    {
                                        for (int cnt1 = 1; cnt1 <= 4; cnt1++)
                                        {
                                            bool bBreak = true;
                                            if (cnt + cnt1 + 1 < dtTarget.Rows.Count)
                                            {
                                                if (dtTarget.Rows[cnt + cnt1][sum].ToString().Length < 3)
                                                    break;
                                                int SUM1 = Convert.ToInt32(dtTarget.Rows[cnt + cnt1][sum].ToString().Remove(dig, 1));
                                                if ((SUM1 > 45 && code == "CN") || GetCode(SUM1, code) == -1)
                                                    break;
                                                SUM1 = GetCode(SUM1, code);
                                                foreach (string col1 in cols)
                                                {
                                                    int N1 = Convert.ToInt32(dtTarget.Rows[cnt + cnt1+1][col1]);
                                                    if ((x == 0 && N1 - SUM1 == X[x]) || (x == 1 && SUM1 - N1 == X[x]) || (x == 2 && N1 + SUM1 == X[x]))
                                                    {
                                                        if (cnt == dtTarget.Rows.Count - 3)
                                                        {
                                                             sumlast = Convert.ToInt32(dtTarget.Rows[cnt + 2][sum].ToString().Remove(dig, 1));
                                                            if ((sumlast > 45 && code == "CN") || GetCode(sumlast, code) == -1)
                                                                continue;
                                                            sumlast = GetCode(sumlast, code);
                                                            if (x == 0)
                                                                sumlast = X[x] + sumlast;
                                                            else if(x == 1)
                                                                sumlast =sumlast - X[x];
                                                            else
                                                                sumlast = X[x] - sumlast;
                                                            if (sumlast < 1 || sumlast > 90)
                                                                continue;
                                                        }
                                                        bBreak = false;
                                                        relMatch++;
                                                        if (relMatch == 1)
                                                        {
                                                            DataRow row = dtNumFound.NewRow();
                                                            row["Id"] = dtNumFound.Rows.Count + 1;
                                                            row["col"] = col;
                                                            row["SNo"] = dtTarget.Rows[cnt  + 1]["SNo"];
                                                            row["flag"] = "N";
                                                            row["DBId"] = dtTarget.Rows[cnt]["DBId"];
                                                            dtNumFound.Rows.Add(row);

                                                            DataRow row1 = dtNumFound.NewRow();
                                                            row1["Id"] = dtNumFound.Rows.Count + 1;
                                                            row1["col"] = sum;
                                                            row1["SNo"] = dtTarget.Rows[cnt]["SNo"];
                                                            row1["flag"] = "S";
                                                            row1["DBId"] = dtTarget.Rows[cnt]["DBId"];
                                                            dtNumFound.Rows.Add(row1);

                                                        }

                                                        DataRow row2 = dtNumFound.NewRow();
                                                        row2["Id"] = dtNumFound.Rows.Count + 1;
                                                        row2["col"] = col1;
                                                        row2["SNo"] = dtTarget.Rows[cnt + cnt1 + 1]["SNo"];
                                                        row2["flag"] = "N";
                                                        row2["DBId"] = dtTarget.Rows[cnt]["DBId"];
                                                        dtNumFound.Rows.Add(row2);

                                                        DataRow row3 = dtNumFound.NewRow();
                                                        row3["Id"] = dtNumFound.Rows.Count + 1;
                                                        row3["col"] = sum;
                                                        row3["SNo"] = dtTarget.Rows[cnt + cnt1]["SNo"];
                                                        row3["DBId"] = dtTarget.Rows[cnt]["DBId"];
                                                        row3["flag"] = "S";
                                                        dtNumFound.Rows.Add(row3);

                                                        break;
                                                    }
                                                }//col1

                                            }
                                            if (bBreak)
                                            {
                                                break;
                                            }
                                        }//cnt1
                                        if (relMatch > 1)
                                        {
                                            DataRow rowSummary = dtSummary.NewRow();
                                            rowSummary["Id"] = dtSummary.Rows.Count + 1;
                                            rowSummary["Database"] = SqlClass.GetDBNameById(Convert.ToInt32(dtTarget.Rows[cnt]["DBId"]));
                                            rowSummary["DrawNo"] =dtTarget.Rows[cnt]["SNo"].ToString();
                                            string constant = X[x].ToString();
                                            if (x == 2)
                                                constant += " -";
                                            rowSummary["Description"] = "Applied on " + sum + " truncating digit at position " + (dig + 1).ToString() + " taking its " + code + " and using the constant " + constant;
                                            dtSummary.Rows.Add(rowSummary);
                                            break;
                                        }
                                        else
                                        {
                                            if (relMatch != 0 && cnt != dtTarget.Rows.Count - 3)
                                            {
                                                for (int i = 0; i < (relMatch + 1) * 2; i++)
                                                {
                                                    dtNumFound.Rows[dtNumFound.Rows.Count - 1].Delete();
                                                    dtNumFound.AcceptChanges();
                                                }
                                                relMatch = 0;
                                            }
                                            if (relMatch > 0 && cnt == dtTarget.Rows.Count - 3)
                                            {
                                                DataRow rowSummary = dtSummary.NewRow();
                                                rowSummary["Id"] = dtSummary.Rows.Count + 1;
                                                rowSummary["Database"] = SqlClass.GetDBNameById(Convert.ToInt32(dtTarget.Rows[cnt]["DBId"]));
                                                rowSummary["DrawNo"]  =dtTarget.Rows[cnt]["SNo"].ToString();
                                                string constant = X[x].ToString();
                                                if (x == 2)
                                                    constant += " -";
                                                rowSummary["Description"] = "Applied on " + sum + " truncating digit at position " + (dig + 1).ToString() + " taking its " + code + " and using the constant " + constant;
                                                dtSummary.Rows.Add(rowSummary);
                                                break;
                                            }
                                        }
                                    }//x
                                    if (relMatch > 0)
                                        break;
                                }//col
                                if (relMatch > 0)
                                    break;
                            }//code
                            if (relMatch > 0)
                                break;
                        }//dig
                        if (relMatch > 0)
                            break;
                    }//sum
                    cnt += relMatch + 1;
                    if (cnt == dtTarget.Rows.Count-2 && relMatch != 0)
                        cnt = dtTarget.Rows.Count - 3;
                    if (relMatch > 0)
                    {
                        totalrelationMatch++;
                    }
                }//cnt

                if (sumlast != -1)
                {
                    flag1 = true;
                    DateTime dtLastDate = Convert.ToDateTime(dtTarget.Rows[dtTarget.Rows.Count - 1]["Date"]);
                    dtLastDate = dtLastDate.AddDays(7);
                    dtSummary.Rows[dtSummary.Rows.Count - 1]["ForcastNum"] = sumlast;
                    dtSummary.Rows[dtSummary.Rows.Count - 1]["fDate"] = dtLastDate;
                    ds.Tables[0].Merge(dtTarget);
                    DataRow rowLast = ds.Tables[0].NewRow();
                    rowLast["DBId"] = dtTarget.Rows[0]["DBId"];
                    rowLast["SNo"] = 0;
                    ds.Tables[0].Rows.Add(rowLast);
                }
                grdSummary.DataSource = dtSummary;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
               
                if (dtSummary != null)
                   dtSummary.Rows.Clear();
                if (dtNumFound != null)
                    dtNumFound.Rows.Clear();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = dtSummary.Copy();
                ds.Tables.Add(dt);
                ExportToExcel(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExportToExcel(DataSet ds)
        {
            try
            {
                string file = Application.StartupPath + "\\ReportSumBlindArithmatic.xls";

                if (System.IO.File.Exists(file))
                    System.IO.File.Delete(file);

                System.IO.FileStream fs = new FileStream(file, FileMode.Create);
                fs.Close();
                System.IO.StreamWriter sw = new StreamWriter(file);
                StringBuilder sb = new StringBuilder();
                sb.Append("Forcast Num        Database                            DrawNo           Description");
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
                                    content += row[dc].ToString() + "          ";
                                    int contentLength = 0;
                                    if (dc.Caption == "ForcastNum")
                                        contentLength = 20;
                                    else if (dc.Caption == "Database")
                                        contentLength = 60;
                                    else if (dc.Caption == "SNo")
                                        contentLength = 80;
                                    else
                                        contentLength = content.Length;

                                    if (content.Length < contentLength)
                                    {
                                        int l = contentLength - content.Length;
                                        for (int i = 1; i < l; i++)
                                        {
                                            content += " ";
                                        }
                                    }
                                    //sb.Append(content);
                                }
                            }
                            sb.AppendLine();
                            sb.Append(content);
                            sb.AppendLine();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtForcast = new DataTable();
                dtForcast.Columns.Add("fDate");
                dtForcast.Columns.Add("DBName");
                dtForcast.Columns.Add("Numbers");
                dtForcast.Columns.Add("Method");

                if (dtSummary != null)
                {
                    if (dtSummary.Rows.Count > 0)
                    {
                        if (rdInternal.Checked)
                        {
                            if (dtSummary.Rows[0]["ForcastNum"] != DBNull.Value)
                            {
                                DataRow rForcast = dtForcast.NewRow();
                                rForcast["fDate"] = dtSummary.Rows[0]["fDate"];
                                rForcast["DBName"] = dtSummary.Rows[0]["Database"];
                                rForcast["Numbers"] = dtSummary.Rows[0]["ForcastNum"];
                                rForcast["Method"] = "SUM BLIND ARITHMATIC"; 
                                dtForcast.Rows.Add(rForcast);
                                SqlClass.SaveForCastResult(dtForcast);
                            }
                        }
                        else
                        {
                            foreach (DataRow rSummary in dtSummary.Rows)
                            {
                                DataRow rForcast = dtForcast.NewRow();
                                rForcast["fDate"] = rSummary["fDate"];
                                rForcast["DBName"] = rSummary["Database"];
                                rForcast["Numbers"] = rSummary["ForcastNum"];
                                rForcast["Method"] = "SUM BLIND ARITHMATIC";
                                dtForcast.Rows.Add(rForcast);
                            }
                            SqlClass.SaveForCastResult(dtForcast);
                        }
                        MessageBox.Show("Saved Successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fail'd to save");
            }
        }
    }
}
