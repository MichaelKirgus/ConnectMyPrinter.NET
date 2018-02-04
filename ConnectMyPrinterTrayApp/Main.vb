Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports System.Xml.Serialization
Imports ConnectMyPrinterAppSettingsHandler
Imports ConnectMyPrinterEnumerationLib

Public Class AppContext

    Inherits ApplicationContext

    Private WithEvents Tray As NotifyIcon
    Private WithEvents MainMenu As ContextMenuStrip
    Private WithEvents mnuManagePrinter As ToolStripMenuItem
    Private WithEvents mnuSep1 As ToolStripSeparator
    Private WithEvents mnuSep2 As ToolStripSeparator
    Private WithEvents mnuExit As ToolStripMenuItem
    Private WithEvents mnuLogo As ToolStripLabel

    Public MainApp As New ConnectMyPrinter.NET.Form1
    Public LocalPrinters As New List(Of PrinterQueueInfo)
    Public PrinterManageService As New ConnectMyPrinterPrinterManageLib.ManagePrinter

    Public AppSettings As New AppSettingsClass
    Public AppSettingFile As String = "AppSettings.xml"


    Public Shared Sub Main()
        Application.EnableVisualStyles()
        Application.Run(New AppContext)
    End Sub

    Public Sub New()
        'Initialize the menus

        mnuSep1 = New ToolStripSeparator()
        mnuManagePrinter = New ToolStripMenuItem("Drucker verwalten...")
        mnuSep2 = New ToolStripSeparator()
        mnuExit = New ToolStripMenuItem("Beenden")
        MainMenu = New ContextMenuStrip
        MainMenu.BackColor = Drawing.Color.White
        MainMenu.Items.AddRange(New ToolStripItem() {mnuSep1, mnuManagePrinter, mnuExit, mnuSep2})

        'Initialize the tray
        Tray = New NotifyIcon
        Tray.Icon = My.Resources.connectmyprinter_tray
        Tray.ContextMenuStrip = MainMenu
        Tray.Text = "ConnectMyPrinter.NET - Drucker"

        'Display
        Tray.Visible = True

        'Lade Anwendungseinstellungen
        'Laden der Einstellungen für alle Benutzer
        If IO.File.Exists(Environment.SpecialFolder.LocalApplicationData & "\" & AppSettingFile) Then
            AppSettingFile = Environment.SpecialFolder.LocalApplicationData & "\" & AppSettingFile
        Else
            'Laden der Einstellungen (über AppData)
            If IO.File.Exists(Environment.SpecialFolder.ApplicationData & "\" & AppSettingFile) Then
                AppSettingFile = Environment.SpecialFolder.ApplicationData & "\" & AppSettingFile
            End If
        End If

        'Laden der Einstellungen (im Programmverzeichnis oder über Befehlszeile)
        MainApp.AppSettings = MainApp.LoadSettings(AppSettingFile)

        mnuLogo = New ToolStripLabel
        mnuLogo.BackgroundImageLayout = ImageLayout.Center
        mnuLogo.AutoSize = False
        Dim ii As Image
        Try
            ii = Image.FromFile(MainApp.AppSettings.CompanyLogoImagePath)
        Catch ex As Exception
            ii = My.Resources._1472877498_BT_printer.ToBitmap
        End Try

        mnuLogo.BackgroundImage = ii
        mnuLogo.Height = ii.Height

        MainMenu.Items.Insert(0, mnuLogo)

        mnuLogo.Visible = MainApp.AppSettings.ShowCompanyLogoInTrayApp
        mnuSep1.Visible = MainApp.AppSettings.ShowCompanyLogoInTrayApp
        mnuManagePrinter.Visible = MainApp.AppSettings.ShowManagePrintersEntryInTrayApp
        mnuExit.Visible = MainApp.AppSettings.ShowExitEntryInTrayApp

        If (MainApp.AppSettings.ShowExitEntryInTrayApp = False) And (MainApp.AppSettings.ShowManagePrintersEntryInTrayApp = False) Then
            mnuSep2.Visible = False
        End If

        LoadPrintersAndAddToMenu()
    End Sub

    Public Function LoadPrintersAndAddToMenu()
        Try
            LocalPrinters = MainApp.LoadLocalPrinters()

            For Each item As PrinterQueueInfo In LocalPrinters
                Dim jj As New ToolStripMenuItem(item.ShareName)
                jj.Checked = item.DefaultPrinter
                jj.Tag = item
                Dim settings As New ToolStripMenuItem("Standardeinstellungen...")
                settings.Tag = item
                AddHandler settings.Click, AddressOf ClickOnChangePrinterDefaultSettings
                Dim delprinter As New ToolStripMenuItem("Drucker löschen")
                delprinter.Tag = item
                If MainApp.AppSettings.AllowUserDeleteLocalPrinter = False Then
                    If item.Server = "Lokal" Then
                        delprinter.Enabled = False
                    End If
                End If
                AddHandler delprinter.Click, AddressOf ClickOnDeletePrinterEntry
                jj.DropDownItems.Add(settings)
                jj.DropDownItems.Add(delprinter)
                AddHandler jj.Click, AddressOf ClickOnPrinterEntry
                MainMenu.Items.AddRange(New ToolStripItem() {jj})
                AddHandler MainMenu.Opening, AddressOf MenuOpen
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub MenuOpen(ByVal sender As Object, ByVal e As System.EventArgs)
        mnuLogo.Width = MainMenu.ClientRectangle.Width - 80
    End Sub

    Public Sub UnsetAllEntries()
        For Each item As ToolStripItem In MainMenu.Items
            Try
                Dim uu As ToolStripMenuItem
                uu = item
                uu.Checked = False
            Catch ex As Exception
            End Try
        Next
    End Sub

    Private Sub ClickOnPrinterEntry(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            UnsetAllEntries()
            Dim qq As ToolStripMenuItem
            qq = sender
            PrinterManageService.SetDefaultPrinter(qq.Tag)
            qq.Checked = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ClickOnChangePrinterDefaultSettings(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim qq As ToolStripMenuItem
            qq = sender
            PrinterManageService.ShowPrinterDriverSettings(qq.Tag)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ClickOnDeletePrinterEntry(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim qq As ToolStripMenuItem
            qq = sender
            PrinterManageService.DeletePrinter(qq.Tag)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub AppContext_ThreadExit(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles Me.ThreadExit
        'Guarantees that the icon will not linger.
        Tray.Visible = False
    End Sub

    Private Sub mnuDisplayForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles mnuManagePrinter.Click
        StartMainApp()
    End Sub

    Public Function StartMainApp() As Boolean
        Try
            Dim jj As New Process
            jj.StartInfo.FileName = "ConnectMyPrinter.NET.exe"
            jj.StartInfo.WorkingDirectory = Application.StartupPath
            jj.StartInfo.Arguments = "/FROMTRAY"

            jj.Start()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub mnuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles mnuExit.Click
        ExitApplication()
    End Sub

    Private Sub Tray_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles Tray.DoubleClick
        If MainApp.AppSettings.DoubleClickOnTrayIconStartsMainApp Then
            StartMainApp()
        End If
    End Sub
End Class

