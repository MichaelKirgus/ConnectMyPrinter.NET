'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports System.Windows.Forms

Friend Module OtherMethods

    Private PF As PopupForm

    Public Sub ExitApplication()
        'Perform any clean-up here
        'Then exit the application
        Application.Exit()
    End Sub

    Public Sub ShowDialog()
        If PF IsNot Nothing AndAlso Not PF.IsDisposed Then Exit Sub

        Dim CloseApp As Boolean = False

        PF = New PopupForm
        PF.ShowDialog()
        CloseApp = (PF.DialogResult = DialogResult.Abort)
        PF = Nothing

        If CloseApp Then ExitApplication()
    End Sub

End Module
