@echo.�������ڰ�װ......  
@echo off  
@cd C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727
@sc create WindowsTimeService binPath= D:\WindowsTimeService\Service.exe 
@echo.����װ�ɹ�����������...... 
@echo off 
@net start WindowsTimeService  
@sc config WindowsTimeService start= AUTO  
@echo off  
@echo.������������  
@pause