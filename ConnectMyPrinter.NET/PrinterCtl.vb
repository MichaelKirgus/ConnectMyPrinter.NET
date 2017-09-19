﻿Imports System.Runtime.Remoting
Imports ConnectMyPrinterEnumerationLib
Imports System.ServiceProcess
Imports ConnectMyPrinterPrinterManageLib
Imports System.Text
Imports System.Security

Public Class PrinterCtl
    Public _parent As Form1
    Dim ElevationHelper As New ElevationHelperClass
    Public Property CollapsedHeight = 42
    Public Property ExpandedHeight = 320
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
                        AddHandler ll.MouseDoubleClick, AddressOf DefaultDoublecklickAction
                    End If
                End If
            Catch ex As Exception
            End Try
        Next
    End Sub

    Sub DefaultDoublecklickAction()
        If _parent.AppSettings.DoubleClickActionOnPrinterItem = ConnectMyPrinterAppSettingsHandler.AppSettingsClass.DoubleClickActionOnPrinterItemAction.ShowPrinterQueueDialog Then
            _parent.PrinterManageService.ShowPrinterQueue(Me.Tag)
        End If
        If _parent.AppSettings.DoubleClickActionOnPrinterItem = ConnectMyPrinterAppSettingsHandler.AppSettingsClass.DoubleClickActionOnPrinterItemAction.ShowPrinterDriverSettingsDialog Then
            _parent.PrinterManageService.ShowPrinterDriverSettings(Me.Tag)
        End If
        If _parent.AppSettings.DoubleClickActionOnPrinterItem = ConnectMyPrinterAppSettingsHandler.AppSettingsClass.DoubleClickActionOnPrinterItemAction.ShowPrinterPropertiesDialog Then
            _parent.PrinterManageService.ShowPrinterSettings(Me.Tag)
        End If
        If _parent.AppSettings.DoubleClickActionOnPrinterItem = ConnectMyPrinterAppSettingsHandler.AppSettingsClass.DoubleClickActionOnPrinterItemAction.MakePrinterToDefaultPrinter Then
            _parent.PrinterManageService.SetDefaultPrinter(Me.Tag)
            _parent.ReloadLocalPrinters()
        End If
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        _parent.PrinterManageService.ShowPrinterSettings(Me.Tag)
    End Sub

    Sub DeletePrinter()
        Try
            Dim isok As Boolean = False

            If _parent.AppSettings.AskUserIfDisconnectPrinter Then
                Dim aa As PrinterQueueInfo
                aa = Me.Tag
                Dim oo As New DeletePrinterDlg
                _parent._Log.Write(ConnectMyPrinterLog.Logging.LogType.Information, Me.ParentForm, "Drucker " & aa.ShareName & " entfernen", Err)
                oo.MetroLabel2.Text = "Möchten Sie wirklich den Drucker" & vbNewLine & aa.ShareName & " entfernen?"
                Dim uu As MsgBoxResult
                uu = oo.ShowDialog()
                If uu = MsgBoxResult.Yes Then
                    isok = True
                End If
            Else
                isok = True
            End If

            If isok = True Then
                Dim jj As New ProcessingDlg
                jj.Show(Me.ParentForm)
                _parent.PrinterManageService.DeletePrinter(Me.Tag)
                If _parent.AppSettings.CleanPrinterDriverPackagesAtPrinterRemove Then
                    If _parent.AppSettings.LocalActionsNeedElevation Then
                        ElevationHelper.GenerateActionFile("DeleteUnusedDrivers", Me.Tag, _parent, Me)
                        ElevationHelper.GenerateActionFile("RestartPrinterService", Me.Tag, _parent, Me)
                        ElevationHelper.GenerateActionFile("DeleteUnusedDrivers", Me.Tag, _parent, Me)
                        ElevationHelper.StartElevatedActions(_parent, Me)
                    Else
                        _parent.PrinterDriverRemoverService.DeleteUnusedDrivers(_parent.AppSettings.PrinterAdminPath)
                        _parent.PrinterManageService.RestartPrinterService()
                        _parent.PrinterDriverRemoverService.DeleteUnusedDrivers(_parent.AppSettings.PrinterAdminPath)
                    End If
                End If

                _parent.ReloadLocalPrinters()
                jj.Close()
            End If
        Catch ex As Exception
            _parent._Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me.ParentForm, "Fehler", Err)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DeletePrinter()
    End Sub

    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
        _parent.PrinterManageService.PrintTestPage(Me.Tag)
    End Sub

    Private Sub MetroButton2_Click(sender As Object, e As EventArgs) Handles MetroButton2.Click
        _parent.PrinterManageService.ShowPrinterQueue(Me.Tag)
    End Sub

    Private Sub MetroButton3_Click(sender As Object, e As EventArgs) Handles MetroButton3.Click
        Dim jj As New ProcessingDlg
        jj.Show(Me.ParentForm)
        Application.DoEvents()
        _parent.PrinterManageService.PurgePrinterQueue(Me.Tag)
        jj.Close()
    End Sub

    Private Sub StandardeinstellungenLöschenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StandardeinstellungenLöschenToolStripMenuItem.Click
        _parent._Log.Write(ConnectMyPrinterLog.Logging.LogType.Information, Me.ParentForm, "Lösche Standardeinstellungen", Err)
        Dim jj As New ProcessingDlg
        jj.Show(Me.ParentForm)
        Application.DoEvents()
        _parent.PrinterManageService.DeleteDevModeSettings(Me.Tag)
        Application.DoEvents()
        _parent.PrinterManageService.DeleteDevMode2Settings(Me.Tag)
        jj.Close()
    End Sub

    Private Sub MetroButton4_Click(sender As Object, e As EventArgs) Handles MetroButton4.Click
        Dim jj As New ProcessingDlg
        jj.Show(Me.ParentForm)

        DeletePrinterInt()

        jj.Close()

        _parent.ReloadLocalPrinters()
    End Sub

    Public Function DeletePrinterInt() As List(Of PrinterQueueInfo)

        Try
            'Prüfen, ob noch andere Drucker den selben Treiber verwenden:
            Dim dd As New PrinterDriverDependenciesProvider
            Dim ll As List(Of PrinterQueueInfo)
            ll = dd.GetPrintersWithSameDriverName(_parent.LocalPrinters, Me.Tag)

            Dim aa As PrinterQueueInfo
            aa = Me.Tag

            If ll.Count > 1 Then
                Dim pp As String = "Folgende Drucker verwenden noch den Treiber " & aa.DriverName & " :" & vbNewLine

                For index = 0 To ll.Count - 1
                    pp += ll(index).ShareName & " Server: " & ll(index).Server & vbNewLine
                Next

                Dim qq As MsgBoxResult
                qq = MsgBox(pp & vbNewLine & vbNewLine & "Der Treiber kann nicht entfernt werden, ohne alle von dem Treiber abhängigen Drucker zu löschen. Möchten Sie alle abhängigen Drucker löschen?", MsgBoxStyle.OkCancel)

                If qq = MsgBoxResult.Ok Then
                    For index = 0 To ll.Count - 1
                        DeletePrinterIntJob(ll(index), False)
                    Next
                Else
                    Return New List(Of PrinterQueueInfo)
                End If
            Else
                DeletePrinterIntJob(Me.Tag, True)
            End If

            'Aufräumen
            DoLastActionsDeletePrinters(ll)

            Return ll
        Catch ex As Exception
            _parent._Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me.ParentForm, "Fehler", Err)
            Return New List(Of PrinterQueueInfo)
        End Try
    End Function

    Public Sub DeletePrinterIntJob(ByVal PrinterObj As PrinterQueueInfo, ByVal RestartPrinterSpooler As Boolean)
        Application.DoEvents()
        _parent.PrinterManageService.PurgePrinterQueue(Me.Tag)
        Application.DoEvents()
        _parent.PrinterManageService.DeletePrinter(Me.Tag)
        Application.DoEvents()
        If _parent.AppSettings.LocalActionsNeedElevation Then
            If RestartPrinterSpooler Then
                ElevationHelper.GenerateActionFile("RestartPrinterService", Me.Tag, _parent, Me)
            End If
            ElevationHelper.GenerateActionFile("DeletePrinterAndDriverElevated", Me.Tag, _parent, Me)
            If _parent.AppSettings.DeletePrinterDriverLowLevel Then
                ElevationHelper.GenerateActionFile("TakeownPrinterDriver", Me.Tag, _parent, Me)
                ElevationHelper.GenerateActionFile("DeletePrinterDriverFiles", Me.Tag, _parent, Me)
            End If
            If RestartPrinterSpooler Then
                ElevationHelper.GenerateActionFile("RestartPrinterService", Me.Tag, _parent, Me)
            End If
            ElevationHelper.StartElevatedActions(_parent, Me)
        Else
            If RestartPrinterSpooler Then
                _parent.PrinterManageService.RestartPrinterService()
            End If
            Application.DoEvents()
            _parent.PrinterManageService.DeletePrinterAndDriverElevated(Me.Tag)
            Application.DoEvents()
            If RestartPrinterSpooler Then
                _parent.PrinterManageService.RestartPrinterService()
            End If
        End If
    End Sub

    Public Function DoLastActionsDeletePrinters(ByVal PrintObj As List(Of PrinterQueueInfo)) As Boolean
        Try
            Dim qq As New SpoolerHandler

            If _parent.AppSettings.PrinterSpoolerRestartNeedElevation Then
                ElevationHelper.GenerateActionFile("StopPrinterService", Me.Tag, _parent, Me)
            Else
                _parent.PrinterManageService.StopPrinterService()
            End If

            If _parent.AppSettings.PrinterSpoolerRestartNeedElevation And Not _parent.AppSettings.LocalActionsNeedElevation Then
                ElevationHelper.StartElevatedActions(_parent, Me)
            End If

            For Each item As PrinterQueueInfo In PrintObj
                _parent.PrinterManageService.DeletePrinterRegKeysHKCU(item)
                _parent.PrinterManageService.DeleteDevice(item)

                If _parent.AppSettings.LocalActionsNeedElevation Then
                    ElevationHelper.GenerateActionFile("DeletePrinterRegKeysHKLM", Me.Tag, _parent, Me)
                Else
                    _parent.PrinterManageService.DeletePrinterRegKeysHKLM(item)
                End If
            Next

            If _parent.AppSettings.LocalActionsNeedElevation Then
                ElevationHelper.StartElevatedActions(_parent, Me)
            End If

            If _parent.AppSettings.PrinterSpoolerRestartNeedElevation Then
                ElevationHelper.GenerateActionFile("StartPrinterService", Me.Tag, _parent, Me)
            Else
                _parent.PrinterManageService.StartPrinterService()
            End If

            If _parent.AppSettings.PrinterSpoolerRestartNeedElevation And Not _parent.AppSettings.LocalActionsNeedElevation Then
                ElevationHelper.StartElevatedActions(_parent, Me)
            End If

            If _parent.AppSettings.CleanPrinterDriverPackagesAtPrinterDriverRemove Then
                If _parent.AppSettings.LocalActionsNeedElevation Then
                    ElevationHelper.GenerateActionFile("DeleteUnusedDrivers", Me.Tag, _parent, Me)
                Else
                    _parent.PrinterDriverRemoverService.DeleteUnusedDrivers(_parent.AppSettings.PrinterAdminPath)
                End If
            End If

            If _parent.AppSettings.LocalActionsNeedElevation Then
                ElevationHelper.StartElevatedActions(_parent, Me)
            End If

            Return True
        Catch ex As Exception
            _parent._Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me.ParentForm, "Fehler", Err)
            Return False
        End Try
    End Function

    Sub ReinstallPrinter()
        Try
            Dim jj As New ProcessingDlg
            jj.Show(Me.ParentForm)
            Application.DoEvents()

            Dim aa As PrinterQueueInfo
            aa = Me.Tag

            Dim deprinters As List(Of PrinterQueueInfo)
            deprinters = DeletePrinterInt()

            _parent.ConnectPrinter(aa.ShareName, _parent.PrintQueues, aa.DefaultPrinter, True)

            'Evtl. noch andere gelöschte Drucker wieder verbinden
            For index = 0 To deprinters.Count - 1
                _parent.ConnectPrinter(deprinters(index).ShareName, _parent.PrintQueues, deprinters(index).DefaultPrinter, True)
            Next

            jj.Close()

            _parent.ReloadLocalPrinters()
        Catch ex As Exception
            _parent._Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me.ParentForm, "Fehler", Err)
        End Try
    End Sub

    Private Sub MetroButton5_Click(sender As Object, e As EventArgs) Handles MetroButton5.Click
        ReinstallPrinter()
    End Sub

    Private Sub MetroButton6_Click(sender As Object, e As EventArgs) Handles MetroButton6.Click
        Dim jj As New ProcessingDlg
        jj.Show()
        Application.DoEvents()
        _parent.PrinterManageService.RetrievePrinterInformation(Me.Tag)
        jj.Close()
    End Sub

    Private Sub AlsStandarddruckerFestlegenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AlsStandarddruckerFestlegenToolStripMenuItem.Click
        Dim jj As New ProcessingDlg
        jj.Show()
        Application.DoEvents()
        _parent.PrinterManageService.SetDefaultPrinter(Me.Tag)
        jj.Close()

        _parent.ReloadLocalPrinters()
    End Sub

    Private Sub DruckerImProfilSpeichernToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DruckerImProfilSpeichernToolStripMenuItem.Click
        Try
            Dim ll As New SavedPrinterEnumerationSerializer

            _parent.AddSavedPrinter(Me.Tag, True)
            ll.SaveMyPrinterCollectionFile(_parent.MyPrinters, _parent.AppSettings.SavedPrintersProfileFile)
            _parent.LoadMyPrinters()
            _parent.FillGUIWithSavedPrinters()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If Not _parent.AppSettings.UsePrinterSavingFeature Then
            DruckerImProfilSpeichernToolStripMenuItem.Visible = False
            ToolStripSeparator2.Visible = False
        End If
        If My.Computer.Keyboard.ShiftKeyDown Then
            DruckereinstellungenExportierenToolStripMenuItem.Visible = True
            DruckereinstellungenImportierenToolStripMenuItem.Visible = True
            DruckereigenschaftenToolStripMenuItem.Visible = True
            DruckereinstellungenToolStripMenuItem.Visible = True
            DruckerNeuInstallierenToolStripMenuItem.Visible = True
            ToolStripSeparator3.Visible = True
            ToolStripSeparator4.Visible = True
        Else
            DruckereinstellungenExportierenToolStripMenuItem.Visible = False
            DruckereinstellungenImportierenToolStripMenuItem.Visible = False
            DruckereigenschaftenToolStripMenuItem.Visible = False
            DruckereinstellungenToolStripMenuItem.Visible = False
            DruckerNeuInstallierenToolStripMenuItem.Visible = False
            ToolStripSeparator3.Visible = False
            ToolStripSeparator4.Visible = False
        End If
        SetSelectedState()
    End Sub

    Private Sub ContextMenuStrip1_Closing(sender As Object, e As ToolStripDropDownClosingEventArgs) Handles ContextMenuStrip1.Closing
        SetNonSelectedState()
    End Sub

    Private Sub DruckereinstellungenExportierenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DruckereinstellungenExportierenToolStripMenuItem.Click
        'Druckereinstellungen exportieren

        Try
            Dim gg As New ExportImportPrinterSettings
            SaveFileDialog1.ShowDialog()
            If Not SaveFileDialog1.FileName = "" Then
                gg.ExportPrinterSettings(Me.Tag, SaveFileDialog1.FileName)
                MsgBox("Druckereinstellungen erfolgreich exportiert.")
            End If
        Catch ex As Exception
            _parent._Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me.ParentForm, "Fehler", Err)
        End Try
    End Sub

    Private Sub DruckereinstellungenImportierenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DruckereinstellungenImportierenToolStripMenuItem.Click
        'Druckereinstellungen importieren

        Try
            Dim gg As New ExportImportPrinterSettings
            SaveFileDialog1.ShowDialog()
            If Not SaveFileDialog1.FileName = "" Then
                gg.ExportPrinterSettings(Me.Tag, SaveFileDialog1.FileName)
                MsgBox("Druckereinstellungen erfolgreich importiert.")
            End If
        Catch ex As Exception
            _parent._Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me.ParentForm, "Fehler", Err)
        End Try
    End Sub

    Private Sub DruckerEntfernenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DruckerEntfernenToolStripMenuItem.Click
        DeletePrinter()
    End Sub

    Private Sub DruckereigenschaftenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DruckereigenschaftenToolStripMenuItem.Click
        _parent.PrinterManageService.ShowPrinterSettings(Me.Tag)
    End Sub

    Private Sub DruckereinstellungenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DruckereinstellungenToolStripMenuItem.Click
        _parent.PrinterManageService.ShowPrinterDriverSettings(Me.Tag)
    End Sub

    Private Sub DruckerNeuInstallierenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DruckerNeuInstallierenToolStripMenuItem.Click
        ReinstallPrinter()
    End Sub
End Class
