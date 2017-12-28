﻿Imports ConnectMyPrinterForceDeleteManageLib
Imports ConnectMyPrinterUserListLib

Public Class DeleteClass
    Public DeletedKeyCount As Integer = 0
    Public Errors As String = ""

    Public Function DeletePrinterFromRegistry(ByVal AppSettingsObj As ConnectMyPrinterAppSettingsHandler.AppSettingsClass, ByVal PrinterName As String, ByVal UserInfo As UserListClass, ByVal CreateRestorePoint As Boolean, Optional ByVal RestorePointName As String = "Erzwungenes Löschen eines Druckers", Optional ByVal RestartSpooler As Boolean = True, Optional ByVal DeleteLocalMachinePart As Boolean = False) As Boolean
        Try
            Dim jj As New ConnectMyPrinterSystemRestorePointLib.CreateRestorePointClass
            jj.EnsureCreationPoint()
            jj.CreatePoint(RestorePointName, True)
            Dim aa As New ConnectMyPrinterPrinterManageLib.PrinterDriverRemover
            Dim bb As New ConnectMyPrinterPrinterManageLib.ManagePrinter
            bb.CancelAllPrintJobs(PrinterName)
            bb.ResetPrinter(PrinterName)
            Dim SpoolerHelper As New ConnectMyPrinterPrinterManageLib.ManagePrinter
            If RestartSpooler Then
                SpoolerHelper.StopPrinterService()
                KillSpoolServer()
            End If
            Dim qq As New ForcePrinterDelete
            qq.DeletePrinter(PrinterName, UserInfo._KEY, DeleteLocalMachinePart)
            DeletedKeyCount = qq.FinishedRuns
            Errors = qq.Errors
            If RestartSpooler Then
                SpoolerHelper.StartPrinterService()
            End If
            aa.DeleteUnusedDrivers(AppSettingsObj.PrinterAdminPath)

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function KillSpoolServer() As Boolean
        Try
            For Each item As Process In Process.GetProcessesByName("spoolsv")
                Try
                    item.Kill()
                Catch ex As Exception
                End Try
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
