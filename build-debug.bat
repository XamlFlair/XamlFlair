@echo off
REM *** Make sure to delete bin/obj folders
REM *** Make sure to rebuild using the 'debug' configuration for every platform (Any CPU, ARM, x64, x86)

REM --- RESTORE & REBUILD ---
nuget restore "XamlFlair.sln"

SET BUILD="C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe"
%BUILD% "XamlFlair.sln" /p:Configuration=Debug /p:Platform="Any CPU"
%BUILD% "XamlFlair.sln" /p:Configuration=Debug /p:Platform=ARM
%BUILD% "XamlFlair.sln" /p:Configuration=Debug /p:Platform=x64
%BUILD% "XamlFlair.sln" /p:Configuration=Debug /p:Platform=x86
REM -------------------------