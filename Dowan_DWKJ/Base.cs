using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using FluorineFx;
using FluorineFx.AMF3;
using System.Net;
using System.IO;
using FluorineFx.Json;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Drawing;
using log4net;

namespace Dowan
{
    class Base
    {
        //-1表示未进行
        public uint Channel;
        Form1 pform;
        bool flowering;
        bool logSwitch;//登录开关      
        int loginIndex;
        private System.Threading.Timer sendTimer;
        List<Thread> loginThreadPool;
        /// <summary>
        /// 取消登录
        /// </summary>
        public bool LogSwitch
        {
            set { logSwitch = value; }
            get { return logSwitch; }
        }
       
        public Base(Form1 fi)
        {
            pform = fi;
            Channel = 0;         
            logSwitch = false;
            flowering = false;
           
            loginIndex = 0;
            loginThreadPool = new List<Thread>();
            sendTimer = new System.Threading.Timer(new TimerCallback(TimerEx));
            sendTimer.Change(60000, 60000);
        }

        /// <summary>
        /// 导入账号
        /// </summary>
        public bool AddAccount()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Txt File|*.txt";
            if (ofd.ShowDialog() != DialogResult.OK)
                return false;
           // pform.Pathtext.Text = ofd.FileName;
            StreamReader sr = null;
            bool flag = false;
            try
            {
                sr = new StreamReader(ofd.FileName);//确保汉字能够正常显示//Encoding.GetEncoding("gb2312")              
                string strRead;
                string[] ss = { "----" };
                int uindex = 0;
                while (uindex<Common.LoginNum)
                {
                    uindex++;
                    strRead = sr.ReadLine();//读取一行
                    if (string.IsNullOrEmpty(strRead))//如果strRead读取为空
                        break;
                    string[] arrValue = strRead.Split(ss, StringSplitOptions.RemoveEmptyEntries);
                    User user = new User(arrValue[0], arrValue[1]);
                    user.Indexz = uindex;
                    Common.LoadUserList.Add(user);
                }
                pform.userDGV.DataSource = Common.LoadUserList;
                pform.Text += "---共" + Common.LoadUserList.Count + "个帐号.";
                flag = true;
            }
            catch
            {
                MessageBox.Show("检查路径或文件格式，是否正确！");
                flag = false;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }            
            return flag;
        }

        /// <summary>
        /// 导入马甲
        /// </summary>
        public bool AddMaJia()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Txt File|*.txt";
            if (ofd.ShowDialog() != DialogResult.OK)
                return false;
            // pform.Pathtext.Text = ofd.FileName;
            StreamReader sr = null;
            bool flag = false;
            try
            {
                sr = new StreamReader(ofd.FileName, Encoding.GetEncoding("gb2312"));//确保汉字能够正常显示//Encoding.GetEncoding("gb2312")              
                Common.MajaList.Clear();
                string strRead;
                //string[] ss = { "----" };
                int uindex = 0;
                while (uindex < Common.LoginNum)
                {
                    uindex++;
                    strRead = sr.ReadLine();//读取一行
                    if (string.IsNullOrEmpty(strRead))//如果strRead读取为空
                        break;
                    //string[] arrValue = strRead.Split(ss, StringSplitOptions.RemoveEmptyEntries);
                    //User user = new User(arrValue[0], arrValue[1]);
                    //user.Indexz = uindex;
                    Common.MajaList.Add(strRead);
                }
                //pform.userDGV.DataSource = Common.LoadUserList;
                //pform.Text += "---共" + Common.LoadUserList.Count + "个帐号.";
                flag = true;
            }
            catch
            {
                MessageBox.Show("检查路径或文件格式，是否正确！");
                flag = false;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }

            return flag;
        }

        /// <summary>
        /// 导入签名
        /// </summary>
        public bool AddQianMing()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Txt File|*.txt";
            if (ofd.ShowDialog() != DialogResult.OK)
                return false;
            // pform.Pathtext.Text = ofd.FileName;
            StreamReader sr = null;
            bool flag = false;
            try
            {
                sr = new StreamReader(ofd.FileName, Encoding.GetEncoding("gb2312"));//确保汉字能够正常显示//Encoding.GetEncoding("gb2312")              
                Common.QianMing.Clear();
                string strRead;
                //string[] ss = { "----" };
                int uindex = 0;
                while (uindex < Common.LoginNum)
                {
                    uindex++;
                    strRead = sr.ReadLine();//读取一行
                    if (string.IsNullOrEmpty(strRead))//如果strRead读取为空
                        break;
                    //string[] arrValue = strRead.Split(ss, StringSplitOptions.RemoveEmptyEntries);
                    //User user = new User(arrValue[0], arrValue[1]);
                    //user.Indexz = uindex;
                    Common.QianMing.Add(strRead);
                }
                //pform.userDGV.DataSource = Common.LoadUserList;
                //pform.Text += "---共" + Common.LoadUserList.Count + "个帐号.";
                flag = true;
            }
            catch
            {
                MessageBox.Show("检查路径或文件格式，是否正确！");
                flag = false;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }

            return flag;
        }

        /// <summary>
        /// 登录
        /// </summary>
        public bool LoginStart()
        {
            logSwitch = !logSwitch;
            if (!logSwitch)
                return true;//开始
            foreach (Thread t in loginThreadPool)
            {
                if (t.ThreadState == ThreadState.Running)
                    return false;//登录中
            }

            loginThreadPool.Clear();
            //登录线程
            for (int i = 0; i < Common.LoginThreadNum; i++)
            {
                Thread loginThread = new Thread(Login);
                loginThread.IsBackground = true;
                loginThread.Start();
                loginThreadPool.Add(loginThread);
                
            }
            return true;//开始
        }

        /// <summary>
        /// 登陆
        /// </summary>
        void Login()
        {
            if (Common.LoadUserList.Count <= 0)
                return;
            byte[] guid = Guid.NewGuid().ToByteArray();
            int count = new Random(guid[0] + guid[13] + guid[9]).Next(5000,10000);
            while (true)
            {
                //本机登录
                if (Common.UserList.Count < 100)
                    Thread.Sleep(count);
                else//代理登录
                    Thread.Sleep(50);

                try
                {
                    if (!logSwitch)//取消登录
                        return;
                    User user = GetUser();
                    if (user == null)
                        return;
                    if (user.IsLogined)
                        continue;
                    if (user.Login(Common.ProxySwitch))//登录成功
                    {
                        //设置伺服对象/未锁定
                        if (Common.ServoUser == null)
                        {
                            Common.ServoUser = user;
                            MsgLoopStart();//启动消息循环
                        }
                        lock (Common.UserList)
                        {
                            //登录的加入临时列表
                            Common.UserList.Add(user);
                        }
                    }                    
                }
                catch (Exception e)
                {
                    //if (user.Socket != null)
                    //    Common.WriteLogFile("Lo \r\n Addr:" + user.Socket.RemoteEndPoint.ToString() + ",username:" + user.UserName + "\r\n" + e.ToString());
                    // else
                    //    Common.WriteLogFile("Lo \r\n Addr:null,username:" + user.UserName + "\r\n" + e.ToString());
                }
            }
        }
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <returns></returns>
        User GetUser()
        {
            User u = null;
            while (u == null)
            {
                lock (Common.LoadUserList)
                {
                    if (loginIndex < Common.LoadUserList.Count)
                    {
                        u = Common.LoadUserList[loginIndex];
                        loginIndex++;
                    }
                    else
                        return null;
                }
                if (u!=null && u.IsLogined)
                    u = null;
            }
            return u;
        }
               

        /// <summary>
        /// 开始消息循环
        /// </summary>
        public void MsgLoopStart()
        {
            Thread TNetMsgLoop = new Thread(NetMsgLoop);
            TNetMsgLoop.IsBackground = true;
            TNetMsgLoop.Start();
        }


        /// <summary>
        /// 消息循环
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        public void NetMsgLoop()
        {
            while (true)
            {
                try
                {
                    Common.ServoUser.NetMsgLoop();
                }
                catch (SocketException se)
                {
                    switch (se.ErrorCode)
                    {
                        case 10060://如果是连接超时异常
                            Common.WriteLogFile("NML-se: \r\n Addr:" + Common.ServoUser.Socket.RemoteEndPoint.ToString() + ",username:" + Common.ServoUser.UserName + "\r\n" + se.ToString());
                            break;
                        default:
                            Common.WriteLogFile("NML-se: \r\n Addr:" + Common.ServoUser.Socket.RemoteEndPoint.ToString() + ",username:" + Common.ServoUser.UserName + "\r\n" + se.ToString());
                            break;
                    }
                }
                catch (Exception e)
                {
                    Common.WriteLogFile("NML-e: \r\n Addr:" + Common.ServoUser.Socket.RemoteEndPoint.ToString() + ",username:" + Common.ServoUser.UserName + "\r\n" + e.ToString());
                }
                //当前消息循环异常时，另设置一个
                foreach (User u in Common.UserList)
                {
                    if (u.IsLogined && (u.UserName != Common.ServoUser.UserName))
                    {
                        Common.ServoUser = u;
                        break;
                    }
                }
                //sleep
                Thread.Sleep(100);
            }
        }


        /// <summary>
        /// 开始切换频道
        /// </summary>
        public void ChangeChannelStart()
        {
            //Thread TChangeChannel = new Thread(ChangeChannel);
            //TChangeChannel.IsBackground = true;
            //TChangeChannel.Start();
        }       

        void TimerEx(Object state)
        {
            if (!Common.AccountCheck)
                return;
            string addr = "http://125.67.235.237:9638/iuioppoo.ashx?&token=" + Common.token+"&name="+Common.UserName;
            HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(addr, null, null, null);
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string jsonStr = sr.ReadToEnd();
            if (jsonStr.IndexOf("0010") == -1)
            {                
                foreach (User ui in Common.UserList)
                    ui.Socket.Close();
                MessageBox.Show("软件检测到异常！");
            }     
        }
    }
}
