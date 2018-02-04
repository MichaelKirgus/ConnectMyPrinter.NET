<Serializable> Public Class RemoteFileClass
    Public Property CustomAppSettingsFile As String = ""
    Public Property Preactions As New List(Of RemoteFileActions)
    Public Property DisconnectPrinters As New List(Of RemoteFilePrinterDisconnectItem)
    Public Property IntermediateActions As New List(Of RemoteFileActions)
    Public Property ConnectPrinters As New List(Of RemoteFilePrinterConnectItem)
    Public Property Postactions As New List(Of RemoteFileActions)
End Class
