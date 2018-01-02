Imports System.Deployment
Imports System.Security.Principal
Imports System.Threading

Module Module1

    Public HelperFunc As New ConnectMyPrinterACLHelperLib.HelperFunctions

    Sub ShowHelp()
        Console.WriteLine("-----------------------------------------------------------")
        Console.WriteLine("Tool zum Bearbeiten der ACLs des Windows-Spoolerdienstes")
        Console.WriteLine("Copyright 2016 Michael Kirgus")
        Console.WriteLine("-----------------------------------------------------------")
        Console.WriteLine("Folgende Befehlszeilenparameter sind zugelassen:")
        Console.WriteLine("/ELEVATED: Die Anwendung wurde mit Administratorrechten gestartet. Wenn die Anwendung ohne Argumente gestartet wird, startet die Anwendung sich selbst mit Administrator-Rechten und fügt dieses Argument ein.")
        Console.WriteLine("/USER:<Benutzer>: Gibt den Benutzer an, welcher für den Start/Stop des Spoolerdienstes berechtigt werden soll.")
        Console.WriteLine("/DOMAIN:<Domain>: Gibt die Domäne an, in welcher die Anwendung ausgeführt wird.")
        Console.WriteLine("/V: Unterdrückt das Schließen des Konsolenfensters nach der Ausführung.")
        Console.WriteLine("/?: Zeigt diese Hilfe an und führt keine weiteren Aktionen aus.")
        Console.WriteLine("-----------------------------------------------------------")
    End Sub

    Sub Main()
        'Wurde Anwendung bereits mit einem Benutzer gestartet?
        Try
            Try
                If Not My.Application.CommandLineArgs(0).StartsWith("/ELEVATED") Then
                    If My.Application.CommandLineArgs(0).StartsWith("/?") Then
                        'Nur Hilfe ausgeben, und danach Anwendung beenden.
                        ShowHelp()
                        Exit Sub
                    End If

                    'Die Anwendung muss mit Administratorrechten sowie mit der Übergabe des aktuellen Benutzers erneut gestartet werden.
                    RestartApp()
                    Exit Sub
                End If
            Catch ex As Exception
                RestartApp()
                Exit Sub
            End Try

            'Benutzer ermitteln
            Console.WriteLine("Ermittle Benutzer...")
            Dim user As String = ""
            Dim domain As String = ""
            Try
                user = My.Application.CommandLineArgs(1).Split(":")(1)
            Catch ex As Exception
            End Try
            Try
                domain = My.Application.CommandLineArgs(2).Split(":")(1)
            Catch ex As Exception
            End Try

            'Da die Anwendung mit dem /ELEVATED-Argument ausgeführt wird, hat die Anwendung Adminrechte:
            Console.WriteLine("Der aktuelle Benutzer ist Administrator.")
            Console.WriteLine("Nun Berechtigung für Benutzer " & user & " einrichten...")
            IO.File.WriteAllBytes(My.Computer.FileSystem.SpecialDirectories.Temp & "\subinacl.exe", My.Resources.subinacl)
            Threading.Thread.Sleep(100)
            Dim cmdstr As String = ""
            If domain = "" Then
                cmdstr = "/service Spooler /grant=" & user & "=to"
            Else
                cmdstr = "/service Spooler /grant=" & domain & "\" & user & "=to"
            End If

            'Änderung ausführen
            Dim resultstr As String
            resultstr = ExecACLChange(My.Computer.FileSystem.SpecialDirectories.Temp & "\subinacl.exe", cmdstr)

            If resultstr.Contains("verweigert") Then
                Console.WriteLine("Der Dienst kann nicht bearbeitet werden, ACLs zurücksetzen...")
                Shell(My.Resources.ResetCMD, AppWinStyle.Hide, True)
                Threading.Thread.Sleep(100)
                Console.WriteLine("Wiederhole Änderung...")
                'Änderung nochmals ausführen
                Dim resultstr2 As String
                resultstr2 = ExecACLChange(My.Computer.FileSystem.SpecialDirectories.Temp & "\subinacl.exe", cmdstr)
                If resultstr2.Contains("verweigert") Then
                    Console.WriteLine("Die Berechtigungen konnten nicht eingerichtet werden!")
                End If
                If resultstr2.Contains("change(s)") Then
                    Console.WriteLine("Fertig, räume auf...")
                End If
            End If

            If resultstr.Contains("change(s)") Then
                Console.WriteLine("Fertig, räume auf...")
            End If

            Threading.Thread.Sleep(100)
            Try
                IO.File.Delete(My.Computer.FileSystem.SpecialDirectories.Temp & "\subinacl.exe")
            Catch ex As Exception
            End Try

            Dim wait As Boolean = False
            For Each item As String In My.Application.CommandLineArgs
                If item.Contains("/V") Then
                    wait = True
                End If
            Next

            Console.WriteLine("Setze Berechtigungen in Registry für Benutzer " & user & " in Domäne " & domain & "...")
            If HelperFunc.SetLocalMachineSpoolKeyPermissions(domain, user) Then
                Console.WriteLine("Berechtigungen erfolgreich angepasst.")
            Else
                Console.WriteLine("Berechtigungen konnten nicht angepasst werden.")
            End If

            If wait = True Then
                Console.ReadLine()
            End If
        Catch ex As Exception
            Console.WriteLine("Ein Fehler ist aufgetreten: " & Err.Description)
            Console.ReadLine()
        End Try
    End Sub

    Public Function ExecACLChange(ByVal Filename As String, ByVal Args As String) As String
        Try
            Dim rr As New Process
            rr.StartInfo.FileName = Filename
            rr.StartInfo.Arguments = Args
            rr.StartInfo.UseShellExecute = False
            rr.StartInfo.RedirectStandardOutput = True
            rr.StartInfo.RedirectStandardError = True
            rr.StartInfo.StandardOutputEncoding = Text.Encoding.Unicode
            rr.StartInfo.StandardErrorEncoding = Text.Encoding.Unicode
            rr.Start()
            rr.WaitForExit(2000)
            Dim resultstr As String
            resultstr = rr.StandardOutput.ReadToEnd
            resultstr += vbNewLine & rr.StandardError.ReadToEnd
            resultstr = resultstr.Replace(" ", "")

            Return resultstr
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub RestartApp()
        Try
            Console.WriteLine("Die Anwendung wird nicht als Administrator ausgeführt.")
            Console.WriteLine("Die Anwendung wird nun mit den benötigten Berechtigungen gestartet.")
            Dim ii As New Process
            ii.StartInfo.FileName = My.Application.Info.DirectoryPath & "\" & My.Application.Info.AssemblyName & ".exe"
            ii.StartInfo.Verb = "runas"
            Dim addcmd As String
            addcmd = ""
            Try
                addcmd = " " & My.Application.CommandLineArgs(0)
            Catch ex As Exception
            End Try

            If HelperFunc.CheckIfInDomain() Then
                ii.StartInfo.Arguments = "/ELEVATED /USER:" & Environment.UserName & " /DOMAIN:" & Environment.UserDomainName & addcmd
            Else
                ii.StartInfo.Arguments = "/ELEVATED /USER:" & Environment.UserName & addcmd
            End If

            ii.Start()
        Catch ex As Exception
        End Try
    End Sub
End Module
