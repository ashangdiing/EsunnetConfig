using Config.ConfigLeaf.messageBox;
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
    public class agent
    {
        public DataGridView DataGridView { get; set; }
        public DataGridViewColumn[] Columns;
        public Tools.DataBase db;
        public DataGridViewTextBoxColumn agentId;
        public DataGridViewTextBoxColumn Id;
        public DataGridViewTextBoxColumn AgentName;
        public DataGridViewComboBoxColumn isMonitor;
        public DataGridViewComboBoxColumn serviceType;
        public DataGridViewButtonColumn modifyPassWord;
        public void showDataGridView(System.Windows.Forms.DataGridView DataGridView)
        {
            this.DataGridView = DataGridView;
            clearDriveDataGridView();
            initColumns();
            DataGridView.Columns.AddRange(Columns);
            DataGridView.CellValueChanged += driveDriveDataGridView_CellValueChanged;
            DataGridView.CellClick += DriveDataGridView_CellClick;    
            bindDatagridView();
        }
        private void driveDriveDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string agentId = "", agentName = "", isMonitor = "", serviceType = "",id="";
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DataGridView.Rows[e.RowIndex];
                agentId = row.Cells["agentId"].Value.ToString().Trim();
                agentName = row.Cells["AgentName"].Value.ToString().Trim();
                isMonitor = row.Cells["isMonitor"].Value.ToString().Trim();
                serviceType = row.Cells["serviceType"].Value.ToString().Trim();
                id = row.Cells["id"].Value.ToString();

                if (string.IsNullOrEmpty(agentId.Trim()) && !string.IsNullOrEmpty(id.Trim()))
                {
                     delteDrive(id);
                     DataGridView.Rows.RemoveAt(e.RowIndex);
                }
                else
                {
                    addOrUpdate(id,agentId, agentName, isMonitor, serviceType);
                    if (string.IsNullOrEmpty(id)){
                    row.Cells["id"].Value = row.Cells["agentId"].Value;
                     row.Cells["isMonitor"].Value="否";
                     row.Cells["serviceType"].Value="全部";
                     row.Cells["AgentName"].Value = row.Cells["agentId"].Value;}
                }
            }
       //     bindDatagridView();
        }
        private void delteDrive(string id)
        {
            SqlCommand sqlcmd = new SqlCommand();
            Tools.DataBase db1 = new Tools.DataBase();
            SqlConnection sqlcon = db1.getConnection();
            sqlcon.Open();
            sqlcmd.Connection = sqlcon;
            sqlcmd.Parameters.Add(new SqlParameter("id", id.Trim()));
            sqlcmd.CommandText = "delete from [TS_AGENT] where [TS_AGENT_ID]=@id";
            int x = sqlcmd.ExecuteNonQuery();
            sqlcmd.Dispose();
            sqlcon.Dispose();  
        }
        private void addOrUpdate(string id,string agentId, string agentName, string isMonitor, string serviceType)
        {
           int mi=0, si=0;
            SqlCommand sqlcmd = new SqlCommand();
            Tools.DataBase db1 = new Tools.DataBase();
            SqlConnection sqlcon = db1.getConnection();
            if (isMonitor.Trim() == "是")
                mi = 1;
            else
                mi = 0;
          
            if (serviceType.Trim() == "呼出")
                si = 2;
            else if (serviceType.Trim() == "呼入")
                si= 1;
            else si = 3;
            sqlcon.Open();
            sqlcmd.Connection = sqlcon;
            sqlcmd.Parameters.Add(new SqlParameter("agentName", agentName.Trim()));
            sqlcmd.Parameters.Add(new SqlParameter("isMonitor", mi));
            sqlcmd.Parameters.Add(new SqlParameter("serviceType", si));
            sqlcmd.Parameters.Add(new SqlParameter("agentId", agentId));
            if (string.IsNullOrEmpty(id))
            {


                sqlcmd.CommandText = " insert into TS_AGENT(TS_AGENT_ID,TS_AGENT_NAME,TS_IS_MONITOR,TS_CATEGORY,TS_PASSWORD,TS_AGENT,TS_SERVERFLAG) values(@agentId,@agentName,@isMonitor,@serviceType,@agentId,'','')";
                
            }
            else { 
            //修改
                sqlcmd.Parameters.Add(new SqlParameter("Id", id.Trim()));
                sqlcmd.CommandText = " update TS_AGENT set TS_AGENT_ID=@agentId,TS_AGENT_NAME=@agentName,TS_IS_MONITOR=@isMonitor,TS_CATEGORY=@serviceType where TS_AGENT_ID=@Id";
                
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
        public void initColumns()
        {
            // key
            agentId = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.agentId.HeaderText = "编号";
            this.agentId.Name = "agentId";
            this.agentId.DataPropertyName = "agentId";
            this.agentId.ReadOnly = false;
            this.agentId.Width = 200;

            // key
            AgentName = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.AgentName.HeaderText = "名字";
            this.AgentName.Name = "AgentName";
            this.AgentName.DataPropertyName = "AgentName";
            this.AgentName.ReadOnly = false;
            this.AgentName.Width = 200;


            // key
            isMonitor = new System.Windows.Forms.DataGridViewComboBoxColumn();

            this.isMonitor.HeaderText = "班长坐席";
            this.isMonitor.Name = "isMonitor";
            this.isMonitor.DataPropertyName = "isMonitor";
            this.isMonitor.ReadOnly = false;
            this.isMonitor.Width = 100;
            this.isMonitor.Items.AddRange(new string[] { "是", "否" });
            // key
            serviceType = new System.Windows.Forms.DataGridViewComboBoxColumn();

            this.serviceType.HeaderText = "服务类型";
            this.serviceType.Name = "serviceType";
            this.serviceType.DataPropertyName = "serviceType";
            this.serviceType.ReadOnly = false;
            this.serviceType.Width = 100;
            this.serviceType.Items.AddRange(new string[] { "全部", "呼入", "呼出" });


            modifyPassWord = new System.Windows.Forms.DataGridViewButtonColumn();
            this.modifyPassWord.HeaderText = "操作";
            this.modifyPassWord.Name = "modifyPassWord";
            //this.modifyPassWord.DataPropertyName = "serviceType";
            this.modifyPassWord.ReadOnly = true;
            this.modifyPassWord.Width = 80;
            this.modifyPassWord.Text = "修改密码";
            this.modifyPassWord.UseColumnTextForButtonValue = true;


            // key
            Id = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.Id.HeaderText = "隐藏编号";
            this.Id.Name = "Id";
            this.Id.DataPropertyName = "Id";
            this.Id.Visible = false;
            
           

            Columns = new System.Windows.Forms.DataGridViewColumn[]{
           agentId,
           AgentName,
            isMonitor, 
            serviceType,
            modifyPassWord
            ,Id
            };

        }     
        public void bindDatagridView()
        {
            db = new Tools.DataBase();
            SqlConnection sqlcon = db.getConnection();
            SqlDataAdapter sqldap = new SqlDataAdapter("SELECT [TS_AGENT_ID] as agentId,[TS_AGENT_ID] as Id,[TS_AGENT_NAME] as AgentName,case [TS_IS_MONITOR] when '1' then '是' else  '否' end as isMonitor ,case [TS_CATEGORY] when '1' then '呼入' when '3' then '全部' when '2' then '呼出' end as serviceType FROM [Esunnet].[dbo].[TS_AGENT] order by TS_AGENT_ID ", sqlcon);
            DataSet ds = new DataSet();
            sqldap.Fill(ds);
            this.DataGridView.DataSource = ds.Tables[0];
               
        }
        void DriveDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != this.modifyPassWord.Index)
                return;
            if (e.RowIndex < 0 || e.RowIndex >DataGridView.Rows.Count-2)
                return;

            DataGridViewRow row = DataGridView.Rows[e.RowIndex];
          
            modifyPassWordBox mpw = new modifyPassWordBox();
            mpw.title ="坐席id号："+ row.Cells["agentId"].Value.ToString() + "--坐席名字："+row.Cells["AgentName"].Value.ToString();
            mpw.userName = row.Cells["agentId"].Value.ToString();
            mpw.marking = "agent";
            mpw.init();
            mpw.ShowDialog();
          
          //  MessageBox.Show("暂时未做");

        }

    }
}
