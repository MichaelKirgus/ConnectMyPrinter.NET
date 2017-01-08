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
End Class
