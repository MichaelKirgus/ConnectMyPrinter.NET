'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports System.Windows.Forms

Public Class Logging
    Public Property Enable As Boolean = False
    Public Property LogFile As String = "Log.txt"
    Public Property _InformationTypeDesc As String = "INFORMATION"
    Public Property _WarningTypeDesc As String = "WARNING"
    Public Property _ErrorTypeDesc As String = "ERROR"

    Public Enum LogType
        Information = 0
        Warning = 1
        _Error = 2
    End Enum

    Public Sub WriteLogSystemInfo()
        If Enable = False Then Exit Sub
        Try
            Dim datetime As String = DateAndTime.Now()
            Dim sysinfo As String
            sysinfo = datetime & " ----Start der Logdatei----" & vbNewLine
            sysinfo += datetime & " Anwendungsversion: " & My.Application.Info.Version.ToString & vbNewLine
            sysinfo += datetime & " OS: " & My.Computer.Info.OSFullName & vbNewLine

            sysinfo += datetime & " Architektur: "
            If Environment.Is64BitOperatingSystem Then
                sysinfo += "64-Bit" & vbNewLine
            Else
                sysinfo += "32-Bit" & vbNewLine
            End If

            sysinfo += datetime & " User: " & My.User.Name & vbNewLine & vbNewLine

            IO.File.AppendAllText(LogFile, sysinfo)
        Catch ex As Exception
        End Try
    End Sub


    Public Sub Write(ByVal _LogType As LogType, ByVal _target As Form, ByVal Text As String, Optional ByVal ErrorInfo As ErrObject = Nothing)
        If Enable = False Then Exit Sub
        Try
            Dim TypeStr As String = ""
            If _LogType = LogType.Information Then TypeStr = _InformationTypeDesc
            If _LogType = LogType.Warning Then TypeStr = _WarningTypeDesc
            If _LogType = LogType._Error Then TypeStr = _ErrorTypeDesc

            Dim DateStr As String = DateAndTime.Now()

            Dim FormInfo As String = ""
            Try
                FormInfo = _target.Name & ", " & _target.Parent.Name
            Catch ex As Exception
                Try
                    FormInfo = _target.Name
                Catch ex2 As Exception
                End Try
            End Try

            Dim ErrorInfoStr As String = ""
            If _LogType = LogType._Error Then
                Try
                    ErrorInfoStr = ErrorInfo.Description & ", " & ErrorInfo.Erl & ", " & ErrorInfo.LastDllError & ", " & ErrorInfo.Source
                Catch ex As Exception
                    Try
                        ErrorInfoStr = ErrorInfo.Description
                    Catch ex2 As Exception
                    End Try
                End Try
            End If

            Dim logstr As String
            logstr = DateStr & " " & TypeStr & " " & FormInfo & " " & Text & " " & ErrorInfoStr & vbNewLine

            IO.File.AppendAllText(LogFile, logstr)
        Catch ex As Exception
        End Try
    End Sub
End Class
