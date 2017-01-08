Imports System.IO
Imports System.Xml.Serialization

Public Class SavedPrinterEnumerationSerializer
    Public Function LoadMyPrinterCollectionFile(ByVal Filename As String) As List(Of PrinterQueueInfo)
        'Diese Funktion lädt eine serialisierte Infodatei über einen Drucker

        Try
            Dim objStreamReader As New StreamReader(Filename)
            Dim p2 As New List(Of PrinterQueueInfo)
            Dim x As New XmlSerializer(p2.GetType)
            p2 = x.Deserialize(objStreamReader)
            objStreamReader.Close()

            Return p2
        Catch ex As Exception
            Return New List(Of PrinterQueueInfo)
        End Try
    End Function

    Public Function SaveMyPrinterCollectionFile(ByVal CollectionObj As List(Of PrinterQueueInfo), ByVal Filename As String) As Boolean
        'Diese Funktion speichert eine serialisierte Infodatei über einen Drucker

        Try
            Dim XML As New XmlSerializer(CollectionObj.GetType)
            Dim FS As New FileStream(Filename, FileMode.Create)
            XML.Serialize(FS, CollectionObj)
            FS.Close()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
