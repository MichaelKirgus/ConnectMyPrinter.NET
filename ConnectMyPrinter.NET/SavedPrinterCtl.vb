Imports System.Runtime.Remoting
Imports ConnectMyPrinterEnumerationLib
Imports System.ServiceProcess
Imports ConnectMyPrinterPrinterManageLib

Public Class SavedPrinterCtl
    Public _parent As Form1
    Public Property CollapsedHeight = 42
    Public Property ExpandedHeight = 105
    Public IsFirstStart As Boolean = True
    Public IsExpanded As Boolean = False
    Public CurrActionFileIndex As Integer = 0

    Private Sub PrinterCtl_MouseEnter(sender As Object, e As EventArgs) Handles MyBase.MouseEnter
        SetSelectedState()
    End Sub

    Private Sub PrinterCtl_MouseLeave(sender As Object, e As EventArgs) Handles MyBase.MouseLeave
        SetNonSelectedState()
    End Sub

    Sub SetSelectedState()
        Me.BackColor = Color.WhiteSmoke
        For index = 0 To Me.Controls.Count - 1
            Try
                Dim ll As Control
                ll = Me.Controls(index)
                If ll.Name.Contains("Lbl") Then
                    ll.BackColor = Color.WhiteSmoke
                End If
            Catch ex As Exception
            End Try
        Next
    End Sub

    Sub SetNonSelectedState()
        Me.BackColor = Color.White
        For index = 0 To Me.Controls.Count - 1
            Try
                Dim ll As Control
                ll = Me.Controls(index)
                If ll.Name.Contains("Lbl") Then
                    ll.BackColor = Color.White
                    If IsFirstStart Then
                        AddHandler ll.MouseEnter, AddressOf SetSelectedState
                        AddHandler ll.MouseLeave, AddressOf SetNonSelectedState
                    End If
                End If
            Catch ex As Exception
            End Try
        Next
    End Sub

    Private Sub PrinterCtl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetNonSelectedState()
        IsFirstStart = False
    End Sub

    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter
        SetSelectedState()
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        SetNonSelectedState()
    End Sub

    Private Sub Button1_MouseEnter(sender As Object, e As EventArgs) Handles Button1.MouseEnter
        SetSelectedState()
    End Sub

    Private Sub Button2_MouseEnter(sender As Object, e As EventArgs) Handles Button2.MouseEnter
        SetSelectedState()
    End Sub

    Private Sub PictureBox2_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox2.MouseEnter
        SetSelectedState()
        If IsExpanded Then
            PictureBox2.Image = My.Resources.decollapse
        Else
            PictureBox2.Image = My.Resources.expandable_2
        End If
    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles Button1.MouseLeave
        SetNonSelectedState()
    End Sub

    Private Sub Button2_MouseLeave(sender As Object, e As EventArgs) Handles Button2.MouseLeave
        SetNonSelectedState()
    End Sub

    Private Sub PictureBox2_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox2.MouseLeave
        SetNonSelectedState()
        If IsExpanded Then
            PictureBox2.Image = My.Resources.decollapse_2
        Else
            PictureBox2.Image = My.Resources.expandable
        End If
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        If IsExpanded Then
            Me.Height = CollapsedHeight
            PictureBox2.Image = My.Resources.expandable
            IsExpanded = False
        Else
            Me.Height = ExpandedHeight
            PictureBox2.Image = My.Resources.decollapse_2
            IsExpanded = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim isok As Boolean = False

            If _parent.AppSettings.AskUserIfRemovePrinterFromSavedPrinters Then
                Dim aa As PrinterQueueInfo
                aa = Me.Tag
                Dim oo As New DeletePrinterDlg
                oo.MetroLabel2.Text = "Möchten Sie wirklich den Drucker" & vbNewLine & aa.ShareName & " aus Ihren gespeicherten Druckern entfernen?" & vbNewLine & "Der Drucker wird nur in dieser Liste gelöscht."
                Dim uu As MsgBoxResult
                uu = oo.ShowDialog()
                If uu = MsgBoxResult.Yes Then
                    isok = True
                End If
            Else
                isok = True
            End If

            If isok = True Then
                _parent.DeleteSavedPrinter(Me.Tag)
                Dim ll As New SavedPrinterEnumerationSerializer
                ll.SaveMyPrinterCollectionFile(_parent.MyPrinters, _parent.AppSettings.SavedPrintersProfileFile)

                _parent.LoadMyPrinters()
                _parent.FillGUIWithSavedPrinters()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim jj As PrinterQueueInfo
            jj = Me.Tag

            _parent.ConnectPrinter(jj.ShareName, _parent.PrintQueues, _parent.MetroCheckBox1.Checked, False)
            _parent.ReloadLocalPrinters()
        Catch ex As Exception
        End Try
    End Sub
End Class
