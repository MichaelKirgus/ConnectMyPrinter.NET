﻿Public Class ConnectPrinterDlg

    Private Sub MetroButton2_Click(sender As Object, e As EventArgs) Handles MetroButton2.Click
        Me.DialogResult = DialogResult.No
        Me.Close()
    End Sub

    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
        Me.DialogResult = DialogResult.Yes
        Me.Close()
    End Sub
End Class