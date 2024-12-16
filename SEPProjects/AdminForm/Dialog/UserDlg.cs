using AdminForm.Manager;
using AdminForm.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminForm.Dialog
{
    public partial class UserDlg : Form
    {
        private DataTable dtNewRow = null;
        public UserDlg()
        {
            InitializeComponent();
            btnOK.DialogResult = DialogResult.OK;
            btnCancel.DialogResult = DialogResult.Cancel;
        }

        public EmployeeInfo SelectedEmployee { get; set; }
        public UserInfo SelectedUser { get; set; }


        private DataTable LoadDT()
        {
            dtNewRow = new DataTable();
            dtNewRow.Columns.Add("ID");
            dtNewRow.Columns.Add("员工编号");
            dtNewRow.Columns.Add("姓名");
            dtNewRow.Columns.Add("账号");
            dtNewRow.Columns.Add("职务");

            return dtNewRow;
        }

        private void UserDlg_Load(object sender, EventArgs e)
        {
            List<DepartmentInfo> deptList1 = DepartmentManager.GetList(x => x.ParentID == 0);
            cmbDept1.DisplayMember = "DepartmentName";
            cmbDept1.ValueMember = "DepartmentId";
            cmbDept1.DataSource = deptList1;


            dgUser.DataSource = LoadDT();
            dgUser.Columns["ID"].Visible = false;

        }

        private void cmbDept1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<DepartmentInfo> deptList2 = DepartmentManager.GetList(x => x.ParentID == int.Parse(cmbDept1.SelectedValue.ToString()));
            cmbDept2.DisplayMember = "DepartmentName";
            cmbDept2.ValueMember = "DepartmentId";
            cmbDept2.DataSource = deptList2;

        }

        private void cmbDept2_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<DepartmentInfo> deptList3 = DepartmentManager.GetList(x => x.ParentID == int.Parse(cmbDept2.SelectedValue.ToString()));
            cmbDept3.DisplayMember = "DepartmentName";
            cmbDept3.ValueMember = "DepartmentId";
            cmbDept3.DataSource = deptList3;


        }

        private void cmbDept3_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgUser.DataSource = null;
            dtNewRow.Rows.Clear();

            var positionList = EmployeesInpositionManager.GetList(x => x.DepartmentID == int.Parse(cmbDept3.SelectedValue.ToString()));
            var userList = EmployeeManager.GetList(x => x.Status == 1 && positionList.Select(p => p.UserID).Contains(x.UserID));



            foreach (var user in userList)
            {
                var umodel = UserManager.Get(user.UserID);
                DataRow dr = dtNewRow.NewRow();
                dr["ID"] = user.UserID;
                dr["员工编号"] = user.Code;
                dr["姓名"] = umodel.LastNameCN+umodel.FirstNameCN;
                dr["账号"] = umodel.Username;
                dr["职务"] = "";
                dtNewRow.Rows.Add(dr);
            }

            dgUser.DataSource = dtNewRow;
            dgUser.Columns["ID"].Visible = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int userid = int.Parse( this.dgUser.SelectedRows[0].Cells[0].Value.ToString());

            SelectedEmployee= EmployeeManager.Get(userid);
            SelectedUser = UserManager.Get(userid);



        }

    }
}
