'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports System.Globalization
Imports System.Resources
Imports System.Windows.Forms

Public Class LanguageApplyHelper
    Public Function ApplyCultureToGUI(ByVal ResourceName As String, ByVal FormInstance As Type, ByVal Culture As CultureInfo, ByVal ExControl As Control) As Boolean
        Try
            '
            'FormInstance 
            Dim rm As ResourceManager = New ResourceManager(ResourceName, FormInstance.Assembly)

            Dim translationstr1 As String
            translationstr1 = rm.GetString(ExControl.Name, Culture)

            If Not translationstr1 = "" Then
                ExControl.Text = translationstr1
            End If

            If Not ExControl.Controls.Count = 0 Then
                For ind = 0 To ExControl.Controls.Count - 1
                    Dim translationstr2 As String
                    translationstr2 = rm.GetString(ExControl.Controls(ind).Name, Culture)

                    If Not translationstr2 = "" Then
                        ExControl.Controls(ind).Text = translationstr2
                    End If

                    If Not ExControl.Controls(ind).Controls.Count = 0 Then
                        ApplyCultureToGUI(ResourceName, FormInstance, Culture, ExControl.Controls(ind))
                    End If
                Next
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetCultureString(ByVal ResourceName As String, ByVal FormInstance As Type, ByVal Culture As CultureInfo, ByVal StringName As String, Optional StringIfEmpty As String = "") As String
        Try
            Dim rm As ResourceManager = New ResourceManager(ResourceName, FormInstance.Assembly)

            Dim translationstr1 As String
            translationstr1 = rm.GetString(StringName, Culture)

            If translationstr1 = "" Then
                Return StringIfEmpty
            Else
                Return translationstr1
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Class
