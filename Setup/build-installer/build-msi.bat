rd  /s /q ..\dist
rd /s /q ..\msi
nant -buildfile:main.build  -D:build.number=52
pause