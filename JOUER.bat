@echo off
chcp 65001 >nul
setlocal
cd /d "%~dp0"
title Code Quest - Installation et lancement

echo ============================================================
echo                     CODE QUEST
echo ============================================================
echo.

set "ROOT=%~dp0"
set "ROOT=%ROOT:~0,-1%"
set "DOTNET_DIR=%ROOT%\.dotnet"
set "DOTNET=%DOTNET_DIR%\dotnet.exe"
set "EXE=%ROOT%\bin\Release\net8.0\win-x64\publish\CodeQuest.exe"
set "SHORTCUT=%USERPROFILE%\Desktop\Code Quest.lnk"

:: ---------- 1. Installer .NET DANS le dossier si absent ----------
if not exist "%DOTNET%" (
    echo [1/4] Installation de .NET 8 dans le dossier du projet...
    echo       Environ 200 Mo, une seule fois.
    echo.
    powershell -NoProfile -ExecutionPolicy Bypass -Command ^
        "$ProgressPreference='SilentlyContinue';" ^
        "Invoke-WebRequest -Uri 'https://dot.net/v1/dotnet-install.ps1' -OutFile '%ROOT%\dotnet-install.ps1';" ^
        "& '%ROOT%\dotnet-install.ps1' -Channel 8.0 -InstallDir '%DOTNET_DIR%' -NoPath"
    if errorlevel 1 (
        echo.
        echo [X] Echec de l'installation de .NET.
        pause
        exit /b 1
    )
    del "%ROOT%\dotnet-install.ps1" >nul 2>nul
    echo [OK] .NET installe dans : %DOTNET_DIR%
) else (
    echo [1/4] .NET deja installe dans le projet.
)

:: ---------- 2. Compiler le jeu ----------
if not exist "%EXE%" (
    echo [2/4] Compilation du jeu ^(1-2 minutes^)...
    echo.
    "%DOTNET%" publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
    if errorlevel 1 (
        echo.
        echo [X] Erreur de compilation.
        pause
        exit /b 1
    )
    echo.
    echo [OK] Compilation terminee.
) else (
    echo [2/4] Jeu deja compile.
)

:: ---------- 3. Creer le raccourci sur le Bureau ----------
echo [3/4] Creation du raccourci sur le Bureau...
if exist "%SHORTCUT%" del "%SHORTCUT%" >nul 2>nul

powershell -NoProfile -ExecutionPolicy Bypass -Command ^
    "$ws = New-Object -ComObject WScript.Shell;" ^
    "$sc = $ws.CreateShortcut('%SHORTCUT%');" ^
    "$sc.TargetPath = '%EXE%';" ^
    "$sc.WorkingDirectory = '%ROOT%';" ^
    "$sc.IconLocation = '%EXE%,0';" ^
    "$sc.Description = 'Code Quest';" ^
    "$sc.Save()"

if exist "%SHORTCUT%" (
    echo [OK] Raccourci "Code Quest" cree sur le Bureau.
) else (
    echo [!] Raccourci non cree, mais le jeu va se lancer.
)

:: ---------- 4. Lancer le jeu ----------
echo [4/4] Lancement du jeu...
echo.
echo ============================================================
echo.
"%EXE%"

echo.
echo Le jeu est termine.
timeout /t 3 >nul
exit /b 0