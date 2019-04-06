'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports Microsoft.Win32

Public Class ForcePrinterDelete
    Public Errors As String = ""
    Public FinishedRuns As Integer = 0

    Public Function DeletePrinter(ByVal PrinterName As String, ByVal UserUUID As String, Optional ByVal DeleteLocalMachinePart As Boolean = True) As Boolean
        Dim splitreg As String = PrinterName.ToLower
        Dim servername As String = ""
        Try
            splitreg = PrinterName.Split("\")(3)
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try
        Try
            servername = PrinterName.Split("\")(2)
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
            If key.Contains(PrinterName.ToLower) Or key.Contains(splitreg) Then
                ww.DeleteValue("Device")
                FinishedRuns += 1
            End If
        Catch ex As Exception
            Errors += Err.Description & vbNewLine
        End Try
        If DeleteLocalMachinePart Then
            Try
                Dim ww As RegistryKey
                ww = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Providers\Client Side Rendering Print Provider", True)
                ww.DeleteValue(PrinterName)
                FinishedRuns += 1
                ww.DeleteValue(splitreg)
                FinishedRuns += 1
            Catch ex As Exception
                Errors += Err.Description & vbNewLine
            End Try
            Try
                Dim ww As RegistryKey
                ww = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Providers\Client Side Rendering Print Provider", True)
                For Each item As String In ww.GetSubKeyNames
                    Try
                        For Each item2 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Providers\Client Side Rendering Print Provider\" & item, True).GetSubKeyNames
                            For Each item3 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Providers\Client Side Rendering Print Provider\" & item & "\" & item2, True).GetSubKeyNames
                                If (item3.ToLower.Contains(PrinterName.ToLower) And item3.ToLower.Contains(servername)) Then
                                    Try
                                        Dim ww2 As RegistryKey
                                        ww2 = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Providers\Client Side Rendering Print Provider\" & item & "\" & item2, True)
                                        ww2.DeleteSubKey(item2)
                                        FinishedRuns += 1
                                    Catch ex As Exception
                                    End Try
                                End If
                            Next
                        Next
                    Catch ex As Exception
                        Errors += Err.Description & vbNewLine
                    End Try
                Next
            Catch ex As Exception
                Errors += Err.Description & vbNewLine
            End Try
            Try
                Dim ww As RegistryKey
                ww = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Providers\Client Side Rendering Print Provider\Servers\" & servername, True)
                For Each item As String In ww.GetSubKeyNames
                    Try
                        For Each item2 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Providers\Client Side Rendering Print Provider\Servers\" & servername & "\" & item, True).GetSubKeyNames
                            Try
                                Dim dd As RegistryKey
                                dd = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Providers\Client Side Rendering Print Provider\Servers\" & servername & "\" & item, False)
                                Dim desc As String
                                desc = dd.GetValue("Description")
                                If desc.ToLower.Contains(PrinterName.ToLower) Then
                                    ww.DeleteSubKeyTree(item)
                                    FinishedRuns += 1
                                End If
                            Catch ex As Exception
                                Errors += Err.Description & vbNewLine
                            End Try
                        Next
                    Catch ex As Exception
                        Errors += Err.Description & vbNewLine
                    End Try
                Next
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
                            If item2.ToLower.Contains(PrinterName.ToLower) Or item2.ToLower.Contains(splitreg) Then
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
                        If item.ToLower.Contains(PrinterName.ToLower) Or item.ToLower.Contains(splitreg) Then
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
                        If item.ToLower.Contains(PrinterName.ToLower) Or item.ToLower.Contains(splitreg) Then
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
                        If item.ToLower.Contains(PrinterName.ToLower) Or item.ToLower.Contains(splitreg) Then
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
                        If item.ToLower.Contains(PrinterName.ToLower) Or item.ToLower.Contains(splitreg) Then
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
                        If item.ToLower.Contains(PrinterName.ToLower) Or item.ToLower.Contains(splitreg) Then
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
                ww = My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet001\Control\Print\Environments", True)
                For Each item As String In ww.GetSubKeyNames
                    Try
                        For Each item2 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet001\Control\Print\Environments\" & item, True).GetSubKeyNames
                            For Each item3 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet001\Control\Print\Environments\" & item & "\" & item2, True).GetSubKeyNames
                                For Each item4 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet001\Control\Print\Environments\" & item & "\" & item2 & "\" & item3, True).GetSubKeyNames
                                    If item4.ToLower.Contains(PrinterName.ToLower) Or item4.ToLower.Contains(splitreg) Then
                                        Try
                                            Dim ww2 As RegistryKey
                                            ww2 = My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet001\Control\Print\Environments\" & item & "\" & item2 & "\" & item3, True)
                                            ww2.DeleteSubKey(item4)
                                            FinishedRuns += 1
                                        Catch ex As Exception
                                        End Try
                                    End If
                                Next
                            Next
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
                ww = My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Print\Environments", True)
                For Each item As String In ww.GetSubKeyNames
                    Try
                        For Each item2 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Print\Environments\" & item, True).GetSubKeyNames
                            For Each item3 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Print\Environments\" & item & "\" & item2, True).GetSubKeyNames
                                For Each item4 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Print\Environments\" & item & "\" & item2 & "\" & item3, True).GetSubKeyNames
                                    If item4.ToLower.Contains(PrinterName.ToLower) Or item4.ToLower.Contains(splitreg) Then
                                        Try
                                            Dim ww2 As RegistryKey
                                            ww2 = My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Print\Environments\" & item & "\" & item2 & "\" & item3, True)
                                            ww2.DeleteSubKey(item4)
                                            FinishedRuns += 1
                                        Catch ex As Exception
                                        End Try
                                    End If
                                Next
                            Next
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
                ww = My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services\LanmanServer\Shares\Security", True)
                ww.DeleteSubKey(PrinterName)
                FinishedRuns += 1
                ww.DeleteSubKey(splitreg)
                FinishedRuns += 1
            Catch ex As Exception
                Errors += Err.Description & vbNewLine
            End Try
        End If

        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.CurrentUser.OpenSubKey("Printers\Settings", True)

            For Each item As String In ww.GetValueNames
                If item.ToLower.Contains(PrinterName.ToLower) Or item.ToLower.Contains(splitreg) Then
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
                If item.ToLower.Contains(PrinterName.ToLower) Or item.ToLower.Contains(splitreg) Then
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
                If item.ToLower.Contains(PrinterName.ToLower) Or item.ToLower.Contains(splitreg) Then
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
