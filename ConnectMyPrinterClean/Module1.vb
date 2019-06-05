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

    Sub Main()
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterClean.TranslatedStrings", GetType(Module1), MCultureInf, "LoadEnvStr", ""))

        Dim ElevationHelper As New ElevationHelperClass
        Dim PrinterDriverRemoverService As New PrinterDriverRemover
        Dim AppSettings As New AppSettingsClass
        Dim PrinterManageService As New ManagePrinter
        Dim AppSettingFile As String = "AppSettings.xml"
        Dim ActionFileDir As String = "PrinterActionsElv"
        Dim FormModule As Form1 = New Form1

        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterClean.TranslatedStrings", GetType(Module1), MCultureInf, "LoadSettingsStr", ""))
        'Korrekte AppSettings-Datei laden
        Dim MUIHelper As New MUISettingsHandler
        AppSettingFile = MUIHelper.GetAppSettingsFilePath(True)

        'Befehlszeilenparameter prüfen
        For Each argument In My.Application.CommandLineArgs
            If argument.StartsWith("/SETTINGS|") Then
                AppSettingFile = argument.Split("|")(1)
            End If
        Next
        AppSettings = FormModule.LoadSettings(AppSettingFile)

        'Entferne ungenutzte Treiberpakete
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterClean.TranslatedStrings", GetType(Module1), MCultureInf, "DeleteUnusedDriverPacketsStr", ""))
        PrinterDriverRemoverService.DeleteUnusedDrivers(AppSettings.PrinterAdminPath)
        PrinterManageService.RestartPrinterService()
        PrinterDriverRemoverService.DeleteUnusedDrivers(AppSettings.PrinterAdminPath)
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterClean.TranslatedStrings", GetType(Module1), MCultureInf, "FinishedStr", ""))
    End Sub

End Module
