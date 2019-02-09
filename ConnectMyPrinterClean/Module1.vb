Imports System.Globalization
Imports ConnectMyPrinter.NET
Imports ConnectMyPrinterAppSettingsHandler
Imports ConnectMyPrinterLanguageHelper
Imports ConnectMyPrinterPrinterManageLib

Module Module1

    Public MLangHelper As New LanguageApplyHelper
    Public MCultureInf As CultureInfo = CultureInfo.CurrentUICulture

    Sub Main()
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterClean.TranslatedStrings", GetType(AppContext), MCultureInf, "LoadEnvStr", ""))

        Dim ElevationHelper As New ElevationHelperClass
        Dim PrinterDriverRemoverService As New PrinterDriverRemover
        Dim AppSettings As New AppSettingsClass
        Dim PrinterManageService As New ManagePrinter
        Dim AppSettingFile As String = "AppSettings.xml"
        Dim ActionFileDir As String = "PrinterActionsElv"
        Dim FormModule As Form1 = New Form1

        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterClean.TranslatedStrings", GetType(AppContext), MCultureInf, "LoadSettingsStr", ""))
        'Laden der Einstellungen für alle Benutzer
        If IO.File.Exists(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & AppSettingFile) Then
            AppSettingFile = My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & AppSettingFile
        Else
            'Laden der Einstellungen (über AppData)
            If IO.File.Exists(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & AppSettingFile) Then
                AppSettingFile = My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & AppSettingFile
            End If
        End If

        'Befehlszeilenparameter prüfen
        For Each argument In My.Application.CommandLineArgs
            If argument.StartsWith("/SETTINGS|") Then
                AppSettingFile = argument.Split("|")(1)
            End If
        Next
        AppSettings = FormModule.LoadSettings(AppSettingFile)

        'Entferne ungenutzte Treiberpakete
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterClean.TranslatedStrings", GetType(AppContext), MCultureInf, "DeleteUnusedDriverPacketsStr", ""))
        PrinterDriverRemoverService.DeleteUnusedDrivers(AppSettings.PrinterAdminPath)
        PrinterManageService.RestartPrinterService()
        PrinterDriverRemoverService.DeleteUnusedDrivers(AppSettings.PrinterAdminPath)
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterClean.TranslatedStrings", GetType(AppContext), MCultureInf, "FinishedStr", ""))
    End Sub

End Module
