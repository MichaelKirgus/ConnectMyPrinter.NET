'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
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
