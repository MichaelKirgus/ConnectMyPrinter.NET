Module Module1

    Sub Main()
        Console.WriteLine("Erstelle Wiederherstellungspunkt, bitte warten...")
        Dim aa As New ConnectMyPrinterSystemRestorePointLib.CreateRestorePointClass
        aa.EnsureCreationPoint()
        aa.CreatePoint("ConnectMyPrinter.NET (manuell durch Benutzer)", True)
        Console.WriteLine("Abgeschlossen.")
    End Sub
End Module
