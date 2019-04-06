'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
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
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterDeleteAllPrinters.TranslatedStrings", GetType(Module1), MCultureInf, "LoadEnvStr", ""))
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterDeleteAllPrinters.TranslatedStrings", GetType(Module1), MCultureInf, "LoadSettingsFileStr", ""))
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
                    Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterDeleteAllPrinters.TranslatedStrings", GetType(Module1), MCultureInf, "ExecWithoutAdminStr", ""))
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
                Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterDeleteAllPrinters.TranslatedStrings", GetType(Module1), MCultureInf, "ExecWithoutAdminStr", ""))
            Else
                DeleteAllPrinters()
                Exit Sub
            End If
        End Try
    End Sub

    Sub DeleteAllPrinters()
        'Entferne ungenutzte Treiberpakete
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterDeleteAllPrinters.TranslatedStrings", GetType(Module1), MCultureInf, "Step1Str", ""))
        PrinterManageService.RestartPrinterService()
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterDeleteAllPrinters.TranslatedStrings", GetType(Module1), MCultureInf, "Step2Str", ""))
        PrinterDriverRemoverService.DeleteUnusedDrivers(AppSettings.PrinterAdminPath)
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterDeleteAllPrinters.TranslatedStrings", GetType(Module1), MCultureInf, "Step3Str", ""))
        PrinterDriverRemoverService.DeleteAllPrintersAndDrivers(AppSettings.PrinterAdminPath)
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterDeleteAllPrinters.TranslatedStrings", GetType(Module1), MCultureInf, "Step4Str", ""))
        PrinterManageService.RestartPrinterService()
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterDeleteAllPrinters.TranslatedStrings", GetType(Module1), MCultureInf, "Step5Str", ""))
        PrinterDriverRemoverService.DeleteUnusedDrivers(AppSettings.PrinterAdminPath)
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterDeleteAllPrinters.TranslatedStrings", GetType(Module1), MCultureInf, "FinishedStepStr", ""))
    End Sub
End Module
