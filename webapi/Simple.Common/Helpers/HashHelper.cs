using System.Security.Cryptography;

namespace Simple.Common.Helpers;

/// <summary>
/// 散列算法帮助类，包含 MD5、SHA1、SHA256 等
/// </summary>
public static class HashHelper
{
    //public static string Create(string input)
    //{
    //    using var md5 = MD5.Create();
    //    var result = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
    //    var strResult = BitConverter.ToString(result);
    //    return strResult.Replace("-", "").ToLower();
    //}

    public static string Md5(string input, string format = "x2")
    {
        var hash = MD5.Create();
        var hashBytes = hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hashBytes.Length; i++)
        {
            sb.Append(hashBytes[i].ToString(format));
        }
        return sb.ToString();
    }

    public static string Sha1(string input, string format = "x2")
    {
        var hash = SHA1.Create();
        var hashBytes = hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hashBytes.Length; i++)
        {
            sb.Append(hashBytes[i].ToString(format));
        }
        return sb.ToString();
    }

    public static string Sha256(string input, string format = "x2")
    {
        var hash = SHA256.Create();
        var hashBytes = hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hashBytes.Length; i++)
        {
            sb.Append(hashBytes[i].ToString(format));
        }
        return sb.ToString();
    }
}
