using CommandLine;

public class Options
{
    [Option('p', "proxy", HelpText = "设置代理 例如:127.0.0.1:8080 如果不设置自动使用系统代理")]
    public string Proxy { get; set; }

    [Option('s', "serverip", HelpText = "指定服务器IP")]
    public string ServerIp { get; set; }

    [Option('a', "autorun", HelpText = "是否自动运行")]
    public bool AutoRun { get; set; }
}
