@echo off
echo ��1���ύ
echo ��������Ķ�message
set/p message=
git commit -m %message%
echo �ύ�ɹ�
echo.

echo ��2����ȡ
git pull
echo ��ȡ�ɹ�
echo.

echo ��3������
git push origin master
echo ���ͳɹ�
echo.

pause