﻿Public Class AboutFrm
    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
        Me.Close()
    End Sub

    Private Sub AboutFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            MetroTextBox1.Text = My.Computer.FileSystem.ReadAllText("Lizenzbedingungen.txt", System.Text.Encoding.Default)
            MetroTextBox2.Text = My.Computer.FileSystem.ReadAllText("MetroFramework.txt", System.Text.Encoding.Default)
            MetroTextBox3.Text = My.Computer.FileSystem.ReadAllText("OpenIconLibraryLicense.txt", System.Text.Encoding.Default)

            MetroTabControl1.SelectedIndex = 0
            MetroTextBox1.Select(0, 0)
        Catch ex As Exception
        End Try
    End Sub
End Class