using System.Xml;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace System.Reflection;

public static class SummaryExtensions
{
    private const string SummaryXPathTemplate = "doc/members/member[@name='{0}']/summary";

    private static readonly Dictionary<Assembly, XmlDocument> _cache = new Dictionary<Assembly, XmlDocument>();

    private static readonly Dictionary<Assembly, Exception> _cacheFail = new Dictionary<Assembly, Exception>();

    /// <summary>
    /// 获取注释
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string GetSummary(this Type type)
    {
        // 不能用 FullName
        // 因为可能出现如：Simple.Repository.Models.BusinessEntityBase`1[[System.Guid, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]
        // 实际上有用的为 Simple.Repository.Models.BusinessEntityBase`1 这一部分
        //return GetSummary(type, $"T:{type.FullName}");

        // 拼凑如这样的字符串：T:Simple.Repository.Models.BusinessEntityBase`1
        return GetSummary(type, $"T:{type.Namespace}.{type.Name}");
    }

    /// <summary>
    /// 获取注释
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public static string GetSummary(this MemberInfo info)
    {
        // 前缀，如：P、M
        //char prefix = info.MemberType.ToString()[0];

        // 不能用 FullName 理由同 Type 的 GetSummary
        //return GetSummary(info.DeclaringType!, $"{prefix}:{info.DeclaringType!.FullName}.{info.Name}");

        // 拼凑如这样的字符串： P:Simple.Repository.Models.BusinessEntityBase`1.IsDeleted
        //return GetSummary(info.DeclaringType!, $"{prefix}:{info.DeclaringType!.Namespace}.{info.DeclaringType!.Name}.{info.Name}");

        // 如果是有参数的 MethodInfo 没法正确获取，如：M:Simple.WebApi.Controllers.AccountController.Login(Simple.Services.LoginModel)
        // 直接使用 Swagger 的方法，用 XmlCommentsNodeNameHelper 获取 name
        string name = "";
        if (info is MethodInfo methodInfo)
        {
            name = XmlCommentsNodeNameHelper.GetMemberNameForMethod(methodInfo);
        }
        else if (info is PropertyInfo propertyInfo)
        {
            name = XmlCommentsNodeNameHelper.GetMemberNameForFieldOrProperty(propertyInfo);
        }
        return GetSummary(info.DeclaringType!, name);
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

            XmlNode? node = xmlDocument.SelectSingleNode(string.Format(SummaryXPathTemplate, name));
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
