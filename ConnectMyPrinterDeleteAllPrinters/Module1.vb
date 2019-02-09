Imports System.Globalization
Imports ConnectMyPrinter.NET
Imports ConnectMyPrinterAppSettingsHandler
Imports ConnectMyPrinterLanguageHelper
Imports ConnectMyPrinterPrinterManageLib

Module Module1
    Public MLangHelper As New LanguageApplyHelper
    Public MCultureInf As CultureInfo = CultureInfo.CurrentUICulture

    Dim AdminCheck As New ConnectMyPrinterACLHelperLib.HelperFunctions
    Dim ElevationHelper As New ElevationHelperClass
    Dim PrinterDriverRemoverService As New PrinterDriverRemover
    Dim AppSettings As New AppSettingsClass
    Dim PrinterManageService As New ManagePrinter
    Dim AppSettingFile As String = "AppSettings.xml"
    Dim ActionFileDir As String = "PrinterActionsElv"
    Dim FormModule As Form1 = New Form1

    Sub Main()
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterDeleteAllPrinters.TranslatedStrings", GetType(AppContext), MCultureInf, "LoadEnvStr", ""))
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterDeleteAllPrinters.TranslatedStrings", GetType(AppContext), MCultureInf, "LoadSettingsFileStr", ""))
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

        'Prüfen, ob aktueller User Adminrechte hat...
        Try
            If AdminCheck.IsAdmin = False Then
                If AppSettings.AllowDeleteAllPrintersStartWithoutAdminRights = False Then
                    Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterDeleteAllPrinters.TranslatedStrings", GetType(AppContext), MCultureInf, "ExecWithoutAdminStr", ""))
                Else
                    DeleteAllPrinters()
                    Exit Sub
                End If
            Else
                DeleteAllPrinters()
            End If
        Catch ex As Exception
            'Es ist zu einem Fehler kommen, da der Benutzer keine Adminrechte hat.

            If AppSettings.AllowDeleteAllPrintersStartWithoutAdminRights = False Then
                Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterDeleteAllPrinters.TranslatedStrings", GetType(AppContext), MCultureInf, "ExecWithoutAdminStr", ""))
            Else
                DeleteAllPrinters()
                Exit Sub
            End If
        End Try
    End Sub

    Sub DeleteAllPrinters()
        'Entferne ungenutzte Treiberpakete
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterDeleteAllPrinters.TranslatedStrings", GetType(AppContext), MCultureInf, "Step1Str", ""))
        PrinterManageService.RestartPrinterService()
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterDeleteAllPrinters.TranslatedStrings", GetType(AppContext), MCultureInf, "Step2Str", ""))
        PrinterDriverRemoverService.DeleteUnusedDrivers(AppSettings.PrinterAdminPath)
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterDeleteAllPrinters.TranslatedStrings", GetType(AppContext), MCultureInf, "Step3Str", ""))
        PrinterDriverRemoverService.DeleteAllPrintersAndDrivers(AppSettings.PrinterAdminPath)
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterDeleteAllPrinters.TranslatedStrings", GetType(AppContext), MCultureInf, "Step4Str", ""))
        PrinterManageService.RestartPrinterService()
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterDeleteAllPrinters.TranslatedStrings", GetType(AppContext), MCultureInf, "Step5Str", ""))
        PrinterDriverRemoverService.DeleteUnusedDrivers(AppSettings.PrinterAdminPath)
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterDeleteAllPrinters.TranslatedStrings", GetType(AppContext), MCultureInf, "FinishedStepStr", ""))
    End Sub
End Module
