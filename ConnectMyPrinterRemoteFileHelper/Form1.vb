Imports System.Globalization
Imports ConnectMyPrinterAppSettingsHandler
Imports ConnectMyPrinterLanguageHelper
Imports ConnectMyPrinterRemoteFileHandler

Public Class Form1
    Public MLangHelper As New LanguageApplyHelper
    Public MCultureInf As CultureInfo = CultureInfo.CurrentUICulture

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
            kk = MsgBox(MLangHelper.GetCultureString("ConnectMyPrinterRemoteFileHelper.TranslatedStrings", GetType(Form1), MCultureInf, "PrinterAlreadyAddedStr", ""), MsgBoxStyle.YesNo)

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
                MsgBox(MLangHelper.GetCultureString("ConnectMyPrinterRemoteFileHelper.TranslatedStrings", GetType(Form1), MCultureInf, "PrinterPushFeatureNotEnabledStr", ""), MsgBoxStyle.Exclamation)
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
                        filename = "\\" & clientlist(index) & Environment.ExpandEnvironmentVariables(AppSettings.ActionsTraceAdminPath) & "\LM_" & uuid & "_" & options & ".prpr"
                    Else
                        filename = "\\" & clientlist(index) & Environment.ExpandEnvironmentVariables(AppSettings.ActionsTraceAdminPath) & "\" & Username & "_" & uuid & "_" & options & ".prpr"
                    End If

                    If IO.Directory.Exists("\\" & clientlist(index) & AppSettings.ActionsTraceAdminPath) Then
                        If Not IO.File.Exists(filename) Then
                            Dim yy As New RemoteFileSerializer
                            If yy.SaveRemoteFile(RemoteFile, filename) Then
                                LogTxt += MLangHelper.GetCultureString("ConnectMyPrinterRemoteFileHelper.TranslatedStrings", GetType(Form1), MCultureInf, "ProfilePublishedSuccessStr1", "") & clientlist(index) & MLangHelper.GetCultureString("ConnectMyPrinterRemoteFileHelper.TranslatedStrings", GetType(Form1), MCultureInf, "ProfilePublishedSuccessStr2", "") & vbNewLine
                            Else
                                LogTxt += MLangHelper.GetCultureString("ConnectMyPrinterRemoteFileHelper.TranslatedStrings", GetType(Form1), MCultureInf, "ProfilePublishedFailStr1", "") & clientlist(index) & MLangHelper.GetCultureString("ConnectMyPrinterRemoteFileHelper.TranslatedStrings", GetType(Form1), MCultureInf, "ProfilePublishedFailStr2", "") & vbNewLine
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

    Public Function RequestPrinterProfileFromClient(ByVal Clientname As String) As Boolean
        Try
            Dim uuid As String
            uuid = Guid.NewGuid.ToString.Replace("{", "").Replace("}", "")

            IO.File.WriteAllText("\\" & Clientname & Environment.ExpandEnvironmentVariables(AppSettings.ActionsTraceAdminPath) & "\REQ_" & uuid & "_.prpr", "")

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetRequestedProfileFromClient(ByVal Clientname As String) As Boolean
        Try
            If IO.File.Exists("\\" & Clientname & "\" & Environment.ExpandEnvironmentVariables(AppSettings.ActionsTraceAdminPath) & "\RESULT.prpr") Then
                Dim yy As New RemoteFileSerializer
                RemoteFile = yy.LoadRemoteFile("\\" & Clientname & Environment.ExpandEnvironmentVariables(AppSettings.ActionsTraceAdminPath) & "\RESULT.prpr")
                PropertyGrid1.SelectedObject = Nothing
                PropertyGrid1.SelectedObject = RemoteFile

                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function LoadPrinterProfileFromClient(ByVal Clientname As String) As Boolean
        Me.UseWaitCursor = True
        Application.DoEvents()
        RequestPrinterProfileFromClient(Clientname)
        Dim counter As Integer = 0
        Do Until (IO.File.Exists("\\" & Clientname & Environment.ExpandEnvironmentVariables(AppSettings.ActionsTraceAdminPath) & "\RESULT.prpr")) Or (counter = 500)
            Application.DoEvents()
            Threading.Thread.Sleep(10)
            counter += 1
        Loop
        If Not counter = 500 Then
            Threading.Thread.Sleep(250)
            If GetRequestedProfileFromClient(Clientname) Then
                IO.File.Delete("\\" & Clientname & Environment.ExpandEnvironmentVariables(AppSettings.ActionsTraceAdminPath) & "\RESULT.prpr")
                Me.UseWaitCursor = False
                Return True
            End If
        End If

        Me.UseWaitCursor = False
        Application.DoEvents()
        Return False
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
            Dim translatedstr As String
            translatedstr = MLangHelper.GetCultureString("ConnectMyPrinterRemoteFileHelper.TranslatedStrings", GetType(Form1), MCultureInf, "SendPrinterProfileNameStr", "")
            tmpfilename = My.Computer.FileSystem.SpecialDirectories.Temp & "\" & translatedstr & ".prpr"
            If IO.File.Exists(tmpfilename) Then
                IO.File.Delete(tmpfilename)
            End If

            yy.SaveRemoteFile(RemoteFile, tmpfilename)
            Dim MailHelper As New ConnectMyPrinterOutlookHelper.OutlookHelperClass
            MailHelper.SendOutlookMail(translatedstr, "", tmpfilename)
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

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        LoadPrinterProfileFromClient(TextBox5.Text)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        LoadPrinterProfileFromClient(TextBox5.Text)
        ToolStripButton2.PerformClick()
    End Sub
End Class
