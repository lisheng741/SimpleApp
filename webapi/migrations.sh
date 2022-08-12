#!/bin/bash

# 变量设置
settings=" -s ../Simple.WebApi "

# 确保脚本抛出遇到的异常
set -e

# 进入仓储项目
cd "./Simple.Repository"

version=0
files=$(ls "./Migrations")
for file in ${files}
do
    # 迁移文件形如：20220809122112_0.cs
    # 包含下划线（_）且不包含 Designer 的文件名
    if [ ${file:14:1} == "_" ] && !([[ $file =~ "Designer" ]])
    then
        # 截取下划线后面的字符串
        str=${file:15}

        # 匹配数字
        n=`expr ${str} | tr -cd "[0-9]"`

        # n 不为空且大于 version
        if [ -n "$n" ] && (( $n > $version ))
        then
            # 取最大的 version
            version=$n
        fi
    fi
done

# 版本+1
version=$(( $version+1 ))

echo "执行的迁移版本为: $version"

# 执行迁移
dotnet ef migrations add $version $settings

# 读取字符串
echo "迁移完成，按下回车关闭窗口……"
read ending

# # 删除迁移
# dotnet ef migrations remove -s ../Simple.WebApi
# # 更新数据库
# dotnet ef database update
