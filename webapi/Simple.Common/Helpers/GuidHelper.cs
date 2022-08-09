using System.Security.Cryptography;

namespace Simple.Common.Helpers;

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
    private const byte Rfc4122Version4 = (byte)4;
    private const byte Rfc4122Variant = (byte)8;
    private const byte FilterHighBit = 0b00001111;
    private const byte FilterLowBit = 0b11110000;
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
    /// 生成连续 Guid（生成的 Guid 并不符合 RFC 4122 标准）.
    /// 来源：Abp. from jhtodd/SequentialGuid https://github.com/jhtodd/SequentialGuid/blob/master/SequentialGuid/Classes/SequentialGuid.cs .
    /// </summary>
    /// <param name="guidType"></param>
    /// <returns></returns>
    public static Guid NextOld(SequentialGuidType guidType)
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

                // .NET中，Data1、Data2、Data3 块 分别视为 Int32、Int16、Int16，在小端系统，需要翻转这3个块。
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
                Buffer.BlockCopy(timestampBytes, 6, guidBytes, 8, 2);
                Buffer.BlockCopy(timestampBytes, 0, guidBytes, 10, 6);
                break;
        }

        return new Guid(guidBytes);
    }

    /// <summary>
    /// 生成连续 Guid.
    /// </summary>
    /// <param name="guidType"></param>
    /// <returns></returns>
    public static Guid Next(SequentialGuidType guidType)
    {
        // see: What is a GUID? http://guid.one/guid
        // see: https://github.com/richardtallent/RT.Comb#gory-details-about-uuids-and-guids
        // According to RFC 4122:
        // dddddddd-dddd-Mddd-Ndrr-rrrrrrrrrrrr
        // - M = RFC 版本（version）, 版本4的话，值为4
        // - N = RFC 变体（variant），值为 8, 9, A, B 其中一个，这里固定为8
        // - d = 从公元1年1月1日0时至今的时钟周期数（DateTime.UtcNow.Ticks）
        // - r = 随机数（random bytes）

        var randomBytes = new byte[8];
        _randomNumberGenerator.GetBytes(randomBytes);

        long timestamp = DateTimeOffset.UtcNow.Ticks;

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

                // AsString: dddddddd-dddd-Mddd-Ndrr-rrrrrrrrrrrr

                // dddddddd-dddd : 时间戳前6个字节，共48位
                Buffer.BlockCopy(timestampBytes, 0, guidBytes, 0, 6);
                // Md : 高4位为版本 | 低4位取时间戳序号[6]的元素的高4位
                guidBytes[6] = (byte)((Rfc4122Version4 << 4) | ((timestampBytes[6] & FilterLowBit) >> 4));
                // dd : 高4位取：[6]低4位 | 低4位取：[7]高4位
                guidBytes[7] = (byte)(((timestampBytes[6] & FilterHighBit) << 4) | ((timestampBytes[7] & FilterLowBit) >> 4));
                // Nd : 高4位为：变体 | 低4位取：[7]低4位
                guidBytes[8] = (byte)((Rfc4122Variant << 4) | (timestampBytes[7] & FilterHighBit));
                // rr-rrrrrrrrrrrr : 余下7个字节由随机数组填充
                Buffer.BlockCopy(randomBytes, 0, guidBytes, 9, 7);

                // .NET中，Data1、Data2、Data3 块 分别视为 Int32、Int16、Int16，在小端系统，需要翻转这3个块。
                if (guidType == SequentialGuidType.AsString && BitConverter.IsLittleEndian)
                {
                    Array.Reverse(guidBytes, 0, 4);
                    Array.Reverse(guidBytes, 4, 2);
                    Array.Reverse(guidBytes, 6, 2);
                }

                break;

            case SequentialGuidType.AtEnd:

                // AtEnd: rrrrrrrr-rrrr-Mxdr-Nddd-dddddddddddd
                // Block: 1        2    3    4    5
                // Data : 1        2    3    4
                // Data4 = Block4 + Block5
                // 排序顺序：Block5 > Block4 > Block3 > Block2 > Block1
                // Data3 = Block3 被认为是 uint16，排序并不是从左到右，为消除影响，x 取固定值0

                // rrrrrrrr-rrrr
                Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 6);
                // Mx : 高4位为版本 | 低4位取：全0
                guidBytes[6] = (byte)(Rfc4122Version4 << 4);
                // dr : 高4位为：时间戳[7]低4位 | 低4位取：随机数
                guidBytes[7] = (byte)(((timestampBytes[7] & FilterHighBit) << 4) | (randomBytes[7] & FilterHighBit));
                // Nd : 高4位为：变体 | 低4位取：时间戳[6]高4位
                guidBytes[8] = (byte)((Rfc4122Variant << 4) | ((timestampBytes[6] & FilterLowBit) >> 4));
                // dd : 高4位为：时间戳[6]低4位 | 低4位取：时间戳[7]高4位
                guidBytes[9] = (byte)(((timestampBytes[6] & FilterHighBit) << 4) | ((timestampBytes[7] & FilterLowBit) >> 4));
                // dddddddddddd : 时间戳前6个字节
                Buffer.BlockCopy(timestampBytes, 0, guidBytes, 10, 6);

                if (BitConverter.IsLittleEndian)
                {
                    //Array.Reverse(guidBytes, 0, 4); // 随机数就不翻转了
                    //Array.Reverse(guidBytes, 4, 2);
                    Array.Reverse(guidBytes, 6, 2); // 包含版本号的 Data3 块需要翻转
                }
                break;
        }

        return new Guid(guidBytes);
    }
}
