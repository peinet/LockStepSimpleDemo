@echo off
for /r %%i in (./*.proto) do ( 
echo %%~ni
protoc --proto_path=./ %%~ni.proto --csharp_out=../
)
pause