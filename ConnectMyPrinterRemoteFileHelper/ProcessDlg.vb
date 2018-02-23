Imports System.IO
Imports System.Windows.Forms
Imports System.Xml.Serialization
Imports ConnectMyPrinterAppSettingsHandler
Imports ConnectMyPrinterEnumerationLib
Imports ConnectMyPrinterRemoteFileHandler

Public Class ProcessDlg
    Public RemoteFilePath As String = ""
    Public Profile As New RemoteFileClass
    Public DefaultAppSettings As New AppSettingsClass

    Private Sub ProcessDlg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim kk As New RemoteFileSerializer
            Dim xProfile As RemoteFileClass
            xProfile = kk.LoadRemoteFile(RemoteFilePath)
            Profile = xProfile

            If IO.File.Exists(xProfile.CustomAppSettingsFile) Then
                DefaultAppSettings = LoadSettings(xProfile.CustomAppSettingsFile)
            End If

            BackgroundWorker1.RunWorkerAsync()
        Catch ex As Exception
        End Try
    End Sub

    Public Function ExecAction(ByVal ActionItem As RemoteFileActions) As Boolean
        Try
            If ActionItem.Action = RemoteFileActionsEnums.ActionEnum.CleanUnusedDrivers Then
                'unbenutzte Treiberpakete löschen
                Dim aa As New ConnectMyPrinterPrinterManageLib.PrinterDriverRemover
                aa.DeleteUnusedDrivers(DefaultAppSettings.PrinterAdminPath)
                Return True
            End If
            If ActionItem.Action = RemoteFileActionsEnums.ActionEnum.ResetPrinterSettings Then
                'Druckereinstellungen zurücksetzen
                Dim aa As New ConnectMyPrinterPrinterManageLib.ManagePrinter
                Dim jj As New PrinterQueueInfo
                jj.ShareName = ActionItem.CustomData
                aa.DeleteDevModeSettings(jj)
                aa.DeleteDevMode2Settings(jj)
                Return True
            End If
            If ActionItem.Action = RemoteFileActionsEnums.ActionEnum.RestartSpooler Then
                'Spooler neu starten
                Dim aa As New ConnectMyPrinterPrinterManageLib.ManagePrinter
                aa.RestartPrinterService()
                Return True
            End If
            If ActionItem.Action = RemoteFileActionsEnums.ActionEnum.RunExecCommand Then
                'Benutzerdefinierter Befehl ausführen
                Shell(ActionItem.CustomData)
                Return True
            End If
            If ActionItem.Action = RemoteFileActionsEnums.ActionEnum.ShowMessage Then
                'Hinweismeldung anzeigen
                Me.TopMost = False
                MsgBox(ActionItem.CustomData, MsgBoxStyle.Information)
                Me.TopMost = True
                Return True
            End If
            If ActionItem.Action = RemoteFileActionsEnums.ActionEnum.StartSpooler Then
                'Spooler starten
                Dim aa As New ConnectMyPrinterPrinterManageLib.ManagePrinter
                aa.StartPrinterService()
                Return True
            End If
            If ActionItem.Action = RemoteFileActionsEnums.ActionEnum.StopSpooler Then
                'Spooler stoppen
                Dim aa As New ConnectMyPrinterPrinterManageLib.ManagePrinter
                aa.StopPrinterService()
                Return True
            End If
            If ActionItem.Action = RemoteFileActionsEnums.ActionEnum.WaitForMilliseconds Then
                'Benutzerdefinierte Zeit (in Millisekunden) warten
                Threading.Thread.Sleep(ActionItem.CustomData)
                Return True
            End If
            If ActionItem.Action = RemoteFileActionsEnums.ActionEnum.SetDefaultPrinter Then
                'Standarddrucker ändern
                Try
                    Dim uu As New ConnectMyPrinterPrinterManageLib.ManagePrinter
                    Dim qq As New PrinterQueueInfo
                    qq.ShareName = ActionItem.PrinterName
                    qq.Name = ActionItem.PrinterName
                    qq.Server = "\\" & ActionItem.PrintServer

                    uu.SetDefaultPrinter(qq)
                Catch ex As Exception
                End Try
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            'Verarbeite Preactions
            For Each item As RemoteFileActions In Profile.Preactions
                ExecAction(item)
            Next

            'Drucker trennen
            For Each item As RemoteFilePrinterDisconnectItem In Profile.DisconnectPrinters
                Try
                    Dim uu As New ConnectMyPrinterPrinterManageLib.ManagePrinter
                    Dim qq As New PrinterQueueInfo
                    qq.ShareName = item.PrinterName
                    qq.Name = item.PrinterName
                    qq.Server = "\\" & item.PrintServer
                    uu.DeletePrinter(qq)
                Catch ex As Exception
                End Try
            Next

            'Zwischenaktionen verarbeiten
            For Each item As RemoteFileActions In Profile.IntermediateActions
                ExecAction(item)
            Next

            'Drucker verbinden
            For Each item As RemoteFilePrinterConnectItem In Profile.ConnectPrinters
                Try
                    Dim uu As New ConnectMyPrinterPrinterManageLib.ManagePrinter
                    Dim qq As New PrinterQueueInfo
                    qq.ShareName = item.PrinterName
                    qq.Server = "\\" & item.Printserver
                    qq.Name = item.PrinterName

                    Shell("rundll32 printui.dll PrintUIEntry /in /n \\" & item.Printserver & "\" & item.PrinterName, AppWinStyle.Hide, True, 5000)

                    If item.SetDefaultPrinter Then
                        uu.SetDefaultPrinter(qq)
                    End If
                Catch ex As Exception
                End Try
            Next

            'Verarbeite Postactions
            For Each item As RemoteFileActions In Profile.Postactions
                ExecAction(item)
            Next
        Catch ex As Exception
        End Try
    End Sub

    Public Function LoadSettings(ByVal Filename As String) As AppSettingsClass
        'Diese Funktion lädt die Einstellungen der Anwendung

        Try
            Dim objStreamReader As New StreamReader(Filename)
            Dim p2 As New AppSettingsClass
            Dim x As New XmlSerializer(p2.GetType)
            p2 = x.Deserialize(objStreamReader)
            objStreamReader.Close()

            Return p2
        Catch ex As Exception
            Return New AppSettingsClass
        End Try
    End Function

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Application.Exit()
    End Sub
End Class
