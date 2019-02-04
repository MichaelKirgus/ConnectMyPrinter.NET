﻿Imports System.Globalization
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
                If argument.StartsWith(My.Resources.TranslatedStrings.trenn) Then
                    RemoteFilePath = argument.Replace(My.Resources.TranslatedStrings.trenn, "")
                Else
                    RemoteFilePath = argument
                End If
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
            If Not RemoteFile.ConnectPrinters(0).PrinterName = TextBox2.Text Then
                Dim kk As MsgBoxResult
                kk = MsgBox(MLangHelper.GetCultureString("ConnectMyPrinterRemoteFileHelper.TranslatedStrings", GetType(Form1), MCultureInf, "PrinterAlreadyAddedStr", ""), MsgBoxStyle.YesNo)

                If kk = MsgBoxResult.Yes Then
                    RemoteFile.ConnectPrinters.Clear()
                End If
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
            If IO.File.Exists("\\" & Clientname & Environment.ExpandEnvironmentVariables(AppSettings.ActionsTraceAdminPath) & "\RESULT.prpr") Then
                Debug.WriteLine("File exists")
                Dim yy As New RemoteFileSerializer
                RemoteFile = yy.LoadRemoteFile("\\" & Clientname & Environment.ExpandEnvironmentVariables(AppSettings.ActionsTraceAdminPath) & "\RESULT.prpr")

                If AppSettings.IgnoreLocalPrintersAtRemoteFetching Then
                    If Not RemoteFile.ConnectPrinters.Count = 0 Then
                        Dim newcoll As New List(Of RemoteFilePrinterConnectItem)

                        For Each item As RemoteFilePrinterConnectItem In RemoteFile.ConnectPrinters
                            If (Not item.Printserver = "Lokal") And (Not item.Printserver = "Local") And (Not item.Printserver.ToLower = Clientname.ToLower) Then
                                newcoll.Add(item)
                            End If
                        Next

                        RemoteFile.ConnectPrinters.Clear()
                        RemoteFile.ConnectPrinters = newcoll
                    End If
                End If

                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CleanOldRequestFiles(ByVal Clientname As String) As Boolean
        Try
            For Each item As String In IO.Directory.GetFiles("\\" & Clientname & Environment.ExpandEnvironmentVariables(AppSettings.ActionsTraceAdminPath))
                Dim kk As New IO.FileInfo(item)
                If kk.Name.StartsWith("REQ") Then
                    IO.File.Delete(item)
                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function LoadPrinterProfileFromClient(ByVal Clientname As String) As Boolean
        Me.UseWaitCursor = True
        Application.DoEvents()
        RequestPrinterProfileFromClient(Clientname)
        Dim counter As Integer = 0
        Debug.WriteLine("\\" & Clientname & Environment.ExpandEnvironmentVariables(AppSettings.ActionsTraceAdminPath) & "\RESULT.prpr")

        Do Until counter = 500
            Application.DoEvents()
            Threading.Thread.Sleep(10)
            If IO.File.Exists("\\" & Clientname & Environment.ExpandEnvironmentVariables(AppSettings.ActionsTraceAdminPath) & "\RESULT.prpr") Then
                Exit Do
            End If
            counter += 1
        Loop
        If Not counter = 500 Then
            Threading.Thread.Sleep(150)
            If GetRequestedProfileFromClient(Clientname) Then
                IO.File.Delete("\\" & Clientname & Environment.ExpandEnvironmentVariables(AppSettings.ActionsTraceAdminPath) & "\RESULT.prpr")
                CleanOldRequestFiles(Clientname)
                Me.UseWaitCursor = False
                Return True
            End If
        End If

        Me.UseWaitCursor = False
        Application.DoEvents()
        Return False
    End Function

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        PublishProfileToClient(TextBox3.Text, TextBox4.Text, CheckBox2.Checked, CheckBox3.Checked)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ApplyPrinterConfigToClass()
        LoadNewRemoteFile()
        SendClassFileWithMail()
    End Sub

    Public Function SendClassFileWithMail() As Boolean
        Try
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

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

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
            Me.Height = 550
            SplitContainer1.SplitterDistance = 185
        Else
            SplitContainer1.Panel2Collapsed = True
            Me.Height = 250
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Button6.Enabled = False
        Me.UseWaitCursor = True
        Application.DoEvents()

        TextBox5.BackColor = Color.LightGray
        If LoadPrinterProfileFromClient(TextBox5.Text) Then
            TextBox5.BackColor = Color.LightGreen
        Else
            TextBox5.BackColor = Color.LightCoral
        End If
        PropertyGrid1.SelectedObject = Nothing
        PropertyGrid1.SelectedObject = RemoteFile

        Button6.Enabled = False
        Me.UseWaitCursor = False
        Application.DoEvents()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TextBox5.BackColor = Color.LightGray
        If LoadPrinterProfileFromClient(TextBox5.Text) Then
            TextBox5.BackColor = Color.LightGreen
        Else
            TextBox5.BackColor = Color.LightCoral
        End If
        PropertyGrid1.SelectedObject = Nothing
        PropertyGrid1.SelectedObject = RemoteFile
        ToolStripButton2.PerformClick()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        ApplyPrinterConfigToClass()
        LoadNewRemoteFile()
        PublishProfileToClient(TextBox3.Text, TextBox4.Text, CheckBox2.Checked, CheckBox3.Checked)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Button8.Enabled = False
        TextBox6.Enabled = False
        TextBox7.Enabled = False
        Me.UseWaitCursor = True
        Application.DoEvents()

        Try
            If TextBox4.Text = "" Then
                CheckBox2.Checked = True
            End If

            Dim clientcoll As New List(Of String)

            If TextBox7.Text.Contains(";") Then
                clientcoll.AddRange(TextBox7.Text.Split(";"))
            Else
                clientcoll.Add(TextBox7.Text)
            End If

            Dim LogTxt As String = ""
            For index = 0 To clientcoll.Count - 1
                If LoadPrinterProfileFromClient(TextBox6.Text) Then
                    If PublishProfileToClient(clientcoll(index), TextBox4.Text, CheckBox2.Checked, CheckBox3.Checked) Then
                        LogTxt += MLangHelper.GetCultureString("ConnectMyPrinterRemoteFileHelper.TranslatedStrings", GetType(Form1), MCultureInf, "ProfilePublishedSuccessStr1", "") & clientcoll(index) & MLangHelper.GetCultureString("ConnectMyPrinterRemoteFileHelper.TranslatedStrings", GetType(Form1), MCultureInf, "ProfilePublishedSuccessStr2", "") & vbNewLine
                    Else
                        LogTxt += MLangHelper.GetCultureString("ConnectMyPrinterRemoteFileHelper.TranslatedStrings", GetType(Form1), MCultureInf, "ProfilePublishedFailStr1", "") & clientcoll(index) & MLangHelper.GetCultureString("ConnectMyPrinterRemoteFileHelper.TranslatedStrings", GetType(Form1), MCultureInf, "ProfilePublishedFailStr2", "") & vbNewLine
                    End If
                End If
            Next

            MsgBox(LogTxt, MsgBoxStyle.Information)
        Catch ex As Exception
        End Try

        Button8.Enabled = True
        TextBox6.Enabled = True
        TextBox7.Enabled = True
        Me.UseWaitCursor = False
        Application.DoEvents()
    End Sub

    Private Sub LöscheAlleZuVerbindenendenDruckerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LöscheAlleZuVerbindenendenDruckerToolStripMenuItem.Click
        RemoteFile.ConnectPrinters.Clear()
        PropertyGrid1.SelectedObject = Nothing
        PropertyGrid1.SelectedObject = RemoteFile
    End Sub

    Private Sub SetzeProfilZurückToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetzeProfilZurückToolStripMenuItem.Click
        RemoteFile = New RemoteFileClass
        PropertyGrid1.SelectedObject = Nothing
        PropertyGrid1.SelectedObject = RemoteFile
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        LoadNewRemoteFile()
        SendClassFileWithMail()
    End Sub

    Public Sub ResetGUI()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        CheckBox1.Checked = False
        CheckBox2.Checked = False
        CheckBox3.Checked = False
    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        ResetGUI()
    End Sub
End Class
