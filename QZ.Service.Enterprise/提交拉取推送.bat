@echo off
echo （1）提交
echo 请输入你改动message
set/p message=
git commit -m %message%
echo 提交成功
echo.

echo （2）拉取
git pull
echo 拉取成功
echo.

echo （3）推送
git push origin master
echo 推送成功
echo.

pause