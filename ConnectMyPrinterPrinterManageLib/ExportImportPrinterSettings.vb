Imports System.Management
Imports System.Printing
Imports ConnectMyPrinterEnumerationLib
Imports Microsoft.Win32
Public Class ExportImportPrinterSettings
    Public Function ExportPrinterSettings(ByVal PrinterEntry As PrinterQueueInfo, ByVal Filename As String) As Boolean
        Try
            If PrinterEntry.Server = "Lokal" Then
                Shell("rundll32 printui.dll,PrintUIEntry /Ss /n " & My.Resources.trenn & PrinterEntry.Name & My.Resources.trenn & " /a " & My.Resources.trenn & Filename & My.Resources.trenn)
            Else
                Shell("rundll32 printui.dll,PrintUIEntry /Ss /n " & PrinterEntry.Server & "\" & PrinterEntry.ShareName & " /a " & My.Resources.trenn & Filename & My.Resources.trenn)
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ImportPrinterSettings(ByVal PrinterEntry As PrinterQueueInfo, ByVal Filename As String) As Boolean
        Try
            If PrinterEntry.Server = "Lokal" Then
                Shell("rundll32 printui.dll,PrintUIEntry /Sr /n " & My.Resources.trenn & PrinterEntry.Name & My.Resources.trenn & " /a " & My.Resources.trenn & Filename & My.Resources.trenn)
            Else
                Shell("rundll32 printui.dll,PrintUIEntry /Sr /n " & PrinterEntry.Server & "\" & PrinterEntry.ShareName & " /a " & My.Resources.trenn & Filename & My.Resources.trenn)
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
