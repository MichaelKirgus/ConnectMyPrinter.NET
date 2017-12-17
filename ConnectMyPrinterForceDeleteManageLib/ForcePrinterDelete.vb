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
            ww = My.Computer.Registry.LocalMachine.OpenSubKey(UserUUID & "\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Connections", True)
            ww.DeleteValue(PrinterName)
        Catch ex As Exception
        End Try
        Dim printerid As String = ""
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey(UserUUID & "\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers", True)
            ww.DeleteSubKey(PrinterName)
        Catch ex As Exception
        End Try

        Return True
    End Function
End Class
