using Newtonsoft.Json;

public class JsonHelper
{
    /// <summary>
    /// Json序列化数据
    /// </summary>
    public static string Serialize<T>(T data)
    {
        return JsonConvert.SerializeObject(data, Formatting.Indented);
    }

    /// <summary>
    /// Json反序列化数据
    /// </summary>
    public static T DeSerialize<T>(string data)
    {
        return (T)JsonConvert.DeserializeObject(data, typeof(T));
    }
}
