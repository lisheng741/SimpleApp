@echo off
setlocal enabledelayedexpansion

REM UTF-8
chcp 65001

REM 恢复 dotnet-ef
dotnet tool restore

REM 进入仓储项目
cd ./Simple.Repository

echo %cd%

if not exist "./Migrations" (
  mkdir ./Migrations
)

set "max=0"

for /r "./Migrations" %%G in (*.cs) do (
    set "filename=%%~nG"
    if "!filename:~14,1!"=="_" (
        set "number=!filename:*_=!"
        if "!filename:Designer=!"=="!filename!" (
            if !number! gtr !max! (
                set "max=!number!"
            )
        )
    )
)

REM 版本+1
set /a "version=!max! + 1"

echo "执行的迁移版本为: %version%"

REM 执行迁移
dotnet ef migrations add %version% -s ../Simple.WebApi

REM 迁移后续操作：0/1-无动作，2-更新数据库，3-删除迁移
echo "迁移完成，请输入数字继续操作"
echo "（0/1-无动作，2-更新数据库，3-删除迁移）"
echo "请输入:"
set /p input=

if defined input (
  if not "%input%"=="0" if not "%input%"=="1" (
    REM 2-更新数据库
    if "%input%"=="2" (
      dotnet ef database update -s ../Simple.WebApi
    )
    REM 3-删除迁移
    if "%input%"=="3" (
      dotnet ef migrations remove -s ../Simple.WebApi
    )
    echo "迁移完成，按下回车关闭窗口……"
    pause >nul
  )
)

endlocal
