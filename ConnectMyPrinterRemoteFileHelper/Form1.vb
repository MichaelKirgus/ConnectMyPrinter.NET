Imports ConnectMyPrinterAppSettingsHandler
Imports ConnectMyPrinterRemoteFileHandler

Public Class Form1
    Public RemoteFile As New RemoteFileClass
    Public MainApp As New ConnectMyPrinter.NET.Form1
    Public AppSettings As New AppSettingsClass
    Public AppSettingFile As String = "AppSettings.xml"
    Public AppSettingsHandler As ConnectMyPrinterAppSettingsHandler.AppSettingsClass

    Public Sub LoadNewRemoteFile()
        PropertyGrid1.SelectedObject = RemoteFile
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim RemoteFilePath As String = ""
            For Each argument In My.Application.CommandLineArgs
                RemoteFilePath = argument
            Next
            If IO.File.Exists(RemoteFilePath) Then
                'Aktionen ausführen, Anwendung nicht im Audit-Modus öffnen...
                Dim kk As New ProcessDlg
                kk.RemoteFilePath = RemoteFilePath
                kk.Show()
                Me.Close()
            End If

            LoadSettingsFile()
            LoadNewRemoteFile()
        Catch ex As Exception
        End Try
    End Sub

    Public Sub LoadSettingsFile()
        'Lade Anwendungseinstellungen
        'Laden der Einstellungen für alle Benutzer
        If IO.File.Exists(Environment.SpecialFolder.LocalApplicationData & "\" & AppSettingFile) Then
            AppSettingFile = Environment.SpecialFolder.LocalApplicationData & "\" & AppSettingFile
            Debug.WriteLine(Environment.SpecialFolder.LocalApplicationData & "\" & AppSettingFile)
        Else
            'Laden der Einstellungen (über AppData)
            If IO.File.Exists(Environment.SpecialFolder.ApplicationData & "\" & AppSettingFile) Then
                AppSettingFile = Environment.SpecialFolder.ApplicationData & "\" & AppSettingFile
                Debug.WriteLine(Environment.SpecialFolder.ApplicationData & "\" & AppSettingFile)
            End If
        End If

        'Laden der Einstellungen (im Programmverzeichnis oder über Befehlszeile)
        AppSettings = MainApp.LoadSettings(AppSettingFile)
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        Dim yy As New RemoteFileSerializer
        RemoteFile = yy.LoadRemoteFile(OpenFileDialog1.FileName)
    End Sub

    Private Sub SaveFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        Dim yy As New RemoteFileSerializer
        yy.SaveRemoteFile(RemoteFile, SaveFileDialog1.FileName)
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        SaveFileDialog1.ShowDialog()
    End Sub

    Public Sub ApplyPrinterConfigToClass()
        If Not RemoteFile.ConnectPrinters.Count = 0 Then
            Dim kk As MsgBoxResult
            kk = MsgBox("Es ist bereits ein oder mehrere Drucker in der Konfiguration vorhanden. Drucker überschreiben?", MsgBoxStyle.YesNo)

            If kk = MsgBoxResult.Yes Then
                RemoteFile.ConnectPrinters.Clear()
            End If
        End If

        Dim qq As New RemoteFilePrinterConnectItem
        qq.PrinterName = TextBox2.Text
        qq.Printserver = TextBox1.Text
        qq.SetDefaultPrinter = CheckBox1.Checked

        RemoteFile.ConnectPrinters.Add(qq)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ApplyPrinterConfigToClass()
        LoadNewRemoteFile()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ApplyPrinterConfigToClass()
        LoadNewRemoteFile()
        ToolStripButton2.PerformClick()
    End Sub

    Public Function PublishProfileToClient(ByVal Clientname As String, ByVal Username As String, ByVal LocalMachine As Boolean, ByVal Permanent As Boolean) As Boolean
        Try
            If Not AppSettings.UseTracePathFeature Then
                MsgBox("Diese Funktion ist in der Einstellungsdatei clientseitig nicht aktiviert.", MsgBoxStyle.Exclamation)
                Return False
            End If

            Dim clientlist As New List(Of String)
            If Clientname.Contains(";") Then
                Dim spliarr As Array
                spliarr = Clientname.Split(";")
                clientlist.AddRange(spliarr)
            Else
                clientlist.Add(Clientname)
            End If

            Dim uuid As String
            uuid = Guid.NewGuid.ToString.Replace("{", "").Replace("}", "")

            Dim LogTxt As String = ""

            For index = 0 To clientlist.Count - 1
                If Not clientlist(index) = "" Then
                    Dim filename As String
                    Dim options As String = ""
                    If Permanent Then
                        options = "permanent"
                    End If
                    If LocalMachine Then
                        filename = "\\" & clientlist(index) & AppSettings.ActionsTraceAdminPath & "\LM_" & uuid & "_" & options & ".prpr"
                    Else
                        filename = "\\" & clientlist(index) & AppSettings.ActionsTraceAdminPath & "\" & Username & "_" & uuid & "_" & options & ".prpr"
                    End If

                    If IO.Directory.Exists("\\" & clientlist(index) & AppSettings.ActionsTraceAdminPath) Then
                        If Not IO.File.Exists(filename) Then
                            Dim yy As New RemoteFileSerializer
                            If yy.SaveRemoteFile(RemoteFile, filename) Then
                                LogTxt += "Profildatei erfolgreich auf Client " & clientlist(index) & " veröffentlicht." & vbNewLine
                            Else
                                LogTxt += "Die Profildatei konnte nicht auf Client " & clientlist(index) & " geschrieben werden. Bitte Hostname überprüfen." & vbNewLine
                            End If
                        End If
                    End If
                End If
            Next

            MsgBox(LogTxt, MsgBoxStyle.Information)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ApplyPrinterConfigToClass()
        LoadNewRemoteFile()
        PublishProfileToClient(TextBox3.Text, TextBox4.Text, CheckBox2.Checked, CheckBox3.Checked)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            ApplyPrinterConfigToClass()
            LoadNewRemoteFile()
            Dim yy As New RemoteFileSerializer
            Dim tmpfilename As String
            tmpfilename = My.Computer.FileSystem.SpecialDirectories.Temp & "\Druckerprofil.prpr"
            If IO.File.Exists(tmpfilename) Then
                IO.File.Delete(tmpfilename)
            End If

            yy.SaveRemoteFile(RemoteFile, tmpfilename)
            Dim MailHelper As New ConnectMyPrinterOutlookHelper.OutlookHelperClass
            MailHelper.SendOutlookMail("Druckerprofil", "", tmpfilename)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            TextBox4.Enabled = False
        Else
            TextBox4.Enabled = True
        End If
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        If ToolStripButton3.Checked Then
            SplitContainer1.Panel2Collapsed = False
            Me.Height = 520
        Else
            SplitContainer1.Panel2Collapsed = True
            Me.Height = 250
        End If
    End Sub
End Class
