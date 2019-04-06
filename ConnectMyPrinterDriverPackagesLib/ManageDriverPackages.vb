'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports Microsoft.Win32

Public Class ManageDriverPackages
    Public Function ListAllDriverPackets(Optional ByVal ResolveDriverName As Boolean = False) As List(Of DriverPackageItem)
        Try
            Dim collection As New List(Of DriverPackageItem)

            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\PackageInstallation", False)
            For Each item As String In ww.GetSubKeyNames
                Try
                    For Each item2 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\PackageInstallation\" & item, False).GetSubKeyNames
                        For Each item3 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\PackageInstallation\" & item & "\" & item2, False).GetSubKeyNames
                            Dim ntprint As Boolean = False
                            Dim classkey As Boolean = False
                            If item3.Contains("ntprint.inf") Then
                                ntprint = True
                            End If
                            If item3.Contains("{") Then
                                classkey = True
                            End If

                            If ntprint = False And classkey = False Then
                                Try
                                    Dim jj As New DriverPackageItem
                                    Dim ww2 As RegistryKey
                                    ww2 = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\PackageInstallation\" & item & "\" & item2 & "\" & item3, False)
                                    jj.CabPath = ww2.GetValue("CabPath")
                                    jj.DriverStorePath = ww2.GetValue("DriverStorePath")
                                    jj.DriverKeyName = item3
                                    If ResolveDriverName Then
                                        jj.DriverName = GetDriverNameFromDriverPacket(jj)
                                    End If

                                    collection.Add(jj)
                                Catch ex As Exception
                                End Try
                            End If
                        Next
                    Next
                Catch ex As Exception
                End Try
            Next

            Return collection
        Catch ex As Exception
            Return New List(Of DriverPackageItem)
        End Try
    End Function

    Public Function GetDriverNameFromDriverPacket(ByVal Packet As DriverPackageItem) As String
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Print\Environments", False)
            For Each item As String In ww.GetSubKeyNames
                Try
                    For Each item2 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Print\Environments\" & item, False).GetSubKeyNames
                        For Each item3 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Print\Environments\" & item & "\" & item2, False).GetSubKeyNames
                            For Each item4 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Print\Environments\" & item & "\" & item2 & "\" & item3, False).GetSubKeyNames
                                Dim ww2 As RegistryKey
                                ww2 = My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Print\Environments\" & item & "\" & item2 & "\" & item3 & "\" & item4, False)
                                Dim infpath As String
                                infpath = ww2.GetValue("InfPath")
                                Dim splitinf As Array
                                splitinf = Packet.DriverStorePath.Split("\")
                                Dim inffile As String
                                inffile = splitinf(splitinf.Length - 1)
                                If infpath.EndsWith(inffile) Then
                                    Return item4
                                End If
                            Next
                        Next
                    Next
                Catch ex As Exception
                End Try
            Next
            Return ""
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function GetAllDrivers() As List(Of DriverPackageItem)
        Try
            Dim oo As New List(Of DriverPackageItem)
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Print\Environments", False)
            For Each item As String In ww.GetSubKeyNames
                Try
                    For Each item2 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Print\Environments\" & item, False).GetSubKeyNames
                        For Each item3 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Print\Environments\" & item & "\" & item2, False).GetSubKeyNames
                            For Each item4 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Print\Environments\" & item & "\" & item2 & "\" & item3, False).GetSubKeyNames
                                Dim Package As New DriverPackageItem
                                Dim ww2 As RegistryKey
                                ww2 = My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Print\Environments\" & item & "\" & item2 & "\" & item3 & "\" & item4, False)
                                Package.DriverStorePath = ww2.GetValue("InfPath")

                                Try
                                    Package.DriverVersion = ww2.GetValue("DriverVersion")
                                    Package.DriverDate = ww2.GetValue("DriverDate")
                                    Package.DriverKeyName = ww2.GetValue("Driver")
                                Catch ex As Exception
                                End Try

                                Package.DriverName = item4
                                oo.Add(Package)
                            Next
                        Next
                    Next
                Catch ex As Exception
                End Try
            Next
            Return oo
        Catch ex As Exception
            Return New List(Of DriverPackageItem)
        End Try
    End Function

    Public Function GetDriverCABFileCreationTime(ByVal File As String) As String
        Try
            Dim bb As New IO.FileInfo(File)
            Return bb.CreationTime.ToShortDateString
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function DeleteDriver(ByVal Packet As DriverPackageItem, Optional ByVal DeleteDriverFiles As Boolean = True) As Boolean
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Print\Environments", False)
            For Each item As String In ww.GetSubKeyNames
                Try
                    For Each item2 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Print\Environments\" & item, False).GetSubKeyNames
                        For Each item3 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Print\Environments\" & item & "\" & item2, False).GetSubKeyNames
                            For Each item4 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Print\Environments\" & item & "\" & item2 & "\" & item3, False).GetSubKeyNames
                                Dim ww2 As RegistryKey
                                ww2 = My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Print\Environments\" & item & "\" & item2 & "\" & item3 & "\" & item4, False)
                                Dim infpath As String
                                infpath = ww2.GetValue("InfPath")
                                Dim splitinf As Array
                                splitinf = Packet.DriverStorePath.Split("\")
                                Dim inffile As String
                                inffile = splitinf(splitinf.Length - 1)
                                If infpath.EndsWith(inffile) Then
                                    Dim ww3 As RegistryKey
                                    ww3 = My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Print\Environments\" & item & "\" & item2 & "\" & item3, True)
                                    ww3.DeleteSubKeyTree(item4)
                                    If DeleteDriverFiles Then
                                        Dim qq As New IO.FileInfo(infpath)
                                        For Each item5 As String In IO.Directory.GetFiles(qq.DirectoryName)
                                            My.Computer.FileSystem.DeleteFile(item5)
                                        Next
                                        IO.Directory.Delete(qq.DirectoryName, True)
                                    End If
                                End If
                                Dim depfiles As String()
                                depfiles = ww2.GetValue("Dependent Files")
                                Dim x86driverbasicpath As String = "%SystemRoot%\System32\spool\drivers\W32X86\3"
                                Dim x64driverbasicpath As String = "%SystemRoot%\System32\spool\drivers\x64\3"
                                For Each files As String In IO.Directory.GetFiles(x86driverbasicpath)
                                    Dim filename As New IO.FileInfo(files)
                                    For Each drvfile As String In depfiles
                                        If filename.Name.ToLower = drvfile.ToLower Then
                                            Try
                                                My.Computer.FileSystem.DeleteFile(files)
                                            Catch ex As Exception
                                            End Try
                                        End If
                                    Next
                                Next
                                For Each files As String In IO.Directory.GetFiles(x64driverbasicpath)
                                    Dim filename As New IO.FileInfo(files)
                                    For Each drvfile As String In depfiles
                                        If filename.Name.ToLower = drvfile.ToLower Then
                                            Try
                                                My.Computer.FileSystem.DeleteFile(files)
                                            Catch ex As Exception
                                            End Try
                                        End If
                                    Next
                                Next
                            Next
                        Next
                    Next
                Catch ex As Exception
                End Try
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DeleteDriverPacket(ByVal Packet As DriverPackageItem) As Boolean
        Try
            Dim ww As RegistryKey
            ww = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\PackageInstallation", False)
            For Each item As String In ww.GetSubKeyNames
                Try
                    For Each item2 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\PackageInstallation\" & item, False).GetSubKeyNames
                        For Each item3 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\PackageInstallation\" & item & "\" & item2, False).GetSubKeyNames
                            If item3.ToLower = Packet.DriverKeyName.ToLower Then
                                Dim ww2 As RegistryKey
                                ww2 = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\PackageInstallation\" & item & "\" & item2, True)
                                ww2.DeleteSubKeyTree(item3)
                            End If
                        Next
                    Next
                Catch ex As Exception
                End Try
            Next

            Dim ww3 As RegistryKey
            ww3 = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion\Print\PackageInstallation", False)
            For Each item As String In ww3.GetSubKeyNames
                Try
                    For Each item2 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion\Print\PackageInstallation\" & item, False).GetSubKeyNames
                        For Each item3 As String In My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion\Print\PackageInstallation\" & item & "\" & item2, False).GetSubKeyNames
                            If item3.ToLower = Packet.DriverKeyName.ToLower Then
                                Dim ww2 As RegistryKey
                                ww2 = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion\Print\PackageInstallation\" & item & "\" & item2, True)
                                ww2.DeleteSubKeyTree(item3)
                            End If
                        Next
                    Next
                Catch ex As Exception
                End Try
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
