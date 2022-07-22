using System.Xml;

namespace System.Reflection;

public static class SummaryExtensions
{
    private const string summaryXPathTemplate = "doc/members/member[@name='{0}']/summary";

    private static readonly Dictionary<Assembly, XmlDocument> _cache = new Dictionary<Assembly, XmlDocument>();

    private static readonly Dictionary<Assembly, Exception> _cacheFail = new Dictionary<Assembly, Exception>();

    /// <summary>
    /// 获取注释
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string GetSummary(this Type type)
    {
        return GetSummary(type, $"T:{type.FullName}");
    }

    /// <summary>
    /// 获取注释
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public static string GetSummary(this MemberInfo info)
    {
        char prefix = info.MemberType.ToString()[0];
        return GetSummary(info.DeclaringType!, $"{prefix}:{info.DeclaringType!.FullName}.{info.Name}");
    }

    /// <summary>
    /// 获取注释
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string GetSummary(Type type, string name)
    {
        XmlDocument? xmlDocument = GetXmlDocument(type.Assembly);
        if (xmlDocument == null)
        {
            return "";
        }

        XmlNode? node = xmlDocument.SelectSingleNode(string.Format(summaryXPathTemplate, name));
        if (node == null)
        {
            return "";
        }

        return node.InnerText.Trim();
    }

    /// <summary>
    /// 获取程序集对应的 XML 注释文档
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    private static XmlDocument? GetXmlDocument(Assembly assembly)
    {
        // 如果有失败的记录，直接返回 null
        if (_cacheFail.ContainsKey(assembly))
        {
            return null;
        }

        try
        {
            // 读取程序集对应的 XML 文档
            if (!_cache.ContainsKey(assembly))
            {
                _cache.Add(assembly, GetXmlDocumentNoCache(assembly));
            }
            return _cache[assembly];
        }
        catch (Exception ex)
        {
            // 记录读取失败的程序集
            _cacheFail.Add(assembly, ex);
            return null;
        }
    }

    /// <summary>
    /// 获取程序集对应的 XML 注释文档
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    private static XmlDocument GetXmlDocumentNoCache(Assembly assembly)
    {
        string xmlName = string.Concat(assembly.GetName().Name, ".xml");
        string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlName);

        var xmlDocument = new XmlDocument();
        xmlDocument.Load(xmlPath);
        return xmlDocument;
    }
}
