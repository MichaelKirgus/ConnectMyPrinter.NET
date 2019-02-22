<Serializable> Public Class RemoteFileClass
    ''' <summary>
    ''' Definiert eine benutzerdefinierte Einstellungsdatei für die Anwendungseinstellungen. Standardmäßig wird die AppSettings.xml im Anwendungsverzeichnis geladen.
    ''' </summary>
    Public Property CustomAppSettingsFile As String = ""
    ''' <summary>
    ''' Aktionen, welche vor dem Verbinden und Trennen von Druckern ausgeführt werden.
    ''' </summary>
    Public Property Preactions As New List(Of RemoteFileActions)
    ''' <summary>
    ''' Drucker, welche auf dem Client entfernt werden.
    ''' </summary>
    Public Property DisconnectPrinters As New List(Of RemoteFilePrinterDisconnectItem)
    ''' <summary>
    ''' Aktionen, welche nach dem Verbinden, aber vor dem Trennen von Druckern ausgeführt werden.
    ''' </summary>
    Public Property IntermediateActions As New List(Of RemoteFileActions)
    ''' <summary>
    ''' Drucker, welche auf dem Client verbunden werden sollen oder bereits auf dem Client verbunden sind.
    ''' </summary>
    Public Property ConnectPrinters As New List(Of RemoteFilePrinterConnectItem)
    ''' <summary>
    ''' Aktionen, welche nach dem Verbinden und Trennen von Druckern ausgeführt werden.
    ''' </summary>
    Public Property Postactions As New List(Of RemoteFileActions)
End Class
