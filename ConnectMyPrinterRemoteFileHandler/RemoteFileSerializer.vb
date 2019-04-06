'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
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
