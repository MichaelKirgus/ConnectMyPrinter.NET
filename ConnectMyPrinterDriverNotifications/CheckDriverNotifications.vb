Public Class CheckDriverNotifications
    Public Function CheckForNotifications(ByVal DriverName As String, ByVal DriverNotificationsList As List(Of DriverNotifications)) As String
        Try
            For Each item As DriverNotifications In DriverNotificationsList
                If DriverName.Contains(item.ContainsStringInDriverName) Then
                    Return item.RtfFile
                End If
            Next

            Return ""
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Class
