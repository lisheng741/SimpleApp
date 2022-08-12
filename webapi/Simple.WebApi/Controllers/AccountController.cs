﻿using System.ComponentModel.DataAnnotations;
using Simple.Services.Account;

namespace Simple.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    [HttpPost]
    public Task<ApiResult> Login([Required] LoginModel login)
        => _accountService.LoginAsync(login);

    [HttpGet]
    public string GetUserInfo()
    {
        return "{\"success\":true,\"code\":200,\"message\":\"请求成功\",\"data\":{\"id\":\"1265476890672672808\",\"account\":\"superAdmin\",\"nickName\":\"超级管理员\",\"name\":\"超级管理员\",\"avatar\":\"1473367216907927553\",\"birthday\":\"2020-03-18 00:00:00.000\",\"sex\":1,\"email\":\"superAdmin@qq.com\",\"phone\":\"18012121011\",\"tel\":\"1234567890\",\"adminType\":1,\"lastLoginIp\":\"127.0.0.1\",\"lastLoginTime\":\"2022-08-12 16:18:39\",\"lastLoginAddress\":\"-\",\"lastLoginBrowser\":\"MSEdge\",\"lastLoginOs\":\"Windows 10 or Windows Server 2016\",\"loginEmpInfo\":{\"jobNum\":null,\"orgId\":null,\"orgName\":null,\"extOrgPos\":[],\"positions\":[]},\"apps\":[{\"code\":\"system\",\"name\":\"系统应用\",\"active\":true},{\"code\":\"office\",\"name\":\"在线办公\",\"active\":false},{\"code\":\"experience\",\"name\":\"高级体验\",\"active\":false},{\"code\":\"system_tool\",\"name\":\"系统工具\",\"active\":false}],\"roles\":[],\"permissions\":[],\"menus\":[{\"id\":\"1264622039642255311\",\"pid\":\"0\",\"name\":\"system_index\",\"component\":\"RouteView\",\"redirect\":\"/analysis\",\"meta\":{\"title\":\"主控面板\",\"icon\":\"home\",\"show\":true,\"target\":null,\"link\":null},\"path\":\"/\",\"hidden\":false},{\"id\":\"1264622039642255331\",\"pid\":\"1264622039642255311\",\"name\":\"system_index_workplace\",\"component\":\"system/dashboard/Workplace\",\"redirect\":null,\"meta\":{\"title\":\"工作台\",\"icon\":null,\"show\":true,\"target\":null,\"link\":null},\"path\":\"workplace\",\"hidden\":false},{\"id\":\"1264622039642255341\",\"pid\":\"0\",\"name\":\"sys_mgr\",\"component\":\"PageView\",\"redirect\":null,\"meta\":{\"title\":\"组织架构\",\"icon\":\"team\",\"show\":true,\"target\":null,\"link\":null},\"path\":\"/sys\",\"hidden\":false},{\"id\":\"1264622039642255671\",\"pid\":\"0\",\"name\":\"auth_manager\",\"component\":\"PageView\",\"redirect\":null,\"meta\":{\"title\":\"权限管理\",\"icon\":\"safety-certificate\",\"show\":true,\"target\":null,\"link\":null},\"path\":\"/auth\",\"hidden\":false},{\"id\":\"1264622039642255351\",\"pid\":\"1264622039642255341\",\"name\":\"sys_user_mgr\",\"component\":\"system/user/index\",\"redirect\":null,\"meta\":{\"title\":\"用户管理\",\"icon\":null,\"show\":true,\"target\":null,\"link\":null},\"path\":\"/mgr_user\",\"hidden\":false},{\"id\":\"1264622039642255961\",\"pid\":\"0\",\"name\":\"system_tools\",\"component\":\"PageView\",\"redirect\":null,\"meta\":{\"title\":\"开发管理\",\"icon\":\"euro\",\"show\":true,\"target\":null,\"link\":null},\"path\":\"/tools\",\"hidden\":false},{\"id\":\"1264622039642255521\",\"pid\":\"1264622039642255341\",\"name\":\"sys_org_mgr\",\"component\":\"system/org/index\",\"redirect\":null,\"meta\":{\"title\":\"机构管理\",\"icon\":null,\"show\":true,\"target\":null,\"link\":null},\"path\":\"/org\",\"hidden\":false},{\"id\":\"1264622039642256281\",\"pid\":\"0\",\"name\":\"sys_log_mgr\",\"component\":\"PageView\",\"redirect\":null,\"meta\":{\"title\":\"日志管理\",\"icon\":\"read\",\"show\":true,\"target\":null,\"link\":null},\"path\":\"/log\",\"hidden\":false},{\"id\":\"1264622039642255601\",\"pid\":\"1264622039642255341\",\"name\":\"sys_pos_mgr\",\"component\":\"system/pos/index\",\"redirect\":null,\"meta\":{\"title\":\"职位管理\",\"icon\":null,\"show\":true,\"target\":null,\"link\":null},\"path\":\"/pos\",\"hidden\":false},{\"id\":\"1264622039642255681\",\"pid\":\"1264622039642255671\",\"name\":\"sys_app_mgr\",\"component\":\"system/app/index\",\"redirect\":null,\"meta\":{\"title\":\"应用管理\",\"icon\":null,\"show\":true,\"target\":null,\"link\":null},\"path\":\"/app\",\"hidden\":false},{\"id\":\"1264622039642256351\",\"pid\":\"0\",\"name\":\"sys_monitor_mgr\",\"component\":\"PageView\",\"redirect\":null,\"meta\":{\"title\":\"系统监控\",\"icon\":\"deployment-unit\",\"show\":true,\"target\":null,\"link\":null},\"path\":\"/monitor\",\"hidden\":false},{\"id\":\"1264622039642256421\",\"pid\":\"0\",\"name\":\"sys_notice\",\"component\":\"PageView\",\"redirect\":null,\"meta\":{\"title\":\"通知公告\",\"icon\":\"sound\",\"show\":true,\"target\":null,\"link\":null},\"path\":\"/notice\",\"hidden\":false},{\"id\":\"1264622039642255761\",\"pid\":\"1264622039642255671\",\"name\":\"sys_menu_mgr\",\"component\":\"system/menu/index\",\"redirect\":null,\"meta\":{\"title\":\"菜单管理\",\"icon\":null,\"show\":true,\"target\":null,\"link\":null},\"path\":\"/menu\",\"hidden\":false},{\"id\":\"1264622039642256521\",\"pid\":\"0\",\"name\":\"sys_file_mgr\",\"component\":\"PageView\",\"redirect\":null,\"meta\":{\"title\":\"文件管理\",\"icon\":\"file\",\"show\":true,\"target\":null,\"link\":null},\"path\":\"/file\",\"hidden\":false},{\"id\":\"1264622039642255851\",\"pid\":\"1264622039642255671\",\"name\":\"sys_role_mgr\",\"component\":\"system/role/index\",\"redirect\":null,\"meta\":{\"title\":\"角色管理\",\"icon\":null,\"show\":true,\"target\":null,\"link\":null},\"path\":\"/role\",\"hidden\":false},{\"id\":\"1264622039642255971\",\"pid\":\"1264622039642255961\",\"name\":\"system_tools_config\",\"component\":\"system/config/index\",\"redirect\":null,\"meta\":{\"title\":\"系统配置\",\"icon\":null,\"show\":true,\"target\":null,\"link\":null},\"path\":\"/config\",\"hidden\":false},{\"id\":\"1264622039642256041\",\"pid\":\"1264622039642255961\",\"name\":\"sys_email_mgr\",\"component\":\"system/email/index\",\"redirect\":null,\"meta\":{\"title\":\"邮件发送\",\"icon\":null,\"show\":true,\"target\":null,\"link\":null},\"path\":\"/email\",\"hidden\":false},{\"id\":\"1264622039642256071\",\"pid\":\"1264622039642255961\",\"name\":\"sys_sms_mgr\",\"component\":\"system/sms/index\",\"redirect\":null,\"meta\":{\"title\":\"短信管理\",\"icon\":null,\"show\":true,\"target\":null,\"link\":null},\"path\":\"/sms\",\"hidden\":false},{\"id\":\"1264622039642256111\",\"pid\":\"1264622039642255961\",\"name\":\"sys_dict_mgr\",\"component\":\"system/dict/index\",\"redirect\":null,\"meta\":{\"title\":\"字典管理\",\"icon\":null,\"show\":true,\"target\":null,\"link\":null},\"path\":\"/dict\",\"hidden\":false},{\"id\":\"1264622039642256271\",\"pid\":\"1264622039642255961\",\"name\":\"sys_swagger_mgr\",\"component\":\"Iframe\",\"redirect\":null,\"meta\":{\"title\":\"接口文档\",\"icon\":null,\"show\":true,\"target\":null,\"link\":\"https://snowyapi.xiaonuo.vip/doc.html\"},\"path\":\"/swagger\",\"hidden\":false},{\"id\":\"1264622039642256291\",\"pid\":\"1264622039642256281\",\"name\":\"sys_log_mgr_vis_log\",\"component\":\"system/log/vislog/index\",\"redirect\":null,\"meta\":{\"title\":\"访问日志\",\"icon\":null,\"show\":true,\"target\":null,\"link\":null},\"path\":\"/vislog\",\"hidden\":false},{\"id\":\"1264622039642256321\",\"pid\":\"1264622039642256281\",\"name\":\"sys_log_mgr_op_log\",\"component\":\"system/log/oplog/index\",\"redirect\":null,\"meta\":{\"title\":\"操作日志\",\"icon\":null,\"show\":true,\"target\":null,\"link\":null},\"path\":\"/oplog\",\"hidden\":false},{\"id\":\"1264622039642256361\",\"pid\":\"1264622039642256351\",\"name\":\"sys_monitor_mgr_machine_monitor\",\"component\":\"system/machine/index\",\"redirect\":null,\"meta\":{\"title\":\"服务监控\",\"icon\":null,\"show\":true,\"target\":null,\"link\":null},\"path\":\"/machine\",\"hidden\":false},{\"id\":\"1264622039642256381\",\"pid\":\"1264622039642256351\",\"name\":\"sys_monitor_mgr_online_user\",\"component\":\"system/onlineUser/index\",\"redirect\":null,\"meta\":{\"title\":\"在线用户\",\"icon\":null,\"show\":true,\"target\":null,\"link\":null},\"path\":\"/onlineUser\",\"hidden\":false},{\"id\":\"1264622039642256411\",\"pid\":\"1264622039642256351\",\"name\":\"sys_monitor_mgr_druid\",\"component\":\"Iframe\",\"redirect\":null,\"meta\":{\"title\":\"数据监控\",\"icon\":null,\"show\":true,\"target\":null,\"link\":\"https://snowyapi.xiaonuo.vip/druid/login.html\"},\"path\":\"/druid\",\"hidden\":false},{\"id\":\"1264622039642256431\",\"pid\":\"1264622039642256421\",\"name\":\"sys_notice_mgr\",\"component\":\"system/notice/index\",\"redirect\":null,\"meta\":{\"title\":\"公告管理\",\"icon\":null,\"show\":true,\"target\":null,\"link\":null},\"path\":\"/notice\",\"hidden\":false},{\"id\":\"1264622039642256501\",\"pid\":\"1264622039642256421\",\"name\":\"sys_notice_mgr_received\",\"component\":\"system/noticeReceived/index\",\"redirect\":null,\"meta\":{\"title\":\"已收公告\",\"icon\":null,\"show\":true,\"target\":null,\"link\":null},\"path\":\"/noticeReceived\",\"hidden\":false},{\"id\":\"1264622039642256531\",\"pid\":\"1264622039642256521\",\"name\":\"sys_file_mgr_sys_file\",\"component\":\"system/file/index\",\"redirect\":null,\"meta\":{\"title\":\"系统文件\",\"icon\":null,\"show\":true,\"target\":null,\"link\":null},\"path\":\"/file\",\"hidden\":false},{\"id\":\"1264622039642256602\",\"pid\":\"1264622039642256521\",\"name\":\"sys_file_mgr_sys_online_file\",\"component\":\"system/fileOnline/index\",\"redirect\":null,\"meta\":{\"title\":\"在线文档\",\"icon\":null,\"show\":true,\"target\":null,\"link\":null},\"path\":\"/fileOnline\",\"hidden\":false},{\"id\":\"1264622039642256621\",\"pid\":\"1264622039642256611\",\"name\":\"sys_timers_mgr\",\"component\":\"system/timers/index\",\"redirect\":null,\"meta\":{\"title\":\"任务管理\",\"icon\":null,\"show\":true,\"target\":null,\"link\":null},\"path\":\"/timers\",\"hidden\":false},{\"id\":\"1264622039642256712\",\"pid\":\"0\",\"name\":\"sys_area\",\"component\":\"PageView\",\"redirect\":null,\"meta\":{\"title\":\"区域管理\",\"icon\":\"environment\",\"show\":true,\"target\":null,\"link\":null},\"path\":\"/area\",\"hidden\":false},{\"id\":\"1264622039642256713\",\"pid\":\"1264622039642256712\",\"name\":\"sys_area_mgr\",\"component\":\"system/area/index\",\"redirect\":null,\"meta\":{\"title\":\"系统区域\",\"icon\":null,\"show\":true,\"target\":null,\"link\":null},\"path\":\"/area\",\"hidden\":false},{\"id\":\"1264622039642255321\",\"pid\":\"1264622039642255311\",\"name\":\"system_index_dashboard\",\"component\":\"system/dashboard/Analysis\",\"redirect\":null,\"meta\":{\"title\":\"分析页\",\"icon\":null,\"show\":true,\"target\":null,\"link\":null},\"path\":\"analysis\",\"hidden\":false},{\"id\":\"1264622039642256611\",\"pid\":\"0\",\"name\":\"sys_timers\",\"component\":\"PageView\",\"redirect\":null,\"meta\":{\"title\":\"定时任务\",\"icon\":\"dashboard\",\"show\":true,\"target\":null,\"link\":null},\"path\":\"/timers\",\"hidden\":false}],\"dataScopes\":[],\"tenants\":null}}";
    }
}
