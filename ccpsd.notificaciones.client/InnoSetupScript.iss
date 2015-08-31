; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "CCPSD Notificaciones"
#define MyAppVersion "0.1"
#define MyAppPublisher "CCPSD"
#define MyAppURL "http://www.camarasantodomingo.do/"
#define MyAppExeName "ccpsd.notificaciones.service.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{9AEBABB5-FF52-4644-A41C-6F20D85A7569}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
OutputDir=C:\Users\dcabrera\Desktop\New folder
OutputBaseFilename=CCPSDNotificacionesSetup
Compression=lzma
SolidCompression=yes
PrivilegesRequired=admin

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "C:\WorkingDir\ccpsd.notificaciones\ccpsd.notificaciones.service\bin\Release\ccpsd.notificaciones.service.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\WorkingDir\ccpsd.notificaciones\ccpsd.notificaciones.service\bin\Release\ccpsd.notificaciones.core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\WorkingDir\ccpsd.notificaciones\ccpsd.notificaciones.service\bin\Release\ccpsd.notificaciones.service.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\WorkingDir\ccpsd.notificaciones\ccpsd.notificaciones.service\bin\Release\Interop.COMAdmin.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\WorkingDir\ccpsd.notificaciones\ccpsd.notificaciones.service\bin\Release\Interop.SensEvents.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\WorkingDir\ccpsd.notificaciones\ccpsd.notificaciones.service\bin\Release\Microsoft.AspNet.SignalR.Client.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\WorkingDir\ccpsd.notificaciones\ccpsd.notificaciones.service\bin\Release\Microsoft.AspNet.SignalR.Client.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\WorkingDir\ccpsd.notificaciones\ccpsd.notificaciones.service\bin\Release\Microsoft.AspNet.SignalR.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\WorkingDir\ccpsd.notificaciones\ccpsd.notificaciones.service\bin\Release\Microsoft.AspNet.SignalR.Core.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\WorkingDir\ccpsd.notificaciones\ccpsd.notificaciones.service\bin\Release\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\WorkingDir\ccpsd.notificaciones\ccpsd.notificaciones.service\bin\Release\Owin.dll"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{commonstartup}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

