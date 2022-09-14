namespace Simple.Repository.DataSeed;

internal class DefaultDataSeed : IConfigureDataSeed
{
    public void ConfigureDataSeed(ModelBuilder builder)
    {
        // SysOrganization
        builder.Entity<SysOrganization>().HasData(new SysOrganization[]
        {
            new SysOrganization()
            {
                Id = new Guid("08da7df0-d44a-4332-8a5c-7921cdc25450"),
                ParentId = new Guid("00000000-0000-0000-0000-000000000000"),
                Code = "org1",
                Name = "测试公司1",
                Sort = 100,
                IsEnabled = true,
            },
            new SysOrganization()
            {
                Id = new Guid("08da7e1c-95ee-4ba2-8226-ff1d286558bb"),
                ParentId = new Guid("08da7df0-d44a-4332-8a5c-7921cdc25450"),
                Code = "org1-1",
                Name = "测试部门1-1",
                Sort = 100,
                IsEnabled = true,
            },
        });

        // SysPosition
        builder.Entity<SysPosition>().HasData(new SysPosition[]
        {
            new SysPosition()
            {
                Id = new Guid("08da7eb0-c4a6-4bd5-81da-9698273a18ea"),
                Code = "pos1",
                Name = "测试岗位1",
                Sort = 100,
                Remark = "测试岗位1",
                IsEnabled = true,
            },
        });

        // SysUser
        builder.Entity<SysUser>().HasData(new SysUser[]
        {
            new SysUser()
            {
                Id = new Guid("08da8454-bc92-4590-8d28-b1034e400e1e"),
                UserName = "superAdmin",
                Password = "e10adc3949ba59abbe56e057f20f883e",
                Name = "超级管理员",
                Gender = GenderType.Male,
                //PositionId =  new Guid("08da7eb0-c4a6-4bd5-81da-9698273a18ea"),
                //OrganizationId = new Guid("08da7df0-d44a-4332-8a5c-7921cdc25450"),
                IsEnabled = true,
                AdminType = AdminType.SuperAdmin,
            },
        });

        // SysApplication
        builder.Entity<SysApplication>().HasData(new SysApplication[]
        {
            new SysApplication()
            {
                Id = new Guid("08da837b-258f-4948-8a7b-68985ee857d1"),
                Code = "system",
                Name = "系统应用",
                IsEnabled = true,
                IsActive = true,
            }
        });

        // SysMenu
        builder.Entity<SysMenu>().HasData(new SysMenu[]
        {
            new SysMenu()
            {
                Id = new Guid("08da8388-013c-4576-8723-e65736bc54b5"),
                Application = "system",
                Code = "system_index",
                Icon = "home",
                Component = "RouteView",
                IsEnabled = true,
                Name = "主控面板",
                OpenType = MenuOpenType.None,
                ParentId = new Guid("00000000-0000-0000-0000-000000000000"),
                Permission = "",
                Redirect = "/analysis",
                Router = "/",
                Sort = 1,
                Type = MenuType.Directory,
                Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da8388-023c-4576-875b-24e078c437f6"),
                Application = "system",
                Code = "system_index_workplace",
                Component = "system/dashboard/Workplace",
                Icon = null,
                IsEnabled = true,
                Name = "工作台",
                OpenType = MenuOpenType.Component,
                ParentId = new Guid("08da8388-013c-4576-8723-e65736bc54b5"),
                Permission = "",
                Redirect = "",
                Router = "workplace",
                Sort = 2,
                Type = MenuType.Menu,
                Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da8388-033c-4576-8d50-acdc2bf0b298"),
                Application = "system",
                Code = "system_index_dashboard",
                Component = "system/dashboard/Analysis",
                Icon = null,
                IsEnabled = true,
                Name = "分析页",
                OpenType = MenuOpenType.Component,
                ParentId = new Guid("08da8388-013c-4576-8723-e65736bc54b5"),
                Permission = "",
                Redirect = "",
                Router = "analysis",
                Sort = 3,
                Type = MenuType.Menu,
                Visible = "Y",
            },

            new SysMenu()
            {
                Id = new Guid("08da8388-113c-4576-8b87-7408b2fcafcd"),
                Application = "system",
                Code = "sys_mgr",
                Icon = "team",
                Component = "PageView",
                IsEnabled = true,
                Name = "组织架构",
                OpenType = MenuOpenType.None,
                ParentId = new Guid("00000000-0000-0000-0000-000000000000"),
                Permission = "",
                Redirect = "",
                Router = "/sys",
                Sort = 2,
                Type = MenuType.Directory,
                Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da838e-a68d-4cf3-85eb-2dc3613cb532"),
                Application = "system",
                Code = "sys_org_mgr",
                Component = "system/org/index",
                Icon = null,
                IsEnabled = true,
                Name = "机构管理",
                OpenType = MenuOpenType.Component,
                ParentId = new Guid("08da8388-113c-4576-8b87-7408b2fcafcd"),
                Permission = "",
                Redirect = "",
                Router = "/org",
                Sort = 3,
                Type = MenuType.Menu,
                Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da9600-fc88-4c43-8481-dd6f8b423bd0"),
                Code = "sys_org_mgr_page", Name = "机构查询", Permission = "sysorg:page",
                ParentId = new Guid("08da838e-a68d-4cf3-85eb-2dc3613cb532"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da9600-fc8d-400c-8afb-40ce0cdacc97"),
                Code = "sys_org_mgr_list", Name = "机构列表", Permission = "sysorg:list",
                ParentId = new Guid("08da838e-a68d-4cf3-85eb-2dc3613cb532"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da9601-6d25-4eb8-8767-6cb2bec3a8e7"),
                Code = "sys_org_mgr_add", Name = "机构增加", Permission = "sysorg:add",
                ParentId = new Guid("08da838e-a68d-4cf3-85eb-2dc3613cb532"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da9601-6d27-446c-8296-4350a9994a4b"),
                Code = "sys_org_mgr_edit", Name = "机构编辑", Permission = "sysorg:edit",
                ParentId = new Guid("08da838e-a68d-4cf3-85eb-2dc3613cb532"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da9601-6d27-459e-8adc-8a219846bf35"),
                Code = "sys_org_mgr_delete", Name = "机构删除", Permission = "sysorg:delete",
                ParentId = new Guid("08da838e-a68d-4cf3-85eb-2dc3613cb532"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            //new SysMenu()
            //{
            //    Id = new Guid("08da9601-6d27-45b1-8ffe-aad2d27c0d51"),
            //    Code = "sys_org_mgr_detail", Name = "机构详情", Permission = "sysorg:detail",
            //    ParentId = new Guid("08da838e-a68d-4cf3-85eb-2dc3613cb532"),
            //    Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            //},
            new SysMenu()
            {
                Id = new Guid("08da9601-6d27-45bf-8b95-41e1d0077df2"),
                Code = "sys_org_mgr_tree", Name = "机构树", Permission = "sysorg:tree",
                ParentId = new Guid("08da838e-a68d-4cf3-85eb-2dc3613cb532"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da8413-3c48-4772-85e1-92d4c8b86c6c"),
                Application = "system",
                Code = "sys_pos_mgr",
                Component = "system/pos/index",
                Icon = null,
                IsEnabled = true,
                Name = "岗位管理",
                OpenType = MenuOpenType.Component,
                ParentId = new Guid("08da8388-113c-4576-8b87-7408b2fcafcd"),
                Permission = "",
                Redirect = "",
                Router = "/pos",
                Sort = 4,
                Type = MenuType.Menu,
                Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da9602-4fb2-4ca1-863e-2047136f6464"),
                Code = "sys_pos_mgr_page", Name = "职位查询", Permission = "syspos:page",
                ParentId = new Guid("08da8413-3c48-4772-85e1-92d4c8b86c6c"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da9602-4fb2-4d14-8b9b-01073ff06205"),
                Code = "sys_pos_mgr_list", Name = "职位列表", Permission = "syspos:list",
                ParentId = new Guid("08da8413-3c48-4772-85e1-92d4c8b86c6c"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da9602-4fb2-4e71-81de-3fa44768da1e"),
                Name = "职位增加", Code = "sys_pos_mgr_add", Permission = "syspos:add",
                ParentId = new Guid("08da8413-3c48-4772-85e1-92d4c8b86c6c"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da9602-4fb2-4e88-8b09-cbe17e117902"),
                Name = "职位编辑", Code = "sys_pos_mgr_edit", Permission = "syspos:edit",
                ParentId = new Guid("08da8413-3c48-4772-85e1-92d4c8b86c6c"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da9602-4fb2-4e99-86ca-b0f087fe1aae"),
                Name = "职位删除", Code = "sys_pos_mgr_delete", Permission = "syspos:delete",
                ParentId = new Guid("08da8413-3c48-4772-85e1-92d4c8b86c6c"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da8413-552c-48e7-8a8b-accf58bca8c6"),
                Application = "system",
                Code = "sys_user_mgr",
                Component = "system/user/index",
                Icon = null,
                IsEnabled = true,
                Name = "用户管理",
                OpenType = MenuOpenType.Component,
                ParentId = new Guid("08da8388-113c-4576-8b87-7408b2fcafcd"),
                Permission = "",
                Redirect = "",
                Router = "/mgr_user",
                Sort = 5,
                Type = MenuType.Menu,
                Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da9602-f163-4f10-8995-56c8475ca584"),
                Name = "用户查询", Code = "sys_user_mgr_page", Permission = "sysuser:page",
                ParentId = new Guid("08da8413-552c-48e7-8a8b-accf58bca8c6"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da9602-f163-4f8f-8e94-cdbb886a685b"),
                Name = "用户列表", Code = "sys_user_mgr_list", Permission = "sysuser:list",
                ParentId = new Guid("08da8413-552c-48e7-8a8b-accf58bca8c6"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da9602-f164-4463-8112-1e470a746c82"),
                Name = "用户增加", Code = "sys_user_mgr_add", Permission = "sysuser:add",
                ParentId = new Guid("08da8413-552c-48e7-8a8b-accf58bca8c6"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da9602-f164-4f06-88b9-28ffd82446eb"),
                Name = "用户编辑", Code = "sys_user_mgr_edit", Permission = "sysuser:edit",
                ParentId = new Guid("08da8413-552c-48e7-8a8b-accf58bca8c6"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da9602-f165-453d-8da8-c4bf86e62297"),
                Name = "用户删除", Code = "sys_user_mgr_delete", Permission = "sysuser:delete",
                ParentId = new Guid("08da8413-552c-48e7-8a8b-accf58bca8c6"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da9606-4316-486f-8b35-e4ff2683847d"),
                Name = "用户修改状态", Code = "sys_user_mgr_change_status", Permission = "sysuser:changestatus",
                ParentId = new Guid("08da8413-552c-48e7-8a8b-accf58bca8c6"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            //new SysMenu()
            //{
            //    Id = new Guid("08da9606-4316-497e-8b51-b439fdc75371"),
            //    Name = "用户修改密码", Code = "sys_user_mgr_update_pwd", Permission = "sysuser:updatepwd",
            //    ParentId = new Guid("08da8413-552c-48e7-8a8b-accf58bca8c6"),
            //    Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            //},
            new SysMenu()
            {
                Id = new Guid("08da9606-4316-4b52-82ca-dcdce86adba3"),
                Name = "用户拥有角色", Code = "sys_user_mgr_own_role", Permission = "sysuser:ownrole",
                ParentId = new Guid("08da8413-552c-48e7-8a8b-accf58bca8c6"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da9606-4316-4b74-8f29-252231eaf689"),
                Name = "用户授权角色", Code = "sys_user_mgr_grant_role", Permission = "sysuser:grantrole",
                ParentId = new Guid("08da8413-552c-48e7-8a8b-accf58bca8c6"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da9606-4316-4b9c-80b5-559df317b84d"),
                Name = "用户拥有数据", Code = "sys_user_mgr_own_data", Permission = "sysuser:owndata",
                ParentId = new Guid("08da8413-552c-48e7-8a8b-accf58bca8c6"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da9607-5902-472c-8f77-fdcc0a720051"),
                Name = "用户授权数据", Code = "sys_user_mgr_grant_data", Permission = "sysuser:grantdata",
                ParentId = new Guid("08da8413-552c-48e7-8a8b-accf58bca8c6"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da9607-5902-479d-84b4-850ae7c495ed"),
                Name = "用户重置密码", Code = "sys_user_mgr_reset_pwd", Permission = "sysuser:resetpwd",
                ParentId = new Guid("08da8413-552c-48e7-8a8b-accf58bca8c6"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },

            new SysMenu()
            {
                Id = new Guid("08da8413-8f23-4b96-8704-db715135e646"),
                Application = "system",
                Code = "auth_manager",
                Icon = "safety-certificate",
                Component = "PageView",
                IsEnabled = true,
                Name = "权限管理",
                OpenType = MenuOpenType.None,
                ParentId = new Guid("00000000-0000-0000-0000-000000000000"),
                Permission = "",
                Redirect = "",
                Router = "/auth",
                Sort = 3,
                Type = MenuType.Directory,
                Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da8413-ad1f-4e73-87b2-a17113b1a85a"),
                Application = "system",
                Code = "sys_app_mgr",
                Component = "system/app/index",
                Icon = null,
                IsEnabled = true,
                Name = "应用管理",
                OpenType = MenuOpenType.Component,
                ParentId = new Guid("08da8413-8f23-4b96-8704-db715135e646"),
                Permission = "",
                Redirect = "",
                Router = "/app",
                Sort = 4,
                Type = MenuType.Menu,
                Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da9607-5902-4a74-8d10-a87e9fa1983a"),
                Name = "设为默认应用", Code = "sys_app_mgr_set_as_default", Permission = "sysapp:setasdefault",
                ParentId = new Guid("08da8413-ad1f-4e73-87b2-a17113b1a85a"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960a-62bd-4f1f-893e-3333d4d328d9"),
                Name = "应用查询", Code = "sys_app_mgr_page", Permission = "sysapp:page",
                ParentId = new Guid("08da8413-ad1f-4e73-87b2-a17113b1a85a"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960a-62bd-4fc7-8403-b11094ba1c3e"),
                Name = "应用列表", Code = "sys_app_mgr_list", Permission = "sysapp:list",
                ParentId = new Guid("08da8413-ad1f-4e73-87b2-a17113b1a85a"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960a-62be-49f7-888b-76bfa12660ba"),
                Name = "应用增加", Code = "sys_app_mgr_add", Permission = "sysapp:add",
                ParentId = new Guid("08da8413-ad1f-4e73-87b2-a17113b1a85a"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960a-62be-4cff-8b05-8017c2d2d657"),
                Name = "应用编辑", Code = "sys_app_mgr_edit", Permission = "sysapp:edit",
                ParentId = new Guid("08da8413-ad1f-4e73-87b2-a17113b1a85a"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960a-62bf-4cff-8b05-8017c2d2d658"),
                Name = "应用删除", Code = "sys_app_mgr_delete", Permission = "sysapp:delete",
                ParentId = new Guid("08da8413-ad1f-4e73-87b2-a17113b1a85a"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da8413-d0aa-4ee1-8e70-999ee1c005e8"),
                Application = "system",
                Code = "sys_menu_mgr",
                Component = "system/menu/index",
                Icon = null,
                IsEnabled = true,
                Name = "菜单管理",
                OpenType = MenuOpenType.Component,
                ParentId = new Guid("08da8413-8f23-4b96-8704-db715135e646"),
                Permission = "",
                Redirect = "",
                Router = "/menu",
                Sort = 5,
                Type = MenuType.Menu,
                Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960a-f614-4a2f-8b56-cb68377ec651"),
                Name = "菜单查询", Code = "sys_menu_mgr_page", Permission = "sysmenu:page",
                ParentId = new Guid("08da8413-d0aa-4ee1-8e70-999ee1c005e8"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960a-f614-4aa7-8592-7e85974a52ef"),
                Name = "菜单列表", Code = "sys_menu_mgr_list", Permission = "sysmenu:list",
                ParentId = new Guid("08da8413-d0aa-4ee1-8e70-999ee1c005e8"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960a-f615-427e-8b58-3e35b4e12288"),
                Name = "菜单增加", Code = "sys_menu_mgr_add", Permission = "sysmenu:add",
                ParentId = new Guid("08da8413-d0aa-4ee1-8e70-999ee1c005e8"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960a-f615-42bc-8af9-246832a503a5"),
                Name = "菜单编辑", Code = "sys_menu_mgr_edit", Permission = "sysmenu:edit",
                ParentId = new Guid("08da8413-d0aa-4ee1-8e70-999ee1c005e8"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960a-f615-4388-8d27-8f231318f4c0"),
                Name = "菜单删除", Code = "sys_menu_mgr_delete", Permission = "sysmenu:delete",
                ParentId = new Guid("08da8413-d0aa-4ee1-8e70-999ee1c005e8"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960b-9371-45c0-88ac-69917bf4f379"),
                Name = "菜单树", Code = "sys_menu_mgr_tree", Permission = "sysmenu:tree",
                ParentId = new Guid("08da8413-d0aa-4ee1-8e70-999ee1c005e8"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960b-9371-463b-847b-f54670b3ad70"),
                Name = "菜单授权树", Code = "sys_menu_mgr_grant_tree", Permission = "sysmenu:treeforgrant",
                ParentId = new Guid("08da8413-d0aa-4ee1-8e70-999ee1c005e8"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da8413-e52e-4503-8dbd-c12317f070ba"),
                Application = "system",
                Code = "sys_role_mgr",
                Component = "system/role/index",
                Icon = null,
                IsEnabled = true,
                Name = "角色管理",
                OpenType = MenuOpenType.Component,
                ParentId = new Guid("08da8413-8f23-4b96-8704-db715135e646"),
                Permission = "",
                Redirect = "",
                Router = "/role",
                Sort = 6,
                Type = MenuType.Menu,
                Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960b-9371-4b49-8d22-8948da3c9e18"),
                Name = "角色查询", Code = "sys_role_mgr_page", Permission = "sysrole:page",
                ParentId = new Guid("08da8413-e52e-4503-8dbd-c12317f070ba"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960b-9372-40a0-8a43-7d015cd1a1bc"),
                Name = "角色列表", Code = "sys_role_mgr_list", Permission = "sysrole:list",
                ParentId = new Guid("08da8413-e52e-4503-8dbd-c12317f070ba"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960b-9372-41be-8a2c-10e8f33d3080"),
                Name = "角色增加", Code = "sys_role_mgr_add", Permission = "sysrole:add",
                ParentId = new Guid("08da8413-e52e-4503-8dbd-c12317f070ba"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960c-2e88-4cb5-8bb0-887557d003c7"),
                Name = "角色编辑", Code = "sys_role_mgr_edit", Permission = "sysrole:edit",
                ParentId = new Guid("08da8413-e52e-4503-8dbd-c12317f070ba"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960c-2e88-4d48-8325-a80a9f5cdd6e"),
                Name = "角色删除", Code = "sys_role_mgr_delete", Permission = "sysrole:delete",
                ParentId = new Guid("08da8413-e52e-4503-8dbd-c12317f070ba"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960c-2e89-454a-86bf-e8c717250585"),
                Name = "角色拥有菜单", Code = "sys_role_mgr_own_menu", Permission = "sysrole:ownmenu",
                ParentId = new Guid("08da8413-e52e-4503-8dbd-c12317f070ba"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960c-2e89-493e-849a-083fc4fda13e"),
                Name = "角色授权菜单", Code = "sys_role_mgr_grant_menu", Permission = "sysrole:grantmenu",
                ParentId = new Guid("08da8413-e52e-4503-8dbd-c12317f070ba"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960c-2e89-496d-816b-0d2e0e37fae6"),
                Name = "角色拥有数据", Code = "sys_role_mgr_own_data", Permission = "sysrole:owndata",
                ParentId = new Guid("08da8413-e52e-4503-8dbd-c12317f070ba"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960c-9454-494a-8d07-ed1070449cdf"),
                Name = "角色授权数据", Code = "sys_role_mgr_grant_data", Permission = "sysrole:grantdata",
                ParentId = new Guid("08da8413-e52e-4503-8dbd-c12317f070ba"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },

            new SysMenu()
            {
                Id = new Guid("08da8414-4deb-4f62-83b6-e2c3bcd4c311"),
                Application = "system",
                Code = "system_tools",
                Icon = "euro",
                Component = "PageView",
                IsEnabled = true,
                Name = "开发管理",
                OpenType = MenuOpenType.None,
                ParentId = new Guid("00000000-0000-0000-0000-000000000000"),
                Permission = "",
                Redirect = "",
                Router = "/tools",
                Sort = 4,
                Type = MenuType.Directory,
                Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da8414-6676-4f0a-8eb4-2bc4c51e07f0"),
                Application = "system",
                Code = "sys_dict_mgr",
                Component = "system/dict/index",
                Icon = null,
                IsEnabled = true,
                Name = "字典管理",
                OpenType = MenuOpenType.Component,
                ParentId = new Guid("08da8414-4deb-4f62-83b6-e2c3bcd4c311"),
                Permission = "",
                Redirect = "",
                Router = "/dict",
                Sort = 5,
                Type = MenuType.Menu,
                Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960c-f4a9-4a80-8bb2-6238a7c78927"),
                Name = "字典类型查询", Code = "sys_dict_mgr_dict_type_page", Permission = "sysdicttype:page",
                ParentId = new Guid("08da8414-6676-4f0a-8eb4-2bc4c51e07f0"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960c-f4ab-42dc-8f5a-0e768de99bb2"),
                Name = "字典类型列表", Code = "sys_dict_mgr_dict_type_list", Permission = "sysdicttype:list",
                ParentId = new Guid("08da8414-6676-4f0a-8eb4-2bc4c51e07f0"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960c-f4ab-446d-89e7-26abfd7c27a9"),
                Name = "字典类型增加", Code = "sys_dict_mgr_dict_type_add", Permission = "sysdicttype:add",
                ParentId = new Guid("08da8414-6676-4f0a-8eb4-2bc4c51e07f0"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960c-f4ab-4485-854a-7bffea6ccf87"),
                Name = "字典类型编辑", Code = "sys_dict_mgr_dict_type_edit", Permission = "sysdicttype:edit",
                ParentId = new Guid("08da8414-6676-4f0a-8eb4-2bc4c51e07f0"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960c-f4ab-4496-8ded-b0fbb4838be6"),
                Name = "字典类型删除", Code = "sys_dict_mgr_dict_type_delete", Permission = "sysdicttype:delete",
                ParentId = new Guid("08da8414-6676-4f0a-8eb4-2bc4c51e07f0"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960d-b000-4643-8f2b-e5b61b962147"),
                Name = "字典值查询", Code = "sys_dict_mgr_dict_page", Permission = "sysdictdata:page",
                ParentId = new Guid("08da8414-6676-4f0a-8eb4-2bc4c51e07f0"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960d-b001-450f-840b-f8de994c995f"),
                Name = "字典值列表", Code = "sys_dict_mgr_dict_list", Permission = "sysdictdata:list",
                ParentId = new Guid("08da8414-6676-4f0a-8eb4-2bc4c51e07f0"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960d-b001-4697-8538-3e843c5639ee"),
                Name = "字典值增加", Code = "sys_dict_mgr_dict_add", Permission = "sysdictdata:add",
                ParentId = new Guid("08da8414-6676-4f0a-8eb4-2bc4c51e07f0"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960d-b001-46bd-8078-ce074b400f85"),
                Name = "字典值编辑", Code = "sys_dict_mgr_dict_edit", Permission = "sysdictdata:edit",
                ParentId = new Guid("08da8414-6676-4f0a-8eb4-2bc4c51e07f0"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960d-b001-46d1-8211-46a3a060a9f3"),
                Name = "字典值删除", Code = "sys_dict_mgr_dict_delete", Permission = "sysdictdata:delete",
                ParentId = new Guid("08da8414-6676-4f0a-8eb4-2bc4c51e07f0"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },

            new SysMenu()
            {
                Id = new Guid("08da9540-3f8d-4a70-8a3c-71a969a0f819"),
                Application = "system",
                Code = "sys_log_mgr",
                Icon = "read",
                Component = "PageView",
                IsEnabled = true,
                Name = "日志管理",
                OpenType = MenuOpenType.None,
                ParentId = new Guid("00000000-0000-0000-0000-000000000000"),
                Permission = "",
                Redirect = "",
                Router = "/log",
                Sort = 5,
                Type = MenuType.Directory,
                Visible = "Y",
            },
            //new SysMenu()
            //{
            //    Id = new Guid("08da9540-55c6-477d-8226-88e8d9c2f501"),
            //    Application = "system",
            //    Code = "sys_log_mgr_vis_log",
            //    Component = "system/log/vislog/index",
            //    Icon = null,
            //    IsEnabled = true,
            //    Name = "访问日志",
            //    OpenType = MenuOpenType.Component,
            //    ParentId = new Guid("08da9540-3f8d-4a70-8a3c-71a969a0f819"),
            //    Permission = "",
            //    Redirect = "",
            //    Router = "/vislog",
            //    Sort = 6,
            //    Type = MenuType.Menu,
            //    Visible = "Y",
            //},
            new SysMenu()
            {
                Id = new Guid("08da9540-6a12-4af7-85b2-4e47577b1adb"),
                Application = "system",
                Code = "sys_log_mgr_op_log",
                Component = "system/log/oplog/index",
                Icon = null,
                IsEnabled = true,
                Name = "操作日志",
                OpenType = MenuOpenType.Component,
                ParentId = new Guid("08da9540-3f8d-4a70-8a3c-71a969a0f819"),
                Permission = "",
                Redirect = "",
                Router = "/oplog",
                Sort = 7,
                Type = MenuType.Menu,
                Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960e-40d7-4c88-857a-1acd8cbae17a"),
                Name = "操作日志查询", Code = "sys_log_mgr_op_log_page", Permission = "sysoplog:page",
                ParentId = new Guid("08da9540-6a12-4af7-85b2-4e47577b1adb"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da9540-a7cd-4b8c-8a25-222202d4adcf"),
                Application = "system",
                Code = "sys_log_mgr_ex_log",
                Component = "system/log/exlog/index",
                Icon = null,
                IsEnabled = true,
                Name = "异常日志",
                OpenType = MenuOpenType.Component,
                ParentId = new Guid("08da9540-3f8d-4a70-8a3c-71a969a0f819"),
                Permission = "",
                Redirect = "",
                Router = "/exlog",
                Sort = 8,
                Type = MenuType.Menu,
                Visible = "Y",
            },
            new SysMenu()
            {
                Id = new Guid("08da960e-40d7-4d0b-8ef2-4206c7296d96"),
                Name = "异常日志查询", Code = "sys_log_mgr_ex_log_page", Permission = "sysexlog:page",
                ParentId = new Guid("08da9540-a7cd-4b8c-8a25-222202d4adcf"),
                Type = MenuType.Button, Application = "system", Component = "", Icon = null, OpenType = MenuOpenType.None,IsEnabled = true, Redirect = "", Router = "", Sort = 100, Visible = "Y",
            },
        });

        // SysRole
        builder.Entity<SysRole>().HasData(new SysRole[]
        {
            new SysRole()
            {
                Id = new Guid("08da7f2c-abf5-4090-8788-0a62e419030e"),
                Code = "admin",
                Name = "管理员",
                IsEnabled = true,
            },
        });

        // SysDictionary
        builder.Entity<SysDictionary>().HasData(new SysDictionary[]
        {
            new SysDictionary()
            {
                Id = new Guid("08da7f55-9dd1-4d8c-8eff-6398d2b2cfee"),
                Code = "gender",
                Name = "性别",
                Sort = 100,
                Remark = "1-男，2-女",
                IsEnabled = true,
            },
            new SysDictionary()
            {
                Id = new Guid("08da7f8e-076f-4fc6-8ef7-f776c63bdb87"),
                Code = "common_status",
                Name = "通用状态",
                Sort = 100,
                Remark = "1-启用，0-禁用",
                IsEnabled = true,
            },
            new SysDictionary()
            {
                Id = new Guid("08da837b-d2e5-4456-8e8c-9f65ac9669e0"),
                Code = "yes_or_no",
                Name = "是否",
                Sort = 100,
                Remark = "Y-是，N-否",
                IsEnabled = true,
            },
            new SysDictionary()
            {
                Id = new Guid("08da837c-3a8c-43e6-802a-148c713b642b"),
                Code = "menu_weight",
                Name = "菜单权重",
                Sort = 100,
                Remark = "菜单权重",
                IsEnabled = true,
            },
            new SysDictionary()
            {
                Id = new Guid("08da837c-4250-4b2f-8c9e-440af0dc8441"),
                Code = "menu_type",
                Name = "菜单类型",
                Sort = 100,
                Remark = "菜单类型",
                IsEnabled = true,
            },
            new SysDictionary()
            {
                Id = new Guid("08da837c-854e-471a-8923-47cef82ea251"),
                Code = "open_type",
                Name = "打开方式",
                Sort = 100,
                Remark = "打开方式",
                IsEnabled = true,
            },
            new SysDictionary()
            {
                Id = new Guid("08da873d-a3d6-4cdc-8623-ec70bc54bd3b"),
                Code = "data_scope_type",
                Name = "数据范围类型",
                Sort = 100,
                Remark = "数据范围类型",
                IsEnabled = true,
            },
        });

        // SysDictionaryItem
        builder.Entity<SysDictionaryItem>().HasData(new SysDictionaryItem[]
        {
            new SysDictionaryItem()
            {
                Id = new Guid("08da7f7b-4b68-408b-8ead-32709a2d2e99"),
                DictionaryId = new Guid("08da7f55-9dd1-4d8c-8eff-6398d2b2cfee"),
                Code = "1",
                Name = "男",
                Sort = 100,
                Remark = "",
                IsEnabled = true,
            },
            new SysDictionaryItem()
            {
                Id = new Guid("08da7f8c-0172-4d3f-8d35-3970755675d0"),
                DictionaryId = new Guid("08da7f55-9dd1-4d8c-8eff-6398d2b2cfee"),
                Code = "2",
                Name = "女",
                Sort = 100,
                Remark = "",
                IsEnabled = true,
            },

            new SysDictionaryItem()
            {
                Id = new Guid("08da7f8f-0c7d-4f20-8b12-1f08d59265d8"),
                DictionaryId = new Guid("08da7f8e-076f-4fc6-8ef7-f776c63bdb87"),
                Code = "1",
                Name = "启用",
                Sort = 100,
                Remark = "",
                IsEnabled = true,
            },
            new SysDictionaryItem()
            {
                Id = new Guid("08da7f8f-1287-4676-8e0f-353356645345"),
                DictionaryId = new Guid("08da7f8e-076f-4fc6-8ef7-f776c63bdb87"),
                Code = "0",
                Name = "禁用",
                Sort = 100,
                Remark = "",
                IsEnabled = true,
            },

            new SysDictionaryItem()
            {
                Id = new Guid("08da837b-d966-4068-8729-633d3a67e3cf"),
                DictionaryId = new Guid("08da837b-d2e5-4456-8e8c-9f65ac9669e0"),
                Code = "Y",
                Name = "是",
                Sort = 100,
                Remark = "",
                IsEnabled = true,
            },
            new SysDictionaryItem()
            {
                Id = new Guid("08da837b-ddc6-41f5-84b0-ad06da753448"),
                DictionaryId = new Guid("08da837b-d2e5-4456-8e8c-9f65ac9669e0"),
                Code = "N",
                Name = "否",
                Sort = 100,
                Remark = "",
                IsEnabled = true,
            },

            new SysDictionaryItem()
            {
                Id = new Guid("08da837c-53c8-420e-8ba7-698d83fe7657"),
                DictionaryId = new Guid("08da837c-3a8c-43e6-802a-148c713b642b"),
                Code = "1",
                Name = "系统权重",
                Sort = 100,
                Remark = "",
                IsEnabled = true,
            },
            new SysDictionaryItem()
            {
                Id = new Guid("08da837c-5a65-4cfa-8b2c-062e35e51454"),
                DictionaryId = new Guid("08da837c-3a8c-43e6-802a-148c713b642b"),
                Code = "2",
                Name = "业务权重",
                Sort = 100,
                Remark = "",
                IsEnabled = true,
            },

            new SysDictionaryItem()
            {
                Id = new Guid("08da837c-69fd-415c-814f-71101cd72799"),
                DictionaryId = new Guid("08da837c-4250-4b2f-8c9e-440af0dc8441"),
                Code = "1",
                Name = "菜单",
                Sort = 100,
                Remark = "",
                IsEnabled = true,
            },
            new SysDictionaryItem()
            {
                Id = new Guid("08da837c-6e87-4731-8593-2edb59502c44"),
                DictionaryId = new Guid("08da837c-4250-4b2f-8c9e-440af0dc8441"),
                Code = "0",
                Name = "目录",
                Sort = 100,
                Remark = "",
                IsEnabled = true,
            },
            new SysDictionaryItem()
            {
                Id = new Guid("08da837c-734e-4d2c-88ca-c18b0a044ad0"),
                DictionaryId = new Guid("08da837c-4250-4b2f-8c9e-440af0dc8441"),
                Code = "2",
                Name = "按钮",
                Sort = 100,
                Remark = "",
                IsEnabled = true,
            },

            new SysDictionaryItem()
            {
                Id = new Guid("08da837c-904e-42b5-80f2-4f73fd06cbbd"),
                DictionaryId = new Guid("08da837c-854e-471a-8923-47cef82ea251"),
                Code = "2",
                Name = "内链",
                Sort = 100,
                Remark = "",
                IsEnabled = true,
            },
            new SysDictionaryItem()
            {
                Id = new Guid("08da837c-954b-4aef-8c2e-42d17a6e7df9"),
                DictionaryId = new Guid("08da837c-854e-471a-8923-47cef82ea251"),
                Code = "3",
                Name = "外链",
                Sort = 100,
                Remark = "",
                IsEnabled = true,
            },
            new SysDictionaryItem()
            {
                Id = new Guid("08da837c-99ff-493e-8126-102b5c4a6b72"),
                DictionaryId = new Guid("08da837c-854e-471a-8923-47cef82ea251"),
                Code = "1",
                Name = "组件",
                Sort = 100,
                Remark = "",
                IsEnabled = true,
            },
            new SysDictionaryItem()
            {
                Id = new Guid("08da837c-9da8-4c77-8c63-b5d82adbbffb"),
                DictionaryId = new Guid("08da837c-854e-471a-8923-47cef82ea251"),
                Code = "0",
                Name = "无",
                Sort = 100,
                Remark = "",
                IsEnabled = true,
            },

            new SysDictionaryItem()
            {
                Id = new Guid("08da873d-b07e-4d79-8afd-2dda57180791"),
                DictionaryId = new Guid("08da873d-a3d6-4cdc-8623-ec70bc54bd3b"),
                Code = "1",
                Name = "全部数据",
                Sort = 100,
                Remark = "",
                IsEnabled = true,
            },
            new SysDictionaryItem()
            {
                Id = new Guid("08da873d-b7e0-49dc-8b24-c62a89e8ec35"),
                DictionaryId = new Guid("08da873d-a3d6-4cdc-8623-ec70bc54bd3b"),
                Code = "2",
                Name = "本部门及以下数据",
                Sort = 100,
                Remark = "",
                IsEnabled = true,
            },
            new SysDictionaryItem()
            {
                Id = new Guid("08da873d-bd6d-497a-84d4-f3c108f79401"),
                DictionaryId = new Guid("08da873d-a3d6-4cdc-8623-ec70bc54bd3b"),
                Code = "3",
                Name = "本部门数据",
                Sort = 100,
                Remark = "",
                IsEnabled = true,
            },
            new SysDictionaryItem()
            {
                Id = new Guid("08da873d-c267-4fd7-8524-fc325d9589e3"),
                DictionaryId = new Guid("08da873d-a3d6-4cdc-8623-ec70bc54bd3b"),
                Code = "4",
                Name = "仅本人数据",
                Sort = 100,
                Remark = "",
                IsEnabled = true,
            },
            new SysDictionaryItem()
            {
                Id = new Guid("08da873d-c68f-4198-86ff-24a0748a76eb"),
                DictionaryId = new Guid("08da873d-a3d6-4cdc-8623-ec70bc54bd3b"),
                Code = "5",
                Name = "自定义数据",
                Sort = 100,
                Remark = "",
                IsEnabled = true,
            },
        });
    }
}
