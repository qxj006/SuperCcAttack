
namespace SuperCcAttack
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.RichTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.HttpUrlList = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Pipelined = new System.Windows.Forms.CheckBox();
            this.KeepAlive = new System.Windows.Forms.CheckBox();
            this.HttpAutomaticDecompression = new System.Windows.Forms.CheckBox();
            this.RandomSpiderUserAgent = new System.Windows.Forms.CheckBox();
            this.RandomUserAgent = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.HttpRequestHeaders = new System.Windows.Forms.TextBox();
            this.RandomIp = new System.Windows.Forms.CheckBox();
            this.HttpRequestContent = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Number = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.FailWords = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.HttpTimeout = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.NormalWords = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.MaxConcurrence = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.SleepTimeMax = new System.Windows.Forms.TextBox();
            this.SleepTimeMin = new System.Windows.Forms.TextBox();
            this.btnClearStatisticInfo = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.LimitTime = new System.Windows.Forms.TextBox();
            this.LimitRequest = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.ProxyPassword = new System.Windows.Forms.TextBox();
            this.ProxyUserName = new System.Windows.Forms.TextBox();
            this.ProxyServer = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.ProxyRetry = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.ProxyRetryInterval = new System.Windows.Forms.TextBox();
            this.PerProxyMaxFails = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.ProxyMaxFail = new System.Windows.Forms.TextBox();
            this.ProxyInterval = new System.Windows.Forms.TextBox();
            this.PerProxyLiveSeconds = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.ProxyApiUrl = new System.Windows.Forms.TextBox();
            this.ServerIp = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.WafVerify = new System.Windows.Forms.TextBox();
            this.WAFWords = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label37 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // RichTextBoxLog
            // 
            this.RichTextBoxLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.RichTextBoxLog.Location = new System.Drawing.Point(0, 757);
            this.RichTextBoxLog.Name = "RichTextBoxLog";
            this.RichTextBoxLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.RichTextBoxLog.Size = new System.Drawing.Size(690, 204);
            this.RichTextBoxLog.TabIndex = 0;
            this.RichTextBoxLog.Text = "";
            // 
            // HttpUrlList
            // 
            this.HttpUrlList.Location = new System.Drawing.Point(104, 8);
            this.HttpUrlList.Multiline = true;
            this.HttpUrlList.Name = "HttpUrlList";
            this.HttpUrlList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.HttpUrlList.Size = new System.Drawing.Size(409, 79);
            this.HttpUrlList.TabIndex = 1;
            this.HttpUrlList.TextChanged += new System.EventHandler(this.HttpUrlList_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 36);
            this.label1.TabIndex = 5;
            this.label1.Text = "攻击网址列表\r\n([Post]开头\r\n为Post请求)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Pipelined);
            this.groupBox1.Controls.Add(this.KeepAlive);
            this.groupBox1.Controls.Add(this.HttpAutomaticDecompression);
            this.groupBox1.Controls.Add(this.RandomSpiderUserAgent);
            this.groupBox1.Controls.Add(this.RandomUserAgent);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.HttpRequestHeaders);
            this.groupBox1.Controls.Add(this.RandomIp);
            this.groupBox1.Controls.Add(this.HttpRequestContent);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(16, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(660, 231);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "请求头设置";
            // 
            // Pipelined
            // 
            this.Pipelined.AutoSize = true;
            this.Pipelined.Location = new System.Drawing.Point(577, 113);
            this.Pipelined.Name = "Pipelined";
            this.Pipelined.Size = new System.Drawing.Size(78, 16);
            this.Pipelined.TabIndex = 20;
            this.Pipelined.Text = "Pipelined";
            this.Pipelined.UseVisualStyleBackColor = true;
            this.Pipelined.CheckedChanged += new System.EventHandler(this.Pipelined_CheckedChanged);
            // 
            // KeepAlive
            // 
            this.KeepAlive.AutoSize = true;
            this.KeepAlive.Checked = true;
            this.KeepAlive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.KeepAlive.Location = new System.Drawing.Point(493, 113);
            this.KeepAlive.Name = "KeepAlive";
            this.KeepAlive.Size = new System.Drawing.Size(78, 16);
            this.KeepAlive.TabIndex = 19;
            this.KeepAlive.Text = "KeepAlive";
            this.KeepAlive.UseVisualStyleBackColor = true;
            this.KeepAlive.CheckedChanged += new System.EventHandler(this.KeepAlive_CheckedChanged);
            // 
            // HttpAutomaticDecompression
            // 
            this.HttpAutomaticDecompression.AutoSize = true;
            this.HttpAutomaticDecompression.Checked = true;
            this.HttpAutomaticDecompression.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HttpAutomaticDecompression.Location = new System.Drawing.Point(358, 113);
            this.HttpAutomaticDecompression.Name = "HttpAutomaticDecompression";
            this.HttpAutomaticDecompression.Size = new System.Drawing.Size(132, 16);
            this.HttpAutomaticDecompression.TabIndex = 16;
            this.HttpAutomaticDecompression.Text = "自动解压缩响应内容";
            this.HttpAutomaticDecompression.UseVisualStyleBackColor = true;
            this.HttpAutomaticDecompression.CheckedChanged += new System.EventHandler(this.HttpAutomaticDecompression_CheckedChanged);
            // 
            // RandomSpiderUserAgent
            // 
            this.RandomSpiderUserAgent.AutoSize = true;
            this.RandomSpiderUserAgent.Location = new System.Drawing.Point(199, 113);
            this.RandomSpiderUserAgent.Name = "RandomSpiderUserAgent";
            this.RandomSpiderUserAgent.Size = new System.Drawing.Size(156, 16);
            this.RandomSpiderUserAgent.TabIndex = 14;
            this.RandomSpiderUserAgent.Text = "随机搜索引擎浏览器标识";
            this.RandomSpiderUserAgent.UseVisualStyleBackColor = true;
            this.RandomSpiderUserAgent.CheckedChanged += new System.EventHandler(this.RandomSpiderUserAgent_CheckedChanged);
            // 
            // RandomUserAgent
            // 
            this.RandomUserAgent.AutoSize = true;
            this.RandomUserAgent.Location = new System.Drawing.Point(89, 113);
            this.RandomUserAgent.Name = "RandomUserAgent";
            this.RandomUserAgent.Size = new System.Drawing.Size(108, 16);
            this.RandomUserAgent.TabIndex = 13;
            this.RandomUserAgent.Text = "随机浏览器标识";
            this.RandomUserAgent.UseVisualStyleBackColor = true;
            this.RandomUserAgent.CheckedChanged += new System.EventHandler(this.RandomUserAgent_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "自定义请求头";
            // 
            // HttpRequestHeaders
            // 
            this.HttpRequestHeaders.Location = new System.Drawing.Point(88, 15);
            this.HttpRequestHeaders.Multiline = true;
            this.HttpRequestHeaders.Name = "HttpRequestHeaders";
            this.HttpRequestHeaders.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.HttpRequestHeaders.Size = new System.Drawing.Size(566, 82);
            this.HttpRequestHeaders.TabIndex = 4;
            this.HttpRequestHeaders.TextChanged += new System.EventHandler(this.HttpRequestHeaders_TextChanged);
            // 
            // RandomIp
            // 
            this.RandomIp.AutoSize = true;
            this.RandomIp.Location = new System.Drawing.Point(5, 113);
            this.RandomIp.Name = "RandomIp";
            this.RandomIp.Size = new System.Drawing.Size(84, 16);
            this.RandomIp.TabIndex = 0;
            this.RandomIp.Text = "随机代理IP";
            this.RandomIp.UseVisualStyleBackColor = true;
            this.RandomIp.CheckedChanged += new System.EventHandler(this.RandomIp_CheckedChanged);
            // 
            // HttpRequestContent
            // 
            this.HttpRequestContent.Location = new System.Drawing.Point(88, 142);
            this.HttpRequestContent.Multiline = true;
            this.HttpRequestContent.Name = "HttpRequestContent";
            this.HttpRequestContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.HttpRequestContent.Size = new System.Drawing.Size(566, 82);
            this.HttpRequestContent.TabIndex = 6;
            this.HttpRequestContent.TextChanged += new System.EventHandler(this.HttpRequestContent_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 36);
            this.label3.TabIndex = 5;
            this.label3.Text = "请求数据内容\r\n(仅对Post请\r\n求有效)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(516, 21);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(137, 12);
            this.label13.TabIndex = 17;
            this.label13.Text = "随机中国IP([RandomIp])";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Number);
            this.groupBox2.Controls.Add(this.label38);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.FailWords);
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.HttpTimeout);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.NormalWords);
            this.groupBox2.Location = new System.Drawing.Point(16, 406);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(660, 94);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "其它设置";
            // 
            // Number
            // 
            this.Number.Location = new System.Drawing.Point(557, 58);
            this.Number.Name = "Number";
            this.Number.Size = new System.Drawing.Size(94, 21);
            this.Number.TabIndex = 16;
            this.Number.TextChanged += new System.EventHandler(this.Number_TextChanged);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(497, 63);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(59, 12);
            this.label38.TabIndex = 15;
            this.label38.Text = "固定数字:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(608, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "秒";
            // 
            // FailWords
            // 
            this.FailWords.Location = new System.Drawing.Point(175, 60);
            this.FailWords.Name = "FailWords";
            this.FailWords.Size = new System.Drawing.Size(302, 21);
            this.FailWords.TabIndex = 1;
            this.FailWords.TextChanged += new System.EventHandler(this.FailWords_TextChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(15, 64);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(155, 12);
            this.label26.TabIndex = 0;
            this.label26.Text = "失败响应包含内容(以,分隔)";
            // 
            // HttpTimeout
            // 
            this.HttpTimeout.Location = new System.Drawing.Point(573, 28);
            this.HttpTimeout.Name = "HttpTimeout";
            this.HttpTimeout.Size = new System.Drawing.Size(32, 21);
            this.HttpTimeout.TabIndex = 13;
            this.HttpTimeout.Text = "30";
            this.HttpTimeout.TextChanged += new System.EventHandler(this.HttpTimeout_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "正常响应包含内容(以,分隔)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(495, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "请求超时时间";
            // 
            // NormalWords
            // 
            this.NormalWords.Location = new System.Drawing.Point(174, 28);
            this.NormalWords.Name = "NormalWords";
            this.NormalWords.Size = new System.Drawing.Size(302, 21);
            this.NormalWords.TabIndex = 1;
            this.NormalWords.TextChanged += new System.EventHandler(this.NormalWords_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(445, 63);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 12);
            this.label10.TabIndex = 29;
            this.label10.Text = "最大并发数:";
            // 
            // MaxConcurrence
            // 
            this.MaxConcurrence.Location = new System.Drawing.Point(519, 59);
            this.MaxConcurrence.Name = "MaxConcurrence";
            this.MaxConcurrence.Size = new System.Drawing.Size(39, 21);
            this.MaxConcurrence.TabIndex = 28;
            this.MaxConcurrence.Text = "100";
            this.MaxConcurrence.TextChanged += new System.EventHandler(this.MaxConcurrence_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(404, 63);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(29, 12);
            this.label17.TabIndex = 26;
            this.label17.Text = "毫秒";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(244, 62);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 27;
            this.label14.Text = "请求间隔";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(342, 62);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(17, 12);
            this.label15.TabIndex = 25;
            this.label15.Text = "到";
            // 
            // SleepTimeMax
            // 
            this.SleepTimeMax.Location = new System.Drawing.Point(360, 58);
            this.SleepTimeMax.Name = "SleepTimeMax";
            this.SleepTimeMax.Size = new System.Drawing.Size(42, 21);
            this.SleepTimeMax.TabIndex = 23;
            this.SleepTimeMax.Text = "300";
            this.SleepTimeMax.TextChanged += new System.EventHandler(this.SleepTimeMax_TextChanged);
            // 
            // SleepTimeMin
            // 
            this.SleepTimeMin.Location = new System.Drawing.Point(298, 58);
            this.SleepTimeMin.Name = "SleepTimeMin";
            this.SleepTimeMin.Size = new System.Drawing.Size(40, 21);
            this.SleepTimeMin.TabIndex = 24;
            this.SleepTimeMin.Text = "1000";
            this.SleepTimeMin.TextChanged += new System.EventHandler(this.SleepTimeMin_TextChanged);
            // 
            // btnClearStatisticInfo
            // 
            this.btnClearStatisticInfo.Location = new System.Drawing.Point(604, 49);
            this.btnClearStatisticInfo.Name = "btnClearStatisticInfo";
            this.btnClearStatisticInfo.Size = new System.Drawing.Size(76, 26);
            this.btnClearStatisticInfo.TabIndex = 22;
            this.btnClearStatisticInfo.Text = "清空统计";
            this.btnClearStatisticInfo.UseVisualStyleBackColor = true;
            this.btnClearStatisticInfo.Click += new System.EventHandler(this.btnClearStatisticInfo_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(520, 49);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(76, 26);
            this.btnStart.TabIndex = 22;
            this.btnStart.Text = "开始攻击";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(181, 62);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(59, 12);
            this.label19.TabIndex = 21;
            this.label19.Text = "(0为不限)";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(53, 61);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 12);
            this.label16.TabIndex = 20;
            this.label16.Text = "秒内最多请求";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(168, 61);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(17, 12);
            this.label18.TabIndex = 19;
            this.label18.Text = "个";
            // 
            // LimitTime
            // 
            this.LimitTime.Location = new System.Drawing.Point(17, 56);
            this.LimitTime.Name = "LimitTime";
            this.LimitTime.Size = new System.Drawing.Size(32, 21);
            this.LimitTime.TabIndex = 17;
            this.LimitTime.Text = "60";
            this.LimitTime.TextChanged += new System.EventHandler(this.LimitTime_TextChanged);
            // 
            // LimitRequest
            // 
            this.LimitRequest.Location = new System.Drawing.Point(134, 57);
            this.LimitRequest.Name = "LimitRequest";
            this.LimitRequest.Size = new System.Drawing.Size(32, 21);
            this.LimitRequest.TabIndex = 18;
            this.LimitRequest.Text = "300";
            this.LimitRequest.TextChanged += new System.EventHandler(this.LimitRequest_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(327, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(173, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "随机字符串([String,Min,Max])";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "随机数字([Number,Min,Max])";
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            // 
            // timer
            // 
            this.timer.Interval = 3000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label32);
            this.groupBox3.Controls.Add(this.label33);
            this.groupBox3.Controls.Add(this.label34);
            this.groupBox3.Controls.Add(this.ProxyPassword);
            this.groupBox3.Controls.Add(this.ProxyUserName);
            this.groupBox3.Controls.Add(this.ProxyServer);
            this.groupBox3.Controls.Add(this.label31);
            this.groupBox3.Controls.Add(this.label29);
            this.groupBox3.Controls.Add(this.ProxyRetry);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.ProxyRetryInterval);
            this.groupBox3.Controls.Add(this.PerProxyMaxFails);
            this.groupBox3.Controls.Add(this.label28);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label30);
            this.groupBox3.Controls.Add(this.ProxyMaxFail);
            this.groupBox3.Controls.Add(this.ProxyInterval);
            this.groupBox3.Controls.Add(this.PerProxyLiveSeconds);
            this.groupBox3.Controls.Add(this.label27);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.ProxyApiUrl);
            this.groupBox3.Location = new System.Drawing.Point(16, 520);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(660, 126);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "代理服务器设置";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(10, 96);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(119, 12);
            this.label32.TabIndex = 27;
            this.label32.Text = "指定代理服务器地址:";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(342, 97);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(41, 12);
            this.label33.TabIndex = 26;
            this.label33.Text = "用户名";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(506, 97);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(29, 12);
            this.label34.TabIndex = 25;
            this.label34.Text = "密码";
            // 
            // ProxyPassword
            // 
            this.ProxyPassword.Location = new System.Drawing.Point(540, 93);
            this.ProxyPassword.Name = "ProxyPassword";
            this.ProxyPassword.Size = new System.Drawing.Size(112, 21);
            this.ProxyPassword.TabIndex = 24;
            this.ProxyPassword.TextChanged += new System.EventHandler(this.ProxyPassword_TextChanged);
            // 
            // ProxyUserName
            // 
            this.ProxyUserName.Location = new System.Drawing.Point(387, 93);
            this.ProxyUserName.Name = "ProxyUserName";
            this.ProxyUserName.Size = new System.Drawing.Size(112, 21);
            this.ProxyUserName.TabIndex = 23;
            this.ProxyUserName.TextChanged += new System.EventHandler(this.ProxyUserName_TextChanged);
            // 
            // ProxyServer
            // 
            this.ProxyServer.Location = new System.Drawing.Point(130, 92);
            this.ProxyServer.Name = "ProxyServer";
            this.ProxyServer.Size = new System.Drawing.Size(205, 21);
            this.ProxyServer.TabIndex = 22;
            this.ProxyServer.TextChanged += new System.EventHandler(this.ProxyServer_TextChanged);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(168, 60);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(29, 12);
            this.label31.TabIndex = 21;
            this.label31.Text = "毫秒";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(201, 61);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(131, 12);
            this.label29.TabIndex = 18;
            this.label29.Text = "提取代理最大出错次数:";
            // 
            // ProxyRetry
            // 
            this.ProxyRetry.Location = new System.Drawing.Point(623, 21);
            this.ProxyRetry.Name = "ProxyRetry";
            this.ProxyRetry.Size = new System.Drawing.Size(30, 21);
            this.ProxyRetry.TabIndex = 20;
            this.ProxyRetry.Text = "3";
            this.ProxyRetry.TextChanged += new System.EventHandler(this.ProxyRetry_TextChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(589, 62);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(65, 12);
            this.label23.TabIndex = 4;
            this.label23.Text = "次退出代理";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(522, 25);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(101, 12);
            this.label22.TabIndex = 19;
            this.label22.Text = "提取失败重试次数";
            // 
            // ProxyRetryInterval
            // 
            this.ProxyRetryInterval.Location = new System.Drawing.Point(110, 55);
            this.ProxyRetryInterval.Name = "ProxyRetryInterval";
            this.ProxyRetryInterval.Size = new System.Drawing.Size(52, 21);
            this.ProxyRetryInterval.TabIndex = 20;
            this.ProxyRetryInterval.Text = "500";
            this.ProxyRetryInterval.TextChanged += new System.EventHandler(this.ProxyRetryInterval_TextChanged);
            // 
            // PerProxyMaxFails
            // 
            this.PerProxyMaxFails.Location = new System.Drawing.Point(558, 57);
            this.PerProxyMaxFails.Name = "PerProxyMaxFails";
            this.PerProxyMaxFails.Size = new System.Drawing.Size(28, 21);
            this.PerProxyMaxFails.TabIndex = 3;
            this.PerProxyMaxFails.Text = "60";
            this.PerProxyMaxFails.TextChanged += new System.EventHandler(this.PerProxyMaxFails_TextChanged);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(478, 62);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(77, 12);
            this.label28.TabIndex = 17;
            this.label28.Text = "秒或请求失败";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(494, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 17;
            this.label11.Text = "毫秒";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(9, 59);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(101, 12);
            this.label30.TabIndex = 19;
            this.label30.Text = "提取失败重试间隔";
            // 
            // ProxyMaxFail
            // 
            this.ProxyMaxFail.Location = new System.Drawing.Point(332, 57);
            this.ProxyMaxFail.Name = "ProxyMaxFail";
            this.ProxyMaxFail.Size = new System.Drawing.Size(39, 21);
            this.ProxyMaxFail.TabIndex = 16;
            this.ProxyMaxFail.Text = "30";
            this.ProxyMaxFail.TextChanged += new System.EventHandler(this.ProxyMaxFail_TextChanged);
            // 
            // ProxyInterval
            // 
            this.ProxyInterval.Location = new System.Drawing.Point(440, 19);
            this.ProxyInterval.Name = "ProxyInterval";
            this.ProxyInterval.Size = new System.Drawing.Size(51, 21);
            this.ProxyInterval.TabIndex = 16;
            this.ProxyInterval.Text = "100000";
            this.ProxyInterval.TextChanged += new System.EventHandler(this.ProxyInterval_TextChanged);
            // 
            // PerProxyLiveSeconds
            // 
            this.PerProxyLiveSeconds.Location = new System.Drawing.Point(436, 57);
            this.PerProxyLiveSeconds.Name = "PerProxyLiveSeconds";
            this.PerProxyLiveSeconds.Size = new System.Drawing.Size(39, 21);
            this.PerProxyLiveSeconds.TabIndex = 16;
            this.PerProxyLiveSeconds.Text = "30";
            this.PerProxyLiveSeconds.TextChanged += new System.EventHandler(this.PerProxyLiveSeconds_TextChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(376, 61);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(59, 12);
            this.label27.TabIndex = 15;
            this.label27.Text = "代理时长:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(380, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 15;
            this.label9.Text = "提取间隔:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 12);
            this.label12.TabIndex = 14;
            this.label12.Text = "代理提取网址:";
            // 
            // ProxyApiUrl
            // 
            this.ProxyApiUrl.Location = new System.Drawing.Point(90, 20);
            this.ProxyApiUrl.Name = "ProxyApiUrl";
            this.ProxyApiUrl.Size = new System.Drawing.Size(281, 21);
            this.ProxyApiUrl.TabIndex = 0;
            this.ProxyApiUrl.TextChanged += new System.EventHandler(this.ProxyApiUrl_TextChanged);
            // 
            // ServerIp
            // 
            this.ServerIp.Location = new System.Drawing.Point(578, 14);
            this.ServerIp.Name = "ServerIp";
            this.ServerIp.Size = new System.Drawing.Size(100, 21);
            this.ServerIp.TabIndex = 14;
            this.ServerIp.TextChanged += new System.EventHandler(this.ServerIp_TextChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(521, 17);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(53, 12);
            this.label20.TabIndex = 15;
            this.label20.Text = "服务器IP";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.SleepTimeMax);
            this.groupBox4.Controls.Add(this.SleepTimeMin);
            this.groupBox4.Controls.Add(this.MaxConcurrence);
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.Controls.Add(this.WafVerify);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.LimitTime);
            this.groupBox4.Controls.Add(this.WAFWords);
            this.groupBox4.Controls.Add(this.LimitRequest);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Location = new System.Drawing.Point(18, 661);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(658, 90);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "防火墙设置";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(531, 26);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(113, 12);
            this.label25.TabIndex = 7;
            this.label25.Text = "次为防火墙验证请求";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(486, 25);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(17, 12);
            this.label24.TabIndex = 6;
            this.label24.Text = "前";
            // 
            // WafVerify
            // 
            this.WafVerify.Location = new System.Drawing.Point(506, 21);
            this.WafVerify.Name = "WafVerify";
            this.WafVerify.Size = new System.Drawing.Size(24, 21);
            this.WafVerify.TabIndex = 5;
            this.WafVerify.Text = "0";
            this.WafVerify.TextChanged += new System.EventHandler(this.WafVerify_TextChanged);
            // 
            // WAFWords
            // 
            this.WAFWords.Location = new System.Drawing.Point(173, 23);
            this.WAFWords.Name = "WAFWords";
            this.WAFWords.Size = new System.Drawing.Size(302, 21);
            this.WAFWords.TabIndex = 1;
            this.WAFWords.TextChanged += new System.EventHandler(this.WAFWords_TextChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(7, 28);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(167, 12);
            this.label21.TabIndex = 0;
            this.label21.Text = "防火墙响应包含内容(以,分隔)";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label37);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Location = new System.Drawing.Point(16, 340);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(658, 50);
            this.groupBox5.TabIndex = 23;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "随机标签";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(196, 21);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(113, 12);
            this.label37.TabIndex = 18;
            this.label37.Text = "固定数字([Number])";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 961);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.btnClearStatisticInfo);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.ServerIp);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.HttpUrlList);
            this.Controls.Add(this.RichTextBoxLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "防火墙穿透CC";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox RichTextBoxLog;
        private System.Windows.Forms.TextBox HttpUrlList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox HttpAutomaticDecompression;
        private System.Windows.Forms.CheckBox RandomSpiderUserAgent;
        private System.Windows.Forms.CheckBox RandomUserAgent;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox HttpRequestHeaders;
        private System.Windows.Forms.CheckBox RandomIp;
        private System.Windows.Forms.TextBox HttpRequestContent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox LimitTime;
        private System.Windows.Forms.TextBox LimitRequest;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox HttpTimeout;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox ProxyApiUrl;
        private System.Windows.Forms.Button btnClearStatisticInfo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox SleepTimeMax;
        private System.Windows.Forms.TextBox SleepTimeMin;
        private System.Windows.Forms.TextBox MaxConcurrence;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox ServerIp;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox WAFWords;
        private System.Windows.Forms.TextBox NormalWords;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox PerProxyMaxFails;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox WafVerify;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox ProxyInterval;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox FailWords;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox PerProxyLiveSeconds;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox ProxyMaxFail;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox ProxyRetryInterval;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.CheckBox KeepAlive;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox ProxyRetry;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox ProxyPassword;
        private System.Windows.Forms.TextBox ProxyUserName;
        private System.Windows.Forms.TextBox ProxyServer;
        private System.Windows.Forms.CheckBox Pipelined;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox Number;
        private System.Windows.Forms.Label label38;
    }
}

