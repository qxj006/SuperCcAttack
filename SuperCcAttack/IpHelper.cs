using System;
using System.Text;

public class IpHelper
{
    public long IPToNumber(string ip)
    {
        string[] arrayIP = ip.Split('.');
        long sip1 = long.Parse(arrayIP[0]);
        long sip2 = long.Parse(arrayIP[1]);
        long sip3 = long.Parse(arrayIP[2]);
        long sip4 = long.Parse(arrayIP[3]);
        long ipNum = sip1 * 256 * 256 * 256 + sip2 * 256 * 256 + sip3 * 256 + sip4;
        return ipNum;
    }

    public string NumberToIP(long ipNum)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append((ipNum >> 24) & 0xFF).Append(".");
        builder.Append((ipNum >> 16) & 0xFF).Append(".");
        builder.Append((ipNum >> 8) & 0xFF).Append(".");
        builder.Append(ipNum & 0xFF);
        return builder.ToString();
    }

    public static string getRandomIp()
    {
        int[][] range =
            {
                new int[]{607649792,608174079},//36.56.0.0-36.63.255.255
                new int[]{1038614528,1039007743},//61.232.0.0-61.237.255.255
                new int[]{1783627776,1784676351},//106.80.0.0-106.95.255.255
                new int[]{2035023872,2035154943},//121.76.0.0-121.77.255.255
                new int[]{2078801920,2079064063},//123.232.0.0-123.235.255.255
                new int[]{-1950089216,-1948778497},//139.196.0.0-139.215.255.255
                new int[]{-1425539072,-1425014785},//171.8.0.0-171.15.255.255
                new int[]{-1236271104,-1235419137},//182.80.0.0-182.92.255.255
                new int[]{-770113536,-768606209},//210.25.0.0-210.47.255.255
                new int[]{-569376768,-564133889} //222.16.0.0-222.95.255.255
            };

        Random random = new Random();

        int index = random.Next(10);

        return num2ip(range[index][0] + new Random().Next(range[index][1] - range[index][0]));
    }

    public static string num2ip(int ip)
    {
        int[] b = new int[4];
        string x = "";
        //位移然后与255 做高低位转换
        b[0] = (int)((ip >> 24) & 0xff);
        b[1] = (int)((ip >> 16) & 0xff);
        b[2] = (int)((ip >> 8) & 0xff);
        b[3] = (int)(ip & 0xff);
        x = (b[0]).ToString() + "." + (b[1]).ToString() + "." + (b[2]).ToString() + "." + (b[3]).ToString();
        return x;
    }
}

