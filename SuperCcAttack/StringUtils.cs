using System;

public static class StringUtils
{
    public static bool Contains(this string source, string value, StringComparison stringComparison)
    {
        return source.IndexOf(value, stringComparison) >= 0;
    }

    /// <summary>
    /// 包含字符串(不区分大小写)
    /// </summary>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool ContainsIgnoreCase(this string source, string value)
    {
        return source.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
    }

    /// <summary>
    /// 截取字符串
    /// </summary>
    /// <param name="source"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static string Substring(this string source, string start, string end)
    {
        if ((!source.Contains(start)) || (!source.Contains(end)))
        {
            return string.Empty;
        }

        string text = source.Substring(source.IndexOf(start));

        text = text.Remove(0, start.Length);

        text = text.Substring(0, text.IndexOf(end));

        return text;
    }

    /// <summary>
    /// 截取字符串(不区分大小写)
    /// </summary>
    /// <param name="source"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="ignoreCase"></param>
    /// <returns></returns>
    public static string Substring(this string source, string start, string end, bool ignoreCase)
    {
        if (ignoreCase)
        {
            if ((!source.ContainsIgnoreCase(start)) || (!source.ContainsIgnoreCase(end)))
            {
                return string.Empty;
            }

            string text = source.Substring(source.IndexOf(start, StringComparison.OrdinalIgnoreCase));

            text = text.Remove(0, start.Length);

            text = text.Substring(0, text.IndexOf(end, StringComparison.OrdinalIgnoreCase));

            return text;
        }
        else
        {
            return Substring(source, start, end);
        }
    }
}
