Module Module1
    Private CliWrapper As New ConnectMyPrinterCLIHelper.CLIWrapper

    Sub Main(args As String())
        If CliWrapper.LunchCli(args, True) Then
            Environment.ExitCode = 0
        Else
            Environment.ExitCode = 1
        End If
    End Sub
End Module
