'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports System.Globalization
Imports System.IO
Imports System.Xml.Serialization
Imports ConnectMyPrinterACLHelperLib
Imports ConnectMyPrinterAppSettingsHandler
Imports ConnectMyPrinterLanguageHelper

Public Class Form1
    Public MLangHelper As New LanguageApplyHelper
    Public MCultureInf As CultureInfo = CultureInfo.CurrentUICulture

    Public AppSettings As New AppSettingsClass
    Public AppSettingFile As String = "AppSettings.xml"
    Public AllowStart As Boolean = True

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
                'Korrekte AppSettings-Datei laden
                Dim MUIHelper As New MUISettingsHandler
                AppSettingFile = MUIHelper.GetAppSettingsFilePath(True)

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

            'Prüfen, ob Anwender ohne administrative Rechte Konsole öffnen darf:
            Dim HelperFunc As New HelperFunctions

            If AppSettings.AllowUserToChangeSettingsOnlyWithElevatedRights Then
                If Not HelperFunc.IsAdmin Then
                    AllowStart = False
                    Dim ii As New Process
                    ii.StartInfo.FileName = My.Application.Info.DirectoryPath & "\" & My.Application.Info.AssemblyName & ".exe"
                    ii.StartInfo.Verb = "runas"
                    ii.Start()
                End If
            End If

            If AllowStart Then
                ToolStripTextBox1.Text = AppSettingFile
                PropertyGrid1.SelectedObject = AppSettings
            Else
                Me.Enabled = False
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        XLoadSettings()
        If AllowStart = False Then
            Application.Exit()
        End If
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If AllowStart Then
            Dim jj As MsgBoxResult
            jj = MsgBox(MLangHelper.GetCultureString("ConnectMyPrinterSettingsConsole.TranslatedStrings", GetType(Form1), MCultureInf, "SaveSettingsStr", ""), MsgBoxStyle.YesNo)
            If jj = MsgBoxResult.Yes Then
                If Not SaveSettings(AppSettings, AppSettingFile) Then
                    MsgBox(MLangHelper.GetCultureString("ConnectMyPrinterSettingsConsole.TranslatedStrings", GetType(Form1), MCultureInf, "SaveSettingsErrorStr", ""), MsgBoxStyle.Exclamation)
                    e.Cancel = True
                End If
            End If
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
            If Not SaveSettings(AppSettings, SaveFileDialog1.FileName) Then
                MsgBox(MLangHelper.GetCultureString("ConnectMyPrinterSettingsConsole.TranslatedStrings", GetType(Form1), MCultureInf, "SaveSettingsErrorStr", ""), MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub
End Class
