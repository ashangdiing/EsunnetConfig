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
    public class URLConfig
    {
          public DataGridView URLConfigDataGridView { get; set; }
        public DataGridViewColumn[] Columns;
        public Tools.DataBase db;
        public DataGridViewTextBoxColumn URLName;
        public DataGridViewTextBoxColumn URLAdd;
        public DataGridViewTextBoxColumn URLId;
        public void showDataGridView(System.Windows.Forms.DataGridView DataGridView)
        {
            this.URLConfigDataGridView = DataGridView;
            clearDriveDataGridView();
            initColumns();
            DataGridView.Columns.AddRange(Columns);
            URLConfigDataGridView.CellValueChanged += driveDriveDataGridView_CellValueChanged;
            bindDatagridView();
        }

        private void driveDriveDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string name="", add = "", id = "";
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = URLConfigDataGridView.Rows[e.RowIndex];
                name = row.Cells["URLName"].Value.ToString();
                add = row.Cells["URLAdd"].Value.ToString();
                id = row.Cells["id"].Value.ToString();

                if (string.IsNullOrEmpty(name.Trim()) && !string.IsNullOrEmpty(id.Trim()))
                {
                    //删除
                    deleteURLConfig(id);
                }
                else
                {
                    addOrUpdate(name, add, id);
                }
            }
            bindDatagridView();
        }

        private void deleteURLConfig(string id)
        {
            SqlCommand sqlcmd = new SqlCommand();
            Tools.DataBase db1 = new Tools.DataBase();
            SqlConnection sqlcon = db1.getConnection();
            sqlcon.Open();
            sqlcmd.Connection = sqlcon;
            sqlcmd.Parameters.Add(new SqlParameter("id", id.Trim()));
            sqlcmd.CommandText = "delete from [TS_POPUP_URL] where TS_URLNAME=@id";
            int x = sqlcmd.ExecuteNonQuery();
            sqlcmd.Dispose();
            sqlcon.Dispose();

        }
        private void addOrUpdate(string name, string add, string id)
        {
           
            SqlCommand sqlcmd = new SqlCommand();
            Tools.DataBase db1 = new Tools.DataBase();
            SqlConnection sqlcon = db1.getConnection();
          
            sqlcon.Open();
            sqlcmd.Connection = sqlcon;
            sqlcmd.Parameters.Add(new SqlParameter("name", name.Trim())); 
            sqlcmd.Parameters.Add(new SqlParameter("add", add.Trim()));         

            if (string.IsNullOrEmpty(id))
            {
                sqlcmd.CommandText = " insert into TS_POPUP_URL(TS_URLNAME,TS_URLADDRESS) values(@name,@add)";

            }
            else
            {
                //修改
                sqlcmd.Parameters.Add(new SqlParameter("id", id.Trim()));
                sqlcmd.CommandText = " update TS_POPUP_URL set TS_URLNAME=@name,TS_URLADDRESS=@add where TS_URLNAME=@id";

            }
            int x = sqlcmd.ExecuteNonQuery();
            sqlcmd.Dispose();
            sqlcon.Dispose();
        }
        public void initColumns() {
            // key
            URLName = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.URLName.HeaderText = "URL名字";
            this.URLName.Name = "URLName";
            this.URLName.DataPropertyName = "URLName";
            this.URLName.ReadOnly = false;
            this.URLName.Width = 250;

            URLAdd = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.URLAdd.HeaderText = "URL地址";
            this.URLAdd.Name = "URLAdd";
            this.URLAdd.DataPropertyName = "URLAdd";
            this.URLAdd.ReadOnly = false;
            this.URLAdd.Width = 570;

            URLId = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.URLId.HeaderText = "id";
            this.URLId.Name = "id";
            this.URLId.DataPropertyName = "id";
            this.URLId.ReadOnly = false;
            this.URLId.Width = 100;
            this.URLId.Visible = false;

            Columns = new System.Windows.Forms.DataGridViewColumn[]{
           URLName,
           URLAdd,
           URLId           
            };
        
        }
      
        public void clearDriveDataGridView()
        {

            URLConfigDataGridView.Columns.Clear();
            this.URLConfigDataGridView.AllowUserToAddRows = true;
            this.URLConfigDataGridView.AllowUserToDeleteRows = true;
            this.URLConfigDataGridView.AllowUserToResizeColumns = false;
            this.URLConfigDataGridView.ReadOnly = false;
            this.URLConfigDataGridView.AllowUserToResizeRows = false;
            this.URLConfigDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.URLConfigDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        }
        public void bindDatagridView()
        {
            db = new Tools.DataBase();
            SqlConnection sqlcon = db.getConnection();
            SqlDataAdapter sqldap = new SqlDataAdapter("SELECT [TS_URLNAME] as URLName ,[TS_URLADDRESS] as URLAdd ,[TS_URLNAME] as id  FROM [Esunnet].[dbo].[TS_POPUP_URL]", sqlcon);
            DataSet ds = new DataSet();
            sqldap.Fill(ds);
            this.URLConfigDataGridView.DataSource = ds.Tables[0];
        }

    }
}
