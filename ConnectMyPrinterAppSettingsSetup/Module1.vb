Imports System.IO
Imports System.Security.AccessControl

Module Module1

    Sub Main()
        Console.WriteLine("Kopiere Einstellungsdatei von C:\Temp nach C:\Program Files (x86)\Michael Kirgus\ConnectMyPrinter.NET...")
        My.Computer.FileSystem.CopyFile("C:\Temp\AppSettings.xml", "C:\Program Files (x86)\Michael Kirgus\ConnectMyPrinter.NET\AppSettings.xml", True)
        Console.WriteLine("Ändere ACLs für Einstellungsdatei...")
        Try
            AddFileSecurity("C:\Program Files (x86)\Michael Kirgus\ConnectMyPrinter.NET\AppSettings.xml", "Everyone",
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
        Console.WriteLine("Fertig.")
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
