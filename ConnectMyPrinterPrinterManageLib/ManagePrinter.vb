'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports System.Management
Imports System.Printing
Imports System.ServiceProcess
Imports ConnectMyPrinterEnumerationLib
Imports Microsoft.Win32

Public Class ManagePrinter


    Public Function PurgePrinterSpooler() As Boolean
        Try
            Shell("net stop Spooler", AppWinStyle.Hide, True)
            Shell("del /q %SystemRoot%\system32\spool\printers\*.*", AppWinStyle.Hide, True)
            Shell("net start Spooler", AppWinStyle.Hide, True)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function RestartPrinterService() As Boolean
        Try
            Shell("net stop Spooler", AppWinStyle.Hide, True)
            Shell("net start Spooler", AppWinStyle.Hide, True)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function StopPrinterService() As Boolean
        Try
            Shell("net stop Spooler", AppWinStyle.Hide, True)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function StartPrinterService() As Boolean
        Try
            Shell("net start Spooler", AppWinStyle.Hide, True)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetPrinterServiceState() As Integer
        Try
            Dim hh As New ServiceController("Spooler")
            If hh.Status = ServiceControllerStatus.Running Then
                Return 1
            End If
            If hh.Status = ServiceControllerStatus.Stopped Then
                Return 0
            End If
            If hh.Status = ServiceControllerStatus.Paused Then
                Return -1
            End If

            Return -1
        Catch ex As Exception
            Return -1
        End Try
    End Function

    Public Function CancelAllPrintJobs(ByVal PrinterName As String) As Boolean
        Try
            Dim classInstance As New ManagementObject(
            "root\CIMV2",
            "Win32_Printer.DeviceID='" & PrinterName & "'",
            Nothing)

            Dim outParams As ManagementBaseObject =
            classInstance.InvokeMethod("CancelAllJobs", Nothing, Nothing)


            Return True
        Catch err As ManagementException
            Return False
        End Try
    End Function

    Public Function ResetPrinter(ByVal PrinterName As String) As Boolean
        Try
            Dim classInstance As New ManagementObject(
                "root\CIMV2",
                "Win32_Printer.DeviceID='" & PrinterName & "'",
                Nothing)

            Dim outParams As ManagementBaseObject =
                classInstance.InvokeMethod("Reset", Nothing, Nothing)

            Return True
        Catch err As ManagementException
            Return False
        End Try
    End Function

    Public Function SetDefaultPrinter(ByVal PrinterEntry As PrinterQueueInfo) As Boolean
        Try
            If (PrinterEntry.Server = "Lokal") Or (PrinterEntry.Server.ToLower.Contains(My.Computer.Name.ToLower) Or PrinterEntry.Server = "Local") Then
                Dim classInstance As New ManagementObject(
        "root\CIMV2",
        "Win32_Printer.DeviceID='" & PrinterEntry.Name & "'",
        Nothing)

                Dim outParams As ManagementBaseObject =
                        classInstance.InvokeMethod("SetDefaultPrinter", Nothing, Nothing)
            Else
                Dim classInstance As New ManagementObject(
                        "root\CIMV2",
                        "Win32_Printer.DeviceID='" & PrinterEntry.Server & "\" & PrinterEntry.ShareName & "'",
                        Nothing)

                Dim outParams As ManagementBaseObject =
                        classInstance.InvokeMethod("SetDefaultPrinter", Nothing, Nothing)
            End If


            Return True
        Catch err As ManagementException
            Return False
        End Try
    End Function

    Public Function DeletePrinterDriver(ByVal DriverName As String) As Boolean
        Try
            Shell("wmic sysdriver where(name=" & My.Resources.trenn & DriverName & My.Resources.trenn & ") delete", AppWinStyle.Hide, True)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ShowPrinterSettings(ByVal PrinterEntry As PrinterQueueInfo) As Boolean
        Try
            If PrinterEntry.Server = "Lokal" Or PrinterEntry.Server = "Local" Then
                Shell("rundll32 printui.dll PrintUIEntry /p /n " & My.Resources.trenn & PrinterEntry.ShareName & My.Resources.trenn)
            Else
                Shell("rundll32 printui.dll PrintUIEntry /p /n" & PrinterEntry.Server & "\" & PrinterEntry.ShareName)
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function ShowPrinterDriverSettings(ByVal PrinterEntry As PrinterQueueInfo) As Boolean
        Try
            If PrinterEntry.Server = "Lokal" Or PrinterEntry.Server = "Local" Then
                Shell("rundll32 printui.dll PrintUIEntry /e /n " & My.Resources.trenn & PrinterEntry.ShareName & My.Resources.trenn)
            Else
                Shell("rundll32 printui.dll PrintUIEntry /e /n" & PrinterEntry.Server & "\" & PrinterEntry.ShareName)
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function PrintTestPage(ByVal PrinterEntry As PrinterQueueInfo) As Boolean
        Try
            If PrinterEntry.Server = "Lokal" Or PrinterEntry.Server = "Local" Then
                Shell("rundll32 printui.dll PrintUIEntry /k /n " & My.Resources.trenn & PrinterEntry.ShareName & My.Resources.trenn)
            Else
                Shell("rundll32 printui.dll PrintUIEntry /k /n" & PrinterEntry.Server & "\" & PrinterEntry.ShareName)
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ShowPrinterQueue(ByVal PrinterEntry As PrinterQueueInfo) As Boolean
        Try
            If PrinterEntry.Server = "Lokal" Or PrinterEntry.Server = "Local" Then
                Shell("rundll32 printui.dll PrintUIEntry /o /n " & My.Resources.trenn & PrinterEntry.ShareName & My.Resources.trenn)
            Else
                Shell("rundll32 printui.dll PrintUIEntry /o /n" & PrinterEntry.Server & "\" & PrinterEntry.ShareName)
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DeletePrinter(ByVal PrinterEntry As PrinterQueueInfo) As Boolean
        Try
            If PrinterEntry.Server = "Lokal" Or PrinterEntry.Server = "Local" Then
                Shell("rundll32 printui.dll PrintUIEntry /dl /n " & My.Resources.trenn & PrinterEntry.ShareName & My.Resources.trenn & " /q")
            Else
                Shell("rundll32 printui.dll PrintUIEntry /dn /n " & PrinterEntry.Server & "\" & PrinterEntry.ShareName & " /q")
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DeleteDevModeSettings(ByVal PrinterEntry As PrinterQueueInfo) As Boolean
        Try
            My.Computer.Registry.CurrentUser.OpenSubKey _
("Printers\DevModePerUser", True).DeleteValue(PrinterEntry.ShareName)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DeleteDevMode2Settings(ByVal PrinterEntry As PrinterQueueInfo) As Boolean
        Try
            My.Computer.Registry.CurrentUser.OpenSubKey _
("Printers\DevModes2", True).DeleteValue(PrinterEntry.ShareName)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DeletePrinterSettings(ByVal PrinterEntry As PrinterQueueInfo) As Boolean
        Try
            My.Computer.Registry.CurrentUser.OpenSubKey _
("Printers\Settings", True).DeleteValue(PrinterEntry.ShareName)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DeletePrinterPort(ByVal PrinterEntry As PrinterQueueInfo) As Boolean
        Try
            My.Computer.Registry.CurrentUser.OpenSubKey _
("SOFTWARE\Microsoft\Windows NT\CurrentVersion\PrinterPorts", True).DeleteValue(PrinterEntry.ShareName)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DeleteDevice(ByVal PrinterEntry As PrinterQueueInfo) As Boolean
        Try
            My.Computer.Registry.CurrentUser.OpenSubKey _
("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Devices", True).DeleteValue(PrinterEntry.ShareName)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DeletePrinterAndDriverElevated(ByVal PrinterInfo As PrinterQueueInfo) As Boolean
        Try
            'Alle Treiber entfernen

            For index = 0 To 3
                Shell("RUNDLL32.EXE PRINTUI.DLL,PrintUIEntry /dd /m " & My.Resources.trenn & PrinterInfo.DriverName & My.Resources.trenn & " /h " & My.Resources.trenn & "x64" & My.Resources.trenn & " /v " & My.Resources.trenn & "Type 2 - Kernel Mode" & My.Resources.trenn & " /q")
                Shell("RUNDLL32.EXE PRINTUI.DLL,PrintUIEntry /dd /m " & My.Resources.trenn & PrinterInfo.DriverName & My.Resources.trenn & " /h " & My.Resources.trenn & "x86" & My.Resources.trenn & " /v " & My.Resources.trenn & "Type 2 - Kernel Mode" & My.Resources.trenn & " /q")
                Shell("RUNDLL32.EXE PRINTUI.DLL,PrintUIEntry /dd /m " & My.Resources.trenn & PrinterInfo.DriverName & My.Resources.trenn & " /h " & My.Resources.trenn & "x64" & My.Resources.trenn & " /v " & My.Resources.trenn & "Type 3 - User Mode" & My.Resources.trenn & " /q")
                Shell("RUNDLL32.EXE PRINTUI.DLL,PrintUIEntry /dd /m " & My.Resources.trenn & PrinterInfo.DriverName & My.Resources.trenn & " /h " & My.Resources.trenn & "x86" & My.Resources.trenn & " /v " & My.Resources.trenn & "Type 3 - User Mode" & My.Resources.trenn & " /q")
            Next

            'Evtl. Reste in Regsitry löschen
            Try
                My.Computer.Registry.CurrentUser.OpenSubKey _
    ("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers", True).DeleteValue(PrinterInfo.ShareName)
            Catch ex As Exception
            End Try
            Try
                My.Computer.Registry.CurrentUser.OpenSubKey _
    ("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers", True).DeleteSubKey(PrinterInfo.ShareName)
            Catch ex As Exception
            End Try
            Try
                My.Computer.Registry.LocalMachine.OpenSubKey _
    ("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers", True).DeleteValue(PrinterInfo.ShareName)
            Catch ex As Exception
            End Try
            Try
                My.Computer.Registry.LocalMachine.OpenSubKey _
    ("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers", True).DeleteSubKey(PrinterInfo.ShareName)
            Catch ex As Exception
            End Try

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DeletePrinterRegKeysHKCU(ByVal PrinterEntry As PrinterQueueInfo) As Boolean

        'Evtl. Reste in Registry löschen
        Try
            My.Computer.Registry.CurrentUser.OpenSubKey _
("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers", True).DeleteValue(PrinterEntry.ShareName)
        Catch ex As Exception
        End Try
        Try
            My.Computer.Registry.CurrentUser.OpenSubKey _
("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers", True).DeleteSubKey(PrinterEntry.ShareName)
        Catch ex As Exception
        End Try
        Try
            My.Computer.Registry.CurrentUser.OpenSubKey _
("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers", True).DeleteValue(PrinterEntry.DriverName)
        Catch ex As Exception
        End Try
        Try
            My.Computer.Registry.CurrentUser.OpenSubKey _
("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers", True).DeleteSubKey(PrinterEntry.DriverName)
        Catch ex As Exception
        End Try

        Return True
    End Function

    Public Function DeletePrinterRegKeysHKLM(ByVal PrinterEntry As PrinterQueueInfo) As Boolean
        Try
            My.Computer.Registry.LocalMachine.OpenSubKey _
("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers", True).DeleteValue(PrinterEntry.ShareName)
        Catch ex As Exception
        End Try
        Try
            My.Computer.Registry.LocalMachine.OpenSubKey _
("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers", True).DeleteSubKey(PrinterEntry.ShareName)
        Catch ex As Exception
        End Try
        Try
            My.Computer.Registry.LocalMachine.OpenSubKey _
("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers", True).DeleteValue(PrinterEntry.DriverName)
        Catch ex As Exception
        End Try
        Try
            My.Computer.Registry.LocalMachine.OpenSubKey _
("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers", True).DeleteSubKey(PrinterEntry.DriverName)
        Catch ex As Exception
        End Try

        Return True
    End Function

    Public Function PurgePrinterQueue(ByVal PrinterEntry As PrinterQueueInfo) As Boolean
        Try
            'Die Lokalen drucker synchron ermitteln
            Dim ll As New List(Of PrintQueueCollection)

            Dim ff As New EnumeratePrinters
            ll = ff.InternalLocalPrinterCollector(True)

            Dim success As Boolean = False

            For Each item As PrintQueueCollection In ll
                For Each item2 As PrintQueue In item
                    If item2.Name = PrinterEntry.ShareName Then
                        Try
                            item2.Refresh()
                            item2.PrintingIsCancelled = True
                            item2.Commit()
                            item2.Purge()
                            success = True
                        Catch ex As Exception
                            'Evtl. wird Zugriff auf Warteschlange verweigert.
                        End Try
                    End If
                Next
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function RetrievePrinterInformation(ByVal PrinterEntry As PrinterQueueInfo) As Boolean
        Try
            'Die Lokalen drucker synchron ermitteln
            Dim ll As New List(Of PrintQueueCollection)

            Dim ff As New EnumeratePrinters
            ll = ff.InternalLocalPrinterCollector(True)

            Dim success As Boolean = False

            For Each item As PrintQueueCollection In ll
                For Each item2 As PrintQueue In item
                    If item2.Name = PrinterEntry.ShareName Then
                        Try
                            item2.Refresh()
                            item2.Commit()
                            success = True
                        Catch ex As Exception
                            'Evtl. wird Zugriff auf Warteschlange verweigert.
                        End Try
                    End If
                Next
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
