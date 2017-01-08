Imports System.Security
Imports System.Text
Imports ConnectMyPrinterEnumerationLib

Public Class ElevationHelperClass
    Public Function GenerateActionFile(ByVal Action As String, PrinterObj As PrinterQueueInfo, ByVal _parent As Form1, ByVal CurrCtl As PrinterCtl) As Boolean
        Try
            Dim tmp As String
            Dim hh As New PrinterEnumerationSerializer
            Dim tmp2 As String = "C:\Windows\Temp"
            tmp = tmp2 & "\" & _parent.ActionFileDir & "\" & CurrCtl.CurrActionFileIndex & "_" & Action & ".UAC"
            If Not IO.Directory.Exists(tmp2 & "\" & _parent.ActionFileDir) Then
                IO.Directory.CreateDirectory(tmp2 & "\" & _parent.ActionFileDir)
            End If

            hh.SaveQueueFile(PrinterObj, tmp)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ConvertToSecureString(ByVal Str As String) As Security.SecureString
        Try
            Dim passwordChars As Char() = Str.ToCharArray()
            Dim password As New SecureString()
            For Each c As Char In passwordChars
                password.AppendChar(c)
            Next

            Return password
        Catch ex As Exception
            Return New SecureString
        End Try
    End Function

    Public Function StartElevatedActions(ByVal _parent As Form1, ByVal CurrCtl As PrinterCtl) As Boolean
        Try
            Dim jj As New Process
            Dim tmp2 As String = "C:\Windows\Temp"
            jj.StartInfo.FileName = "ConnectMyPrinterUACHelper.exe"
            jj.StartInfo.Arguments = tmp2 & "\" & _parent.ActionFileDir
            jj.StartInfo.Verb = "runas"

            If _parent.AppSettings.EnableElevationBypass = True Then
                jj.StartInfo.Domain = _parent.AppSettings.ElevationBypassDomain
                jj.StartInfo.UserName = Encoding.UTF8.GetString(Convert.FromBase64String(_parent.AppSettings.ElevationBypassUsername))
                jj.StartInfo.Password = ConvertToSecureString(Encoding.UTF8.GetString(Convert.FromBase64String(_parent.AppSettings.ElevationBypassUsername)))
            End If
            jj.Start()
            jj.WaitForExit()

            CurrCtl.CurrActionFileIndex = 0

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
