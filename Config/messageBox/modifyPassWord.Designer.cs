namespace Config.ConfigLeaf.messageBox
{
    partial class modifyPassWordBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.passWordLab = new System.Windows.Forms.Label();
            this.textBoxPassWord = new System.Windows.Forms.TextBox();
            this.textBoxConfirm = new System.Windows.Forms.TextBox();
            this.labeltextBoxConfirm = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // passWordLab
            // 
            this.passWordLab.AutoSize = true;
            this.passWordLab.Location = new System.Drawing.Point(117, 50);
            this.passWordLab.Name = "passWordLab";
            this.passWordLab.Size = new System.Drawing.Size(41, 12);
            this.passWordLab.TabIndex = 0;
            this.passWordLab.Text = "密码：";
            // 
            // textBoxPassWord
            // 
            this.textBoxPassWord.Location = new System.Drawing.Point(236, 50);
            this.textBoxPassWord.Name = "textBoxPassWord";
            this.textBoxPassWord.PasswordChar = '*';
            this.textBoxPassWord.Size = new System.Drawing.Size(142, 21);
            this.textBoxPassWord.TabIndex = 1;
            // 
            // textBoxConfirm
            // 
            this.textBoxConfirm.Location = new System.Drawing.Point(236, 125);
            this.textBoxConfirm.Name = "textBoxConfirm";
            this.textBoxConfirm.PasswordChar = '*';
            this.textBoxConfirm.Size = new System.Drawing.Size(142, 21);
            this.textBoxConfirm.TabIndex = 3;
            // 
            // labeltextBoxConfirm
            // 
            this.labeltextBoxConfirm.AutoSize = true;
            this.labeltextBoxConfirm.Location = new System.Drawing.Point(117, 125);
            this.labeltextBoxConfirm.Name = "labeltextBoxConfirm";
            this.labeltextBoxConfirm.Size = new System.Drawing.Size(65, 12);
            this.labeltextBoxConfirm.TabIndex = 2;
            this.labeltextBoxConfirm.Text = "密码确认：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(107, 196);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "修改";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(327, 196);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // modifyPassWordBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 273);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxConfirm);
            this.Controls.Add(this.labeltextBoxConfirm);
            this.Controls.Add(this.textBoxPassWord);
            this.Controls.Add(this.passWordLab);
            this.Name = "modifyPassWordBox";
            this.Text = "密码修改";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label passWordLab;
        private System.Windows.Forms.TextBox textBoxPassWord;
        private System.Windows.Forms.TextBox textBoxConfirm;
        private System.Windows.Forms.Label labeltextBoxConfirm;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}