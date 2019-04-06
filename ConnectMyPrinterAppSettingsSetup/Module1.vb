'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports System.Globalization
Imports System.IO
Imports System.Security.AccessControl
Imports ConnectMyPrinterLanguageHelper

Module Module1

    Public MLangHelper As New LanguageApplyHelper
    Public MCultureInf As CultureInfo = CultureInfo.CurrentUICulture

    Sub Main()
        If Environment.Is64BitOperatingSystem Then
            MLangHelper.GetCultureString("ConnectMyPrinterAppSettingsSetup.TranslatedStrings", GetType(Module1), MCultureInf, "CopyPictureLogoToProgramPathStr", "")

            Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterAppSettingsSetup.TranslatedStrings", GetType(Module1), MCultureInf, "CopyFileToProgramPathStr", ""))
            Try
                My.Computer.FileSystem.CopyFile("C:\Temp\AppSettings.xml", "C:\Program Files\Michael Kirgus\ConnectMyPrinter.NET\AppSettings.xml", True)
                Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterAppSettingsSetup.TranslatedStrings", GetType(Module1), MCultureInf, "CopyFileToProgramPathFinishedStr", ""))
            Catch ex As Exception
                Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterAppSettingsSetup.TranslatedStrings", GetType(Module1), MCultureInf, "CopyFileToProgramPathErrorStr", ""))
            End Try
            Try
                My.Computer.FileSystem.CopyFile("C:\Temp\AppSettings_de-DE.xml", "C:\Program Files\Michael Kirgus\ConnectMyPrinter.NET\AppSettings_de-DE.xml", True)
            Catch ex As Exception
            End Try
            Try
                My.Computer.FileSystem.CopyFile("C:\Temp\AppSettings_en-US.xml", "C:\Program Files\Michael Kirgus\ConnectMyPrinter.NET\AppSettings_en-US.xml", True)
            Catch ex As Exception
            End Try

            Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterAppSettingsSetup.TranslatedStrings", GetType(Module1), MCultureInf, "ChangeACLsAtAppSettingsFileStr", ""))

            Try
                AddFileSecurity("C:\Program Files\Michael Kirgus\ConnectMyPrinter.NET\AppSettings.xml", "Everyone", FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files\Michael Kirgus\ConnectMyPrinter.NET\AppSettings.xml", "Jeder", FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files\Michael Kirgus\ConnectMyPrinter.NET\AppSettings.xml", "Benutzer", FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files\Michael Kirgus\ConnectMyPrinter.NET\AppSettings.xml", "User", FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files\Michael Kirgus\ConnectMyPrinter.NET\AppSettings.xml", My.User.Name, FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files\Michael Kirgus\ConnectMyPrinter.NET\AppSettings_de-DE.xml", "Everyone", FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files\Michael Kirgus\ConnectMyPrinter.NET\AppSettings_de-DE.xml", "Jeder", FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files\Michael Kirgus\ConnectMyPrinter.NET\AppSettings_de-DE.xml", "Benutzer", FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files\Michael Kirgus\ConnectMyPrinter.NET\AppSettings_de-DE.xml", "User", FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files\Michael Kirgus\ConnectMyPrinter.NET\AppSettings_de-DE.xml", My.User.Name, FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files\Michael Kirgus\ConnectMyPrinter.NET\AppSettings_en-US.xml", "Everyone", FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files\Michael Kirgus\ConnectMyPrinter.NET\AppSettings_en-US.xml", "Jeder", FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files\Michael Kirgus\ConnectMyPrinter.NET\AppSettings_en-US.xml", "Benutzer", FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files\Michael Kirgus\ConnectMyPrinter.NET\AppSettings_en-US.xml", "User", FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files\Michael Kirgus\ConnectMyPrinter.NET\AppSettings_en-US.xml", My.User.Name, FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterAppSettingsSetup.TranslatedStrings", GetType(Module1), MCultureInf, "CopyPictureLogoToProgramPathStr", ""))
            Try
                My.Computer.FileSystem.CopyFile("C:\Temp\logo.gif", "C:\Program Files\Michael Kirgus\ConnectMyPrinter.NET\logo.gif", True)
                Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterAppSettingsSetup.TranslatedStrings", GetType(Module1), MCultureInf, "CopyPictureLogoToProgramPathFinishedStr", ""))
            Catch ex As Exception
                Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterAppSettingsSetup.TranslatedStrings", GetType(Module1), MCultureInf, "CopyPictureLogoToProgramPathErrorStr", ""))
            End Try
        Else
            MLangHelper.GetCultureString("ConnectMyPrinterAppSettingsSetup.TranslatedStrings", GetType(Module1), MCultureInf, "CopyPictureLogoToProgramPathStr", "")
            Try
                My.Computer.FileSystem.CopyFile("C:\Temp\AppSettings.xml", "C:\Program Files (x86)\Michael Kirgus\ConnectMyPrinter.NET\AppSettings.xml", True)
                Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterAppSettingsSetup.TranslatedStrings", GetType(Module1), MCultureInf, "CopyFileToProgramPathFinishedStr", ""))
            Catch ex As Exception
                Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterAppSettingsSetup.TranslatedStrings", GetType(Module1), MCultureInf, "CopyFileToProgramPathErrorStr", ""))
            End Try
            Try
                My.Computer.FileSystem.CopyFile("C:\Temp\AppSettings_de-DE.xml", "C:\Program Files (x86)\Michael Kirgus\ConnectMyPrinter.NET\AppSettings_de-DE.xml", True)
            Catch ex As Exception
            End Try
            Try
                My.Computer.FileSystem.CopyFile("C:\Temp\AppSettings_en-US.xml", "C:\Program Files (x86)\Michael Kirgus\ConnectMyPrinter.NET\AppSettings_en-US.xml", True)
            Catch ex As Exception
            End Try

            Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterAppSettingsSetup.TranslatedStrings", GetType(Module1), MCultureInf, "ChangeACLsAtAppSettingsFileStr", ""))

            Try
                AddFileSecurity("C:\Program Files (x86)\Michael Kirgus\ConnectMyPrinter.NET\AppSettings.xml", "Everyone",
                        FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files (x86)\Michael Kirgus\ConnectMyPrinter.NET\AppSettings.xml", "Jeder",
                        FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files (x86)\Michael Kirgus\ConnectMyPrinter.NET\AppSettings.xml", "Benutzer",
                        FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files (x86)\Michael Kirgus\ConnectMyPrinter.NET\AppSettings.xml", "User",
                        FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files (x86)\Michael Kirgus\ConnectMyPrinter.NET\AppSettings.xml", My.User.Name,
    FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files (x86)\Michael Kirgus\ConnectMyPrinter.NET\AppSettings_de-DE.xml", "Everyone", FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files (x86)\Michael Kirgus\ConnectMyPrinter.NET\AppSettings_de-DE.xml", "Jeder", FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files (x86)\Michael Kirgus\ConnectMyPrinter.NET\AppSettings_de-DE.xml", "Benutzer", FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files (x86)\Michael Kirgus\ConnectMyPrinter.NET\AppSettings_de-DE.xml", "User", FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files (x86)\Michael Kirgus\ConnectMyPrinter.NET\AppSettings_de-DE.xml", My.User.Name, FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files (x86)\Michael Kirgus\ConnectMyPrinter.NET\AppSettings_en-US.xml", "Everyone", FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files (x86)\Michael Kirgus\ConnectMyPrinter.NET\AppSettings_en-US.xml", "Jeder", FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files (x86)\Michael Kirgus\ConnectMyPrinter.NET\AppSettings_en-US.xml", "Benutzer", FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files (x86)\Michael Kirgus\ConnectMyPrinter.NET\AppSettings_en-US.xml", "User", FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files (x86)\Michael Kirgus\ConnectMyPrinter.NET\AppSettings_en-US.xml", My.User.Name, FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterAppSettingsSetup.TranslatedStrings", GetType(Module1), MCultureInf, "CopyPictureLogoToProgramPathStr", ""))
            Try
                My.Computer.FileSystem.CopyFile("C:\Temp\logo.gif", "C:\Program Files (x86)\Michael Kirgus\ConnectMyPrinter.NET\logo.gif", True)
                Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterAppSettingsSetup.TranslatedStrings", GetType(Module1), MCultureInf, "CopyPictureLogoToProgramPathFinishedStr", ""))
            Catch ex As Exception
                Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterAppSettingsSetup.TranslatedStrings", GetType(Module1), MCultureInf, "CopyPictureLogoToProgramPathErrorStr", ""))
            End Try
        End If

        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterAppSettingsSetup.TranslatedStrings", GetType(Module1), MCultureInf, "FinishedStr", ""))
    End Sub

    Sub AddFileSecurity(ByVal fileName As String, ByVal account As String,
    ByVal rights As FileSystemRights, ByVal controlType As AccessControlType)

        ' Get a FileSecurity object that represents the 
        ' current security settings.
        Dim fSecurity As FileSecurity = File.GetAccessControl(fileName)

        ' Add the FileSystemAccessRule to the security settings. 
        Dim accessRule As FileSystemAccessRule =
            New FileSystemAccessRule(account, rights, controlType)

        fSecurity.AddAccessRule(accessRule)

        ' Set the new access settings.
        File.SetAccessControl(fileName, fSecurity)
    End Sub
End Module
