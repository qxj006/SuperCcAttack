using CommandLine;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SuperCcAttack
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunApplication);
        }

        private static void RunApplication(Options options)
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常  
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //处理非UI线程异常  
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(options));
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());

            File.AppendAllText("Exception.txt", str + "\r\n\r\n");
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.Exception, e.ToString());

            File.AppendAllText("Exception.txt", str + "\r\n\r\n");
        }

        /// <summary>  
        /// 生成自定义异常消息  
        /// </summary>  
        /// <param name="ex">异常对象</param>  
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>  
        /// <returns>异常字符串文本</returns>  
        private static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("****************************异常文本****************************");

            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));

            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);

                sb.AppendLine("【异常信息】：" + ex.Message);

                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);

                sb.AppendLine("【异常方法】：" + ex.TargetSite);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }

            sb.AppendLine("***************************************************************");

            return sb.ToString();
        }
    }
}
