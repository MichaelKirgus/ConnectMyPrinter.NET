Imports System.Printing
Imports ConnectMyPrinterEnumerationLib
Imports ConnectMyPrinterPrinterManageLib

Public Class MigrationHelper
    Public PrinterEnumerationService As New EnumeratePrinters

    Public Function GetAllPrintersFromPrintserver(ByVal Printserver As String) As List(Of PrinterQueueInfo)
        Try
            'Alle Drucker vom Printserver abrufen

            'Initialisierung des PrintServer-Objekts
            Dim myPrintServer As New PrintServer("\\" & Printserver, PrintSystemDesiredAccess.EnumerateServer)

            Dim result As New List(Of PrinterQueueInfo)

            'Druckerwarteschlangen auflisten
            Dim myPrintQueues As PrintQueueCollection = myPrintServer.GetPrintQueues()

            For Each pq As PrintQueue In myPrintQueues
                If Not pq.ShareName = "" Then
                    Dim hh As New PrinterQueueInfo
                    hh.Server = pq.HostingPrintServer.Name
                    hh.ShareName = pq.ShareName
                    result.Add(hh)
                End If
            Next pq

            Return result
        Catch ex As Exception
            Return New List(Of PrinterQueueInfo)
        End Try
    End Function

    Public Function LoadLocalPrintersLite(Optional ByVal GetLocalPrinters As Boolean = False) As List(Of PrinterQueueInfo)
        'Diese Funktion lädt alle auf dem Computer (bzw. im aktuellen Benutzerprofil) vorhandenen Drucker.

        Try
            Dim hh As List(Of PrintQueueCollection)

            If GetLocalPrinters = False Then
                hh = PrinterEnumerationService.InternalLocalPrinterCollector(False)
            Else
                hh = PrinterEnumerationService.InternalLocalPrinterCollector(True)
            End If

            Dim result As New List(Of PrinterQueueInfo)
            Dim DefaultPrinter As String = ""
            Dim printdoc As New System.Drawing.Printing.PrintDocument
            DefaultPrinter = printdoc.PrinterSettings.PrinterName
            If DefaultPrinter.StartsWith("\\") Then
                Try
                    DefaultPrinter = DefaultPrinter.Split("\")(3)
                Catch ex As Exception
                End Try
            End If

            Dim duplicatefinder As New List(Of PrinterQueueInfo)

            'Druckerwarteschlangen auflisten und Standarddrucker markieren
            For Each item As PrintQueueCollection In hh
                For Each pq As PrintQueue In item
                    Dim zz As New PrinterQueueInfo
                    Dim IsLocal As Boolean = False
                    zz.Server = pq.HostingPrintServer.Name
                    zz.Name = pq.Name
                    If pq.ShareName = "" Then
                        zz.ShareName = pq.Name
                        IsLocal = True
                    Else
                        zz.ShareName = pq.ShareName
                    End If
                    If (pq.ShareName = DefaultPrinter) Or (pq.Name = DefaultPrinter) Then
                        zz.DefaultPrinter = True
                    End If
                    If IsLocal Then
                        zz.Server = "Local"
                    End If
                    zz.State = String.Join(",", pq.QueueStatus)
                    zz.DriverName = pq.QueueDriver.Name

                    zz.Description = pq.Comment
                    zz.Location = pq.Location

                    Dim isdup As Boolean = False
                    For index = 0 To duplicatefinder.Count - 1
                        If zz.Name = duplicatefinder(index).Name Then
                            isdup = True
                        End If
                    Next

                    If isdup = True Then
                        'Der Drucker ist bereits vorhanden und wurde durch eine andere Enumeration ermittelt.
                    Else
                        duplicatefinder.Add(zz)
                        result.Add(zz)
                    End If
                Next pq
            Next

            Return result
        Catch ex As Exception
            Return New List(Of PrinterQueueInfo)
        End Try
    End Function

    Public Function ComparePrinters(ByVal PrintersOnNewPrintserver As List(Of PrinterQueueInfo), ByVal LocalPrinters As List(Of PrinterQueueInfo)) As List(Of PrinterQueueInfo)
        'Diese Funktion vergleicht die Freigaben der neuen Druckerwarteschlangen, um identische Freigabenamen zu finden und gibt Drucker zurück, welche auf dem neuen Printserver den aktuell lokalen entsprechen (neue Drucker)

        Try
            Dim result As New List(Of PrinterQueueInfo)

            For index = 0 To PrintersOnNewPrintserver.Count - 1
                For index1 = 0 To LocalPrinters.Count - 1
                    If PrintersOnNewPrintserver(index).ShareName.ToLower = LocalPrinters(index1).ShareName.ToLower Then
                        result.Add(PrintersOnNewPrintserver(index))
                    End If
                Next
            Next

            Return result
        Catch ex As Exception
            Return New List(Of PrinterQueueInfo)
        End Try
    End Function

    Public Function GetOldPrinters(ByVal PrintersOnNewPrintserver As List(Of PrinterQueueInfo), ByVal LocalPrinters As List(Of PrinterQueueInfo)) As List(Of PrinterQueueInfo)
        'Diese Funktion vergleicht die Freigaben der neuen Druckerwarteschlangen, um identische Freigabenamen zu finden und gibt Drucker zurück, welche auf dem neuen Printserver den aktuell lokalen entsprechen (aktuell verbundene Drucker)

        Try
            Dim result As New List(Of PrinterQueueInfo)

            For index = 0 To PrintersOnNewPrintserver.Count - 1
                For index1 = 0 To LocalPrinters.Count - 1
                    If PrintersOnNewPrintserver(index).ShareName.ToLower = LocalPrinters(index1).ShareName.ToLower Then
                        result.Add(LocalPrinters(index1))
                    End If
                Next
            Next

            Return result
        Catch ex As Exception
            Return New List(Of PrinterQueueInfo)
        End Try
    End Function

    Public Function ConnectPrinterCollectionFunc(ByVal ConnectPrinterCollectionObj As List(Of PrinterQueueInfo)) As Boolean
        Try
            For Each item As PrinterQueueInfo In ConnectPrinterCollectionObj
                Try
                    Dim uu As New ConnectMyPrinterPrinterManageLib.ManagePrinter

                    Shell("rundll32 printui.dll PrintUIEntry /in /n \\" & item.Server & "\" & item.ShareName, AppWinStyle.Hide, True, 120)
                Catch ex As Exception
                    Return False
                End Try
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function MigratePrinters(ByVal NewPrintserver As String, ByVal MigrateSettings As Boolean, ByVal DisconnectOldPrinter As Boolean, PurgeOldPrinter As Boolean, ByVal RestartSpooler As Boolean, ByVal DeleteTempFolder As Boolean, ByVal TempPath As String, ByVal Simulate As Boolean) As Boolean
        Try
            'Drucker von neuem Printserver abrufen
            Console.WriteLine("Get printers from server " & NewPrintserver & " ...")
            Dim newprinters As List(Of PrinterQueueInfo)
            newprinters = GetAllPrintersFromPrintserver(NewPrintserver)

            'Sind Drucker auf dem Server verfügbar?
            If Not newprinters.Count = 0 Then
                Console.WriteLine("Found " & newprinters.Count & " remote printers.")
                'Drucker des lokalen Clients abrufen

                Dim connectedprinters As List(Of PrinterQueueInfo)
                Console.WriteLine("Get printers from client...")
                connectedprinters = LoadLocalPrintersLite(False)

                'Sind Drucker auf dem Client verfügbar?

                If Not connectedprinters.Count = 0 Then
                    Console.WriteLine("Found " & connectedprinters.Count & " local printers.")
                    'Welcher Drucker ist der Standarddrucker?
                    Console.WriteLine("Get current default printer...")
                    Dim olddefprinter As PrinterQueueInfo = Nothing
                    For index = 0 To connectedprinters.Count - 1
                        If connectedprinters(index).DefaultPrinter Then
                            olddefprinter = connectedprinters(index)
                        End If
                    Next
                    Console.WriteLine("Current default printer: " & olddefprinter.ShareName & " on " & olddefprinter.Server)

                    'Nun anhand dem Freigabenamen prüfen, ob Drucker auf dem neuen Printserver mit demselben Freigabenamen existieren...
                    Dim newmatchedprinters As New List(Of PrinterQueueInfo)
                    Dim oldprinters As New List(Of PrinterQueueInfo)
                    Console.WriteLine("Get matching list (step 1)...")
                    newmatchedprinters = ComparePrinters(newprinters, connectedprinters)
                    Console.WriteLine("Get matching list (step 2)...")
                    oldprinters = GetOldPrinters(newprinters, connectedprinters)
                    Console.WriteLine("Matching list contains " & newmatchedprinters.Count & " results.")

                    'Wurden passende Drucker auf dem neuen Printserver gefunden?
                    If Not newmatchedprinters.Count = 0 Then
                        'Müssen die Einstellungen der alten Drucker gesichert werden?
                        If MigrateSettings Then
                            Console.WriteLine("Create folder " & TempPath & " ...")
                            IO.Directory.CreateDirectory(TempPath)

                            Dim SHelper As New ExportImportPrinterSettings

                            For index = 0 To oldprinters.Count - 1
                                Console.WriteLine("Saving printer settings for " & oldprinters(index).ShareName & " to " & TempPath & "\" & oldprinters(index).ShareName & ".dat")
                                If Not Simulate Then
                                    SHelper.ExportPrinterSettings(oldprinters(index), TempPath & "\" & oldprinters(index).ShareName.ToUpper & ".dat")
                                End If
                            Next
                        End If

                        Dim ManagePrinterHelper As New ManagePrinter

                        'Entfernen der alten Drucker vom Client
                        If DisconnectOldPrinter Then
                            Console.WriteLine("Disconnect old printers...")
                            For index = 0 To oldprinters.Count - 1
                                Console.WriteLine("Disconnect printer " & oldprinters(index).ShareName & " ...")
                                If Not Simulate Then
                                    ManagePrinterHelper.DeletePrinter(oldprinters(index))
                                    If PurgeOldPrinter Then
                                        'Hier werden die Einstellungen, Treiber sowie der Port gelöscht
                                        Console.WriteLine("Purge printer " & oldprinters(index).ShareName & " ...")
                                        ManagePrinterHelper.DeletePrinterDriver(oldprinters(index).DriverName)
                                        ManagePrinterHelper.DeletePrinterPort(oldprinters(index))
                                        Dim gg As New ConnectMyPrinterDriverPackagesLib.ManageDriverPackages
                                        Dim dummydrv As New ConnectMyPrinterDriverPackagesLib.DriverPackageItem
                                        dummydrv.DriverName = oldprinters(index).DriverName
                                        gg.DeleteDriver(dummydrv)
                                    End If
                                Else
                                    If PurgeOldPrinter Then
                                        Console.WriteLine("Purge printer " & oldprinters(index).ShareName & " ...")
                                    End If
                                End If
                            Next
                        End If

                        If RestartSpooler Then
                            'Neustart der Druckerwarteschlange...
                            Console.WriteLine("Restart spooler...")
                            If Not Simulate Then
                                ManagePrinterHelper.RestartPrinterService()
                            End If

                            '2 Sek. warten
                            Threading.Thread.Sleep(2000)
                        End If

                        'Verbinden der neuen Drucker
                        Console.WriteLine("Connect new printers...")
                        If Not Simulate Then
                            ConnectPrinterCollectionFunc(newmatchedprinters)
                        Else
                            For index = 0 To newmatchedprinters.Count - 1
                                Console.WriteLine("Connect printer " & newmatchedprinters(index).ShareName)
                            Next
                        End If

                        'Setzen des Standarddruckers
                        Console.WriteLine("Set default printer...")
                        Dim dummyitm As New PrinterQueueInfo
                        dummyitm.Name = olddefprinter.Name
                        dummyitm.ShareName = olddefprinter.ShareName
                        dummyitm.Server = NewPrintserver

                        Console.WriteLine("Set printer " & dummyitm.ShareName & " as new default printer...")
                        If Not Simulate Then
                            ManagePrinterHelper.SetDefaultPrinter(dummyitm)
                        End If

                        'Müssen Einstellungen zurückgesichert werden?
                        If MigrateSettings Then
                            Dim SHelper As New ExportImportPrinterSettings

                            For index = 0 To newmatchedprinters.Count - 1
                                Console.WriteLine("Apply printer settings for " & newmatchedprinters(index).ShareName & " to " & TempPath & "\" & newmatchedprinters(index).ShareName & ".dat")
                                If Not Simulate Then
                                    SHelper.ImportPrinterSettings(newmatchedprinters(index), TempPath & "\" & newmatchedprinters(index).ShareName.ToUpper & ".dat")
                                End If
                            Next
                        End If

                        If RestartSpooler Then
                            'Neustart der Druckerwarteschlange...
                            If Not Simulate Then
                                ManagePrinterHelper.RestartPrinterService()
                            End If

                            '2 Sek. warten
                            Threading.Thread.Sleep(2000)
                        End If

                        'Temp-Verzeichnis löschen
                        If DeleteTempFolder Then
                            Console.WriteLine("Delete folder " & TempPath & " ...")
                            If Not Simulate Then
                                Try
                                    My.Computer.FileSystem.DeleteDirectory(TempPath, FileIO.DeleteDirectoryOption.DeleteAllContents, FileIO.RecycleOption.DeletePermanently)
                                Catch ex As Exception
                                End Try
                            End If
                        End If

                        Console.WriteLine("Migration successful.")

                        Return True
                    End If
                End If
            End If

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
