Imports ConnectMyPrinterEnumerationLib

Public Class RemoteFileCreator
    Public Function CreateAddPrinterRemoteFile(ByVal Filename As String, ByVal PrinterObj As PrinterQueueInfo) As Boolean
        Try
            Dim aa As New RemoteFileClass
            Dim jj As New RemoteFilePrinterConnectItem
            jj.PrinterName = PrinterObj.ShareName
            Dim onlyserver As String
            If PrinterObj.Server.Contains("\") Then
                onlyserver = PrinterObj.Server.Split("\")(2)
            Else
                onlyserver = PrinterObj.Server
            End If
            jj.Printserver = onlyserver
            aa.ConnectPrinters.Add(jj)
            Dim yy As New RemoteFileSerializer
            yy.SaveRemoteFile(aa, Filename)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CreateMultiplePrinterRemoteFile(ByVal Filename As String, ByVal PrinterObj As List(Of PrinterQueueInfo)) As Boolean
        Try
            Dim aa As New RemoteFileClass
            For Each item As PrinterQueueInfo In PrinterObj
                If Not item.ShareName = "" Then
                    'Drucker ist nicht lokal, er kann remote verbunden werden
                    Dim jj As New RemoteFilePrinterConnectItem
                    jj.PrinterName = item.ShareName
                    Dim onlyserver As String
                    If item.Server.Contains("\") Then
                        onlyserver = item.Server.Split("\")(2)
                    Else
                        onlyserver = item.Server
                    End If
                    jj.Printserver = onlyserver
                    aa.ConnectPrinters.Add(jj)
                End If
            Next

            Dim yy As New RemoteFileSerializer
            yy.SaveRemoteFile(aa, Filename)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CreateRemoveAndAddPrinterRemoteFile(ByVal Filename As String, ByVal PrinterObj As PrinterQueueInfo) As Boolean
        Try
            Dim aa As New RemoteFileClass
            Dim jj As New RemoteFilePrinterConnectItem
            Dim ii As New RemoteFilePrinterDisconnectItem
            jj.PrinterName = PrinterObj.ShareName
            Dim onlyserver As String
            If PrinterObj.Server.Contains("\") Then
                onlyserver = PrinterObj.Server.Split("\")(2)
            Else
                onlyserver = PrinterObj.Server
            End If
            jj.Printserver = onlyserver
            ii.PrinterName = PrinterObj.ShareName
            aa.ConnectPrinters.Add(jj)
            aa.DisconnectPrinters.Add(ii)
            Dim yy As New RemoteFileSerializer
            yy.SaveRemoteFile(aa, Filename)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
