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
