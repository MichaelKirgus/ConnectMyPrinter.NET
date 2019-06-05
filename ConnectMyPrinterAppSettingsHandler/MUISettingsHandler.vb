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
