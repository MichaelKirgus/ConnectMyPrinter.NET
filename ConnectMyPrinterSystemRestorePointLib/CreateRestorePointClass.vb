Imports Microsoft.Win32

Public Class CreateRestorePointClass
    Public Function CreatePoint(ByVal Name As String, ByVal Wait As Boolean) As Boolean
        Try
            Shell("Wmic.exe /Namespace:\\root\default Path SystemRestore Call CreateRestorePoint " & My.Resources.trenn & Name & My.Resources.trenn & ", 100, 1", AppWinStyle.Hide, Wait)
            Return True
        Catch ex As Exception
            Console.WriteLine(Err.Description)
            Return False
        End Try
    End Function

    Public Function EnsureCreationPoint() As Boolean
        Try
            Dim pp As RegistryKey
            pp = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\SystemRestore", True)
            pp.SetValue("SystemRestorePointCreationFrequency", 1, RegistryValueKind.DWord)
            pp.Flush()

            Return True
        Catch ex As Exception
            Console.WriteLine(Err.Description)
            Return False
        End Try
    End Function
End Class
