Imports System
Imports System.Security.AccessControl
Imports System.Security.Principal
Imports System.ServiceProcess
Imports System.Threading
Imports Microsoft.Win32

Public Class HelperFunctions
    Public Function CheckIfInDomain() As Boolean
        Try
            Dim hostname As String
            hostname = My.Computer.Name
            If hostname.ToLower = Environment.UserDomainName.ToLower Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function IsAdmin() As Boolean
        IsAdmin = CType(Thread.CurrentPrincipal, WindowsPrincipal).IsInRole(
          WindowsBuiltInRole.Administrator)
    End Function

    Public Function CheckForSpoolerPermission() As Boolean
        Try
            Dim controller As New ServiceController("spooler")

            controller.Pause()
            controller.Continue()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Function SetLocalMachineSpoolKeyPermissions(ByVal Domain As String, ByVal Username As String) As Boolean
        Try
            Dim user As String = Domain & "\" & Username
            If Domain = "" Then
                'Keine Domäne, lokaler Computer wird eingesetzt.
                user = My.Computer.Name & "\" & Username
            End If
            If user = "Jeder" Or user = "Everyone" Then
                user = Username
            End If
            Dim rs As New RegistrySecurity()

            ' Dem aktuellen Benutzer die Berechtigung zum Ändern und vor allen Löschen der Einträge im Spool-Key geben.
            '
            rs.AddAccessRule(New RegistryAccessRule(user,
            RegistryRights.ReadKey Or RegistryRights.Delete Or RegistryRights.SetValue Or RegistryRights.WriteKey Or RegistryRights.ReadPermissions Or RegistryRights.ChangePermissions Or RegistryRights.FullControl,
            InheritanceFlags.ContainerInherit,
            PropagationFlags.InheritOnly,
            AccessControlType.Allow))

            Dim qq As Microsoft.Win32.RegistryKey
            qq = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print", True)
            qq.SetAccessControl(rs)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Function SetHKEYUsersKeyPermissions(ByVal Domain As String, ByVal Username As String) As Boolean
        Try
            Dim user As String = Domain & "\" & Username
            If Domain = "" Then
                'Keine Domäne, lokaler Computer wird eingesetzt.
                user = My.Computer.Name & "\" & Username
            End If
            If user = "Jeder" Or user = "Everyone" Then
                user = Username
            End If
            Dim rs As New RegistrySecurity()

            ' Dem aktuellen Benutzer die Berechtigung zum Auslesen aller Drucker von jedem Benutzer geben.
            '
            rs.AddAccessRule(New RegistryAccessRule(user,
            RegistryRights.ReadKey Or RegistryRights.ReadPermissions,
            InheritanceFlags.ContainerInherit,
            PropagationFlags.InheritOnly,
            AccessControlType.Allow))

            Dim qq As Microsoft.Win32.RegistryKey
            qq = Registry.Users
            qq.SetAccessControl(rs)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
