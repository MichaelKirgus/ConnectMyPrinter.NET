'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Public Class SelectFromMultipleServersDlg
    Public _parent As Form1
    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub SelectFromMultipleServersDlg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Style = _parent.Style
        Catch ex As Exception
        End Try
        Try
            ListBox1.SelectedItem = ListBox1.Items(0)
        Catch ex As Exception
        End Try
    End Sub
End Class