namespace ccpsd.notificaciones.client
{
    partial class SettingDlg
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingDlg));
            this.ctlNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnHide = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSaveAndConnect = new System.Windows.Forms.Button();
            this.checkBoxLogActive = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ctlNotifyIcon
            // 
            this.ctlNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("ctlNotifyIcon.Icon")));
            this.ctlNotifyIcon.Text = "ctlNotifyIcon";
            // 
            // btnHide
            // 
            this.btnHide.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnHide.Location = new System.Drawing.Point(297, 123);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(70, 23);
            this.btnHide.TabIndex = 0;
            this.btnHide.Text = "&OK";
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Servidor";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 47);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(232, 20);
            this.textBox1.TabIndex = 3;
            // 
            // btnSaveAndConnect
            // 
            this.btnSaveAndConnect.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSaveAndConnect.Location = new System.Drawing.Point(253, 45);
            this.btnSaveAndConnect.Name = "btnSaveAndConnect";
            this.btnSaveAndConnect.Size = new System.Drawing.Size(114, 23);
            this.btnSaveAndConnect.TabIndex = 4;
            this.btnSaveAndConnect.Text = "Guardar";
            this.btnSaveAndConnect.UseVisualStyleBackColor = true;
            this.btnSaveAndConnect.Click += new System.EventHandler(this.btnSaveAndConnect_Click);
            // 
            // checkBoxLogActive
            // 
            this.checkBoxLogActive.AutoSize = true;
            this.checkBoxLogActive.Checked = true;
            this.checkBoxLogActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLogActive.Location = new System.Drawing.Point(13, 85);
            this.checkBoxLogActive.Name = "checkBoxLogActive";
            this.checkBoxLogActive.Size = new System.Drawing.Size(77, 17);
            this.checkBoxLogActive.TabIndex = 5;
            this.checkBoxLogActive.Text = "Log Activo";
            this.checkBoxLogActive.UseVisualStyleBackColor = true;
            this.checkBoxLogActive.CheckedChanged += new System.EventHandler(this.checkBoxLogActive_CheckedChanged);
            // 
            // SettingDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 158);
            this.ControlBox = false;
            this.Controls.Add(this.checkBoxLogActive);
            this.Controls.Add(this.btnSaveAndConnect);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnHide);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "CCPSD";
            this.Load += new System.EventHandler(this.SettingDlg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon ctlNotifyIcon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSaveAndConnect;
        private System.Windows.Forms.CheckBox checkBoxLogActive;
    }
}