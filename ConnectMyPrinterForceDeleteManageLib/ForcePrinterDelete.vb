﻿Imports Microsoft.Win32

Public Class ForcePrinterDelete
    Public Errors As String = ""
    Public FinishedRuns As Integer = 0

    Public Function DeletePrinter(ByVal PrinterName As String, ByVal UserUUID As String) As Boolean
        Dim splitreg As String = PrinterName
        Try
            splitreg = PrinterName.Split("\")(2)
            FinishedRuns += 1
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.Users.OpenSubKey(UserUUID & "\Printers\Settings", True)
            ww.DeleteValue(PrinterName)
            FinishedRuns += 1
            ww.DeleteValue(splitreg)
            FinishedRuns += 1
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.Users.OpenSubKey(UserUUID & "\Printers\DevModes2", True)
            ww.DeleteValue(PrinterName)
            FinishedRuns += 1
            ww.DeleteValue(splitreg)
            FinishedRuns += 1
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.Users.OpenSubKey(UserUUID & "\Printers\DevModePerUser", True)
            ww.DeleteValue(PrinterName)
            FinishedRuns += 1
            ww.DeleteValue(splitreg)
            FinishedRuns += 1
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.Users.OpenSubKey(UserUUID & "\Printers\Connections", True)
            ww.DeleteValue(PrinterName)
            FinishedRuns += 1
            ww.DeleteValue(splitreg)
            FinishedRuns += 1
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.Users.OpenSubKey(UserUUID & "\Software\Microsoft\Windows NT\CurrentVersion\Devices", True)
            ww.DeleteValue(PrinterName)
            FinishedRuns += 1
            ww.DeleteValue(splitreg)
            FinishedRuns += 1
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.Users.OpenSubKey(UserUUID & "\Software\Microsoft\Windows NT\CurrentVersion\PrinterPorts", True)
            ww.DeleteValue(PrinterName)
            FinishedRuns += 1
            ww.DeleteValue(splitreg)
            FinishedRuns += 1
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.Users.OpenSubKey(UserUUID & "\Software\Microsoft\Windows NT\CurrentVersion\Devices", True)
            Dim key As String
            key = ww.GetValue("Device")
            If key.Contains(PrinterName) Or key.Contains(splitreg) Then
                ww.DeleteValue("Device")
                FinishedRuns += 1
            End If
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Connections", True)
            ww.DeleteValue(PrinterName)
            FinishedRuns += 1
            ww.DeleteValue(splitreg)
            FinishedRuns += 1
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Connections", True)
            For Each item As String In ww.GetSubKeyNames
                Try
                    For Each item2 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Connections\" & item, True).GetSubKeyNames
                        If item2.ToLower.Contains(PrinterName) Or item2.ToLower.Contains(splitreg) Then
                            Try
                                Dim ww2 As RegistryKey
                                ww2 = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Connections\" & item, True)
                                ww2.DeleteSubKey(item2)
                                FinishedRuns += 1
                            Catch ex As Exception
                            End Try
                        End If
                    Next
                Catch ex As Exception
                    Errors += Err.Description & vbNewLine
                End Try
            Next
            ww.DeleteValue(PrinterName)
            FinishedRuns += 1
            ww.DeleteValue(splitreg)
            FinishedRuns += 1
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion\Print\Printers", True)
            For Each item As String In ww.GetSubKeyNames
                Try
                    If item.ToLower.Contains(PrinterName) Or item.ToLower.Contains(splitreg) Then
                        ww.DeleteSubKey(item)
                        FinishedRuns += 1
                    End If
                Catch ex As Exception
                    Errors += Err.Description & vbNewLine
                End Try
            Next
            ww.DeleteValue(PrinterName)
            FinishedRuns += 1
            ww.DeleteValue(splitreg)
            FinishedRuns += 1
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion\Print\Printers", True)
            For Each item As String In ww.GetSubKeyNames
                Try
                    If item.ToLower.Contains(PrinterName) Or item.ToLower.Contains(splitreg) Then
                        ww.DeleteSubKeyTree(item)
                        FinishedRuns += 1
                    End If
                Catch ex As Exception
                    Errors += Err.Description & vbNewLine
                End Try
            Next
            ww.DeleteSubKeyTree(PrinterName)
            FinishedRuns += 1
            ww.DeleteSubKeyTree(splitreg)
            FinishedRuns += 1
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers", True)
            For Each item As String In ww.GetSubKeyNames
                Try
                    If item.ToLower.Contains(PrinterName) Or item.ToLower.Contains(splitreg) Then
                        ww.DeleteSubKey(item)
                        FinishedRuns += 1
                    End If
                Catch ex As Exception
                    Errors += Err.Description & vbNewLine
                End Try
            Next
            ww.DeleteValue(PrinterName)
            FinishedRuns += 1
            ww.DeleteValue(splitreg)
            FinishedRuns += 1
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet001\Control\Print\Printers", True)
            For Each item As String In ww.GetSubKeyNames
                Try
                    If item.ToLower.Contains(PrinterName) Or item.ToLower.Contains(splitreg) Then
                        ww.DeleteSubKey(item)
                        FinishedRuns += 1
                    End If
                Catch ex As Exception
                    Errors += Err.Description & vbNewLine
                End Try
            Next
            ww.DeleteValue(PrinterName)
            FinishedRuns += 1
            ww.DeleteValue(splitreg)
            FinishedRuns += 1
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Print\Printers", True)
            For Each item As String In ww.GetSubKeyNames
                Try
                    If item.ToLower.Contains(PrinterName) Or item.ToLower.Contains(splitreg) Then
                        ww.DeleteSubKey(item)
                        FinishedRuns += 1
                    End If
                Catch ex As Exception
                    Errors += Err.Description & vbNewLine
                End Try
            Next
            ww.DeleteValue(PrinterName)
            FinishedRuns += 1
            ww.DeleteValue(splitreg)
            FinishedRuns += 1
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers", True)
            ww.DeleteSubKey(PrinterName)
            FinishedRuns += 1
            ww.DeleteSubKey(splitreg)
            FinishedRuns += 1
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers", True)
            ww.DeleteSubKeyTree(PrinterName)
            FinishedRuns += 1
            ww.DeleteSubKeyTree(splitreg)
            FinishedRuns += 1
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet001\Services\LanmanServer\Shares", True)
            ww.DeleteSubKey(PrinterName)
            FinishedRuns += 1
            ww.DeleteSubKey(splitreg)
            FinishedRuns += 1
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet001\Services\LanmanServer\Shares\Security", True)
            ww.DeleteSubKey(PrinterName)
            FinishedRuns += 1
            ww.DeleteSubKey(splitreg)
            FinishedRuns += 1
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services\LanmanServer\Shares", True)
            ww.DeleteSubKey(PrinterName)
            FinishedRuns += 1
            ww.DeleteSubKey(splitreg)
            FinishedRuns += 1
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services\LanmanServer\Shares\Security", True)
            ww.DeleteSubKey(PrinterName)
            FinishedRuns += 1
            ww.DeleteSubKey(splitreg)
            FinishedRuns += 1
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.CurrentUser.OpenSubKey("Printers\Settings", True)

            For Each item As String In ww.GetValueNames
                If item.ToLower.Contains(PrinterName) Or item.ToLower.Contains(splitreg) Then
                    ww.DeleteValue(item)
                    FinishedRuns += 1
                End If
            Next
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.CurrentUser.OpenSubKey("\Software\Microsoft\Windows NT\CurrentVersion\PrinterPorts", True)

            For Each item As String In ww.GetValueNames
                If item.ToLower.Contains(PrinterName) Or item.ToLower.Contains(splitreg) Then
                    ww.DeleteValue(item)
                    FinishedRuns += 1
                End If
            Next
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.CurrentUser.OpenSubKey("\Software\Microsoft\Windows NT\CurrentVersion\Devices", True)

            For Each item As String In ww.GetValueNames
                If item.ToLower.Contains(PrinterName) Or item.ToLower.Contains(splitreg) Then
                    ww.DeleteValue(item)
                    FinishedRuns += 1
                End If
            Next
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try

        Return True
    End Function
End Class
