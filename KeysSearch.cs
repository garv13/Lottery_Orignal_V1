using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SystamaticDBSearch
{
    public partial class KeysSearch : Form
    {
        public KeysSearch()
        {
            InitializeComponent();
        }

        private void KeysSearch_Load(object sender, EventArgs e)
        {
            fillKeys();
            LoadDataSet();
            CreatePetternTable();
        }
        private static DataTable dtKeys;
        private static DataSet ds;
        private static DataTable dtPettern;
        private static DataTable dtPatternValues;
        private static DataTable dtTargetdt;
        private static DataTable dtSummary;
        private static string strDBName;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            addNumbers();
            apendNumbers();

        }
        private void fillKeys()
        {
            try
            {
                dtKeys = new DataTable();
                dtKeys.Columns.Add("X", Type.GetType("System.Int32"));
                dtKeys.Columns.Add("Y", Type.GetType("System.Int32"));
                for (int i = 1; i <= 180; i++)
                {
                    for (int j = -89; j <= 180; j++)
                    {
                        if (j == 0)
                            continue;
                        DataRow row = dtKeys.NewRow();
                        row["X"] = i;
                        row["Y"] = j;
                        dtKeys.Rows.Add(row);
                    }
                }
                for (int i = -89; i < 0; i++)
                {
                    for (int j = -89; j <= 180; j++)
                    {
                        if (j == 0)
                            continue;
                        DataRow row = dtKeys.NewRow();
                        row["X"] = i;
                        row["Y"] = j;
                        dtKeys.Rows.Add(row);
                    }
                }
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
                    strDBName = (SqlClass.GetInternalDatabase()).Rows[0]["DBName"].ToString();
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
        private void addNumbers()
        {
            try
            {
                cmbFirstOprd.Items.Add(cmbNumbers.SelectedItem.ToString());
                cmbSecOprd.Items.Add(cmbNumbers.SelectedItem.ToString());
                cmbPairNo1.Items.Add(cmbNumbers.SelectedItem.ToString());
                cmbPairNo2.Items.Add(cmbNumbers.SelectedItem.ToString());
                cmbNo1.Items.Add(cmbNumbers.SelectedItem.ToString());
                cmbNo2.Items.Add(cmbNumbers.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
            }
        }
        private void apendNumbers()
        {
            try
            {
                DataRow row = dtPettern.NewRow();
                row["No"] = cmbNumbers.SelectedItem.ToString();
                row["DrawNo"] = cmbDrawNo.SelectedItem.ToString();
                row["OprandFirst"] = cmbFirstOprd.SelectedItem.ToString();
                string value = cmbNumbers.SelectedItem.ToString() + " = " + cmbFirstOprd.SelectedItem.ToString();
                if (!(cmbSecOprd.Text == "" || cmbSecOprd.Text == "none"))
                {
                    row["OprandSecond"] = cmbSecOprd.SelectedItem.ToString();
                    row["Operator"] = cmbOp.SelectedItem.ToString();
                    value += " " + cmbOp.SelectedItem.ToString() + " " + cmbSecOprd.SelectedItem.ToString() + " in " + cmbDrawNo.SelectedItem.ToString() + " th Draw";
                }
                else
                    value += " " + " in " + cmbDrawNo.SelectedItem.ToString() + " th Draw";
                NumberList.Items.Add(value);
                dtPettern.Rows.Add(row);
            }
            catch (Exception ex)
            {

            }
        }
        private static int GroupId = 0;
        private static DataTable dtNumFound;
        private bool flag1 = false;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //if (rdInternal.Checked)
            //{
                //if (KeySearchM())
                //    MessageBox.Show("Match Found");
                //else
                //    MessageBox.Show("No Match Found");

                KeySearchNew();
            //}
            //else
            //{
            //    DataTable dt = SqlClass.GetExternalDatabaseList();
            //    string DBIdList = "";
            //    int i = 0;
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        int DBId = Convert.ToInt32(row["DBId"]);
            //        strDBName = row["DBName"].ToString();
            //        LoadDataSet(DBId);
            //        if (KeySearchM())
            //            break;
            //        else
            //        {
            //            if (i == dt.Rows.Count - 1)
            //                MessageBox.Show("No Match Found");
            //        }
            //        i++;
            //    }
            //}
            flag1 = true;
            searchGrid.Refresh();
        }
        private void LoadDataSet(int DBId)
        {
            try
            {
                ds = new DataSet();
                SqlClass.GetWin_Machin_DataByDBId(DBId, ref ds);
                Date.DefaultCellStyle.Format = "dd/MM/yyyy";
                searchGrid.DataSource = ds.Tables[0];
                searchGrid.Columns["Id"].Visible = false;
                searchGrid.Columns["DBId"].Visible = false;
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
                NumberList.Items.Clear();
                txtConstantNo.Text = "";
                cmbNo1.Items.Clear();
                cmbNo2.Items.Clear();
                cmbPairNo1.Items.Clear();
                cmbPairNo2.Items.Clear();
                if (cmbFirstOprd.Items.Count > 14)
                {
                    int i = cmbFirstOprd.Items.Count - 14;
                    for (int j = 0; j < i; j++)
                    {
                        cmbFirstOprd.Items.RemoveAt(cmbFirstOprd.Items.Count - 1);
                    }

                }
                if (cmbSecOprd.Items.Count > 15)
                {
                    int i = cmbSecOprd.Items.Count - 15;
                    for (int j = 0; j < i; j++)
                    {
                        cmbSecOprd.Items.RemoveAt(cmbSecOprd.Items.Count - 1);
                    }

                }
                dtPettern.Rows.Clear();
                dtPatternValues.Rows.Clear();
                dtNumFound.Rows.Clear();
                flag1 = false;
                dtSummary.Rows.Clear();
                rdInternal.Checked = true;
                LoadDataSet();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbFirstOprd_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CreatePetternTable()
        {
            try
            {
                dtPettern = new DataTable();
                dtPettern.Columns.Add("No");
                dtPettern.Columns.Add("OprandFirst");
                dtPettern.Columns.Add("OprandSecond");
                dtPettern.Columns.Add("Operator");
                dtPettern.Columns.Add("DrawNo");
                dtPettern.Columns.Add("Value");
                dtPettern.Columns.Add("Col");
                dtPettern.Columns.Add("SNo");

                dtPatternValues = new DataTable();
                dtPatternValues.Columns.Add("Id");
                dtPatternValues.Columns.Add("No");
                dtPatternValues.Columns.Add("Value");
                dtPatternValues.Columns.Add("DrawNo");
                dtPatternValues.Columns.Add("col1");
                dtPatternValues.Columns.Add("col2");
                dtPatternValues.Columns.Add("GroupId", Type.GetType("System.Int32"));

                dtSummary = new DataTable();
                dtSummary.Columns.Add("Id");
                dtSummary.Columns.Add("XY");
                dtSummary.Columns.Add("CMatch");
                dtSummary.Columns.Add("Desc");
                dtSummary.Columns.Add("DBName");

                dtNumFound = new DataTable();
                dtNumFound.Columns.Add("Id");
                dtNumFound.Columns.Add("col");
                dtNumFound.Columns.Add("SNo");
                dtNumFound.Columns.Add("flag");
            }
            catch (Exception ex)
            {
            }
        }

        private bool KeySearchM()
        {
            try
            {
                if (dtPettern.Rows.Count > 0)
                {
                    dtTargetdt = ds.Tables[0].Copy();
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
                    dtNumFound.Rows.Clear();
                    dtSummary.Rows.Clear();
                    int X, Y;
                    foreach (DataRow row in dtPettern.Rows)
                    {
                        row["Value"] = "";
                        row["Col"] = "";
                        row["SNo"] = "";
                    }
                    dtPatternValues.Rows.Clear();
                    int KeyCount = 1;
                    if (rdAllKeys.Checked)
                        KeyCount = dtKeys.Rows.Count;
                    bool break_i = false;
                    for (int j = 0; j < KeyCount; j++)
                    {
                        X = Convert.ToInt32(dtKeys.Rows[j]["X"]);
                        Y = Convert.ToInt32(dtKeys.Rows[j]["Y"]);
                        if (rdSpecificKey.Checked)
                        {
                            X = Convert.ToInt32(txtX.Text);
                            Y = Convert.ToInt32(txtY.Text);
                        }
                        int found = 0;

                        for (int i = 0; i < count; i++)
                        {

                            dtPatternValues.Rows.Clear();
                            GroupId = 0;
                            recSearch(X, Y, 0, i);
                            bool break_g = false;
                            for (int g = 1; g <= GroupId; g++)
                            {
                                string firstPairNo = cmbPairNo1.Text.Trim();
                                string secPairNo = cmbPairNo2.Text.Trim();
                                DataRow[] rowPatternV = dtPatternValues.Select("No IN ( '" + firstPairNo + "','" + secPairNo + "') AND GroupId = " + g.ToString());
                                int PairNo1 = Convert.ToInt32(rowPatternV[0]["Value"]);
                                int PairNo2 = Convert.ToInt32(rowPatternV[1]["Value"]);
                                if (!(PairNo1 > 0 && PairNo1 <= 90) || !(PairNo2 > 0 && PairNo2 <= 90))
                                {
                                    continue;
                                }
                                else
                                {
                                    int week = Convert.ToInt32(cmbWeeks.Text);
                                    string cap = "W";
                                    if (rdMachine.Checked)
                                        cap = "M";
                                    if (rdFirstOption.Checked)
                                    {
                                        string strNo1 = cmbNo1.Text.Trim();
                                        DataRow[] rowNo1 = dtPatternValues.Select("No ='" + strNo1 + "' AND GroupId = " + g.ToString());
                                        int No1 = Convert.ToInt32(rowNo1[0]["Value"]);
                                        if (!(No1 > 0 && No1 <= 90))
                                        {
                                            continue;
                                        }
                                        else
                                        {

                                            string colName = cmbOccuresIn.Text.Trim();
                                            for (int k = 0; k < count - week; k++)
                                            {
                                                foreach (DataColumn dc in dtTargetdt.Columns)
                                                {
                                                    if (dc.Caption.StartsWith(colName))
                                                    {
                                                        if (No1 == Convert.ToInt32(dtTargetdt.Rows[k][dc]))
                                                        {
                                                            int cnt = 0;
                                                            string lastColumn = "W1";
                                                            foreach (DataColumn dc1 in dtTargetdt.Columns)
                                                            {

                                                                if (dc1.Caption.StartsWith(cap))
                                                                {
                                                                    if (Convert.ToInt32(dtTargetdt.Rows[k + week][dc1]) == PairNo1 || Convert.ToInt32(dtTargetdt.Rows[k + week][dc1]) == PairNo2)
                                                                    {
                                                                        cnt++;
                                                                        if (cnt == 2)
                                                                        {
                                                                            break_g = true;
                                                                            found++;
                                                                            string flag = "Y";
                                                                            if (found == 2)
                                                                                break_i = true;

                                                                            DataRow rowNumbers = dtNumFound.NewRow();
                                                                            rowNumbers["Id"] = dtNumFound.Rows.Count + 1;
                                                                            rowNumbers["col"] = dc.Caption;
                                                                            rowNumbers["SNo"] = dtTargetdt.Rows[k]["SNo"];
                                                                            rowNumbers["flag"] = "Y";

                                                                            DataRow rowNumbers1 = dtNumFound.NewRow();
                                                                            rowNumbers1["Id"] = dtNumFound.Rows.Count + 2;
                                                                            rowNumbers1["col"] = lastColumn;
                                                                            rowNumbers1["SNo"] = dtTargetdt.Rows[k + week]["SNo"];
                                                                            rowNumbers1["flag"] = "Y";

                                                                            DataRow rowNumbers2 = dtNumFound.NewRow();
                                                                            rowNumbers2["Id"] = dtNumFound.Rows.Count + 3;
                                                                            rowNumbers2["col"] = dc1.Caption;
                                                                            rowNumbers2["SNo"] = dtTargetdt.Rows[k + week]["SNo"];
                                                                            rowNumbers2["flag"] = "Y";

                                                                            DataRow rowSummary = dtSummary.NewRow();
                                                                            rowSummary["Id"] = dtSummary.Rows.Count + 1;
                                                                            rowSummary["XY"] = X.ToString() + " , " + Y.ToString();
                                                                            rowSummary["CMatch"] = dtTargetdt.Rows[i]["SNo"].ToString();
                                                                            rowSummary["PMatch"] = dtTargetdt.Rows[k + week]["SNo"].ToString();
                                                                            rowSummary["DBName"] = strDBName;
                                                                            dtSummary.Rows.Add(rowSummary);


                                                                            if (break_i)
                                                                            {
                                                                                rowNumbers["flag"] = "B";
                                                                                rowNumbers1["flag"] = "B";
                                                                                rowNumbers2["flag"] = "B";
                                                                                flag = "B";
                                                                            }
                                                                            dtNumFound.Rows.Add(rowNumbers);
                                                                            dtNumFound.Rows.Add(rowNumbers1);
                                                                            dtNumFound.Rows.Add(rowNumbers2);
                                                                            fillFromPattern(g, flag);
                                                                            break;
                                                                        }
                                                                        lastColumn = dc1.Caption;
                                                                    }
                                                                }
                                                            }

                                                        }
                                                    }
                                                }
                                                if (break_g)
                                                    break;
                                            }
                                            if (break_g)
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        if (i + week < count)
                                        {
                                            string strNo2 = cmbNo2.Text.Trim();
                                            DataRow[] rowNo2 = dtPatternValues.Select("No ='" + strNo2 + "' AND GroupId = " + g.ToString());
                                            int No2 = Convert.ToInt32(rowNo2[0]["Value"]);
                                            int constant = Convert.ToInt32(txtConstantNo.Text);
                                            if (No2 == constant)
                                            {
                                                int cnt = 0;
                                                string lastColumn = "W1";
                                                foreach (DataColumn dc1 in dtTargetdt.Columns)
                                                {
                                                    if (dc1.Caption.StartsWith(cap))
                                                    {

                                                        if (Convert.ToInt32(dtTargetdt.Rows[i + week][dc1]) == PairNo1 || Convert.ToInt32(dtTargetdt.Rows[i + week][dc1]) == PairNo2)
                                                        {

                                                            cnt++;

                                                            if (cnt == 2)
                                                            {
                                                                found++;
                                                                break_g = true;
                                                                if (found == 2)
                                                                    break_i = true;

                                                                string flag = "Y";
                                                                DataRow rowNumbers1 = dtNumFound.NewRow();
                                                                rowNumbers1["Id"] = dtNumFound.Rows.Count + 1;
                                                                rowNumbers1["col"] = lastColumn;
                                                                rowNumbers1["SNo"] = dtTargetdt.Rows[i + week]["SNo"];
                                                                rowNumbers1["flag"] = "Y";

                                                                DataRow rowNumbers2 = dtNumFound.NewRow();
                                                                rowNumbers2["Id"] = dtNumFound.Rows.Count + 2;
                                                                rowNumbers2["col"] = dc1.Caption;
                                                                rowNumbers2["SNo"] = dtTargetdt.Rows[i + week]["SNo"];
                                                                rowNumbers2["flag"] = "Y";

                                                                DataRow rowSummary = dtSummary.NewRow();
                                                                rowSummary["Id"] = dtSummary.Rows.Count + 1;
                                                                rowSummary["XY"] = X.ToString() + " , " + Y.ToString();
                                                                rowSummary["CMatch"] = dtTargetdt.Rows[i]["SNo"].ToString();
                                                                rowSummary["PMatch"] = dtTargetdt.Rows[i + week]["SNo"].ToString();
                                                                rowSummary["DBName"] = strDBName;
                                                                dtSummary.Rows.Add(rowSummary);

                                                                if (break_i)
                                                                {

                                                                    rowNumbers1["flag"] = "B";
                                                                    rowNumbers2["flag"] = "B";
                                                                    flag = "B";
                                                                }
                                                                dtNumFound.Rows.Add(rowNumbers1);
                                                                dtNumFound.Rows.Add(rowNumbers2);
                                                                fillFromPattern(g, flag);
                                                                break;
                                                            }
                                                            lastColumn = dc1.Caption;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (break_g)
                                    break;
                            }
                            if (break_i)
                                break;
                        }

                        if (break_i)
                            break;
                        else
                        {
                            dtSummary.Rows.Clear();
                            dtPatternValues.Rows.Clear();
                        }
                    }

                    grdSummary.DataSource = dtSummary;
                    return break_i;
                }
                return false;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void fillFromPattern(int g, string flag)
        {
            try
            {
                DataRow[] rowPatterns = dtPatternValues.Select("GroupId = " + g.ToString());
                foreach (DataRow rowp in rowPatterns)
                {
                    string[] cols = rowp["col1"].ToString().Split(' ');
                    foreach (string col in cols)
                    {
                        DataRow rowNum = dtNumFound.NewRow();
                        rowNum["Id"] = dtNumFound.Rows.Count + 1;
                        rowNum["col"] = col.Trim();
                        rowNum["SNo"] = rowp["DrawNo"];
                        rowNum["flag"] = flag;
                        dtNumFound.Rows.Add(rowNum);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void recSearch(int X, int Y, int index, int i)
        {
            try
            {

                int count = dtTargetdt.Rows.Count;

                #region Prepare Pattern Value Table

                DataRow row = dtPettern.Rows[index];
                int No1, No2, Value;
                string Op1 = row["OprandFirst"].ToString();
                string sDrawNo = row["DrawNo"].ToString();
                {

                    int DrawNo = 0;
                    if (sDrawNo.Trim() == "n")
                        DrawNo = i;
                    else
                    {
                        DrawNo = i + Convert.ToInt32(sDrawNo.Substring(1));
                        if (!(DrawNo >= 0 && DrawNo < count))
                        {

                            return;
                        }
                    }

                    if (Op1.Trim() == "X")
                    {
                        No1 = X;
                        #region Second Oprand
                        if (row["OprandSecond"] != DBNull.Value)
                        {
                            string Op2 = row["OprandSecond"].ToString();
                            string Op = row["Operator"].ToString();
                            if (Op2.Trim() == "X")
                            {
                                No2 = X;
                                if (Op.Trim() == "+")
                                    Value = No1 + No2;
                                else if (Op.Trim() == "-")
                                    Value = No1 - No2;
                                else
                                    Value = Math.Abs(No1 - No2);

                                row["Value"] = Value;
                                if (!(index + 1 == dtPettern.Rows.Count))
                                    recSearch(X, Y, index + 1, i);
                                else
                                {
                                    GroupId++;
                                    InserttoPatternValue();
                                    return;
                                }
                            }
                            else if (Op2.Trim() == "Y")
                            {
                                No2 = Y;
                                if (Op.Trim() == "+")
                                    Value = No1 + No2;
                                else if (Op.Trim() == "-")
                                    Value = No1 - No2;
                                else
                                    Value = Math.Abs(No1 - No2);
                                row["Value"] = Value;
                                if (!(index + 1 == dtPettern.Rows.Count))
                                    recSearch(X, Y, index + 1, i);
                                else
                                {
                                    GroupId++;
                                    InserttoPatternValue();
                                    return;
                                }
                            }
                            else if (Op2.StartsWith("W") || Op2.StartsWith("M"))
                            {
                                foreach (DataColumn dc2 in dtTargetdt.Columns)
                                {
                                    if (dc2.Caption.StartsWith(Op2))
                                    {
                                        No2 = Convert.ToInt32(dtTargetdt.Rows[DrawNo][dc2]);
                                        if (Op.Trim() == "+")
                                            Value = No1 + No2;
                                        else if (Op.Trim() == "-")
                                            Value = No1 - No2;
                                        else
                                            Value = Math.Abs(No1 - No2);
                                        row["SNo"] = dtTargetdt.Rows[DrawNo]["SNo"];
                                        row["Col"] = dc2.Caption + " ";
                                        row["Value"] = Value;
                                        if (!(index + 1 == dtPettern.Rows.Count))
                                            recSearch(X, Y, index + 1, i);
                                        else
                                        {
                                            GroupId++;
                                            InserttoPatternValue();
                                            return;
                                        }
                                    }
                                }



                            }
                            else
                            {
                                DataRow[] rowPatterns = dtPettern.Select("No = '" + Op2 + "'");
                                foreach (DataRow rowp in rowPatterns)
                                {
                                    No2 = Convert.ToInt32(rowp["Value"]);
                                    if (Op.Trim() == "+")
                                        Value = No1 + No2;
                                    else if (Op.Trim() == "-")
                                        Value = No1 - No2;
                                    else
                                        Value = Math.Abs(No1 - No2);
                                    row["SNo"] = rowp["SNo"];
                                    row["Col"] = rowp["Col"].ToString() + " ";
                                    row["Value"] = Value;
                                    if (!(index + 1 == dtPettern.Rows.Count))
                                        recSearch(X, Y, index + 1, i);
                                    else
                                    {
                                        GroupId++;
                                        InserttoPatternValue();
                                        return;
                                    }
                                }

                            }
                        }
                        else
                        {
                            row["Value"] = No1;
                            if (!(index + 1 == dtPettern.Rows.Count))
                                recSearch(X, Y, index + 1, i);
                            else
                            {
                                GroupId++;
                                InserttoPatternValue();
                                return;
                            }
                        }// 
                        #endregion
                    }
                    else if (Op1.Trim() == "Y")
                    {
                        No1 = Y;
                        #region Second Oprand
                        if (row["OprandSecond"] != DBNull.Value)
                        {
                            string Op2 = row["OprandSecond"].ToString();
                            string Op = row["Operator"].ToString();
                            if (Op2.Trim() == "X")
                            {
                                No2 = X;
                                if (Op.Trim() == "+")
                                    Value = No1 + No2;
                                else if (Op.Trim() == "-")
                                    Value = No1 - No2;
                                else
                                    Value = Math.Abs(No1 - No2);
                                row["Value"] = Value;
                                if (!(index + 1 == dtPettern.Rows.Count))
                                    recSearch(X, Y, index + 1, i);
                                else
                                {
                                    GroupId++;
                                    InserttoPatternValue();
                                    return;
                                }
                            }
                            else if (Op2.Trim() == "Y")
                            {
                                No2 = Y;
                                if (Op.Trim() == "+")
                                    Value = No1 + No2;
                                else if (Op.Trim() == "-")
                                    Value = No1 - No2;
                                else
                                    Value = Math.Abs(No1 - No2);
                                row["Value"] = Value;
                                if (!(index + 1 == dtPettern.Rows.Count))
                                    recSearch(X, Y, index + 1, i);
                                else
                                {
                                    GroupId++;
                                    InserttoPatternValue();
                                    return;
                                }
                            }
                            else if (Op2.StartsWith("W") || Op2.StartsWith("M"))
                            {
                                foreach (DataColumn dc2 in dtTargetdt.Columns)
                                {
                                    if (dc2.Caption.StartsWith(Op2))
                                    {
                                        No2 = Convert.ToInt32(dtTargetdt.Rows[DrawNo][dc2]);
                                        if (Op.Trim() == "+")
                                            Value = No1 + No2;
                                        else if (Op.Trim() == "-")
                                            Value = No1 - No2;
                                        else
                                            Value = Math.Abs(No1 - No2);
                                        row["SNo"] = dtTargetdt.Rows[DrawNo]["SNo"];
                                        row["Col"] = dc2.Caption + " ";
                                        row["Value"] = Value;
                                        if (!(index + 1 == dtPettern.Rows.Count))
                                            recSearch(X, Y, index + 1, i);
                                        else
                                        {
                                            GroupId++;
                                            InserttoPatternValue();
                                            return;
                                        }
                                    }
                                }



                            }
                            else
                            {
                                DataRow[] rowPatterns = dtPettern.Select("No = '" + Op2 + "'");
                                foreach (DataRow rowp in rowPatterns)
                                {
                                    No2 = Convert.ToInt32(rowp["Value"]);
                                    if (Op.Trim() == "+")
                                        Value = No1 + No2;
                                    else if (Op.Trim() == "-")
                                        Value = No1 - No2;
                                    else
                                        Value = Math.Abs(No1 - No2);
                                    row["SNo"] = rowp["SNo"];
                                    row["Col"] = rowp["Col"].ToString();
                                    row["Value"] = Value;
                                    if (!(index + 1 == dtPettern.Rows.Count))
                                        recSearch(X, Y, index + 1, i);
                                    else
                                    {
                                        GroupId++;
                                        InserttoPatternValue();
                                        return;
                                    }
                                }

                            }
                        }
                        else
                        {
                            row["Value"] = No1;
                            if (!(index + 1 == dtPettern.Rows.Count))
                                recSearch(X, Y, index + 1, i);
                            else
                            {
                                GroupId++;
                                InserttoPatternValue();
                                return;
                            }
                        }// 
                        #endregion
                    }
                    else if (Op1.StartsWith("W") || Op1.StartsWith("M"))
                    {
                        foreach (DataColumn dc1 in dtTargetdt.Columns)
                        {
                            if (dc1.Caption.StartsWith(Op1))
                            {
                                No1 = Convert.ToInt32(dtTargetdt.Rows[DrawNo][dc1]);
                                #region Second Oprand
                                if (row["OprandSecond"] != DBNull.Value)
                                {
                                    string Op2 = row["OprandSecond"].ToString();
                                    string Op = row["Operator"].ToString();
                                    if (Op2.Trim() == "X")
                                    {
                                        No2 = X;
                                        if (Op.Trim() == "+")
                                            Value = No1 + No2;
                                        else if (Op.Trim() == "-")
                                            Value = No1 - No2;
                                        else
                                            Value = Math.Abs(No1 - No2);
                                        row["SNo"] = dtTargetdt.Rows[DrawNo]["SNo"];
                                        row["Col"] = dc1.Caption;
                                        row["Value"] = Value;
                                        if (!(index + 1 == dtPettern.Rows.Count))
                                            recSearch(X, Y, index + 1, i);
                                        else
                                        {
                                            GroupId++;
                                            InserttoPatternValue();
                                            return;
                                        }
                                    }
                                    else if (Op2.Trim() == "Y")
                                    {
                                        No2 = Y;
                                        if (Op.Trim() == "+")
                                            Value = No1 + No2;
                                        else if (Op.Trim() == "-")
                                            Value = No1 - No2;
                                        else
                                            Value = Math.Abs(No1 - No2);
                                        row["SNo"] = dtTargetdt.Rows[DrawNo]["SNo"];
                                        row["Col"] = dc1.Caption;
                                        row["Value"] = Value;
                                        if (!(index + 1 == dtPettern.Rows.Count))
                                            recSearch(X, Y, index + 1, i);
                                        else
                                        {
                                            GroupId++;
                                            InserttoPatternValue();
                                            return;
                                        }
                                    }
                                    else if (Op2.StartsWith("W") || Op2.StartsWith("M"))
                                    {



                                        foreach (DataColumn dc2 in dtTargetdt.Columns)
                                        {
                                            if (dc2.Caption.StartsWith(Op2) && dc1.Caption != dc2.Caption)
                                            {
                                                No2 = Convert.ToInt32(dtTargetdt.Rows[DrawNo][dc2]);
                                                if (Op.Trim() == "+")
                                                    Value = No1 + No2;
                                                else if (Op.Trim() == "-")
                                                    Value = No1 - No2;
                                                else
                                                    Value = Math.Abs(No1 - No2);
                                                row["SNo"] = dtTargetdt.Rows[DrawNo]["SNo"];
                                                row["Col"] = dc1.Caption + " " + dc2.Caption + " ";
                                                row["Value"] = Value;
                                                if (!(index + 1 == dtPettern.Rows.Count))
                                                    recSearch(X, Y, index + 1, i);
                                                else
                                                {
                                                    GroupId++;
                                                    InserttoPatternValue();
                                                    return;
                                                }
                                            }
                                        }

                                        //}//

                                    }
                                    else
                                    {
                                        DataRow[] rowPatterns = dtPettern.Select("No = '" + Op2 + "'");
                                        foreach (DataRow rowp in rowPatterns)
                                        {
                                            No2 = Convert.ToInt32(rowp["Value"]);
                                            if (Op.Trim() == "+")
                                                Value = No1 + No2;
                                            else if (Op.Trim() == "-")
                                                Value = No1 - No2;
                                            else
                                                Value = Math.Abs(No1 - No2);
                                            row["SNo"] = rowp["SNo"];
                                            row["Col"] = dc1.Caption + " " + rowp["Col"].ToString();
                                            row["Value"] = Value;
                                            if (!(index + 1 == dtPettern.Rows.Count))
                                                recSearch(X, Y, index + 1, i);
                                            else
                                            {
                                                GroupId++;
                                                InserttoPatternValue();
                                                return;
                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    row["SNo"] = dtTargetdt.Rows[DrawNo]["SNo"];
                                    row["Col"] = dc1.Caption + " ";
                                    row["Value"] = No1;
                                    if (!(index + 1 == dtPettern.Rows.Count))
                                        recSearch(X, Y, index + 1, i);
                                    else
                                    {
                                        GroupId++;
                                        InserttoPatternValue();
                                        return;
                                    }
                                }// 
                                #endregion
                            }
                        }
                    }
                    else
                    {
                        DataRow[] rowN = dtPettern.Select("No = '" + Op1 + "'");
                        foreach (DataRow rown in rowN)
                        {
                            No1 = Convert.ToInt32(rown["Value"]);
                            #region Second Oprand
                            if (row["OprandSecond"] != DBNull.Value)
                            {
                                string Op2 = row["OprandSecond"].ToString();
                                string Op = row["Operator"].ToString();
                                if (Op2.Trim() == "X")
                                {
                                    No2 = X;
                                    if (Op.Trim() == "+")
                                        Value = No1 + No2;
                                    else if (Op.Trim() == "-")
                                        Value = No1 - No2;
                                    else
                                        Value = Math.Abs(No1 - No2);
                                    row["SNo"] = rown["SNo"];
                                    row["Col"] = rown["Col"].ToString() + " ";
                                    row["Value"] = Value;
                                    if (!(index + 1 == dtPettern.Rows.Count))
                                        recSearch(X, Y, index + 1, i);
                                    else
                                    {
                                        GroupId++;
                                        InserttoPatternValue();
                                        return;
                                    }
                                }
                                else if (Op2.Trim() == "Y")
                                {
                                    No2 = Y;
                                    if (Op.Trim() == "+")
                                        Value = No1 + No2;
                                    else if (Op.Trim() == "-")
                                        Value = No1 - No2;
                                    else
                                        Value = Math.Abs(No1 - No2);
                                    row["SNo"] = rown["SNo"];
                                    row["Col"] = rown["Col"].ToString() + " ";
                                    row["Value"] = Value;
                                    if (!(index + 1 == dtPettern.Rows.Count))
                                        recSearch(X, Y, index + 1, i);
                                    else
                                    {
                                        GroupId++;
                                        InserttoPatternValue();
                                        return;
                                    }
                                }
                                else if (Op2.StartsWith("W") || Op2.StartsWith("M"))
                                {
                                    foreach (DataColumn dc2 in dtTargetdt.Columns)
                                    {
                                        if (dc2.Caption.StartsWith(Op2))
                                        {
                                            No2 = Convert.ToInt32(dtTargetdt.Rows[DrawNo][dc2]);
                                            if (Op.Trim() == "+")
                                                Value = No1 + No2;
                                            else if (Op.Trim() == "-")
                                                Value = No1 - No2;
                                            else
                                                Value = Math.Abs(No1 - No2);
                                            row["SNo"] = rown["SNo"];
                                            row["Col"] = rown["Col"].ToString() + " " + dc2.Caption;
                                            row["Value"] = Value;
                                            if (!(index + 1 == dtPettern.Rows.Count))
                                                recSearch(X, Y, index + 1, i);
                                            else
                                            {
                                                GroupId++;
                                                InserttoPatternValue();
                                                return;
                                            }
                                        }
                                    }



                                }
                                else
                                {
                                    DataRow[] rowPatterns = dtPettern.Select("No = '" + Op2 + "'");
                                    foreach (DataRow rowp in rowPatterns)
                                    {
                                        No2 = Convert.ToInt32(rowp["Value"]);
                                        if (Op.Trim() == "+")
                                            Value = No1 + No2;
                                        else if (Op.Trim() == "-")
                                            Value = No1 - No2;
                                        else
                                            Value = Math.Abs(No1 - No2);
                                        row["SNo"] = rown["SNo"];
                                        row["Col"] = rown["Col"].ToString() + " " + rowp["Col"].ToString();
                                        row["Value"] = Value;
                                        if (!(index + 1 == dtPettern.Rows.Count))
                                            recSearch(X, Y, index + 1, i);
                                        else
                                        {
                                            GroupId++;
                                            InserttoPatternValue();
                                            return;
                                        }
                                    }

                                }
                            }
                            else
                            {
                                row["SNo"] = rown["SNo"];
                                row["Col"] = rown["Col"].ToString() + " ";
                                row["Value"] = No1;
                                if (!(index + 1 == dtPettern.Rows.Count))
                                    recSearch(X, Y, index + 1, i);
                                else
                                {
                                    GroupId++;
                                    InserttoPatternValue();
                                    return;
                                }
                            }// 
                            #endregion
                        }
                    }

                }// 
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InserttoPatternValue()
        {
            try
            {
                int i = 0;
                foreach (DataRow row in dtPettern.Rows)
                {
                    i++;
                    DataRow rowPatternValue = dtPatternValues.NewRow();
                    rowPatternValue["Id"] = dtPatternValues.Rows.Count + 1;
                    rowPatternValue["No"] = row["No"];
                    rowPatternValue["Value"] = row["Value"];
                    rowPatternValue["DrawNo"] = "";
                    rowPatternValue["col1"] = "";
                    rowPatternValue["col2"] = "";
                    rowPatternValue["GroupId"] = GroupId;
                    rowPatternValue["col1"] = row["Col"];
                    rowPatternValue["DrawNo"] = row["SNo"];
                    dtPatternValues.Rows.Add(rowPatternValue);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int i = NumberList.SelectedIndex;
                if (i != -1)
                {
                    NumberList.Items.RemoveAt(i);
                    cmbNo1.Items.RemoveAt(i);
                    cmbNo2.Items.RemoveAt(i);
                    cmbPairNo1.Items.RemoveAt(i);
                    cmbPairNo2.Items.RemoveAt(i);
                    cmbFirstOprd.Items.RemoveAt(i + 14);
                    cmbSecOprd.Items.RemoveAt(i + 15);
                    dtPettern.Rows[i].Delete();
                    dtPettern.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                            if (searchGrid.Columns[e.ColumnIndex].DataPropertyName == row["col"].ToString() && (searchGrid.Columns[e.ColumnIndex].DataPropertyName.StartsWith("W") || searchGrid.Columns[e.ColumnIndex].DataPropertyName.StartsWith("M")))
                            {

                                if (Convert.ToInt32(searchGrid.Rows[e.RowIndex].Cells["SNo"].Value) == Convert.ToInt32(row["SNo"]))
                                {
                                    if (row["flag"].ToString() == "Y")
                                        e.CellStyle.BackColor = Color.Yellow;
                                    else
                                        e.CellStyle.BackColor = Color.Brown;
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

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void rdInternal_CheckedChanged(object sender, EventArgs e)
        {
            if (rdInternal.Checked)
            {
                LoadDataSet();
            }
        }

        private void KeySearchNew()
        {
            try
            {
                DataTable dtTemp = ds.Tables[0].Copy();
                dtTemp.DefaultView.RowFilter = "W1 is NOT null";
                dtTargetdt = dtTemp.DefaultView.ToTable();
                int lastI = 0;
                #region Prepare dtForcastNum
                DataTable dtForcastNum = new DataTable();
                dtForcastNum.Columns.Add("Id");
                dtForcastNum.Columns.Add("No1");
                dtForcastNum.Columns.Add("No2");
                dtForcastNum.Columns.Add("Col1");
                dtForcastNum.Columns.Add("Col2");
                dtForcastNum.Columns.Add("Op1");
                dtForcastNum.Columns.Add("Op2");
                dtForcastNum.Columns.Add("Desc");
                #endregion

                dtSummary.Rows.Clear();
                dtNumFound.Rows.Clear();
                string[] Ops = { "+", "-", "~" };
                int X = 0, Y = 0;
                progressBar1.Maximum = 500;
                progressBar1.Value = 0;
                int keyCount=0;
                foreach (DataRow rowKey in dtKeys.Rows)
                {
                    keyCount++;
                    progressBar1.Value = keyCount%500;
                    X = (int)rowKey["X"];
                    Y = (int)rowKey["Y"];
                    bool bBreak = false;
                    for (int cnt = 0; cnt < dtTargetdt.Rows.Count; cnt++)
                    {
                        dtForcastNum.Rows.Clear();

                        #region R1
                        foreach (DataColumn dc1 in dtTargetdt.Columns)
                        {
                            string dcCap = dc1.Caption.Substring(0, 1);
                            if (dcCap == "W" || dcCap == "M")
                            {
                                foreach (string op in Ops)
                                {
                                   
                                    int No1 = X + (int)dtTargetdt.Rows[cnt][dc1];
                                    string Desc = "No1 = X + " + dc1.Caption;
                                    if (op == "-")
                                    {
                                        No1 = X - (int)dtTargetdt.Rows[cnt][dc1];
                                        Desc = "No1 = X - " + dc1.Caption;
                                    }
                                    if (op == "~")
                                    {
                                        No1 = (int)dtTargetdt.Rows[cnt][dc1] - X;
                                        Desc = "No1 = " + dc1.Caption + " - X";
                                    }
                                    if (No1 < 1 || No1 > 90)
                                        continue;
                                    foreach (DataColumn dc2 in dtTargetdt.Columns)
                                    {
                                        if (dc2.Caption.StartsWith(dcCap) && dc1.Caption != dc2.Caption)
                                        {
                                            foreach (string op1 in Ops)
                                            {
                                                string DescS = "";
                                                int No2 = Y + (int)dtTargetdt.Rows[cnt][dc2];
                                                if(op1 == "+")
                                                    DescS = " and " + "No2 = Y + " + dc2.Caption;
                                                if (op1 == "-")
                                                {
                                                    No2 = Y - (int)dtTargetdt.Rows[cnt][dc2];
                                                    DescS = " and " + "No2 = Y - " + dc2.Caption;
                                                }
                                                if (op1 == "~")
                                                {
                                                    No2 = (int)dtTargetdt.Rows[cnt][dc2] - Y;
                                                    DescS = " and " + "No2 = " + dc2.Caption + " - Y";
                                                }
                                                if (No2 < 1 || No2 > 90)
                                                    continue;
                                                DataRow rForcast = dtForcastNum.NewRow();
                                                rForcast["Id"] = dtForcastNum.Rows.Count + 1;
                                                rForcast["No1"] = No1;
                                                rForcast["No2"] = No2;
                                                rForcast["Col1"] = dc1.Caption;
                                                rForcast["Col2"] = dc2.Caption;
                                                rForcast["Op1"] = op;
                                                rForcast["Op2"] = op1;
                                                rForcast["Desc"] = Desc + DescS;
                                                dtForcastNum.Rows.Add(rForcast);
                                            }

                                        }
                                    }
                                }
                            }
                        }// 
                        #endregion

                        if (dtForcastNum.Rows.Count > 0)
                        {
                            for (int j = 1; j < 10; j++)
                            {
                                if (cnt + j < dtTargetdt.Rows.Count)
                                {

                                    #region Match First Group
                                    DataRow rowForcast = dtForcastNum.NewRow();
                                    foreach (DataRow rForcast in dtForcastNum.Rows)
                                    {
                                        int No1 = Convert.ToInt32(rForcast["No1"]);
                                        int No2 = Convert.ToInt32(rForcast["No2"]);
                                       
                                        #region Check For Valid Group
                                        bool cont = true;
                                        for (int k = j; k <= 1; k++)
                                        {
                                            string cl1 = rForcast["Col1"].ToString();
                                            string cl2 = rForcast["Col2"].ToString();
                                            string O1 = rForcast["Op1"].ToString();
                                            string O2 = rForcast["Op2"].ToString();

                                            int N1 = Convert.ToInt32(dtTargetdt.Rows[dtTargetdt.Rows.Count - k][cl1]);
                                            int N2 = Convert.ToInt32(dtTargetdt.Rows[dtTargetdt.Rows.Count - k][cl2]);

                                            if (O1 == "+")
                                                N1 = N1 + X;
                                            else if (O1 == "-")
                                                N1 = X - N1;
                                            else
                                                N1 = N1 - X;

                                            if (O2 == "+")
                                                N2 = N2 + Y;
                                            else if (O2 == "-")
                                                N2 = Y - N2;
                                            else
                                                N2 = N2 - Y;
                                            if (N1 > 0 && N1 <= 90 && N2 > 1 && N2 <= 90)
                                            {
                                                cont = false;
                                                break;
                                            }
                                        }
                                        if (cont)
                                            continue;

                                         #endregion

                                        foreach (DataColumn dc1 in dtTargetdt.Columns)
                                        {
                                            //if (dc1.Caption.StartsWith("W") || dc1.Caption.StartsWith("M"))
                                            if (dc1.Caption.StartsWith("W"))
                                            {
                                                foreach (DataColumn dc2 in dtTargetdt.Columns)
                                                {
                                                    //if ((dc2.Caption.StartsWith("W") || dc2.Caption.StartsWith("M")) && dc1.Caption != dc2.Caption)
                                                    if (dc2.Caption.StartsWith("W") && dc1.Caption != dc2.Caption)
                                                    {
                                                        //string dcCap = dc1.Caption.Substring(0, 1);
                                                        //if (dc2.Caption.StartsWith(dcCap))
                                                        {
                                                            if (No1 == (int)dtTargetdt.Rows[cnt + j][dc1] && No2 == (int)dtTargetdt.Rows[cnt + j][dc2])
                                                            {
                                                                

                                                                bBreak = true;
                                                                foreach (DataColumn c in dtForcastNum.Columns)
                                                                    rowForcast[c] = rForcast[c];

                                                                #region Add rows to dtNumfound

                                                                DataRow rowNumbers = dtNumFound.NewRow();
                                                                rowNumbers["Id"] = dtNumFound.Rows.Count + 1;
                                                                rowNumbers["col"] = rForcast["Col1"];
                                                                rowNumbers["SNo"] = dtTargetdt.Rows[cnt]["SNo"];
                                                                rowNumbers["flag"] = "B";
                                                                dtNumFound.Rows.Add(rowNumbers);

                                                                DataRow rowNumbers1 = dtNumFound.NewRow();
                                                                rowNumbers1["Id"] = dtNumFound.Rows.Count + 1;
                                                                rowNumbers1["col"] = rForcast["Col2"];
                                                                rowNumbers1["SNo"] = dtTargetdt.Rows[cnt]["SNo"];
                                                                rowNumbers1["flag"] = "B";
                                                                dtNumFound.Rows.Add(rowNumbers1);

                                                                DataRow rowNumbers2 = dtNumFound.NewRow();
                                                                rowNumbers2["Id"] = dtNumFound.Rows.Count + 1;
                                                                rowNumbers2["col"] = dc1.Caption;
                                                                rowNumbers2["SNo"] = dtTargetdt.Rows[cnt + j]["SNo"];
                                                                rowNumbers2["flag"] = "Y";
                                                                dtNumFound.Rows.Add(rowNumbers2);

                                                                DataRow rowNumbers3 = dtNumFound.NewRow();
                                                                rowNumbers3["Id"] = dtNumFound.Rows.Count + 1;
                                                                rowNumbers3["col"] = dc2.Caption;
                                                                rowNumbers3["SNo"] = dtTargetdt.Rows[cnt + j]["SNo"];
                                                                rowNumbers3["flag"] = "Y";
                                                                dtNumFound.Rows.Add(rowNumbers3);
                                                                #endregion

                                                                #region Add row to dtSummary
                                                                DataRow rowSummary = dtSummary.NewRow();
                                                                rowSummary["Id"] = dtSummary.Rows.Count + 1;
                                                                rowSummary["XY"] = X.ToString() + " , " + Y.ToString();
                                                                rowSummary["CMatch"] = dtTargetdt.Rows[cnt]["SNo"].ToString();
                                                                rowSummary["Desc"] = rForcast["Desc"].ToString() + " are played after " + j.ToString() + " weeks";
                                                                rowSummary["DBName"] = SqlClass.GetDBNameById((int)dtTargetdt.Rows[0]["DBId"]);
                                                                dtSummary.Rows.Add(rowSummary);

                                                                #endregion
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            if (bBreak)
                                                break;
                                        }
                                        if (bBreak)
                                            break;
                                    }
                                    #endregion

                                    if (bBreak)
                                    {
                                        #region Match Other Group
                                        bBreak = false;
                                        for (int i = cnt + j + 1; i < dtTargetdt.Rows.Count; i++)
                                        {
                                            bool bbBreak = false;
                                            string Col1 = rowForcast["Col1"].ToString();
                                            string Col2 = rowForcast["Col2"].ToString();
                                            string Op1 = rowForcast["Op1"].ToString();
                                            string Op2 = rowForcast["Op2"].ToString();

                                            int No1 = Convert.ToInt32(dtTargetdt.Rows[i][Col1]);
                                            int No2 = Convert.ToInt32(dtTargetdt.Rows[i][Col2]);

                                            if (Op1 == "+")
                                                No1 = No1 + X;
                                            else if (Op1 == "-")
                                                No1 = X - No1;
                                            else
                                                No1 = No1 - X;

                                            if (Op2 == "+")
                                                No2 = No2 +Y;
                                            else if (Op2 == "-")
                                                No2 = Y - No2;
                                            else
                                                No2 = No2 - Y;
                                            if (No1 > 0 && No2 > 0 && No1 < 91 && No2 < 91)
                                            {
                                                foreach (DataColumn dc1 in dtTargetdt.Columns)
                                                {
                                                    //if (dc1.Caption.StartsWith("W") || dc1.Caption.StartsWith("M"))
                                                    if (dc1.Caption.StartsWith("W"))
                                                    {
                                                        //string dcCap = dc1.Caption.Substring(0, 1);
                                                        foreach (DataColumn dc2 in dtTargetdt.Columns)
                                                        {
                                                            //if (dc2.Caption.StartsWith(dcCap))
                                                            {
                                                                //if ((dc2.Caption.StartsWith("W") || dc2.Caption.StartsWith("M")) && dc1.Caption != dc2.Caption)
                                                                if (dc2.Caption.StartsWith("W") && dc1.Caption != dc2.Caption)
                                                                {

                                                                    if (i + j < dtTargetdt.Rows.Count)
                                                                    {
                                                                        if (No1 == (int)dtTargetdt.Rows[i + j][dc1] && No2 == (int)dtTargetdt.Rows[i + j][dc2])
                                                                        {
                                                                            bBreak = true;
                                                                            bbBreak = true;
                                                                            #region Add rows to dtNumfound

                                                                            DataRow rowNumbers = dtNumFound.NewRow();
                                                                            rowNumbers["Id"] = dtNumFound.Rows.Count + 1;
                                                                            rowNumbers["col"] = rowForcast["Col1"];
                                                                            rowNumbers["SNo"] = dtTargetdt.Rows[i]["SNo"];
                                                                            rowNumbers["flag"] = "B";
                                                                            dtNumFound.Rows.Add(rowNumbers);

                                                                            DataRow rowNumbers1 = dtNumFound.NewRow();
                                                                            rowNumbers1["Id"] = dtNumFound.Rows.Count + 1;
                                                                            rowNumbers1["col"] = rowForcast["Col2"];
                                                                            rowNumbers1["SNo"] = dtTargetdt.Rows[i]["SNo"];
                                                                            rowNumbers1["flag"] = "B";
                                                                            dtNumFound.Rows.Add(rowNumbers1);

                                                                            DataRow rowNumbers2 = dtNumFound.NewRow();
                                                                            rowNumbers2["Id"] = dtNumFound.Rows.Count + 1;
                                                                            rowNumbers2["col"] = dc1.Caption;
                                                                            rowNumbers2["SNo"] = dtTargetdt.Rows[i + j]["SNo"];
                                                                            rowNumbers2["flag"] = "Y";
                                                                            dtNumFound.Rows.Add(rowNumbers2);

                                                                            DataRow rowNumbers3 = dtNumFound.NewRow();
                                                                            rowNumbers3["Id"] = dtNumFound.Rows.Count + 1;
                                                                            rowNumbers3["col"] = dc2.Caption;
                                                                            rowNumbers3["SNo"] = dtTargetdt.Rows[i + j]["SNo"];
                                                                            rowNumbers3["flag"] = "Y";
                                                                            dtNumFound.Rows.Add(rowNumbers3);
                                                                            #endregion

                                                                            dtSummary.Rows[0]["CMatch"] += "," + dtTargetdt.Rows[i]["SNo"].ToString();
                                                                           
                                                                            break;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            if (bbBreak)
                                                                break;
                                                        }
                                                    }
                                                    if (bbBreak)
                                                        break;
                                                }
                                            }
                                            lastI = i;
                                        }
                                        if (!bBreak)
                                        {
                                            if (dtNumFound.Rows.Count > 3)
                                            {
                                                for (int i = 0; i < 4; i++)
                                                {
                                                    dtNumFound.Rows[dtNumFound.Rows.Count - 1].Delete();
                                                    dtNumFound.AcceptChanges();
                                                }
                                                dtSummary.Rows.Clear();
                                            }
                                        }//
                                        else
                                        {
                                            break;
                                        }
                                        #endregion
                                    }
                                }
                                if (bBreak)
                                {
                                    break;
                                }
                            }
                        }
                        if (bBreak)
                        {
                            break;
                        }
                    }
                    if (bBreak)
                    {
                        break;
                    }
                }
                progressBar1.Value = 0;
                grdSummary.DataSource = dtSummary;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
