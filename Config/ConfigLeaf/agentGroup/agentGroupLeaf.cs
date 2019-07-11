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
   public  class agentGroupLeaf
    {

        public DataGridView DriveDataGridView { get; set; }
        public DataGridViewColumn[] Columns;
        public Tools.DataBase db;
        public DataGridViewTextBoxColumn agentId;      
        public DataGridViewTextBoxColumn AgentName;
        public DataGridViewCheckBoxColumn inAgentGroup;
        public DataGridViewTextBoxColumn agentLevel;
        public void initColumns()
        {
            // key
            agentId = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.agentId.HeaderText = "编号";
            this.agentId.Name = "agentId";
            this.agentId.DataPropertyName = "agentId";
            this.agentId.ReadOnly = true;
            this.agentId.Width = 200;

            // key
            AgentName = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.AgentName.HeaderText = "名字";
            this.AgentName.Name = "AgentName";
            this.AgentName.DataPropertyName = "AgentName";
            this.AgentName.ReadOnly = true;
            this.AgentName.Width = 200;

            inAgentGroup = new DataGridViewCheckBoxColumn();


            this.inAgentGroup.HeaderText = "属于该组";
            this.inAgentGroup.Name = "inAgentGroup";
            this.inAgentGroup.DataPropertyName = "inAgentGroup";
            this.inAgentGroup.ReadOnly = false;
            this.inAgentGroup.Width = 80;


            // key
            agentLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.agentLevel.HeaderText = "坐席等级";
            this.agentLevel.Name = "agentLevel";
            this.agentLevel.DataPropertyName = "agentLevel";
            this.agentLevel.ReadOnly = false;
            this.agentLevel.Width = 100;






            Columns = new System.Windows.Forms.DataGridViewColumn[]{
           agentId,
           AgentName,
          inAgentGroup,
           agentLevel
            };




        }
        public string groupId;

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
        public void bindDatagridView()
        {
            db = new Tools.DataBase();
            SqlConnection sqlcon = db.getConnection();

            SqlDataAdapter sqldap = new SqlDataAdapter(" select TS_AGENT.TS_AGENT_id as agentId,TS_AGENT.TS_AGENT_NAME as agentName, case  when TS_AGENT.TS_AGENT_id  in (select TS_AGENT.TS_AGENT_ID from TS_AGENT_IN_GROUP left join TS_AGENT on TS_AGENT_IN_GROUP.TS_AGENT_ID=TS_AGENT.TS_AGENT_ID  where TS_AGENT_IN_GROUP.TS_GROUP_ID=@groupId ) then 'true' else 'false' end as inAgentGroup, isNull(groupInfo.TS_AGENT_LEVEL,-1) as agentLevel"
                + " from  TS_AGENT left join ( select TS_AGENT_IN_GROUP.TS_GROUP_ID,TS_AGENT.TS_AGENT_ID,TS_AGENT_LEVEL,TS_AGENT_IN_GROUP.TS_SERVERFLAG from TS_AGENT_IN_GROUP right join TS_AGENT on TS_AGENT_IN_GROUP.TS_AGENT_ID=TS_AGENT.TS_AGENT_ID  where TS_AGENT_IN_GROUP.TS_GROUP_ID=@groupId ) as groupInfo on groupInfo.TS_AGENT_ID=TS_AGENT.TS_AGENT_id ", sqlcon);

            /**
             *
            
              select TS_AGENT.TS_AGENT_id as agentId,TS_AGENT.TS_AGENT_NAME as agentName, case  when TS_AGENT.TS_AGENT_id  in (select TS_AGENT.TS_AGENT_ID from TS_AGENT_IN_GROUP left join TS_AGENT on TS_AGENT_IN_GROUP.TS_AGENT_ID=TS_AGENT.TS_AGENT_ID  where TS_AGENT_IN_GROUP.TS_GROUP_ID=@groupId ) then 'true' else 'false' end as inAgentGroup, isNull(groupInfo.TS_AGENT_LEVEL,-1) as level ,isnull(groupInfo.TS_SERVERFLAG,-1) as serverFlag"
                + " from  TS_AGENT left join ( select TS_AGENT_IN_GROUP.TS_GROUP_ID,TS_AGENT.TS_AGENT_ID,TS_AGENT_LEVEL,TS_AGENT_IN_GROUP.TS_SERVERFLAG from TS_AGENT_IN_GROUP right join TS_AGENT on TS_AGENT_IN_GROUP.TS_AGENT_ID=TS_AGENT.TS_AGENT_ID  where TS_AGENT_IN_GROUP.TS_GROUP_ID=@groupId ) as groupInfo on groupInfo.TS_AGENT_ID=TS_AGENT.TS_AGENT_id  
            
            
             */

            sqldap.SelectCommand.Parameters.Add(new SqlParameter("groupId", groupId.Trim()));
            DataSet ds = new DataSet();
            
            sqldap.Fill(ds);
            
            this.DriveDataGridView.DataSource = ds.Tables[0];

        }
        public void showDataGridView(System.Windows.Forms.DataGridView DataGridView)
        {
            this.DriveDataGridView = DataGridView;
            clearDriveDataGridView();
            initColumns();
            DataGridView.Columns.AddRange(Columns);
            DataGridView.CellValueChanged += driveDriveDataGridView_CellValueChanged;
          //  DriveDataGridView.CellClick += DriveDataGridView_CellClick;
            bindDatagridView();
        }



        private void driveDriveDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string agentId = "", agentLevel = "" ; bool inAgentGroup;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DriveDataGridView.Rows[e.RowIndex];
                agentId = row.Cells["agentId"].Value.ToString().Trim();
                string xx = row.Cells["inAgentGroup"].Value.ToString();
                inAgentGroup =row.Cells["inAgentGroup"].Value.ToString().Contains("rue")? true :false;
                agentLevel = row.Cells["agentLevel"].Value.ToString().Trim();
               
                if (this.DriveDataGridView.Columns[e.ColumnIndex].HeaderText == this.inAgentGroup.HeaderText) {
                   
                    if (inAgentGroup)
                    {
                        grant(groupId, agentId,"add");

                    }
                    else {
                        grant(groupId, agentId,"del");

                    }
                    return;
                }
                grant(groupId, agentId, agentLevel);
            }
               //  bindDatagridView();
        }


        public void grant(string groupId, string agentId,string handel)
        {
            SqlCommand sqlcmd = new SqlCommand();
            Tools.DataBase db1 = new Tools.DataBase();
            SqlConnection sqlcon = db1.getConnection();
            sqlcon.Open();
            sqlcmd.Connection = sqlcon;
            sqlcmd.Parameters.Add(new SqlParameter("groupId", groupId.Trim()));
            sqlcmd.Parameters.Add(new SqlParameter("agentId", agentId.Trim()));
            if (handel=="del")
            sqlcmd.CommandText = "delete from [TS_AGENT_IN_GROUP] where [TS_GROUP_ID]=@groupId and TS_AGENT_ID=@agentId";
            else if (handel == "add")
                sqlcmd.CommandText = "insert into TS_AGENT_IN_GROUP (TS_GROUP_ID,TS_AGENT_ID) values(@groupId,@agentId)";
            else
            {
                sqlcmd.Parameters.Add(new SqlParameter("agentLevel", handel.Trim()));
                sqlcmd.CommandText = "update TS_AGENT_IN_GROUP set TS_AGENT_LEVEL=@agentLevel   where [TS_GROUP_ID]=@groupId and TS_AGENT_ID=@agentId";
            }
            int x = sqlcmd.ExecuteNonQuery();
            sqlcmd.Dispose();
            sqlcon.Dispose(); 
       
       }







        /*
        
     
         * 
         
      
  select a.TS_AGENT_ID as agentId,CASe when aig.TS_AGENT_LEVEL is null then 'false' else  'true' end as inAgentGroup,  isnull(aig.TS_AGENT_LEVEL,-1) as agentLevel,isnull(a.TS_SERVERFLAG,'未知') as serverFlag from TS_AGENT a left join TS_AGENT_IN_GROUP aig on a.TS_AGENT_ID=aig.TS_AGENT_ID and aig.TS_GROUP_ID='1000'

       
   select TS_AGENT.TS_AGENT_id as agentId, case  when TS_AGENT.TS_AGENT_id  in (select TS_AGENT.TS_AGENT_ID from TS_AGENT_IN_GROUP left join TS_AGENT on TS_AGENT_IN_GROUP.TS_AGENT_ID=TS_AGENT.TS_AGENT_ID  where TS_AGENT_IN_GROUP.TS_GROUP_ID='1001' ) then 'true' else 'false' end as inAgentGroup, isNull(groupInfo.TS_AGENT_LEVEL,-1) as level ,isnull(groupInfo.TS_SERVERFLAG,-1) as serverFlag
 from  TS_AGENT left join ( select TS_AGENT_IN_GROUP.TS_GROUP_ID,TS_AGENT.TS_AGENT_ID,TS_AGENT_LEVEL,TS_AGENT_IN_GROUP.TS_SERVERFLAG from TS_AGENT_IN_GROUP right join TS_AGENT on TS_AGENT_IN_GROUP.TS_AGENT_ID=TS_AGENT.TS_AGENT_ID  where TS_AGENT_IN_GROUP.TS_GROUP_ID='1001' ) as groupInfo on groupInfo.TS_AGENT_ID=TS_AGENT.TS_AGENT_id 
  
  
        
        
        
        
        
        
        
        
        
        
         */
    }
}
