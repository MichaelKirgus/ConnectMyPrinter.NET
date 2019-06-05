'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports System.Globalization

Public Class MUISettingsHandler
    Private MCultureInf As CultureInfo = CultureInfo.CurrentUICulture
    Private AppSettingFile As String = "AppSettings.xml"
    Private AppSettingDEFile As String = "AppSettings_de-DE.xml"
    Private AppSettingENFile As String = "AppSettings_en-US.xml"

    Public Function GetAppSettingsFilePath(ByVal MUI As Boolean) As String
        Try
            'Laden der Einstellungen für alle Benutzer
            If IO.File.Exists(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & AppSettingFile) Then
                Return My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & AppSettingFile
            Else
                'Laden der Einstellungen (über AppData)
                If IO.File.Exists(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & AppSettingFile) Then
                    Return My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & AppSettingFile
                Else
                    'Es liegen keine Einstellungen in den App-Data-Ordnern.
                    If MUI Then
                        'Prüfen, on lokalisierte Anwendungseinstellungen im Anwendungsordner liegen:
                        'DE
                        If MCultureInf.IetfLanguageTag.Contains("de") Then
                            If IO.File.Exists(AppSettingDEFile) Then
                                Return AppSettingDEFile
                            End If
                        End If
                        'EN
                        If MCultureInf.IetfLanguageTag.Contains("en") Then
                            If IO.File.Exists(AppSettingENFile) Then
                                Return AppSettingENFile
                            End If
                        End If
                    End If

                    Return AppSettingFile
                End If
            End If
        Catch ex As Exception
            Return AppSettingFile
        End Try
    End Function
End Class
