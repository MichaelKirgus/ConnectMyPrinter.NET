Imports System.Diagnostics
Imports System.Windows.Forms

Friend Module OtherMethods

    Public Sub ExitApplication()
        'Perform any clean-up here
        'Then exit the application

        Dim kk As Process
        kk = Process.GetCurrentProcess
        kk.Kill()

        Application.Exit()
    End Sub

End Module
