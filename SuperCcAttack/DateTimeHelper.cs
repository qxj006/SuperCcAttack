using System;

public class DateTimeHelper
{
    /// <summary>
    /// 取时间戳，高并发情况下会有重复。想要解决这问题请使用sleep线程睡眠1毫秒。
    /// </summary>
    /// <param name="AccurateToMilliseconds">精确到毫秒</param>
    /// <returns>返回一个长整数时间戳</returns>
    public static long GetTimeStamp(bool AccurateToMilliseconds = false)
    {
        if (AccurateToMilliseconds)
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
        }
        else
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }
    }

    /// <summary>
    /// 时间戳反转为时间，有很多中翻转方法，但是，请不要使用过字符串（string）进行操作，大家都知道字符串会很慢！
    /// </summary>
    /// <param name="TimeStamp">时间戳</param>
    /// <param name="AccurateToMilliseconds">是否精确到毫秒</param>
    /// <returns>返回一个日期时间</returns>
    public static DateTime GetTime(long TimeStamp, bool AccurateToMilliseconds = false)
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区

        if (AccurateToMilliseconds)
        {
            return startTime.AddTicks(TimeStamp * 10000);
        }
        else
        {
            return startTime.AddTicks(TimeStamp * 10000000);
        }
    }
}

