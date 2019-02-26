Imports System.Globalization
Imports System.IO
Imports System.Xml.Serialization
Imports ConnectMyPrinterAppSettingsHandler
Imports ConnectMyPrinterLanguageHelper

Public Class Form1
    Public MLangHelper As New LanguageApplyHelper
    Public MCultureInf As CultureInfo = CultureInfo.CurrentUICulture

    Public AppSettings As New AppSettingsClass
    Public AppSettingDEFile As String = "AppSettings_de-DE.xml"
    Public AppSettingENFile As String = "AppSettings_en-US.xml"
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
        jj = MsgBox(MLangHelper.GetCultureString("ConnectMyPrinterSettingsConsole.TranslatedStrings", GetType(Form1), MCultureInf, "SaveSettingsStr", ""), MsgBoxStyle.YesNo)
        If jj = MsgBoxResult.Yes Then
            If Not SaveSettings(AppSettings, AppSettingFile) Then
                MsgBox(MLangHelper.GetCultureString("ConnectMyPrinterSettingsConsole.TranslatedStrings", GetType(Form1), MCultureInf, "SaveSettingsErrorStr", ""), MsgBoxStyle.Exclamation)
                e.Cancel = True
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
