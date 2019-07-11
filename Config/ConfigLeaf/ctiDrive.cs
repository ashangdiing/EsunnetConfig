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
    public class ctiDrive
    {
        public Tools.DataBase db;
        public DataGridView ctiDriveDataGridView;
        public DataGridViewTextBoxColumn key;
        public DataGridViewTextBoxColumn description;
        public DataGridViewTextBoxColumn value;
        public DataGridViewColumn[]  Columns;
        public void initColumns()
        {    
            
          // key
           key = new System.Windows.Forms.DataGridViewTextBoxColumn();

           this.key.HeaderText = "属性";
           this.key.Name = "key";
           this.key.DataPropertyName = "key";
           this.key.ReadOnly = true;
           this.key.Width =200;
           //description
           description=  new System.Windows.Forms.DataGridViewTextBoxColumn();

           this.description.HeaderText = "描述";
           this.description.Name = "description";
           this.description.DataPropertyName = "description";
           this.description.ReadOnly = false;
           this.description.Width = 400;


           // value
           value = new System.Windows.Forms.DataGridViewTextBoxColumn();
           this.value.HeaderText = "值";
           this.value.Name = "value";
           this.value.DataPropertyName = "value";
           this.value.ReadOnly = false;
           this.value.Width = 200;

           Columns = new System.Windows.Forms.DataGridViewColumn[]{
           key,
           description,
           value};
       }
        public void showDataGridView(DataGridView DataGridView)
       {

           this.ctiDriveDataGridView =DataGridView;
           clearDriveDataGridView();
           initColumns();
           DataGridView.Columns.AddRange(Columns);
            ctiDriveDataGridView.CellValueChanged+=ctiDriveDataGridView_CellValueChanged;
           bindDatagridView();
       }

        private void ctiDriveDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string key = "", value = "", description="";
            if (e.RowIndex >= 0) {
                DataGridViewRow row = ctiDriveDataGridView.Rows[e.RowIndex];
                key = row.Cells["key"].Value.ToString();
                description = row.Cells["description"].Value.ToString();
                value = row.Cells["value"].Value.ToString();
                if (string.IsNullOrEmpty(key)) 
                    return;
                update(key, description,value);
               // bindDatagridView();
            }
              
        }
        public void update(string key, string description, string value) {
          SqlCommand sqlcmd= new SqlCommand();
         Tools.DataBase db1 = new Tools.DataBase();
          SqlConnection sqlcon = db1.getConnection();
          sqlcon.Open();
          sqlcmd.Connection = sqlcon;
          sqlcmd.Parameters.Add(new SqlParameter("key", key.Trim()));
          sqlcmd.Parameters.Add(new SqlParameter("value", value.Trim()));
          sqlcmd.Parameters.Add(new SqlParameter("description", description.Trim()));
          sqlcmd.CommandText = " update TS_MEDIASERVER_SET set TS_VALUE=@value,TS_DESCRIPTION=@description where TS_PARAMETER=@key";
        int x=  sqlcmd.ExecuteNonQuery();
        sqlcmd.Dispose();
        sqlcon.Dispose();
        }
        public void clearDriveDataGridView(){

            ctiDriveDataGridView.Columns.Clear();            
            this.ctiDriveDataGridView.AllowUserToAddRows = false;
            this.ctiDriveDataGridView.AllowUserToDeleteRows = false;
            this.ctiDriveDataGridView.AllowUserToResizeColumns = false;
            this.ctiDriveDataGridView.ReadOnly = false;
            this.ctiDriveDataGridView.AllowUserToResizeRows = false;
            this.ctiDriveDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.ctiDriveDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        }
        public void bindDatagridView() {
            db=new Tools.DataBase();
            SqlConnection sqlcon =db.getConnection();
            SqlDataAdapter sqldap = new SqlDataAdapter("SELECT  [TS_PARAMETER] as 'key',[TS_DESCRIPTION] as 'description',[TS_VALUE] as 'value' FROM [Esunnet].[dbo].[TS_MEDIASERVER_SET]", sqlcon);
            DataSet ds = new DataSet();
            sqldap.Fill(ds);
            this.ctiDriveDataGridView.DataSource = ds.Tables[0];  
        }
    }
}
