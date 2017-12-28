Public Class ElevationHelper
    Public Function RunAppAsAdmin(ByVal AppPath As String, Optional ByVal WorkingDir As String = "", Optional ByVal Arguments As String = "") As Boolean
        Try
            Dim jj As New Process
            jj.StartInfo.FileName = AppPath
            If Not WorkingDir = "" Then
                jj.StartInfo.WorkingDirectory = WorkingDir
            End If
            If Not Arguments = "" Then
                jj.StartInfo.Arguments = Arguments
            End If

            jj.StartInfo.Verb = "runas"
            jj.Start()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
