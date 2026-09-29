@echo off
chcp 65001 >nul
setlocal
cd /d "%~dp0"
title Code Quest - Installation et lancement

echo ============================================================
echo                     CODE QUEST
echo              Installation et lancement
echo ============================================================
echo.

set "ROOT=%~dp0"
set "ROOT=%ROOT:~0,-1%"
set "DOTNET_DIR=%ROOT%\.dotnet"
set "DOTNET=%DOTNET_DIR%\dotnet.exe"
set "EXE=%ROOT%\bin\Release\net8.0\win-x64\publish\CodeQuest.exe"
set "SHORTCUT=%USERPROFILE%\Desktop\Code Quest.lnk"
set "LOG=%TEMP%\codequest_build.log"

:: ============================================================
:: ETAPE 1 / 4 - Installation de .NET dans le dossier
:: ============================================================
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
        echo   [X] Echec de l'installation de .NET.
        pause
        exit /b 1
    )
    del "%ROOT%\dotnet-install.ps1" >nul 2>nul
    echo       [OK] .NET installe dans : %DOTNET_DIR%
) else (
    echo [1/4] .NET deja installe dans le projet.
)
echo.

:: ============================================================
:: ETAPE 2 / 4 - Compilation du jeu
:: ============================================================
if exist "%EXE%" (
    echo [2/4] Executable deja compile - compilation ignoree.
    echo.
    goto etape3
)

echo [2/4] Compilation du jeu ^(1 a 2 minutes la premiere fois^)...
echo.

if exist "%LOG%" del "%LOG%" >nul 2>nul

powershell -NoProfile -ExecutionPolicy Bypass -Command "$log = '%LOG%'; $p = Start-Process -FilePath 'cmd' -ArgumentList '/c', ('\"%DOTNET%\" publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true > ' + $log + ' 2>&1') -NoNewWindow -PassThru; $spin=@('|','/','-','\'); $i=0; $cr=[char]13; $sw=[System.Diagnostics.Stopwatch]::StartNew(); while (-not $p.HasExited) { $s=$spin[$i %% 4]; $sec=[math]::Floor($sw.Elapsed.TotalSeconds); Write-Host -NoNewline ($cr + '  [' + $s + '] Compilation en cours... ' + $sec + 's    ') -ForegroundColor Yellow; Start-Sleep -Milliseconds 200; $i++ }; $sec=[math]::Floor($sw.Elapsed.TotalSeconds); Write-Host ($cr + '  [OK] Compilation terminee en ' + $sec + 's.              ') -ForegroundColor Green; exit $p.ExitCode"

if errorlevel 1 (
    echo.
    echo   [X] ECHEC de la compilation.
    echo.
    echo   ---- Journal detaille -------------------------------
    if exist "%LOG%" type "%LOG%"
    echo   -----------------------------------------------------
    echo.
    pause
    exit /b 1
)

if not exist "%EXE%" (
    echo.
    echo   [X] La compilation a termine mais l'executable est introuvable :
    echo       %EXE%
    echo.
    pause
    exit /b 1
)

echo.

:: ============================================================
:: ETAPE 3 / 4 - Raccourci sur le Bureau
:: ============================================================
:etape3
echo [3/4] Verification du raccourci Bureau...
if exist "%SHORTCUT%" (
    echo       [OK] Raccourci deja present : "Code Quest"
) else (
    echo       Creation du raccourci "Code Quest"...
    powershell -NoProfile -ExecutionPolicy Bypass -Command "$ws = New-Object -ComObject WScript.Shell; $sc = $ws.CreateShortcut('%SHORTCUT%'); $sc.TargetPath = '%EXE%'; $sc.WorkingDirectory = '%ROOT%'; $sc.IconLocation = '%EXE%,0'; $sc.Description = 'Code Quest - Jeu RPG quiz informatique'; $sc.Save()"
    if exist "%SHORTCUT%" (
        echo       [OK] Raccourci cree sur le Bureau.
    ) else (
        echo       [!] Raccourci non cree ^(le jeu marchera quand meme^).
    )
)
echo.

:: ============================================================
:: ETAPE 4 / 4 - Lancement du jeu
:: ============================================================
echo [4/4] Lancement du jeu...
echo ============================================================
echo.
"%EXE%"

echo.
echo ============================================================
echo Le jeu est termine. A bientot !
echo ============================================================
timeout /t 3 >nul
exit /b 0