using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Dowan
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                Application.Run(new Login());
            }
            catch (Exception e)
            {
                Common.WriteLogFile("全局异常：" + e.ToString());
                MessageBox.Show("全局异常,请联系作者:" + e.ToString());
            }
        }


        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Common.WriteLogFile("线程异常：" + e.ToString());
            MessageBox.Show("线程异常,请联系作者：" + e.ToString());
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = "";
            Exception error = e.ExceptionObject as Exception;           
            if (error != null) 
                str = string.Format("UnhandledException:{0};\n\r堆栈信息:{1}", error.Message, error.StackTrace); 
            else
                str = string.Format("Application UnhandledError:{0}", e);

            Common.WriteLogFile("未知异常:" + str);
            MessageBox.Show("未知异常,请联系作者："+e.ToString());
        }

    }
}
