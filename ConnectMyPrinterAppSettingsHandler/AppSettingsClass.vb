'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports System.Windows.Forms
Imports ConnectMyPrinterDriverNotifications
Imports ConnectMyPrinterEnumerationLib

<Serializable> Public Class AppSettingsClass
    ''' <summary>
    ''' Aktiviere das Schreiben von Logs in der Anwendung.
    ''' </summary>
    Property EnableLogging As Boolean = False
    ''' <summary>
    ''' Die Logdatei, in welche die Logeinträge der Anwendung geschrieben werden.
    ''' </summary>
    Property LogFile As String = "Log.txt"
    ''' <summary>
    ''' Der Fenstertitel der Hauptanwendung.
    ''' </summary>
    Property WindowTitle As String = "ConnectMyPrinter.NET"
    ''' <summary>
    ''' Unterschrift unter dem Fenstertitel (optional).
    ''' </summary>
    Property UserInformation As String = "Bitte geben Sie einen Druckernamen ein."
    ''' <summary>
    ''' Zusätzliche Informationen für den Endanwender (optional).
    ''' </summary>
    Property AdditionalUserInformation As String = ""
    ''' <summary>
    ''' Zusätzliche Informationen für den Endanwender, welche im unteren Bereich des Hauptfensters angezeigt werden (optional).
    ''' </summary>
    Property AdditionalSupportBottomInformation As String = ""
    ''' <summary>
    ''' Zusätzliche Informationen für den Endanwender im Über-Fenster der Anwendung (optional).
    ''' </summary>
    Property AdditionalAboutInformation As String = ""
    ''' <summary>
    ''' Der Dateipfad zum benutzerdefinierten Logo (JPEG, PNG, GIF, BMP) (optional).
    ''' </summary>
    Property CompanyLogoImagePath As String = ""
    ''' <summary>
    ''' Ein Base64-Codierter String, welcher ein Bildstrom im Dateiformat (JPEG, PNG, GIF, BMP) darstellt. (optional).
    ''' </summary>
    Property CompanyLogoImageBase64 As String = ""
    ''' <summary>
    ''' Die URL, welche im Standardbrowser geöffnet werden soll, wenn auf das benutzerdefinierte Loge geklickt wird (optional).
    ''' </summary>
    Property CompanyLogoImageClickURL As String = ""
    ''' <summary>
    ''' Das Design des Hauptfensters (und einiger Dialogfenster).
    ''' </summary>
    Property WindowStyle As MetroFramework.MetroColorStyle = MetroFramework.MetroColorStyle.Black
    ''' <summary>
    ''' Das Design des Eingabefeldes für die Druckersuche.
    ''' </summary>
    Property ComboxBoxStyle As MetroFramework.MetroColorStyle = MetroFramework.MetroColorStyle.Black
    ''' <summary>
    ''' Das Design des TabControls (Lokale Drucker/Gespeicherte Drucker).
    ''' </summary>
    Property TabControlStyle As MetroFramework.MetroColorStyle = MetroFramework.MetroColorStyle.Black
    ''' <summary>
    ''' Das Design der Schaltfläche für das Verbinden des Druckers.
    ''' </summary>
    Property ButtonControlStyle As MetroFramework.MetroColorStyle = MetroFramework.MetroColorStyle.Black
    ''' <summary>
    ''' Das Design der ProgressSpinner für die Anzeige von Ladevorgängen.
    ''' </summary>
    Property SpinnerControlStyle As MetroFramework.MetroColorStyle = MetroFramework.MetroColorStyle.Black
    ''' <summary>
    ''' Verhalten, wie mit DPI-Anpassungen in der Anwendung umgegangen werden soll.
    ''' </summary>
    Property MainWindowAutoScaleMode As AutoScaleMode = AutoScaleMode.Font
    ''' <summary>
    ''' Pfad zu einer benutzerdefinierten RTF-Datei für zusätzliche Informationen für den Endanwender (optional).
    ''' </summary>
    Property AdditionalUserHelpInformationRTF As String = ""
    ''' <summary>
    ''' Legt fest, ob bei einem Klick auf die Benutzerinformation die RTF-Datei angezeigt werden soll.
    ''' </summary>
    Property ShowAdditionalUserHelpOnWindowMouseEnter As Boolean = False
    ''' <summary>
    ''' Legt fest, ob bei einem Klick auf die TextBox für die Druckersuche die RTF-Datei angezeigt werden soll.
    ''' </summary>
    Property ShowAdditionalUserHelpOnTextFieldClick As Boolean = False
    ''' <summary>
    ''' Legt fest, ob nach Anwendungsstart direkt das Eingabefeld für die Eingabe eines Druckernamens fokussiert werden soll.
    ''' </summary>
    Property FocusPrinternameFieldAtStart As Boolean = True
    ''' <summary>
    ''' Legt fest, ob ein Endanwender auch bereits während der Suche nach Druckern auf den Printservern einen Druckernamen eingeben darf.
    ''' </summary>
    Property AllowUserToEnterPrinternameBeforeSearchFinished As Boolean = False
    ''' <summary>
    ''' Legt fest, in welcher Position das Hauptfenster direkt nach dem Start angezeigt wird.
    ''' </summary>
    Property StartWindowPosition As AppWindowPosition = AppWindowPosition.CenterScreen
    ''' <summary>
    ''' Legt fest, welche Höhe das Fenster direkt nach dem Start einnimmt.
    ''' </summary>
    Property StartWindowHeight As Integer = 620
    ''' <summary>
    ''' Legt fest, ob die Anwendung nur im Minimal-Modus gestartet wird, d.h. es können nur Drucker verbunden, aber keine Drucker gelöscht oder die Standardeinstellungen geändert werden.
    ''' </summary>
    Property StartInMinimalMode As Boolean = False
    ''' <summary>
    ''' Legt fest, ob ein Endanwender die Anwendung in den normalen Modus versetzen kann, d.h. expandieren, damit auch Druckereinstellungen geändert werden können.
    ''' </summary>
    Property AllowExpandMode As Boolean = True
    ''' <summary>
    ''' Legt fest, ob das Hauptfenster immer im Vordergrund angezeigt wird.
    ''' </summary>
    Property ShowTopMost As Boolean = False
    ''' <summary>
    ''' Definiert die verfügbaren Printserver, auf welchen nach Druckern gesucht werden soll.
    ''' </summary>
    Property PrintServers As New List(Of PrintServerItem)
    ''' <summary>
    ''' Legt die maximale Zeitspanne fest, in welcher die Anwendung nach Druckern auf einem Printserver sucht. Angabe in ms.
    ''' </summary>
    Property MaxPrinterCollectTime As Integer = 10000
    ''' <summary>
    ''' Legt fest, ob die Suche nach Druckern unterbrochen werden soll, wenn ein Printserver nicht erreichbar ist.
    ''' </summary>
    Property CancelCollectionOnPrintServerNotAvailable As Boolean = False
    ''' <summary>
    ''' Legt fest, ob neben dem Freigabenamen des Druckers zusätzliche Informationen (Beschreibung, Standort, Status) vom Printserver abgerufen werden. Diese Einstellung sollte in größeren Umgebungen deaktiviert werden.
    ''' </summary>
    Property CollectAdditionalInformation As Boolean = True
    ''' <summary>
    ''' Legt fest, ob ein Benutzer nach dem gewünschten Printserver gefragt wird, wenn ein Drucker auf mehreren Printserver angelegt ist.
    ''' </summary>
    Property AskUserIfMultipleResults As Boolean = True
    ''' <summary>
    ''' Legt fest, ob eine Abfrage angezeigt werden soll, bevor ein Drucker verbunden wird.
    ''' </summary>
    Property AskUserIfConnectPrinter As Boolean = True
    ''' <summary>
    ''' Legt fest, ob eine Abfrage angezeigt werden soll, bevor ein Drucker getrennt wird.
    ''' </summary>
    Property AskUserIfDisconnectPrinter As Boolean = True
    ''' <summary>
    ''' Legt fest, ob eine Abfrage angezeigt werden soll, bevor ein Drucker von den gespeicherten Druckern gelöscht wird.
    ''' </summary>
    Property AskUserIfRemovePrinterFromSavedPrinters As Boolean = True
    ''' <summary>
    ''' Legt fest, ob die Anzahl der gefundenen Druckern angezeigt wird.
    ''' </summary>
    Property ShowPrinterCountAfterSearch As Boolean = True
    ''' <summary>
    ''' Legt fest, ob es möglich ist, einen nicht gefundenen Drucker zu verbinden.
    ''' </summary>
    Property AllowUserToConnectToNotCollectedPrinter As Boolean = False
    ''' <summary>
    ''' Legt fest, ob es möglich ist, einen Drucker vor dem Verbinden als Standarddrucker auszuwählen (CheckBox wird angezeigt).
    ''' </summary>
    Property AllowUserToSetPrinterToDefaultByConnecting As Boolean = True
    ''' <summary>
    ''' Legt fest, ob die CheckBox für das Setzen des Standarddruckers aktiviert ist.
    ''' </summary>
    Property DefaultPrinterOptionActivated As Boolean = False
    ''' <summary>
    ''' Legt fest, ob das Speichern von Druckern in der Anwendung aktiviert ist.
    ''' </summary>
    Property UsePrinterSavingFeature As Boolean = True
    ''' <summary>
    ''' Legt fest, unter welchem Dateipfad der Cache der gefundenen Druckern angelegt wird. Wenn kein gültiger Dateiname angegeben wird, ist die Cachefunktion deaktiviert.
    ''' </summary>
    Property CacheFoundPrintersFilepath As String = "%TEMP%\PrinterCache.xml"
    ''' <summary>
    ''' Legt fest, ob vor der Suche der Drucker zuerst die zwischengespeicherten Drucker für die Druckersuche verwendet werden.
    ''' </summary>
    Property FirstLoadCachedPrinters As Boolean = False
    ''' <summary>
    ''' Definiert eine feste Zeichenfolge, mit welcher jede Druckerbezeichnung beginnt.
    ''' </summary>
    Property FixedPrefix As String = ""
    ''' <summary>
    ''' Definiert, ab welcher Anzahl von Zeichen dem Anwender Ergebnisse vorgeschlagen werden.
    ''' </summary>
    Property AutoCompleteCount As Integer = 5
    ''' <summary>
    ''' Definiert, ob dem Benutzer ab einer Anzahl von n Elemente Ergebnisse angezeigt werden.
    ''' </summary>
    Property AutoCompleteCountEnable As Boolean = False
    ''' <summary>
    ''' Definiert, wie viele Elemente dem Benutzer in den Vorschlägen angezeigt werden.
    ''' </summary>
    Property AutoCompleteMaxVisibleItems As Integer = 20
    ''' <summary>
    ''' Definiert, wie groß der Speicher für gefundene Drucker ist. Falls mehr Drucker gefunden wurden, werden alle Drucker über dieser Anzahl ignoriert.
    ''' </summary>
    Property SearchCount As Integer = 5
    ''' <summary>
    ''' Definiert, ob die Groß/Kleinschreibung bei der Suche nach einem Drucker beachtet werden soll.
    ''' </summary>
    Property IgnoreUpperLowerCase As Boolean = True
    ''' <summary>
    ''' Definiert, ob bei einem direkten (einzigen) Treffer der Drucker direkt verbunden werden soll.
    ''' </summary>
    Property AutoConnectPrinterIfExactResult As Boolean = False
    ''' <summary>
    ''' Wenn ein bereits bestehender Drucker verbunden werden soll wird dieser neu verbunden (getrennt + verbunden).
    ''' </summary>
    Property DeletePrinterIfExists As Boolean = True
    ''' <summary>
    ''' Der Endanwender ist in der Lage, über die Oberfläche die Anwendungseinstellungen aufzurufen.
    ''' </summary>
    Property AllowUserToChangeSettings As Boolean = True
    ''' <summary>
    ''' Der Endanwender ist nur dann in der Lage, die Anwendungseinstellungs-Konsole aufzurufen, wenn er administrative Berechtigungen besitzt.
    ''' </summary>
    Property AllowUserToChangeSettingsOnlyWithElevatedRights As Boolean = False
    ''' <summary>
    ''' Die Anwendungseinstellungen können dauerhaft gespeichert werden.
    ''' </summary>
    Property AllowUserToSaveSettings As Boolean = True
    ''' <summary>
    ''' Definiert, ob für das lokale Löschen von Druckern administrative Berechtigungen notwendig sind.
    ''' </summary>
    Property LocalActionsNeedElevation As Boolean = True
    ''' <summary>
    ''' Definiert, ob für das Starten/Stoppen der Druckerwarteschlange administrative Berechtigungen notwendig sind.
    ''' </summary>
    Property PrinterSpoolerRestartNeedElevation As Boolean = True
    ''' <summary>
    ''' Definiert, ob für administrative Prozesse abweichende Benutzerdaten verwendet werden sollen.
    ''' </summary>
    Property EnableElevationBypass As Boolean = False
    ''' <summary>
    ''' Definiert die Arbeitsgruppe/Domäne, welche für die Anmeldung für den administrativen Prozess verwendet werden soll.
    ''' </summary>
    Property ElevationBypassDomain As String = "WORKGROUP"
    ''' <summary>
    ''' Definiert den Benutzernamen, welcher für die Anmeldung für den administrativen Prozess verwendet werden soll.
    ''' </summary>
    Property ElevationBypassUsername As String = ""
    ''' <summary>
    ''' Definiert das Kennwort, welches für die Anmeldung für den administrativen Prozess verwendet werden soll.
    ''' </summary>
    Property ElevationBypassPassword As String = ""
    ''' <summary>
    ''' Definiert, ob ein Treiber neben dem Löschen über die Windows-APIs auch zusätzlich Low-Level in der Registry sowie im Spooler-Verzeichnis gelöscht werden soll. Diese Funktion ist nur sinnvoll, wenn der Benutzer administrative Berechtigungen hat.
    ''' </summary>
    Property DeletePrinterDriverLowLevel As Boolean = False
    ''' <summary>
    ''' Legt fest, ob vom Installationsprogramm Schreibrechte auf den Spool-Registryzweig eingerichtet wurden. Somit kann das Löschen der Treiber in der Registry ohne administrative Berechtigungen erfolgen.
    ''' </summary>
    Property LocalMachineRegistryPermission As Boolean = True
    ''' <summary>
    ''' Legt fest, ob auch Druckereinstellungen des Treibers auf dem Client gelöscht werden sollen, wenn der Drucker gelöscht wird.
    ''' </summary>
    Property DeleteLocalMachinePartOnUserPrinterDelete As Boolean = False
    ''' <summary>
    ''' Definiert den PrinterAdminScripts-Pfad. Unter einer deutschsprachigen Windows-Version ist dies "C:\Windows\System32\Printing_Admin_Scripts\de-DE"
    ''' </summary>
    Property PrinterAdminPath As String = "C:\Windows\System32\Printing_Admin_Scripts\de-DE"
    ''' <summary>
    ''' Definiert, ob auch das Treiberpaket nach dem Entfernen des Druckers gelöscht werden soll.
    ''' </summary>
    Property CleanPrinterDriverPackagesAtPrinterRemove As Boolean = False
    ''' <summary>
    ''' Definiert, ob auch das Treiberpaket nach dem Entfernen des Druckertreibers gelöscht werden soll.
    ''' </summary>
    Property CleanPrinterDriverPackagesAtPrinterDriverRemove As Boolean = True
    ''' <summary>
    ''' Definiert, ob auch das Treiberpaket nach dem Entfernen des Druckers gelöscht werden soll.
    ''' </summary>
    Property RestartPrintSpoolerAtPrinterRemove As Boolean = True
    ''' <summary>
    ''' Definiert, ob alle nicht genutzten Treiberpakete nach dem Entfernen des Druckers gelöscht werden sollen.
    ''' </summary>
    Property CleanLostPrinterDriverItemsAtPrinterRemove As Boolean = True
    ''' <summary>
    ''' Definiert, ob alle nicht genutzten Treiberpakete nach dem Entfernen des Druckertreibers gelöscht werden sollen.
    ''' </summary>
    Property CleanLostPrinterDriverItemsAtPrinterDriverRemove As Boolean = True
    ''' <summary>
    ''' Definiert, ob die Anwendung für das Low-Level-Entfernen zwangsweise nur mit Administrator-Berechtigungen gestartet werden kann.
    ''' </summary>
    Property ForceAdministratorRightsOnForceDelete As Boolean = False
    ''' <summary>
    ''' Definiert, in welcher XML-Datei die gespeicherten Drucker abgelegt werden.
    ''' </summary>
    Property SavedPrintersProfileFile As String = ""
    ''' <summary>
    ''' Definiert, ob die Hauptanwendung lokale Drucker anzeigt (oder nur verbundene Netzwerkdrucker).
    ''' </summary>
    Property ShowLocalPrinters As Boolean = True
    ''' <summary>
    ''' Definiert eine String-Collection von Druckernamen, welche nicht in der Hauptanwendung angezeigt werden.
    ''' </summary>
    Property HiddenPrinterList As List(Of String)
    ''' <summary>
    ''' Definiert eine Prüfung während dem Start der Hauptanwendung, um zu erkennen, ob der Benutzer die Berechtigung zum Steuern der Druckerwarteschlange hat. Falls dies der Fall ist, wird für den Start/Stopp der Druckerwarteschlange keine UAC-Meldung mehr angezeigt.
    ''' </summary>
    Property CheckUserSpoolerPermissions As Boolean = False
    ''' <summary>
    ''' Definiert, ob die Anwendung während der Ausführung laufend auf neue Drucker prüft.
    ''' </summary>
    Property AlwaysCheckForNewPrinters As Boolean = True
    ''' <summary>
    ''' Definiert den Abfrageintervall (ms), in welchem die Anwendung während der Ausführung laufend auf neue Drucker prüft.
    ''' </summary>
    Property AlwaysCheckForNewPrintersInterval As Integer = 2000
    ''' <summary>
    ''' Definiert die Aktion, welche ausgeführt wird, wenn ein Anwender einen Doppelkick auf einen Druckereintrag in der Hauptanwendung ausführt.
    ''' </summary>
    Property DoubleClickActionOnPrinterItem As DoubleClickActionOnPrinterItemAction = DoubleClickActionOnPrinterItemAction.ShowPrinterDriverSettingsDialog
    ''' <summary>
    ''' Legt fest, ob in den Steuerschaltflächen der Hauptanwendung die Schaltfläche für den Neustart der Druckerwarteschlange angezeigt wird.
    ''' </summary>
    Property ShowRestartPrinterQueueButton As Boolean = True
    ''' <summary>
    ''' Legt fest, ob ein Anwender einen lokalen Drucker löschen darf.
    ''' </summary>
    Property AllowUserDeleteLocalPrinter As Boolean = True
    ''' <summary>
    ''' Legt fest, ob die Anwendung für das Low-Level-Entfernen auch optional ohne Administrator-Berechtigungen gestartet werden kann.
    ''' </summary>
    Property AllowForceDeletePrinterStartWithoutAdminRights As Boolean = True
    ''' <summary>
    ''' Legt fest, ob die Anwendung für das Low-Level-Entfernen bei einem Start ohne Administrator-Berechtigungen eine Warnmeldung anzeigt.
    ''' </summary>
    Property ShowForceDeletePrinterNonAdminMessageAtStart As Boolean = True
    ''' <summary>
    ''' Legt fest, ob die Anwendung für das Löschen aller Drucker auch ohne Administrator-Berechtigungen gestartet werden kann.
    ''' </summary>
    Property AllowDeleteAllPrintersStartWithoutAdminRights As Boolean = True
    ''' <summary>
    ''' Legt fest, ob die Hauptanwendung benutzerdefinierte RTF-Dateien für bestimmte Treibernamen anzeigt.
    ''' </summary>
    Property ShowDriverNotifications As Boolean = True
    ''' <summary>
    ''' Legt fest, ob Steuerschaltflächen in der Hauptanwendung angezeigt werden, welche eine Druckersuche in einer großen Listenansicht ermöglicht.
    ''' </summary>
    Property ShowAdvancedPrinterListButton As Boolean = True
    ''' <summary>
    ''' Legt fest, ob die Hauptanwendung im administrativen Kontextmenü einen Eintrag für den Start der Windows-Druckerverwaltung anzeigt.
    ''' </summary>
    Property ShowPrintManagementCenterEntry As Boolean = True
    ''' <summary>
    ''' Legt fest, ob die Hauptanwendung im administrativen Kontextmenü einen Eintrag für den Start der Low-Level-Entfernen-Anwendung anzeigt.
    ''' </summary>
    Property ShowForceDeletePrinterEntry As Boolean = True
    ''' <summary>
    ''' Legt fest, ob die Hauptanwendung im administrativen Kontextmenü einen Eintrag für die Einstellungskonsole anzeigt.
    ''' </summary>
    Property ShowAppSettingsConsoleEntry As Boolean = True
    ''' <summary>
    ''' Legt fest, ob die Hauptanwendung bei jedem Vorgang einen Lade-Kreis anzeigt.
    ''' </summary>
    Property ShowProgressCircleOnEvents As Boolean = True
    ''' <summary>
    ''' Legt fest, ob der Standarddrucker immer ganz oben in der Liste anzeigt werden soll.
    ''' </summary>
    Property ShowDefaultPrinterAlwaysOnTop As Boolean = True
    ''' <summary>
    ''' Legt alle Treiberinformationen fest, bei welchen eine benutzerdefinierte RTF-Datei angezeigt wird. (Nach dem Verbinden eines Druckers)
    ''' </summary>
    Property DriverNotifications As List(Of DriverNotifications)
    ''' <summary>
    ''' Legt fest, ob im Kontextmenü der Trayanwendung ein Eintrag für das Beenden der Trayanwendung angezeigt wird.
    ''' </summary>
    Property ShowExitEntryInTrayApp As Boolean = True
    ''' <summary>
    ''' Legt fest, ob im Kontextmenü der Trayanwendung ein Eintrag für das Starten der Hauptanwendung angezeigt wird.
    ''' </summary>
    Property ShowManagePrintersEntryInTrayApp As Boolean = True
    ''' <summary>
    ''' Legt fest, ob im oberen Bereich des Kontextmenüs der Trayanwendung das benutzerdefinierte Logo angezeigt wird.
    ''' </summary>
    Property ShowCompanyLogoInTrayApp As Boolean = True
    ''' <summary>
    ''' Legt fest, ob im Kontextmenü der Trayanwendung ein Eintrag für das Aktualisieren der Druckerauflistung angezeigt wird. Dies startet die Trayanwendung neu.
    ''' </summary>
    Property ShowRefreshEntryInTrayApp As Boolean = True
    ''' <summary>
    ''' Legt fest, ob das Kontextmenü der Trayanwendung in einem klassischen Design angezeigt wird.
    ''' </summary>
    Property ShowClassicTrayMenuStyleInTrayApp As Boolean = False
    ''' <summary>
    ''' Legt fest, ob ein Doppelklick auf das Trayicon die Hauptanwendung starten soll.
    ''' </summary>
    Property DoubleClickOnTrayIconStartsMainApp As Boolean = True
    ''' <summary>
    ''' Legt fest, ob im Kontextmenü der Trayanwendung ein Eintrag für das Entfernen/Trennen des Druckers angezeigt werden soll. (Bei jedem Drucker im Untermenü)
    ''' </summary>
    Property ShowDeletePrinterEntryInTrayApp As Boolean = True
    ''' <summary>
    ''' Legt fest, ob im Kontextmenü der Trayanwendung ein Eintrag für das Ändern der Standarddruckereinstellungen angezeigt werden soll. (Bei jedem Drucker im Untermenü)
    ''' </summary>
    Property ShowChangeDefaultPrinterDriverSettingsEntryInTrayApp As Boolean = True
    ''' <summary>
    ''' Legt fest, ob im Kontextmenü der Trayanwendung ein Eintrag für das Öffnen der Gerätewebseite (analog Freigabename) im Standardbrowser angezeigt werden soll. (Bei jedem Drucker im Untermenü)
    ''' </summary>
    Property ShowOpenPrinterWebsiteEntryInTrayApp As Boolean = False
    ''' <summary>
    ''' Legt fest, ob im Kontextmenü der Trayanwendung ein Eintrag für den Neustart der Druckerwarteschlange angezeigt werden soll.
    ''' </summary>
    Property ShowRestartPrinterServiceEntryInTrayApp As Boolean = False
    ''' <summary>
    ''' Legt fest, ob im Kontextmenü der Trayanwendung ein Eintrag für die Sicherung der Druckerumgebung angezeigt werden soll.
    ''' </summary>
    Property ShowBackupPrinterEnvironmentEntryInTrayApp As Boolean = False
    ''' <summary>
    ''' Legt fest, ob die Trayanwendung direkt nach der Installation der Anwendung gestartet werden soll.
    ''' </summary>
    Property ShowTrayAppAfterInstall As Boolean = True
    ''' <summary>
    ''' Legt fest, ob auch die lokalen Drucker in dem Kontextmenü der Trayanwendung aufgeführt werden sollen.
    ''' </summary>
    Property ShowLocalPrintersInTrayApp As Boolean = True
    ''' <summary>
    ''' Legt fest, ob die Trayanwendung sichtbar im Infobereich der Taskleiste sein soll. (Sonst wird diese versteckt nur als Prozess ausgeführt)
    ''' </summary>
    Property ShowTrayAppIcon As Boolean = True
    ''' <summary>
    ''' Legt fest, wie lange auf einen Shell bzw. API-Befehl gewartet werden soll.
    ''' </summary>
    Property MaxPrinterAPIShellTimout As Integer = 30000
    ''' <summary>
    ''' Legt fest, on die Anwendung auf Remote-Profildateien reagieren soll.
    ''' </summary>
    Property UseTracePathFeature As Boolean = False
    ''' <summary>
    ''' Legt fest, in welchem Pfad die Anwendung auf Remote-Profildateien reagieren soll.
    ''' </summary>
    Property ActionsTracePath As String = "%TEMP%\PrinterActions"
    ''' <summary>
    ''' Legt fest, in welchem Pfad der Profileditor sowie die Konsolenanwendung Remote-Profildateien generieren soll. Der Pfad muss mit "\" starten, da dieser Pfad direkt nach dem Hostnamen angefügt wird.
    ''' </summary>
    Property ActionsTraceAdminPath As String = "\c$\%TEMP%\PrinterActions"
    ''' <summary>
    ''' Legt fest, on die Trayanwendung beim Start auf nicht ausgeführte Remote-Profildateien prüfen und diese ausführen soll.
    ''' </summary>
    Property ProcessActionsOnTrayStart As Boolean = True
    ''' <summary>
    ''' Legt fest, ob die Trayanwendung die Druckerumgebung beim Tray-Anwendungsstart sichern soll.
    ''' </summary>
    Property AutoBackupPrinterEnvironmentAtStartup As Boolean = False
    ''' <summary>
    ''' Legt fest, ob die Trayanwendung die Druckerumgebung bei der Abmeldung des Benutzers sichern soll.
    ''' </summary>
    Property AutoBackupPrinterEnvironmentAtLogout As Boolean = False
    ''' <summary>
    ''' Legt fest, ob die Trayanwendung die Cache-Datei für die Offline-Funktionalität (z.B. über VPN) erstellen soll.
    ''' </summary>
    Property AutoGeneratePrinterCacheIfServerIsReachable As Boolean = False
    ''' <summary>
    ''' Legt fest, welchen Hostname bzw. IP die Trayanwendung pingt, um zu prüfen, ob die Cache-Datei aktualisiert werden soll. Wird nur genutzt, wenn "AutoGeneratePrinterCacheIfServerIsReachable" gesetzt ist. Bei einer leeren Zeichnfolge wird der Cache in jedem Fall aktualisiert.
    ''' </summary>
    Property AutoGeneratePrinterCacheProbeServer As String = ""
    ''' <summary>
    ''' Legt den Timeout fest, wie lange auf eine Antwort des Servers für die Hintergrund-Aktualisierung gewartet wird. Wird nur genutzt, wenn "AutoGeneratePrinterCacheIfServerIsReachable" gesetzt ist.
    ''' </summary>
    Property AutoGeneratePrinterCacheProbeServerTimeout As Integer = 1000
    ''' <summary>
    ''' Legt fest, wie lange nach dem Start der Tray-Anwendung gewartet wird, bis die Prüfung des Pings ausgeführt wird und ggf. der Cache aktualisiert wird.
    ''' </summary>
    Property AutoGeneratePrinterCacheDelay As Integer = 60000
    ''' <summary>
    ''' Legt fest, ob im Kontextmenü der Trayanwendung ein zusätzlicher benutzerdefinierter Menüeintrag angezeigt werden soll, welcher einen benutzerdefinierten Shell-Befehl mit Benutzerrechten ausführt.
    ''' </summary>
    Property ShowAdditionalCustomMenuEntryInTrayApp As Boolean = False
    ''' <summary>
    ''' Legt den Text bzw. die Bezeichnung fest, welcher für den zusätzlichen benutzerdefinierter Menüeintrag im Kontextmenü eingezeigt werden soll.
    ''' </summary>
    Property AdditionalCustomMenuEntryText As String = ""
    ''' <summary>
    ''' Legt das Icon/Symbol fest, welcher für den zusätzlichen benutzerdefinierter Menüeintrag im Kontextmenü eingezeigt werden soll. Als Text wird eine Base64-Codierte Zeichenfolge erwartet.
    ''' </summary>
    Property AdditionalCustomMenuEntryIconBase64 As String = ""
    ''' <summary>
    ''' Legt den Befehl fest, welcher für den zusätzlichen benutzerdefinierter Menüeintrag im Kontextmenü bei einm Klick ausgeführt werden soll.
    ''' </summary>
    Property AdditionalCustomMenuEntryShellCommand As String = ""
    ''' <summary>
    ''' Legt fest, ob der generische Shell-Befehl zum Ausführen des Befehls verwendet werden soll (False) oder ob eine alternative Methode zum Start verwendet wird (True).
    ''' </summary>
    Property AdditionalCustomMenuEntryUseAlternativeShellExecMode As Boolean = False
    ''' <summary>
    ''' Legt den Pfad fest, in welcher die Trayanwendung die Druckerumgebung sichert.
    ''' </summary>
    Property AutoBackupPrinterEnvironmentPath As String = ""
    ''' <summary>
    ''' Legt den ersten Teil des Dateinamens für die Sicherung fest. Syntax ist hierbei FilenameStart_%Hostname%.prpr
    ''' </summary>
    Property AutoBackupPrinterEnvironmentFilenameBegin As String = ""
    ''' <summary>
    ''' Definiert, ob bei einer entfernen Abfrage der installierten/verbundenen Drucker die lokalen Drucker ignoriert werden.
    ''' </summary>
    Property IgnoreLocalPrintersAtRemoteFetching As Boolean = True
    ''' <summary>
    ''' Definiert, ob bei einer Sicherung der installierten/verbundenen Drucker die lokalen Drucker ignoriert werden.
    ''' </summary>
    Property IgnoreLocalPrintersAtAutoBackup As Boolean = True
    ''' <summary>
    ''' Definiert, ob beim Reporting der installierten/verbundenen Drucker die lokalen Drucker ignoriert werden.
    ''' </summary>
    Property IgnoreLocalPrintersAtReporting As Boolean = True
    ''' <summary>
    ''' Definiert, ob beim Start der Trayanwendung Reports über die verbundenen Drucker auf einer zentralen Freigabe generiert werden.
    ''' </summary>
    Property UseReportingFeature As Boolean = False
    ''' <summary>
    ''' Definiert, in welchen Pfad die Reports über die verbundenen Drucker auf einer zentralen Freigabe generiert werden.
    ''' </summary>
    Property ReportingPath As String = ""
    ''' <summary>
    ''' Definiert Benutzer, welche für die Reports über die verbundenen Drucker auf einer zentralen Freigabe ausgenommen sind.
    ''' </summary>
    Property ReportingUserBlacklist As String = ""
    ''' <summary>
    ''' Definiert, ob es über eine Profildatei möglich ist, einen benutzerdefinierten Shell-Befehl auszuführen.
    ''' </summary>
    Property AllowExecuteCustomCommandFromProfileFile As Boolean = True

    ''' <summary>
    ''' Definiert die Standardaktion bei einem Doppelklick auf einen Druckereintrag in der Hauptanwendung.
    ''' </summary>
    Public Enum DoubleClickActionOnPrinterItemAction

        DoNothing = 0
        ShowPrinterQueueDialog = 1
        ShowPrinterPropertiesDialog = 2
        ShowPrinterDriverSettingsDialog = 3
        MakePrinterToDefaultPrinter = 4
    End Enum

    ''' <summary>
    ''' Definiert die Startposition des Hauptfensters der Hauptanwendung.
    ''' </summary>
    Public Enum AppWindowPosition As Integer
        CenterScreen = 0
        AtNotificationBar = 1
        AtStartMenu = 2
    End Enum
End Class
