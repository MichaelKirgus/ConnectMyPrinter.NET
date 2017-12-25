
Imports System.IO
Imports System.Xml.Serialization
Imports ConnectMyPrinterAppSettingsHandler
Imports ConnectMyPrinterUserListLib
Imports Microsoft.Win32

Public Class Form1
    Public AppSettingFile As String = "AppSettings.xml"
    Public AppSettings As ConnectMyPrinterAppSettingsHandler.AppSettingsClass
    Public RegistryHelperHandler As New ConnectMyPrinterRegistryHandler.RegistryHandler

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Laden der Einstellungen (über AppData)
        If IO.File.Exists(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & AppSettingFile) Then
            AppSettingFile = My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & AppSettingFile
        End If

        'Befehlszeilenparameter prüfen
        For Each argument In My.Application.CommandLineArgs
            If argument.StartsWith("/SETTINGS|") Then
                AppSettingFile = argument.Split("|")(1)
            End If
        Next

        'Laden der Einstellungen (im Programmverzeichnis oder über Befehlszeile)
        If Not IO.File.Exists(AppSettingFile) Then
            SaveSettings(AppSettings, AppSettingFile)
        End If
        AppSettings = LoadSettings(AppSettingFile)
        SaveSettings(AppSettings, AppSettingFile)

        Dim aclhelp As New ConnectMyPrinterACLHelperLib.HelperFunctions
        If Not aclhelp.IsAdmin Then
            MsgBox("Die Anwendung wird ohne Admin-Rechte ausgeführt. Drucker können nur für den aktuellen Benutzer gelöscht werden. Dies kann zu Problemen beim Beenden der Druckerwarteschlange führen.", MsgBoxStyle.Critical)
        End If
        Dim kk As New List(Of UserListClass)
        kk = RegistryHelperHandler.GetAllUsers()

        For Each item As UserListClass In kk
            For Each item2 As String In RegistryHelperHandler.GetAllPrintersForUser(item)
                Dim qq As New ListViewItem
                qq.Text = item._Username
                qq.SubItems.Add(item2)
                qq.SubItems.Add("verbunden")
                qq.Tag = item

                ListView1.Items.Add(qq)
            Next
        Next

        Dim jj As List(Of String)
        jj = RegistryHelperHandler.GetLocalPrinters()

        For Each item As String In jj
            Dim qq As New ListViewItem
            qq.Text = "Alle Benutzer"
            qq.SubItems.Add(item)
            qq.SubItems.Add("lokal installiert")
            qq.Tag = New UserListClass

            ListView1.Items.Add(qq)
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim kk As MsgBoxResult
        kk = MsgBox("Achtung: Bitte schließen Sie alle nicht gespeicherten Arbeiten bzw. Dokumente, da für die korrekte Löschung des Druckers einige Anwendungen geschlossen werden müssen. Bestätigen Sie mit JA, wenn Sie alle Daten gespeichert haben.", MsgBoxStyle.YesNo)

        If kk = MsgBoxResult.No Then
            MsgBox("Vorgang abgebrochen.", MsgBoxStyle.Information)
        End If
        If kk = MsgBoxResult.Yes Then
            Dim ww As ListViewItem
            ww = ListView1.SelectedItems(0)

            Dim AA As New ConnectMyPrinterForceDeleteLib.DeleteClass
            AA.DeletePrinterFromRegistry(AppSettings, ww.SubItems(1).Text, ww.Tag, True)

            MsgBox("Der Drucker wurde erfolgreich gelöscht. " & AA.DeletedKeyCount & " Löschungen durchgeführt.", MsgBoxStyle.Information)
        End If
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
End Class
