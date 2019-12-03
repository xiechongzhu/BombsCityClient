namespace BombsCityClient
{
    partial class CfgForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tabPageFlowCam = new System.Windows.Forms.TabPage();
            this.listViewFlowCam = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnDelFlowCam = new System.Windows.Forms.Button();
            this.btnAddFlowCam = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageGlobal = new System.Windows.Forms.TabPage();
            this.textBoxParkingUrl = new System.Windows.Forms.TextBox();
            this.textBoxRtPeopleUrl = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxResourceCode = new System.Windows.Forms.TextBox();
            this.textBoxClusterTag = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageParking = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxParingCamAddr = new System.Windows.Forms.TextBox();
            this.textBoxParkingCamPort = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxParkingCamUserName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxParkingCamPassword = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxParkingTotal = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tabPageFlowCam.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageGlobal.SuspendLayout();
            this.tabPageParking.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(956, 729);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(122, 41);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(811, 729);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(122, 41);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // tabPageFlowCam
            // 
            this.tabPageFlowCam.Controls.Add(this.listViewFlowCam);
            this.tabPageFlowCam.Controls.Add(this.btnDelFlowCam);
            this.tabPageFlowCam.Controls.Add(this.btnAddFlowCam);
            this.tabPageFlowCam.Location = new System.Drawing.Point(8, 39);
            this.tabPageFlowCam.Name = "tabPageFlowCam";
            this.tabPageFlowCam.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFlowCam.Size = new System.Drawing.Size(1057, 651);
            this.tabPageFlowCam.TabIndex = 0;
            this.tabPageFlowCam.Text = "人流量摄像头";
            this.tabPageFlowCam.UseVisualStyleBackColor = true;
            // 
            // listViewFlowCam
            // 
            this.listViewFlowCam.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listViewFlowCam.FullRowSelect = true;
            this.listViewFlowCam.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewFlowCam.HideSelection = false;
            this.listViewFlowCam.Location = new System.Drawing.Point(0, 0);
            this.listViewFlowCam.Name = "listViewFlowCam";
            this.listViewFlowCam.Size = new System.Drawing.Size(1047, 569);
            this.listViewFlowCam.TabIndex = 10;
            this.listViewFlowCam.UseCompatibleStateImageBehavior = false;
            this.listViewFlowCam.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "IP地址";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "端口";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "用户名";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "密码";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "描述";
            this.columnHeader5.Width = 200;
            // 
            // btnDelFlowCam
            // 
            this.btnDelFlowCam.Location = new System.Drawing.Point(176, 595);
            this.btnDelFlowCam.Name = "btnDelFlowCam";
            this.btnDelFlowCam.Size = new System.Drawing.Size(122, 41);
            this.btnDelFlowCam.TabIndex = 8;
            this.btnDelFlowCam.Text = "删除";
            this.btnDelFlowCam.UseVisualStyleBackColor = true;
            this.btnDelFlowCam.Click += new System.EventHandler(this.btnDelFlowCam_Click);
            // 
            // btnAddFlowCam
            // 
            this.btnAddFlowCam.Location = new System.Drawing.Point(28, 595);
            this.btnAddFlowCam.Name = "btnAddFlowCam";
            this.btnAddFlowCam.Size = new System.Drawing.Size(122, 41);
            this.btnAddFlowCam.TabIndex = 6;
            this.btnAddFlowCam.Text = "添加";
            this.btnAddFlowCam.UseVisualStyleBackColor = true;
            this.btnAddFlowCam.Click += new System.EventHandler(this.btnAddFlowCam_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageGlobal);
            this.tabControl1.Controls.Add(this.tabPageFlowCam);
            this.tabControl1.Controls.Add(this.tabPageParking);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1073, 698);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageGlobal
            // 
            this.tabPageGlobal.Controls.Add(this.textBoxParkingUrl);
            this.tabPageGlobal.Controls.Add(this.textBoxRtPeopleUrl);
            this.tabPageGlobal.Controls.Add(this.label4);
            this.tabPageGlobal.Controls.Add(this.label3);
            this.tabPageGlobal.Controls.Add(this.textBoxResourceCode);
            this.tabPageGlobal.Controls.Add(this.textBoxClusterTag);
            this.tabPageGlobal.Controls.Add(this.label2);
            this.tabPageGlobal.Controls.Add(this.label1);
            this.tabPageGlobal.Location = new System.Drawing.Point(8, 39);
            this.tabPageGlobal.Name = "tabPageGlobal";
            this.tabPageGlobal.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGlobal.Size = new System.Drawing.Size(1057, 651);
            this.tabPageGlobal.TabIndex = 1;
            this.tabPageGlobal.Text = "景区信息";
            this.tabPageGlobal.UseVisualStyleBackColor = true;
            // 
            // textBoxParkingUrl
            // 
            this.textBoxParkingUrl.Location = new System.Drawing.Point(284, 182);
            this.textBoxParkingUrl.Name = "textBoxParkingUrl";
            this.textBoxParkingUrl.Size = new System.Drawing.Size(692, 35);
            this.textBoxParkingUrl.TabIndex = 7;
            // 
            // textBoxRtPeopleUrl
            // 
            this.textBoxRtPeopleUrl.Location = new System.Drawing.Point(284, 134);
            this.textBoxRtPeopleUrl.Name = "textBoxRtPeopleUrl";
            this.textBoxRtPeopleUrl.Size = new System.Drawing.Size(692, 35);
            this.textBoxRtPeopleUrl.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(238, 24);
            this.label4.TabIndex = 5;
            this.label4.Text = "实时停车位推送接口:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(262, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "实时游览人数推送接口:";
            // 
            // textBoxResourceCode
            // 
            this.textBoxResourceCode.Location = new System.Drawing.Point(140, 75);
            this.textBoxResourceCode.Name = "textBoxResourceCode";
            this.textBoxResourceCode.Size = new System.Drawing.Size(240, 35);
            this.textBoxResourceCode.TabIndex = 3;
            // 
            // textBoxClusterTag
            // 
            this.textBoxClusterTag.Location = new System.Drawing.Point(140, 21);
            this.textBoxClusterTag.Name = "textBoxClusterTag";
            this.textBoxClusterTag.Size = new System.Drawing.Size(240, 35);
            this.textBoxClusterTag.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "资源编码:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "站点:";
            // 
            // tabPageParking
            // 
            this.tabPageParking.Controls.Add(this.textBoxParkingTotal);
            this.tabPageParking.Controls.Add(this.label9);
            this.tabPageParking.Controls.Add(this.textBoxParkingCamPassword);
            this.tabPageParking.Controls.Add(this.label8);
            this.tabPageParking.Controls.Add(this.textBoxParkingCamUserName);
            this.tabPageParking.Controls.Add(this.label7);
            this.tabPageParking.Controls.Add(this.textBoxParkingCamPort);
            this.tabPageParking.Controls.Add(this.label6);
            this.tabPageParking.Controls.Add(this.textBoxParingCamAddr);
            this.tabPageParking.Controls.Add(this.label5);
            this.tabPageParking.Location = new System.Drawing.Point(8, 39);
            this.tabPageParking.Name = "tabPageParking";
            this.tabPageParking.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageParking.Size = new System.Drawing.Size(1057, 651);
            this.tabPageParking.TabIndex = 2;
            this.tabPageParking.Text = "停车位摄像头";
            this.tabPageParking.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(166, 24);
            this.label5.TabIndex = 0;
            this.label5.Text = "摄像头IP地址:";
            // 
            // textBoxParingCamAddr
            // 
            this.textBoxParingCamAddr.Location = new System.Drawing.Point(218, 19);
            this.textBoxParingCamAddr.Name = "textBoxParingCamAddr";
            this.textBoxParingCamAddr.Size = new System.Drawing.Size(279, 35);
            this.textBoxParingCamAddr.TabIndex = 1;
            // 
            // textBoxParkingCamPort
            // 
            this.textBoxParkingCamPort.Location = new System.Drawing.Point(218, 68);
            this.textBoxParkingCamPort.Name = "textBoxParkingCamPort";
            this.textBoxParkingCamPort.Size = new System.Drawing.Size(279, 35);
            this.textBoxParkingCamPort.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(142, 24);
            this.label6.TabIndex = 2;
            this.label6.Text = "摄像头端口:";
            // 
            // textBoxParkingCamUserName
            // 
            this.textBoxParkingCamUserName.Location = new System.Drawing.Point(218, 119);
            this.textBoxParkingCamUserName.Name = "textBoxParkingCamUserName";
            this.textBoxParkingCamUserName.Size = new System.Drawing.Size(279, 35);
            this.textBoxParkingCamUserName.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 24);
            this.label7.TabIndex = 4;
            this.label7.Text = "用户名:";
            // 
            // textBoxParkingCamPassword
            // 
            this.textBoxParkingCamPassword.Location = new System.Drawing.Point(218, 171);
            this.textBoxParkingCamPassword.Name = "textBoxParkingCamPassword";
            this.textBoxParkingCamPassword.Size = new System.Drawing.Size(279, 35);
            this.textBoxParkingCamPassword.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 176);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 24);
            this.label8.TabIndex = 6;
            this.label8.Text = "密码:";
            // 
            // textBoxParkingTotal
            // 
            this.textBoxParkingTotal.Location = new System.Drawing.Point(218, 227);
            this.textBoxParkingTotal.Name = "textBoxParkingTotal";
            this.textBoxParkingTotal.Size = new System.Drawing.Size(279, 35);
            this.textBoxParkingTotal.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 232);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(190, 24);
            this.label9.TabIndex = 8;
            this.label9.Text = "停车场车位总数:";
            // 
            // CfgForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1110, 825);
            this.ControlBox = false;
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CfgForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置";
            this.tabPageFlowCam.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageGlobal.ResumeLayout(false);
            this.tabPageGlobal.PerformLayout();
            this.tabPageParking.ResumeLayout(false);
            this.tabPageParking.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TabPage tabPageFlowCam;
        private System.Windows.Forms.Button btnDelFlowCam;
        private System.Windows.Forms.Button btnAddFlowCam;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ListView listViewFlowCam;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.TabPage tabPageGlobal;
        private System.Windows.Forms.TextBox textBoxResourceCode;
        private System.Windows.Forms.TextBox textBoxClusterTag;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxParkingUrl;
        private System.Windows.Forms.TextBox textBoxRtPeopleUrl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPageParking;
        private System.Windows.Forms.TextBox textBoxParkingTotal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxParkingCamPassword;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxParkingCamUserName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxParkingCamPort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxParingCamAddr;
        private System.Windows.Forms.Label label5;
    }
}