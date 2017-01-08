Public Class AboutFrm
    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
        Me.Close()
    End Sub

    Private Sub AboutFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            MetroTextBox1.Text = My.Computer.FileSystem.ReadAllText("Lizenzbedingungen.txt")
            MetroTextBox2.Text = My.Computer.FileSystem.ReadAllText("MetroFramework.txt")
        Catch ex As Exception
        End Try
    End Sub
End Class