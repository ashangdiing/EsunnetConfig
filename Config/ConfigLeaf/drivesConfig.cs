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
   public  class drivesConfig
    {
        public Tools.DataBase db;
        public DataGridView DriveDataGridView;
        public DataGridViewTextBoxColumn Number;
        public DataGridViewTextBoxColumn Channel;
        public DataGridViewTextBoxColumn description;
        public DataGridViewComboBoxColumn UserType;
        public DataGridViewComboBoxColumn DriveType;
        public DataGridViewColumn[] Columns;
        public void initColumns()
        {
           
            // key
            Number = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.Number.HeaderText = "分机号码";
            this.Number.Name = "Number";
            this.Number.DataPropertyName = "Number";
            this.Number.ReadOnly = false;
            this.Number.Width = 150;
           
            Channel = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.Channel.HeaderText = "编号/通道号";
            this.Channel.Name = "Channel";
            this.Channel.DataPropertyName = "Channel";
            this.Channel.ReadOnly = false;
            this.Channel.Width = 100;

            description = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.description.HeaderText = "描述";
            this.description.Name = "description";
            this.description.DataPropertyName = "description";
            this.description.ReadOnly = false;
            this.description.Width = 100;
            this.description.Visible = false;

            UserType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.UserType.HeaderText = "用户类型";
            this.UserType.Name = "UserType";
            this.UserType.DataPropertyName = "userType";
            this.UserType.ReadOnly = false;
            this.UserType.Width = 200;
            this.UserType.Items.AddRange(new string[] { "坐席", "自动语音", "其他" });



            DriveType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.DriveType.HeaderText = "线路类型";
            this.DriveType.Name = "DriveType";
            this.DriveType.DataPropertyName = "deviceType";
            this.DriveType.ReadOnly = false;
            this.DriveType.Width = 200;
            this.DriveType.Items.AddRange(new string[] { "模拟", "数字", "路由" });

            Columns = new System.Windows.Forms.DataGridViewColumn[]{
           Number,
           Channel,
           UserType,
           DriveType,
           description
            };
        }
        public void showDataGridView(System.Windows.Forms.DataGridView DataGridView)
        {

            this.DriveDataGridView = DataGridView;
            clearDriveDataGridView();
            initColumns();
            DataGridView.Columns.AddRange(Columns);
            DriveDataGridView.CellValueChanged += driveDriveDataGridView_CellValueChanged;       
            bindDatagridView();
        }
    
        private void driveDriveDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string number="", channel="", userType="", driveType="", description="";
            if (e.RowIndex >= 0) {
                DataGridViewRow row = DriveDataGridView.Rows[e.RowIndex];
                number = row.Cells["Number"].Value.ToString();
                channel = row.Cells["channel"].Value.ToString();
                userType = row.Cells["userType"].Value.ToString();
                driveType = row.Cells["driveType"].Value.ToString();
                description = row.Cells["description"].Value.ToString();

                if (string.IsNullOrEmpty(number.Trim()) &&!string.IsNullOrEmpty(channel.Trim()))
                {
                    //删除
                    delteDrive(number, channel);
                    DriveDataGridView.Rows.RemoveAt(e.RowIndex);
                }
                else {
                    addOrUpdate(number, userType, driveType, description, channel);
                    if (string.IsNullOrEmpty(channel)) {
                        row.Cells["channel"].Value =max;
                        row.Cells["userType"].Value = "其他";
                        row.Cells["driveType"].Value = "路由";
                        row.Cells["description"].Value = number;
                    }
                }
            }
          //  bindDatagridView();
        }

        private void addOrUpdate(string number, string userType, string driveType, string description,string channel)
        {
            int ut=0, dt=0;
            SqlCommand sqlcmd = new SqlCommand();
            Tools.DataBase db1 = new Tools.DataBase();
            SqlConnection sqlcon = db1.getConnection();
            if (userType.Trim() == "坐席")
                ut = 1;
            else if (userType.Trim() == "自动语音")
                ut = 0;
            else ut = 2;
            if (driveType.Trim() == "模拟")
                dt = 1;
            else if (driveType.Trim() == "数字")
                dt = 0;
            else dt = 2;
            sqlcon.Open();
            sqlcmd.Connection = sqlcon;
            sqlcmd.Parameters.Add(new SqlParameter("number", number.Trim()));
            sqlcmd.Parameters.Add(new SqlParameter("userType", ut));
            sqlcmd.Parameters.Add(new SqlParameter("driveType", dt));
           
            if (string.IsNullOrEmpty(channel))
            {
                //添加
                channel = queryChannelMax().ToString();
                sqlcmd.Parameters.Add(new SqlParameter("channel", channel.Trim()));
                sqlcmd.CommandText = " insert into TS_DEVICE(TS_NUMBER,TS_DEVICE_TYPE,TS_USER_TYPE,TS_DESCRIPTION,TS_CHANNEL) values(@number,@driveType,@userType,@number,@channel)";
                
            }
            else { 
            //修改
                sqlcmd.CommandText = " update TS_DEVICE set TS_USER_TYPE=@userType,TS_DEVICE_TYPE=@driveType where TS_NUMBER=@number";
                
            }
            int x = sqlcmd.ExecuteNonQuery();
            sqlcmd.Dispose();
            sqlcon.Dispose();
        }

        private void delteDrive(string number, string channel)
        {
            SqlCommand sqlcmd = new SqlCommand();
            Tools.DataBase db1 = new Tools.DataBase();
            SqlConnection sqlcon = db1.getConnection();
            sqlcon.Open();
            sqlcmd.Connection = sqlcon;
            sqlcmd.Parameters.Add(new SqlParameter("channel", channel.Trim()));
            sqlcmd.CommandText = "delete from [TS_DEVICE] where TS_CHANNEL=@channel";
            int x = sqlcmd.ExecuteNonQuery();
            sqlcmd.Dispose();
            sqlcon.Dispose();   

        }
        int max = 0;
        public int  queryChannelMax() {
           
            SqlCommand sqlcmd = new SqlCommand();
            Tools.DataBase db1 = new Tools.DataBase();
            SqlConnection sqlcon = db1.getConnection();
            sqlcon.Open();
            sqlcmd.Connection = sqlcon;
            sqlcmd.CommandText = "select max(TS_CHANNEL) as channelMax from TS_DEVICE";
            System.Data.SqlClient.SqlDataReader SDR = sqlcmd.ExecuteReader();
            if (SDR.HasRows)
            {

                SDR.Read();

                max = Convert.ToInt32(SDR["channelMax"]);
            }
            
            sqlcmd.Dispose();
            sqlcon.Dispose();
            max=max + 1;
            return max;
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
        public void bindDatagridView()
        {
            db = new Tools.DataBase();
            SqlConnection sqlcon = db.getConnection();
            SqlDataAdapter sqldap = new SqlDataAdapter("SELECT  [TS_NUMBER] as number,case TS_DEVICE_TYPE  when '1' then '模拟'  when '0' then '数字'  when '2' then '路由'   end   as deviceType,case TS_USER_TYPE when '1' then '坐席' when '0' then '自动语音' when '2' then '其他' end as userType,[TS_CHANNEL] as Channel,TS_DESCRIPTION as description FROM [Esunnet].[dbo].[TS_DEVICE] order by TS_CHANNEL", sqlcon);
            DataSet ds = new DataSet();
            sqldap.Fill(ds);
            this.DriveDataGridView.DataSource = ds.Tables[0];
        }
    }
}
