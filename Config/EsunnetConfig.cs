using Config.ConfigLeaf;
using Config.ConfigLeaf.agentGroup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Config
{
    public partial class EsunnetConfig : Form
    {
        System.Windows.Forms.TreeNode lastNode; 
        public EsunnetConfig()
        {
            InitializeComponent();

            showMain(initNode); 
        }

        private void treeViewLeft_MouseClick(object sender, MouseEventArgs e)
        {
            this.treeViewLeft.Select();
          System.Windows.Forms.TreeNode  currentNode=this.treeViewLeft.SelectedNode;
          if (lastNode != null)
          {
              if (currentNode.Equals(lastNode)) return;
          }
            //不是点的同一个就清空
          if (lastNode.Name!=currentNode.Name&&currentNode.Nodes.Count>0 ) {
          foreach(  System.Windows.Forms.TreeNode temp in currentNode.Nodes){
              temp.Remove();
          }
          }
            showMain(currentNode);
         //   showSonView(currentNode.Parent.Name);
            lastNode = currentNode;
        }

   
        public void showSonNode(System.Windows.Forms.TreeNode currentNode)
        {
            
            System.Windows.Forms.TreeNode treeNode;
            string sonNodeName;
            SqlCommand sqlcmd = new SqlCommand();
            Tools.DataBase db1 = new Tools.DataBase();
            SqlConnection sqlcon = db1.getConnection();
            sqlcon.Open();
            sqlcmd.Connection = sqlcon;
            string last = "";
            if (currentNode.Name == "agentGroup")
            {
                sqlcmd.CommandText = "select [TS_GROUP_NAME] as nodeId from [TS_AGENT_GROUP] order by TS_GROUP_ID";
                last = "组";
            }

            
            System.Data.SqlClient.SqlDataReader SDR = sqlcmd.ExecuteReader();
            while(SDR.Read()) {
              //  SDR.NextResult();
                sonNodeName = SDR["nodeId"].ToString();
                treeNode = new System.Windows.Forms.TreeNode(sonNodeName);

                treeNode.Name = currentNode.Name + "<" + sonNodeName;
                treeNode.Text = sonNodeName + last;
                if (!currentNode.Nodes.ContainsKey(treeNode.Name))
                    currentNode.Nodes.Add(treeNode);
            }
            sqlcmd.Dispose();
            sqlcon.Dispose();
        }
        public void showMain(System.Windows.Forms.TreeNode currentNode)
        {
            clearCellValueChangedEvent();
            clearCellClickEvent();
            if (currentNode.Name == "ctiDrive")
            {
                ConfigLeaf.ctiDrive cti = new ConfigLeaf.ctiDrive();
                cti.showDataGridView(dataGridView1);
                MainConfigAreaTitle.Text = "CTI数据库配置";
            }
            else if (currentNode.Name == "drivesConfig")
            {
                drivesConfig drivesConfig = new drivesConfig();
                drivesConfig.showDataGridView(dataGridView1);
                MainConfigAreaTitle.Text = "分机,坐席设备";
            }
            else if (currentNode.Name == "URLConfig")
            {
                URLConfig URLConfig = new URLConfig();
                URLConfig.showDataGridView(dataGridView1);
                MainConfigAreaTitle.Text = "URL跳转配置";
            }
            else if (currentNode.Name == "agent" || currentNode.Name == "agentConfig")
            {
                agent agent = new agent();
                agent.showDataGridView(dataGridView1);
                MainConfigAreaTitle.Text = "坐席配置";
            }
            else if (currentNode.Name == "agentGroup")
            {
                showSonNode(currentNode);
                agentGroup ag = new agentGroup();
                ag.esunnetConfig = this;
                ag.showDataGridView(dataGridView1);
                MainConfigAreaTitle.Text = "坐席组配置";
                
            }
          else if (currentNode.Name == "agentStatus")
            {
                agentStatus ageSta=new agentStatus();

                ageSta.showDataGridView(dataGridView1);
                MainConfigAreaTitle.Text = "坐席忙碌原因";               
            }
            else if (currentNode.Parent!=null) {
                if (currentNode.Parent.Name == "agentGroup")
                {
                    string[] fullName = currentNode.Name.Split('<');
                    agentGroupLeaf aGL = new agentGroupLeaf();
                    aGL.groupId = fullName[1];
                    aGL.showDataGridView(dataGridView1);
                    MainConfigAreaTitle.Text = "坐席组" + fullName[1];
                };
              
            };

        }

      //  EventHandlerList myEventHandlerList;
        public void clearCellValueChangedEvent(){
            PropertyInfo propertyInfo =
               (typeof(System.Windows.Forms.DataGridView)).GetProperty("Events", BindingFlags.Instance |BindingFlags.NonPublic);

            EventHandlerList eventHandlerList = (EventHandlerList)propertyInfo.GetValue(dataGridView1, null);
            FieldInfo fieldInfo = (typeof(DataGridView)).GetField("EVENT_" + "DATAGRIDVIEW" + "CELLVALUECHANGED", BindingFlags.Static | BindingFlags.NonPublic);
            Delegate d = eventHandlerList[fieldInfo.GetValue(this.dataGridView1)];

          
            
           // Delegate d = myEventHandlerList[dataGridView1];
            if (d == null)
                return;
            foreach (Delegate dd in d.GetInvocationList())
            {
                dataGridView1.CellValueChanged -= (DataGridViewCellEventHandler)dd;
              
                eventHandlerList.RemoveHandler(dataGridView1, dd);
            }
           
        }
        public void clearCellClickEvent() {
            PropertyInfo propertyInfo =
                (typeof(System.Windows.Forms.DataGridView)).GetProperty("Events", BindingFlags.Instance | BindingFlags.NonPublic);

            EventHandlerList eventHandlerList = (EventHandlerList)propertyInfo.GetValue(dataGridView1, null);
            FieldInfo fieldInfo = (typeof(DataGridView)).GetField("EVENT_" + "DATAGRIDVIEW" + "CELLCLICK", BindingFlags.Static | BindingFlags.NonPublic);
            Delegate d = eventHandlerList[fieldInfo.GetValue(this.dataGridView1)];



            // Delegate d = myEventHandlerList[dataGridView1];
            if (d == null)
                return;
            foreach (Delegate dd in d.GetInvocationList())
            {
                dataGridView1.CellClick -= (DataGridViewCellEventHandler)dd;

                eventHandlerList.RemoveHandler(dataGridView1, dd);
            }
        }

        private void treeViewLeft_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            showMain(e.Node);
        }

        private void EsunnetConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            Application.Exit();
        }
    }
}
