
"protoc-25.6-win64/bin/protoc" --proto_path=../Src/Lib/proto --csharp_out=../Src/Lib/Protocol message.proto

@echo off
setlocal

set "SOURCE_DIR=../Src/Lib/Protocol/obj/Debug"
set "TARGET_DIR=../Src/Client/Assets/Plugins"
set "SOURCE_PRO=../Src/Lib/Protocol"

set "FILE1=Protocol.dll"
set "FILE2=Protocol.pdb"

dotnet build "%SOURCE_PRO%"

if not exist "%TARGET_DIR%" (
    echo 目标目录不存在，正在创建...
    mkdir "%TARGET_DIR%"
)

if exist "%TARGET_DIR%\%FILE1%" (
    echo 发现老版本文件 %FILE1%，正在删除...
    del "%TARGET_DIR%\%FILE1%"
)

if exist "%TARGET_DIR%\%FILE2%" (
    echo 发现老版本文件 %FILE2%，正在删除...
    del "%TARGET_DIR%\%FILE2%"
)

echo 正在复制文件到目标目录...
copy "%SOURCE_DIR%\%FILE1%" "%TARGET_DIR%"
copy "%SOURCE_DIR%\%FILE2%" "%TARGET_DIR%"

echo 文件复制完成！
endlocal

@pause