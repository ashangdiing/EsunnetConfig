using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Config.ConfigLeaf.messageBox
{
    public partial class LoginBox : Form
    {
        public static string daAddress=".";
        public EsunnetConfig ec;
        public LoginBox()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string userName, pw, dbPW;
            userName = userNameText.Text.Trim();
            pw = passWordText.Text.Trim();
            SqlCommand sqlcmd = new SqlCommand();
            Tools.DataBase db1 = new Tools.DataBase();
            SqlConnection sqlcon = db1.getConnection();
            sqlcon.Open();
            sqlcmd.Connection = sqlcon;
            sqlcmd.Parameters.Add(new SqlParameter("uN", userName.Trim()));
            sqlcmd.CommandText = "SELECT  * FROM [Esunnet].[dbo].[TS_ADMIN] where TS_ADMIN_ID=@uN";
         
            System.Data.SqlClient.SqlDataReader SDR = sqlcmd.ExecuteReader();
            SDR.Read();
            dbPW = SDR["TS_PASSWORD"].ToString(); ;
            sqlcmd.Dispose();
            sqlcon.Dispose();
            if (!dbPW.Equals(pw))
            {
                MessageBox.Show("密码错误", "警告");
                return;
            }
            this.Visible = false;
            ec = new EsunnetConfig();
            ec.Show();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}
