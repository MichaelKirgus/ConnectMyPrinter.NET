Imports System.Windows.Forms
Imports ConnectMyPrinterDriverNotifications
Imports ConnectMyPrinterEnumerationLib

<Serializable> Public Class AppSettingsClass
    ''' <summary>
    ''' Aktiviere das schreiben von Logs in der Anwendung.
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
    Property AutoConnectPrinterIfExactResult As Boolean = False
    Property DeletePrinterIfExists As Boolean = True
    Property AllowUserToChangeSettings As Boolean = True
    Property AllowUserToSaveSettings As Boolean = True
    Property LocalActionsNeedElevation As Boolean = True
    Property PrinterSpoolerRestartNeedElevation As Boolean = True
    Property EnableElevationBypass As Boolean = False
    Property ElevationBypassDomain As String = "WORKGROUP"
    Property ElevationBypassUsername As String = ""
    Property ElevationBypassPassword As String = ""
    Property DeletePrinterDriverLowLevel As Boolean = False
    Property LocalMachineRegistryPermission As Boolean = True
    Property DeleteLocalMachinePartOnUserPrinterDelete As Boolean = False
    Property PrinterAdminPath As String = "C:\Windows\System32\Printing_Admin_Scripts\de-DE"
    Property CleanPrinterDriverPackagesAtPrinterRemove As Boolean = False
    Property CleanPrinterDriverPackagesAtPrinterDriverRemove As Boolean = True
    Property RestartPrintSpoolerAtPrinterRemove As Boolean = True
    Property CleanLostPrinterDriverItemsAtPrinterRemove As Boolean = True
    Property CleanLostPrinterDriverItemsAtPrinterDriverRemove As Boolean = True
    Property ForceAdministratorRightsOnForceDelete As Boolean = False
    Property SavedPrintersProfileFile As String = ""
    Property ShowLocalPrinters As Boolean = True
    Property HiddenPrinterList As List(Of String)
    Property CheckUserSpoolerPermissions As Boolean = False
    Property AlwaysCheckForNewPrinters As Boolean = True
    Property AlwaysCheckForNewPrintersInterval As Integer = 2000
    Property DoubleClickActionOnPrinterItem As DoubleClickActionOnPrinterItemAction = DoubleClickActionOnPrinterItemAction.ShowPrinterDriverSettingsDialog
    Property ShowRestartPrinterQueueButton As Boolean = True
    Property AllowUserDeleteLocalPrinter As Boolean = True
    Property AllowForceDeletePrinterStartWithoutAdminRights As Boolean = True
    Property ShowForceDeletePrinterNonAdminMessageAtStart As Boolean = True
    Property AllowDeleteAllPrintersStartWithoutAdminRights As Boolean = True
    Property ShowDriverNotifications As Boolean = True
    Property ShowAdvancedPrinterListButton As Boolean = True
    Property ShowPrintManagementCenterEntry As Boolean = True
    Property ShowForceDeletePrinterEntry As Boolean = True
    Property ShowAppSettingsConsoleEntry As Boolean = True
    Property ShowProgressCircleOnEvents As Boolean = True
    Property ShowDefaultPrinterAlwaysOnTop As Boolean = True
    Property DriverNotifications As List(Of DriverNotifications)
    Property ShowExitEntryInTrayApp As Boolean = True
    Property ShowManagePrintersEntryInTrayApp As Boolean = True
    Property ShowCompanyLogoInTrayApp As Boolean = True
    Property ShowRefreshEntryInTrayApp As Boolean = True
    Property ShowClassicTrayMenuStyleInTrayApp As Boolean = False
    Property DoubleClickOnTrayIconStartsMainApp As Boolean = True
    Property ShowDeletePrinterEntryInTrayApp As Boolean = True
    Property ShowChangeDefaultPrinterDriverSettingsEntryInTrayApp As Boolean = True
    Property ShowOpenPrinterWebsiteEntryInTrayApp As Boolean = False
    Property ShowRestartPrinterServiceEntryInTrayApp As Boolean = False
    Property ShowBackupPrinterEnvironmentEntryInTrayApp As Boolean = False
    Property ShowTrayAppAfterInstall As Boolean = True
    Property UseTracePathFeature As Boolean = False
    Property ActionsTracePath As String = "%TEMP%\PrinterActions"
    Property ActionsTraceAdminPath As String = "\c$\%TEMP%\PrinterActions"
    Property ProcessActionsOnTrayStart As Boolean = True
    Property AutoBackupPrinterEnvironmentAtStartup As Boolean = False
    Property AutoBackupPrinterEnvironmentAtLogout As Boolean = False
    Property AutoBackupPrinterEnvironmentPath As String = ""
    Property AutoBackupPrinterEnvironmentFilenameBegin As String = ""
    Property IgnoreLocalPrintersAtRemoteFetching As Boolean = True
    Property IgnoreLocalPrintersAtAutoBackup As Boolean = True

    Public Enum DoubleClickActionOnPrinterItemAction
        DoNothing = 0
        ShowPrinterQueueDialog = 1
        ShowPrinterPropertiesDialog = 2
        ShowPrinterDriverSettingsDialog = 3
        MakePrinterToDefaultPrinter = 4
    End Enum

    Public Enum AppWindowPosition As Integer
        CenterScreen = 0
        AtNotificationBar = 1
        AtStartMenu = 2
    End Enum
End Class
