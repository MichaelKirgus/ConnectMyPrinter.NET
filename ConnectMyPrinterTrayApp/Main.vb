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
    Private WithEvents mnuRefresh As ToolStripMenuItem
    Private WithEvents mnuRestartPrinterService As ToolStripMenuItem
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
        mnuRestartPrinterService = New ToolStripMenuItem("Druckerwarteschlange neu starten")
        mnuRefresh = New ToolStripMenuItem("Ansicht aktualisieren")
        mnuSep2 = New ToolStripSeparator()
        mnuExit = New ToolStripMenuItem("Beenden")
        MainMenu = New ContextMenuStrip
        MainMenu.BackColor = Drawing.Color.White
        MainMenu.Items.AddRange(New ToolStripItem() {mnuSep1, mnuManagePrinter, mnuRestartPrinterService, mnuRefresh, mnuExit, mnuSep2})

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
        mnuRefresh.Visible = MainApp.AppSettings.ShowRefreshEntryInTrayApp
        mnuRestartPrinterService.Visible = MainApp.AppSettings.ShowRestartPrinterServiceEntryInTrayApp

        If MainApp.AppSettings.ShowClassicTrayMenuStyleInTrayApp Then
            MainMenu.RenderMode = ToolStripRenderMode.System
        End If

        If (MainApp.AppSettings.ShowExitEntryInTrayApp = False) And (MainApp.AppSettings.ShowManagePrintersEntryInTrayApp = False) And (MainApp.AppSettings.ShowRefreshEntryInTrayApp = False) Then
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
                If MainApp.AppSettings.ShowChangeDefaultPrinterDriverSettingsEntryInTrayApp Then
                    Dim settings As New ToolStripMenuItem("Standardeinstellungen...")
                    settings.Tag = item
                    AddHandler settings.Click, AddressOf ClickOnChangePrinterDefaultSettings
                    jj.DropDownItems.Add(settings)
                End If

                If MainApp.AppSettings.ShowDeletePrinterEntryInTrayApp Then
                    Dim delprinter As New ToolStripMenuItem("Drucker löschen")
                    delprinter.Tag = item
                    If MainApp.AppSettings.AllowUserDeleteLocalPrinter = False Then
                        If item.Server = "Lokal" Then
                            delprinter.Enabled = False
                        End If
                    End If
                    AddHandler delprinter.Click, AddressOf ClickOnDeletePrinterEntry
                    jj.DropDownItems.Add(delprinter)
                End If

                If MainApp.AppSettings.ShowOpenPrinterWebsiteEntryInTrayApp Then
                    Dim opengui As New ToolStripMenuItem("Gerätewebseite öffnen...")
                    opengui.Tag = item
                    If item.Server = "Lokal" Then
                        opengui.Enabled = False
                    End If
                    AddHandler opengui.Click, AddressOf ClickOnPrinterWebguiEntry
                    jj.DropDownItems.Add(opengui)
                End If

                AddHandler jj.Click, AddressOf ClickOnPrinterEntry
                MainMenu.Items.AddRange(New ToolStripItem() {jj})
                AddHandler MainMenu.Opening, AddressOf MenuOpen
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub RestartMyself()
        Dim aa As String = "cmd.exe /C timeout 1 && start " & My.Resources.String1 & Application.StartupPath & My.Resources.String1 & " " & My.Resources.String1 & Application.ExecutablePath & My.Resources.String1
        Shell(aa, AppWinStyle.Hide, False)
    End Sub

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

    Private Sub ClickOnPrinterWebguiEntry(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim qq As ToolStripMenuItem
            qq = sender
            Dim ff As PrinterQueueInfo
            ff = qq.Tag

            Dim hh As New Process
            hh.StartInfo.FileName = "http://" & ff.ShareName
            hh.Start()
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

    Private Sub mnuRestartPrinterService_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles mnuRestartPrinterService.Click
        Try
            Shell("ConnectMyPrinterRestartSpooler.exe", AppWinStyle.NormalFocus, True)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub mnuRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles mnuRefresh.Click
        RestartMyself()
        ExitApplication()
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

