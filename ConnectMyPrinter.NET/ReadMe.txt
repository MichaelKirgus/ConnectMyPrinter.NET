Die Anwendung benötigt das .NET Framework 4.5.2.
Das benutzerdefinierte Bild sollte genau 118x44 groß sein.
Um dem aktuell angemeldeten Benutzer das Stoppen/Starten der Druckerwarteschlange zu ermöglichen, bitte die Anwendung "ConnectMyPrinterACLHelper.exe" ausführen.
Um die Anwendung selbst aus einem Installationssystem aufzurufen, ist folgende Syntax notwendig:

ConnectMyPrinterACLHelper.exe /ELEVATED /DOMAIN:%Domäne% /USER:%Benutzername des aktuell angemeldeten Benutzers%

Mit dem Argument /? kann eine ausführliche Hilfe in der Konsole angezeigt werden.


Falls nicht die Einstellungsdatei im Anwendungsverzeichnis verwendet werden soll, kann über die Befehlszeile mit

ConnectMyPrinter.NET.exe /SETTINGS|%AppSettings-Datei% 

eine benutzerdefinierte Einstellungsdatei geladen werden.
Falls diese im Pfad nicht existiert, wird eine neue Einstellungsdatei generiert.

Wird kein Befehlszeilenargument übergeben und die Anwendung findet keine Einstellungsdatei im Startverzeichnis,
wird unter C:\Users\%Benutzername%\AppData\Roaming\Michael Kirgus\ConnectMyPrinter.NET nach der Datei AppSettings.xml gesucht.
Wenn dort keine Datei gefunden wird, legt die Anwendung eine neue Einstellungsdatei im Startverzeichnis an.