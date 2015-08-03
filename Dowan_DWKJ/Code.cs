using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace Dowan
{

    class Code
    {
        ///命令功能:查询剩余验证码点数 
        ///strVcodeUser：联众账号
        ///strVcodePass：联众密码 
        ///return string 成功返回->剩余验证码点数 
        [DllImport("FastVerCode.dll")]
        private static extern string GetUserInfo(string strVcodeUser, string strVcodePass);


        ///命令功能:通过作者的下线注册联众账号 
        ///strUser:注册用户
        ///strPass:注册密码
        ///strEmail:注册邮箱
        ///strQQ:注册qq
        ///strAgentid：开发者软件id
        ///strAgentName:软件开发者账号id
        ///return int  1=成功;-1=网络传输异常;0=未知异常 
        [DllImport("FastVerCode.dll")]
        private static extern int Reglz(string strUser, string strPass, string strEmail, string strQQ, string strAgentid, string strAgentName);

        ///命令功能:通过上传验证码图片字节到服务器进行验证码识别，方便多线程发送 
        ///b:上传验证码图片字节集
        ///len:上传验证码图片字节集长度
        ///strVcodeUser：联众账号
        ///strVcodePass：联众密码
        ///成功返回->验证码结果|!|打码工人；后台没点数了返回:No Money! ；未注册返回:No Reg! ；上传验证码失败:Error:Put Fail!  ；识别超时了:Error:TimeOut!  ；上传无效验证码:Error:empty picture!  
        [DllImport("FastVerCode.dll")]
        private static extern string RecByte(byte[] b, int len, string strVcodeUser, string strVcodePass);

        ///通过上传验证码图片字节到服务器进行验证码识别，方便多线程发送,这个函数可以保护作者的收入
        ///b:上传验证码图片字节集
        ///len:上传验证码图片字节集长度
        ///strVcodeUser：联众账号
        ///strVcodePass：联众密码
        ///strAgentUser：软件开发者账号
        ///成功返回->验证码结果|!|打码工人；后台没点数了返回:No Money! ；未注册返回:No Reg! ；上传验证码失败:Error:Put Fail!  ；识别超时了:Error:TimeOut!  ；上传无效验证码:Error:empty picture!  
        [DllImport("FastVerCode.dll")]
        private static extern string RecByte_A(byte[] b, int len, string strVcodeUser, string strVcodePass, string strAgentUser);

        ///命令功能:通过发送验证码本地图片到服务器识别 
        ///strYZMPath：验证码本地路径，例如（c:\1.jpg)  
        ///strVcodeUser：联众账号
        ///strVcodePass：联众密码
        ///成功返回->验证码结果|!|打码工人；后台没点数了返回:No Money! ；未注册返回:No Reg! ；上传验证码失败:Error:Put Fail!  ；识别超时了:Error:TimeOut!  ；上传无效验证码:Error:empty picture!  
        [DllImport("FastVerCode.dll")]
        private static extern string RecYZM(string strYZMPath, string strVcodeUser, string strVcodePass);

        ///命令功能:通过发送验证码本地图片到服务器识别,这个函数可以保护作者的收入
        ///strYZMPath：验证码本地路径，例如（c:\1.jpg)  
        ///strVcodeUser：联众账号
        ///strVcodePass：联众密码
        ///strAgentUser：软件开发者账号
        ///成功返回->验证码结果|!|打码工人；后台没点数了返回:No Money! ；未注册返回:No Reg! ；上传验证码失败:Error:Put Fail!  ；识别超时了:Error:TimeOut!  ；上传无效验证码:Error:empty picture!  
        [DllImport("FastVerCode.dll")]
        private static extern string RecYZM_A(string strYZMPath, string strVcodeUser, string strVcodePass, string strAgentUser);

        ///命令功能:对打错的验证码进行报告。
        ///strVcodeUser：联众用户
        ///strDaMaWorker：打码工人
        ///返回值类型:空    无返回值
        [DllImport("FastVerCode.dll")]
        private static extern void ReportError(string strVcodeUser, string strDaMaWorker);

        public static string GetCode(Image image)
        {
            string msg = string.Empty;           
            if (string.IsNullOrEmpty(Common.CodePass) || string.IsNullOrEmpty(Common.CodeUser))
                return "打码平台，请输入帐号密码";;
            MemoryStream ms = new MemoryStream();
            Bitmap bmp = new Bitmap(image);
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] photo_byte = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(photo_byte, 0, Convert.ToInt32(ms.Length));
            bmp.Dispose();
            string returnMess = RecByte_A(photo_byte, photo_byte.Length, Common.CodeUser, Common.CodePass, "yilinjun");
            if (returnMess.Equals("No Money!"))
            {
                //MessageBox.Show("点数不足", "友情提示");
                //lockthread = true;
                return "打码平台，点数不足";
            }
            else if (returnMess.Equals("No Reg!"))
            {
                //MessageBox.Show("没有注册", "友情提示");
                //lockthread = true;
                return "打码平台，帐号密码错误";
            }
            else if (returnMess.Equals("Error:Put Fail!"))
            {
                //MessageBox.Show("上传验证码失败", "友情提示");
                return "打码平台，上传验证码失败";
            }
            else if (returnMess.Equals("Error:TimeOut!"))
            {
               // MessageBox.Show("", "友情提示");
                return "打码平台，识别超时";
            }
            else if (returnMess.Equals("Error:empty picture!"))
            {
                //MessageBox.Show("上传无效验证码", "友情提示");
                return "打码平台，上传无效验证码";
            }
            else
            {
                return returnMess.Split('|')[0];
                //textBox6.Text = returnMess.Split('|')[2];
                //MessageBox.Show("识别成功", "友情提示");
            } 
        }
    }
}
