# 迁移数据库到MySQL
## 操作步骤
### 1.在项目Simple.Repository中安装Mysql数据库相关的nuget包 Pomelo.EntityFrameworkCore.MySql 版本选择6.0.1 版本太高会和EF的版本产生冲突，无法使用
### 2.找到项目Simple.Repository的Extensions文件夹下的类：EfCoreServiceCollectionExtensions 中的 AddRepository方法，把options.UseSqlServer(connectionString); 注释掉，换成options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
### 3.在项目Simple.WebApi中找到 appsettings.json 文件，注释掉ConnectionStrings节点下的SqlServer 数据库连接字符串，修改MySql数据库连接字符串为自己要连接的数据库的字符串
### 4.重新生成项目,并保证项目Simple.Repository的Migrations 文件夹下面是空的
### 5. 运行migrations.sh 脚本，根据作者编写的 使用手册.md文档中的"数据库迁移"进行迁移操作即可。