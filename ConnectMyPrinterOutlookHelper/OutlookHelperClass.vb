Public Class OutlookHelperClass
    Public Sub SendOutlookMail(ByVal Betreff As String,
                ByVal Bodytext As String,
                Optional ByVal Anhang As String = "Datei als Anhang",
                Optional ByVal Empfaenger As String = "example@domain.ltd",
                Optional ByVal CCEmpfaenger As String = "")

        Try
            If Process.GetProcessesByName("outlook").Count > 0 Then
            Else
                '--- Outlook starten ---
                Dim ww As New Process
                ww.StartInfo.FileName = "outlook"
                ww.Start()
                ww.WaitForInputIdle()
            End If

            '--- Objekte des Outlook-Objektmodels initialisieren ---
            Dim objOutlook As Object
            Dim objOutlookMsg As Object
            Const cMailItem = 0
            Const cTo = 1

            '--- erstellen der notwendigen Objekte ---
            objOutlook = CreateObject("Outlook.Application")
            objOutlookMsg = objOutlook.CreateItem(cMailItem)

            '--- Zusammenbasteln der Mail ---
            With objOutlookMsg
                Dim objOutlookRecip As Object = .Recipients.Add(Empfaenger)
                objOutlookRecip.type = cTo
                .Subject = Betreff
                .Body = Bodytext
                .CC = CCEmpfaenger
                If Anhang <> "Datei als Anhang" Then .Attachments.add(Anhang)
                .Display()
            End With

            '--- Objekte zerstören ---
            objOutlookMsg = Nothing
            objOutlook = Nothing

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try

    End Sub
End Class
