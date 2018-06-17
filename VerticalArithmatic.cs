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
    public partial class VerticalArithmatic : Form
    {
        private static DataTable dtLast;
        private static DataTable dtSearch;
        private static DataTable dtTarget;
        private static DataTable dtBase;
        private static DataTable dtResult;
        private static DataTable dtNumFound;
        private static DataTable dtExternal;
        private static DataTable dtSummary;
        private static int totalresultFound;
        private static bool flag = false;
        private static DataTable dtForcast;
        private static DataTable dtSummaryFinal;
        
        public VerticalArithmatic()
        {
            InitializeComponent();
        }

        private void btnSearch1_Click(object sender, EventArgs e)
        {
            flag = true;
            totalresultFound = 0;
            dtResult.Rows.Clear();
            dtNumFound.Rows.Clear();
            dtSummary.Rows.Clear();
            dtSummaryFinal.Rows.Clear();
            dtForcast.Rows.Clear();
            lblResultFound.Text = "Total Result Found : ";
            string Name = ((Button)sender).Name;
            int No = Convert.ToInt32(Name.Substring(9));
            Search(No);
            lblResultFound.Text = "Total Result Found : " + totalresultFound;
            panelSearchResult.Visible = true;
            panelSearchResult.BringToFront();
            ResultGrid.Visible = true;
            Date1.DefaultCellStyle.Format = "dd/MM/yyyy";
            ResultGrid.DataSource = dtResult;
            PreparSummary();
            dtSummaryFinal.DefaultView.RowFilter = "Count > 1";
            dtSummaryFinal.DefaultView.Sort = "Count desc";
            grdSummary.DataSource = dtSummaryFinal.DefaultView;
            
        }

        private void Search(int No)
        {
            if (rdInternal.Checked)
                SearchById(No);
            else
            {
                if (flag)
                    progressBar1.Maximum = dtExternal.Rows.Count;
                int i = 0;
                foreach (DataRow row in dtExternal.Rows)
                {
                    if (flag)
                        progressBar1.Value = i;

                    int DBId = Convert.ToInt32(row["DBId"]);
                    PreSerchSetting(DBId);
                    SearchById(No);
                    i++;
                }
                if (flag)
                    progressBar1.Value = 0;
            }
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            flag = false;
            totalresultFound = 0;
            dtResult.Rows.Clear();
            dtNumFound.Rows.Clear();
            dtSummary.Rows.Clear();
            dtSummaryFinal.Rows.Clear();
            dtForcast.Rows.Clear();
            lblResultFound.Text = "Total Result Found : ";
            progressBar1.Maximum = dtSearch.Rows.Count;

            for (int i = 1; i <= dtSearch.Rows.Count; i++)
            {
                progressBar1.Value = i;
                if (btnSearch1.Enabled)
                    Search(i);
                else
                {
                    if (Convert.ToInt32(dtSearch.Rows[i - 1]["Sum"]) > 0)
                    {
                        Search(i);
                    }
                }
            }
            lblResultFound.Text = "Total Result Found : " + totalresultFound;
            panelSearchResult.Visible = true;
            panelSearchResult.BringToFront();
            ResultGrid.Visible = true;
            Date1.DefaultCellStyle.Format = "dd/MM/yyyy";

            ResultGrid.DataSource = dtResult;
            PreparSummary();
            dtSummaryFinal.DefaultView.RowFilter = "Count > 1";
            dtSummaryFinal.DefaultView.Sort = "Count desc";
            grdSummary.DataSource = dtSummaryFinal.DefaultView;
            progressBar1.Value = 0;
            
        }

        private void getLastRows()
        {
            try
            {
                dtBase = SqlClass.GetWin_Machin_Data();
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
                    dtLast = ds.Tables[0].Clone();
                    for (int i = j; i > 0; i--)
                    {
                        DataRow rowSerch = dtLast.NewRow();
                        rowSerch["SNo"] = ds.Tables[0].Rows[rowCount - i]["SNo"];
                        rowSerch["Id"] = ds.Tables[0].Rows[rowCount - i]["Id"];
                        rowSerch["DBId"] = ds.Tables[0].Rows[rowCount - i]["DBId"];
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
                        dtLast.Rows.Add(rowSerch);
                    }
                    PreSerchSetting(DBId);
                }
                Date.DefaultCellStyle.Format = "dd/MM/yyyy";
                searchGrid.DataSource = dtLast;



                dtExternal = SqlClass.GetExternalDatabaseList();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PrepareSearchTable()
        {
            try
            {
                dtSearch = new DataTable();
                dtSearch.Columns.Add("Id", Type.GetType("System.Int32"));
                dtSearch.Columns.Add("Op1");
                dtSearch.Columns.Add("Op2");
                dtSearch.Columns.Add("Op");
                dtSearch.Columns.Add("Sum", Type.GetType("System.Int32"));

                DataRow rowA, rowB;
                int cnt = dtLast.Rows.Count - 1;
                int Count = 0;
                while (dtLast.Rows[cnt]["W1"] == null || dtLast.Rows[cnt]["W1"] == DBNull.Value)
                    cnt--;
                rowB = dtLast.Rows[cnt];
                cnt--;
                while (dtLast.Rows[cnt]["W1"] == null || dtLast.Rows[cnt]["W1"] == DBNull.Value)
                    cnt--;
                rowA = dtLast.Rows[cnt];
                int id = 1;
                string[] W_M = { "W", "M" };
                string[] Oparator = { "+", "-","~"};
                foreach (string stW_M in W_M)
                {
                    foreach (string Optr in Oparator)
                    {
                        for (int i = 1; i <= 5; i++)//B and A
                        {
                           
                                string op1, op2;
                                op1 = stW_M + i.ToString();
                                op2 = stW_M + i.ToString();
                                int sum = Convert.ToInt32(rowA[op1]) + Convert.ToInt32(rowB[op2]);
                                if (Optr == "-")
                                    sum = Convert.ToInt32(rowA[op1]) - Convert.ToInt32(rowB[op2]);
                                if(Optr == "~")
                                    sum =Convert.ToInt32(rowB[op2]) - Convert.ToInt32(rowA[op1]);
                                DataRow row = dtSearch.NewRow();
                                row["Id"] = id;
                                row["Op1"] = op1;
                                row["Op2"] = op2;
                                row["Op"] = Optr;
                                row["Sum"] = sum;
                                dtSearch.Rows.Add(row);
                                this.Controls.Find("l" + id.ToString(), true)[0].Text = sum.ToString();
                                id++;
                          }                     
                    }
                }
                dtResult = dtLast.Clone();
                dtResult.Columns.Add("MatchType");
                dtResult.Columns.Add("DataBaseName");
                dtResult.Columns.Add("RecNo");

                dtNumFound = new DataTable();
                dtNumFound.Columns.Add("Id", Type.GetType("System.Int32"));
                dtNumFound.Columns.Add("RecNo", Type.GetType("System.Int32"));
                dtNumFound.Columns.Add("col");


                dtSummary = new DataTable();
                dtSummary.Columns.Add("Id", Type.GetType("System.Int32"));
                dtSummary.Columns.Add("Number");
                dtSummary.Columns.Add("Count", Type.GetType("System.Int32"));
                dtSummary.Columns.Add("Draw");
                dtSummary.Columns.Add("Database");

                dtSummaryFinal = new DataTable();
                dtSummaryFinal.Columns.Add("Id", Type.GetType("System.Int32"));
                dtSummaryFinal.Columns.Add("Code");
                dtSummaryFinal.Columns.Add("Number");
                dtSummaryFinal.Columns.Add("Count", Type.GetType("System.Int32"));
                dtSummaryFinal.Columns.Add("Database");
                dtSummaryFinal.Columns.Add("Draw");
                dtSummaryFinal.Columns.Add("DBId", Type.GetType("System.Int32"));
               

                dtForcast = new DataTable();
                dtForcast.Columns.Add("Id", Type.GetType("System.Int32"));
                dtForcast.Columns.Add("DBId", Type.GetType("System.Int32"));
                dtForcast.Columns.Add("SNo", Type.GetType("System.Int32"));
                dtForcast.Columns.Add("W1", Type.GetType("System.Int32"));
                dtForcast.Columns.Add("W2", Type.GetType("System.Int32"));
                dtForcast.Columns.Add("W3", Type.GetType("System.Int32"));
                dtForcast.Columns.Add("W4", Type.GetType("System.Int32"));
                dtForcast.Columns.Add("W5", Type.GetType("System.Int32"));
                dtForcast.Columns.Add("Code");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchById(int No)
        {
            try
            {
                int sum = Convert.ToInt32(dtSearch.Rows[No - 1]["Sum"]);
                string colA = dtSearch.Rows[No - 1]["Op1"].ToString();
                string colB = dtSearch.Rows[No - 1]["Op2"].ToString();
                string Op = dtSearch.Rows[No - 1]["Op"].ToString();
                int count = dtTarget.Rows.Count;
                if (rdInternal.Checked)
                    count--;
                for (int cnt = 14; cnt < count; cnt++)
                {
                    if (dtTarget.Rows[cnt][colB] != null && dtTarget.Rows[cnt][colB] != DBNull.Value && dtTarget.Rows[cnt - 1][colA] != null && dtTarget.Rows[cnt - 1][colA] != DBNull.Value)
                    {
                        if (dtTarget.Rows[cnt][colB].ToString() != "" && dtTarget.Rows[cnt - 1][colA].ToString() != "")
                        {
                            int No1 = Convert.ToInt32(dtTarget.Rows[cnt - 1][colA]);
                            int No2 = Convert.ToInt32(dtTarget.Rows[cnt][colB]);
                            int sum1 = No1 + No2;
                            if (Op == "-")
                                sum1 = No1 - No2;
                            if (Op == "~")
                                sum1 =No2 - No1 ;

                            if (sum == sum1)
                            {
                                DataRow rowNumFound = dtNumFound.NewRow();
                                rowNumFound["col"] = colA;
                                rowNumFound["RecNo"] = -1;
                                rowNumFound["Id"] = dtNumFound.Rows.Count + 1;
                                dtNumFound.Rows.Add(rowNumFound);

                                DataRow rowNumFound1 = dtNumFound.NewRow();
                                rowNumFound1["col"] = colB;
                                rowNumFound1["RecNo"] = -1;
                                rowNumFound1["Id"] = dtNumFound.Rows.Count + 1;
                                dtNumFound.Rows.Add(rowNumFound1);


                                string matchType = colA + "A" + Op + colB + "B = " + sum.ToString();
                                if (Op == "~")
                                    matchType = colB + "B - "  + colA + "A = " + sum.ToString();
                                int startId = Convert.ToInt32(dtTarget.Rows[cnt - 14]["Id"]);
                                int endId = Convert.ToInt32(dtTarget.Rows[cnt]["Id"]);
                                int DBId = Convert.ToInt32(dtTarget.Rows[cnt]["DBId"]);
                                int sendId = Convert.ToInt32(dtTarget.Rows[cnt - 1]["Id"]);
                                ImportToResult(startId, endId, sendId, matchType, DBId);


                                string Database = SqlClass.GetDBNameById(DBId);
                                string Draw1 = dtTarget.Rows[cnt - 1]["SNo"].ToString();
                                string Draw2 = dtTarget.Rows[cnt]["SNo"].ToString();
                                string Draws = "( " + Draw1 + " , " + Draw2 + " )";
                                DataRow[] rowSums = dtSummary.Select("Database = '" + DBId + "' and Number = '" + matchType + "'");
                                if (rowSums.Length > 0)
                                {
                                    rowSums[0]["Count"] = Convert.ToInt32(rowSums[0]["Count"]) + 1;
                                    rowSums[0]["Draw"] = rowSums[0]["Draw"].ToString() + " , " + Draws;
                                }
                                else
                                {
                                    DataRow rowSummary = dtSummary.NewRow();
                                    rowSummary["Id"] = dtSummary.Rows.Count + 1;
                                    rowSummary["Count"] = 1;
                                    rowSummary["Draw"] = Draws;
                                    rowSummary["Number"] = matchType;
                                    rowSummary["Database"] = DBId;
                                    dtSummary.Rows.Add(rowSummary);
                                }
                                totalresultFound++;
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

        private void PreSerchSetting(int DBId)
        {
            try
            {
                dtBase.DefaultView.RowFilter = "DBId = " + DBId.ToString();
                dtTarget = dtBase.DefaultView.ToTable();
                int count = dtTarget.Rows.Count;
                for (int cnt = 0; cnt < count; cnt++)
                {
                    while ((cnt < count) && (dtTarget.Rows[cnt]["W1"] == null || dtTarget.Rows[cnt]["W1"] == DBNull.Value))
                    {
                        dtTarget.Rows[cnt].Delete();
                        dtTarget.AcceptChanges();
                        count = count - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ImportToResult(int startId, int endId, int sendId, string MatchType, int DBId)
        {
            try
            {

                DataRow[] rows = dtBase.Select("DBId = " + DBId.ToString() + " and Id > " + (startId - 1).ToString() + " and Id < " + (endId + 3).ToString());
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow rowResult = dtResult.NewRow();
                    rowResult["RecNo"] = dtResult.Rows.Count + 1;
                    rowResult["Id"] = rows[i]["Id"];
                    rowResult["DBId"] = rows[i]["DBId"];
                    rowResult["W1"] = rows[i]["W1"];
                    rowResult["W2"] = rows[i]["W2"];
                    rowResult["W3"] = rows[i]["W3"];
                    rowResult["W4"] = rows[i]["W4"];
                    rowResult["W5"] = rows[i]["W5"];
                    rowResult["Sum_W"] = rows[i]["Sum_W"];
                    rowResult["M1"] = rows[i]["M1"];
                    rowResult["M2"] = rows[i]["M2"];
                    rowResult["M3"] = rows[i]["M3"];
                    rowResult["M4"] = rows[i]["M4"];
                    rowResult["M5"] = rows[i]["M5"];
                    rowResult["Sum_M"] = rows[i]["Sum_M"];
                    rowResult["Date"] = rows[i]["Date"];
                    rowResult["SNo"] = rows[i]["SNo"];
                    if (i == 0)
                    {
                        rowResult["MatchType"] = MatchType;

                        rowResult["DataBaseName"] = SqlClass.GetDBNameById(DBId);
                    }

                    if (endId == Convert.ToInt32(rows[i]["Id"]) || sendId == Convert.ToInt32(rows[i]["Id"]))
                    {
                        DataRow[] rowFond = dtNumFound.Select("RecNo = -1");
                        rowFond[0]["RecNo"] = dtResult.Rows.Count + 1;
                    }
                    else
                    {
                        if (i == rows.Length - 1 || i == rows.Length - 2)
                        {
                            rowResult["RecNo"] = -1;
                            if (i == rows.Length - 2)
                            {
                                if (rows[i]["W1"] != null && rows[i]["W1"] != DBNull.Value)
                                {
                                    if (rows[i]["W1"].ToString() != "")
                                    {
                                        DataRow rowForcast = dtForcast.NewRow();
                                        rowForcast["Id"] = rows[i]["Id"];
                                        rowForcast["DBId"] = rows[i]["DBId"];
                                        rowForcast["SNo"] = rows[i]["SNo"];
                                        rowForcast["W1"] = rows[i]["W1"];
                                        rowForcast["W2"] = rows[i]["W2"];
                                        rowForcast["W3"] = rows[i]["W3"];
                                        rowForcast["W4"] = rows[i]["W4"];
                                        rowForcast["W5"] = rows[i]["W5"];
                                        rowForcast["Code"] = MatchType;
                                        dtForcast.Rows.Add(rowForcast);
                                    }
                                }
                            }
                        }
                    }
                    dtResult.Rows.Add(rowResult);
                }

                DataRow rowResult1 = dtResult.NewRow();
                rowResult1["RecNo"] = dtResult.Rows.Count + 1;
                dtResult.Rows.Add(rowResult1);
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
                DataTable dtInter = SqlClass.GetInternalDatabase();
                int DBId;
                if (dtInter.Rows.Count > 0)
                {
                    DBId = Convert.ToInt32(dtInter.Rows[0]["DBId"]);
                    PreSerchSetting(DBId);
                }
            }
        }

        private void rdExternal_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void VerticalArithmatic_Load(object sender, EventArgs e)
        {
            getLastRows();
            PrepareSearchTable();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            panelSearchResult.Visible = false;
        }

        private void ResultGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                    if (dtNumFound.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtNumFound.Rows)
                        {
                            if (ResultGrid.Columns[e.ColumnIndex].DataPropertyName == row["col"].ToString())
                            {
                                if (Convert.ToInt32(row["RecNo"]) == Convert.ToInt32(ResultGrid.Rows[e.RowIndex].Cells["RecNo"].Value))
                                {
                                    e.CellStyle.BackColor = Color.Yellow;
                                }
                            }
                        }
                        if (Convert.ToInt32(ResultGrid.Rows[e.RowIndex].Cells["RecNo"].Value) == -1)
                        {
                            e.CellStyle.BackColor = Color.Aqua;
                        }
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PreparSummary()
        {
            try
            {
                dtSummaryFinal.Rows.Clear();
                foreach (DataRow row in dtSummary.Rows)
                {
                    dtForcast.DefaultView.RowFilter = "Code = '" + row["Number"].ToString() + "' and DBId = " + row["Database"].ToString();
                    DataTable dtTemp = dtForcast.DefaultView.ToTable();
                    dtTemp.Columns.Remove("Id");
                    dtTemp.Columns.Remove("DBId");
                    dtTemp.Columns.Remove("Code");
                    foreach(DataRow rowTemp in dtTemp.Rows)
                    {
                        foreach(DataColumn dc in dtTemp.Columns)
                        {
                            if(dc.Caption.StartsWith("W"))
                            {
                                DataRow[] rowCount = dtSummaryFinal.Select("Number = '" +rowTemp[dc].ToString()+"' and Code = '" +row["Number"].ToString()+ "' and DBId ="+row["Database"].ToString());
                               if(rowCount.Length == 0)
                               {
                                   DataRow[] rowTemps = dtTemp.Select("W1 = " + rowTemp[dc].ToString() + " OR W2 = " + rowTemp[dc].ToString() + " OR W3 = " + rowTemp[dc].ToString() + " OR W4 = " + rowTemp[dc].ToString() + " OR W5 = " + rowTemp[dc].ToString());
                                   DataRow rowSummaryFinal = dtSummaryFinal.NewRow();
                                   rowSummaryFinal["Id"] = dtSummaryFinal.Rows.Count + 1;
                                   rowSummaryFinal["Code"] = row["Number"].ToString();
                                   rowSummaryFinal["Number"] = rowTemp[dc];
                                   rowSummaryFinal["Count"] = rowTemps.Length;
                                   rowSummaryFinal["DBId"] = row["Database"].ToString();
                                   rowSummaryFinal["Database"] = SqlClass.GetDBNameById(Convert.ToInt32(row["Database"]));
                                   string sDarw = "";
                                   foreach (DataRow r in rowTemps)
                                   {
                                       sDarw += "( " + (Convert.ToInt32(r["SNo"]) - 2).ToString() + "," + (Convert.ToInt32(r["SNo"]) - 1) + ")";
                                   }
                                   rowSummaryFinal["Draw"] = sDarw;
                                   dtSummaryFinal.Rows.Add(rowSummaryFinal);
                               }
                              }
                        }
                    }
                    DataRow[] rowpairs = dtSummaryFinal.Select("Code = '" + row["Number"].ToString() + "' and DBId =" + row["Database"].ToString());
                    for (int i = 0; i < rowpairs.Length - 1; i++)
                    {
                        for (int j = i+1; j < rowpairs.Length ; j++)
                        {
                            string firstNo = rowpairs[i]["Number"].ToString();
                            string secNo = rowpairs[j]["Number"].ToString();
                            string filter = "( W1 = " + firstNo + " OR W2 = " + firstNo + " OR W3 = " + firstNo + " OR W4 = " + firstNo + " OR W5 = " + firstNo + ") AND ( W1 = " + secNo + " OR W2 = " + secNo + " OR W3 = " + secNo + " OR W4 = " + secNo + " OR W5 = " + secNo + ")";
                            DataRow[] rowTemps = dtTemp.Select(filter);
                            if (rowTemps.Length > 0)
                            {
                                DataRow rowSummaryFinal = dtSummaryFinal.NewRow();
                                rowSummaryFinal["Id"] = dtSummaryFinal.Rows.Count + 1;
                                rowSummaryFinal["Code"] = row["Number"].ToString();
                                rowSummaryFinal["Number"] = firstNo + "," + secNo  ;
                                rowSummaryFinal["Count"] = rowTemps.Length;
                                rowSummaryFinal["DBId"] = row["Database"].ToString();
                                rowSummaryFinal["Database"] = SqlClass.GetDBNameById(Convert.ToInt32(row["Database"]));
                                string sDarw = "";
                                foreach (DataRow r in rowTemps)
                                {
                                    sDarw += "( " + (Convert.ToInt32(r["SNo"]) - 2).ToString() + "," + (Convert.ToInt32(r["SNo"]) - 1) + ")";
                                }
                                rowSummaryFinal["Draw"] = sDarw;
                                dtSummaryFinal.Rows.Add(rowSummaryFinal);
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt = dtSummaryFinal.Copy();
            ds.Tables.Add(dt);
            ExportToExcel(ds);
        }
        private void ExportToExcel(DataSet ds)
        {
            string file = Application.StartupPath + "\\ReportVerticalArithmatic.xls";
            if (System.IO.File.Exists(file))
                System.IO.File.Delete(file);

            System.IO.FileStream fs = new FileStream(file, FileMode.Create);
            fs.Close();
            System.IO.StreamWriter sw = new StreamWriter(file);
            StringBuilder sb = new StringBuilder();
            sb.Append("Summation Code           Number                   Appearance               Databse                  Draw Mode");
            sb.AppendLine();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (Convert.ToInt32(row["Count"]) > 1)
                {
                    string content = "";
                    foreach (DataColumn dc in ds.Tables[0].Columns)
                    {
                        if (dc.Caption == "Code" || dc.Caption == "Database" || dc.Caption == "Number" || dc.Caption == "Count" || dc.Caption == "Draw")
                        {
                            content += row[dc].ToString();
                            if (dc.Caption == "Count")
                                content +=" Times";
                            int length = content.Length;
                            int lenSpaces = 25 - length % 25;
                            for (int i = 0; i < lenSpaces; i++)
                                content += " ";

                                //sb.Append(content);
                        }
                    }
                    sb.Append(content);
                    sb.AppendLine();
                }
            }
            sw.Write(sb);
            sw.Close();
            sw.Dispose();
            MessageBox.Show("Export Successfully");
        }
    }
}
