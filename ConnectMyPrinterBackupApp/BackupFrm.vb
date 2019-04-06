'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports System.Globalization
Imports System.Windows.Forms
Imports ConnectMyPrinter.NET
Imports ConnectMyPrinterAppSettingsHandler
Imports ConnectMyPrinterEnumerationLib
Imports ConnectMyPrinterLanguageHelper
Imports ConnectMyPrinterPrinterManageLib
Imports ConnectMyPrinterRemoteFileHandler

Public Class BackupFrm
    Public MLangHelper As New LanguageApplyHelper
    Public MCultureInf As CultureInfo = CultureInfo.CurrentUICulture

    Dim AppSettings As New AppSettingsClass
    Dim PrinterManageService As New ManagePrinter
    Dim RemoteFileService As New RemoteFileCreator
    Dim PrinterExportImportService As New ExportImportPrinterSettings
    Dim AppSettingFile As String = "AppSettings.xml"
    Dim ActionFileDir As String = "PrinterActionsElv"
    Dim FormModule As Form1 = New Form1

    Dim BackupFilePath As String = ""
    Dim PrinterSettingsBackupPath As String = ""

    Dim Silent As Boolean = False
    Dim ShowNotifys As Boolean = False

    Private Sub BackupFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            If argument.StartsWith("/NOTIFY") Then
                ShowNotifys = True
            End If
        Next
        FormModule.AppSettings = FormModule.LoadSettings(AppSettingFile)

        Try
            BackupFilePath = My.Application.CommandLineArgs(0)
        Catch ex As Exception
        End Try
        Try
            PrinterSettingsBackupPath = My.Application.CommandLineArgs(1)
        Catch ex As Exception
        End Try

        Dim BWork As New BackupWorker

        If BackupFilePath = "" Then
            'Es wurde keine Backupdatei übergeben, Auswahl für Profildatei anzeigen
            SaveFileDialog1.ShowDialog()

            If SaveFileDialog1.FileName = "" Then
                Application.Exit()
            End If

            Dim hh As MsgBoxResult
            hh = MsgBox(MLangHelper.GetCultureString("ConnectMyPrinterBackupApp.TranslatedStrings", GetType(BackupFrm), MCultureInf, "ExportPrinterSettingsMessageStr", ""), MsgBoxStyle.YesNo)

            If hh = MsgBoxResult.Yes Then
                FolderBrowserDialog1.ShowDialog()
            End If

            Me.WindowState = FormWindowState.Normal
            Me.Height += 35
            Me.Width += 5
            Application.DoEvents()

            If Not SaveFileDialog1.FileName = "" Then
                BWork.BackupFilePath = SaveFileDialog1.FileName
                BWork.PrinterSettingsFolder = FolderBrowserDialog1.SelectedPath
                'Es wurde eine Datei angegeben, sichern...
                BackgroundWorker1.RunWorkerAsync(BWork)
            Else
                Application.Exit()
            End If
        Else
            'Es wurde eine Datei für die Sicherung übergeben, führe alle Aktionen ohne GUI aus...
            Silent = True
            BWork.BackupFilePath = BackupFilePath
            BWork.PrinterSettingsFolder = PrinterSettingsBackupPath

            If ShowNotifys Then
                NotifyIcon1.Visible = True
                NotifyIcon1.ShowBalloonTip(1000, MLangHelper.GetCultureString("ConnectMyPrinterBackupApp.TranslatedStrings", GetType(BackupFrm), MCultureInf, "PrintingEnvSaveTitleStr", ""),
                                           MLangHelper.GetCultureString("ConnectMyPrinterBackupApp.TranslatedStrings", GetType(BackupFrm), MCultureInf, "PrintingEnvSaveTextStr", ""), ToolTipIcon.Info)
            End If

            BackgroundWorker1.RunWorkerAsync(BWork)
        End If
    End Sub

    Public Class BackupWorker
        Public BackupFilePath As String = ""
        Public PrinterSettingsFolder As String = ""
    End Class

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            Dim BWork As BackupWorker
            BWork = e.Argument

            Dim LocalPrinters As List(Of PrinterQueueInfo)
            LocalPrinters = FormModule.LoadLocalPrinters()

            Dim ConnectedPrinters As New List(Of PrinterQueueInfo)

            For Each item As PrinterQueueInfo In LocalPrinters
                If (Not item.Server = "Lokal") Or (Not item.Server = "Local") Then
                    ConnectedPrinters.Add(item)
                End If
            Next

            Dim Success As Boolean
            Success = RemoteFileService.CreateMultiplePrinterRemoteFile(BWork.BackupFilePath, ConnectedPrinters)

            'Prüfen, ob zusätzlich alle Druckereinstellungen serialisiert werden sollen
            If Not BWork.PrinterSettingsFolder = "" Then
                For Each item As PrinterQueueInfo In LocalPrinters
                    item.Name = item.ShareName

                    Try
                        PrinterExportImportService.ExportPrinterSettings(item, BWork.PrinterSettingsFolder & "\" & item.ShareName & ".dat")
                    Catch ex As Exception
                    End Try
                Next
            End If

            If Success = False Then
                e.Result = -1
            Else
                e.Result = ConnectedPrinters.Count
            End If
        Catch ex As Exception
            e.Result = -1
        End Try
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Try
            If ShowNotifys Then
                NotifyIcon1.ShowBalloonTip(1000, MLangHelper.GetCultureString("ConnectMyPrinterBackupApp.TranslatedStrings", GetType(BackupFrm), MCultureInf, "PrintingEnvSaveTitleStr", ""),
                                           MLangHelper.GetCultureString("ConnectMyPrinterBackupApp.TranslatedStrings", GetType(BackupFrm), MCultureInf, "PrintingEnvSaveFinishedTextStr", ""), ToolTipIcon.Info)
            End If

            If Silent = False Then
                Me.TopMost = False
                If e.Result = 0 Then
                    MsgBox(MLangHelper.GetCultureString("ConnectMyPrinterBackupApp.TranslatedStrings", GetType(BackupFrm), MCultureInf, "NoPrinterToSaveFoundStr", ""), MsgBoxStyle.Exclamation)
                    Exit Try
                End If
                If e.Result = -1 Then
                    MsgBox(MLangHelper.GetCultureString("ConnectMyPrinterBackupApp.TranslatedStrings", GetType(BackupFrm), MCultureInf, "PrinterSavingFailStr", ""), MsgBoxStyle.Critical)
                Else
                    MsgBox(MLangHelper.GetCultureString("ConnectMyPrinterBackupApp.TranslatedStrings", GetType(BackupFrm), MCultureInf, "PrinterSavingFinishedStr", "") & e.Result, MsgBoxStyle.Information)
                End If
            End If
        Catch ex As Exception
        End Try

        NotifyIcon1.Visible = False
        Application.Exit()
    End Sub
End Class
