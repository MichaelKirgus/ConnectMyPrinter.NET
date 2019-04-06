'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports ConnectMyPrinter.NET
Imports ConnectMyPrinterAppSettingsHandler
Imports ConnectMyPrinterDistributionLib
Imports ConnectMyPrinterEnumerationLib
Imports ConnectMyPrinterOutlookHelper
Imports ConnectMyPrinterPrinterManageLib
Imports ConnectMyPrinterRemoteFileHandler

Public Class CLIWrapper
    Public AppSettingFile As String = "AppSettings.xml"
    Public AppSettings As New AppSettingsClass
    Public MainForm As New Form1

    Public ConnectPrinterCollection As New List(Of RemoteFilePrinterConnectItem)
    Public DisconnectPrinterCollection As New List(Of RemoteFilePrinterDisconnectItem)
    Public ConnectRemoteMachineCollection As New List(Of String)
    Public DisconnectRemoteMachineCollection As New List(Of String)

    Public BackupFilePath As String = ""
    Public RestoreFilePath As String = ""
    Public Merge1FilePath As String = ""
    Public Merge2FilePath As String = ""
    Public SMTPServer As String = ""
    Public SMTPUseSSL As Boolean = True
    Public SMTPUsername As String = ""
    Public SMTPPassword As String = ""
    Public SMTPServerPort As String = ""
    Public FromMail As String = ""
    Public ToMail As String = ""
    Public MailSubject As String = ""
    Public MailMessage As String = ""
    Public MailProfilePath As String = ""
    Public Clientname As String = ""
    Public Verbose As Boolean = False
    Public PingClients As Boolean = False
    Public CheckForAdminTracePath As Boolean = False
    Public WaitForUserInput As Boolean = False

    Public CLIAction As CLIActionEnum = 0

    Public Enum CLIActionEnum
        DoNothing = 0
        ListLocalPrinters = 1
        ListRemotePrinters = 2
        ConnectLocalPrinters = 3
        ConnectRemotePrinters = 4
        RemoveLocalPrinters = 5
        RemoveRemotePrinters = 6
        SetLocalDefaultPrinter = 7
        SetRemoteDefaultPrinter = 8
        ListConnectedLocalPrinters = 9
        ListConnectedRemotePrinters = 10
        BackupPrintersFromClient = 11
        RestoreBackupToClient = 12
        ApplyProfileToRemoteClient = 13
        BackupPrintersFromLocalMachine = 14
        RestorePrintersToLocalMachine = 15
        RemoveAllPrintersFromLocalMachine = 16
        RemoveAllPrintersFromRemoteMachine = 17
        RemoveAllConnectedPrintersFromLocalMachine = 18
        RemoveAllConnectedPrintersFromRemoteMachine = 19
        CreateConnectPrinterProfileFile = 20
        CreateDisconnectPrinterProfileFile = 21
        CreateChangeDefaultPrinterProfileFile = 22
        ListProfileFileContent = 23
        ConnectRemotePrintersUNC = 24
        DisconnectRemotePrintersUNC = 25
        SetRemoteDefaultPrinterUNC = 26
        Merge2FilesToNewProfileFile = 27
        SendProfileMailAuth = 28
        SendProfileMail = 29
    End Enum

    Public Sub LoadSettingsFile()
        'Lade Anwendungseinstellungen
        'Laden der Einstellungen für alle Benutzer
        If IO.File.Exists(Environment.SpecialFolder.LocalApplicationData & "\" & AppSettingFile) Then
            AppSettingFile = Environment.SpecialFolder.LocalApplicationData & "\" & AppSettingFile
            Debug.WriteLine(Environment.SpecialFolder.LocalApplicationData & "\" & AppSettingFile)
        Else
            'Laden der Einstellungen (über AppData)
            If IO.File.Exists(Environment.SpecialFolder.ApplicationData & "\" & AppSettingFile) Then
                AppSettingFile = Environment.SpecialFolder.ApplicationData & "\" & AppSettingFile
                Debug.WriteLine(Environment.SpecialFolder.ApplicationData & "\" & AppSettingFile)
            End If
        End If

        LoadAppSettingsInternal()
    End Sub

    Public Function LoadAppSettingsInternal() As Boolean
        Try
            'Laden der Einstellungen (im Programmverzeichnis oder über Befehlszeile)
            AppSettings = MainForm.LoadSettings(AppSettingFile)
            AppSettings.UseTracePathFeature = True
            MainForm.AppSettings = AppSettings

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function LunchCli(args As String(), Optional ByVal OutputErrors As Boolean = True) As Boolean
        Try
            LoadSettingsFile()

            If args IsNot Nothing Then
                If args.Length = 0 Then
                    If OutputErrors Then
                        ShowHelp()
                    End If
                Else
                    If args.Length >= 1 Then
                        Dim oldappsettingsfile As String
                        oldappsettingsfile = AppSettingFile
                        If ReadAllCommandSwitchesAndSetSettings(args) Then
                            PostVerboseText("Check application file path...")
                            If Not oldappsettingsfile = AppSettingFile Then
                                PostVerboseText("Reload application settings...")
                                LoadAppSettingsInternal()
                                PostVerboseText("Success: Reload application settings")
                                PostVerboseText("AppSettingsPath: " & AppSettingFile)
                            Else
                                PostVerboseText("No application settings reload needed.")
                            End If
                            PostVerboseText("Run actions...")
                            If ProcessActions() Then
                                PostVerboseText("Success: Run actions", ConsoleColor.Green)
                                If WaitForUserInput Then
                                    Console.WriteLine("Press any key to continue...")
                                    Console.ReadLine()
                                End If
                                If Verbose Then
                                    Console.ForegroundColor = ConsoleColor.White
                                End If

                                Return True
                            Else
                                PostVerboseText("Failed running actions!", ConsoleColor.Red)
                                If OutputErrors Then
                                    ShowHelp("", "Processing actions failed.", True)
                                End If
                                If WaitForUserInput Then
                                    Console.WriteLine("Press any key to continue...")
                                    Console.ReadLine()
                                End If
                                Return False
                            End If
                        Else
                            ShowHelp("", "Parsing command line arguments failed. Please check syntax.", True)
                        End If
                    Else
                        If OutputErrors Then
                            ShowHelp("", "No valid command-line arguments.")
                        End If
                    End If
                End If
            Else
                If OutputErrors Then
                    ShowHelp("", "No valid command-line arguments.")
                End If
            End If
        Catch ex As Exception
            If OutputErrors Then
                ShowHelp(ex.Message)
            End If
        End Try

        Return False
    End Function

    Public Sub PostVerboseText(ByVal TextStr As String, Optional ForeColor As ConsoleColor = ConsoleColor.White)
        If Verbose Then
            Console.ForegroundColor = ForeColor
            Console.WriteLine(TextStr)
        End If
    End Sub

    Public Function ReadAllCommandSwitchesAndSetSettings(ByVal CmdArgs As String()) As Boolean
        Try
            Dim arglist As List(Of String)
            arglist = CmdArgs.ToList

            For ind = 0 To arglist.Count - 1
                Try
                    PostVerboseText("Parsing argument '" & arglist(ind) & "'")

                    If arglist(ind).StartsWith("-CLP") Then
                        CLIAction = CLIActionEnum.ConnectLocalPrinters
                        Dim QQ As New RemoteFilePrinterConnectItem
                        QQ.PrinterName = arglist(ind + 1)
                        QQ.Printserver = arglist(ind + 2)
                        Try
                            If arglist(ind + 3) = "Default" Then
                                QQ.SetDefaultPrinter = True
                            End If
                        Catch ex As Exception
                        End Try
                        ConnectPrinterCollection.Add(QQ)
                    End If
                    If arglist(ind).StartsWith("-CRP") Then
                        CLIAction = CLIActionEnum.ConnectRemotePrinters
                        Dim hostnamestr As String
                        hostnamestr = arglist(ind + 1)
                        ConnectRemoteMachineCollection.Add(hostnamestr)
                        Dim QQ As New RemoteFilePrinterConnectItem
                        QQ.PrinterName = arglist(ind + 2)
                        QQ.Printserver = arglist(ind + 3)
                        Try
                            If arglist(ind + 4) = "Default" Then
                                QQ.SetDefaultPrinter = True
                            End If
                        Catch ex As Exception
                        End Try
                        ConnectPrinterCollection.Add(QQ)
                    End If
                    If arglist(ind).StartsWith("-UNCCRP") Then
                        CLIAction = CLIActionEnum.ConnectRemotePrintersUNC
                        Dim hostnamestr As String
                        hostnamestr = arglist(ind + 1)
                        ConnectRemoteMachineCollection.Add(hostnamestr)
                        Dim QQ As New RemoteFilePrinterConnectItem
                        Dim uncserverstr As String
                        uncserverstr = arglist(ind + 2).Split("\")(2)
                        Dim uncprinterstr As String
                        uncprinterstr = arglist(ind + 2).Split("\")(3)
                        QQ.PrinterName = uncprinterstr
                        QQ.Printserver = uncserverstr
                        Try
                            If arglist(ind + 3) = "Default" Then
                                QQ.SetDefaultPrinter = True
                            End If
                        Catch ex As Exception
                        End Try
                        ConnectPrinterCollection.Add(QQ)
                    End If
                    If arglist(ind).StartsWith("-RLP") Then
                        CLIAction = CLIActionEnum.RemoveLocalPrinters
                        Dim QQ As New RemoteFilePrinterDisconnectItem
                        QQ.PrinterName = arglist(ind + 1)
                        QQ.PrintServer = arglist(ind + 2)
                        DisconnectPrinterCollection.Add(QQ)
                    End If
                    If arglist(ind).StartsWith("-RRP") Then
                        CLIAction = CLIActionEnum.RemoveRemotePrinters
                        Dim hostnamestr As String
                        hostnamestr = arglist(ind + 1)
                        DisconnectRemoteMachineCollection.Add(hostnamestr)
                        Dim QQ As New RemoteFilePrinterDisconnectItem
                        QQ.PrinterName = arglist(ind + 2)
                        QQ.PrintServer = arglist(ind + 3)
                        DisconnectPrinterCollection.Add(QQ)
                    End If
                    If arglist(ind).StartsWith("-UNCRRP") Then
                        CLIAction = CLIActionEnum.DisconnectRemotePrintersUNC
                        Dim hostnamestr As String
                        hostnamestr = arglist(ind + 1)
                        DisconnectRemoteMachineCollection.Add(hostnamestr)
                        Dim QQ As New RemoteFilePrinterConnectItem
                        Dim uncserverstr As String
                        uncserverstr = arglist(ind + 2).Split("\")(2)
                        Dim uncprinterstr As String
                        uncprinterstr = arglist(ind + 2).Split("\")(3)
                        QQ.PrinterName = uncprinterstr
                        QQ.Printserver = uncserverstr
                        ConnectPrinterCollection.Add(QQ)
                    End If
                    If arglist(ind).StartsWith("-CALP") Then
                        CLIAction = CLIActionEnum.RemoveAllPrintersFromLocalMachine
                    End If
                    If arglist(ind).StartsWith("-CACLP") Then
                        CLIAction = CLIActionEnum.RemoveAllConnectedPrintersFromLocalMachine
                    End If
                    If arglist(ind).StartsWith("-CACRP") Then
                        CLIAction = CLIActionEnum.RemoveAllConnectedPrintersFromRemoteMachine
                        Clientname = arglist(ind + 1)
                    End If
                    If arglist(ind).StartsWith("-CLDP") Then
                        CLIAction = CLIActionEnum.SetLocalDefaultPrinter
                        Dim QQ As New RemoteFilePrinterConnectItem
                        QQ.PrinterName = arglist(ind + 1)
                        QQ.Printserver = arglist(ind + 2)
                        QQ.SetDefaultPrinter = True
                        ConnectPrinterCollection.Add(QQ)
                    End If
                    If arglist(ind).StartsWith("-CRDP") Then
                        CLIAction = CLIActionEnum.SetRemoteDefaultPrinter
                        Dim hostnamestr As String
                        hostnamestr = arglist(ind + 1)
                        ConnectRemoteMachineCollection.Add(hostnamestr)
                        Dim QQ As New RemoteFilePrinterConnectItem
                        QQ.PrinterName = arglist(ind + 2)
                        QQ.Printserver = arglist(ind + 3)
                        QQ.SetDefaultPrinter = True
                        ConnectPrinterCollection.Add(QQ)
                    End If
                    If arglist(ind).StartsWith("-UNCCRDP") Then
                        CLIAction = CLIActionEnum.SetRemoteDefaultPrinterUNC
                        Dim hostnamestr As String
                        hostnamestr = arglist(ind + 1)
                        ConnectRemoteMachineCollection.Add(hostnamestr)
                        Dim QQ As New RemoteFilePrinterConnectItem
                        Dim uncserverstr As String
                        uncserverstr = arglist(ind + 2).Split("\")(2)
                        Dim uncprinterstr As String
                        uncprinterstr = arglist(ind + 2).Split("\")(3)
                        QQ.PrinterName = uncprinterstr
                        QQ.Printserver = uncserverstr
                        QQ.SetDefaultPrinter = True
                        ConnectPrinterCollection.Add(QQ)
                    End If
                    If arglist(ind).StartsWith("-LLP") Then
                        CLIAction = CLIActionEnum.ListLocalPrinters
                    End If
                    If arglist(ind).StartsWith("-LRP") Then
                        CLIAction = CLIActionEnum.ListRemotePrinters
                    End If
                    If arglist(ind).StartsWith("-LLCP") Then
                        CLIAction = CLIActionEnum.ListConnectedLocalPrinters
                    End If
                    If arglist(ind).StartsWith("-LRCP") Then
                        CLIAction = CLIActionEnum.ListConnectedRemotePrinters
                    End If
                    If arglist(ind).StartsWith("-BPEL") Then
                        CLIAction = CLIActionEnum.BackupPrintersFromLocalMachine
                        BackupFilePath = arglist(ind + 1)
                    End If
                    If arglist(ind).StartsWith("-RPEL") Then
                        CLIAction = CLIActionEnum.RestorePrintersToLocalMachine
                        RestoreFilePath = arglist(ind + 1)
                    End If
                    If arglist(ind).StartsWith("-BPERC") Then
                        CLIAction = CLIActionEnum.BackupPrintersFromClient
                        Clientname = arglist(ind + 1)
                        BackupFilePath = arglist(ind + 2)
                    End If
                    If arglist(ind).StartsWith("-RPERC") Then
                        CLIAction = CLIActionEnum.RestoreBackupToClient
                        Clientname = arglist(ind + 1)
                        RestoreFilePath = arglist(ind + 2)
                    End If
                    If arglist(ind).StartsWith("-ARPPF") Then
                        CLIAction = CLIActionEnum.ApplyProfileToRemoteClient
                        Clientname = arglist(ind + 1)
                        RestoreFilePath = arglist(ind + 2)
                    End If
                    If arglist(ind).StartsWith("-BCPPF") Then
                        CLIAction = CLIActionEnum.CreateConnectPrinterProfileFile
                        BackupFilePath = arglist(ind + 1)
                        Dim QQ As New RemoteFilePrinterConnectItem
                        QQ.PrinterName = arglist(ind + 2)
                        QQ.Printserver = arglist(ind + 3)
                        Try
                            If arglist(ind + 4) = "Default" Then
                                QQ.SetDefaultPrinter = True
                            End If
                        Catch ex As Exception
                        End Try
                        ConnectPrinterCollection.Add(QQ)
                    End If
                    If arglist(ind).StartsWith("-BDPPF") Then
                        CLIAction = CLIActionEnum.CreateDisconnectPrinterProfileFile
                        BackupFilePath = arglist(ind + 1)
                        Dim QQ As New RemoteFilePrinterDisconnectItem
                        QQ.PrinterName = arglist(ind + 2)
                        QQ.PrintServer = arglist(ind + 3)
                        DisconnectPrinterCollection.Add(QQ)
                    End If
                    If arglist(ind).StartsWith("-BCDPF") Then
                        CLIAction = CLIActionEnum.CreateChangeDefaultPrinterProfileFile
                        BackupFilePath = arglist(ind + 1)
                        Dim QQ As New RemoteFilePrinterConnectItem
                        QQ.PrinterName = arglist(ind + 2)
                        QQ.Printserver = arglist(ind + 3)
                        QQ.SetDefaultPrinter = True
                        ConnectPrinterCollection.Add(QQ)
                    End If
                    If arglist(ind).StartsWith("-LPFC") Then
                        CLIAction = CLIActionEnum.ListProfileFileContent
                        RestoreFilePath = arglist(ind + 1)
                    End If
                    If arglist(ind).StartsWith("-CBPF") Then
                        CLIAction = CLIActionEnum.Merge2FilesToNewProfileFile
                        Merge1FilePath = arglist(ind + 1)
                        Merge2FilePath = arglist(ind + 2)
                        BackupFilePath = arglist(ind + 3)
                    End If
                    If arglist(ind).StartsWith("-SAMTA") Then
                        CLIAction = CLIActionEnum.SendProfileMailAuth
                        SMTPServer = arglist(ind + 1)
                        SMTPUsername = arglist(ind + 2)
                        SMTPPassword = arglist(ind + 3)
                        SMTPServerPort = arglist(ind + 4)
                        FromMail = arglist(ind + 5)
                        ToMail = arglist(ind + 6)
                        MailSubject = arglist(ind + 7)
                        MailMessage = arglist(ind + 8)
                        MailProfilePath = arglist(ind + 9)
                    End If
                    If arglist(ind).StartsWith("-SMTA") Then
                        CLIAction = CLIActionEnum.SendProfileMail
                        SMTPServer = arglist(ind + 1)
                        SMTPServerPort = arglist(ind + 2)
                        FromMail = arglist(ind + 3)
                        ToMail = arglist(ind + 4)
                        MailSubject = arglist(ind + 5)
                        MailMessage = arglist(ind + 6)
                        MailProfilePath = arglist(ind + 7)
                    End If
                    If arglist(ind).StartsWith("-AS") Then
                        AppSettingFile = arglist(ind + 1)
                    End If
                    If arglist(ind).StartsWith("-V") Then
                        Verbose = True
                    End If
                    If arglist(ind).StartsWith("-CTAP") Then
                        CheckForAdminTracePath = True
                    End If
                    If arglist(ind).StartsWith("-CATP") Then
                        AppSettings.ActionsTraceAdminPath = arglist(ind + 1)
                    End If
                    If arglist(ind).StartsWith("-P") Then
                        PingClients = True
                    End If
                    If arglist(ind).StartsWith("-NSSL") Then
                        SMTPUseSSL = False
                    End If
                    If arglist(ind).StartsWith("-W") Then
                        WaitForUserInput = True
                    End If
                Catch ex As Exception
                    ShowHelp(ex.Message, "Unknown cli parameter or parameter list is not complete.")
                    Return False
                End Try
            Next

            Return True
        Catch ex As Exception
            ShowHelp(ex.Message, "Unknown error.")
            Return False
        End Try
    End Function

    Public Function BuildRemoteFile() As RemoteFileClass
        Try
            Dim remotefileobj As New RemoteFileClass

            If CLIAction = CLIActionEnum.ConnectRemotePrinters Or CLIAction = CLIActionEnum.SetRemoteDefaultPrinter Or CLIAction = CLIActionEnum.ConnectRemotePrintersUNC Or CLIAction = CLIActionEnum.SetRemoteDefaultPrinterUNC Then
                remotefileobj.ConnectPrinters.AddRange(ConnectPrinterCollection)
            End If
            If CLIAction = CLIActionEnum.RemoveRemotePrinters Or CLIAction = CLIActionEnum.DisconnectRemotePrintersUNC Then
                remotefileobj.DisconnectPrinters.AddRange(DisconnectPrinterCollection)
            End If

            Return remotefileobj
        Catch ex As Exception
            Return New RemoteFileClass
        End Try
    End Function

    Public Function ProcessActions() As Boolean
        Try
            If CLIAction = CLIActionEnum.DoNothing Then
                PostVerboseText("Selected action: Nothing")
                Return True
            End If

            If CLIAction = CLIActionEnum.RemoveLocalPrinters Then
                'Drucker trennen
                PostVerboseText("Selected action: Disconnect local printer")
                For Each item As RemoteFilePrinterDisconnectItem In DisconnectPrinterCollection
                    PostVerboseText("Processing Item " & item.PrinterName)
                    Try
                        Dim uu As New ManagePrinter
                        Dim qq As New PrinterQueueInfo
                        qq.ShareName = item.PrinterName
                        qq.Name = item.PrinterName
                        uu.DeletePrinter(qq)
                    Catch ex As Exception
                        PostVerboseText("Failed: Processing Item " & item.PrinterName)
                        Return False
                    End Try
                Next
                Return True
            End If
            If CLIAction = CLIActionEnum.ConnectRemotePrinters Or CLIAction = CLIActionEnum.RemoveRemotePrinters Or CLIAction = CLIActionEnum.SetRemoteDefaultPrinter Or CLIAction = CLIActionEnum.ConnectRemotePrintersUNC Or CLIAction = CLIActionEnum.DisconnectRemotePrintersUNC Or CLIAction = CLIActionEnum.SetRemoteDefaultPrinterUNC Then
                PostVerboseText("Selected action: Remote client action")
                Dim disthandler As New DistributionHelper
                PostVerboseText("Publish profile to client " & ConnectRemoteMachineCollection(0))
                If disthandler.PublishProfileToClient(ConnectRemoteMachineCollection(0), "", True, False, BuildRemoteFile, AppSettings, True, PingClients, CheckForAdminTracePath) Then
                    PostVerboseText("Success: Publish profile to client")
                    Return True
                Else
                    PostVerboseText("Failed: Publish profile to client")
                    Return False
                End If
            End If
            If CLIAction = CLIActionEnum.ListRemotePrinters Then
                PostVerboseText("Selected action: List all remote printers")
                Dim disthandler As New DistributionHelper
                AppSettings.IgnoreLocalPrintersAtRemoteFetching = False
                PostVerboseText("Fetch all printers from client " & Clientname)
                ListPrinterRemoteFileToOutput(disthandler.LoadPrinterProfileFromClient(Clientname, AppSettings, PingClients, CheckForAdminTracePath))
                Return True
            End If
            If CLIAction = CLIActionEnum.ListConnectedRemotePrinters Then
                PostVerboseText("Selected action: List all remote connected printers")
                Dim disthandler As New DistributionHelper
                AppSettings.IgnoreLocalPrintersAtRemoteFetching = True
                PostVerboseText("Fetch all connected printers from client " & Clientname)
                ListPrinterRemoteFileToOutput(disthandler.LoadPrinterProfileFromClient(Clientname, AppSettings, PingClients, CheckForAdminTracePath), True)
                Return True
            End If
            If CLIAction = CLIActionEnum.RemoveAllPrintersFromLocalMachine Then
                PostVerboseText("Selected action: Remove all printers from local machine")
                Dim dd As New PrinterDriverRemover
                PostVerboseText("Delete all printers from local machine...")
                If dd.DeleteAllPrintersAndDrivers(AppSettings.PrinterAdminPath) Then
                    PostVerboseText("Success: Delete all printers")
                    Return True
                Else
                    PostVerboseText("Failed: Delete all printers")
                    Return False
                End If
            End If
            If CLIAction = CLIActionEnum.RemoveAllConnectedPrintersFromLocalMachine Then
                PostVerboseText("Selected action: Remove all connected printers from local machine")
                Dim connectedprinters
                connectedprinters = MainForm.LoadLocalPrinters()
                PostVerboseText("Delete all connected printers from local machine...")
                For Each item As RemoteFilePrinterDisconnectItem In DisconnectPrinterCollection
                    If (Not item.PrintServer = "Lokal") And (Not item.PrintServer = "Local") Then
                        PostVerboseText("Processing Item " & item.PrinterName)
                        Try
                            Dim uu As New ManagePrinter
                            Dim qq As New PrinterQueueInfo
                            qq.ShareName = item.PrinterName
                            qq.Name = item.PrinterName
                            uu.DeletePrinter(qq)
                        Catch ex As Exception
                            PostVerboseText("Failed: Processing Item " & item.PrinterName)
                            Return False
                        End Try
                    End If
                Next
                Return True
            End If
            If CLIAction = CLIActionEnum.RemoveAllConnectedPrintersFromRemoteMachine Then
                PostVerboseText("Selected action: Remove all connected printers from remote machine")
                Dim disthandler As New DistributionHelper
                Dim resultclass As RemoteFileClass
                PostVerboseText("Fetch all connected printers from remote machine...")
                resultclass = disthandler.LoadPrinterProfileFromClient(Clientname, AppSettings, PingClients, CheckForAdminTracePath)
                PostVerboseText("Build remote file...")
                For Each item As RemoteFilePrinterConnectItem In resultclass.ConnectPrinters
                    If (Not item.Printserver = "Lokal") And (Not item.Printserver = "Local") Then
                        PostVerboseText("Parsing profile file: Item " & item.PrinterName)
                        Dim jj As New RemoteFilePrinterDisconnectItem
                        jj.PrinterName = item.PrinterName
                        jj.PrintServer = item.Printserver
                        resultclass.DisconnectPrinters.Add(jj)
                    End If
                Next
                resultclass.ConnectPrinters.Clear()
                PostVerboseText("Delete all connected printers from remote machine...")
                If disthandler.PublishProfileToClient(Clientname, "", True, False, resultclass, AppSettings, True, PingClients, CheckForAdminTracePath) Then
                    Return True
                Else
                    Return False
                End If
            End If
            If CLIAction = CLIActionEnum.BackupPrintersFromLocalMachine Then
                PostVerboseText("Selected action: Backup printers from local machine")
                Dim RemoteFileService As New RemoteFileCreator
                If RemoteFileService.CreateMultiplePrinterRemoteFile(BackupFilePath, MainForm.LoadLocalPrinters) Then
                    PostVerboseText("Success: Backup local printers")
                    Return True
                Else
                    PostVerboseText("Failed: Backup local printers")
                    Return False
                End If
            End If
            If CLIAction = CLIActionEnum.RestorePrintersToLocalMachine Then
                PostVerboseText("Selected action: Restore printers to local machine")
                Dim jj As New RemoteFileSerializer
                If ConnectPrinterCollectionFunc(jj.LoadRemoteFile(RestoreFilePath).ConnectPrinters) Then
                    PostVerboseText("Success: Restore local printers")
                    Return True
                Else
                    PostVerboseText("Failed: Restore local printers")
                    Return False
                End If
            End If
            If CLIAction = CLIActionEnum.BackupPrintersFromClient Then
                PostVerboseText("Selected action: Backup printers from remote machine")
                Dim disthandler As New DistributionHelper
                AppSettings.IgnoreLocalPrintersAtRemoteFetching = True
                Dim jj As New RemoteFileSerializer
                If jj.SaveRemoteFile(disthandler.LoadPrinterProfileFromClient(Clientname, AppSettings, PingClients, CheckForAdminTracePath), BackupFilePath) Then
                    PostVerboseText("Success: Backup printers from remote machine")
                    Return True
                Else
                    PostVerboseText("Failed: Backup printers from remote machine")
                    Return False
                End If
            End If
            If CLIAction = CLIActionEnum.RestoreBackupToClient Or CLIAction = CLIActionEnum.ApplyProfileToRemoteClient Then
                PostVerboseText("Selected action: Restore printers to remote machine or apply profile to client")
                Dim disthandler As New DistributionHelper
                AppSettings.IgnoreLocalPrintersAtRemoteFetching = True
                Dim jj As New RemoteFileSerializer
                If disthandler.PublishProfileToClient(Clientname, "", True, False, jj.LoadRemoteFile(RestoreFilePath), AppSettings, True, PingClients, CheckForAdminTracePath) Then
                    PostVerboseText("Success: Restore printers to remote machine or apply profile to client")
                    Return True
                Else
                    PostVerboseText("Failed: Restore printers to remote machine or apply profile to client")
                    Return False
                End If
            End If
            If CLIAction = CLIActionEnum.ConnectLocalPrinters Then
                PostVerboseText("Selected action: Connect local printer")
                If ConnectPrinterCollectionFunc(ConnectPrinterCollection) Then
                    PostVerboseText("Success: Connect local printer")
                    Return True
                Else
                    PostVerboseText("Failed: Connect local printer")
                    Return False
                End If
            End If
            If CLIAction = CLIActionEnum.SetLocalDefaultPrinter Then
                PostVerboseText("Selected action: Set default printer")
                For Each item As RemoteFilePrinterConnectItem In ConnectPrinterCollection
                    PostVerboseText("Processing Item " & item.PrinterName)
                    Try
                        Dim uu As New ManagePrinter
                        Dim qq As New PrinterQueueInfo
                        qq.ShareName = item.PrinterName
                        qq.Name = item.PrinterName

                        If item.Printserver = "Lokal" Or item.Printserver = "Local" Then
                            qq.Server = item.Printserver
                        Else
                            qq.Server = "\\" & item.Printserver
                        End If

                        uu.SetDefaultPrinter(qq)
                    Catch ex As Exception
                        PostVerboseText("Failed: Processing Item " & item.PrinterName)
                        Return False
                    End Try
                Next
                Return True
            End If
            If CLIAction = CLIActionEnum.ListLocalPrinters Then
                PostVerboseText("Selected action: List all local printers")
                ListPrinterToOutput(MainForm.LoadLocalPrinters)
                Return True
            End If
            If CLIAction = CLIActionEnum.ListConnectedLocalPrinters Then
                PostVerboseText("Selected action: List all connected local printers")
                ListPrinterToOutput(MainForm.LoadLocalPrinters, True)
                Return True
            End If
            If CLIAction = CLIActionEnum.CreateConnectPrinterProfileFile Then
                PostVerboseText("Selected action: Create connect printer profile file")
                Dim tt As New RemoteFileClass
                Dim savehandler As New RemoteFileSerializer
                tt.ConnectPrinters.AddRange(ConnectPrinterCollection)
                PostVerboseText("Save profile file...")
                If savehandler.SaveRemoteFile(tt, BackupFilePath) Then
                    Return True
                Else
                    Return False
                End If
            End If
            If CLIAction = CLIActionEnum.CreateDisconnectPrinterProfileFile Then
                PostVerboseText("Selected action: Create disconnect printer profile file")
                Dim tt As New RemoteFileClass
                Dim savehandler As New RemoteFileSerializer
                tt.DisconnectPrinters.AddRange(DisconnectPrinterCollection)
                PostVerboseText("Save profile file...")
                If savehandler.SaveRemoteFile(tt, BackupFilePath) Then
                    Return True
                Else
                    Return False
                End If
            End If
            If CLIAction = CLIActionEnum.CreateChangeDefaultPrinterProfileFile Then
                PostVerboseText("Selected action: Create change default printer profile file")
                Dim tt As New RemoteFileClass
                Dim savehandler As New RemoteFileSerializer
                Dim jj As New RemoteFileActions
                jj.Action = RemoteFileActionsEnums.ActionEnum.SetDefaultPrinter
                jj.PrinterName = ConnectPrinterCollection(0).PrinterName
                jj.PrintServer = ConnectPrinterCollection(0).Printserver
                tt.IntermediateActions.Add(jj)
                PostVerboseText("Save profile file...")
                If savehandler.SaveRemoteFile(tt, BackupFilePath) Then
                    Return True
                Else
                    Return False
                End If
            End If
            If CLIAction = CLIActionEnum.ListProfileFileContent Then
                PostVerboseText("Selected action: List all items from profile file")
                Dim tt As RemoteFileClass
                Dim fileopener As New RemoteFileSerializer
                PostVerboseText("Parsing profile file...")
                If IO.File.Exists(RestoreFilePath) Then
                    tt = fileopener.LoadRemoteFile(RestoreFilePath)
                    If Not (tt.ConnectPrinters.Count = 0 And tt.DisconnectPrinters.Count = 0 And tt.IntermediateActions.Count = 0 And tt.Postactions.Count = 0 And tt.Preactions.Count = 0 And tt.CustomAppSettingsFile = "") Then
                        ListAllPrinterRemoteFileContentToOutput(tt)
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            End If
            If CLIAction = CLIActionEnum.Merge2FilesToNewProfileFile Then
                PostVerboseText("Selected action: Merge multiple profile files to new profile file")
                Dim fileopener As New RemoteFileSerializer
                Dim merge1class As RemoteFileClass
                Dim merge2class As RemoteFileClass
                Dim newclass As New RemoteFileClass
                PostVerboseText("Parsing 1st profile file...")
                merge1class = fileopener.LoadRemoteFile(Merge1FilePath)
                PostVerboseText("Parsing 2nd profile file...")
                merge2class = fileopener.LoadRemoteFile(Merge2FilePath)
                PostVerboseText("Merging profiles...")
                newclass.ConnectPrinters.AddRange(merge1class.ConnectPrinters)
                newclass.ConnectPrinters.AddRange(merge2class.ConnectPrinters)
                newclass.DisconnectPrinters.AddRange(merge1class.DisconnectPrinters)
                newclass.DisconnectPrinters.AddRange(merge2class.DisconnectPrinters)
                newclass.Preactions.AddRange(merge1class.Preactions)
                newclass.Preactions.AddRange(merge2class.Preactions)
                newclass.IntermediateActions.AddRange(merge1class.IntermediateActions)
                newclass.IntermediateActions.AddRange(merge2class.IntermediateActions)
                newclass.Postactions.AddRange(merge1class.Postactions)
                newclass.Postactions.AddRange(merge2class.Postactions)
                PostVerboseText("Saving new profile file...")
                If fileopener.SaveRemoteFile(newclass, BackupFilePath) Then
                    Return True
                Else
                    Return False
                End If
            End If
            If CLIAction = CLIActionEnum.SendProfileMailAuth Then
                PostVerboseText("Selected action: Send mail (auth) with profile file to e-mail-address")
                Dim senderh As New OutlookHelperClass
                PostVerboseText("Sending mail...")
                If senderh.SendMailInternal(SMTPServer, SMTPUsername, SMTPPassword, SMTPServerPort, SMTPUseSSL, FromMail, ToMail, MailSubject, False, MailMessage, MailProfilePath) Then
                    Return True
                Else
                    Return False
                End If
            End If
            If CLIAction = CLIActionEnum.SendProfileMail Then
                PostVerboseText("Selected action: Send mail with profile file to e-mail-address")
                Dim senderh As New OutlookHelperClass
                PostVerboseText("Sending mail...")
                If senderh.SendMailInternal(SMTPServer, "", "", SMTPServerPort, SMTPUseSSL, FromMail, ToMail, MailSubject, False, MailMessage, MailProfilePath) Then
                    Return True
                Else
                    Return False
                End If
            End If

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ConnectPrinterCollectionFunc(ByVal ConnectPrinterCollectionObj As List(Of RemoteFilePrinterConnectItem)) As Boolean
        Try
            For Each item As RemoteFilePrinterConnectItem In ConnectPrinterCollectionObj
                Try
                    Dim uu As New ConnectMyPrinterPrinterManageLib.ManagePrinter
                    Dim qq As New PrinterQueueInfo
                    qq.ShareName = item.PrinterName
                    qq.Server = "\\" & item.Printserver
                    qq.Name = item.PrinterName

                    If (Not item.Printserver = "Lokal") And (Not item.Printserver = "Local") Then
                        Shell("rundll32 printui.dll PrintUIEntry /in /n \\" & item.Printserver & "\" & item.PrinterName, AppWinStyle.Hide, True, 120)
                    End If

                    If item.SetDefaultPrinter Then
                        Threading.Thread.Sleep(500)
                        uu.SetDefaultPrinter(qq)
                    End If
                Catch ex As Exception
                    Return False
                End Try
            Next
        Catch ex As Exception

        End Try
    End Function

    Sub ListPrinterToOutput(ByVal PrinterCollection As List(Of PrinterQueueInfo), Optional ByVal OnlyConnectedPrinters As Boolean = False)
        Console.WriteLine("")

        If OnlyConnectedPrinters Then
            For ind = 0 To PrinterCollection.Count - 1
                If (Not PrinterCollection(ind).Server = "Lokal") And (Not PrinterCollection(ind).Server = "Local") Then
                    Console.WriteLine(PrinterCollection(ind).ShareName & vbTab & PrinterCollection(ind).Server & vbTab & PrinterCollection(ind).DefaultPrinter.ToString)
                End If
            Next
        Else
            For ind = 0 To PrinterCollection.Count - 1
                Dim servernamestr As String
                servernamestr = PrinterCollection(ind).Server
                If servernamestr = "Lokal" Then
                    servernamestr = servernamestr.Replace("Lokal", "Local")
                End If

                Console.WriteLine(PrinterCollection(ind).ShareName & vbTab & servernamestr & vbTab & PrinterCollection(ind).DefaultPrinter.ToString)
            Next
        End If

        Console.WriteLine("")
    End Sub

    Sub ListPrinterRemoteFileToOutput(ByVal PrinterCollection As RemoteFileClass, Optional ByVal OnlyConnectedPrinters As Boolean = False)
        Console.WriteLine("")

        If OnlyConnectedPrinters Then
            For ind = 0 To PrinterCollection.ConnectPrinters.Count - 1
                If (Not PrinterCollection.ConnectPrinters(ind).Printserver = "Lokal") And (Not PrinterCollection.ConnectPrinters(ind).Printserver = "Local") Then
                    Console.WriteLine(PrinterCollection.ConnectPrinters(ind).PrinterName & vbTab & PrinterCollection.ConnectPrinters(ind).Printserver & vbTab & PrinterCollection.ConnectPrinters(ind).SetDefaultPrinter.ToString)
                End If
            Next
        Else
            For ind = 0 To PrinterCollection.ConnectPrinters.Count - 1
                Dim servernamestr As String
                servernamestr = PrinterCollection.ConnectPrinters(ind).Printserver
                If servernamestr = "Lokal" Then
                    servernamestr = servernamestr.Replace("Lokal", "Local")
                End If

                Console.WriteLine(PrinterCollection.ConnectPrinters(ind).PrinterName & vbTab & servernamestr & vbTab & PrinterCollection.ConnectPrinters(ind).SetDefaultPrinter.ToString)
            Next
        End If

        Console.WriteLine("")
    End Sub

    Sub ListAllPrinterRemoteFileContentToOutput(ByVal PrinterCollection As RemoteFileClass)
        Console.WriteLine("")

        For ind = 0 To PrinterCollection.ConnectPrinters.Count - 1
            Dim servernamestr As String
            servernamestr = PrinterCollection.ConnectPrinters(ind).Printserver
            If servernamestr = "Lokal" Then
                servernamestr = servernamestr.Replace("Lokal", "Local")
            End If

            Console.WriteLine("Connect" & vbTab & PrinterCollection.ConnectPrinters(ind).PrinterName & vbTab & servernamestr & vbTab & PrinterCollection.ConnectPrinters(ind).SetDefaultPrinter.ToString)
        Next

        For ind = 0 To PrinterCollection.DisconnectPrinters.Count - 1
            Dim servernamestr As String
            servernamestr = PrinterCollection.DisconnectPrinters(ind).PrintServer
            If servernamestr = "Lokal" Then
                servernamestr = servernamestr.Replace("Lokal", "Local")
            End If

            Console.WriteLine("Disconnect" & vbTab & PrinterCollection.DisconnectPrinters(ind).PrinterName & vbTab & servernamestr)
        Next

        For ind = 0 To PrinterCollection.Preactions.Count - 1
            Console.WriteLine("Preaction" & vbTab & PrinterCollection.Preactions(ind).Action.ToString & vbTab & PrinterCollection.Preactions(ind).CustomData & vbTab & PrinterCollection.Preactions(ind).PrinterName & vbTab & PrinterCollection.Preactions(ind).PrintServer)
        Next
        For ind = 0 To PrinterCollection.IntermediateActions.Count - 1
            Console.WriteLine("IntermediateAction" & vbTab & PrinterCollection.IntermediateActions(ind).Action.ToString & vbTab & PrinterCollection.IntermediateActions(ind).CustomData & vbTab & PrinterCollection.IntermediateActions(ind).PrinterName & vbTab & PrinterCollection.IntermediateActions(ind).PrintServer)
        Next
        For ind = 0 To PrinterCollection.Postactions.Count - 1
            Console.WriteLine("Postaction" & vbTab & PrinterCollection.Postactions(ind).Action.ToString & vbTab & PrinterCollection.Postactions(ind).CustomData & vbTab & PrinterCollection.Postactions(ind).PrinterName & vbTab & PrinterCollection.Postactions(ind).PrintServer)
        Next

        Console.WriteLine("")
    End Sub


    Public Sub ShowHelp(Optional ByVal IntErrorText As String = "", Optional ByVal UserErrorText As String = "", Optional ByVal ShowOnlyError As Boolean = False)
        Console.WriteLine("ConnectMyPrinter.NET CLI (" & Environment.Version.ToString & ")")
        If Not UserErrorText = "" Then
            Console.WriteLine("Error: " & UserErrorText)
            Console.WriteLine("Error (internal): " & IntErrorText)
        End If

        If ShowOnlyError = False Then
            Console.WriteLine("Help:")
            Console.WriteLine("-CLP" & vbTab & vbTab & "Connect printers to local machine: <Printer share name> <Print server> [Default]")
            Console.WriteLine("-CRP" & vbTab & vbTab & "Connect printers to remote machine: <Hostname> <Printer share name> <Print server> [Default]")
            Console.WriteLine("-UNCCRP" & vbTab & vbTab & "Connect printers to remote machine (UNC): <Hostname> <UNC share name> [Default]")
            Console.WriteLine("-RLP" & vbTab & vbTab & "Disconnect printers from local machine: <Printer share name> <Print server>")
            Console.WriteLine("-RRP" & vbTab & vbTab & "Disconnect printers from remote machine: <Hostname> <Printer share name> <Print server>")
            Console.WriteLine("-UNCRRP" & vbTab & vbTab & "Disconnect printers from remote machine (UNC): <Hostname> <UNC share name>")
            Console.WriteLine("-CALP" & vbTab & vbTab & "Remove all printers from local machine")
            Console.WriteLine("-CACLP" & vbTab & vbTab & "Remove all connected printers from local machine")
            Console.WriteLine("-CACRP" & vbTab & vbTab & "Remove all connected printers from remote machine: <Hostname>")
            Console.WriteLine("-CLDP" & vbTab & vbTab & "Change local machine default printer: <Printer share name> <Print server>")
            Console.WriteLine("-CRDP" & vbTab & vbTab & "Change remote machine default printer: <Hostname> <Printer share name> <Print server>")
            Console.WriteLine("-UNCCRDP" & vbTab & "Change remote machine default printer (UNC): <Hostname> <UNC share name>")
            Console.WriteLine("-LLP" & vbTab & vbTab & "List all local machine printers")
            Console.WriteLine("-LRP" & vbTab & vbTab & "List all remote machine printers: <Hostname>")
            Console.WriteLine("-LLCP" & vbTab & vbTab & "List all local connected printers")
            Console.WriteLine("-LRCP" & vbTab & vbTab & "List all remote machine connected printers: <Hostname>")
            Console.WriteLine("-BPEL" & vbTab & vbTab & "Backup all printers from local machine: <Filename>")
            Console.WriteLine("-RPEL" & vbTab & vbTab & "Restore all printers to local machine: <Filename>")
            Console.WriteLine("-BPERC" & vbTab & vbTab & "Backup all printers from remote client: <Hostname> <Filename>")
            Console.WriteLine("-RPERC" & vbTab & vbTab & "Restore all printers to remote client: <Hostname> <Filename>")
            Console.WriteLine("-ARPPF" & vbTab & vbTab & "Apply profile file to remote machine: <Hostname> <Filename>")
            Console.WriteLine("-BCPPF" & vbTab & vbTab & "Build printer connect profile file: <Filename> <Printer share name> <Print server>")
            Console.WriteLine("-BDPPF" & vbTab & vbTab & "Build printer disconnect profile file: <Filename> <Printer share name> <Print server>")
            Console.WriteLine("-BCDPF" & vbTab & vbTab & "Build change default printer profile file: <Filename> <Printer share name> <Print server>")
            Console.WriteLine("-LPFC" & vbTab & vbTab & "List all items from profile file: <Filename>")
            Console.WriteLine("-CBPF" & vbTab & vbTab & "Merge 2 profile files to new profile file: <Filename 1> <Filename 2> <New filename>")
            Console.WriteLine("-SAMTA" & vbTab & vbTab & "Send printer profile to e-mail-address (SMTP-Auth): <SMTP server> <SMTPUsername> <SMTPPassword> <SMTPPort> <FROMAdress> <TOAddress> <Subject> <Message> <Profile filename>")
            Console.WriteLine("-SMTA" & vbTab & vbTab & "Send printer profile to e-mail-address: <SMTP server> <SMTPPort> <FROMAdress> <TOAddress> <Subject> <Message> <Profile filename>")
            Console.WriteLine("[-AS]" & vbTab & vbTab & "Load custom settings file <File>")
            Console.WriteLine("[-CATP]" & vbTab & vbTab & "Set custom admin trace path <Tracing path>")
            Console.WriteLine("[-V]" & vbTab & vbTab & "Verbose output")
            Console.WriteLine("[-P]" & vbTab & vbTab & "Check if remote machine is up")
            Console.WriteLine("[-CTAP]" & vbTab & vbTab & "Check if admin-trace path is accessible")
            Console.WriteLine("[-NSSL]" & vbTab & vbTab & "Do not use SSL for sending mails")
            Console.WriteLine("[-W]" & vbTab & vbTab & "Wait for user input after processing actions")
        End If
    End Sub
End Class
