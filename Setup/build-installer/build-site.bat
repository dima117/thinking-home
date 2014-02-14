rd  /s /q ..\dist
rd /s /q ..\msi
nant -buildfile:site.build 
pause