'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Public Class PrinterDriverDependenciesProvider
    Public Function GetPrintersWithSameDriverName(ByVal LocalPrinters As List(Of PrinterQueueInfo), ByVal CurrentPrinter As PrinterQueueInfo) As List(Of PrinterQueueInfo)
        Try
            Dim PrintersResult As New List(Of PrinterQueueInfo)

            For index = 0 To LocalPrinters.Count - 1
                If LocalPrinters(index).DriverName = CurrentPrinter.DriverName Then
                    PrintersResult.Add(LocalPrinters(index))
                End If
            Next

            Return PrintersResult
        Catch ex As Exception
            Return New List(Of PrinterQueueInfo)
        End Try
    End Function
End Class
