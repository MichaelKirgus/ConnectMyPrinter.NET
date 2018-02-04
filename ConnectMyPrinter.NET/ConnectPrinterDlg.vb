Public Class ConnectPrinterDlg
    Public _parent As Form1

    Private Sub MetroButton2_Click(sender As Object, e As EventArgs) Handles MetroButton2.Click
        Me.DialogResult = DialogResult.No
        Me.Close()
    End Sub

    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
        Me.DialogResult = DialogResult.Yes
        Me.Close()
    End Sub

    Private Sub ConnectPrinterDlg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Style = _parent.Style
        Catch ex As Exception
        End Try
    End Sub
End Class