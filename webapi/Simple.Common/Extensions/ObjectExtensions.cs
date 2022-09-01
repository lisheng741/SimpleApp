namespace System;

public static class ObjectExtensions
{
    /// <summary>
    /// 判断列表中是否存在对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="item"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool IsIn<T>(this T item, params T[] list)
    {
        return list.Contains(item);
    }
}
