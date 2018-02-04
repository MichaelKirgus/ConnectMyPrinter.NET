Imports System.IO
Imports System.Xml.Serialization

Public Class RemoteFileSerializer
    Public Function LoadRemoteFile(ByVal Filename As String) As RemoteFileClass
        'Diese Funktion lädt die Profildatei

        Try
            Dim objStreamReader As New StreamReader(Filename)
            Dim p2 As New RemoteFileClass
            Dim x As New XmlSerializer(p2.GetType)
            p2 = x.Deserialize(objStreamReader)
            objStreamReader.Close()

            Return p2
        Catch ex As Exception
            Return New RemoteFileClass
        End Try
    End Function

    Public Function SaveRemoteFile(ByVal RemoteFileClassx As RemoteFileClass, ByVal Filename As String) As Boolean
        'Diese Funktion speichert die Profildatei

        Try
            Dim XML As New XmlSerializer(RemoteFileClassx.GetType)
            Dim FS As New FileStream(Filename, FileMode.Create)
            XML.Serialize(FS, RemoteFileClassx)
            FS.Close()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
