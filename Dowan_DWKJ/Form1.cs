using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Dowan
{
    public partial class Form1 : Form
    {
        public Login loginForm;
        Base b;
        public Form1()
        {
            InitializeComponent();
            Text = Common.SystemName;
            userDGV.AutoGenerateColumns = false;
            Control.CheckForIllegalCrossThreadCalls = false;
            Common.MainForm = this;
            b = new Base(this);
            checkBox1.Checked = Common.CodeSwitch;
            textBox1.Enabled = checkBox1.Checked;
            textBox2.Enabled = checkBox1.Checked;
            if (Common.ProxySwitch)
            {
                rbYesProxy.Checked = true;
                rbNoProxy.Checked = false;
            }
            else
            {
                rbYesProxy.Checked = false;
                rbNoProxy.Checked = true;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Application.Exit();
           
                
        }
        //------login
        private void button1_Click(object sender, EventArgs e)
        {
            Common.b = b;
            if (b.AddAccount())
            {
                btnImport.Enabled = false;
                btnLogin.Enabled = true;
                btnImport.Text = "导入帐号";
                btnExit.Enabled = true;
                button1.Enabled = true;
            }
        }
      
        //ReLogin
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (b == null)
                return;
            if (btnFlower.Text == "刷花中")
            {
                btnFlower.Text = "开启刷花";
                Common.IsFlower = false;
            }
            else
            {
                btnFlower.Text = "刷花中";
                Common.IsFlower = true;
            }
            
        }

        private void userDGV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.Value == null)
                return;
            if (e.ColumnIndex == 2)
            {
                if (e.Value.ToString() == "False")
                    e.Value = "";
                else
                    e.Value = "已登录";
            }
            else if (e.ColumnIndex == 3)
            {
                if (e.Value.ToString() == "0")
                    e.Value = "无";
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (b == null)
                return;           
            uint c = 0;
            string cbch=cbChannel.Text;
            if (string.IsNullOrEmpty(cbch) || cbch == "不进频道" || cbch == "退出频道")
            {
                Common.topsid = 0;
            }
            else //有频道号            
            {
                try
                {
                    c = uint.Parse(cbChannel.Text);
                }
                catch
                {
                    MessageBox.Show("频道号错误");
                    return;
                }
                if (c < 0)
                {
                    MessageBox.Show("频道号错误");
                    return;
                }
                Common.topsid = c;
            }


            if (btnLogin.Text != "切换频道")
            {
                b.LoginStart();
                btnLogin.Text = "切换频道";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            llOnlineMsg.Text = "共:"+Common.LoadUserList.Count+"个，登录:"+Common.LoginedNum+"次,在线:"+Common.OnLineNum+"个";
            llGetProxyNum.Text = "共获取"+Common.ProxyNum+"个代理IP";
               // llProxyNum.Text = Common.ProxyNum.ToString();
            userDGV.Refresh();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("您确定退出 "+Common.SystemName+" 吗？", "退出", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                e.Cancel = true;
                return;
            }
           loginForm.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Common.ProxyManager.ProxyStatistics();
            MessageBox.Show("统计完成");
        }
               
      
        
        /// <summary>
        /// mj
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            Common.b = b;
            if (b.AddMaJia())
            {
                button1.Text = "导入马甲";
                for (int i = 0; i < Common.LoadUserList.Count; i++)
                {
                    User u = Common.LoadUserList[i];
                    u.Nick = Common.MajaList[(i % Common.MajaList.Count)];
                }
            }
        }
        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            Common.b = b;
            if (b.AddQianMing())
            {
                btnExit.Text = "导入签名";
                for (int i = 0; i < Common.LoadUserList.Count; i++)
                {
                    User u = Common.LoadUserList[i];
                    u.QianMing = Common.QianMing[(i % Common.QianMing.Count)];
                }
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            uint c = 0;
            try
            {
                c = uint.Parse(cbChannel.Text);
            }
            catch
            {
                MessageBox.Show("频道号错误");
                return;
            }
            if (c < 0)
            {
                MessageBox.Show("频道号错误");
                return;
            }
            Common.topsid = c;
        }

      

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Common.CodeSwitch = checkBox1.Checked;
            textBox1.Enabled = checkBox1.Checked;
            textBox2.Enabled = checkBox1.Checked;
        }

        private void rbNoProxy_CheckedChanged(object sender, EventArgs e)
        {
            if (rbYesProxy.Checked)
            {
                Common.ProxySwitch = true;
                if (Common.ProxyService)
                {
                    tbProxyAddr.Text = "自动获取";
                }
                else
                {
                    tbProxyAddr.Enabled = true;
                    tbProxySper.Enabled = true;
                }
            }
            else
            {
                Common.ProxySwitch = false;
                tbProxyAddr.Enabled = false;
                tbProxySper.Enabled = false;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Common.CodeUser = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Common.CodePass = textBox2.Text;
        }

      
       
    }
}
