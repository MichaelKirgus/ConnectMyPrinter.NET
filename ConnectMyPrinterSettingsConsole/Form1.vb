﻿Imports System.IO
Imports System.Xml.Serialization
Imports ConnectMyPrinterAppSettingsHandler

Public Class Form1

    Public AppSettings As New AppSettingsClass
    Public AppSettingFile As String = "AppSettings.xml"

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

    Public Function SaveSettings(ByVal Settingsx As AppSettingsClass, ByVal Filename As String) As Boolean
        'Diese Funktion speichert die Einstellungen der Anwendung

        Try
            Dim XML As New XmlSerializer(Settingsx.GetType)
            Dim FS As New FileStream(Filename, FileMode.Create)
            XML.Serialize(FS, Settingsx)
            FS.Close()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub XLoadSettings(Optional ByVal File As String = "")
        Try
            If File = "" Then
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

                AppSettings = LoadSettings(AppSettingFile)
            Else
                AppSettings = LoadSettings(AppSettingFile)
            End If

            ToolStripTextBox1.Text = AppSettingFile
            PropertyGrid1.SelectedObject = AppSettings
        Catch ex As Exception
        End Try
    End Sub


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        XLoadSettings()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim jj As MsgBoxResult
        jj = MsgBox("Möchten Sie die Änderungen speichern?", MsgBoxStyle.YesNo)
        If jj = MsgBoxResult.Yes Then
            SaveSettings(AppSettings, AppSettingFile)
        End If
    End Sub

    Private Sub DateiÖffnenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DateiÖffnenToolStripMenuItem.Click
        OpenFileDialog1.ShowDialog()
        If Not OpenFileDialog1.FileName = "" Then
            AppSettingFile = OpenFileDialog1.FileName
            XLoadSettings(AppSettingFile)
        End If
    End Sub

    Private Sub DateiSpeichernToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DateiSpeichernToolStripMenuItem.Click
        SaveFileDialog1.ShowDialog()
        If Not SaveFileDialog1.FileName = "" Then
            SaveSettings(AppSettings, SaveFileDialog1.FileName)
        End If
    End Sub
End Class
