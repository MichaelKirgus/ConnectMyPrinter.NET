Public Class DeletePrinterDriverDlg
    Public _parent As Form1
    Private Sub DeletePrinterDriverDlg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Style = _parent.Style
        Catch ex As Exception
        End Try
    End Sub
End Class