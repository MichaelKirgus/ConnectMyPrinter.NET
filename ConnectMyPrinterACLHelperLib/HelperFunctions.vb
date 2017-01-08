Imports System
Imports System.Security.Principal
Imports System.ServiceProcess
Imports System.Threading

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
End Class
