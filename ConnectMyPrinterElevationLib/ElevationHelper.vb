'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Public Class ElevationHelper
    Public Function RunAppAsAdmin(ByVal AppPath As String, Optional ByVal WorkingDir As String = "", Optional ByVal Arguments As String = "") As Boolean
        Try
            Dim jj As New Process
            jj.StartInfo.FileName = AppPath
            If Not WorkingDir = "" Then
                jj.StartInfo.WorkingDirectory = WorkingDir
            End If
            If Not Arguments = "" Then
                jj.StartInfo.Arguments = Arguments
            End If

            jj.StartInfo.Verb = "runas"
            jj.Start()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
