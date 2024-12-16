using AdminForm.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminForm.Finance
{
    public partial class FormHandOver : Form
    {
        DataTable dtNewRow = null;

        public FormHandOver()
        {
            InitializeComponent();
        }

        private DataTable LoadDT()
        {
            dtNewRow = new DataTable();
            dtNewRow.Columns.Add("ID");
            dtNewRow.Columns.Add("编号");
            dtNewRow.Columns.Add("公司名称");

            return dtNewRow;
        }

        private AdminForm.Model.UserInfo DimissionUser = null;
        private AdminForm.Model.UserInfo ReceiverUser = null;

        private void FormHandOver_Load(object sender, EventArgs e)
        {
            dgBranch.DataSource = LoadDT();
            dgBranch.Columns["ID"].Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnSelect1_Click(object sender, EventArgs e)
        {
            AdminForm.Dialog.UserDlg dlg = new Dialog.UserDlg();
            dlg.ShowDialog();
            lblDimission.Text = dlg.SelectedUser.LastNameCN + dlg.SelectedUser.FirstNameCN + "[" + dlg.SelectedEmployee.Code + "]";
            DimissionUser = dlg.SelectedUser;
            dlg.Close();

            var branchList = BranchManager.GetList(x =>x.FirstFinanceID ==dlg.SelectedUser.UserID);

            foreach (var branch in branchList)
            {
                DataRow dr = dtNewRow.NewRow();
                dr["ID"] = branch.BranchID;
                dr["编号"] = branch.BranchCode;
                dr["公司名称"] = branch.BranchName;

                dtNewRow.Rows.Add(dr);
            }

            dgBranch.DataSource = dtNewRow;
            dgBranch.Columns["ID"].Visible = false;

        }

        private void btnSelect2_Click(object sender, EventArgs e)
        {
            AdminForm.Dialog.UserDlg dlg = new Dialog.UserDlg();
            dlg.ShowDialog();
            lblReceiver.Text = dlg.SelectedUser.LastNameCN + dlg.SelectedUser.FirstNameCN + "[" + dlg.SelectedEmployee.Code + "]";
            ReceiverUser = dlg.SelectedUser;

            dlg.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.DimissionUser == null || this.ReceiverUser == null)
            {
                MessageBox.Show("请选择离职人员或交接人");
                return;
            }
            if (this.dgBranch.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择分公司");
                return;
            }
            
            int branchId = int.Parse(this.dgBranch.SelectedRows[0].Cells[0].Value.ToString());

            HandOverManager.FirstFinanceHandOver(DimissionUser, ReceiverUser,branchId);

        }
    }
}
