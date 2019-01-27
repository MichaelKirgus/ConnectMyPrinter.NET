Imports ConnectMyPrinterRemoteFileHandler

Public Class Form1
    Public RemoteFile As New RemoteFileClass

    Public Sub LoadNewRemoteFile()
        PropertyGrid1.SelectedObject = RemoteFile
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim RemoteFilePath As String = ""
            For Each argument In My.Application.CommandLineArgs
                RemoteFilePath = argument
            Next
            If IO.File.Exists(RemoteFilePath) Then
                'Aktionen ausführen, Anwendung nicht im Audit-Modus öffnen...
                Dim kk As New ProcessDlg
                kk.RemoteFilePath = RemoteFilePath
                kk.Show()
                Me.Close()
            End If

            LoadNewRemoteFile()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        Dim yy As New RemoteFileSerializer
        RemoteFile = yy.LoadRemoteFile(OpenFileDialog1.FileName)
    End Sub

    Private Sub SaveFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        Dim yy As New RemoteFileSerializer
        yy.SaveRemoteFile(RemoteFile, SaveFileDialog1.FileName)
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        SaveFileDialog1.ShowDialog()
    End Sub

    Public Sub ApplPrinterConfigToClass()
        If Not RemoteFile.ConnectPrinters.Count = 0 Then
            Dim kk As MsgBoxResult
            kk = MsgBox("Es ist bereits ein oder mehrere Drucker in der Konfiguration vorhanden. Drucker überschreiben?", MsgBoxStyle.YesNo)

            If kk = MsgBoxResult.Yes Then
                RemoteFile.ConnectPrinters.Clear()
            End If
        End If

        Dim qq As New RemoteFilePrinterConnectItem
        qq.PrinterName = TextBox1.Text
        qq.Printserver = TextBox2.Text
        qq.SetDefaultPrinter = CheckBox1.Checked

        RemoteFile.ConnectPrinters.Add(qq)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ApplPrinterConfigToClass()
        LoadNewRemoteFile()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ApplPrinterConfigToClass()
        LoadNewRemoteFile()
        ToolStripButton2.PerformClick()
    End Sub
End Class
