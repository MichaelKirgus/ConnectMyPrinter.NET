Imports ConnectMyPrinter.NET
Imports ConnectMyPrinterAppSettingsHandler
Imports ConnectMyPrinterPrinterManageLib

Module Module1

    Sub Main()
        Console.WriteLine("Lade Umgebung...")

        Dim ElevationHelper As New ElevationHelperClass
        Dim PrinterDriverRemoverService As New PrinterDriverRemover
        Dim AppSettings As New AppSettingsClass
        Dim PrinterManageService As New ManagePrinter
        Dim AppSettingFile As String = "AppSettings.xml"
        Dim ActionFileDir As String = "PrinterActionsElv"
        Dim FormModule As Form1 = New Form1

        Console.WriteLine("Lade Einstellungsdatei...")
        AppSettings = FormModule.LoadSettings(AppSettingFile)

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
