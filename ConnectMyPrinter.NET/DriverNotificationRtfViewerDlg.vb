'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Public Class DriverNotificationRtfViewerDlg
    Public _parent As Form1
    Private Sub ConnectPrinterDlg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Style = _parent.Style
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MetroButton2_Click(sender As Object, e As EventArgs) Handles MetroButton2.Click
        Me.DialogResult = DialogResult.No
        Me.Close()
    End Sub

    Private Sub MetroButton1_Click(sender As Object, e As EventArgs)
        Me.DialogResult = DialogResult.Yes
        Me.Close()
    End Sub
End Class