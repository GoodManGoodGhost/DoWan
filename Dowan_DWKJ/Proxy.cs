using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Dowan
{
    /// <summary>
    /// 代理
    /// </summary>
    class Proxy
    {
        List<ProxyInfo> proxyList;
        int index;      
        /// <summary>
        /// 构造
        /// </summary>
        public Proxy()
        {
            proxyList = new List<ProxyInfo>();
            index = 0;
           
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        public ProxyInfo GetProxyInfo()
        {
            ProxyInfo pi = null;
            while (true)
            {
                Thread.Sleep(200);
                lock (proxyList)
                {
                    if (index >= proxyList.Count)
                    {
                        proxyList.Clear();
                        index = 0;
                        GetServerProxy();                       
                    }
                    if (proxyList.Count > 0)
                    {
                        pi = proxyList[index];
                        index++;
                    }
                }
                //未连接
                if (pi.ReConnectNum == 0)
                    break;               
                //掉线率
                float dxl=pi.DroppedNum / ((float)pi.ConnectedNum);
                //连接数
                int connNum = pi.ConnectedNum;
                //低于30连接
                if (connNum < 2 && dxl<0.5f)
                    break;
            }

            return pi;
        }

        /// <summary>
        /// 评分
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="socroeType"></param>
        public void Score(string ip, ScoreType scoreType)
        {
            foreach (ProxyInfo pi in proxyList)
            {
                if (pi.IP != ip)
                    continue;
                switch (scoreType)
                {
                    case ScoreType.ReConnect:
                        pi.ReConnectNum++;
                        break;
                    case ScoreType.Dropped:
                        pi.DroppedNum++;
                        break;
                    case ScoreType.Connected:
                        pi.ConnectedNum++;
                        break;
                    case ScoreType.Discard:
                        pi.DiscardNum++;
                        break;
                    case ScoreType.Exception:
                        pi.ExceptionNum++;
                        break;
                    default: break;
                }
                string ss = "连接:" + pi.ConnectedNum;
                ss += ",丢弃:" + pi.DiscardNum;
                ss += ",掉线:" + pi.DroppedNum;
                ss += ",重连:" + pi.ReConnectNum;

                //Common.WriteLogFile("代理:"+pi.IP+"\r\n"+ss);

                break;
            }
        }

        /// <summary>
        /// 添加代理
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void AddProxyInfo(string ip,int port)
        {
            //if (!TestProxy(ip, port))
            //    return;
            ProxyInfo pi = new ProxyInfo();
            pi.IP = ip;
            pi.Port = port;
            Common.ProxyNum++;
            proxyList.Add(pi);
        }

        /// <summary>
        /// 获取服务器代理
        /// </summary>
        public bool GetServerProxy()
        {
            try
            {
                int gNum=0;
                if (Common.LoadUserList.Count < 2)
                    gNum = Common.LoadUserList.Count;
                else
                    gNum = Common.LoadUserList.Count / 2; 
                string addr=string.Empty;
                string[] spstr = null;
                if (Common.ProxyService)
                {
                    addr = "http://125.67.235.237:9638/aegour.ashx?&gNum=" + gNum+"&token="+Common.token;
                    spstr = new string[] { "-" };
                }
                else
                {
                    addr = Common.MainForm.tbProxyAddr.Text;
                    string sps = Common.MainForm.tbProxySper.Text;
                    if (sps == "\\r\\n")
                        sps = "\r\n";
                    spstr = new string[] { sps };
                }
                HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(addr, null, null, null);
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string jsonStr = sr.ReadToEnd();
                if (jsonStr.IndexOf("!") != -1)
                {                    
                    return false;
                }
                string[] getProxys = jsonStr.Split(spstr,StringSplitOptions.RemoveEmptyEntries);
                if (getProxys.Length <= 0)
                    return false;
                foreach (string npi in getProxys)
                {
                    try
                    {

                        string[] pp = npi.Split(':');
                        AddProxyInfo(pp[0], int.Parse(pp[1]));
                        Common.ProxyNum++;
                    }
                    catch (Exception e)
                    {
                        Common.WriteLogFile("ParseProxy:" + e.ToString(), null);
                    }
                }
                return true;
            }
            catch (Exception e)
            {              
                return false;
            }

        }

        /// <summary>
        /// 测试代理
        /// </summary>
        bool TestProxy(string ip, int port)
        {
            byte[] data = new byte[1024];
            IPAddress ipd = IPAddress.Parse(ip);
            IPEndPoint ipEnd = new IPEndPoint(ipd, port);
            try
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipEnd);
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 5000);
                StringBuilder buf = new StringBuilder();
                buf.Append("CONNECT ").Append("tcpconn2.tencent.com:443").Append(" HTTP/1.1\r\n");
                buf.Append("Host: tcpconn2.tencent.com:443\r\n");
                buf.Append("Accept: */*\r\n");
                buf.Append("Content-Type: text/html\r\n");
                buf.Append("Proxy-Connection: Keep-Alive\r\n");
                buf.Append("Content-length: 0\r\n");
                buf.Append("\r\n");
                byte[] ms = System.Text.UTF8Encoding.UTF8.GetBytes(buf.ToString());
                socket.Send(ms);
                int recv = 0;
                string textt = string.Empty;
                do
                {
                    recv = socket.Receive(data);
                    textt = (Encoding.Default.GetString(data, 0, recv));

                    if (textt.IndexOf("200 OK") != -1 || //http
                        textt.IndexOf("200 Connection established") != -1)
                    {
                        socket.Close();
                        return true;//连接成功
                    }
                    else if (textt.IndexOf("403 Forbidden") != -1 ||
                              textt.IndexOf("400 Bad") != -1)
                    {
                        break;
                    }

                } while (recv != 0);
                socket.Close();
            }
            catch (Exception e)
            {
            }
            return false;
        }

        /// <summary>
        /// 统计
        /// </summary>
        public void ProxyStatistics()
        {
            foreach (ProxyInfo pi in proxyList)
            {
                string ss = "连接:" + pi.ConnectedNum;
                ss += ",丢弃:" + pi.DiscardNum;
                ss += ",掉线:" + pi.DroppedNum;
                ss += ",重连:" + pi.ReConnectNum;
                ss += ",异常:" + pi.ExceptionNum;
                Common.ProxyStaics(ss + "    地址:" + pi.IP);
            }
        }
                
    }
    enum ScoreType{ 
        /// <summary>
        /// 重连
        /// </summary>
        ReConnect=0,
        /// <summary>
        /// 连接
        /// </summary>
        Connected,
        /// <summary>
        /// 掉线
        /// </summary>
        Dropped,
        /// <summary>
        /// 丢弃
        /// </summary>
        Discard,
        /// <summary>
        /// 异常
        /// </summary>
        Exception
    }
    class ProxyInfo
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string IP = null;
        /// <summary>
        /// 端口
        /// </summary>
        public int Port = 0;
        /// <summary>
        /// 重连次数
        /// </summary>
        public int ReConnectNum = 0;
        /// <summary>
        /// 掉线次数
        /// </summary>
        public int DroppedNum = 0;
        /// <summary>
        /// 连接次数
        /// </summary>
        public int ConnectedNum = 0;
        /// <summary>
        /// 丢弃次数
        /// </summary>
        public int DiscardNum = 0;

        /// <summary>
        /// 异常次数
        /// </summary>
        public int ExceptionNum = 0;

        //public float ConnectAF = ConnectedNum / ReConnectNum;
    }

    

   
}
