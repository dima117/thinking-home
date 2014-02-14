rd  /s /q ..\dist
rd /s /q ..\msi
nant -buildfile:main.build 
pause