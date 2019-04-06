'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.

Imports System.Globalization
Imports System.IO
Imports System.Xml.Serialization
Imports ConnectMyPrinter.NET
Imports ConnectMyPrinterAppSettingsHandler
Imports ConnectMyPrinterElevationLib
Imports ConnectMyPrinterLanguageHelper
Imports ConnectMyPrinterUserListLib

Public Class Form1
    Public MLangHelper As New LanguageApplyHelper
    Public MCultureInf As CultureInfo = CultureInfo.CurrentUICulture

    Public AppSettingFile As String = "AppSettings.xml"
    Public AppSettings As ConnectMyPrinterAppSettingsHandler.AppSettingsClass
    Public RegistryHelperHandler As New ConnectMyPrinterRegistryHandler.RegistryHandler
    Public ElevatedHelper As New ConnectMyPrinterACLHelperLib.HelperFunctions
    Public ActionRaised As Boolean = False

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        'Laden der Einstellungen (im Programmverzeichnis oder über Befehlszeile)
        AppSettings = LoadSettings(AppSettingFile)

        'Prüfen auf Administratorrechte
        If ElevatedHelper.IsAdmin = False Then
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
            If AppSettings.AllowForceDeletePrinterStartWithoutAdminRights Then
                If AppSettings.ShowForceDeletePrinterNonAdminMessageAtStart Then
                    MsgBox(MLangHelper.GetCultureString("ConnectMyPrinterForceDelete.TranslatedStrings", GetType(Form1), MCultureInf, "StartWithoutAdministrativeRightsStr", ""), MsgBoxStyle.Critical)
                End If
            Else
                MsgBox(MLangHelper.GetCultureString("ConnectMyPrinterForceDelete.TranslatedStrings", GetType(Form1), MCultureInf, "UserWithoutAdministrativeRightsDenyStr", ""), MsgBoxStyle.Exclamation)
                Application.Exit()
            End If
        Else
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
                qq.SubItems.Add(MLangHelper.GetCultureString("ConnectMyPrinterForceDelete.TranslatedStrings", GetType(Form1), MCultureInf, "ConnectedListStr", ""))
                qq.Tag = item

                ListView1.Items.Add(qq)
            Next
        Next

        'Lokale Drucker ermitteln
        Dim jj As List(Of String)
        jj = RegistryHelperHandler.GetLocalPrinters()

        For Each item As String In jj
            Dim qq As New ListViewItem
            qq.Text = MLangHelper.GetCultureString("ConnectMyPrinterForceDelete.TranslatedStrings", GetType(Form1), MCultureInf, "AllUsersListStr", "")
            qq.SubItems.Add(item)
            qq.SubItems.Add(MLangHelper.GetCultureString("ConnectMyPrinterForceDelete.TranslatedStrings", GetType(Form1), MCultureInf, "LocalInstalledStr", ""))
            qq.Tag = New UserListClass

            ListView1.Items.Add(qq)
        Next

        'Lokale Treiberpakete ermitteln
        Dim hh As New ConnectMyPrinterDriverPackagesLib.ManageDriverPackages
        Dim ii As List(Of ConnectMyPrinterDriverPackagesLib.DriverPackageItem)
        ii = hh.ListAllDriverPackets(True)

        For Each item As ConnectMyPrinterDriverPackagesLib.DriverPackageItem In ii
            Dim aa As New ListViewItem
            If item.DriverName = "" Then
                aa.Text = MLangHelper.GetCultureString("ConnectMyPrinterForceDelete.TranslatedStrings", GetType(Form1), MCultureInf, "DriverPacketNotUsedStr", "")
            Else
                aa.Text = MLangHelper.GetCultureString("ConnectMyPrinterForceDelete.TranslatedStrings", GetType(Form1), MCultureInf, "DriverPacketStr", "") & item.DriverName
            End If

            aa.SubItems.Add("")
            If Not item.CabPath = "" Then
                aa.SubItems.Add(hh.GetDriverCABFileCreationTime(item.CabPath))
            Else
                aa.SubItems.Add("")
            End If

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
            aa.Text = MLangHelper.GetCultureString("ConnectMyPrinterForceDelete.TranslatedStrings", GetType(Form1), MCultureInf, "DriverStr", "") & item.DriverName
            aa.SubItems.Add(item.DriverVersion)
            aa.SubItems.Add(item.DriverDate)
            aa.SubItems.Add(item.DriverKeyName)
            aa.SubItems.Add(item.CabPath)
            aa.SubItems.Add(item.DriverStorePath)
            aa.Tag = item
            ListView2.Items.Add(aa)
        Next

        ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
        ListView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ActionRaised = True
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
        ActionRaised = True
        For Each ww As ListViewItem In ListView2.SelectedItems
            Dim AA As New ConnectMyPrinterDriverPackagesLib.ManageDriverPackages
            AA.DeleteDriverPacket(ww.Tag)
        Next
        LoadAllItems()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ActionRaised = True
        For Each ww As ListViewItem In ListView2.SelectedItems
            Dim AA As New ConnectMyPrinterDriverPackagesLib.ManageDriverPackages
            AA.DeleteDriver(ww.Tag, True)
        Next
        LoadAllItems()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ActionRaised = True
        For Each ww As ListViewItem In ListView2.SelectedItems
            Dim AA As New ConnectMyPrinterDriverPackagesLib.ManageDriverPackages
            AA.DeleteDriverPacket(ww.Tag)
            AA.DeleteDriver(ww.Tag, True)
        Next
        LoadAllItems()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If ActionRaised Then
                Me.UseWaitCursor = True
                Shell("ConnectMyPrinterRestartSpooler.exe", AppWinStyle.NormalFocus, True)
                Me.UseWaitCursor = False
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            Dim ss As New ConnectMyPrinterPrinterManageLib.ManagePrinter
            ss.StopPrinterService()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            Dim ss As New ConnectMyPrinterPrinterManageLib.ManagePrinter
            ss.StartPrinterService()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SpoolerStoppenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SpoolerStoppenToolStripMenuItem.Click
        Try
            Dim ElevationHelper As New ElevationHelperClass
            Dim SpoolerElevationHelper As New ElevationHelper
            Dim frm As New ConnectMyPrinter.NET.Form1
            ElevationHelper.GenerateActionFile("StopPrinterService", New ConnectMyPrinterEnumerationLib.PrinterQueueInfo, frm, New PrinterCtl)
            ElevationHelper.StartElevatedActions(frm, New PrinterCtl)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SpoolerStartenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SpoolerStartenToolStripMenuItem.Click
        Try
            Dim ElevationHelper As New ElevationHelperClass
            Dim SpoolerElevationHelper As New ElevationHelper
            Dim frm As New ConnectMyPrinter.NET.Form1
            ElevationHelper.GenerateActionFile("StartPrinterService", New ConnectMyPrinterEnumerationLib.PrinterQueueInfo, frm, New PrinterCtl)
            ElevationHelper.StartElevatedActions(frm, New PrinterCtl)
        Catch ex As Exception
        End Try
    End Sub
End Class
