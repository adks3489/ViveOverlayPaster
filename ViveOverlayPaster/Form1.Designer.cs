namespace ViveOverlayPaster {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnStartVR = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.txtPos = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnStartVR
            // 
            this.btnStartVR.Enabled = false;
            this.btnStartVR.Location = new System.Drawing.Point(313, 13);
            this.btnStartVR.Name = "btnStartVR";
            this.btnStartVR.Size = new System.Drawing.Size(75, 23);
            this.btnStartVR.TabIndex = 7;
            this.btnStartVR.Text = "啟動VR";
            this.btnStartVR.UseVisualStyleBackColor = true;
            this.btnStartVR.Click += new System.EventHandler(this.btnStartVR_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(19, 45);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(369, 129);
            this.txtLog.TabIndex = 6;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(313, 180);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 82);
            this.btnSend.TabIndex = 8;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtMsg
            // 
            this.txtMsg.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtMsg.Location = new System.Drawing.Point(19, 180);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(288, 81);
            this.txtMsg.TabIndex = 0;
            this.txtMsg.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMsg_KeyUp);
            // 
            // txtPos
            // 
            this.txtPos.Location = new System.Drawing.Point(19, 13);
            this.txtPos.Name = "txtPos";
            this.txtPos.Size = new System.Drawing.Size(185, 22);
            this.txtPos.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 275);
            this.Controls.Add(this.txtPos);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnStartVR);
            this.Controls.Add(this.txtLog);
            this.Name = "Form1";
            this.Text = "ViveOverlayPaster";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartVR;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.TextBox txtPos;
    }
}

