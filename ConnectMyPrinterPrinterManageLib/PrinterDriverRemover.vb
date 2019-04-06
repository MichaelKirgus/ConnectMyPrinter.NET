'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports ConnectMyPrinterEnumerationLib

Public Class PrinterDriverRemover
    Public Function DeletePrinterDriverFiles(ByVal PrinterObj As PrinterQueueInfo) As Boolean
        Try
            Dim _parentdir As String
            Dim ll As New IO.FileInfo(PrinterObj.Driver.DriverPath)
            _parentdir = ll.DirectoryName

            For Each item As String In IO.Directory.GetFiles(_parentdir)
                Try
                    My.Computer.FileSystem.DeleteFile(item)
                Catch ex As Exception
                End Try
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TakeownPrinterDriver(ByVal PrinterObj As PrinterQueueInfo) As Boolean
        Try
            Dim _parentdir As String
            Dim ll As New IO.FileInfo(PrinterObj.Driver.DriverPath)
            _parentdir = ll.DirectoryName

            Shell("WMIC FSDir " & My.Resources.trenn & _parentdir & My.Resources.trenn & " CALL TakeOwnershipEx 1", AppWinStyle.Hide, True)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DeleteUnusedDrivers(ByVal PrinterAdminPath As String) As Boolean
        Try
            Dim tmp2 As String = "C:\Windows\Temp\PrinterRemove\DeleteUnusedDrivers.bat"

            If Not IO.Directory.Exists("C:\Windows\Temp\PrinterRemove") Then
                IO.Directory.CreateDirectory("C:\Windows\Temp\PrinterRemove")
            End If

            Dim shellstr As String
            shellstr = "cscript " & PrinterAdminPath & "\prndrvr.vbs -x"
            My.Computer.FileSystem.WriteAllText(tmp2, shellstr, False, Text.Encoding.ASCII)

            Shell(tmp2, AppWinStyle.Hide, True)

            My.Computer.FileSystem.DeleteFile(tmp2)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DeleteAllPrintersAndDrivers(ByVal PrinterAdminPath As String) As Boolean
        Try
            Dim tmp2 As String = "C:\Windows\Temp\PrinterRemove\DeleteAllPrinters.bat"

            If Not IO.Directory.Exists("C:\Windows\Temp\PrinterRemove") Then
                IO.Directory.CreateDirectory("C:\Windows\Temp\PrinterRemove")
            End If

            Dim shellstr As String
            shellstr = "cscript " & PrinterAdminPath & "\prnmngr.vbs -x"
            My.Computer.FileSystem.WriteAllText(tmp2, shellstr, False, Text.Encoding.ASCII)

            Shell(tmp2, AppWinStyle.Hide, True)

            My.Computer.FileSystem.DeleteFile(tmp2)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
