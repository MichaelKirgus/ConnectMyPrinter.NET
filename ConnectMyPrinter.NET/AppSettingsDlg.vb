Imports ConnectMyPrinterPrinterManageLib

Public Class AppSettingsDlg
    Public _parent As Form1
    Dim ElevationHelper As New ElevationHelperClass
    Dim ActionLib As New ConnectMyPrinterPrinterManageLib.ManagePrinter
    Dim dummyctl As New PrinterCtl

    Private Sub AppSettingsDlg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Style = _parent.Style
        Catch ex As Exception
        End Try

        Try
            MetroCheckBox1.Checked = _parent.AppSettings.AskUserIfConnectPrinter
            MetroCheckBox2.Checked = _parent.AppSettings.AskUserIfDisconnectPrinter
            MetroCheckBox3.Checked = _parent.AppSettings.AskUserIfMultipleResults

            MetroCheckBox4.Checked = _parent.AppSettings.CleanPrinterDriverPackagesAtPrinterRemove
            MetroCheckBox5.Checked = _parent.AppSettings.CleanPrinterDriverPackagesAtPrinterDriverRemove

            If _parent.AppSettings.AllowUserToChangeSettings = False Then
                MetroCheckBox1.Enabled = False
                MetroCheckBox2.Enabled = False
                MetroCheckBox3.Enabled = False

                MetroCheckBox4.Enabled = False
                MetroCheckBox5.Enabled = False
            End If
        Catch ex As Exception
        End Try

        MetroTabControl1.SelectedTab = MetroTabControl1.TabPages(0)

        CheckSpoolerServiceStatus()
    End Sub

    Sub CheckSpoolerServiceStatus()
        Try
            Dim hh As New SpoolerHandler
            If hh.IsSpoolerRunning Then
                status_ok.Visible = True
                status_error.Visible = False
            Else
                status_ok.Visible = False
                status_error.Visible = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MetroButton7_Click(sender As Object, e As EventArgs) Handles MetroButton7.Click
        Try
            _parent.AppSettings.AskUserIfConnectPrinter = MetroCheckBox1.Checked
            _parent.AppSettings.AskUserIfDisconnectPrinter = MetroCheckBox2.Checked
            _parent.AppSettings.AskUserIfMultipleResults = MetroCheckBox3.Checked

            _parent.AppSettings.CleanPrinterDriverPackagesAtPrinterRemove = MetroCheckBox4.Checked
            _parent.AppSettings.CleanPrinterDriverPackagesAtPrinterDriverRemove = MetroCheckBox5.Checked

            If _parent.AppSettings.AllowUserToSaveSettings = True Then
                _parent.SaveSettings(_parent.AppSettings, _parent.AppSettingFile)
            End If

            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
        Dim hh As New ProcessingDlg
        hh._parent = _parent
        hh.Show(Me.ParentForm)
        Application.DoEvents()
        ElevationHelper.GenerateActionFile("DeleteAllPrintersAndDrivers", New ConnectMyPrinterEnumerationLib.PrinterQueueInfo, _parent, dummyctl)
        ElevationHelper.StartElevatedActions(_parent, Nothing)
        hh.Close()
        Application.DoEvents()
        CheckSpoolerServiceStatus()
    End Sub

    Private Sub MetroButton2_Click(sender As Object, e As EventArgs) Handles MetroButton2.Click
        Dim hh As New ProcessingDlg
        hh._parent = _parent
        hh.Show(Me.ParentForm)
        Application.DoEvents()
        ElevationHelper.GenerateActionFile("DeleteUnusedDrivers", New ConnectMyPrinterEnumerationLib.PrinterQueueInfo, _parent, dummyctl)
        ElevationHelper.GenerateActionFile("StartPrinterService", New ConnectMyPrinterEnumerationLib.PrinterQueueInfo, _parent, dummyctl)
        ElevationHelper.StartElevatedActions(_parent, Nothing)
        hh.Close()
        Application.DoEvents()
        CheckSpoolerServiceStatus()
    End Sub

    Private Sub MetroButton3_Click(sender As Object, e As EventArgs) Handles MetroButton3.Click
        Dim hh As New ProcessingDlg
        hh._parent = _parent
        hh.Show(Me.ParentForm)
        Application.DoEvents()
        ElevationHelper.GenerateActionFile("PurgePrinterSpooler", New ConnectMyPrinterEnumerationLib.PrinterQueueInfo, _parent, dummyctl)
        ElevationHelper.StartElevatedActions(_parent, Nothing)
        hh.Close()
        Application.DoEvents()
        CheckSpoolerServiceStatus()
    End Sub

    Private Sub MetroButton6_Click(sender As Object, e As EventArgs) Handles MetroButton6.Click
        Dim hh As New ProcessingDlg
        hh._parent = _parent
        hh.Show(Me.ParentForm)
        Application.DoEvents()
        If _parent.AppSettings.PrinterSpoolerRestartNeedElevation Or (_parent.UserCanControlSpooler = False) Then
            ElevationHelper.GenerateActionFile("StartPrinterService", New ConnectMyPrinterEnumerationLib.PrinterQueueInfo, _parent, dummyctl)
            ElevationHelper.StartElevatedActions(_parent, Nothing)
        Else
            ActionLib.StartPrinterService()
        End If
        hh.Close()
        Application.DoEvents()
        CheckSpoolerServiceStatus()
    End Sub

    Private Sub MetroButton4_Click(sender As Object, e As EventArgs) Handles MetroButton4.Click
        Dim hh As New ProcessingDlg
        hh._parent = _parent
        hh.Show(Me.ParentForm)
        Application.DoEvents()
        If _parent.AppSettings.PrinterSpoolerRestartNeedElevation Or (_parent.UserCanControlSpooler = False) Then
            ElevationHelper.GenerateActionFile("RestartPrinterService", New ConnectMyPrinterEnumerationLib.PrinterQueueInfo, _parent, dummyctl)
            ElevationHelper.StartElevatedActions(_parent, Nothing)
        Else
            ActionLib.RestartPrinterService()
        End If
        hh.Close()
        Application.DoEvents()
        CheckSpoolerServiceStatus()
    End Sub

    Private Sub MetroButton5_Click(sender As Object, e As EventArgs) Handles MetroButton5.Click
        Dim hh As New ProcessingDlg
        hh._parent = _parent
        hh.Show(Me.ParentForm)
        Application.DoEvents()
        If _parent.AppSettings.PrinterSpoolerRestartNeedElevation Or (_parent.UserCanControlSpooler = False) Then
            ElevationHelper.GenerateActionFile("StopPrinterService", New ConnectMyPrinterEnumerationLib.PrinterQueueInfo, _parent, dummyctl)
            ElevationHelper.StartElevatedActions(_parent, Nothing)
        Else
            ActionLib.StopPrinterService()
        End If
        hh.Close()
        Application.DoEvents()
        CheckSpoolerServiceStatus()
    End Sub
End Class