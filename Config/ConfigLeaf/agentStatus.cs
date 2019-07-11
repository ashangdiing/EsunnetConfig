using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Config.ConfigLeaf
{
   public  class agentStatus
    {
       public Tools.DataBase db;
        public DataGridView DriveDataGridView;
        public DataGridViewTextBoxColumn caseId;
        public DataGridViewTextBoxColumn description;
       // public DataGridViewTextBoxColumn serverFlag;
   
        public DataGridViewColumn[] Columns;
        public void initColumns()
        {



            caseId = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.caseId.HeaderText = "组名";
            this.caseId.Name = "caseId";
            this.caseId.DataPropertyName = "caseId";
            this.caseId.ReadOnly = false;
            this.caseId.Width = 100;
            this.caseId.Visible = false;



            description = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.description.HeaderText = "组名";
            this.description.Name = "description";
            this.description.DataPropertyName = "description";
            this.description.ReadOnly = false;
            this.description.Width = 100;
            this.description.Visible = true;




            Columns = new System.Windows.Forms.DataGridViewColumn[]{
           caseId,
           description
            };

        }

        public void clearDriveDataGridView()
        {

            DriveDataGridView.Columns.Clear();
            this.DriveDataGridView.AllowUserToAddRows = true;
            this.DriveDataGridView.AllowUserToDeleteRows = true;
            this.DriveDataGridView.AllowUserToResizeColumns = false;
            this.DriveDataGridView.ReadOnly = false;
            this.DriveDataGridView.AllowUserToResizeRows = false;
            this.DriveDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.DriveDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        }
        public void showDataGridView(System.Windows.Forms.DataGridView DataGridView)
        {
            this.DriveDataGridView = DataGridView;
            clearDriveDataGridView();
            initColumns();
            DataGridView.Columns.AddRange(Columns);
            DataGridView.CellValueChanged +=DriveDataGridView_CellValueChanged;
            //  DriveDataGridView.CellClick += DriveDataGridView_CellClick;
            bindDatagridView();
        }

        private void DriveDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
          
        }
        public void bindDatagridView()
        {
            db = new Tools.DataBase();
            SqlConnection sqlcon = db.getConnection();

            SqlDataAdapter sqldap = new SqlDataAdapter("  select TS_CAUSE_ID as caseId,TS_DESCRIPTION as description from TS_LEAVE_DESCRIPTION ", sqlcon);        
           // sqldap.SelectCommand.Parameters.Add(new SqlParameter("groupId", groupId.Trim()));
            DataSet ds = new DataSet();

            sqldap.Fill(ds);

            this.DriveDataGridView.DataSource = ds.Tables[0];

        }

    }
}
