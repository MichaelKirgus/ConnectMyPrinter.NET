
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
        AppSettings = LoadSettings(AppSettingFile)
        If AppSettings.ForceAdministratorRightsOnForceDelete Then
            Dim ii As New Process
            ii.StartInfo.FileName = My.Application.Info.DirectoryPath & "\" & My.Application.Info.AssemblyName & ".exe"
            ii.StartInfo.Verb = "runas"
            Dim addcmd As String
            addcmd = ""
            Try
                addcmd = " " & My.Application.CommandLineArgs(0)
            Catch ex As Exception
            End Try
            ii.StartInfo.Arguments = addcmd
            ii.Start()
            Application.Exit()
        End If

        Dim aclhelp As New ConnectMyPrinterACLHelperLib.HelperFunctions
        If Not aclhelp.IsAdmin Then
            MsgBox("Die Anwendung wird ohne Admin-Rechte ausgeführt. Drucker können nur für den aktuellen Benutzer gelöscht werden. Dies kann zu Problemen beim Löschen Treiberpaketen sowei Treibern führen.", MsgBoxStyle.Critical)
        End If
        LoadAllItems()
    End Sub

    Public Sub LoadAllItems()
        ListView1.Items.Clear()
        ListView2.Items.Clear()

        Dim kk As New List(Of UserListClass)
        kk = RegistryHelperHandler.GetAllUsers()

        'Drucker für den aktuellen Benutzer ermitteln
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

        'Lokale Drucker ermitteln
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

        'Lokale Treiberpakete ermitteln
        Dim hh As New ConnectMyPrinterDriverPackagesLib.ManageDriverPackages
        Dim ii As List(Of ConnectMyPrinterDriverPackagesLib.DriverPackageItem)
        ii = hh.ListAllDriverPackets(True)

        For Each item As ConnectMyPrinterDriverPackagesLib.DriverPackageItem In ii
            Dim aa As New ListViewItem
            aa.Text = "[Treiberpaket] " & item.DriverName
            aa.SubItems.Add(item.DriverKeyName)
            aa.SubItems.Add(item.CabPath)
            aa.SubItems.Add(item.DriverStorePath)
            aa.Tag = item
            ListView2.Items.Add(aa)
        Next

        'Lokale Treiber ermitteln
        Dim ww As New ConnectMyPrinterDriverPackagesLib.ManageDriverPackages
        Dim uu As List(Of ConnectMyPrinterDriverPackagesLib.DriverPackageItem)
        uu = ww.GetAllDrivers()

        For Each item As ConnectMyPrinterDriverPackagesLib.DriverPackageItem In uu
            Dim aa As New ListViewItem
            aa.Text = "[Treiber] " & item.DriverName
            aa.SubItems.Add("")
            aa.SubItems.Add("")
            aa.SubItems.Add(item.DriverStorePath)
            aa.Tag = item
            ListView2.Items.Add(aa)
        Next
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        For Each ww As ListViewItem In ListView1.SelectedItems
            Dim AA As New ConnectMyPrinterForceDeleteLib.DeleteClass
            AA.DeletePrinterFromRegistry(AppSettings, ww.SubItems(1).Text, ww.Tag, True)
        Next
        LoadAllItems()
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        For Each ww As ListViewItem In ListView2.SelectedItems
            Dim AA As New ConnectMyPrinterDriverPackagesLib.ManageDriverPackages
            AA.DeleteDriverPacket(ww.Tag)
        Next
        LoadAllItems()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        For Each ww As ListViewItem In ListView2.SelectedItems
            Dim AA As New ConnectMyPrinterDriverPackagesLib.ManageDriverPackages
            AA.DeleteDriver(ww.Tag, True)
        Next
        LoadAllItems()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        For Each ww As ListViewItem In ListView2.SelectedItems
            Dim AA As New ConnectMyPrinterDriverPackagesLib.ManageDriverPackages
            AA.DeleteDriverPacket(ww.Tag)
            AA.DeleteDriver(ww.Tag, True)
        Next
        LoadAllItems()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            Me.UseWaitCursor = True
            Dim pp As New ConnectMyPrinterPrinterManageLib.ManagePrinter
            pp.RestartPrinterService()
            Me.UseWaitCursor = False
        Catch ex As Exception
        End Try
    End Sub
End Class
