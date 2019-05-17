@echo off
REM *** Make sure to delete bin/obj folders
REM *** Make sure to rebuild using the 'release' configuration for every platform (Any CPU, ARM, x64, x86) 
REM *** 'Samples' projects have been omitted from building and deploying in 'release'

REM --- RESTORE & REBUILD ---
nuget restore "XamlFlair.sln"

SET BUILD="C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\MSBuild.exe"
%BUILD% "XamlFlair.sln" /p:Configuration=Release /p:Platform="Any CPU"
%BUILD% "XamlFlair.sln" /p:Configuration=Release /p:Platform=ARM
%BUILD% "XamlFlair.sln" /p:Configuration=Release /p:Platform=x64
%BUILD% "XamlFlair.sln" /p:Configuration=Release /p:Platform=x86
REM -------------------------