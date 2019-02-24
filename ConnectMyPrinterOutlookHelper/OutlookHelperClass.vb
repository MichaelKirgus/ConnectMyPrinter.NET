Imports System.Net.Mail

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

    Public Function SendMailInternal(ByVal SMTPServer As String, ByVal SMTPUsername As String, ByVal SMTPPassword As String, ByVal SMTPPort As Integer, ByVal EnableSMTPSSL As Boolean, ByVal FROMAdress As String, ByVal ToAdress As String, ByVal Subject As String, ByVal IsBodyHTML As Boolean, ByVal Message As String, ByVal AttachmentFile As String) As Boolean
        Try
            Dim Smtp_Server As New SmtpClient
            Dim e_mail As New MailMessage()
            If SMTPUsername = "" Then
                Smtp_Server.UseDefaultCredentials = True
            Else
                Smtp_Server.UseDefaultCredentials = False
                Smtp_Server.Credentials = New Net.NetworkCredential(SMTPUsername, SMTPPassword)
            End If

            Smtp_Server.Port = SMTPPort
            Smtp_Server.EnableSsl = EnableSMTPSSL
            Smtp_Server.Host = SMTPServer

            e_mail = New MailMessage()
            e_mail.From = New MailAddress(FROMAdress)

            If Not AttachmentFile = "" Then
                Dim att As New Attachment(AttachmentFile)
                e_mail.Attachments.Add(att)
            End If

            e_mail.To.Add(ToAdress)
            e_mail.Subject = Subject
            e_mail.IsBodyHtml = IsBodyHTML
            e_mail.Body = Message
            Smtp_Server.Send(e_mail)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
