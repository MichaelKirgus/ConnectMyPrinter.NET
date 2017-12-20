Imports Microsoft.Win32

Public Class ForcePrinterDelete
    Public Function DeletePrinter(ByVal PrinterName As String, ByVal UserUUID As String) As Boolean
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.Users.OpenSubKey(UserUUID & "\Printers\Settings", True)
            ww.DeleteValue(PrinterName)
        Catch ex As Exception
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.Users.OpenSubKey(UserUUID & "\Printers\DevModes2", True)
            ww.DeleteValue(PrinterName)
        Catch ex As Exception
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.Users.OpenSubKey(UserUUID & "\Printers\DevModePerUser", True)
            ww.DeleteValue(PrinterName)
        Catch ex As Exception
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.Users.OpenSubKey(UserUUID & "\Printers\Connections", True)
            ww.DeleteValue(PrinterName)
        Catch ex As Exception
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.Users.OpenSubKey(UserUUID & "\Software\Microsoft\Windows NT\CurrentVersion\Devices", True)
            ww.DeleteValue(PrinterName)
        Catch ex As Exception
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.Users.OpenSubKey(UserUUID & "\Software\Microsoft\Windows NT\CurrentVersion\PrinterPorts", True)
            ww.DeleteValue(PrinterName)
        Catch ex As Exception
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.Users.OpenSubKey(UserUUID & "\Software\Microsoft\Windows NT\CurrentVersion\Devices", True)
            Dim key As String
            key = ww.GetValue("Device")
            If key.Contains(PrinterName) Then
                ww.DeleteValue("Device")
            End If
        Catch ex As Exception
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Connections", True)
            ww.DeleteValue(PrinterName)
        Catch ex As Exception
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Connections", True)
            For Each item As String In ww.GetSubKeyNames
                Try
                    For Each item2 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Connections\" & item, True).GetSubKeyNames
                        If item2.ToLower.Contains(PrinterName) Then
                            Try
                                Dim ww2 As RegistryKey
                                ww2 = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Connections\" & item, True)
                                ww2.DeleteSubKey(item2)
                            Catch ex As Exception
                            End Try
                        End If
                    Next
                Catch ex As Exception
                End Try
            Next
            ww.DeleteValue(PrinterName)
        Catch ex As Exception
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion\Print\Printers", True)
            For Each item As String In ww.GetSubKeyNames
                Try
                    If item.ToLower.Contains(PrinterName) Then
                        ww.DeleteSubKey(item)
                    End If
                Catch ex As Exception
                End Try
            Next
            ww.DeleteValue(PrinterName)
        Catch ex As Exception
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet001\Control\Print\Printers", True)
            For Each item As String In ww.GetSubKeyNames
                Try
                    If item.ToLower.Contains(PrinterName) Then
                        ww.DeleteSubKey(item)
                    End If
                Catch ex As Exception
                End Try
            Next
            ww.DeleteValue(PrinterName)
        Catch ex As Exception
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Print\Printers", True)
            For Each item As String In ww.GetSubKeyNames
                Try
                    If item.ToLower.Contains(PrinterName) Then
                        ww.DeleteSubKey(item)
                    End If
                Catch ex As Exception
                End Try
            Next
            ww.DeleteValue(PrinterName)
        Catch ex As Exception
        End Try

        Dim printerid As String = ""
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers", True)
            ww.DeleteSubKey(PrinterName)
        Catch ex As Exception
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet001\Services\LanmanServer\Shares", True)
            ww.DeleteSubKey(PrinterName)
        Catch ex As Exception
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet001\Services\LanmanServer\Shares\Security", True)
            ww.DeleteSubKey(PrinterName)
        Catch ex As Exception
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services\LanmanServer\Shares", True)
            ww.DeleteSubKey(PrinterName)
        Catch ex As Exception
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services\LanmanServer\Shares\Security", True)
            ww.DeleteSubKey(PrinterName)
        Catch ex As Exception
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.CurrentUser.OpenSubKey("Printers\Settings", True)

            For Each item As String In ww.GetValueNames
                If item.ToLower.Contains(PrinterName) Then
                    ww.DeleteValue(item)
                End If
            Next
        Catch ex As Exception
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.CurrentUser.OpenSubKey("\Software\Microsoft\Windows NT\CurrentVersion\PrinterPorts", True)

            For Each item As String In ww.GetValueNames
                If item.ToLower.Contains(PrinterName) Then
                    ww.DeleteValue(item)
                End If
            Next
        Catch ex As Exception
        End Try
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.CurrentUser.OpenSubKey("\Software\Microsoft\Windows NT\CurrentVersion\Devices", True)

            For Each item As String In ww.GetValueNames
                If item.ToLower.Contains(PrinterName) Then
                    ww.DeleteValue(item)
                End If
            Next
        Catch ex As Exception
        End Try

        Return True
    End Function
End Class
