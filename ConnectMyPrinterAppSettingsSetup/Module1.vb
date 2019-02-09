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

            Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterAppSettingsSetup.TranslatedStrings", GetType(Module1), MCultureInf, "ChangeACLsAtAppSettingsFileStr", ""))

            Try
                AddFileSecurity("C:\Program Files\Michael Kirgus\ConnectMyPrinter.NET\AppSettings.xml", "Everyone",
                        FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files\Michael Kirgus\ConnectMyPrinter.NET\AppSettings.xml", "Jeder",
                        FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files\Michael Kirgus\ConnectMyPrinter.NET\AppSettings.xml", "Benutzer",
                        FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files\Michael Kirgus\ConnectMyPrinter.NET\AppSettings.xml", "User",
                        FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception
            End Try
            Try
                AddFileSecurity("C:\Program Files\Michael Kirgus\ConnectMyPrinter.NET\AppSettings.xml", My.User.Name,
    FileSystemRights.FullControl, AccessControlType.Allow)
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
