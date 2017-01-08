Public Class SelectFromMultipleServersDlg
    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub SelectFromMultipleServersDlg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ListBox1.SelectedItem = ListBox1.Items(0)
        Catch ex As Exception
        End Try
    End Sub
End Class