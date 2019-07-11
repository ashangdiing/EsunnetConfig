using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Config.ConfigLeaf.agentGroup
{
    class agentGroup
    {
        public Tools.DataBase db;
        public DataGridView DataGridView;
        public DataGridViewTextBoxColumn agentId;
        public DataGridViewTextBoxColumn agentName;
        public DataGridViewTextBoxColumn agentDescription;
        public DataGridViewButtonColumn modifyAgentMember;
        public DataGridViewColumn[] Columns;
        public void initColumns()
        {
            


            agentName = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.agentName.HeaderText = "组名";
            this.agentName.Name = "agentName";
            this.agentName.DataPropertyName = "agentName";
            this.agentName.ReadOnly = false;
            this.agentName.Width = 100;
            this.agentName.Visible = true;

            // key
            agentDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.agentDescription.HeaderText = "描述";
            this.agentDescription.Name = "agentDescription";
            this.agentDescription.DataPropertyName = "agentDescription";
            this.agentDescription.ReadOnly = false;
            this.agentDescription.Width = 150;
            this.agentDescription.Visible = true;

            modifyAgentMember = new System.Windows.Forms.DataGridViewButtonColumn();
            this.modifyAgentMember.HeaderText = "操作";
            this.modifyAgentMember.Name = "modifyAgentMember";
            this.modifyAgentMember.ReadOnly = true;
            this.modifyAgentMember.Width = 80;
            this.modifyAgentMember.Text = "修改组成员";
            this.modifyAgentMember.UseColumnTextForButtonValue = true;



            agentId = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.agentId.HeaderText = "隐藏编号";
            this.agentId.Name = "agentId";
            this.agentId.DataPropertyName = "agentId";
            this.agentId.Visible = false;



            Columns = new System.Windows.Forms.DataGridViewColumn[]{
               
          agentName,
           agentDescription,
           modifyAgentMember,
            agentId
            };
        }

        public void showDataGridView(System.Windows.Forms.DataGridView DataGridView)
        {

            this.DataGridView = DataGridView;
            clearDriveDataGridView();
            initColumns();
            DataGridView.Columns.AddRange(Columns);
            DataGridView.CellValueChanged +=DriveDataGridView_CellValueChanged;
            DataGridView.CellClick += DriveDataGridView_CellClick;    
            bindDatagridView();
        }

        private void DriveDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            string agentName = "", agentDescription = "", agentId=""; 
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DataGridView.Rows[e.RowIndex];
                agentName = row.Cells["agentName"].Value.ToString().Trim();
                agentId = row.Cells["agentId"].Value.ToString().Trim();
                agentDescription = row.Cells["agentDescription"].Value.ToString().Trim();
                if (string.IsNullOrEmpty(agentName) && !string.IsNullOrEmpty(agentId))
                {
                    //删除
                    delteDrive(agentId);
                    DataGridView.Rows.RemoveAt(e.RowIndex);
                }
                else {
                    addOrUpdate( agentName, agentId, agentDescription);
                    row.Cells["agentName"].Value = agentName;
                    row.Cells["agentId"].Value = agentName;
                    row.Cells["agentDescription"].Value = agentDescription;
                }
            }
        }

        private void delteDrive(string agentId)
        {
            SqlCommand sqlcmd = new SqlCommand();
            Tools.DataBase db1 = new Tools.DataBase();
            SqlConnection sqlcon = db1.getConnection();
            sqlcon.Open();
            sqlcmd.Connection = sqlcon;
            sqlcmd.Parameters.Add(new SqlParameter("agentId", agentId.Trim()));
            sqlcmd.CommandText = "delete from [TS_AGENT_GROUP] where TS_GROUP_ID=@agentId";
            int x = sqlcmd.ExecuteNonQuery();
            sqlcmd.Dispose();
            sqlcon.Dispose();

        }
        private void addOrUpdate(string agentName,string agentId,string agentDescription)
        {
            
            SqlCommand sqlcmd = new SqlCommand();
            Tools.DataBase db1 = new Tools.DataBase();
            SqlConnection sqlcon = db1.getConnection();
           
            sqlcon.Open();
            sqlcmd.Connection = sqlcon;
            sqlcmd.Parameters.Add(new SqlParameter("agentName", agentName));
            sqlcmd.Parameters.Add(new SqlParameter("agentDescription", agentDescription));


            if (string.IsNullOrEmpty(agentId))
            {


                sqlcmd.CommandText = " insert into TS_AGENT_GROUP(TS_GROUP_ID,TS_GROUP_NAME,TS_DESCRIPTION) values(@agentName,@agentName,@agentDescription)";

            }
            else
            {
                //修改
                sqlcmd.Parameters.Add(new SqlParameter("agentId", agentId));
                sqlcmd.CommandText = " update TS_AGENT_GROUP set TS_GROUP_NAME=@agentName,TS_DESCRIPTION=@agentDescription where TS_GROUP_ID=@agentId";

            }
            int x = sqlcmd.ExecuteNonQuery();
            sqlcmd.Dispose();
            sqlcon.Dispose();
        }
        public void clearDriveDataGridView()
        {

            DataGridView.Columns.Clear();
            this.DataGridView.AllowUserToAddRows = true;
            this.DataGridView.AllowUserToDeleteRows = true;
            this.DataGridView.AllowUserToResizeColumns = false;
            this.DataGridView.ReadOnly = false;
            this.DataGridView.AllowUserToResizeRows = false;
            this.DataGridView.BackgroundColor = System.Drawing.Color.White;
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        }
        public void bindDatagridView()
        {
            db = new Tools.DataBase();
            SqlConnection sqlcon = db.getConnection();
            SqlDataAdapter sqldap = new SqlDataAdapter("SELECT [TS_GROUP_NAME] as agentName, TS_DESCRIPTION as agentDescription  , [TS_GROUP_ID] as agentId FROM [Esunnet].[dbo].[TS_AGENT_GROUP] order by TS_GROUP_ID", sqlcon);
            DataSet ds = new DataSet();
            sqldap.Fill(ds);
            this.DataGridView.DataSource = ds.Tables[0];
        }
        public EsunnetConfig esunnetConfig;
        void DriveDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;
            if (this.DataGridView.Columns[e.ColumnIndex].HeaderText != this.modifyAgentMember.HeaderText)
                return;
            if (e.RowIndex >= 0 && e.RowIndex < DataGridView.Rows.Count-1)
            {
                DataGridViewRow row = DataGridView.Rows[e.RowIndex];
                agentGroupLeaf aGL = new agentGroupLeaf();
                aGL.groupId = row.Cells["agentId"].Value.ToString().Trim();
                aGL.showDataGridView(DataGridView);
                esunnetConfig.MainConfigAreaTitle.Text = "坐席组" + aGL.groupId;
            }
        }
    }
}
