Imports System.Management
Imports System.Printing

Public Class EnumeratePrinters

    Public Function GetLocalPrinterDriversFromWMI(ByVal EnumerateLocalPrinters As Boolean) As List(Of PrinterDriverInfo)
        'Diese Funktion lädt die lokalen Druckertreiber und gibt diese zurück.

        Try
            Try
                Dim searcher As New ManagementObjectSearcher(
                    "root\CIMV2",
                    "SELECT * FROM Win32_PrinterDriver")

                Dim jj As New List(Of PrinterDriverInfo)

                For Each queryObj As ManagementObject In searcher.Get()
                    Dim gg As New PrinterDriverInfo
                    Dim splitx As String = ""
                    splitx = queryObj("Name")
                    gg.SysRawName = queryObj("Name")
                    gg.Name = splitx.Split(",")(0)
                    Try
                        gg.Version = FileVersionInfo.GetVersionInfo(queryObj("DriverPath")).ProductVersion
                    Catch ex As Exception
                    End Try
                    gg.ConfigFile = queryObj("ConfigFile")
                    gg.DataFile = queryObj("DataFile")
                    gg.DriverPath = queryObj("DriverPath")
                    gg.FilePath = queryObj("FilePath")
                    gg.InfName = queryObj("InfName")
                    gg.InstallDate = queryObj("InstallDate")

                    jj.Add(gg)
                Next

                Return jj
            Catch err As ManagementException
                Return New List(Of PrinterDriverInfo)
            End Try
        Catch ex As Exception
            Return New List(Of PrinterDriverInfo)
        End Try
    End Function

    Public Function InternalLocalPrinterCollector(ByVal EnumerateLocalPrinters As Boolean) As List(Of PrintQueueCollection)
        Try
            'Initialisierung des PrintServer-Objekts
            Dim enumerationFlags1() As EnumeratedPrintQueueTypes = {EnumeratedPrintQueueTypes.Connections}
            Dim enumerationFlags2() As EnumeratedPrintQueueTypes = {EnumeratedPrintQueueTypes.DirectPrinting}
            Dim enumerationFlags4() As EnumeratedPrintQueueTypes = {EnumeratedPrintQueueTypes.PublishedInDirectoryServices}
            Dim enumerationFlags5() As EnumeratedPrintQueueTypes = {EnumeratedPrintQueueTypes.PushedMachineConnection}
            Dim enumerationFlags6() As EnumeratedPrintQueueTypes = {EnumeratedPrintQueueTypes.PushedUserConnection}
            Dim enumerationFlags7() As EnumeratedPrintQueueTypes = {EnumeratedPrintQueueTypes.TerminalServer}
            Dim enumerationFlags8() As EnumeratedPrintQueueTypes = {EnumeratedPrintQueueTypes.Shared}
            Dim enumerationFlags9() As EnumeratedPrintQueueTypes = {EnumeratedPrintQueueTypes.WorkOffline}

            Dim printServer1 As New LocalPrintServer()

            'Drucker filtern
            Dim printQueuesOnLocalServer1 As PrintQueueCollection = printServer1.GetPrintQueues(enumerationFlags1)
            Dim printQueuesOnLocalServer2 As PrintQueueCollection = printServer1.GetPrintQueues(enumerationFlags2)
            Dim printQueuesOnLocalServer4 As PrintQueueCollection = printServer1.GetPrintQueues(enumerationFlags4)
            Dim printQueuesOnLocalServer5 As PrintQueueCollection = printServer1.GetPrintQueues(enumerationFlags5)
            Dim printQueuesOnLocalServer6 As PrintQueueCollection = printServer1.GetPrintQueues(enumerationFlags6)
            Dim printQueuesOnLocalServer7 As PrintQueueCollection = printServer1.GetPrintQueues(enumerationFlags7)
            Dim printQueuesOnLocalServer8 As PrintQueueCollection = printServer1.GetPrintQueues(enumerationFlags8)
            Dim printQueuesOnLocalServer9 As PrintQueueCollection = printServer1.GetPrintQueues(enumerationFlags9)

            Dim hh As New List(Of PrintQueueCollection)
            hh.Add(printQueuesOnLocalServer1)
            hh.Add(printQueuesOnLocalServer2)
            If EnumerateLocalPrinters = True Then
                Dim enumerationFlags3() As EnumeratedPrintQueueTypes = {EnumeratedPrintQueueTypes.Local}
                Dim printQueuesOnLocalServer3 As PrintQueueCollection = printServer1.GetPrintQueues(enumerationFlags3)
                hh.Add(printQueuesOnLocalServer3)
            End If
            hh.Add(printQueuesOnLocalServer4)
            hh.Add(printQueuesOnLocalServer5)
            hh.Add(printQueuesOnLocalServer6)
            hh.Add(printQueuesOnLocalServer7)
            hh.Add(printQueuesOnLocalServer8)
            hh.Add(printQueuesOnLocalServer9)

            Return hh
        Catch ex As Exception
            Return New List(Of PrintQueueCollection)
        End Try
    End Function
End Class
