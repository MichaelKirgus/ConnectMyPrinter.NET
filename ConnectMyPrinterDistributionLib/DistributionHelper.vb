Imports System.Globalization
Imports ConnectMyPrinterAppSettingsHandler
Imports ConnectMyPrinterLanguageHelper
Imports ConnectMyPrinterRemoteFileHandler

Public Class DistributionHelper
    Public MLangHelper As New LanguageApplyHelper
    Public MCultureInf As CultureInfo = CultureInfo.CurrentUICulture

    Public Function PublishProfileToClient(ByVal Clientname As String, ByVal Username As String, ByVal LocalMachine As Boolean, ByVal Permanent As Boolean, ByVal RemoteFileProfile As RemoteFileClass, ByVal AppSettingsClass As AppSettingsClass, Optional ByVal Silent As Boolean = False, Optional PingClients As Boolean = False, Optional ByVal CheckForTraceAdminPath As Boolean = False) As Boolean
        Try
            If Not AppSettingsClass.UseTracePathFeature Then
                If Silent = False Then
                    MsgBox(MLangHelper.GetCultureString("ConnectMyPrinterDistributionLib.TranslatedStrings", GetType(DistributionHelper), MCultureInf, "PrinterPushFeatureNotEnabledStr", ""), MsgBoxStyle.Exclamation)
                End If
                Return False
            End If

            Dim clientlist As New List(Of String)
            If Clientname.Contains(";") Then
                Dim spliarr As Array
                spliarr = Clientname.Split(";")
                clientlist.AddRange(spliarr)
            Else
                clientlist.Add(Clientname)

                If PingClients Then
                    If Not My.Computer.Network.Ping(Clientname, 1000) Then
                        Return False
                    Else
                        If CheckForTraceAdminPath Then
                            If Not IO.Directory.Exists("\\" & Clientname & AppSettingsClass.ActionsTraceAdminPath) Then
                                Return False
                            End If
                        End If
                    End If
                End If
            End If

            Dim uuid As String
            uuid = Guid.NewGuid.ToString.Replace("{", "").Replace("}", "")

            Dim LogTxt As String = ""

            For index = 0 To clientlist.Count - 1
                If Not clientlist(index) = "" Then
                    Dim filename As String
                    Dim options As String = ""
                    If Permanent Then
                        options = "permanent"
                    End If
                    If LocalMachine Then
                        filename = "\\" & clientlist(index) & Environment.ExpandEnvironmentVariables(AppSettingsClass.ActionsTraceAdminPath) & "\LM_" & uuid & "_" & options & ".prpr"
                    Else
                        filename = "\\" & clientlist(index) & Environment.ExpandEnvironmentVariables(AppSettingsClass.ActionsTraceAdminPath) & "\" & Username.ToLower & "_" & uuid & "_" & options & ".prpr"
                    End If

                    Try
                        If PingClients Then
                            If Not My.Computer.Network.Ping(clientlist(index), 1000) Then
                                Exit Try
                            Else
                                If CheckForTraceAdminPath Then
                                    If Not IO.Directory.Exists("\\" & clientlist(index) & AppSettingsClass.ActionsTraceAdminPath) Then
                                        Exit Try
                                    End If
                                End If
                            End If
                        End If

                        If IO.Directory.Exists("\\" & clientlist(index) & AppSettingsClass.ActionsTraceAdminPath) Then
                            If Not IO.File.Exists(filename) Then
                                Dim yy As New RemoteFileSerializer
                                If yy.SaveRemoteFile(RemoteFileProfile, filename) Then
                                    LogTxt += MLangHelper.GetCultureString("ConnectMyPrinterDistributionLib.TranslatedStrings", GetType(DistributionHelper), MCultureInf, "ProfilePublishedSuccessStr1", "") & clientlist(index) & MLangHelper.GetCultureString("ConnectMyPrinterDistributionLib.TranslatedStrings", GetType(DistributionHelper), MCultureInf, "ProfilePublishedSuccessStr2", "") & vbNewLine
                                Else
                                    LogTxt += MLangHelper.GetCultureString("ConnectMyPrinterDistributionLib.TranslatedStrings", GetType(DistributionHelper), MCultureInf, "ProfilePublishedFailStr1", "") & clientlist(index) & MLangHelper.GetCultureString("ConnectMyPrinterDistributionLib.TranslatedStrings", GetType(DistributionHelper), MCultureInf, "ProfilePublishedFailStr2", "") & vbNewLine
                                End If
                            End If
                        End If
                    Catch ex As Exception
                    End Try
                End If
            Next

            If Silent = False Then
                MsgBox(LogTxt, MsgBoxStyle.Information)
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function RequestPrinterProfileFromClient(ByVal Clientname As String, ByVal AppSettingsClass As AppSettingsClass, Optional PingClients As Boolean = False, Optional ByVal CheckForTraceAdminPath As Boolean = False) As Boolean
        Try
            Dim uuid As String
            uuid = Guid.NewGuid.ToString.Replace("{", "").Replace("}", "")

            If PingClients Then
                If Not My.Computer.Network.Ping(Clientname, 1000) Then
                    Return False
                Else
                    If CheckForTraceAdminPath Then
                        If Not IO.Directory.Exists("\\" & Clientname & AppSettingsClass.ActionsTraceAdminPath) Then
                            Return False
                        End If
                    End If
                End If
            End If

            IO.File.WriteAllText("\\" & Clientname & Environment.ExpandEnvironmentVariables(AppSettingsClass.ActionsTraceAdminPath) & "\REQ_" & uuid & "_.prpr", "")

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetRequestedProfileFromClient(ByVal Clientname As String, ByVal AppSettingsClass As AppSettingsClass, Optional PingClients As Boolean = False, Optional ByVal CheckForTraceAdminPath As Boolean = False) As RemoteFileClass
        Try
            If PingClients Then
                If Not My.Computer.Network.Ping(Clientname, 1000) Then
                    Return New RemoteFileClass
                Else
                    If CheckForTraceAdminPath Then
                        If Not IO.Directory.Exists("\\" & Clientname & AppSettingsClass.ActionsTraceAdminPath) Then
                            Return New RemoteFileClass
                        End If
                    End If
                End If
            End If

            If IO.File.Exists("\\" & Clientname & Environment.ExpandEnvironmentVariables(AppSettingsClass.ActionsTraceAdminPath) & "\RESULT.prpr") Then
                Debug.WriteLine("File exists")
                Dim yy As New RemoteFileSerializer
                Dim reqfile As RemoteFileClass
                reqfile = yy.LoadRemoteFile("\\" & Clientname & Environment.ExpandEnvironmentVariables(AppSettingsClass.ActionsTraceAdminPath) & "\RESULT.prpr")

                If AppSettingsClass.IgnoreLocalPrintersAtRemoteFetching Then
                    If Not reqfile.ConnectPrinters.Count = 0 Then
                        Dim newcoll As New List(Of RemoteFilePrinterConnectItem)

                        For Each item As RemoteFilePrinterConnectItem In reqfile.ConnectPrinters
                            If (Not item.Printserver = "Lokal") And (Not item.Printserver = "Local") And (Not item.Printserver.ToLower = Clientname.ToLower) Then
                                newcoll.Add(item)
                            End If
                        Next

                        reqfile.ConnectPrinters.Clear()
                        reqfile.ConnectPrinters = newcoll
                    End If
                End If

                Return reqfile
            Else
                Return New RemoteFileClass
            End If
        Catch ex As Exception
            Return New RemoteFileClass
        End Try
    End Function

    Public Function CleanOldRequestFiles(ByVal Clientname As String, ByVal AppSettingsClass As AppSettingsClass) As Boolean
        Try
            For Each item As String In IO.Directory.GetFiles("\\" & Clientname & Environment.ExpandEnvironmentVariables(AppSettingsClass.ActionsTraceAdminPath))
                Dim kk As New IO.FileInfo(item)
                If kk.Name.StartsWith("REQ") Then
                    IO.File.Delete(item)
                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function LoadPrinterProfileFromClient(ByVal Clientname As String, ByVal AppSettingsClass As AppSettingsClass, Optional PingClients As Boolean = False, Optional ByVal CheckForTraceAdminPath As Boolean = False) As RemoteFileClass
        RequestPrinterProfileFromClient(Clientname, AppSettingsClass, PingClients, CheckForTraceAdminPath)
        Dim counter As Integer = 0
        Debug.WriteLine("\\" & Clientname & Environment.ExpandEnvironmentVariables(AppSettingsClass.ActionsTraceAdminPath) & "\RESULT.prpr")

        Do Until counter = 500
            Threading.Thread.Sleep(10)
            If IO.File.Exists("\\" & Clientname & Environment.ExpandEnvironmentVariables(AppSettingsClass.ActionsTraceAdminPath) & "\RESULT.prpr") Then
                Exit Do
            End If
            counter += 1
        Loop
        If Not counter = 500 Then
            Threading.Thread.Sleep(150)
            Dim rs1 As RemoteFileClass
            rs1 = GetRequestedProfileFromClient(Clientname, AppSettingsClass, PingClients, CheckForTraceAdminPath)

            If Not rs1.ConnectPrinters.Count = 0 Then
                IO.File.Delete("\\" & Clientname & Environment.ExpandEnvironmentVariables(AppSettingsClass.ActionsTraceAdminPath) & "\RESULT.prpr")
                CleanOldRequestFiles(Clientname, AppSettingsClass)
                Return rs1
            End If
        End If

        Return New RemoteFileClass
    End Function
End Class
