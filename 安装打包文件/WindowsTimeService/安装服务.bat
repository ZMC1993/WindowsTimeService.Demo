@echo.服务正在安装......  
@echo off  
@cd C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727
@sc create WindowsTimeService binPath= D:\WindowsTimeService\Service.exe 
@echo.服务安装成功，正在启动...... 
@echo off 
@net start WindowsTimeService  
@sc config WindowsTimeService start= AUTO  
@echo off  
@echo.服务已启动！  
@pause