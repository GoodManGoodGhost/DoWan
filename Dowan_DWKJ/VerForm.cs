using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace Dowan
{
    public partial class VerForm : Form
    {
        public static int i = 0;
        public String VerUrl
        {
            set
            {
                Image image = GetImage(value);
                i++;
                llWaitNum.Text = i + "个等待中";
                lock (this)
                {
                    pictureBox1.Image = image;
                    textBox2.Text = "输入验证码";
                    ShowDialog(Common.MainForm);
                }
                i--;
            }
        }

        public Image GetImage(string url)
        {
            return Image.FromStream(WebRequest.Create(Common.HomeUrl + url).GetResponse().GetResponseStream());
        }


        public string VerCode
        {
            get { return textBox2.Text; }
        }
        public VerForm()
        {
            InitializeComponent();
        }
        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBox2.Text == "输入验证码")
                textBox2.Text = null;
            if (textBox2.Text.Length != 4)
                return;
                Hide();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Common.b.LogSwitch = !Common.b.LogSwitch;
            if (button1.Text == "暂停")
                button1.Text = "继续";
            else
                button1.Text = "暂停";
        }
    }
}
