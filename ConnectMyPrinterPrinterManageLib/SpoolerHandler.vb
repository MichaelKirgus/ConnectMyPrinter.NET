'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
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
