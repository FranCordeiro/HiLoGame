@echo off
cd /d "%~dp0"

start "" "GamingServer\GamingServer\bin\Debug\net6.0\GamingServer.exe"

timeout /t 2 /nobreak > nul

start "" "GamingClient\GamingClient\bin\Debug\net6.0\GamingClient.exe"
timeout /t 1 /nobreak > nul
start "" "GamingClient\GamingClient\bin\Debug\net6.0\GamingClient.exe"