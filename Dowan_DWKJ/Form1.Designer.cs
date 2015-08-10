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
            this.button_Import_Account = new System.Windows.Forms.Button();
            this.button_Flower = new System.Windows.Forms.Button();
            this.user_DataGridView = new System.Windows.Forms.DataGridView();
            this.index_DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.username_DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.logined_DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.channel_DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.majia_DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qianming_DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastMsg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnLogin = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.function_GroupBox = new System.Windows.Forms.GroupBox();
            this.rbYesProxy = new System.Windows.Forms.RadioButton();
            this.rbNoProxy = new System.Windows.Forms.RadioButton();
            this.button_Exit = new System.Windows.Forms.Button();
            this.button_Access_Channel = new System.Windows.Forms.Button();
            this.get_Proxy_IP_Address = new System.Windows.Forms.Label();
            this.tbProxySper = new System.Windows.Forms.TextBox();
            this.tbProxyAddr = new System.Windows.Forms.TextBox();
            this.fen_Ge_Fu = new System.Windows.Forms.Label();
            this.total_Get_Proxy_Number = new System.Windows.Forms.Label();
            this.exit_Channel = new System.Windows.Forms.Button();
            this.change_Channel = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.kai_Shi_Qiang_Mai = new System.Windows.Forms.Button();
            this.qiang_Mai_Shu_Liang = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.qu_Xiao_Qiang_Mai = new System.Windows.Forms.Button();
            this.send_Words = new System.Windows.Forms.Button();
            this.words_To_Say = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.user_DataGridView)).BeginInit();
            this.function_GroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Import_Account
            // 
            this.button_Import_Account.Location = new System.Drawing.Point(12, 16);
            this.button_Import_Account.Margin = new System.Windows.Forms.Padding(2);
            this.button_Import_Account.Name = "button_Import_Account";
            this.button_Import_Account.Size = new System.Drawing.Size(73, 23);
            this.button_Import_Account.TabIndex = 10;
            this.button_Import_Account.Text = "导入帐号";
            this.button_Import_Account.UseVisualStyleBackColor = true;
            this.button_Import_Account.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_Flower
            // 
            this.button_Flower.Location = new System.Drawing.Point(12, 50);
            this.button_Flower.Margin = new System.Windows.Forms.Padding(2);
            this.button_Flower.Name = "button_Flower";
            this.button_Flower.Size = new System.Drawing.Size(73, 23);
            this.button_Flower.TabIndex = 10;
            this.button_Flower.Text = "开启刷花";
            this.button_Flower.UseVisualStyleBackColor = true;
            this.button_Flower.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // user_DataGridView
            // 
            this.user_DataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.user_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.user_DataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.index_DataGridViewTextBoxColumn,
            this.username_DataGridViewTextBoxColumn,
            this.logined_DataGridViewTextBoxColumn,
            this.channel_DataGridViewTextBoxColumn,
            this.majia_DataGridViewTextBoxColumn,
            this.qianming_DataGridViewTextBoxColumn,
            this.lastMsg});
            this.user_DataGridView.Location = new System.Drawing.Point(1, 1);
            this.user_DataGridView.Margin = new System.Windows.Forms.Padding(2);
            this.user_DataGridView.Name = "user_DataGridView";
            this.user_DataGridView.RowHeadersVisible = false;
            this.user_DataGridView.RowTemplate.Height = 19;
            this.user_DataGridView.Size = new System.Drawing.Size(658, 321);
            this.user_DataGridView.TabIndex = 14;
            this.user_DataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.userDGV_CellFormatting);
            // 
            // index_DataGridViewTextBoxColumn
            // 
            this.index_DataGridViewTextBoxColumn.DataPropertyName = "Indexz";
            this.index_DataGridViewTextBoxColumn.HeaderText = "编号";
            this.index_DataGridViewTextBoxColumn.Name = "index_DataGridViewTextBoxColumn";
            this.index_DataGridViewTextBoxColumn.Width = 70;
            // 
            // username_DataGridViewTextBoxColumn
            // 
            this.username_DataGridViewTextBoxColumn.DataPropertyName = "UserName";
            this.username_DataGridViewTextBoxColumn.HeaderText = "帐号";
            this.username_DataGridViewTextBoxColumn.Name = "username_DataGridViewTextBoxColumn";
            this.username_DataGridViewTextBoxColumn.ReadOnly = true;
            this.username_DataGridViewTextBoxColumn.Width = 118;
            // 
            // logined_DataGridViewTextBoxColumn
            // 
            this.logined_DataGridViewTextBoxColumn.DataPropertyName = "IsLogined";
            this.logined_DataGridViewTextBoxColumn.HeaderText = "登录";
            this.logined_DataGridViewTextBoxColumn.Name = "logined_DataGridViewTextBoxColumn";
            this.logined_DataGridViewTextBoxColumn.ReadOnly = true;
            this.logined_DataGridViewTextBoxColumn.Width = 80;
            // 
            // channel_DataGridViewTextBoxColumn
            // 
            this.channel_DataGridViewTextBoxColumn.DataPropertyName = "OldChannel";
            this.channel_DataGridViewTextBoxColumn.HeaderText = "频道";
            this.channel_DataGridViewTextBoxColumn.Name = "channel_DataGridViewTextBoxColumn";
            this.channel_DataGridViewTextBoxColumn.ReadOnly = true;
            this.channel_DataGridViewTextBoxColumn.Width = 80;
            // 
            // majia_DataGridViewTextBoxColumn
            // 
            this.majia_DataGridViewTextBoxColumn.DataPropertyName = "Nick";
            this.majia_DataGridViewTextBoxColumn.HeaderText = "马甲";
            this.majia_DataGridViewTextBoxColumn.Name = "majia_DataGridViewTextBoxColumn";
            // 
            // qianming_DataGridViewTextBoxColumn
            // 
            this.qianming_DataGridViewTextBoxColumn.DataPropertyName = "QianMing";
            this.qianming_DataGridViewTextBoxColumn.HeaderText = "签名";
            this.qianming_DataGridViewTextBoxColumn.Name = "qianming_DataGridViewTextBoxColumn";
            this.qianming_DataGridViewTextBoxColumn.Visible = false;
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
            // function_GroupBox
            // 
            this.function_GroupBox.Controls.Add(this.tbProxySper);
            this.function_GroupBox.Controls.Add(this.total_Get_Proxy_Number);
            this.function_GroupBox.Controls.Add(this.textBox3);
            this.function_GroupBox.Controls.Add(this.fen_Ge_Fu);
            this.function_GroupBox.Controls.Add(this.tbProxyAddr);
            this.function_GroupBox.Controls.Add(this.words_To_Say);
            this.function_GroupBox.Controls.Add(this.send_Words);
            this.function_GroupBox.Controls.Add(this.get_Proxy_IP_Address);
            this.function_GroupBox.Controls.Add(this.qu_Xiao_Qiang_Mai);
            this.function_GroupBox.Controls.Add(this.textBox4);
            this.function_GroupBox.Controls.Add(this.textBox5);
            this.function_GroupBox.Controls.Add(this.qiang_Mai_Shu_Liang);
            this.function_GroupBox.Controls.Add(this.change_Channel);
            this.function_GroupBox.Controls.Add(this.exit_Channel);
            this.function_GroupBox.Controls.Add(this.kai_Shi_Qiang_Mai);
            this.function_GroupBox.Controls.Add(this.rbYesProxy);
            this.function_GroupBox.Controls.Add(this.rbNoProxy);
            this.function_GroupBox.Controls.Add(this.button_Import_Account);
            this.function_GroupBox.Controls.Add(this.button1);
            this.function_GroupBox.Controls.Add(this.btnLogin);
            this.function_GroupBox.Controls.Add(this.button_Exit);
            this.function_GroupBox.Controls.Add(this.button_Access_Channel);
            this.function_GroupBox.Controls.Add(this.button_Flower);
            this.function_GroupBox.Controls.Add(this.label2);
            this.function_GroupBox.Location = new System.Drawing.Point(6, 326);
            this.function_GroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.function_GroupBox.Name = "function_GroupBox";
            this.function_GroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.function_GroupBox.Size = new System.Drawing.Size(653, 139);
            this.function_GroupBox.TabIndex = 25;
            this.function_GroupBox.TabStop = false;
            this.function_GroupBox.Text = "功能区 --- 本软件唯一联系QQ:1501915019";
            // 
            // rbYesProxy PS：此处Yes/No看不懂，故没改名字，保留原始信息。
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
            // rbNoProxy PS：此处Yes/No看不懂，故没改名字，保留原始信息。
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
            // button_Exit
            // 
            this.button_Exit.Location = new System.Drawing.Point(262, 53);
            this.button_Exit.Margin = new System.Windows.Forms.Padding(2);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(72, 23);
            this.button_Exit.TabIndex = 10;
            this.button_Exit.Text = "退出软件";
            this.button_Exit.UseVisualStyleBackColor = true;
            this.button_Exit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // button_Access_Channel
            // 
            this.button_Access_Channel.Location = new System.Drawing.Point(259, 16);
            this.button_Access_Channel.Margin = new System.Windows.Forms.Padding(2);
            this.button_Access_Channel.Name = "button_Access_Channel";
            this.button_Access_Channel.Size = new System.Drawing.Size(75, 23);
            this.button_Access_Channel.TabIndex = 10;
            this.button_Access_Channel.Text = "进入频道";
            this.button_Access_Channel.UseVisualStyleBackColor = true;
            // 
            // get_Proxy_IP_Address
            // 
            this.get_Proxy_IP_Address.AutoSize = true;
            this.get_Proxy_IP_Address.Location = new System.Drawing.Point(4, 114);
            this.get_Proxy_IP_Address.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.get_Proxy_IP_Address.Name = "get_Proxy_IP_Address";
            this.get_Proxy_IP_Address.Size = new System.Drawing.Size(95, 12);
            this.get_Proxy_IP_Address.TabIndex = 22;
            this.get_Proxy_IP_Address.Text = "获取代理IP地址:";
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
            // fen_Ge_Fu
            // 
            this.fen_Ge_Fu.AutoSize = true;
            this.fen_Ge_Fu.Location = new System.Drawing.Point(434, 114);
            this.fen_Ge_Fu.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.fen_Ge_Fu.Name = "fen_Ge_Fu";
            this.fen_Ge_Fu.Size = new System.Drawing.Size(47, 12);
            this.fen_Ge_Fu.TabIndex = 21;
            this.fen_Ge_Fu.Text = "分隔符:";
            // 
            // total_Get_Proxy_Number
            // 
            this.total_Get_Proxy_Number.AutoSize = true;
            this.total_Get_Proxy_Number.Location = new System.Drawing.Point(544, 114);
            this.total_Get_Proxy_Number.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.total_Get_Proxy_Number.Name = "total_Get_Proxy_Number";
            this.total_Get_Proxy_Number.Size = new System.Drawing.Size(95, 12);
            this.total_Get_Proxy_Number.TabIndex = 24;
            this.total_Get_Proxy_Number.Text = "共获取0个代理IP";
            // 
            // exit_Channel
            // 
            this.exit_Channel.Enabled = false;
            this.exit_Channel.Location = new System.Drawing.Point(91, 49);
            this.exit_Channel.Margin = new System.Windows.Forms.Padding(2);
            this.exit_Channel.Name = "exit_Channel";
            this.exit_Channel.Size = new System.Drawing.Size(72, 24);
            this.exit_Channel.TabIndex = 21;
            this.exit_Channel.Text = "退出频道";
            this.exit_Channel.UseVisualStyleBackColor = true;
            // 
            // change_Channel
            // 
            this.change_Channel.Enabled = false;
            this.change_Channel.Location = new System.Drawing.Point(172, 52);
            this.change_Channel.Margin = new System.Windows.Forms.Padding(2);
            this.change_Channel.Name = "change_Channel";
            this.change_Channel.Size = new System.Drawing.Size(72, 24);
            this.change_Channel.TabIndex = 22;
            this.change_Channel.Text = "转换频道";
            this.change_Channel.UseVisualStyleBackColor = true;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(251, 82);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(83, 21);
            this.textBox5.TabIndex = 34;
            // 
            // kai_Shi_Qiang_Mai
            // 
            this.kai_Shi_Qiang_Mai.Location = new System.Drawing.Point(376, 16);
            this.kai_Shi_Qiang_Mai.Name = "kai_Shi_Qiang_Mai";
            this.kai_Shi_Qiang_Mai.Size = new System.Drawing.Size(79, 22);
            this.kai_Shi_Qiang_Mai.TabIndex = 30;
            this.kai_Shi_Qiang_Mai.Text = "开始抢麦";
            this.kai_Shi_Qiang_Mai.UseVisualStyleBackColor = true;
            // 
            // qiang_Mai_Shu_Liang
            // 
            this.qiang_Mai_Shu_Liang.AutoSize = true;
            this.qiang_Mai_Shu_Liang.Location = new System.Drawing.Point(349, 58);
            this.qiang_Mai_Shu_Liang.Name = "qiang_Mai_Shu_Liang";
            this.qiang_Mai_Shu_Liang.Size = new System.Drawing.Size(65, 12);
            this.qiang_Mai_Shu_Liang.TabIndex = 32;
            this.qiang_Mai_Shu_Liang.Text = "抢麦数量：";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(406, 50);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(59, 21);
            this.textBox4.TabIndex = 33;
            // 
            // qu_Xiao_Qiang_Mai
            // 
            this.qu_Xiao_Qiang_Mai.Location = new System.Drawing.Point(475, 16);
            this.qu_Xiao_Qiang_Mai.Name = "qu_Xiao_Qiang_Mai";
            this.qu_Xiao_Qiang_Mai.Size = new System.Drawing.Size(79, 22);
            this.qu_Xiao_Qiang_Mai.TabIndex = 31;
            this.qu_Xiao_Qiang_Mai.Text = "取消抢麦";
            this.qu_Xiao_Qiang_Mai.UseVisualStyleBackColor = true;
            // 
            // send_Words
            // 
            this.send_Words.Location = new System.Drawing.Point(569, 16);
            this.send_Words.Name = "send_Words";
            this.send_Words.Size = new System.Drawing.Size(79, 22);
            this.send_Words.TabIndex = 28;
            this.send_Words.Text = " 点击发言";
            this.send_Words.UseVisualStyleBackColor = true;
            // 
            // words_To_Say
            // 
            this.words_To_Say.AutoSize = true;
            this.words_To_Say.Location = new System.Drawing.Point(349, 87);
            this.words_To_Say.Name = "words_To_Say";
            this.words_To_Say.Size = new System.Drawing.Size(65, 12);
            this.words_To_Say.TabIndex = 27;
            this.words_To_Say.Text = "发言内容：";
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
            this.Controls.Add(this.function_GroupBox);
            this.Controls.Add(this.user_DataGridView);
            this.Controls.Add(this.button2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.user_DataGridView)).EndInit();
            this.function_GroupBox.ResumeLayout(false);
            this.function_GroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Import_Account;
        public System.Windows.Forms.DataGridView user_DataGridView;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Button button_Flower;
        private System.Windows.Forms.DataGridViewTextBoxColumn index;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn index_DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn username_DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn logined_DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn channel_DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn majia_DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qianming_DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastMsg;
        private System.Windows.Forms.GroupBox function_GroupBox;
        public System.Windows.Forms.Button button_Exit;
        public System.Windows.Forms.Button button_Access_Channel;
        private System.Windows.Forms.Label get_Proxy_IP_Address;
        //private System.Windows.Forms.Label llProxyNum;
        private System.Windows.Forms.RadioButton rbYesProxy;
        private System.Windows.Forms.RadioButton rbNoProxy;
        private System.Windows.Forms.Label total_Get_Proxy_Number;
        private System.Windows.Forms.Label fen_Ge_Fu;
        public System.Windows.Forms.TextBox tbProxyAddr;
        public System.Windows.Forms.TextBox tbProxySper;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label words_To_Say;
        private System.Windows.Forms.Button send_Words;
        private System.Windows.Forms.Button qu_Xiao_Qiang_Mai;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label qiang_Mai_Shu_Liang;
        private System.Windows.Forms.Button change_Channel;
        private System.Windows.Forms.Button exit_Channel;
        private System.Windows.Forms.Button kai_Shi_Qiang_Mai;
    }
}

