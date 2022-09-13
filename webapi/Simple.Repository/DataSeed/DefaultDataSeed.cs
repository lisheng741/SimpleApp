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
                PositionId =  new Guid("08da7eb0-c4a6-4bd5-81da-9698273a18ea"),
                OrganizationId = new Guid("08da7df0-d44a-4332-8a5c-7921cdc25450"),
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
                IsActive = true
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
            new SysMenu()
            {
                Id = new Guid("08da9540-55c6-477d-8226-88e8d9c2f501"),
                Application = "system",
                Code = "sys_log_mgr_vis_log",
                Component = "system/log/vislog/index",
                Icon = null,
                IsEnabled = true,
                Name = "访问日志",
                OpenType = MenuOpenType.Component,
                ParentId = new Guid("08da9540-3f8d-4a70-8a3c-71a969a0f819"),
                Permission = "",
                Redirect = "",
                Router = "/vislog",
                Sort = 6,
                Type = MenuType.Menu,
                Visible = "Y",
            },
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
