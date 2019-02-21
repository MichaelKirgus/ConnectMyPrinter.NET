Imports ConnectMyPrinter.NET
Imports ConnectMyPrinterAppSettingsHandler
Imports ConnectMyPrinterDistributionLib
Imports ConnectMyPrinterEnumerationLib
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
    Public Clientname As String = ""

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

        'Laden der Einstellungen (im Programmverzeichnis oder über Befehlszeile)
        AppSettings = MainForm.LoadSettings(AppSettingFile)
        MainForm.AppSettings = AppSettings
    End Sub

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
                        If ReadAllCommandSwitchesAndSetSettings(args) Then
                            If ProcessActions() Then
                                Return True
                            Else
                                If OutputErrors Then
                                    ShowHelp("", "Processing actions failed.", True)
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

    Public Function ReadAllCommandSwitchesAndSetSettings(ByVal CmdArgs As String()) As Boolean
        Try
            Dim arglist As List(Of String)
            arglist = CmdArgs.ToList

            For ind = 0 To arglist.Count - 1
                Try
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

            If CLIAction = CLIActionEnum.ConnectRemotePrinters Or CLIAction = CLIActionEnum.SetRemoteDefaultPrinter Then
                remotefileobj.ConnectPrinters.AddRange(ConnectPrinterCollection)
            End If
            If CLIAction = CLIActionEnum.RemoveRemotePrinters Then
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
                Return True
            End If

            If CLIAction = CLIActionEnum.RemoveLocalPrinters Then
                'Drucker trennen
                For Each item As RemoteFilePrinterDisconnectItem In DisconnectPrinterCollection
                    Try
                        Dim uu As New ManagePrinter
                        Dim qq As New PrinterQueueInfo
                        qq.ShareName = item.PrinterName
                        qq.Name = item.PrinterName
                        uu.DeletePrinter(qq)
                    Catch ex As Exception
                        Return False
                    End Try
                Next
                Return True
            End If
            If CLIAction = CLIActionEnum.ConnectRemotePrinters Or CLIAction = CLIActionEnum.RemoveRemotePrinters Or CLIAction = CLIActionEnum.SetRemoteDefaultPrinter Then
                Dim disthandler As New DistributionHelper
                If disthandler.PublishProfileToClient(ConnectRemoteMachineCollection(0), "", True, False, BuildRemoteFile, AppSettings) Then
                    Return True
                Else
                    Return False
                End If
            End If
            If CLIAction = CLIActionEnum.ListRemotePrinters Then
                Dim disthandler As New DistributionHelper
                AppSettings.IgnoreLocalPrintersAtRemoteFetching = False
                ListPrinterRemoteFileToOutput(disthandler.LoadPrinterProfileFromClient(Clientname, AppSettings))
                Return True
            End If
            If CLIAction = CLIActionEnum.ListConnectedRemotePrinters Then
                Dim disthandler As New DistributionHelper
                AppSettings.IgnoreLocalPrintersAtRemoteFetching = True

                ListPrinterRemoteFileToOutput(disthandler.LoadPrinterProfileFromClient(Clientname, AppSettings), True)
                Return True
            End If
            If CLIAction = CLIActionEnum.RemoveAllPrintersFromLocalMachine Then
                Dim dd As New PrinterDriverRemover
                dd.DeleteAllPrintersAndDrivers(AppSettings.PrinterAdminPath)
                Return True
            End If
            If CLIAction = CLIActionEnum.RemoveAllConnectedPrintersFromLocalMachine Then
                Dim connectedprinters
                connectedprinters = MainForm.LoadLocalPrinters()

                For Each item As RemoteFilePrinterDisconnectItem In DisconnectPrinterCollection
                    If (Not item.PrintServer = "Lokal") And (Not item.PrintServer = "Local") Then
                        Try
                            Dim uu As New ManagePrinter
                            Dim qq As New PrinterQueueInfo
                            qq.ShareName = item.PrinterName
                            qq.Name = item.PrinterName
                            uu.DeletePrinter(qq)
                        Catch ex As Exception
                            Return False
                        End Try
                    End If
                Next
                Return True
            End If
            If CLIAction = CLIActionEnum.RemoveAllConnectedPrintersFromRemoteMachine Then
                Dim disthandler As New DistributionHelper
                Dim resultclass As RemoteFileClass
                resultclass = disthandler.LoadPrinterProfileFromClient(Clientname, AppSettings)
                For Each item As RemoteFilePrinterConnectItem In resultclass.ConnectPrinters
                    If (Not item.Printserver = "Lokal") And (Not item.Printserver = "Local") Then
                        Dim jj As New RemoteFilePrinterDisconnectItem
                        jj.PrinterName = item.PrinterName
                        jj.PrintServer = item.Printserver
                        resultclass.DisconnectPrinters.Add(jj)
                    End If
                Next
                resultclass.ConnectPrinters.Clear()
                If disthandler.PublishProfileToClient(Clientname, "", True, False, resultclass, AppSettings) Then
                    Return True
                Else
                    Return False
                End If
            End If
            If CLIAction = CLIActionEnum.BackupPrintersFromLocalMachine Then
                Dim RemoteFileService As New RemoteFileCreator
                RemoteFileService.CreateMultiplePrinterRemoteFile(BackupFilePath, MainForm.LoadLocalPrinters)
                Return True
            End If
            If CLIAction = CLIActionEnum.RestorePrintersToLocalMachine Then
                Dim jj As New RemoteFileSerializer
                ConnectPrinterCollectionFunc(jj.LoadRemoteFile(RestoreFilePath).ConnectPrinters)
                Return True
            End If
            If CLIAction = CLIActionEnum.BackupPrintersFromClient Then
                Dim disthandler As New DistributionHelper
                AppSettings.IgnoreLocalPrintersAtRemoteFetching = True
                Dim jj As New RemoteFileSerializer
                jj.SaveRemoteFile(disthandler.LoadPrinterProfileFromClient(Clientname, AppSettings), BackupFilePath)
                Return True
            End If
            If CLIAction = CLIActionEnum.RestoreBackupToClient Or CLIAction = CLIActionEnum.ApplyProfileToRemoteClient Then
                Dim disthandler As New DistributionHelper
                AppSettings.IgnoreLocalPrintersAtRemoteFetching = True
                Dim jj As New RemoteFileSerializer
                disthandler.PublishProfileToClient(Clientname, "", True, False, jj.LoadRemoteFile(RestoreFilePath), AppSettings)
                Return True
            End If
            If CLIAction = CLIActionEnum.ConnectLocalPrinters Then
                ConnectPrinterCollectionFunc(ConnectPrinterCollection)
                Return True
            End If
            If CLIAction = CLIActionEnum.SetLocalDefaultPrinter Then
                For Each item As RemoteFilePrinterConnectItem In ConnectPrinterCollection
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
                        Return False
                    End Try
                Next
                Return True
            End If
            If CLIAction = CLIActionEnum.ListLocalPrinters Then
                ListPrinterToOutput(MainForm.LoadLocalPrinters)
                Return True
            End If
            If CLIAction = CLIActionEnum.ListConnectedLocalPrinters Then
                ListPrinterToOutput(MainForm.LoadLocalPrinters, True)
                Return True
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
    End Sub

    Sub ListPrinterRemoteFileToOutput(ByVal PrinterCollection As RemoteFileClass, Optional ByVal OnlyConnectedPrinters As Boolean = False)
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
            Console.WriteLine("-RLP" & vbTab & vbTab & "Disconnect printers from local machine: <Printer share name> <Print server>")
            Console.WriteLine("-RRP" & vbTab & vbTab & "Disconnect printers from remote machine: <Hostname> <Printer share name> <Print server>")
            Console.WriteLine("-CALP" & vbTab & vbTab & "Remove all printers from local machine")
            Console.WriteLine("-CACLP" & vbTab & vbTab & "Remove all connected printers from local machine")
            Console.WriteLine("-CACRP" & vbTab & vbTab & "Remove all connected printers from remote machine: <Hostname>")
            Console.WriteLine("-CLDP" & vbTab & vbTab & "Change local machine default printer: <Printer share name> <Print server>")
            Console.WriteLine("-CRDP" & vbTab & vbTab & "Change remote machine default printer: <Hostname> <Printer share name> <Print server>")
            Console.WriteLine("-LLP" & vbTab & vbTab & "List all local machine printers")
            Console.WriteLine("-LRP" & vbTab & vbTab & "List all remote machine printers: <Hostname>")
            Console.WriteLine("-LLCP" & vbTab & vbTab & "List all local connected printers")
            Console.WriteLine("-LRCP" & vbTab & vbTab & "List all remote machine connected printers: <Hostname>")
            Console.WriteLine("-BPEL" & vbTab & vbTab & "Backup all printers from local machine: <Filename>")
            Console.WriteLine("-RPEL" & vbTab & vbTab & "Restore all printers to local machine: <Filename>")
            Console.WriteLine("-BPERC" & vbTab & vbTab & "Backup all printers from remote client: <Hostname> <Filename>")
            Console.WriteLine("-RPERC" & vbTab & vbTab & "Restore all printers to remote client: <Hostname> <Filename>")
            Console.WriteLine("-ARPPF" & vbTab & vbTab & "Apply profile file to remote machine: <Hostname> <Filename>")
        End If
    End Sub
End Class
