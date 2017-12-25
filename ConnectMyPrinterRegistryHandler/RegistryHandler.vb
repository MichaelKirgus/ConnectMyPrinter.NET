Imports ConnectMyPrinterUserListLib
Imports Microsoft.Win32

Public Class RegistryHandler
    Public Function GetAllUsers() As List(Of UserListClass)
        Try
            Dim qq As New List(Of UserListClass)
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\ProfileList", False)
            For Each item As String In ww.GetSubKeyNames
                Try
                    Dim kk As New UserListClass
                    kk._KEY = item

                    Dim pp As RegistryKey
                    pp = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\ProfileList\" & kk._KEY, False)
                    Dim tmpuser As String
                    tmpuser = pp.GetValue("ProfileImagePath")
                    Try
                        kk._Username = tmpuser.Split("\")(2)
                    Catch ex As Exception
                        kk._Username = "Unbekannt"
                    End Try
                    qq.Add(kk)
                Catch ex As Exception
                End Try
            Next

            Return qq
        Catch ex As Exception
            Return New List(Of UserListClass)
        End Try
    End Function

    Public Function GetLocalPrinters() As List(Of String)
        Try
            Dim _res As New List(Of String)
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers", True)
            For Each item As String In ww.GetSubKeyNames
                Try
                    _res.Add(item)
                Catch ex As Exception
                End Try
            Next

            Return _res
        Catch ex As Exception
            Return New List(Of String)
        End Try
    End Function


    Public Function GetAllPrintersForUser(ByVal Userinfo As UserListClass) As List(Of String)
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.Users.OpenSubKey(Userinfo._KEY & "\Printers\Settings", False)

            Dim jj As New List(Of String)
            For Each item As String In ww.GetValueNames
                jj.Add(item)
            Next

            Return jj
        Catch ex As Exception
            Return New List(Of String)
        End Try
    End Function
End Class
