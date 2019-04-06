'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
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
