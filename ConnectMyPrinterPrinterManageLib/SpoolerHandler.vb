Imports System.ServiceProcess

Public Class SpoolerHandler
    Public Sub WaitForRunningPrintSpooler()
        Try
            Dim sc As New ServiceController("Spooler")

            While sc.Status = ServiceControllerStatus.Stopped
                Threading.Thread.Sleep(100)
            End While
        Catch ex As Exception
        End Try
    End Sub

    Public Sub WaitForStoppedPrintSpooler()
        Try
            Dim sc As New ServiceController("Spooler")

            While sc.Status = ServiceControllerStatus.Running
                Threading.Thread.Sleep(100)
            End While
        Catch ex As Exception
        End Try
    End Sub

    Public Function IsSpoolerRunning() As Boolean
        Try
            Dim sc As New ServiceController("Spooler")

            If sc.Status = ServiceControllerStatus.Running Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
