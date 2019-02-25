Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Linq

Public Class Main
    Sub Main(args As String())
        Try
            Dim arglist As List(Of String)
            arglist = args.ToList

            For ind = 0 To arglist.Count - 1
                Try
                    If arglist(ind).StartsWith("/KP") Then
                        If KillProcess(arglist(ind) + 1) Then
                            Environment.ExitCode = 0
                        Else
                            Environment.ExitCode = 1
                        End If
                    End If
                    If arglist(ind).StartsWith("/CP") Then
                        If CopyFile(arglist(ind) + 1, arglist(ind) + 2) Then
                            Environment.ExitCode = 0
                        Else
                            Environment.ExitCode = 1
                        End If
                    End If
                    If arglist(ind).StartsWith("/RP") Then
                        If RunAppInBackground(arglist(ind) + 1, arglist(ind) + 2) Then
                            Environment.ExitCode = 0
                        Else
                            Environment.ExitCode = 1
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
        End Try
    End Sub

    Public Function KillProcess(ByVal AppName As String) As Boolean
        Try
            Dim zz As Process()
            zz = Process.GetProcessesByName(AppName)

            Dim proclist As List(Of Process)
            proclist = zz.ToList

            For ind = 0 To proclist.Count - 1
                proclist(ind).CloseMainWindow()
                proclist(ind).WaitForExit(2000)
                proclist(ind).Kill()
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CopyFile(ByVal TargetFileName As String, ByVal DestinationFileName As String) As Boolean
        Try
            IO.File.Copy(TargetFileName, DestinationFileName, True)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function RunAppInBackground(ByVal Filename As String, ByVal CMDArgs As String) As Boolean
        Try
            Dim ww As New Process
            ww.StartInfo.FileName = Filename
            ww.StartInfo.Arguments = CMDArgs
            ww.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            ww.Start()
            ww.WaitForExit(60000)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
