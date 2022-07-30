using System.Security.Cryptography;

namespace Simple.Common.Guids;

public enum SequentialGuidType
{
    /// <summary>
    /// 用于 MySql 和 PostgreSql.
    ///  当使用 <see cref="Guid.ToString()" /> 方法进行格式化时连续.
    /// </summary>
    AsString,

    /// <summary>
    /// 用于 Oracle.
    /// 当使用 <see cref="Guid.ToByteArray()" /> 方法进行格式化时连续.
    /// </summary>
    AsBinary,

    /// <summary>
    /// 用以 SqlServer.
    /// 连续性体现于 GUID 的第4块（Data4）.
    /// </summary>
    AtEnd
}

public static class GuidHelper
{
    private static readonly RandomNumberGenerator _randomNumberGenerator = RandomNumberGenerator.Create();

    /// <summary>
    /// 连续 Guid 类型，默认：AsString.
    /// </summary>
    public static SequentialGuidType SequentialGuidType { get; set; } = SequentialGuidType.AsString;

    /// <summary>
    /// 生成连续 Guid.
    /// </summary>
    /// <returns></returns>
    public static Guid Next()
    {
        return Next(SequentialGuidType);
    }

    /// <summary>
    /// 生成连续 Guid.
    /// 来源：Abp.
    /// from jhtodd/SequentialGuid https://github.com/jhtodd/SequentialGuid/blob/master/SequentialGuid/Classes/SequentialGuid.cs .
    /// 进行了一些调整.
    /// </summary>
    /// <param name="guidType"></param>
    /// <returns></returns>
    public static Guid Next(SequentialGuidType guidType)
    {
        var randomBytes = new byte[8];
        _randomNumberGenerator.GetBytes(randomBytes);

        long timestamp = DateTime.UtcNow.Ticks;

        byte[] timestampBytes = BitConverter.GetBytes(timestamp);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(timestampBytes);
        }

        byte[] guidBytes = new byte[16];

        switch (guidType)
        {
            case SequentialGuidType.AsString:
            case SequentialGuidType.AsBinary:

                // 16位数组：前8位为时间戳，后8位为随机数
                Buffer.BlockCopy(timestampBytes, 0, guidBytes, 0, 8);
                Buffer.BlockCopy(randomBytes, 0, guidBytes, 8, 8);

                // If formatting as a string, we have to compensate for the fact
                // that .NET regards the Data1 and Data2 block as an Int32 and an Int16,
                // respectively.  That means that it switches the order on little-endian
                // systems.  So again, we have to reverse.
                if (guidType == SequentialGuidType.AsString && BitConverter.IsLittleEndian)
                {
                    Array.Reverse(guidBytes, 0, 4);
                    Array.Reverse(guidBytes, 4, 2);
                    Array.Reverse(guidBytes, 6, 2);
                }

                break;

            case SequentialGuidType.AtEnd:

                // 16位数组：前8位为随机数，后8位为时间戳
                Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 8);
                Buffer.BlockCopy(timestampBytes, 0, guidBytes, 8, 8);
                break;
        }

        return new Guid(guidBytes);
    }
}
