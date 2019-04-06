'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
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