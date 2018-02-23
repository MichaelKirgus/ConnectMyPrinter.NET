Public Class ProcessingDlg
    Public _parent As Form1
    Private Sub ProcessingDlg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Style = _parent.Style
        Catch ex As Exception
        End Try
    End Sub
End Class