Imports ConnectMyPrinter.NET
Imports ConnectMyPrinterAppSettingsHandler
Imports ConnectMyPrinterPrinterManageLib

Module Module1
    Dim AdminCheck As New ConnectMyPrinterACLHelperLib.HelperFunctions
    Dim ElevationHelper As New ElevationHelperClass
    Dim PrinterDriverRemoverService As New PrinterDriverRemover
    Dim AppSettings As New AppSettingsClass
    Dim PrinterManageService As New ManagePrinter
    Dim AppSettingFile As String = "AppSettings.xml"
    Dim ActionFileDir As String = "PrinterActionsElv"
    Dim FormModule As Form1 = New Form1

    Sub Main()
        Console.WriteLine("Lade Umgebung...")
        Console.WriteLine("Lade Einstellungsdatei...")
        'Laden der Einstellungen für alle Benutzer
        If IO.File.Exists(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & AppSettingFile) Then
            AppSettingFile = My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & AppSettingFile
        Else
            'Laden der Einstellungen (über AppData)
            If IO.File.Exists(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & AppSettingFile) Then
                AppSettingFile = My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & AppSettingFile
            End If
        End If

        'Befehlszeilenparameter prüfen
        For Each argument In My.Application.CommandLineArgs
            If argument.StartsWith("/SETTINGS|") Then
                AppSettingFile = argument.Split("|")(1)
            End If
        Next
        AppSettings = FormModule.LoadSettings(AppSettingFile)

        'Prüfen, ob aktueller User Adminrechte hat...
        Try
            If AdminCheck.IsAdmin = False Then
                If AppSettings.AllowDeleteAllPrintersStartWithoutAdminRights = False Then
                    Console.WriteLine("Die Anwendung darf nicht mit normalen Benutzerrechten gestartet werden.")
                Else
                    DeleteAllPrinters()
                    Exit Sub
                End If
            Else
                DeleteAllPrinters()
            End If
        Catch ex As Exception
            'Es ist zu einem Fehler kommen, da der Benutzer keine Adminrechte hat.

            If AppSettings.AllowDeleteAllPrintersStartWithoutAdminRights = False Then
                Console.WriteLine("Die Anwendung darf nicht mit normalen Benutzerrechten gestartet werden.")
            Else
                DeleteAllPrinters()
                Exit Sub
            End If
        End Try
    End Sub

    Sub DeleteAllPrinters()
        'Entferne ungenutzte Treiberpakete
        Console.WriteLine("1. Neustart Druckerwarteschlange...")
        PrinterManageService.RestartPrinterService()
        Console.WriteLine("2. Unbenutzte Treiberpakete löschen...")
        PrinterDriverRemoverService.DeleteUnusedDrivers(AppSettings.PrinterAdminPath)
        Console.WriteLine("3. Alle Drucker löschen...")
        PrinterDriverRemoverService.DeleteAllPrintersAndDrivers(AppSettings.PrinterAdminPath)
        Console.WriteLine("4. Neustart Druckerwarteschlange...")
        PrinterManageService.RestartPrinterService()
        Console.WriteLine("5. Unbenutzte Treiberpakete löschen...")
        PrinterDriverRemoverService.DeleteUnusedDrivers(AppSettings.PrinterAdminPath)
        Console.WriteLine("Abgeschlossen!")
    End Sub
End Module
