﻿Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.IO
Imports System.Linq
Imports System.Security.AccessControl
Imports System.Windows.Forms

Class MyApplicationContext
    Inherits ApplicationContext

    Public Shared Sub Main(args() As String)
        Application.EnableVisualStyles()
        Application.Run(New MyApplicationContext)
    End Sub

    Public Sub New()
        Console.WriteLine("Starting...")
        If ProcessCMD() Then
            Console.WriteLine("Success!")
        Else
            Console.WriteLine("Failed!")
        End If
        Console.WriteLine("Exit app...")
        ExitApplication()
    End Sub

    Public Function ProcessCMD()
        Try
            Dim arglist As List(Of String)
            arglist = Environment.GetCommandLineArgs().ToList

            If arglist.Count = 0 Or arglist.Count = 1 Then
                ExitApplication()
            End If

            For ind = 1 To arglist.Count - 1
                Try
                    If arglist(ind).StartsWith("/KP") Then
                        If KillProcess(arglist(ind + 1)) Then
                            Environment.ExitCode = 0
                        Else
                            Environment.ExitCode = 1
                        End If
                    End If
                    If arglist(ind).StartsWith("/CP") Then
                        If CopyFile(arglist(ind + 1), arglist(ind + 2)) Then
                            Environment.ExitCode = 0
                        Else
                            Environment.ExitCode = 1
                        End If
                    End If
                    If arglist(ind).StartsWith("/RP") Then
                        If RunAppInBackground(arglist(ind + 1), arglist(ind + 2)) Then
                            Environment.ExitCode = 0
                        Else
                            Environment.ExitCode = 1
                        End If
                    End If
                    If arglist(ind).StartsWith("/RB") Then
                        If RunBatchInBackground(arglist(ind + 1)) Then
                            Environment.ExitCode = 0
                        Else
                            Environment.ExitCode = 1
                        End If
                    End If
                    If arglist(ind).StartsWith("/AR") Then
                        If ApplyFileACLs(arglist(ind + 1)) Then
                            Environment.ExitCode = 0
                        Else
                            Environment.ExitCode = 1
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ApplyFileACLs(ByVal Filename As String) As Boolean
        Try
            AddFileSecurity(Filename, "Everyone", FileSystemRights.FullControl, AccessControlType.Allow)
            AddFileSecurity(Filename, "Jeder", FileSystemRights.FullControl, AccessControlType.Allow)
            AddFileSecurity(Filename, "Benutzer", FileSystemRights.FullControl, AccessControlType.Allow)
            AddFileSecurity(Filename, "User", FileSystemRights.FullControl, AccessControlType.Allow)
            AddFileSecurity(Filename, Environment.UserName, FileSystemRights.FullControl, AccessControlType.Allow)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function AddFileSecurity(ByVal fileName As String, ByVal account As String,
    ByVal rights As FileSystemRights, ByVal controlType As AccessControlType)
        Try
            ' Get a FileSecurity object that represents the 
            ' current security settings.
            Dim fSecurity As FileSecurity = File.GetAccessControl(fileName)

            ' Add the FileSystemAccessRule to the security settings. 
            Dim accessRule As FileSystemAccessRule =
                New FileSystemAccessRule(account, rights, controlType)

            fSecurity.AddAccessRule(accessRule)

            ' Set the new access settings.
            File.SetAccessControl(fileName, fSecurity)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function KillProcess(ByVal AppName As String) As Boolean
        Try
            Dim zz As Process()
            zz = Process.GetProcessesByName(AppName)

            Dim proclist As List(Of Process)
            proclist = zz.ToList

            For ind = 0 To proclist.Count - 1
                Try
                    proclist(ind).CloseMainWindow()
                    proclist(ind).WaitForExit(2000)
                    proclist(ind).Kill()
                Catch ex As Exception
                End Try
            Next

            Return True
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    Public Function CopyFile(ByVal TargetFileName As String, ByVal DestinationFileName As String) As Boolean
        Try
            IO.File.Copy(TargetFileName, DestinationFileName, True)

            Return True
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    Public Function RunAppInBackground(ByVal Filename As String, ByVal CMDArgs As String) As Boolean
        Try
            Dim ww As New Process
            ww.StartInfo.FileName = Filename
            ww.StartInfo.Arguments = CMDArgs
            ww.StartInfo.WorkingDirectory = Environment.CurrentDirectory
            ww.StartInfo.UseShellExecute = False
            ww.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            ww.Start()
            ww.WaitForExit(60000)

            Return True
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    Public Function RunBatchInBackground(ByVal Filename As String) As Boolean
        Try
            Dim ww As New Process
            ww.StartInfo.FileName = Filename
            ww.StartInfo.WorkingDirectory = Environment.CurrentDirectory
            ww.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            ww.StartInfo.UseShellExecute = False
            ww.StartInfo.CreateNoWindow = True
            ww.Start()
            ww.WaitForExit(60000)

            Return True
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function
End Class
