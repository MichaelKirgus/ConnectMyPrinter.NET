Imports System.IO
Imports System.Xml.Serialization

Public Class PrinterEnumerationSerializer
    Public Function LoadQueueFile(ByVal Filename As String) As PrinterQueueInfo
        'Diese Funktion lädt eine serialisierte Infodatei über einen Drucker

        Try
            Dim objStreamReader As New StreamReader(Filename)
            Dim p2 As New PrinterQueueInfo
            Dim x As New XmlSerializer(p2.GetType)
            p2 = x.Deserialize(objStreamReader)
            objStreamReader.Close()

            Return p2
        Catch ex As Exception
            Return New PrinterQueueInfo
        End Try
    End Function

    Public Function SaveQueueFile(ByVal QueueFile As PrinterQueueInfo, ByVal Filename As String) As Boolean
        'Diese Funktion speichert eine serialisierte Infodatei über einen Drucker

        Try
            Dim XML As New XmlSerializer(QueueFile.GetType)
            Dim FS As New FileStream(Filename, FileMode.Create)
            XML.Serialize(FS, QueueFile)
            FS.Close()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
