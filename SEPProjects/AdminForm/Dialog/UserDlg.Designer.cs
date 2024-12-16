namespace AdminForm.Dialog
{
    partial class UserDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbDept1 = new System.Windows.Forms.ComboBox();
            this.cmbDept2 = new System.Windows.Forms.ComboBox();
            this.cmbDept3 = new System.Windows.Forms.ComboBox();
            this.dgUser = new System.Windows.Forms.DataGridView();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgUser)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbDept1
            // 
            this.cmbDept1.FormattingEnabled = true;
            this.cmbDept1.Location = new System.Drawing.Point(13, 13);
            this.cmbDept1.Name = "cmbDept1";
            this.cmbDept1.Size = new System.Drawing.Size(121, 20);
            this.cmbDept1.TabIndex = 0;
            this.cmbDept1.SelectedIndexChanged += new System.EventHandler(this.cmbDept1_SelectedIndexChanged);
            // 
            // cmbDept2
            // 
            this.cmbDept2.FormattingEnabled = true;
            this.cmbDept2.Location = new System.Drawing.Point(141, 12);
            this.cmbDept2.Name = "cmbDept2";
            this.cmbDept2.Size = new System.Drawing.Size(121, 20);
            this.cmbDept2.TabIndex = 1;
            this.cmbDept2.SelectedIndexChanged += new System.EventHandler(this.cmbDept2_SelectedIndexChanged);
            // 
            // cmbDept3
            // 
            this.cmbDept3.FormattingEnabled = true;
            this.cmbDept3.Location = new System.Drawing.Point(269, 12);
            this.cmbDept3.Name = "cmbDept3";
            this.cmbDept3.Size = new System.Drawing.Size(121, 20);
            this.cmbDept3.TabIndex = 2;
            this.cmbDept3.SelectedIndexChanged += new System.EventHandler(this.cmbDept3_SelectedIndexChanged);
            // 
            // dgUser
            // 
            this.dgUser.AllowUserToAddRows = false;
            this.dgUser.AllowUserToDeleteRows = false;
            this.dgUser.AllowUserToResizeRows = false;
            this.dgUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgUser.CausesValidation = false;
            this.dgUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgUser.Location = new System.Drawing.Point(13, 39);
            this.dgUser.MultiSelect = false;
            this.dgUser.Name = "dgUser";
            this.dgUser.ReadOnly = true;
            this.dgUser.RowTemplate.Height = 23;
            this.dgUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgUser.Size = new System.Drawing.Size(574, 302);
            this.dgUser.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(116, 364);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(102, 40);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(363, 364);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(102, 40);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // UserDlg
            // 
            this.ClientSize = new System.Drawing.Size(599, 416);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dgUser);
            this.Controls.Add(this.cmbDept3);
            this.Controls.Add(this.cmbDept2);
            this.Controls.Add(this.cmbDept1);
            this.Name = "UserDlg";
            this.Text = "员工选择";
            this.Load += new System.EventHandler(this.UserDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgUser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDept1;
        private System.Windows.Forms.ComboBox cmbDept2;
        private System.Windows.Forms.ComboBox cmbDept3;
        private System.Windows.Forms.DataGridView dgUser;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}