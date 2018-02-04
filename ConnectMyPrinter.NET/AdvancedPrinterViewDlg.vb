Public Class AdvancedPrinterViewDlg

    Public _AllPrinters As List(Of ConnectMyPrinterEnumerationLib.PrinterQueueInfo)
    Public _parent As Form1

    Private Sub ConnectPrinterDlg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAllItems()
    End Sub

    Public Sub LoadAllItems()
        ListView1.BeginUpdate()

        Try
            For Each item As ConnectMyPrinterEnumerationLib.PrinterQueueInfo In _AllPrinters
                Dim ff As New ListViewItem
                ff.Text = item.ShareName
                ff.SubItems.Add(item.Server)
                ff.SubItems.Add(item.Location)
                ff.SubItems.Add(item.Description)
                ff.SubItems.Add(item.DriverName)
                ff.SubItems.Add(item.State)
                ff.Tag = item

                ListView1.Items.Add(ff)
            Next

            ListView1.EndUpdate()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MetroTextBox1_TextChanged(sender As Object, e As EventArgs) Handles MetroTextBox1.TextChanged
        If MetroTextBox1.Text = "" Then
            Try
                ListView1.EndUpdate()
            Catch ex As Exception
            End Try
            ListView1.Items.Clear()
            LoadAllItems()
        End If
    End Sub

    Private Sub ListView1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListView1.MouseDoubleClick
        Me.TopMost = False
        _parent.ConnectPrinter(ListView1.SelectedItems(0).Text, _parent.PrintQueues, False, False)
        _parent.ReloadLocalPrinters()
        Me.TopMost = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            ListView1.BeginUpdate()
            For index = 0 To ListView1.Items.Count - 1
                Try
                    If ListView1.Items(index).Text.ToLower.Contains(MetroTextBox1.Text) Then
                    Else
                        ListView1.Items.Remove(ListView1.Items(index))
                    End If
                Catch ex As Exception
                End Try
            Next
            ListView1.EndUpdate()
        Catch ex As Exception
        End Try
    End Sub
End Class