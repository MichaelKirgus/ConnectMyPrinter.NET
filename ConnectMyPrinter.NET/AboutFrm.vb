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
End Class