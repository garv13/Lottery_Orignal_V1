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
    public partial class ResultForm : Form
    {
        public ResultForm()
        {
            InitializeComponent();
        }

        private void fillResultGrid()
        {
            try
            {
                if (cmbFilter.SelectedIndex == 1)
                {
                    Date.DefaultCellStyle.Format = "dd/MM/yyyy";
                    grdResult.DataSource = SqlClass.GetForCastResult(true);
                }
                else
                {
                    Date.DefaultCellStyle.Format = "dd/MM/yyyy";
                    grdResult.DataSource = SqlClass.GetForCastResult(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillResultGrid();
        }

        private void ResultForm_Load(object sender, EventArgs e)
        {
            fillResultGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string Ids = "";
                for (int Id = 0; Id < grdResult.SelectedRows.Count; Id++)
                {
                    if(Ids == "")
                        Ids = grdResult.SelectedRows[Id].Cells["Id"].Value.ToString();
                    else
                        Ids += "," + grdResult.SelectedRows[Id].Cells["Id"].Value.ToString();
                }
                if (Ids != "")
                {
                    SqlClass.DeleteForcastResults(Ids);
                    fillResultGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    
}
