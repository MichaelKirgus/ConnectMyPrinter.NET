Imports System.Windows.Forms
Imports ConnectMyPrinter.NET
Imports ConnectMyPrinterAppSettingsHandler
Imports ConnectMyPrinterEnumerationLib
Imports ConnectMyPrinterPrinterManageLib
Imports ConnectMyPrinterRemoteFileHandler

Public Class BackupFrm

    Dim AppSettings As New AppSettingsClass
    Dim PrinterManageService As New ManagePrinter
    Dim RemoteFileService As New RemoteFileCreator
    Dim AppSettingFile As String = "AppSettings.xml"
    Dim ActionFileDir As String = "PrinterActionsElv"
    Dim FormModule As Form1 = New Form1
    Dim Silent As Boolean = False

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
        Next
        AppSettings = FormModule.LoadSettings(AppSettingFile)

        Dim BackupFilePath As String = ""
        Dim PrinterSettingsBackupPath As String = ""
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

            Dim hh As MsgBoxResult
            hh = MsgBox("Möchten Sie zusätzlich die Druckereinstellungen exportieren?", MsgBoxStyle.YesNo)

            If hh = MsgBoxResult.Yes Then
                FolderBrowserDialog1.ShowDialog()
                If Not FolderBrowserDialog1.SelectedPath = "" Then
                    BWork.PrinterSettingsFolder = FolderBrowserDialog1.SelectedPath
                End If
            End If

            If Not SaveFileDialog1.FileName = "" Then
                BWork.BackupFilePath = SaveFileDialog1.FileName
                'Es wurde eine Datei angegeben, sichern...
                BackgroundWorker1.RunWorkerAsync(BWork)
            End If
        Else
            'Es wurde eine Datei für die Sicherung übergeben, führe alle Aktionen ohne GUI aus...
            Silent = True
            BWork.BackupFilePath = BackupFilePath
            BWork.PrinterSettingsFolder = PrinterSettingsBackupPath

            BackgroundWorker1.RunWorkerAsync(BWork)
            Me.Hide()
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
                If Not item.ShareName = "" Then
                    ConnectedPrinters.Add(item)
                End If
            Next

            Dim Success As Boolean
            Success = RemoteFileService.CreateMultiplePrinterRemoteFile(BWork.BackupFilePath, ConnectedPrinters)

            'Prüfen, ob zusätzlich alle Druckereinstellungen serialisiert werden sollen
            If Not BWork.PrinterSettingsFolder = "" Then
                Dim jj As New ExportImportPrinterSettings

                For Each item As PrinterQueueInfo In LocalPrinters
                    Try
                        If item.ShareName = "" Then
                            jj.ExportPrinterSettings(item, BWork.PrinterSettingsFolder & "\" & item.Name & ".dat")
                        Else
                            jj.ExportPrinterSettings(item, BWork.PrinterSettingsFolder & "\" & item.ShareName & ".dat")
                        End If
                    Catch ex As Exception
                        MsgBox(Err.Description)
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
            If Silent = False Then
                If e.Result = 0 Then
                    MsgBox("Es konnten keine Drucker gefunden werden, welche in einer Profildatei gespeichert werden könnten. Bitte beachten Sie, dass keine lokalen Drucker gesichert werden können.", MsgBoxStyle.Exclamation)
                    Exit Try
                End If
                If e.Result = -1 Then
                        MsgBox("Die Drucker konnten nicht in eine Profildatei gesichert werden!", MsgBoxStyle.Critical)
                    Else
                        MsgBox("Es wurden erfolgreich " & e.Result & " Drucker in eine Profildatei gesichert. Die Drucker werden mit einem Doppelklick auf die Profildatei automatisch verbunden.", MsgBoxStyle.Information)
                    End If
                End If
        Catch ex As Exception
        End Try

        Application.Exit()
    End Sub
End Class
