﻿'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports MetroFramework

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProcessingDlg
    Inherits MetroFramework.Forms.MetroForm

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.MetroProgressSpinner1 = New MetroFramework.Controls.MetroProgressSpinner()
        Me.MetroLabel2 = New MetroFramework.Controls.MetroLabel()
        Me.SuspendLayout()
        '
        'MetroProgressSpinner1
        '
        Me.MetroProgressSpinner1.CustomBackground = True
        Me.MetroProgressSpinner1.Location = New System.Drawing.Point(77, 70)
        Me.MetroProgressSpinner1.Maximum = 100
        Me.MetroProgressSpinner1.Name = "MetroProgressSpinner1"
        Me.MetroProgressSpinner1.Size = New System.Drawing.Size(48, 48)
        Me.MetroProgressSpinner1.Style = MetroFramework.MetroColorStyle.Black
        Me.MetroProgressSpinner1.TabIndex = 0
        Me.MetroProgressSpinner1.UseSelectable = True
        '
        'MetroLabel2
        '
        Me.MetroLabel2.AutoSize = True
        Me.MetroLabel2.FontSize = MetroFramework.MetroLabelSize.Small
        Me.MetroLabel2.Location = New System.Drawing.Point(29, 127)
        Me.MetroLabel2.Name = "MetroLabel2"
        Me.MetroLabel2.Size = New System.Drawing.Size(151, 15)
        Me.MetroLabel2.TabIndex = 6
        Me.MetroLabel2.Text = "Aktionen werden ausgeführt."
        '
        'ProcessingDlg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle
        Me.ClientSize = New System.Drawing.Size(203, 151)
        Me.Controls.Add(Me.MetroLabel2)
        Me.Controls.Add(Me.MetroProgressSpinner1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ProcessingDlg"
        Me.Resizable = False
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Style = MetroFramework.MetroColorStyle.Black
        Me.Text = "Bitte warten..."
        Me.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MetroProgressSpinner1 As Controls.MetroProgressSpinner
    Friend WithEvents MetroLabel2 As Controls.MetroLabel
End Class
