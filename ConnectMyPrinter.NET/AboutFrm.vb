'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Public Class AboutFrm
    Public _parentFrm As Form1
    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
        Me.Close()
    End Sub

    Private Sub AboutFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Style = _parentFrm.Style
            Me.MetroTabControl1.Style = _parentFrm.MetroTabControl1.Style
        Catch ex As Exception
        End Try

        Try
            MetroTextBox1.Text = My.Computer.FileSystem.ReadAllText("Lizenzbedingungen.txt", System.Text.Encoding.Default)
            MetroTextBox2.Text = My.Computer.FileSystem.ReadAllText("MetroFramework.txt", System.Text.Encoding.Default)
            MetroTextBox3.Text = My.Computer.FileSystem.ReadAllText("OpenIconLibraryLicense.txt", System.Text.Encoding.Default)

            MetroTabControl1.SelectedIndex = 0
            MetroTextBox1.Select(0, 0)
        Catch ex As Exception
        End Try

        Try
            AppVersionLbl.Text = My.Application.Info.Version.ToString
            UserAboutInfoLbl.Text = _parentFrm.AppSettings.AdditionalAboutInformation
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MetroLink1_Click(sender As Object, e As EventArgs) Handles MetroLink1.Click
        Try
            Dim kk As New Process
            kk.StartInfo.FileName = "https://github.com/MichaelKirgus/ConnectMyPrinter.NET"
            kk.Start()
        Catch ex As Exception
        End Try
    End Sub
End Class