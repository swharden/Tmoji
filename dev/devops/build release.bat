cd ..\..\src\Tmoji
RMDIR /S /Q bin
dotnet build --configuration Release
cd bin\Release
rename net48 Tmoji
tar.exe -a -c -f Tmoji.zip Tmoji
start explorer.exe .\
pause