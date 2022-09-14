namespace DZ_
{
    partial class Form_Client
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
            this.txtClientInfo = new System.Windows.Forms.TextBox();
            this.btnConnectToServer = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtClientInfo
            // 
            this.txtClientInfo.Location = new System.Drawing.Point(12, 12);
            this.txtClientInfo.Multiline = true;
            this.txtClientInfo.Name = "txtClientInfo";
            this.txtClientInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtClientInfo.Size = new System.Drawing.Size(580, 279);
            this.txtClientInfo.TabIndex = 0;
            this.txtClientInfo.TextChanged += new System.EventHandler(this.txtClientInfo_TextChanged);
            // 
            // btnConnectToServer
            // 
            this.btnConnectToServer.Location = new System.Drawing.Point(12, 313);
            this.btnConnectToServer.Name = "btnConnectToServer";
            this.btnConnectToServer.Size = new System.Drawing.Size(132, 29);
            this.btnConnectToServer.TabIndex = 1;
            this.btnConnectToServer.Text = "Connect to server";
            this.btnConnectToServer.UseVisualStyleBackColor = true;
            this.btnConnectToServer.Click += new System.EventHandler(this.btnConnectToServer_Click);
            this.btnConnectToServer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnConnectToServer_MouseClick);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(403, 313);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(189, 29);
            this.btnDisconnect.TabIndex = 2;
            this.btnDisconnect.Text = "Disconnect from server";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // Form_Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 365);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnectToServer);
            this.Controls.Add(this.txtClientInfo);
            this.Name = "Form_Client";
            this.Text = "Form_Client";
            this.Load += new System.EventHandler(this.Form_Client_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtClientInfo;
        private Button btnConnectToServer;
        private Button btnDisconnect;
    }
}