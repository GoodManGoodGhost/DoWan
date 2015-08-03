using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FluorineFx.Json;
using System.Net;
using FluorineFx;
using FluorineFx.AMF3;
using System.Security.Cryptography;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;


namespace Dowan
{

    class User
    {
        #region 属性
        private System.Threading.Timer sendTimer;
        int loginEx = 0;//用于跳验证码
        int indexz = 0;
        const int GET_PUBLIC_KEY_REQ = 343;
        const int HEAD_LEN = 4;
        const uint NO_ENOUGH = 0;
        const uint REC_SUCCESS = 1;//接收数据有效
        const int LOGIN_SUCCESS = 0;//登录成功
        uint oldChannel = 0;

        public uint OldChannel
        {
            get { return oldChannel; }
            set { oldChannel = value; }
        }
        string nick;
        public string Nick
        {
            get { return nick; }
            set { nick = value; }
        }
        string oldMajia;
        string qianMing;

        public string QianMing
        {
            get { return qianMing; }
            set { qianMing = value; }
        }
        string oldQianMing;


        uint uid = 0;
        //需要随机产生的byte
        byte[] mKey = { 29, 66, 130, (byte)new Random().Next(1,255), 7, 89, 234, 67, 86, 198, 49, 70, 159, 237, 252, 29 };
        public NetParams netParams;
        string tagUrl = Common.HomeUrl + "//get-data/9999?subSid=125233252&type=main&_=13749304599" + new Random().Next(1, 9);
        const int heartBeatInterval = 20000;        
        string mypass;
        public string UserName
        {
            get { return myname; }
        }
        string myname;
        string loginVerify = null;
        string channelVerifyUrl = null;
        
        //清理缓存的缓存
        byte[] clearRecv = new byte[4096];

        bool changeChannelOk = false;
        public bool IsChanneled
        {
            get
            {
                return changeChannelOk;
            }
        }
        public int Indexz
        {
            get
            {
                return indexz;
            }
            set {
                indexz = value;
            }
        }
        object changeChannelAndMsgLoop = new object();
        bool changeChannelAndMsgLoopFlag = true;

        bool noNeedData = true;//不需要数据

        bool isLogined;
        public bool IsLogined
        {
            get{return isLogined;}
        }

        string lastMsg = null;
        public string LastMsg
        {
            get { return lastMsg; }
        }
        bool isProxy;
        string server;
        int port;
        Socket socket;
        public Socket Socket
        {
            get { return socket; }
        }
        bool ready;
        public bool Ready
        {
            get { return ready; }
        }
        string proxy;
        int proxyPort;

        DateTime flowerDateTime;
        #endregion

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="username"></param>
        /// <param name="passworld"></param>
        public User(string username,string passworld)
        {
            ready = false;
            isLogined=false;
            proxy = null;
            proxyPort = 0;
            isProxy = true;
            netParams = null;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(passworld))
                return;
            myname = username;
            mypass = passworld;
            flowerDateTime = DateTime.Now;
            //nick = Common.Gb2312();
            //if(user.pass=ilin)
        }
      

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public bool Login(bool p_isProxy)
        {
            isProxy = p_isProxy;
            while (true)
            {
                try
                {
                    if (string.IsNullOrEmpty(myname) || string.IsNullOrEmpty(mypass))
                    {
                        lastMsg = "无帐号密码";
                        return false;
                    }                    
                    //建立连接
                    if (isProxy)//启用代理
                    {
                        while (true)
                        {
                            //获取/更换代理
                            ProxyInfo pi = Common.ProxyManager.GetProxyInfo();
                            proxy = pi.IP;
                            proxyPort = pi.Port;
                            //获取参数
                            if (!GetParams())
                                continue;
                            if (ProxyConnect())
                                break;
                        }
                    }
                    else//本机
                    {
                        //获取参数
                        while (true)
                        {
                            if (GetParams())
                                break;
                        }
                        LocalConnect();
                    }
                    for (int i = 0; i < Common.loginNum; i++)
                    {
                        if (i > 0)
                        {
                            if (isProxy)
                                ProxyConnect();
                            else
                                LocalConnect();
                        }

                        //是否需要验证码
                        if (!string.IsNullOrEmpty(netParams.verifyUrl) && i>0)
                        {
                            if (isProxy)
                            {
                                lastMsg = "已跳过，登录需要验证码";
                                throw new Exception(lastMsg);
                            }
                            //平台打码
                            if (Common.CodeSwitch)
                            {
                                loginVerify = Code.GetCode(Common.verForm.GetImage(netParams.verifyUrl));
                                if (loginVerify.IndexOf("打码平台") > -1)
                                {
                                    lastMsg = loginVerify;
                                    Thread.Sleep(5000);
                                    if (loginVerify.IndexOf("点数不足") > -1 || loginVerify.IndexOf("帐号密码错误") > -1)
                                        return false;                                   
                                    continue;
                                }
                            }//本机打码
                            else
                            {
                                if (loginEx > 2)
                                {
                                    Common.verForm.VerUrl = netParams.verifyUrl;
                                    loginVerify = Common.verForm.VerCode;
                                    netParams.verifyUrl = string.Empty;//清空验证码地址
                                }
                            }
                            loginEx++;
                        }
                        if (inLogin())//登录成功
                        {
                            lastMsg = "登录成功";
                            Common.LoginedNum++;
                            Common.OnLineNum++;
                            if (Common.topsid > 0)
                            {
                                JoinChannel();
                                oldChannel = Common.topsid;
                            }
                            SendReady();
                            UpdateMyInfo();

                            //Receive();

                            //定时
                            sendTimer = new System.Threading.Timer(new TimerCallback(TimerEx));
                            sendTimer.Change(Common.heartBeatInvert, Common.heartBeatInvert);
                            return true;
                        }
                    }
                }
                catch(Exception e)
                {
                    if (Common.LogSwitch)
                        lastMsg = "登录发生异常，重新登录";                    
                }
            }           
        }

        //private void Receive()
        //{
        //    try
        //    {
        //        StateObject state = new StateObject();
        //        state.workSocket = socket;

        //        socket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
        //          new AsyncCallback(ReceiveCallback), state);
        //    }
        //    catch (Exception e)
        //    {
        //        //Console.WriteLine(e.ToString());
        //    }
        //}

        //private static void ReceiveCallback(IAsyncResult ar)
        //{
        //    try
        //    {
        //        StateObject state = (StateObject)ar.AsyncState;
        //        Socket client = state.workSocket;
        //        int bytesRead = client.EndReceive(ar);

        //        if (bytesRead > 0)
        //        {
        //            //state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

        //            //client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
        //            //    new AsyncCallback(ReceiveCallback), state);
        //        }
        //        else
        //        {
        //            //if (state.sb.Length > 1)
        //           // {
        //           //     response = state.sb.ToString();
        //           // }
        //            // Signal that all bytes have been received.
        //            //receiveDone.Set();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        //Console.WriteLine(e.ToString());
        //    }
        //}


        /// <summary>
        /// 定时
        /// </summary>
        /// <param name="state"></param>
        void TimerEx(Object state)
        {
            try
            {
                //心跳
                if (!HearBeat())
                {
                    sendTimer.Dispose();
                    sendTimer = null;
                    ReLogin();
                }
                //joinChannel
                if (Common.topsid!=oldChannel)
                {
                    JoinChannel();
                    oldChannel = Common.topsid;
                }
                //修改信息
                UpdateMyInfo();
                //伺服对象
                if (Common.ServoUser.myname == myname)
                {
                    //sendTimer.Dispose();
                    //sendTimer = null;
                    return;
                }
                //刷花
                if (Common.IsFlower && isLogined)
                {
                    TimeSpan ts = DateTime.Now - flowerDateTime;
                    if (ts.TotalMilliseconds >= 300000)
                    {
                        Flower();
                        flowerDateTime = DateTime.Now;
                    }
                }
            }
            catch (Exception e)
            {
                Common.WriteLogFile("Timer:username:" + myname + "\r\n" + e.ToString());
            }
        }
               
        /// <summary>
        /// 重新登录
        /// </summary>
        /// <returns></returns>
        public bool ReLogin()
        {
            if (Common.LogSwitch)
                lastMsg = "重新登录:" + DateTime.Now;
            return Login(isProxy);
        }


        /// <summary>
        /// in登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="passworld"></param>
        /// <returns></returns>
        bool inLogin()
        {
            try
            {                
                //getPublicKey
                byte[] cmdBuff = new byte[1024];
                byte[] buf = PublicKeyReqData();
                socket.Send(buf, buf.Length, SocketFlags.None);

                //exchangeKey
                int N = socket.Receive(cmdBuff);
                MemoryStream ms = new MemoryStream(cmdBuff, 0, N);
                ByteArray m_recvBuff = new ByteArray(ms);
                byte[] exchangeKeyReqData = ExchangeKeyReqData(m_recvBuff);
                if (exchangeKeyReqData == null)
                    return false;
                socket.Send(exchangeKeyReqData, exchangeKeyReqData.Length, SocketFlags.None);
                //登录数据
                N = socket.Receive(cmdBuff);
                ms = new MemoryStream(cmdBuff, 0, N);
                m_recvBuff = new ByteArray(ms);
                //HearBeat();//开始心跳
                byte[] loginData = LoginReqData(m_recvBuff);//暂时未解析exchangeKey返回数据
                socket.Send(loginData, loginData.Length, SocketFlags.None);
                //登录后
                N = socket.Receive(cmdBuff);
                ms = new MemoryStream(cmdBuff, 0, N);
                m_recvBuff = new ByteArray(ms);
                if (!LoginSuccess(m_recvBuff))
                    return false;
                isLogined = true;
                return true;
            }
            catch (SocketException se)
            {
                switch (se.ErrorCode)
                {
                    case 10060:
                        if (Common.LogSwitch)
                            lastMsg = "登录超时，网络太差了吧。";
                        isLogined = false;
                        return false;
                    default: throw se;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        bool GetParams()
        {
            //if (isProxy)
            //{
            //    byte[] data = new byte[1024];
            //    //IPAddress ip = IPAddress.Parse(proxy);
            //    //IPEndPoint ipEnd = new IPEndPoint(ip, proxyPort);
            //    ///重连X次
            //    for (int i = 0; i < 1; i++)
            //    {
            //        try
            //        {
            //            //socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 5000);
            //            //Common.NowMsg = "代理" + proxy + ":" + proxyPort + "【获取参数】连接中,";
            //            //socket.Connect(ipEnd);
            //            StringBuilder buf = new StringBuilder();
            //            buf.Append("GET ").Append("http://yy.com//get-data/9999?subSid=125233252&type=main&_=137493045" + new Random().Next(1, 9) + "9" + new Random().Next(1, 9)).Append(" HTTP/1.1\r\n");
            //            buf.Append("User-Agent: Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)\r\n");
            //            buf.Append("Host: yy.com\r\n");
            //            buf.Append("Proxy-Connection: Keep-Alive\r\n");
            //            buf.Append("\r\n");
            //            byte[] ms = System.Text.UTF8Encoding.UTF8.GetBytes(buf.ToString());
            //            //发送 
            //            socket.Send(ms);
            //            string jsonStr = string.Empty;
            //            int recv = 0;
            //            do
            //            {
            //                recv = socket.Receive(data);
            //                if (recv > 0)
            //                    jsonStr += (Encoding.Default.GetString(data, 0, recv));
            //                //判断参数结尾
            //                if (jsonStr.IndexOf("core.bmp\"}") != -1)
            //                    break;
            //            } while (recv != 0);
            //            socket.Close();
            //            if (jsonStr.IndexOf("200 OK") == -1)//未取到参数
            //            {
            //                Common.ProxyManager.Score(proxy, ScoreType.Discard);
            //                if (Common.LogSwitch)
            //                    lastMsg = "代理服务器未返回有效参数";
            //                continue;
            //            }
            //            jsonStr = jsonStr.Substring(jsonStr.IndexOf("{\""));
            //            StringReader sread = new StringReader(jsonStr);
            //            JsonReader jr = new JsonReader(sread);
            //            JsonSerializer js = new JsonSerializer();
            //            NetParams np = (NetParams)js.Deserialize(jr, typeof(NetParams));
            //            netParams = np;
            //            server = netParams.pps[0].ip;
            //            port = netParams.pps[0].ports[0];
            //            lastMsg = "获取参数完成";
            //            break;
            //        }
            //        catch (SocketException se)
            //        {
            //            if (se.ErrorCode == 10060)
            //                if (Common.LogSwitch)
            //                    lastMsg = "代理服务器无回应";
            //                else if (se.ErrorCode == 10061)
            //                    if (Common.LogSwitch)
            //                        lastMsg = "代理服务器拒绝连接";
            //                    else
            //                        lastMsg = "";
            //        }
            //        catch (Exception e)
            //        {
            //            //异常
            //            Common.ProxyManager.Score(proxy, ScoreType.Exception);
            //            if (Common.LogSwitch)
            //                lastMsg = "参数数据异常。";
            //            return false;
            //        }
            //    }               
            //}
            //else
            {
                try
                {
                    lastMsg = "获取参数...";
                    HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(tagUrl, null, null, null);
                    StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    string jsonStr = sr.ReadToEnd();
                    StringReader sread = new StringReader(jsonStr);
                    JsonReader jr = new JsonReader(sread);
                    JsonSerializer js = new JsonSerializer();
                    NetParams np = (NetParams)js.Deserialize(jr, typeof(NetParams));
                    netParams = np;
                    //设置缺省服务器及端口
                    server = netParams.pps[0].ip;
                    port = netParams.pps[0].ports[0];
                    lastMsg = "获取参数完成";
                }
                catch (Exception e)
                {
                    if (Common.LogSwitch)
                        lastMsg = "获取参数失败，可能是网速太差。";
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 本地连接
        /// </summary>
        void LocalConnect()
        {
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 7200000);
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 7200000);
                IPAddress ip = IPAddress.Parse(server);//本地
                IPEndPoint ipep = new IPEndPoint(ip, port);
                socket.Connect(ipep);


            }
            catch (Exception e)
            {
                if (Common.LogSwitch)
                    lastMsg = "连接登录服务器，可能是网速太差。";
                throw e;
            }
        }


        /// <summary>
        /// 代理连接
        /// </summary>
        bool ProxyConnect()
        {
            lastMsg = "连接代理服务器...";
            byte[] data = new byte[1024];
            IPAddress ip = IPAddress.Parse(proxy);
            IPEndPoint ipEnd = new IPEndPoint(ip, proxyPort);
            ///重连X次
            for (int i = 0; i < 1; i++)
            {
                try
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    //发起连接/重连
                    Common.ProxyManager.Score(proxy, ScoreType.ReConnect);
                    //Common.NowMsg = "代理" + proxy + ":" + proxyPort + "连接中,";
                    socket.Connect(ipEnd);
                    socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 5000);
                    socket.Blocking = true;//Receive前必须
                    StringBuilder buf = new StringBuilder();
                    buf.Append("CONNECT ").Append(server + ":" + port).Append(" HTTP/1.1\r\n");
                    buf.Append("Host: " + server + ":" + port + "\r\n");
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

                        if (textt.IndexOf("200 OK") != -1 || textt.IndexOf("200 Established") != -1 ||//http
                            textt.IndexOf("200 Connection established") != -1)
                        {
                            Common.ProxyManager.Score(proxy, ScoreType.Connected);
                            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 60000);
                            lastMsg = "连接代理服务器成功";
                            return true;//连接成功
                        }
                        else if (textt.IndexOf("403 Forbidden") != -1 || textt.IndexOf("404 Not Found")!=-1 ||
                                  textt.IndexOf("400 Bad") != -1)
                        {                                                      
                            break;
                        }

                    } while (recv != 0);
                }
                catch (Exception e)
                {
                    //异常
                    Common.ProxyManager.Score(proxy, ScoreType.Exception);
                    if (Common.LogSwitch)
                        lastMsg = "代理连接异常";
                    continue;
                }
                //丢弃
                Common.ProxyManager.Score(proxy, ScoreType.Discard);
            }
            return false;
        }


        /// <summary>
        /// PublicKey请求数据包
        /// </summary>
        /// <returns></returns>
        byte[] PublicKeyReqData()
        {
            byte[] buf={00,00,00,37,10,11,01,37,103,101,116,95,112,117,98,108,105,99,95,107,101,121,95,114,101,113,10,01,01,07,117,114,105,04,130,87,01};
            return buf;
        }

        /// <summary>
        /// exchangeKey请求数据包
        /// </summary>
        byte[] ExchangeKeyReqData(ByteArray publicKeyRecData)
        {
            uint _local4 = Parse(publicKeyRecData);
            if(_local4!=REC_SUCCESS)
                 return null;//接收数据异常
            ASObject ao  = (ASObject)publicKeyRecData.ReadObject();
            ASObject aopka = (ASObject)ao["get_public_key_ack"];
            ByteArray barsa_e = (ByteArray)aopka["rsa_e"];
            byte[] rsa_ebuf = new byte[barsa_e.Length];
            barsa_e.ReadBytes(rsa_ebuf, 0, barsa_e.Length);
            //string rsa_e = bytesValueToHexString(rsa_ebuf);

            ByteArray barsa_n = (ByteArray)aopka["rsa_n"];
            byte[] rsa_nbuf = new byte[barsa_n.Length];
            barsa_n.ReadBytes(rsa_nbuf, 0, barsa_n.Length);
            //string rs = System.Text.Encoding.Default.GetString(rsa_nbuf);
            //RSA
            //rsaParams
            RSAParameters rsaParams = new RSAParameters();
            rsaParams.Exponent = rsa_ebuf;//E
            rsaParams.Modulus = rsa_nbuf;//N
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            RSA.ImportParameters(rsaParams);

           //data
            byte[] rsabuf = RSA.Encrypt(mKey, false);
            byte[] data1={00,00,00,119,10,11,01,33,101,120,99,104,97,110,103,101,95,107,101,121,95,114,101,113,10,01,35,101,110,99,114,121,112,116,101,100,95,114,99,52,95,107,101,121,12,129,01};
            byte[] data3={01,07,117,114,105,04,01,01};
            byte[] data=new byte[119];
            data1.CopyTo(data,0);
            rsabuf.CopyTo(data,data1.Length);
            data3.CopyTo(data,data1.Length+rsabuf.Length);
            return data;
        }

         /// <summary>
        /// Login请求数据包(暂时未解析exchangeKey返回数据)
        /// </summary>
        byte[] LoginReqData(ByteArray exchangeKeyRecData)
        {
            uint _local4 = Parse(exchangeKeyRecData);
            if(_local4!=REC_SUCCESS)
                 return null;//接收数据异常
            //暂时未解析exchangeKey返回数据，直接构造登录数据包
            //登录包
            byte[] topsidnum = Common.ASIntToByte(netParams.topSid);//计算topSid
            //00:00:00:e9(数据长度置零最后计算):0a:0b:01:13:6c:6f:67:69:6e:5f:72:65:71:0a:01: //<--login_req
            byte[] login_req = {0,0,0,0,10,11,1,19,108,111,103,105,110,95,114,101,113,10,01};
            //0d:74:6f:70:73:69:64:04:ce:0f: //topsid（9999）
            byte[] topsid;
            if (topsidnum.Length > 1)
                topsid = new byte[]{ 13,116,111,112,115,105,100,4,topsidnum[0],topsidnum[1]};
            else
                topsid = new byte[]{ 13,116,111,112,115,105,100,4,topsidnum[0]};
            //password
            //11:70:61:73:73:77:6f:72:64:0c:51 :: c7:7b:0f:ab:d1:30:69:98:23:d4:ae:d6:7e:06:d6:71:61:85:76:ca:09:6e:e7:89:c8:40:a1:5f:fd:78:4f:3b:63:43:2d:b7:70:c4:ed:ca:
            byte[] passwordtag = { 17,112,97,115,115,119,111,114,100,12,81};
            byte[] passwordval = PassEncrypt(mypass);
            //15:76:65:72:69:66:79:5f:61:6e:73:06:01://verify_ans
            byte[] verify_ans;
            if (!string.IsNullOrEmpty(loginVerify))
            {
                loginVerify=loginVerify.ToLower();
                byte[] vercodbyte = Encoding.UTF8.GetBytes(loginVerify);
                verify_ans=new byte[]{ 21, 118, 101, 114, 105, 102, 121, 95, 97, 110, 115, 6, 9, vercodbyte[0],  vercodbyte[1],  vercodbyte[2],  vercodbyte[3]};
            }else
            {
                verify_ans=new byte[]{ 21,118,101,114,105,102,121,95,97,110,115,6,1};
            }
            //09:6e:61:6d:65:06:13:79:69:6c:69:6e:6a:75:6e:32://name
            byte[] nametag = { 9,110,97,109,101,6,(byte)((myname.Length*2)+1)};//tag后面的一位是名称字符的长度计算,1:3,4:9,8:11(17),9:13(19),20:29(41),21:43
            byte[] nameval = Encoding.UTF8.GetBytes(myname);
            //0d:73:75:62:73:69:64:04:00://subsid
            byte[] subsid = {13,115,117,98,115,105,100,4,0};
            //token
            //0b:74:6f:6b:65:6e:06:61:41:41:41:41:44:49:30:7a:30:58:66:35:65:5f:48:48:58:50:2d:51:75:30:62:59:54:79:4d:74:79:45:46:49:2d:34:63:7a:34:58:4c:61:39:31:50:4a:54:57:79:65:
            byte[] tokentag = { 11,116,111,107,101,110,6,97};
            byte[] tokenval = Encoding.UTF8.GetBytes(netParams.token);
            //pcinfo   
            //0d:70:63:69:6e:66:6f:06//41:38:38:43:30:42:36:32:41:45:35:42:46:38:37:35:44:34:46:44:38:31:31:36:41:46:30:34:37:46:39:46:37:
            byte[] pcinfotag = { 13, 112, 99, 105, 110, 102, 111, 6 };
            byte[] pcinfoval = Encoding.UTF8.GetBytes("A" + Guid.NewGuid().ToString().Replace("-", "").ToUpper());
            //11:69:6e:76:65:6e:74:6f:72:01://inventor
            byte[] inventor = { 17, 105, 110, 118, 101, 110, 116, 111, 114, 1 };
            //uri
            //01:07:75:72:69:04:07:01
            byte[] uri = { 1,7,117,114,105,4,7,1};
            
            
            //计算数据长度
            int ldLen=login_req.Length+topsid.Length+passwordtag.Length+passwordval.Length+verify_ans.Length+nametag.Length+nameval.Length+subsid.Length+tokentag.Length+tokenval.Length+pcinfotag.Length+pcinfoval.Length+inventor.Length+uri.Length;
            login_req[3] = (byte)ldLen;
            //组包
            byte[] loginData = new byte[ldLen];
            int index = 0;
            login_req.CopyTo(loginData, index);
            index += login_req.Length;
            topsid.CopyTo(loginData, index);
            index += topsid.Length;
            passwordtag.CopyTo(loginData, index);
            index += passwordtag.Length;
            passwordval.CopyTo(loginData, index);
            index += passwordval.Length;
            verify_ans.CopyTo(loginData, index);
            index += verify_ans.Length;
            nametag.CopyTo(loginData, index);
            index += nametag.Length;
            nameval.CopyTo(loginData, index);
            index += nameval.Length;
            subsid.CopyTo(loginData, index);
            index += subsid.Length;
            tokentag.CopyTo(loginData, index);
            index += tokentag.Length;
            tokenval.CopyTo(loginData, index);
            index += tokenval.Length;
            pcinfotag.CopyTo(loginData, index);
            index += pcinfotag.Length;
            pcinfoval.CopyTo(loginData, index);
            index += pcinfoval.Length;
            inventor.CopyTo(loginData, index);
            index += inventor.Length;
            uri.CopyTo(loginData, index);
            index += uri.Length;
            return loginData;
        }


        byte[] hbData = { 0, 0, 0, 46, 10, 11, 1, 29, 104, 101, 97, 114, 116, 95, 98, 101, 97, 116, 95, 114, 101, 113, 10, 1, 21, 108, 111, 99, 97, 108, 95, 116, 105, 109, 101, 4, 224, 57, 1, 7, 117, 114, 105, 4, 5, 1 };
        /// <summary>
        /// 心跳
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool HearBeat()
        {
            //未登录
            if (!isLogined)
                return false;
            try
            {
                //清除接受缓冲区数据，如果其他地方需要数据时，需要暂时停止清除.
                if (noNeedData && (myname!=Common.ServoUser.myname) && ReadTotal())
                    socket.Receive(clearRecv);                
                socket.Send(hbData, hbData.Length, SocketFlags.None);
                lastMsg = "心跳完成:"+DateTime.Now;
                return true;
            }
            catch (Exception ex)
            {
                //三个状态复位
                isLogined = false;
                ready = false;
                changeChannelOk = false;
                //掉线
                Common.ProxyManager.Score(proxy, ScoreType.Dropped);
                if (Common.LogSwitch)
                    lastMsg = "掉线:" + DateTime.Now;
                Common.WriteLogFile("inHB:Addr:" + socket.RemoteEndPoint.ToString() + ",username:" + myname + "\r\n" + ex.ToString());
                //关闭socket;
                Common.OnLineNum--;
                socket.Close();
                socket = null;
                return false;
            }
        }
        
        /// <summary>
        /// 登录成功
        /// </summary>
        /// <param name="loginRecData"></param>
        /// <returns></returns>
        bool LoginSuccess(ByteArray loginRecData)
        {
            uint _local4 = Parse(loginRecData);
            if (_local4 != REC_SUCCESS)
                return false;
            ASObject ao = (ASObject)loginRecData.ReadObject();
            ASObject login_ack = (ASObject)ao["login_ack"];
            //result: 
            // 13 需要验证码
            int s = (int)login_ack["result"];
            if (s == 13)//需要验证码
                netParams.verifyUrl = login_ack["verify_url"].ToString(); 
            if (s == LOGIN_SUCCESS)
            {
                uid = uint.Parse(login_ack["uid"].ToString());//uid                
                return true;
            }

            return false;
        }
       
        /// <summary>
        /// 加入频道
        /// </summary>
        void JoinChannel()
        {
            if (Common.topsid < 0)
                return;

            //00:00:00:65:0a:0b:01:07:
            byte[] b1 = { 0,0,0,101,10,11,1,7};
            //uri:
            //75:72:69=04:11:21:
            byte[] b2 = { 117, 114, 105, 4, 17, 33 };
            //join_session_req:
            //6a:6f:69:6e:5f:73:65:73:73:69:6f:6e:5f:72:65:71=0a:01:19:
            byte[] b3 = { 106,111,105,110,95,115,101,115,115,105,111,110,95,114,101,113,10,1,25};
            //kick_confirm:
            //6b:69:63:6b:5f:63:6f:6e:66:69:72:6d=02:13:
            byte[] b4 = { 107, 105, 99, 107, 95, 99, 111, 110, 102, 105, 114, 109, 2, 19 };
            //is_rejoin:
            //69:73:5f:72:65:6a:6f:69:6e=02:0f:
            byte[] b5 = { 105,115,95,114,101,106,111,105,110,2,15};
            //top_sid:
            //74:6f:70:5f:73:69:64:04=83:8d:f9:d0:
            byte[] b6 = {116, 111,112,95,115,105,100,4};
            byte[] b7 = Common.ASIntToByte(Common.topsid);
            //client_isp:
            //21,63:6c:69:65:6e:74:5f:69:73:70=04:00:1b:
            byte[] b8 = {21, 99,108,105,101,110,116,95,105,115,112,4,0,27};
            //subsession_id
            //73:75:62:73:65:73:73:69:6f:6e:5f:69:64=04:00:01:01
            byte[] b9 = { 115, 117, 98, 115, 101, 115, 115, 105, 111, 110, 95, 105, 100, 4, 0, 1, 1 };

            int length = b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length;
            byte[] reqData = new byte[length];
            b1[3] = (byte)length;
            int index = 0;
            b1.CopyTo(reqData, index);
            index += b1.Length;
            b2.CopyTo(reqData, index);
            index += b2.Length;
            b3.CopyTo(reqData, index);
            index += b3.Length;
            b4.CopyTo(reqData, index);
            index += b4.Length;
            b5.CopyTo(reqData, index);
            index += b5.Length;
            b6.CopyTo(reqData, index);
            index += b6.Length;
            b7.CopyTo(reqData, index);
            index += b7.Length;
            b8.CopyTo(reqData, index);
            index += b8.Length;
            b9.CopyTo(reqData, index);
            index += b9.Length;
            socket.Send(reqData, reqData.Length, SocketFlags.None);

            //未判断是否成功            
            changeChannelOk = true;
        }

        /// <summary>
        /// 刷花
        /// </summary>
        public void Flower()
        {            
            if ((!isLogined))//未登录
                return;
            if(Common.micQueue.Count < 1)//无麦序
                return;
            if (!ready)//未就绪
                return;
            if (!changeChannelOk)//未加入频道
                return;
            //uint my = 795759530;
            //uint mic = 113581884;
            uint m = 1;
            uint maxtype = 10;
            uint mintype = 3;
            //pack
            ByteArray pack = new ByteArray();
            pack.WriteUnsignedInt(Common.endianLittleBig(uid));
            pack.WriteUnsignedInt(Common.endianLittleBig(Common.micQueue[0]));
            pack.WriteUnsignedInt(Common.endianLittleBig(m));//刷的数量，且为1
            //data
            ByteArray data = new ByteArray();
            data.WriteUnsignedInt(Common.endianLittleBig(maxtype));
            data.WriteUnsignedInt(Common.endianLittleBig(mintype));
            UInt16 pl = (UInt16)pack.Length;
            short plength = (short)Common.endianLittleBig(pl);
            data.WriteShort(plength);
            byte[] buf = new byte[pack.Length];
            pack.Position = 0;
            pack.ReadBytes(buf, (uint)0, (uint)buf.Length);
            data.WriteBytes(buf, 0, buf.Length);
            byte[] databuf = new byte[data.Length];
            data.Position = 0;
            data.ReadBytes(databuf, (uint)0, (uint)databuf.Length);
            //senddata
            //00:00:00: 42 :0a:0b:01:07:75:72:69:04:82:46:19:61:70:70:5f:64:61:74:61:5f:72:65:71:0a:01:05:69:64:04:f5:1b:09:64:61:74:61:0c:2d: 0a:00:00:00:03:00:00:00:0c:00:aa:53:6e:2f:3c:1f:c5:06:01:00:00:00:  01:01
            //00:00:00: 42 :0a:0b:01:07:75:72:69:04:82:46:19:61:70:70:5f:64:61:74:61:5f:72:65:71:0a:01:05:69:64:04:f5:1b:09:64:61:74:61:0c:2d: 0a:00:00:00:03:00:00:00:0c:00:aa:53:6e:2f:3c:1f:c5:06:01:00:00:00:  01:01
            //00:00:00: 42: 0a:0b:01:07:75:72:69:04:82:46:19:61:70:70:5f:64:61:74:61:5f:72:65:71:0a:01:05:69:64:04:f5:1b:09:64:61:74:61:0c:2d: 0a:00:00:00:03:00:00:00:0c:00:aa:53:6e:2f:39:73:65:0b:01:00:00:00:  01:01
            //
            byte[] front = { 0, 0, 0, 66, 10, 11, 1, 7, 117, 114, 105, 4, 130, 70, 25, 97, 112, 112, 95, 100, 97, 116, 97, 95, 114, 101, 113, 10, 1, 5, 105, 100, 4, 245, 27, 9, 100, 97, 116, 97, 12, 45 };
            byte[] back = { 1, 1 };
            //长度
            int length = front.Length + databuf.Length + back.Length;
            front[3] = (byte)length;
            //组
            int sendBuffIndex = 0;
            byte[] sendBuff = new byte[length];
            front.CopyTo(sendBuff, sendBuffIndex);
            sendBuffIndex += front.Length;
            databuf.CopyTo(sendBuff, sendBuffIndex);
            sendBuffIndex += databuf.Length;
            back.CopyTo(sendBuff, sendBuffIndex);
            socket.Send(sendBuff);
            lastMsg = "完成刷花:"+DateTime.Now;
        }

        /// <summary>
        /// 就绪
        /// </summary>
        void SendReady()
        {
            if (!isLogined)//未登录
                return;
            //是否需要先加入频道？？
            if (ready)//已就绪
                return;
            ready = !ready;
            //00:00:00:36:0a:0b:01:19:61:70:70:5f:64:61:74:61:5f:72:65:71:0a:01:05:69:64:04:f5:1b:09:64:61:74:61:0c:15:0b:00:00:00:e8:03:00:00:00:00:01:07:75:72:69:04:82:46:01
            byte[] sendBuff = {0,0,0,54,10,11,1,25,97,112,112,95,100,97,116,97,95,114,101,113,10,1,5,105,100,4,245,27,9,100,97,116,97,12,21,11,0,0,0,232,3,0,0,0,0,1,7,117,114,105,4,130,70,1};           
            socket.Send(sendBuff);
            lastMsg = "已就绪:"+DateTime.Now;
        }


        /// <summary>
        /// 接受网络数据(同频道一个user调用就可以)
        /// </summary>
        public void NetMsgLoop()
        {
            if (!isLogined)//未登录
                return;
            //非伺对象不可循环
            if (Common.ServoUser==null || Common.ServoUser.myname != myname)
                return;
            byte[] dataBuff = new byte[409600];
            dataBuff.Initialize();
            int indexBuff = 0;
            byte[] recvBuff = new byte[204800];
            recvBuff.Initialize();
            while (true)
            {
                while (!ReadTotal())
                {
                    //掉线
                    if (!isLogined)
                        return;
                    Thread.Sleep(50);
                }
                int n = socket.Receive(recvBuff);
                recvBuff.CopyTo(dataBuff, indexBuff);                
                recvBuff.Initialize();//清空临时buff
                indexBuff += n;
                //数据异常时
                if (indexBuff >= recvBuff.Length)
                {
                    indexBuff = 0;
                    dataBuff.Initialize();
                    continue;
                }
                MemoryStream ms = new MemoryStream(dataBuff, 0, indexBuff);
                ByteArray m_recvBuff = new ByteArray(ms);
                while (true)
                {
                    uint _local4 = Parse(m_recvBuff);
                    if (_local4 == REC_SUCCESS)
                    {
                        ASObject ao = (ASObject)m_recvBuff.ReadObject();
                        prossObject(ao);
                        if (m_recvBuff.Position == m_recvBuff.Length)//数据刚好取完
                        {
                            dataBuff.Initialize();
                            indexBuff = 0;
                            Thread.Sleep(30);
                            break;
                        }
                    }
                    else
                    {
                        long noDataLength = m_recvBuff.Length - m_recvBuff.Position;//剩余数据长度
                        Array.Copy(dataBuff, m_recvBuff.Position, recvBuff, 0, noDataLength);
                        dataBuff.Initialize();
                        recvBuff.CopyTo(dataBuff, 0);
                        indexBuff = (int)noDataLength;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 处理网络对象
        /// </summary>
        /// <param name="ao"></param>
        bool prossObject(ASObject ao)
        {
            int uris = (int)ao["uri"];
            switch (uris)
            {
                case 15:
                    break;
                case 18://join_session_ack
                    ASObject join_session_ack = (ASObject)ao["join_session_ack"];
                    uint topsid_newtopsid = uint.Parse(join_session_ack["top_sid"].ToString());
                    //互斥标志
                    lock (changeChannelAndMsgLoop)
                    {
                        changeChannelAndMsgLoopFlag = false;
                    }
                    //当result==0时进入频道成功，暂时未解析
                    changeChannelOk = true;
                    break;
                case 158://追加
                    ASObject micdrag = (ASObject)ao["session_micqueue_drag"];
                    uint draguid = uint.Parse(micdrag["uid"].ToString());
                    Common.micQueue.Add(draguid);
                    break;
                case 159://离开
                    ASObject micleave = (ASObject)ao["session_micqueue_leave"];
                    object[] leaveuids = (object[])micleave["uids"];
                    foreach (object o in leaveuids)
                    {
                        uint leaveuid = uint.Parse(o.ToString());
                        Common.micQueue.Remove(leaveuid);
                    }
                    break;
                case 160://加入list,最后
                    //
                    ASObject micjoin = (ASObject)ao["session_micqueue_join"];
                    object[] joinuids = (object[])micjoin["uids"];
                    foreach (object o in joinuids)
                    {
                        uint joinuid = uint.Parse(o.ToString());
                        Common.micQueue.Add(joinuid);
                    }
                    break;
                case 161://剔除
                    //
                    ASObject mickick = (ASObject)ao["session_micqueue_kick"];
                    uint kickuid = uint.Parse(mickick["uid"].ToString());
                    Common.micQueue.Remove(kickuid);                    
                    break;
                case 162://清空
                    Common.micQueue.Clear();
                    break;
                case 167://超时
                    ASObject mictimeout = (ASObject)ao["session_micqueue_timeout"];
                    uint timeoutuid = uint.Parse(mictimeout["uid"].ToString());
                    Common.micQueue.Remove(timeoutuid);
                    break;
                case 169://micqueue_notify                   
                    ASObject micq = (ASObject)ao["session_micqueue_list_notify"];
                    ASObject micqi = (ASObject)micq["micqueue_info"];
                    ByteArray micqueueinfo = (ByteArray)micqi["uids"];
                    micqueueinfo.Position = 0;
                    uint i1 = micqueueinfo.ReadUnsignedInt();
                    i1 = Common.endianLittleBig(i1);
                    uint micqueueLength = micqueueinfo.ReadUnsignedInt();
                    micqueueLength = Common.endianLittleBig(micqueueLength);
                    Common.micQueue.Clear();
                    for (int i = 0; i < micqueueLength; i++)//麦序加入
                    {
                        uint i3 = Common.endianLittleBig(micqueueinfo.ReadUnsignedInt());
                        Common.micQueue.Add(i3);
                    }                   
                    break;
                case 356://频道验证码 
                    ASObject channelAo = (ASObject)ao["change_topsid_ack"];
                    channelVerifyUrl = channelAo["verify_url"].ToString();
                    //互斥标志
                    lock (changeChannelAndMsgLoop)
                    {
                        changeChannelAndMsgLoopFlag = false;
                    }
                    
                    break;
                default:
                    break;
            }
            return false;
        }
             
        /// <summary>
        /// 密码加密
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        byte[] PassEncrypt(string pass)
        {
            string key = Common.ObjectToJson(mKey);
            string ss = HttpHelper.GetHtml("http://125.67.235.237:9638/Je8our.ashx?pass=" + pass + "&id=sss&KEY=" + key+"&token="+Common.token);
            byte[] sss = new byte[1];
            return Common.JsonToObject(ss, sss);
        }
        
        /// <summary>
        /// 返回数据有效性验证
        /// </summary>
        /// <param name="_arg1"></param>
        /// <returns></returns>
        uint Parse(FluorineFx.AMF3.ByteArray _arg1)
        {
            uint _local2;
            if (_arg1.Length < HEAD_LEN){
                return NO_ENOUGH;
            };
            _local2 = _arg1.ReadUnsignedInt();
            long avlength = _arg1.Length - _arg1.Position;
            if (avlength >= (_local2 - HEAD_LEN))
            {
                return REC_SUCCESS;
            };
            _arg1.Position = (_arg1.Position - 4);
            return NO_ENOUGH;
        }

        /// <summary>
        /// 查询接受缓冲区的可读数据
        /// </summary>
        /// <returns></returns>
        bool ReadTotal()
        {
            if (!isLogined)
                return false;
            byte[] buffer = BitConverter.GetBytes(0);
            socket.IOControl(IOControlCode.DataToRead, null, buffer);
            uint nn = BitConverter.ToUInt32(buffer, 0);
            if (nn > 0)
                return true;
            return false;
        }
        /// <summary>
        /// 修改信息
        /// </summary>
        void UpdateMyInfo()
        {
            if (oldMajia == nick && oldQianMing==qianMing)
                return;
            //00:00:00:f8:0a:0b:01:25:75:70:64:61:74:65:5f:6d:79:5f:69:6e:66:6f:5f:72:65:71:0a:01:13:6d:79:5f:64:65:74:61:69:6c:0a:01:11:62:69:72:74:68:64:61:79:06:11:31:39:39:30:30:31:30:31:09:61:72:65:61:01:09:63:69:74:79:04:00:0b:69:6e:74:72:6f:06:03:66:11:70:72:6f:76:69:6e:63:65:04:00:19:69:6d:5f:75:73:65:72:5f:69:6e:66:6f:0a:01:13:75:73:65:72:5f:69:6e:66:6f:0a:01:11:6e:69:63:6b:6e:61:6d:65:06:11://67:67:67:67:6f:6f:6f:6f//8=17,9=19,18=37
            string myinfo1 = "00:00:00:f8:0a:0b:01:25:75:70:64:61:74:65:5f:6d:79:5f:69:6e:66:6f:5f:72:65:71:0a:01:13:6d:79:5f:64:65:74:61:69:6c:0a:01:11:62:69:72:74:68:64:61:79:06:11:31:39:39:30:30:31:30:31:09:61:72:65:61:01:09:63:69:74:79:04:00:0b:69:6e:74:72:6f:06:03:66:11:70:72:6f:76:69:6e:63:65:04:00:19:69:6d:5f:75:73:65:72:5f:69:6e:66:6f:0a:01:13:75:73:65:72:5f:69:6e:66:6f:0a:01:11:6e:69:63:6b:6e:61:6d:65:06:11";
            string myinfo3 = "0d:67:65:6e:64:65:72:04:00:07:70:69:64:04:00:15:75:69:6e:66:6f:6a:69:66:65:6e:04:81:76:07:75:69:64:05";
            string myinfo5 = "01:1c:04:00:07:65:78:70:04:00:09:69:6d:69:64:05:41:c6:69:c6:c1:00:00:00:13:73:69:67:6e:61:74:75:72:65:06:3:20:01:0f:76:65:72:73:69:6f:6e:04:00:01:01:07:75:72:69:04:59:01";
                  
            byte[] dataMyinfo1 = Common.NetDataToByte(myinfo1);
            //截取30位
            if (nick.Length > 30)
                nick = nick.Substring(0, 30);
            byte[] dataMyinfo2 = Encoding.UTF8.GetBytes(nick);
            dataMyinfo1[dataMyinfo1.Length - 1] = (byte)(dataMyinfo2.Length * 2 + 1);
            byte[] dataMyinfo3 = Common.NetDataToByte(myinfo3);
            byte[] dataMyinfo4 = Common.ASIntToByte(uid);//41:c7:b7:29:d5:00:00:00:
            byte[] dataMyinfo5 = Common.NetDataToByte(myinfo5);
           
            //长度
            int length = dataMyinfo1.Length + dataMyinfo2.Length + dataMyinfo3.Length+dataMyinfo4.Length+dataMyinfo5.Length;
            byte[] bytelength = Common.HexstrToByte(length.ToString("X"));
            if(bytelength.Length==1)
                dataMyinfo1[3] = bytelength[0];
            else
            {
                dataMyinfo1[2] = bytelength[0];
                dataMyinfo1[3] = bytelength[1];
            }
            //组
            int sendBuffIndex = 0;
            byte[] sendBuff = new byte[length];
            dataMyinfo1.CopyTo(sendBuff, sendBuffIndex);
            sendBuffIndex += dataMyinfo1.Length;
            dataMyinfo2.CopyTo(sendBuff, sendBuffIndex);
            sendBuffIndex += dataMyinfo2.Length;
            dataMyinfo3.CopyTo(sendBuff, sendBuffIndex);
            sendBuffIndex += dataMyinfo3.Length;
            dataMyinfo4.CopyTo(sendBuff, sendBuffIndex);
            sendBuffIndex += dataMyinfo4.Length;
            dataMyinfo5.CopyTo(sendBuff, sendBuffIndex);
            sendBuffIndex += dataMyinfo5.Length;
            socket.Send(sendBuff);
            oldMajia = nick;
            lastMsg = "完成改马甲:" + DateTime.Now;
        }


        /////////////////////////////////////////////////以下无效//////////////
                
        /// <summary>
        /// 变换频道
        /// </summary>
        /// <param name="topsid"></param>
        /// <param name="subsid"></param>
        bool ChangeChannel(uint topsid, uint subsid)
        {
            if (!isLogined)//未登录
                return false;

            channelVerifyUrl = null;
            string changelVerify = null;
            if (IsChanneled)
                return true;
            changeChannelOk = false;
            //伺服对象
            if (Common.ServoUser.myname == myname)
            {
                while (true)
                {
                    //是否需要验证码
                    if (!string.IsNullOrEmpty(channelVerifyUrl))
                    {
                        Common.verForm.VerUrl = channelVerifyUrl;
                        changelVerify = Common.verForm.VerCode;
                        channelVerifyUrl = null; //清空验证码地址
                    }
                    //进入频道
                    ChangeChannel(topsid, subsid, changelVerify);
                    changelVerify = null;//清空验证码
                    //等待消息循环线程完成
                    while (changeChannelAndMsgLoopFlag)
                        Thread.Sleep(30);
                    //将互斥标志清空
                    lock (changeChannelAndMsgLoop)
                    {
                        changeChannelAndMsgLoopFlag = true;
                    }
                    if (changeChannelOk)
                        return true;
                }
            }
            else
            {
                while (true)
                {
                    //需要数据
                    noNeedData = false;
                    //是否需要验证码
                    if (!string.IsNullOrEmpty(channelVerifyUrl))
                    {
                        Common.verForm.VerUrl = channelVerifyUrl;
                        changelVerify = Common.verForm.VerCode;
                        channelVerifyUrl = null; //清空验证码地址
                    }
                    //进入频道
                    ChangeChannel(topsid, subsid, changelVerify);
                    changelVerify = null;//清空验证码

                    //处理是否有返回验证码地址
                    byte[] cmdBuff = new byte[1024];
                    int i = 0;
                    while (i < 10)
                    {
                        i++;
                        int N = socket.Receive(cmdBuff);
                        if (N <= 0)
                            return true;
                        MemoryStream ms = new MemoryStream(cmdBuff, 0, N);
                        ByteArray m_recvBuff = new ByteArray(ms);
                        uint _local4 = Parse(m_recvBuff);
                        if (_local4 != REC_SUCCESS)//网络数据未判断是否完整接收，进频道错误，就有可能是这里的问题。
                            continue;
                        ASObject ao = (ASObject)m_recvBuff.ReadObject();
                        uint uriv = uint.Parse(ao["uri"].ToString());
                        if (uriv == 356)//需要验证码
                        {
                            ao = (ASObject)ao["change_topsid_ack"];
                            channelVerifyUrl = ao["verify_url"].ToString();
                            changeChannelOk = false;
                            break;
                        }
                        else if (uriv == 18)//进入成功
                        {
                            ASObject join_session_ack = (ASObject)ao["join_session_ack"];
                            //未验证result
                            channelVerifyUrl = null;
                            changeChannelOk = true;
                            break;
                        }
                    }
                    //不需要验证码
                    if (string.IsNullOrEmpty(channelVerifyUrl))
                    {
                        //不需要数据
                        noNeedData = true;
                        return true;
                    }
                }
            }
        }

        /// <summary>
        /// 变换频道
        /// </summary>
        /// <param name="topsid"></param>
        /// <param name="subsid"></param>
        void ChangeChannel(uint topsid, uint subsid, string ver)
        {
            netParams.topSid = topsid;
            netParams.subSid = subsid;
            //#change_topsid_req                                                                                                                                                                       
            //00:00:00:7c:0a:0b:01:23:63:68:61:6e:67:65:5f:74:6f:70:73:69:64:5f:72:65:71:0a:01:
            byte[] change_topsid_req = { 0, 0, 0, 124, 10, 11, 1, 35, 99, 104, 97, 110, 103, 101, 95, 116, 111, 112, 115, 105, 100, 95, 114, 101, 113, 10, 1 };
            //token
            //0b:74:6f:6b:65:6e:06:61:41:41:41:41:44:41:7a:67:71:47:62:35:4f:70:77:77:49:4f:4d:7a:6c:4f:51:6b:70:4f:31:5a:41:63:6b:45:5a:64:77:30:41:4a:7a:4b:69:77:6f:4a:57:68:53:4a:      
            byte[] tokentag = { 11, 116, 111, 107, 101, 110, 6, 97 };
            byte[] tokenval = Encoding.UTF8.GetBytes(netParams.token);
            //topsid                      
            //0d:74:6f:70:73:69:64:04:92:61:   
            byte[] topsidtag = { 13, 116, 111, 112, 115, 105, 100, 4 };
            byte[] topsidval = Common.ASIntToByte(topsid);
            // subsid                       
            //0d:73:75:62:73:69:64:04:00:  
            byte[] subsidtag = { 13, 115, 117, 98, 115, 105, 100, 4 };
            byte[] subsidval = Common.ASIntToByte(subsid);
            //verify_ans                                   
            //15:76:65:72:69:66:79:5f:61:6e:73:06:01:
            //15:76:65:72:69:66:79:5f:61:6e:73:06:01: 
            byte[] verify_ans;
            if (string.IsNullOrEmpty(ver))
                verify_ans = new byte[] { 21, 118, 101, 114, 105, 102, 121, 95, 97, 110, 115, 6, 1 };
            else
            {
                ver = ver.ToLower();
                byte[] vercodbyte = Encoding.UTF8.GetBytes(ver);
                verify_ans = new byte[] { 21, 118, 101, 114, 105, 102, 121, 95, 97, 110, 115, 6, 9, vercodbyte[0], vercodbyte[1], vercodbyte[2], vercodbyte[3] };
            }
            //uri
            //01:07:75:72:69:04:82:63:01
            byte[] uri = { 1, 7, 117, 114, 105, 4, 130, 99, 1 };
            int length = change_topsid_req.Length + tokentag.Length + tokenval.Length + topsidtag.Length + topsidval.Length + subsidtag.Length + subsidval.Length + verify_ans.Length + uri.Length;
            byte[] reqData = new byte[length];
            change_topsid_req[3] = (byte)length;
            int index = 0;
            change_topsid_req.CopyTo(reqData, index);
            index += change_topsid_req.Length;
            tokentag.CopyTo(reqData, index);
            index += tokentag.Length;
            tokenval.CopyTo(reqData, index);
            index += tokenval.Length;
            topsidtag.CopyTo(reqData, index);
            index += topsidtag.Length;
            topsidval.CopyTo(reqData, index);
            index += topsidval.Length;
            subsidtag.CopyTo(reqData, index);
            index += subsidtag.Length;
            subsidval.CopyTo(reqData, index);
            index += subsidval.Length;
            verify_ans.CopyTo(reqData, index);
            index += verify_ans.Length;
            uri.CopyTo(reqData, index);
            index += uri.Length;
            socket.Send(reqData, reqData.Length, SocketFlags.None);
        }
    }

}
