
Imports System.IO
Imports System.Xml.Serialization
Imports ConnectMyPrinterAppSettingsHandler
Imports Microsoft.Win32

Public Class Form1
    Public AppSettingFile As String = "AppSettings.xml"
    Public AppSettings As ConnectMyPrinterAppSettingsHandler.AppSettingsClass

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
        Dim kk As New List(Of ConnectMyPrinterForceDeleteLib.UserListClass)
        kk = GetAllUsers()

        For Each item As ConnectMyPrinterForceDeleteLib.UserListClass In kk
            For Each item2 As String In GetAllPrintersForUser(item)
                Dim qq As New ListViewItem
                qq.Text = item._Username
                qq.SubItems.Add(item2)
                qq.Tag = item

                ListView1.Items.Add(qq)
            Next
        Next
    End Sub

    Public Function GetAllUsers() As List(Of ConnectMyPrinterForceDeleteLib.UserListClass)
        Try
            Dim qq As New List(Of ConnectMyPrinterForceDeleteLib.UserListClass)
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\ProfileList", False)
            For Each item As String In ww.GetSubKeyNames
                Dim kk As New ConnectMyPrinterForceDeleteLib.UserListClass
                kk._KEY = item

                Dim pp As RegistryKey
                pp = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\ProfileList\" & kk._KEY, False)
                Dim tmpuser As String
                tmpuser = pp.GetValue("ProfileImagePath")
                kk._Username = tmpuser.Split("\")(2)
                qq.Add(kk)
            Next

            Return qq
        Catch ex As Exception
            Return New List(Of ConnectMyPrinterForceDeleteLib.UserListClass)
        End Try
    End Function

    Public Function GetAllPrintersForUser(ByVal Userinfo As ConnectMyPrinterForceDeleteLib.UserListClass) As List(Of String)
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.Users.OpenSubKey(Userinfo._KEY & "\Printers\Settings", False)

            Dim jj As New List(Of String)
            For Each item As String In ww.GetValueNames
                jj.Add(item)
            Next

            Return jj
        Catch ex As Exception
            Return New List(Of String)
        End Try
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim ww As ListViewItem
        ww = ListView1.SelectedItems(0)
        Dim UserListClassx As ConnectMyPrinterForceDeleteLib.UserListClass
        UserListClassx = ww.Tag
        Dim jj As New ConnectMyPrinterSystemRestorePointLib.CreateRestorePointClass
        jj.EnsureCreationPoint()
        jj.CreatePoint("Erzwungenes Löschen eines Druckers", True)
        Dim aa As New ConnectMyPrinterPrinterManageLib.PrinterDriverRemover
        Dim bb As New ConnectMyPrinterPrinterManageLib.ManagePrinter
        bb.CancelAllPrintJobs(ww.SubItems(1).Text)
        bb.ResetPrinter(ww.SubItems(1).Text)
        Dim SpoolerHelper As New ConnectMyPrinterPrinterManageLib.ManagePrinter
        SpoolerHelper.StopPrinterService()
        Dim qq As New ConnectMyPrinterForceDeleteManageLib.ForcePrinterDelete
        qq.DeletePrinter(ww.SubItems(1).Text, UserListClassx._KEY)
        SpoolerHelper.StartPrinterService()
        aa.DeleteUnusedDrivers(AppSettings.PrinterAdminPath)

        MsgBox("Der Drucker wurde erfolgreich gelöscht.", MsgBoxStyle.Information)
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
