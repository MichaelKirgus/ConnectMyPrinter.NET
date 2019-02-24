﻿Imports System.Globalization
Imports ConnectMyPrinterAppSettingsHandler
Imports ConnectMyPrinterDistributionLib
Imports ConnectMyPrinterLanguageHelper
Imports ConnectMyPrinterRemoteFileHandler

Public Class Form1
    Public MLangHelper As New LanguageApplyHelper
    Public MCultureInf As CultureInfo = CultureInfo.CurrentUICulture

    Public RemoteFile As New RemoteFileClass
    Public DistHelper As New DistributionHelper
    Public MainApp As New ConnectMyPrinter.NET.Form1
    Public AppSettings As New AppSettingsClass
    Public AppSettingFile As String = "AppSettings.xml"
    Public AppSettingDEFile As String = "AppSettings_de-DE.xml"
    Public AppSettingENFile As String = "AppSettings_en-US.xml"
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
            Else
                'Es liegen keine Einstellungen in den App-Data-Ordnern.
                'Prüfen, on lokalisierte Anwendungseinstellungen im Anwendungsordner liegen:
                If MCultureInf.IetfLanguageTag.Contains("de") Then
                    If IO.File.Exists(AppSettingDEFile) Then
                        AppSettingFile = AppSettingDEFile
                    End If
                End If
                If MCultureInf.IetfLanguageTag.Contains("en") Then
                    If IO.File.Exists(AppSettingDEFile) Then
                        AppSettingFile = AppSettingENFile
                    End If
                End If
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
            Else
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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        DistHelper.PublishProfileToClient(TextBox3.Text, TextBox4.Text, CheckBox2.Checked, CheckBox3.Checked, RemoteFile, AppSettings)
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

        Dim result As New RemoteFileClass
        result = DistHelper.LoadPrinterProfileFromClient(TextBox5.Text, AppSettings)
        If Not result.ConnectPrinters.Count = 0 Then
            TextBox5.BackColor = Color.LightGreen
        Else
            TextBox5.BackColor = Color.LightCoral
        End If

        RemoteFile = result
        PropertyGrid1.SelectedObject = Nothing
        PropertyGrid1.SelectedObject = RemoteFile

        Button6.Enabled = True
        Me.UseWaitCursor = False
        Application.DoEvents()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TextBox5.BackColor = Color.LightGray
        Dim result As New RemoteFileClass
        result = DistHelper.LoadPrinterProfileFromClient(TextBox5.Text, AppSettings)
        If Not result.ConnectPrinters.Count = 0 Then
            TextBox5.BackColor = Color.LightGreen
        Else
            TextBox5.BackColor = Color.LightCoral
        End If
        RemoteFile = result
        PropertyGrid1.SelectedObject = Nothing
        PropertyGrid1.SelectedObject = RemoteFile
        ToolStripButton2.PerformClick()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        ApplyPrinterConfigToClass()
        LoadNewRemoteFile()
        DistHelper.PublishProfileToClient(TextBox3.Text, TextBox4.Text, CheckBox2.Checked, CheckBox3.Checked, RemoteFile, AppSettings)
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

            Dim result As New RemoteFileClass
            result = DistHelper.LoadPrinterProfileFromClient(TextBox6.Text, AppSettings)

            If Not result.ConnectPrinters.Count = 0 Then
                For index = 0 To clientcoll.Count - 1
                    If DistHelper.PublishProfileToClient(clientcoll(index), TextBox4.Text, CheckBox2.Checked, CheckBox3.Checked, result, AppSettings) Then
                        LogTxt += MLangHelper.GetCultureString("ConnectMyPrinterRemoteFileHelper.TranslatedStrings", GetType(Form1), MCultureInf, "ProfilePublishedSuccessStr1", "") & clientcoll(index) & MLangHelper.GetCultureString("ConnectMyPrinterRemoteFileHelper.TranslatedStrings", GetType(Form1), MCultureInf, "ProfilePublishedSuccessStr2", "") & vbNewLine
                    Else
                        LogTxt += MLangHelper.GetCultureString("ConnectMyPrinterRemoteFileHelper.TranslatedStrings", GetType(Form1), MCultureInf, "ProfilePublishedFailStr1", "") & clientcoll(index) & MLangHelper.GetCultureString("ConnectMyPrinterRemoteFileHelper.TranslatedStrings", GetType(Form1), MCultureInf, "ProfilePublishedFailStr2", "") & vbNewLine
                    End If
                Next
            End If

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

    Private Sub ToolStripButton4_ButtonClick(sender As Object, e As EventArgs) Handles ToolStripButton4.ButtonClick
        ToolStripButton4.ShowDropDown()
    End Sub
End Class
