Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Security.Permissions
Imports System.Windows.Forms
Imports System.Xml.Serialization
Imports ConnectMyPrinterAppSettingsHandler
Imports ConnectMyPrinterBackupApp.BackupFrm
Imports ConnectMyPrinterEnumerationLib
Imports ConnectMyPrinterLanguageHelper
Imports ConnectMyPrinterRemoteFileHandler
Imports ConnectMyPrinterReportingLib

<PermissionSet(SecurityAction.Demand, Name:="FullTrust")> Public Class AppContext
    Inherits ApplicationContext

    Public WithEvents Tray As NotifyIcon
    Public WithEvents MainMenu As ContextMenuStrip
    Public WithEvents mnuManagePrinter As ToolStripMenuItem
    Public WithEvents mnuRefresh As ToolStripMenuItem
    Public WithEvents mnuRestartPrinterService As ToolStripMenuItem
    Public WithEvents mnuBackupPrinterEnv As ToolStripMenuItem
    Public WithEvents mnuSep1 As ToolStripSeparator
    Public WithEvents mnuSep2 As ToolStripSeparator
    Public WithEvents mnuExit As ToolStripMenuItem
    Public WithEvents mnuLogo As ToolStripLabel

    Public MLangHelper As New LanguageApplyHelper
    Public MCultureInf As CultureInfo = CultureInfo.CurrentUICulture

    Public MainApp As New ConnectMyPrinter.NET.Form1
    Public LocalPrinters As New List(Of PrinterQueueInfo)
    Public PrinterManageService As New ConnectMyPrinterPrinterManageLib.ManagePrinter

    Public WithEvents TracePrinterProfileWorker As New BackgroundWorker
    Public WithEvents TracePrinterProfileWorkerStartup As New BackgroundWorker
    Public WithEvents TracePrinterProfileWatcher As New FileSystemWatcher
    Public WithEvents BackupPrinterEnvironmentWorker As New BackgroundWorker
    Public WithEvents ReportPrinterEnvironmentWorker As New BackgroundWorker
    Public WithEvents FetchPrinterEnvironmentWorker As New BackgroundWorker

    Public AppSettings As New AppSettingsClass
    Public AppSettingFile As String = "AppSettings.xml"
    Public AppSettingDEFile As String = "AppSettings_de-DE.xml"
    Public AppSettingENFile As String = "AppSettings_en-US.xml"


    Public Shared Sub Main()
        Application.EnableVisualStyles()
        Application.Run(New AppContext)
    End Sub

    Public Sub New()
        'Initialize the menus

        mnuSep1 = New ToolStripSeparator()
        mnuSep1.Tag = New PrinterQueueInfo
        mnuManagePrinter = New ToolStripMenuItem(MLangHelper.GetCultureString("ConnectMyPrinterTrayApp.TranslatedStrings", GetType(AppContext), MCultureInf, "LaunchAppEntry", ""))
        mnuManagePrinter.Image = My.Resources.manageprinters_png
        mnuManagePrinter.Tag = New PrinterQueueInfo
        mnuRestartPrinterService = New ToolStripMenuItem(MLangHelper.GetCultureString("ConnectMyPrinterTrayApp.TranslatedStrings", GetType(AppContext), MCultureInf, "RestartPrinterQ", ""))
        mnuRestartPrinterService.Image = My.Resources.restart_printerq
        mnuRestartPrinterService.Tag = New PrinterQueueInfo
        mnuBackupPrinterEnv = New ToolStripMenuItem(MLangHelper.GetCultureString("ConnectMyPrinterTrayApp.TranslatedStrings", GetType(AppContext), MCultureInf, "BackupPrintingEnv", ""))
        mnuBackupPrinterEnv.Image = My.Resources.backup_printer
        mnuBackupPrinterEnv.Tag = New PrinterQueueInfo
        mnuRefresh = New ToolStripMenuItem(MLangHelper.GetCultureString("ConnectMyPrinterTrayApp.TranslatedStrings", GetType(AppContext), MCultureInf, "RefreshView", ""))
        mnuRefresh.Image = My.Resources.refresh16
        mnuRefresh.Tag = New PrinterQueueInfo
        mnuSep2 = New ToolStripSeparator()
        mnuSep2.Tag = New PrinterQueueInfo
        mnuExit = New ToolStripMenuItem(MLangHelper.GetCultureString("ConnectMyPrinterTrayApp.TranslatedStrings", GetType(AppContext), MCultureInf, "ExitEntry", ""))
        mnuExit.Tag = New PrinterQueueInfo
        mnuExit.Image = My.Resources.exit_gray
        MainMenu = New ContextMenuStrip
        MainMenu.BackColor = Drawing.Color.White
        MainMenu.Items.AddRange(New ToolStripItem() {mnuSep1, mnuManagePrinter, mnuBackupPrinterEnv, mnuRestartPrinterService, mnuRefresh, mnuExit, mnuSep2})

        'Initialize the tray
        Tray = New NotifyIcon
        Tray.ContextMenuStrip = MainMenu
        Tray.Text = MLangHelper.GetCultureString("ConnectMyPrinterTrayApp.TranslatedStrings", GetType(AppContext), MCultureInf, "TrayIconText", "")

        'Lade Anwendungseinstellungen
        'Laden der Einstellungen für alle Benutzer
        If IO.File.Exists(Environment.SpecialFolder.LocalApplicationData & "\" & MainApp.AppSettingFile) Then
            MainApp.AppSettingFile = Environment.SpecialFolder.LocalApplicationData & "\" & MainApp.AppSettingFile
            Debug.WriteLine(Environment.SpecialFolder.LocalApplicationData & "\" & MainApp.AppSettingFile)
        Else
            'Laden der Einstellungen (über AppData)
            If IO.File.Exists(Environment.SpecialFolder.ApplicationData & "\" & MainApp.AppSettingFile) Then
                MainApp.AppSettingFile = Environment.SpecialFolder.ApplicationData & "\" & MainApp.AppSettingFile
                Debug.WriteLine(Environment.SpecialFolder.ApplicationData & "\" & MainApp.AppSettingFile)
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
        MainApp.AppSettings = MainApp.LoadSettings(AppSettingFile)
        AppSettings = MainApp.LoadSettings(AppSettingFile)

        'Display
        If AppSettings.ShowTrayAppIcon Then
            Tray.Icon = My.Resources.connectmyprinter_tray_processing_0000
            Tray.Visible = True
        End If

        'Prüfen, ob ein Verzeichnis für das Verarbeiten von Profildateien überwacht werden soll:
        If MainApp.AppSettings.UseTracePathFeature Then
            Debug.WriteLine("Use TracePath feature...")
            Try
                If Not IO.Directory.Exists(Environment.ExpandEnvironmentVariables(MainApp.AppSettings.ActionsTracePath)) Then
                    IO.Directory.CreateDirectory(Environment.ExpandEnvironmentVariables(MainApp.AppSettings.ActionsTracePath))
                End If

                TracePrinterProfileWatcher.Path = Environment.ExpandEnvironmentVariables(MainApp.AppSettings.ActionsTracePath)
                TracePrinterProfileWatcher.Filter = "*.prpr"
                TracePrinterProfileWatcher.IncludeSubdirectories = False

                If MainApp.AppSettings.ProcessActionsOnTrayStart Then
                    'Soll bei jedem Start das Verzeichnis durchsucht werden?
                    TracePrinterProfileWorkerStartup.RunWorkerAsync(Environment.ExpandEnvironmentVariables(MainApp.AppSettings.ActionsTracePath))
                End If

                TracePrinterProfileWatcher.EnableRaisingEvents = True
            Catch ex As Exception
                Debug.WriteLine(Err.Description)
            End Try
        End If

        'Prüfen, ob Drucker in Profildatei nach Start der Anwendung gesichert werden sollen:
        If MainApp.AppSettings.AutoBackupPrinterEnvironmentAtStartup And Not MainApp.AppSettings.AutoBackupPrinterEnvironmentPath = "" Then
            BackupPrinterEnvironmentWorker.RunWorkerAsync()
        End If

        'Prüfen, ob ein Report erstellt werden soll:
        If MainApp.AppSettings.UseReportingFeature Then
            ReportPrinterEnvironmentWorker.RunWorkerAsync()
        End If

        mnuLogo = New ToolStripLabel
        mnuLogo.BackgroundImageLayout = ImageLayout.Center
        mnuLogo.AutoSize = False
        mnuLogo.Tag = New PrinterQueueInfo
        Dim ii As Image
        Try
            If Not AppSettings.CompanyLogoImageBase64 = "" Then
                Try
                    Dim ByteArray
                    ByteArray = ConvertBase64ToByteArray(AppSettings.CompanyLogoImageBase64)
                    ii = convertbytetoimage(ByteArray)
                Catch ex As Exception
                    ii = My.Resources._1472877498_BT_printer.ToBitmap
                    mnuLogo.Height = My.Resources._1472877498_BT_printer.Height
                End Try
            Else
                ii = Image.FromFile(Environment.ExpandEnvironmentVariables(MainApp.AppSettings.CompanyLogoImagePath))
            End If

            mnuLogo.Height = ii.Height
        Catch ex As Exception
            ii = My.Resources._1472877498_BT_printer.ToBitmap
            mnuLogo.Height = My.Resources._1472877498_BT_printer.Height
        End Try

        mnuLogo.BackgroundImage = ii

        MainMenu.Items.Insert(0, mnuLogo)

        mnuLogo.Visible = MainApp.AppSettings.ShowCompanyLogoInTrayApp
        mnuSep1.Visible = MainApp.AppSettings.ShowCompanyLogoInTrayApp
        mnuManagePrinter.Visible = MainApp.AppSettings.ShowManagePrintersEntryInTrayApp
        mnuExit.Visible = MainApp.AppSettings.ShowExitEntryInTrayApp
        mnuRefresh.Visible = MainApp.AppSettings.ShowRefreshEntryInTrayApp
        mnuRestartPrinterService.Visible = MainApp.AppSettings.ShowRestartPrinterServiceEntryInTrayApp
        mnuBackupPrinterEnv.Visible = MainApp.AppSettings.ShowBackupPrinterEnvironmentEntryInTrayApp

        If MainApp.AppSettings.ShowClassicTrayMenuStyleInTrayApp Then
            MainMenu.RenderMode = ToolStripRenderMode.System
        End If

        If (MainApp.AppSettings.ShowExitEntryInTrayApp = False) And (MainApp.AppSettings.ShowManagePrintersEntryInTrayApp = False) And (MainApp.AppSettings.ShowRefreshEntryInTrayApp = False) Then
            mnuSep2.Visible = False
        End If

        If Environment.GetEnvironmentVariables.Count > 0 Then
            If MainApp.AppSettings.ShowTrayAppAfterInstall = False Then
                ExitApplication()
            End If
        End If

        If Not MainApp.AppSettings.AutoBackupPrinterEnvironmentAtStartup = True Then
            Tray.Icon = My.Resources.connectmyprinter_tray
        End If
    End Sub

    Public Function ConvertBase64ToByteArray(base64 As String) As Byte()
        Return Convert.FromBase64String(base64) 'Convert the base64 back to byte array.
    End Function

    Private Function convertbytetoimage(ByVal BA As Byte())
        Dim ms As MemoryStream = New MemoryStream(BA)
        Dim image = System.Drawing.Image.FromStream(ms)
        Return image
    End Function

    Public Function RaiseProfileFileApply(ByVal ProfileFile As String) As Boolean
        Try
            Dim kk As New Process
            kk.StartInfo.FileName = "ConnectMyPrinterRemoteFileHelper.exe"
            kk.StartInfo.Arguments = ProfileFile
            kk.StartInfo.WindowStyle = ProcessWindowStyle.Hidden

            kk.Start()
            kk.WaitForExit(3600)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DeleteProfileFile(ByVal ProfileFile As String, ByVal ProfileFileFullPath As String) As Boolean
        Try
            If Not ProfileFile.ToLower.Contains("permanent") Then
                IO.File.Delete(ProfileFileFullPath)
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub TracePrinterProfileWatcherChanged(source As Object, e As FileSystemEventArgs) Handles TracePrinterProfileWatcher.Created
        Try
            Debug.WriteLine("File " & e.FullPath & " detected.")
            If Not TracePrinterProfileWorker.IsBusy Then
                Dim uu As New List(Of String)
                uu.Add(e.Name)
                uu.Add(e.FullPath)

                TracePrinterProfileWorker.RunWorkerAsync(uu)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TracePrinterProfileWorkerDoWork(source As Object, e As DoWorkEventArgs) Handles TracePrinterProfileWorker.DoWork
        Try
            Dim filename As String
            Dim obj1 As List(Of String)
            obj1 = e.Argument
            filename = obj1(0)

            If filename.StartsWith(System.Environment.UserName.ToLower) Or filename.StartsWith("LM") Then
                'Gültige Datei erkannt
                RaiseProfileFileApply(obj1(1))
                DeleteProfileFile(obj1(0), obj1(1))
            End If

            If filename.StartsWith("REQ") Then
                'Generiere Profildatei und lege diese temp. ab.
                If Not FetchPrinterEnvironmentWorker.IsBusy Then
                    IO.File.Delete(filename)
                    FetchPrinterEnvironmentWorker.RunWorkerAsync()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TracePrinterProfileWorkerStartupDoWork(source As Object, e As DoWorkEventArgs) Handles TracePrinterProfileWorkerStartup.DoWork
        Dim dir As String
        dir = e.Argument

        For Each item As String In IO.Directory.GetFiles(dir, "*.prpr*", SearchOption.TopDirectoryOnly)
            Try
                Dim hh As New IO.FileInfo(item)
                If (hh.Name.StartsWith(System.Environment.UserName.ToLower) Or hh.Name.StartsWith("LM")) Then
                    'Gültige Datei erkannt
                    RaiseProfileFileApply(hh.FullName)
                    DeleteProfileFile(hh.Name, hh.FullName)
                End If
                If hh.Name.StartsWith("REQ") Then
                    'Generiere Profildatei und lege diese temp. ab.
                    If Not FetchPrinterEnvironmentWorker.IsBusy Then
                        IO.File.Delete(item)
                        FetchPrinterEnvironmentWorker.RunWorkerAsync()
                    End If
                End If
            Catch ex As Exception
            End Try
        Next
    End Sub

    Private Sub BackupPrinterEnvironmentWorkerDoWork() Handles BackupPrinterEnvironmentWorker.DoWork
        Try
            Dim dirstr As String
            dirstr = Environment.ExpandEnvironmentVariables(MainApp.AppSettings.AutoBackupPrinterEnvironmentPath)

            If Not IO.Directory.Exists(dirstr) Then
                IO.Directory.CreateDirectory(dirstr)
            End If

            Dim filenamestr As String
            filenamestr = Environment.ExpandEnvironmentVariables(MainApp.AppSettings.AutoBackupPrinterEnvironmentFilenameBegin)
            filenamestr += "_" & Environment.MachineName & ".prpr"

            If IO.File.Exists(filenamestr) Then
                IO.File.Delete(filenamestr)
            End If

            Dim LocalPrinters As List(Of PrinterQueueInfo)
            LocalPrinters = MainApp.LoadLocalPrinters()

            Dim ConnectedPrinters As New List(Of PrinterQueueInfo)

            For Each item As PrinterQueueInfo In LocalPrinters
                If AppSettings.IgnoreLocalPrintersAtAutoBackup Then
                    If (Not item.Server = "Lokal") And (Not item.Server = "Local") Then
                        ConnectedPrinters.Add(item)
                    End If
                Else
                    ConnectedPrinters.Add(item)
                End If
            Next

            Dim RemoteFileService As New RemoteFileCreator
            RemoteFileService.CreateMultiplePrinterRemoteFile(dirstr & "\" & filenamestr, ConnectedPrinters)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ReportPrinterEnvironmentWorkerDoWork() Handles ReportPrinterEnvironmentWorker.DoWork
        Try
            Dim domainstr As String
            domainstr = Environment.UserDomainName

            Dim usernamestr As String
            usernamestr = Environment.UserName

            Dim hostnamestr As String
            hostnamestr = Environment.MachineName

            Dim ReportingHelper As New ReportingLib

            If Not ReportingHelper.CheckIfUserIsBlacklisted(AppSettings, usernamestr) Then
                If ReportingHelper.CheckForFolderStructure(AppSettings, hostnamestr, usernamestr, domainstr) Then
                    ReportingHelper.SavePrinterProfileToReportingPath(AppSettings, hostnamestr, usernamestr, domainstr)
                    ReportingHelper.SavePrinterEnvironmentToCSV(AppSettings, hostnamestr, usernamestr, domainstr)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BackupRinterEnvironmentWorkerCompleted() Handles BackupPrinterEnvironmentWorker.RunWorkerCompleted
        Tray.Icon = My.Resources.connectmyprinter_tray
    End Sub

    Private Sub FetchPrinterEnvironmentWorkerDoWork() Handles FetchPrinterEnvironmentWorker.DoWork
        Try
            Dim filenamestr As String
            filenamestr = Environment.ExpandEnvironmentVariables(MainApp.AppSettings.ActionsTracePath)
            filenamestr += "\" & "RESULT.prpr"

            If IO.File.Exists(filenamestr) Then
                IO.File.Delete(filenamestr)
            End If

            Dim LocalPrinters As List(Of PrinterQueueInfo)
            LocalPrinters = MainApp.LoadLocalPrinters()

            Dim ConnectedPrinters As New List(Of PrinterQueueInfo)

            For Each item As PrinterQueueInfo In LocalPrinters
                If (Not item.Server = "Lokal") And (Not item.Server = "Local") Then
                    ConnectedPrinters.Add(item)
                End If
            Next

            Dim RemoteFileService As New RemoteFileCreator
            RemoteFileService.CreateMultiplePrinterRemoteFile(filenamestr, ConnectedPrinters)
        Catch ex As Exception
        End Try
    End Sub

    Public Function DeleteOldEntries() As Boolean
        Try
            For index = 6 To MainMenu.Items.Count - 1
                Try
                    If index >= MainMenu.Items.Count Then
                        DeleteOldEntries()
                    Else
                        Dim gg As PrinterQueueInfo
                        gg = MainMenu.Items(index).Tag
                        If gg.ShareName = "" Then
                        Else
                            MainMenu.Items.RemoveAt(index)
                        End If
                    End If
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try
            Next

            Return True
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    Public Function LoadPrintersAndAddToMenu()
        Try
            LocalPrinters = MainApp.LoadLocalPrinters()

            For index = 0 To LocalPrinters.Count - 1
                Try
                    Dim jj As New ToolStripMenuItem(LocalPrinters(index).ShareName)
                    jj.Checked = LocalPrinters(index).DefaultPrinter
                    jj.Tag = LocalPrinters(index)

                    If MainApp.AppSettings.ShowChangeDefaultPrinterDriverSettingsEntryInTrayApp Then
                        Dim settings As New ToolStripMenuItem(MLangHelper.GetCultureString("ConnectMyPrinterTrayApp.TranslatedStrings", GetType(AppContext), MCultureInf, "DefaultSettingsEntry", ""))
                        settings.Tag = LocalPrinters(index)
                        settings.Image = My.Resources.settings_small
                        AddHandler settings.Click, AddressOf ClickOnChangePrinterDefaultSettings
                        jj.DropDownItems.Add(settings)
                    End If

                    If MainApp.AppSettings.ShowDeletePrinterEntryInTrayApp Then
                        Dim delprinter As New ToolStripMenuItem(MLangHelper.GetCultureString("ConnectMyPrinterTrayApp.TranslatedStrings", GetType(AppContext), MCultureInf, "DisconnectPrinterEntry", ""))
                        delprinter.Tag = LocalPrinters(index)
                        delprinter.Image = My.Resources.DeletePrinter2
                        If MainApp.AppSettings.AllowUserDeleteLocalPrinter = False Then
                            If LocalPrinters(index).Server = "Lokal" Or LocalPrinters(index).Server = "Local" Then
                                delprinter.Enabled = False
                            End If
                        End If
                        If LocalPrinters(index).Server = "Lokal" Or LocalPrinters(index).Server = "Local" Then
                            delprinter.Text = MLangHelper.GetCultureString("ConnectMyPrinterTrayApp.TranslatedStrings", GetType(AppContext), MCultureInf, "RemoveLocalPrinterEntry", "")
                        End If
                        AddHandler delprinter.Click, AddressOf ClickOnDeletePrinterEntry
                        jj.DropDownItems.Add(delprinter)
                    End If

                    If MainApp.AppSettings.ShowOpenPrinterWebsiteEntryInTrayApp Then
                        Dim opengui As New ToolStripMenuItem(MLangHelper.GetCultureString("ConnectMyPrinterTrayApp.TranslatedStrings", GetType(AppContext), MCultureInf, "OpenDeviceWebsiteEntry", ""))
                        opengui.Tag = LocalPrinters(index)
                        If LocalPrinters(index).Server = "Lokal" Or LocalPrinters(index).Server = "Local" Then
                            opengui.Enabled = False
                        End If
                        AddHandler opengui.Click, AddressOf ClickOnPrinterWebguiEntry
                        jj.DropDownItems.Add(opengui)
                    End If

                    AddHandler jj.Click, AddressOf ClickOnPrinterEntry
                    MainMenu.Items.AddRange(New ToolStripItem() {jj})
                Catch ex As Exception
                End Try
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

    Public Sub MenuOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles MainMenu.Opening
        DeleteOldEntries()
        LocalPrinters.Clear()
        LoadPrintersAndAddToMenu()

        mnuLogo.Width = MainMenu.ClientRectangle.Width - 80
    End Sub

    Public Sub MainMenuClosing(ByVal sender As Object, ByVal e As System.EventArgs) Handles MainMenu.Closing

    End Sub

    Public Sub UnsetAllEntries()
        For index = 6 To MainMenu.Items.Count - 1
            If index >= MainMenu.Items.Count Then
            Else
                Try
                    Dim uu As ToolStripMenuItem
                    uu = MainMenu.Items(index)
                    uu.Checked = False
                Catch ex As Exception
                End Try
            End If
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

        Try
            TracePrinterProfileWatcher.EnableRaisingEvents = False
            TracePrinterProfileWatcher.Dispose()
        Catch ex As Exception
        End Try

        'Prüfen, ob Drucker in Profildatei bei dem Beenden der Trayanwendung gesichert werden sollen
        If MainApp.AppSettings.AutoBackupPrinterEnvironmentAtLogout And Not MainApp.AppSettings.AutoBackupPrinterEnvironmentPath = "" Then
            If BackupPrinterEnvironmentWorker.IsBusy = False Then
                BackupPrinterEnvironmentWorker.RunWorkerAsync()
                Dim timcnt As Integer = 0
                Do Until timcnt = 300
                    If Not BackupPrinterEnvironmentWorker.IsBusy Then
                        Exit Do
                    End If
                    Threading.Thread.Sleep(10)
                    timcnt += 1
                Loop
            End If
        End If

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

    Private Sub mnuBackupPrinterEnv_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles mnuBackupPrinterEnv.Click
        Try
            Shell("ConnectMyPrinterBackupApp.exe", AppWinStyle.NormalFocus, True)
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

