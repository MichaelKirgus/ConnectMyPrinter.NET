'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports System.Management
Imports System.Printing
Imports ConnectMyPrinterEnumerationLib
Imports Microsoft.Win32
Public Class ExportImportPrinterSettings
    Public Function ExportPrinterSettings(ByVal PrinterEntry As PrinterQueueInfo, ByVal Filename As String, Optional ByVal ShellTimeout As Integer = 60000, Optional ByVal Wait As Boolean = False) As Boolean
        Try
            If PrinterEntry.Server = "Lokal" Or PrinterEntry.Server = "Local" Then
                Shell("rundll32 printui.dll,PrintUIEntry /Ss /n " & My.Resources.trenn & PrinterEntry.Name & My.Resources.trenn & " /a " & My.Resources.trenn & Filename & My.Resources.trenn, AppWinStyle.Hide, Wait, ShellTimeout)
            Else
                Shell("rundll32 printui.dll,PrintUIEntry /Ss /n " & PrinterEntry.Server & "\" & PrinterEntry.ShareName & " /a " & My.Resources.trenn & Filename & My.Resources.trenn, AppWinStyle.Hide, Wait, ShellTimeout)
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ImportPrinterSettings(ByVal PrinterEntry As PrinterQueueInfo, ByVal Filename As String, Optional ByVal ShellTimeout As Integer = 60000, Optional ByVal Wait As Boolean = False) As Boolean
        Try
            If PrinterEntry.Server = "Lokal" Or PrinterEntry.Server = "Local" Then
                Shell("rundll32 printui.dll,PrintUIEntry /Sr /n " & My.Resources.trenn & PrinterEntry.Name & My.Resources.trenn & " /a " & My.Resources.trenn & Filename & My.Resources.trenn, AppWinStyle.Hide, Wait, ShellTimeout)
            Else
                Shell("rundll32 printui.dll,PrintUIEntry /Sr /n " & PrinterEntry.Server & "\" & PrinterEntry.ShareName & " /a " & My.Resources.trenn & Filename & My.Resources.trenn, AppWinStyle.Hide, Wait, ShellTimeout)
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
