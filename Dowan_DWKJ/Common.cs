using System;
using System.Collections.Generic;
using System.Text;
using FluorineFx.AMF3;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Dowan
{
    class Common
    {       
        public static List<User> LoadUserList=new List<User>();//加载帐号
        public static List<User> UserList = new List<User>();  //登录帐号
        public static List<string> MajaList = new List<string>();//马甲
        public static List<string> QianMing = new List<string>();//签名
        public const int heartBeatInvert = 20000;//心跳间隔
        public static DateTime startTime = DateTime.Now;
        public static List<uint> micQueue = new List<uint>();//麦序        
        public static User ServoUser=null;//伺服对象
        public static VerForm verForm = new VerForm();//验证码窗口
        public static Form1 MainForm = null;//验证码的父窗口
        public static int loginNum = 3;//重复登录次数
        public static uint topsid = 0;
        public static Base b = null;
        public static string HomeUrl = "http://yy.com";
        public static Proxy ProxyManager = new Proxy();
        public static string NowMsg;
        public static bool IsFlower = false;     
        public static string token = string.Empty;
        public static string UserName = string.Empty;
        public static uint OnLineNum = 0;//在线
        public static uint LoginedNum = 0;//已登录
        public static string CodeUser = string.Empty;
        public static string CodePass = string.Empty;
        public static uint ProxyNum = 0;
        public static bool LogSwitch = false;//调试日志
        public static bool AccountCheck = true;//检测
        //---------------------------------------------- 
        public static string VerNum = "v1.0";//版本号
        public static string SystemName = "枫竹WEB协议1.0";//经济,黄金,白金，终极
        public static int LoginNum = 100000;//帐号数
        public static int LoginThreadNum = 60;//登录线程
        public static bool ProxySwitch = true;//代理
        public static bool CodeSwitch = true;//打码
        public static bool ProxyService = false;//提供代理服务
        
        
        /// <summary>
        /// uint 流序反转
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static uint endianLittleBig(uint value)
        {
            return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
                (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
        }

        /// <summary>
        /// uint16,ushort 流序反转
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static UInt16 endianLittleBig(UInt16 value)
        {
            return (UInt16)((value & 0xFFU) << 8 | (value & 0xFF00U) >> 8);
        }

        /// <summary>
        /// AS的int转byte[]
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static byte[] ASIntToByte(uint i)
        {
            ByteArray ba = new ByteArray();
            ba.WriteObject(i);
            byte[] ss = new byte[16];
            ba.Position = 1;
            ba.ReadBytes(ss, 0, (uint)ss.Length);
            byte[] rs = new byte[ba.Length - 1];
            for (int j = 0; j < rs.Length; j++)
                rs[j] = ss[j];
            return rs;
        }

        static HttpWebRequest request =null;

        // 从一个对象信息生成Json串
        public static string ObjectToJson(byte[] obj)
        {
            string ks = "[";
            foreach (byte v in obj)
                ks += v.ToString() + ",";
            ks = ks.Substring(0, ks.Length - 1) + "]";

            return ks;
        }


        public static byte[] JsonToObject(string jsonString, object obj)
        {
            StringReader sread = new StringReader(jsonString);
            string[] bytes = jsonString.Replace("[", "").Replace("]", "").Split(',');
            byte[] np = new byte[40];
            for (int i = 0; i < bytes.Length; i++)
                np[i] = byte.Parse(bytes[i]);
            return np;
        }     


        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="input"></param>
        public static void WriteLogFile(string input)
        {
            if (!LogSwitch)
                return;
            WriteLogFile(input, null);
        }

        public static void WriteLogFile(string input, string filename)
        {
            if (!LogSwitch)
                return;
            try
            {
                if (string.IsNullOrEmpty(filename))
                    filename = "\\logfile.txt";
                else
                    filename = "\\" + filename;

                /**/
                ///指定日志文件的目录
                string fname = Application.StartupPath + filename;
                /**/
                ///定义文件信息对象
                FileInfo finfo = new FileInfo(fname);

                /**/
                ///判断文件是否存在以及是否大于2K
                if (finfo.Exists && finfo.Length > 2048)
                {
                    /**/
                    ///删除该文件
                    //finfo.Delete();
                }
                /**/
                ///创建只写文件流
                using (FileStream fs = finfo.OpenWrite())
                {
                    /**/
                    ///根据上面创建的文件流创建写数据流
                    StreamWriter w = new StreamWriter(fs);

                    /**/
                    ///设置写数据流的起始位置为文件流的末尾
                    w.BaseStream.Seek(0, SeekOrigin.End);

                    /**/
                    ///写入“Log Entry : ”
                    w.Write("\nLog Entry : ");

                    /**/
                    ///写入当前系统时间并换行
                    w.Write("{0} {1} \r\n", DateTime.Now.ToLongTimeString(),
                        DateTime.Now.ToLongDateString());

                    /**/
                    ///写入日志内容并换行
                    w.Write(input + "\n");

                    /**/
                    ///写入------------------------------------“并换行
                    w.Write("\r\n------------------------------------\r\n");

                    /**/
                    ///清空缓冲区内容，并把缓冲区内容写入基础流
                    w.Flush();

                    /**/
                    ///关闭写数据流
                    w.Close();
                }
            }
            catch { }
        }


        public static void ProxyStaics(string input)
        {
            try
            {
                string filename = "\\ProxyStaics.txt";
                /**/
                ///指定日志文件的目录
                string fname = Application.StartupPath + filename;
                /**/
                ///定义文件信息对象
                FileInfo finfo = new FileInfo(fname);

                /**/
                ///判断文件是否存在以及是否大于2K
                if (finfo.Exists && finfo.Length > 2048)
                {
                    /**/
                    ///删除该文件
                    //finfo.Delete();
                }
                /**/
                ///创建只写文件流
                using (FileStream fs = finfo.OpenWrite())
                {
                    /**/
                    ///根据上面创建的文件流创建写数据流
                    StreamWriter w = new StreamWriter(fs);

                    /**/
                    ///设置写数据流的起始位置为文件流的末尾
                    w.BaseStream.Seek(0, SeekOrigin.End);

                    /**/
                    ///写入“Log Entry : ”
                    //w.Write("\nLog Entry : ");

                    /**/
                    ///写入当前系统时间并换行
                    w.Write("{0} {1} \r\n", DateTime.Now.ToLongTimeString(),
                        DateTime.Now.ToLongDateString());

                    /**/
                    ///写入日志内容并换行
                    w.Write(input + "\n");

                    /**/
                    ///写入------------------------------------“并换行
                    //w.Write("\r\n------------------------------------\r\n");

                    /**/
                    ///清空缓冲区内容，并把缓冲区内容写入基础流
                    w.Flush();

                    /**/
                    ///关闭写数据流
                    w.Close();
                }
            }
            catch { }
        }

        /// <summary>
        /// 网络数据转Byte
        /// </summary>
        /// <param name="netDataPack"></param>
        /// <returns></returns>
        public static byte[] NetDataToByte(string netDataPack)
        {
            string[] netDataPacks = netDataPack.Split(':');
            byte[] byteData = new byte[netDataPacks.Length];
            for (int i = 0; i < netDataPacks.Length; i++)
            {
                byteData[i] = byte.Parse(netDataPacks[i], System.Globalization.NumberStyles.HexNumber);
            }
            return byteData;
        }

        /// <summary> 
        /// 字符串转16进制字节数组 
        /// </summary> 
        /// <param name="hexString"></param> 
        /// <returns></returns> 
        public static byte[] HexstrToByte(String str)
        {
            int length = str.Length / 2;
            if ((str.Length % 2) != 0)
                length++;
            byte[] byteData=new byte[length];
            string tmpStr = string.Empty;
            for(int i=0;i<length;i++)
            {
                if (str.Length >= 2)
                {
                    tmpStr = str.Substring(str.Length - 2, 2);
                    str = str.Substring(0, str.Length - 2);
                }
                else
                    tmpStr = str;               
                byteData[length-1-i] = byte.Parse(tmpStr, System.Globalization.NumberStyles.HexNumber);
            }
            return byteData;
        }

               

        static string Gb2312All()
        {
            byte[] guid =Guid.NewGuid().ToByteArray();
            int charLen = new Random(guid[0]+guid[13]+guid[9]).Next(0,3);

            int area, code;//汉字由区位和码位组成(都为0-94,其中区位16-55为一级汉字区,56-87为二级汉字区,1-9为特殊字符区)
            string charArrary = string.Empty;
            guid = Guid.NewGuid().ToByteArray();
            Random rand = new Random(guid[8] + guid[6] + guid[15]);
            for (int i = 0; i < charLen; i++)
            {
                area = rand.Next(16, 88);
                if (area == 55)//第55区只有89个字符
                {
                    code = rand.Next(1, 90);
                }
                else
                {
                    code = rand.Next(1, 94);
                }
                charArrary += Encoding.GetEncoding("GB2312").GetString(new byte[] { Convert.ToByte(area + 160), Convert.ToByte(code + 160) });
            }
            return charArrary;

        }

        static string Gb2312G()
        {
            byte[] guid = Guid.NewGuid().ToByteArray();
            int count = new Random(guid[0] + guid[13] + guid[9]).Next(1,8);
            string chineseWords = string.Empty;
            guid = Guid.NewGuid().ToByteArray();
            Random rm = new Random(guid[1] + guid[0] + guid[15]);
            Encoding gb = Encoding.GetEncoding("gb2312");

            for (int i = 0; i < count; i++)
            {
                // 获取区码(常用汉字的区码范围为16-55)   
                int regionCode = rm.Next(16, 56);
                // 获取位码(位码范围为1-94 由于55区的90,91,92,93,94为空,故将其排除)   
                int positionCode;
                if (regionCode == 55)
                {
                    // 55区排除90,91,92,93,94   
                    positionCode = rm.Next(1, 90);
                }
                else
                {
                    positionCode = rm.Next(1, 95);
                }

                // 转换区位码为机内码   
                int regionCode_Machine = regionCode + 160;// 160即为十六进制的20H+80H=A0H   
                int positionCode_Machine = positionCode + 160;// 160即为十六进制的20H+80H=A0H   

                // 转换为汉字   
                byte[] bytes = new byte[] { (byte)regionCode_Machine, (byte)positionCode_Machine };
                chineseWords+=gb.GetString(bytes);
            }
            return chineseWords;   

        }

        public static string Gb2312()
        {
            return Gb2312All() + Gb2312G();
        }
    }
}
