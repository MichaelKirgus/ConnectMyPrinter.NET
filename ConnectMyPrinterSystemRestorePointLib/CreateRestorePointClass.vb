Public Class CreateRestorePointClass
    Public Function CreatePoint(ByVal Name As String, ByVal Wait As Boolean) As Boolean
        Try
            Shell("Wmic.exe /Namespace:\\root\default Path SystemRestore Call CreateRestorePoint " & My.Resources.trenn & Name & My.Resources.trenn & ", 100, 1", AppWinStyle.Hide, Wait)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
