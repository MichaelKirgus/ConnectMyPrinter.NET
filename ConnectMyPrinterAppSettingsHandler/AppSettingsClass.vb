Imports ConnectMyPrinterDriverNotifications
Imports ConnectMyPrinterEnumerationLib

<Serializable> Public Class AppSettingsClass
    Property EnableLogging As Boolean = False
    Property LogFile As String = "Log.txt"
    Property WindowTitle As String = "ConnectMyPrinter.NET"
    Property UserInformation As String = "Bitte geben Sie einen Druckernamen ein."
    Property AdditionalUserInformation As String = ""
    Property CompanyLogoImagePath As String = ""
    Property AdditionalUserHelpInformationRTF As String = ""
    Property ShowAdditionalUserHelpOnWindowMouseEnter As Boolean = False
    Property ShowAdditionalUserHelpOnTextFieldClick As Boolean = False
    Property StartWindowPosition As AppWindowPosition = AppWindowPosition.CenterScreen
    Property StartWindowHeight As Integer = 620
    Property StartInMinimalMode As Boolean = False
    Property AllowExpandMode As Boolean = True
    Property ShowTopMost As Boolean = False
    Property PrintServers As New List(Of PrintServerItem)
    Property CollectAdditionalInformation As Boolean = True
    Property AskUserIfMultipleResults As Boolean = True
    Property AskUserIfConnectPrinter As Boolean = True
    Property AskUserIfDisconnectPrinter As Boolean = True
    Property AskUserIfRemovePrinterFromSavedPrinters As Boolean = True
    Property ShowPrinterCountAfterSearch As Boolean = True
    Property AllowUserToConnectToNotCollectedPrinter As Boolean = False
    Property AllowUserToSetPrinterToDefaultByConnecting As Boolean = True
    Property DefaultPrinterOptionActivated As Boolean = False
    Property UsePrinterSavingFeature As Boolean = True
    Property FixedPrefix As String = ""
    Property AutoCompleteCount As Integer = 5
    Property AutoCompleteCountEnable As Boolean = False
    Property AutoCompleteMaxVisibleItems As Integer = 20
    Property SearchCount As Integer = 5
    Property IgnoreUpperLowerCase As Boolean = True
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
    Property DriverNotifications As List(Of DriverNotifications)

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
