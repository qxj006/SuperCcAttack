using System;
using System.Collections.Generic;

[Serializable]
public class Config
{
    /// <summary>
    /// 攻击网址列表
    /// </summary>
    public List<string> HttpUrlList = new List<string>();

    /// <summary>
    /// 服务器IP地址
    /// </summary>
    public string ServerIp = string.Empty;

    /// <summary>
    /// 自定义请求头
    /// </summary>
    public Dictionary<string, string> HttpRequestHeaders = new Dictionary<string, string>();

    /// <summary>
    /// 是否使用随机代理IP地址
    /// </summary>
    public bool RandomIp = true;

    /// <summary>
    /// 随机浏览器标识
    /// </summary>
    public bool RandomUserAgent = true;

    /// <summary>
    /// 随机搜索引擎浏览器标识
    /// </summary>
    public bool RandomSpiderUserAgent;

    /// <summary>
    /// 自动解压缩响应内容
    /// </summary>
    public bool HttpAutomaticDecompression;

    /// <summary>
    /// 是否保持连接
    /// </summary>
    public bool KeepAlive = true;

    /// <summary>
    /// 是否通过管线传输请求
    /// </summary>
    public bool Pipelined = false;

    /// <summary>
    /// 请求数据内容
    /// </summary>
    public string HttpRequestContent;

    /// <summary>
    /// 请求超时时间(秒)
    /// </summary>
    public double HttpTimeout = 30;

    /// <summary>
    /// 正常关键词
    /// </summary>
    public string NormalWords = string.Empty;

    /// <summary>
    /// 失败关键词
    /// </summary>
    public string FailWords = string.Empty;



    /// <summary>
    /// 代理服务器获取地址
    /// </summary>
    public string ProxyApiUrl = string.Empty;

    /// <summary>
    /// 提取代理间隔(毫秒)
    /// </summary>
    public int ProxyInterval = 0;

    /// <summary>
    /// 提取代理重试次数
    /// </summary>
    public int ProxyRetry = 0;

    /// <summary>
    /// 提取失败重试间隔(毫秒)
    /// </summary>
    public int ProxyRetryInterval = 0;

    /// <summary>
    /// 提取代理最大出错次数
    /// </summary>
    public int ProxyMaxFail = 0;

    /// <summary>
    /// 每个代理存活时长
    /// </summary>
    public int PerProxyLiveSeconds = 30;

    /// <summary>
    /// 每个代理连续出错多少次退出
    /// </summary>
    public int PerProxyMaxFails = 60;

    /// <summary>
    /// 代理服务器
    /// </summary>
    public string ProxyServer = string.Empty;

    /// <summary>
    /// 代理服务器用户名
    /// </summary>
    public string ProxyUserName = string.Empty;

    /// <summary>
    /// 代理服务器密码
    /// </summary>
    public string ProxyPassword = string.Empty;



    /// <summary>
    /// 防火墙关键词列表,以,分割
    /// </summary>
    public string WAFWords = string.Empty;

    /// <summary>
    /// 前N次为防火墙验证请求
    /// </summary>
    public int WafVerify = 0;

    /// <summary>
    /// 限制请求时间(秒)
    /// </summary>
    public double LimitTime = 1.1;

    /// <summary>
    /// 限制请求个数
    /// </summary>
    public int LimitRequest = 100;

    /// <summary>
    /// 请求间隔最小时间(秒)
    /// </summary>
    public int SleepTimeMin;

    /// <summary>
    /// 请求间隔最长时间(秒)
    /// </summary>
    public int SleepTimeMax;

    /// <summary>
    /// 最大并行请求数
    /// </summary>
    public int MaxConcurrence = 10;


    /// <summary>
    /// 固定数字
    /// </summary>
    public int Number = 0;
}
