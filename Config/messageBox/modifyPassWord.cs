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
    public partial class modifyPassWordBox : Form
    {
        public string marking, userName, Password, confirm,title;
        public modifyPassWordBox()
        {
            InitializeComponent();
       
        }
        public void init(){

            if (marking == "agent")
            {
                this.Text = title;

            }
        
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxPassWord.Text.Trim()!= textBoxConfirm.Text.Trim())
            {
                MessageBox.Show("请确认密码一致");
                return;
            }
            Password = textBoxPassWord.Text.Trim();
            confirm = textBoxConfirm.Text.Trim();
                if (marking == "agent") {
                modifyAgentPassword();
            }
        }

        private void modifyAgentPassword()
        {

           
            SqlCommand sqlcmd = new SqlCommand();
            Tools.DataBase db1 = new Tools.DataBase();
            SqlConnection sqlcon = db1.getConnection();
         
            sqlcon.Open();
            sqlcmd.Connection = sqlcon;

            sqlcmd.Parameters.Add(new SqlParameter("Password", Password.Trim()));
            sqlcmd.Parameters.Add(new SqlParameter("agentId", userName.Trim()));
            sqlcmd.CommandText = " update TS_AGENT set TS_PASSWORD=@Password  where TS_AGENT_ID=@agentId";

            int x = sqlcmd.ExecuteNonQuery();
            
            sqlcmd.Dispose();
            sqlcon.Dispose();
            MessageBox.Show("修改成功");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button cancelButton = sender as Button;
            modifyPassWordBox mpwb = cancelButton.Parent as modifyPassWordBox;
            mpwb.Close();
        }
    }
}
