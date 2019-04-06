'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports ConnectMyPrinterAppSettingsHandler
Imports ConnectMyPrinterEnumerationLib
Imports ConnectMyPrinterRemoteFileHandler

Public Class ReportingLib
    Public Function CheckForFolderStructure(ByVal AppSettings As AppSettingsClass, ByVal Hostname As String, ByVal Username As String, ByVal Domain As String) As Boolean
        Try
            Dim reportpath As String
            reportpath = Environment.ExpandEnvironmentVariables(AppSettings.ReportingPath)
            If Not reportpath = "" Then
                'Existiert die Freigabe für das Reporting?
                If IO.Directory.Exists(reportpath) Then
                    If Not IO.Directory.Exists(reportpath & "\" & Domain.ToUpper) Then
                        IO.Directory.CreateDirectory(reportpath & "\" & Domain.ToUpper)
                    End If
                    If Not IO.Directory.Exists(reportpath & "\" & Domain.ToUpper & "\" & Hostname.ToUpper) Then
                        IO.Directory.CreateDirectory(reportpath & "\" & Domain.ToUpper & "\" & Hostname.ToUpper)
                    Else
                        Try
                            IO.Directory.SetLastWriteTime(reportpath & "\" & Domain.ToUpper & "\" & Hostname.ToUpper, DateAndTime.Now)
                        Catch ex As Exception
                        End Try
                    End If
                    If Not IO.Directory.Exists(reportpath & "\" & Domain.ToUpper & "\" & Hostname.ToUpper & "\" & Username.ToUpper) Then
                        IO.Directory.CreateDirectory(reportpath & "\" & Domain.ToUpper & "\" & Hostname.ToUpper & "\" & Username.ToUpper)
                    Else
                        Try
                            IO.Directory.SetLastWriteTime(reportpath & "\" & Domain.ToUpper & "\" & Hostname.ToUpper & "\" & Username.ToUpper, DateAndTime.Now)
                        Catch ex As Exception
                        End Try
                    End If

                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CheckIfUserIsBlacklisted(ByVal AppSettings As AppSettingsClass, ByVal Username As String) As Boolean
        Try
            If Not AppSettings.ReportingUserBlacklist = "" Then
                If AppSettings.ReportingUserBlacklist.Contains(";") Then
                    Dim splitobj As Array
                    splitobj = AppSettings.ReportingUserBlacklist.Split(";")

                    For index = 0 To splitobj.Length - 1
                        If splitobj(index).ToString.ToLower = Username.ToLower Then
                            Return True
                        End If
                    Next

                    Return False
                Else
                    If AppSettings.ReportingUserBlacklist.ToLower = Username.ToLower Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            Else
                Return False
            End If

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SavePrinterProfileToReportingPath(ByVal AppSettings As AppSettingsClass, ByVal MainThread As ConnectMyPrinter.NET.Form1, ByVal PrinterCollection As List(Of PrinterQueueInfo), ByVal Hostname As String, ByVal Username As String, ByVal Domain As String, Optional ByVal UseOwnMainThreadInstance As Boolean = False, Optional ByVal UsePrinterCollection As Boolean = False) As Boolean
        Try
            Dim reportpath As String
            reportpath = Environment.ExpandEnvironmentVariables(AppSettings.ReportingPath)

            Dim ConnectedPrinters As New List(Of PrinterQueueInfo)
            If UsePrinterCollection = False Then
                Dim LocalPrinters As List(Of PrinterQueueInfo)
                If UseOwnMainThreadInstance Then
                    LocalPrinters = MainThread.LoadLocalPrinters()
                Else
                    Dim MainApp As New ConnectMyPrinter.NET.Form1
                    MainApp.AppSettings = AppSettings
                    LocalPrinters = MainApp.LoadLocalPrinters()
                End If

                For Each item As PrinterQueueInfo In LocalPrinters
                    If AppSettings.IgnoreLocalPrintersAtReporting Then
                        If (Not item.Server = "Lokal") And (Not item.Server = "Local") Then
                            ConnectedPrinters.Add(item)
                        End If
                    Else
                        ConnectedPrinters.Add(item)
                    End If
                Next
            Else
                ConnectedPrinters.AddRange(PrinterCollection)
            End If

            Dim RemoteFileService As New RemoteFileCreator
            RemoteFileService.CreateMultiplePrinterRemoteFile(reportpath & "\" & Domain.ToUpper & "\" & Hostname.ToUpper & "\" & Username.ToUpper & "\Profile.prpr", ConnectedPrinters)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SavePrinterEnvironmentToCSV(ByVal AppSettings As AppSettingsClass, ByVal MainThread As ConnectMyPrinter.NET.Form1, ByVal PrinterCollection As List(Of PrinterQueueInfo), ByVal Hostname As String, ByVal Username As String, ByVal Domain As String, Optional ByVal UseOwnMainThreadInstance As Boolean = False, Optional ByVal UsePrinterCollection As Boolean = False) As Boolean
        Try
            Dim reportpath As String
            reportpath = Environment.ExpandEnvironmentVariables(AppSettings.ReportingPath)

            Dim ConnectedPrinters As New List(Of PrinterQueueInfo)
            If UsePrinterCollection = False Then
                Dim LocalPrinters As List(Of PrinterQueueInfo)
                If UseOwnMainThreadInstance Then
                    LocalPrinters = MainThread.LoadLocalPrinters()
                Else
                    Dim MainApp As New ConnectMyPrinter.NET.Form1
                    MainApp.AppSettings = AppSettings
                    LocalPrinters = MainApp.LoadLocalPrinters()
                End If

                For Each item As PrinterQueueInfo In LocalPrinters
                    If AppSettings.IgnoreLocalPrintersAtReporting Then
                        If (Not item.Server = "Lokal") And (Not item.Server = "Local") Then
                            ConnectedPrinters.Add(item)
                        End If
                    Else
                        ConnectedPrinters.Add(item)
                    End If
                Next
            Else
                ConnectedPrinters.AddRange(PrinterCollection)
            End If

            Dim strcoll As String = ""
            For index = 0 To ConnectedPrinters.Count - 1
                strcoll += ConnectedPrinters(index).Server.ToUpper & ";" & ConnectedPrinters(index).ShareName.ToUpper & ";" & ConnectedPrinters(index).DefaultPrinter.ToString & ";" & ConnectedPrinters(index).DriverName & ";" & ConnectedPrinters(index).DriverVersion & vbNewLine
            Next

            IO.File.WriteAllText(reportpath & "\" & Domain.ToUpper & "\" & Hostname.ToUpper & "\" & Username.ToUpper & "\Report.csv", strcoll, Text.Encoding.Default)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
