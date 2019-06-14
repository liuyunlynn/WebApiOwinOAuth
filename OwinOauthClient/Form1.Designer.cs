namespace OwinOauthClient
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnValidateUser = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.btnGetUser = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(117, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(177, 21);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(127, 20);
            this.txtUserName.TabIndex = 2;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(177, 48);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(127, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // btnValidateUser
            // 
            this.btnValidateUser.Location = new System.Drawing.Point(177, 87);
            this.btnValidateUser.Name = "btnValidateUser";
            this.btnValidateUser.Size = new System.Drawing.Size(96, 23);
            this.btnValidateUser.TabIndex = 4;
            this.btnValidateUser.Text = "Validate User";
            this.btnValidateUser.UseVisualStyleBackColor = true;
            this.btnValidateUser.Click += new System.EventHandler(this.BtnValidateUser_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(85, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Result";
            // 
            // lblResult
            // 
            this.lblResult.AllowDrop = true;
            this.lblResult.AutoSize = true;
            this.lblResult.ForeColor = System.Drawing.Color.Red;
            this.lblResult.Location = new System.Drawing.Point(134, 208);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(32, 13);
            this.lblResult.TabIndex = 6;
            this.lblResult.Text = "result";
            // 
            // btnGetUser
            // 
            this.btnGetUser.Location = new System.Drawing.Point(177, 149);
            this.btnGetUser.Name = "btnGetUser";
            this.btnGetUser.Size = new System.Drawing.Size(96, 23);
            this.btnGetUser.TabIndex = 7;
            this.btnGetUser.Text = "Get User";
            this.btnGetUser.UseVisualStyleBackColor = true;
            this.btnGetUser.Click += new System.EventHandler(this.BtnGetUser_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 283);
            this.Controls.Add(this.btnGetUser);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnValidateUser);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnValidateUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Button btnGetUser;
    }
}

