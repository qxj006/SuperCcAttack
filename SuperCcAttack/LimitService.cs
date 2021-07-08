using System;

/// <summary>
/// 限流组件,采用数组做为一个环
/// </summary>
public class LimitService
{
    //当前指针的位置
    int currentIndex = 0;

    //限制的时间的秒数，即：x秒允许多少请求
    double limitTimeSencond = 1;

    //请求环的容器数组
    DateTime?[] requestRing = null;

    //容器改变或者移动指针时候的锁
    object objLock = new object();

    /// <summary>
    /// 限制服务
    /// </summary>
    /// <param name="countPerSecond">每秒个数</param>
    /// <param name="_limitTimeSencond">限制的时间的秒数</param>
    public LimitService(int countPerSecond, double _limitTimeSencond)
    {
        requestRing = new DateTime?[countPerSecond];

        limitTimeSencond = _limitTimeSencond;
    }

    /// <summary>
    /// 程序是否可以继续
    /// </summary>
    /// <returns></returns>
    public bool IsContinue()
    {
        lock (objLock)
        {
            var currentNode = requestRing[currentIndex];

            //如果当前节点的值加上设置的秒 超过当前时间，说明超过限制
            if (currentNode != null && currentNode.Value.AddSeconds(limitTimeSencond) > DateTime.Now)
            {
                return false;
            }

            //当前节点设置为当前时间
            requestRing[currentIndex] = DateTime.Now;

            //指针移动一个位置
            MoveNextIndex(ref currentIndex);
        }

        return true;
    }

    /// <summary>
    /// 改变每秒可以通过的请求数
    /// </summary>
    /// <param name="countPerSecond"></param>
    /// <returns></returns>
    public bool ChangeCountPerSecond(int countPerSecond)
    {
        lock (objLock)
        {
            requestRing = new DateTime?[countPerSecond];

            currentIndex = 0;
        }

        return true;
    }

    /// <summary>
    /// 指针往前移动一个位置
    /// </summary>
    /// <param name="currentIndex"></param>
    private void MoveNextIndex(ref int currentIndex)
    {
        if (currentIndex != requestRing.Length - 1)
        {
            currentIndex = currentIndex + 1;
        }
        else
        {
            currentIndex = 0;
        }
    }
}
