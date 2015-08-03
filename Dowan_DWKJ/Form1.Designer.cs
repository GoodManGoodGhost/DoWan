namespace Dowan
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnImport = new System.Windows.Forms.Button();
            this.btnFlower = new System.Windows.Forms.Button();
            this.userDGV = new System.Windows.Forms.DataGridView();
            this.indexz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.logined = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.channel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.majia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qianming = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastMsg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnLogin = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbYesProxy = new System.Windows.Forms.RadioButton();
            this.rbNoProxy = new System.Windows.Forms.RadioButton();
            this.btnExit = new System.Windows.Forms.Button();
            this.tbInfo = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbProxySper = new System.Windows.Forms.TextBox();
            this.tbProxyAddr = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.llGetProxyNum = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.userDGV)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(12, 16);
            this.btnImport.Margin = new System.Windows.Forms.Padding(2);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(73, 23);
            this.btnImport.TabIndex = 10;
            this.btnImport.Text = "导入帐号";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnFlower
            // 
            this.btnFlower.Location = new System.Drawing.Point(12, 50);
            this.btnFlower.Margin = new System.Windows.Forms.Padding(2);
            this.btnFlower.Name = "btnFlower";
            this.btnFlower.Size = new System.Drawing.Size(73, 23);
            this.btnFlower.TabIndex = 10;
            this.btnFlower.Text = "开启刷花";
            this.btnFlower.UseVisualStyleBackColor = true;
            this.btnFlower.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // userDGV
            // 
            this.userDGV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.userDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.userDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.indexz,
            this.username,
            this.logined,
            this.channel,
            this.majia,
            this.Qianming,
            this.lastMsg});
            this.userDGV.Location = new System.Drawing.Point(1, 1);
            this.userDGV.Margin = new System.Windows.Forms.Padding(2);
            this.userDGV.Name = "userDGV";
            this.userDGV.RowHeadersVisible = false;
            this.userDGV.RowTemplate.Height = 19;
            this.userDGV.Size = new System.Drawing.Size(658, 321);
            this.userDGV.TabIndex = 14;
            this.userDGV.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.userDGV_CellFormatting);
            // 
            // indexz
            // 
            this.indexz.DataPropertyName = "Indexz";
            this.indexz.HeaderText = "编号";
            this.indexz.Name = "indexz";
            this.indexz.Width = 70;
            // 
            // username
            // 
            this.username.DataPropertyName = "UserName";
            this.username.HeaderText = "帐号";
            this.username.Name = "username";
            this.username.ReadOnly = true;
            this.username.Width = 118;
            // 
            // logined
            // 
            this.logined.DataPropertyName = "IsLogined";
            this.logined.HeaderText = "登录";
            this.logined.Name = "logined";
            this.logined.ReadOnly = true;
            this.logined.Width = 80;
            // 
            // channel
            // 
            this.channel.DataPropertyName = "OldChannel";
            this.channel.HeaderText = "频道";
            this.channel.Name = "channel";
            this.channel.ReadOnly = true;
            this.channel.Width = 80;
            // 
            // majia
            // 
            this.majia.DataPropertyName = "Nick";
            this.majia.HeaderText = "马甲";
            this.majia.Name = "majia";
            // 
            // Qianming
            // 
            this.Qianming.DataPropertyName = "QianMing";
            this.Qianming.HeaderText = "签名";
            this.Qianming.Name = "Qianming";
            this.Qianming.Visible = false;
            // 
            // lastMsg
            // 
            this.lastMsg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.lastMsg.DataPropertyName = "LastMsg";
            this.lastMsg.HeaderText = "消息";
            this.lastMsg.Name = "lastMsg";
            this.lastMsg.ReadOnly = true;
            // 
            // btnLogin
            // 
            this.btnLogin.Enabled = false;
            this.btnLogin.Location = new System.Drawing.Point(172, 16);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(72, 24);
            this.btnLogin.TabIndex = 10;
            this.btnLogin.Text = "登录号码";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.button5_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(382, 122);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(68, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "代理统计";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(91, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "导入马甲";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(194, 89);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "频道ID";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbProxySper);
            this.groupBox4.Controls.Add(this.llGetProxyNum);
            this.groupBox4.Controls.Add(this.textBox3);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.tbProxyAddr);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.button6);
            this.groupBox4.Controls.Add(this.textBox4);
            this.groupBox4.Controls.Add(this.textBox5);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.button7);
            this.groupBox4.Controls.Add(this.button4);
            this.groupBox4.Controls.Add(this.button5);
            this.groupBox4.Controls.Add(this.rbYesProxy);
            this.groupBox4.Controls.Add(this.rbNoProxy);
            this.groupBox4.Controls.Add(this.btnImport);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.btnLogin);
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Controls.Add(this.tbInfo);
            this.groupBox4.Controls.Add(this.btnFlower);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Location = new System.Drawing.Point(6, 326);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(653, 139);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "功能区 --- 本软件唯一联系QQ:1501915019";
            // 
            // rbYesProxy
            // 
            this.rbYesProxy.AutoSize = true;
            this.rbYesProxy.Location = new System.Drawing.Point(106, 87);
            this.rbYesProxy.Margin = new System.Windows.Forms.Padding(2);
            this.rbYesProxy.Name = "rbYesProxy";
            this.rbYesProxy.Size = new System.Drawing.Size(59, 16);
            this.rbYesProxy.TabIndex = 20;
            this.rbYesProxy.Text = "代理IP";
            this.rbYesProxy.UseVisualStyleBackColor = true;
            this.rbYesProxy.CheckedChanged += new System.EventHandler(this.rbNoProxy_CheckedChanged);
            // 
            // rbNoProxy
            // 
            this.rbNoProxy.AutoSize = true;
            this.rbNoProxy.Checked = true;
            this.rbNoProxy.Location = new System.Drawing.Point(12, 87);
            this.rbNoProxy.Margin = new System.Windows.Forms.Padding(2);
            this.rbNoProxy.Name = "rbNoProxy";
            this.rbNoProxy.Size = new System.Drawing.Size(59, 16);
            this.rbNoProxy.TabIndex = 20;
            this.rbNoProxy.TabStop = true;
            this.rbNoProxy.Text = "本机IP";
            this.rbNoProxy.UseVisualStyleBackColor = true;
            this.rbNoProxy.CheckedChanged += new System.EventHandler(this.rbNoProxy_CheckedChanged);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(262, 53);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(72, 23);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "退出软件";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // tbInfo
            // 
            this.tbInfo.Location = new System.Drawing.Point(259, 16);
            this.tbInfo.Margin = new System.Windows.Forms.Padding(2);
            this.tbInfo.Name = "tbInfo";
            this.tbInfo.Size = new System.Drawing.Size(75, 23);
            this.tbInfo.TabIndex = 10;
            this.tbInfo.Text = "进入频道";
            this.tbInfo.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 114);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 12);
            this.label4.TabIndex = 22;
            this.label4.Text = "获取代理IP地址:";
            // 
            // tbProxySper
            // 
            this.tbProxySper.Enabled = false;
            this.tbProxySper.Location = new System.Drawing.Point(475, 108);
            this.tbProxySper.Margin = new System.Windows.Forms.Padding(2);
            this.tbProxySper.Name = "tbProxySper";
            this.tbProxySper.Size = new System.Drawing.Size(65, 21);
            this.tbProxySper.TabIndex = 22;
            this.tbProxySper.Text = "\\r\\n";
            // 
            // tbProxyAddr
            // 
            this.tbProxyAddr.Enabled = false;
            this.tbProxyAddr.Location = new System.Drawing.Point(103, 111);
            this.tbProxyAddr.Margin = new System.Windows.Forms.Padding(2);
            this.tbProxyAddr.Name = "tbProxyAddr";
            this.tbProxyAddr.Size = new System.Drawing.Size(311, 21);
            this.tbProxyAddr.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(434, 114);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 21;
            this.label8.Text = "分隔符:";
            // 
            // llGetProxyNum
            // 
            this.llGetProxyNum.AutoSize = true;
            this.llGetProxyNum.Location = new System.Drawing.Point(544, 114);
            this.llGetProxyNum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.llGetProxyNum.Name = "llGetProxyNum";
            this.llGetProxyNum.Size = new System.Drawing.Size(95, 12);
            this.llGetProxyNum.TabIndex = 24;
            this.llGetProxyNum.Text = "共获取0个代理IP";
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(91, 49);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(72, 24);
            this.button4.TabIndex = 21;
            this.button4.Text = "退出频道";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Enabled = false;
            this.button7.Location = new System.Drawing.Point(172, 52);
            this.button7.Margin = new System.Windows.Forms.Padding(2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(72, 24);
            this.button7.TabIndex = 22;
            this.button7.Text = "转换频道";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(251, 82);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(83, 21);
            this.textBox5.TabIndex = 34;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(376, 16);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(79, 22);
            this.button5.TabIndex = 30;
            this.button5.Text = "开始抢麦";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(349, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 32;
            this.label9.Text = "抢麦数量：";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(406, 50);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(59, 21);
            this.textBox4.TabIndex = 33;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(475, 16);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(79, 22);
            this.button6.TabIndex = 31;
            this.button6.Text = "取消抢麦";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(569, 16);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(79, 22);
            this.button3.TabIndex = 28;
            this.button3.Text = " 点击发言";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(349, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 27;
            this.label7.Text = "发言内容：";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(406, 82);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(239, 21);
            this.textBox3.TabIndex = 26;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 469);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.userDGV);
            this.Controls.Add(this.button2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.userDGV)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnImport;
        public System.Windows.Forms.DataGridView userDGV;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Button btnFlower;
        private System.Windows.Forms.DataGridViewTextBoxColumn index;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn indexz;
        private System.Windows.Forms.DataGridViewTextBoxColumn username;
        private System.Windows.Forms.DataGridViewTextBoxColumn logined;
        private System.Windows.Forms.DataGridViewTextBoxColumn channel;
        private System.Windows.Forms.DataGridViewTextBoxColumn majia;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qianming;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastMsg;
        private System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.Button btnExit;
        public System.Windows.Forms.Button tbInfo;
        private System.Windows.Forms.Label label4;
        //private System.Windows.Forms.Label llProxyNum;
        private System.Windows.Forms.RadioButton rbYesProxy;
        private System.Windows.Forms.RadioButton rbNoProxy;
        private System.Windows.Forms.Label llGetProxyNum;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox tbProxyAddr;
        public System.Windows.Forms.TextBox tbProxySper;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}

