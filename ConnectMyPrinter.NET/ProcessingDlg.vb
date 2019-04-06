Public Class ProcessingDlg
    Public _parent As Form1
    Private Sub ProcessingDlg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Style = _parent.Style
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ProcessingDlg_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            _parent.TopMost = True
            If Not _parent.AppSettings.ShowTopMost Then
                _parent.TopMost = False
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class