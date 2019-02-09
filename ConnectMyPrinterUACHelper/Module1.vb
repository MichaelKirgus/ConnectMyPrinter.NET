Imports System.Globalization
Imports System.Management
Imports ConnectMyPrinterAppSettingsHandler
Imports ConnectMyPrinterEnumerationLib
Imports ConnectMyPrinterLanguageHelper
Imports ConnectMyPrinterPrinterManageLib

Module Module1
    Public MLangHelper As New LanguageApplyHelper
    Public MCultureInf As CultureInfo = CultureInfo.CurrentUICulture

    Public DeserializeLib As New PrinterEnumerationSerializer
    Public PrinterServices As New ManagePrinter
    Public PrinterRemover As New PrinterDriverRemover

    Sub Main()
        Dim cnt As Integer = 0

        Dim ll As IO.FileInfo = Nothing

        Dim jj As Array = IO.Directory.GetFiles(My.Application.CommandLineArgs(0))

        For Each argument As String In jj
            Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterUACHelper.TranslatedStrings", GetType(Module1), MCultureInf, "ReadPrinterEntryStr", ""))

            Dim printerobj As PrinterQueueInfo
            ll = New IO.FileInfo(argument)
            printerobj = DeserializeLib.LoadQueueFile(argument)

            Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterUACHelper.TranslatedStrings", GetType(Module1), MCultureInf, "ExecActionStr", ""))

            If ll.Name.Contains("DeletePrinter") Then
                PrinterServices.DeletePrinter(printerobj)
            End If
            If ll.Name.Contains("CancelAllPrintJobs") Then
                PrinterServices.CancelAllPrintJobs(printerobj.ShareName)
            End If
            If ll.Name.Contains("DeleteDevice") Then
                PrinterServices.DeleteDevice(printerobj)
            End If
            If ll.Name.Contains("DeleteDevMode2Settings") Then
                PrinterServices.DeleteDevMode2Settings(printerobj)
            End If
            If ll.Name.Contains("DeleteDevModeSettings") Then
                PrinterServices.DeleteDevModeSettings(printerobj)
            End If
            If ll.Name.Contains("DeletePrinterAndDriverElevated") Then
                PrinterServices.DeletePrinterAndDriverElevated(printerobj)
            End If
            If ll.Name.Contains("DeletePrinterDriver") Then
                PrinterServices.DeletePrinterDriver(printerobj.Driver.Name)
            End If
            If ll.Name.Contains("DeletePrinterPort") Then
                PrinterServices.DeletePrinterPort(printerobj)
            End If
            If ll.Name.Contains("DeletePrinterSettings") Then
                PrinterServices.DeletePrinterSettings(printerobj)
            End If
            If ll.Name.Contains("PrintTestPage") Then
                PrinterServices.PrintTestPage(printerobj)
            End If
            If ll.Name.Contains("PurgePrinterSpooler") Then
                PrinterServices.PurgePrinterSpooler()
            End If
            If ll.Name.Contains("PurgePrinterQueue") Then
                PrinterServices.PurgePrinterQueue(printerobj)
            End If
            If ll.Name.Contains("ShowPrinterSettings") Then
                PrinterServices.ShowPrinterSettings(printerobj)
            End If
            If ll.Name.Contains("ResetPrinter") Then
                PrinterServices.ResetPrinter(printerobj.ShareName)
            End If
            If ll.Name.Contains("RestartPrinterService") Then
                PrinterServices.RestartPrinterService()
            End If
            If ll.Name.Contains("RetrievePrinterInformation") Then
                PrinterServices.RetrievePrinterInformation(printerobj)
            End If
            If ll.Name.Contains("SetDefaultPrinter") Then
                PrinterServices.SetDefaultPrinter(printerobj)
            End If
            If ll.Name.Contains("ShowPrinterQueue") Then
                PrinterServices.ShowPrinterQueue(printerobj)
            End If
            If ll.Name.Contains("TakeownPrinterDriver") Then
                PrinterRemover.TakeownPrinterDriver(printerobj)
            End If
            If ll.Name.Contains("DeletePrinterDriver") Then
                Dim aa As New ConnectMyPrinterDriverPackagesLib.ManageDriverPackages
                Dim pp As New ConnectMyPrinterDriverPackagesLib.DriverPackageItem
                pp.DriverName = printerobj.DriverName
                aa.DeleteDriver(pp)
            End If
            If ll.Name.Contains("DeletePrinterDriverPacket") Then
                Dim aa As New ConnectMyPrinterDriverPackagesLib.ManageDriverPackages
                Dim pp As New ConnectMyPrinterDriverPackagesLib.DriverPackageItem
                pp.DriverName = printerobj.DriverName
                aa.DeleteDriverPacket(pp)
            End If
            If ll.Name.Contains("DeletePrinterDriverFiles") Then
                PrinterRemover.DeletePrinterDriverFiles(printerobj)
            End If
            If ll.Name.Contains("DeletePrinterRegKeysHKLM") Then
                PrinterServices.DeletePrinterRegKeysHKLM(printerobj)
            End If
            If ll.Name.Contains("DeletePrinterRegKeysHKCU") Then
                PrinterServices.DeletePrinterRegKeysHKCU(printerobj)
            End If
            If ll.Name.Contains("StopPrinterService") Then
                PrinterServices.StopPrinterService()
            End If
            If ll.Name.Contains("StartPrinterService") Then
                PrinterServices.StartPrinterService()
            End If
            If ll.Name.Contains("DeleteUnusedDrivers") Then
                Dim rr As New AppSettingsClass
                PrinterRemover.DeleteUnusedDrivers(rr.PrinterAdminPath)
            End If
            If ll.Name.Contains("DeleteAllPrintersAndDrivers") Then
                Dim rr As New AppSettingsClass
                PrinterRemover.DeleteAllPrintersAndDrivers(rr.PrinterAdminPath)
                PrinterServices.StartPrinterService()
            End If
            If ll.Name.Contains("DeleteUnusedDrivers") Then
                Dim rr As New AppSettingsClass
                PrinterRemover.DeleteUnusedDrivers(rr.PrinterAdminPath)
                PrinterServices.StartPrinterService()
            End If

            My.Computer.FileSystem.DeleteFile(argument)
        Next

        Try
            IO.Directory.Delete(ll.DirectoryName)
        Catch ex As Exception
        End Try
    End Sub

End Module
