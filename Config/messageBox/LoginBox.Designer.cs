namespace Config.ConfigLeaf.messageBox
{
    partial class LoginBox
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
            this.userNameLab = new System.Windows.Forms.Label();
            this.userNameText = new System.Windows.Forms.TextBox();
            this.passWordLab = new System.Windows.Forms.Label();
            this.passWordText = new System.Windows.Forms.TextBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userNameLab
            // 
            this.userNameLab.AutoSize = true;
            this.userNameLab.Location = new System.Drawing.Point(84, 64);
            this.userNameLab.Name = "userNameLab";
            this.userNameLab.Size = new System.Drawing.Size(53, 12);
            this.userNameLab.TabIndex = 0;
            this.userNameLab.Text = "用户名：";
            // 
            // userNameText
            // 
            this.userNameText.Location = new System.Drawing.Point(176, 61);
            this.userNameText.Name = "userNameText";
            this.userNameText.Size = new System.Drawing.Size(211, 21);
            this.userNameText.TabIndex = 1;
            this.userNameText.Text = "888";
            // 
            // passWordLab
            // 
            this.passWordLab.AutoSize = true;
            this.passWordLab.Location = new System.Drawing.Point(96, 106);
            this.passWordLab.Name = "passWordLab";
            this.passWordLab.Size = new System.Drawing.Size(41, 12);
            this.passWordLab.TabIndex = 2;
            this.passWordLab.Text = "密码：";
            this.passWordLab.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // passWordText
            // 
            this.passWordText.Location = new System.Drawing.Point(176, 106);
            this.passWordText.Name = "passWordText";
            this.passWordText.PasswordChar = '*';
            this.passWordText.Size = new System.Drawing.Size(211, 21);
            this.passWordText.TabIndex = 4;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(98, 205);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(75, 23);
            this.buttonLogin.TabIndex = 5;
            this.buttonLogin.Text = "登陆";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(312, 205);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 6;
            this.close.Text = "关闭";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // LoginBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 279);
            this.Controls.Add(this.close);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.passWordText);
            this.Controls.Add(this.passWordLab);
            this.Controls.Add(this.userNameText);
            this.Controls.Add(this.userNameLab);
            this.Name = "LoginBox";
            this.Text = "登陆";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label userNameLab;
        private System.Windows.Forms.TextBox userNameText;
        private System.Windows.Forms.Label passWordLab;
        private System.Windows.Forms.TextBox passWordText;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Button close;
    }
}