using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace Dowan
{
    public partial class Login : Form
    {
        string config = "config.dll";
        public Login()
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(Common.token))
                Close();
            Common.token = Guid.NewGuid().ToString().Replace("-", "");
            Text = Common.SystemName;

            ReadConfig();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("请输入用户名和密码");
                return;
            }
            try
            {
                string addr = "http://125.67.235.237:9638/weriujjk.ashx?&name=" + textBox1.Text + "&pass=" + textBox2.Text + "&token=" + Common.token;
                HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(addr, null, null, null);
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string jsonStr = sr.ReadToEnd();
                if (jsonStr.IndexOf("0010") == -1)
                {
                    MessageBox.Show("登录失败！");
                    return;
                }
                if (checkBox1.Checked)
                    WriteConfig();
                Common.MainForm = new Form1();
                Common.MainForm.Show();
                Common.MainForm.loginForm = this;
                Common.UserName = textBox1.Text;
                this.Hide();
            }
            catch {
                MessageBox.Show("登录异常，请检查网络！");
            }
        }

        void ReadConfig()
        {
            if (!File.Exists(config))
                File.Create(config);
            else
            {
                StreamReader sr = new StreamReader(config, Encoding.GetEncoding("gb2312"));
                string strRead = sr.ReadLine();//读取一行
                if (!string.IsNullOrEmpty(strRead))//如果strRead读取为空
                {
                    string[] ss = { "----" };
                    string[] arrValue = strRead.Split(ss, StringSplitOptions.RemoveEmptyEntries);
                    if (arrValue.Length == 2)
                    {
                        textBox1.Text = arrValue[0];
                        textBox2.Text = arrValue[1];
                    }
                }
                sr.Close();
                sr.Dispose();
            }
        }

        void WriteConfig()
        {
            if (!File.Exists(config))
                File.Create(config);
            StreamWriter sw = new StreamWriter(config,false);
            sw.Write(textBox1.Text+"----"+textBox2.Text);
            sw.Close();
            sw.Dispose();
        }

        
    }
}
