﻿'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports MetroFramework

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AppSettingsDlg
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AppSettingsDlg))
        Me.MetroTabControl1 = New MetroFramework.Controls.MetroTabControl()
        Me.MetroTabPage1 = New MetroFramework.Controls.MetroTabPage()
        Me.MetroCheckBox5 = New MetroFramework.Controls.MetroCheckBox()
        Me.MetroCheckBox4 = New MetroFramework.Controls.MetroCheckBox()
        Me.MetroCheckBox3 = New MetroFramework.Controls.MetroCheckBox()
        Me.MetroCheckBox2 = New MetroFramework.Controls.MetroCheckBox()
        Me.MetroCheckBox1 = New MetroFramework.Controls.MetroCheckBox()
        Me.MetroTabPage2 = New MetroFramework.Controls.MetroTabPage()
        Me.status_error = New System.Windows.Forms.PictureBox()
        Me.status_ok = New System.Windows.Forms.PictureBox()
        Me.MetroButton6 = New MetroFramework.Controls.MetroButton()
        Me.MetroButton5 = New MetroFramework.Controls.MetroButton()
        Me.MetroButton4 = New MetroFramework.Controls.MetroButton()
        Me.MetroButton3 = New MetroFramework.Controls.MetroButton()
        Me.MetroButton2 = New MetroFramework.Controls.MetroButton()
        Me.MetroButton1 = New MetroFramework.Controls.MetroButton()
        Me.MetroLabel1 = New MetroFramework.Controls.MetroLabel()
        Me.MetroButton7 = New MetroFramework.Controls.MetroButton()
        Me.MetroTabControl1.SuspendLayout()
        Me.MetroTabPage1.SuspendLayout()
        Me.MetroTabPage2.SuspendLayout()
        CType(Me.status_error, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.status_ok, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MetroTabControl1
        '
        Me.MetroTabControl1.Controls.Add(Me.MetroTabPage1)
        Me.MetroTabControl1.Controls.Add(Me.MetroTabPage2)
        resources.ApplyResources(Me.MetroTabControl1, "MetroTabControl1")
        Me.MetroTabControl1.Name = "MetroTabControl1"
        Me.MetroTabControl1.SelectedIndex = 1
        Me.MetroTabControl1.Style = MetroFramework.MetroColorStyle.Black
        Me.MetroTabControl1.UseSelectable = True
        '
        'MetroTabPage1
        '
        Me.MetroTabPage1.Controls.Add(Me.MetroCheckBox5)
        Me.MetroTabPage1.Controls.Add(Me.MetroCheckBox4)
        Me.MetroTabPage1.Controls.Add(Me.MetroCheckBox3)
        Me.MetroTabPage1.Controls.Add(Me.MetroCheckBox2)
        Me.MetroTabPage1.Controls.Add(Me.MetroCheckBox1)
        Me.MetroTabPage1.HorizontalScrollbarBarColor = True
        Me.MetroTabPage1.HorizontalScrollbarHighlightOnWheel = False
        Me.MetroTabPage1.HorizontalScrollbarSize = 10
        resources.ApplyResources(Me.MetroTabPage1, "MetroTabPage1")
        Me.MetroTabPage1.Name = "MetroTabPage1"
        Me.MetroTabPage1.VerticalScrollbarBarColor = True
        Me.MetroTabPage1.VerticalScrollbarHighlightOnWheel = False
        Me.MetroTabPage1.VerticalScrollbarSize = 10
        '
        'MetroCheckBox5
        '
        resources.ApplyResources(Me.MetroCheckBox5, "MetroCheckBox5")
        Me.MetroCheckBox5.Name = "MetroCheckBox5"
        Me.MetroCheckBox5.Style = MetroFramework.MetroColorStyle.Black
        Me.MetroCheckBox5.UseSelectable = True
        '
        'MetroCheckBox4
        '
        resources.ApplyResources(Me.MetroCheckBox4, "MetroCheckBox4")
        Me.MetroCheckBox4.Name = "MetroCheckBox4"
        Me.MetroCheckBox4.Style = MetroFramework.MetroColorStyle.Black
        Me.MetroCheckBox4.UseSelectable = True
        '
        'MetroCheckBox3
        '
        resources.ApplyResources(Me.MetroCheckBox3, "MetroCheckBox3")
        Me.MetroCheckBox3.Name = "MetroCheckBox3"
        Me.MetroCheckBox3.Style = MetroFramework.MetroColorStyle.Black
        Me.MetroCheckBox3.UseSelectable = True
        '
        'MetroCheckBox2
        '
        resources.ApplyResources(Me.MetroCheckBox2, "MetroCheckBox2")
        Me.MetroCheckBox2.Name = "MetroCheckBox2"
        Me.MetroCheckBox2.Style = MetroFramework.MetroColorStyle.Black
        Me.MetroCheckBox2.UseSelectable = True
        '
        'MetroCheckBox1
        '
        resources.ApplyResources(Me.MetroCheckBox1, "MetroCheckBox1")
        Me.MetroCheckBox1.Name = "MetroCheckBox1"
        Me.MetroCheckBox1.Style = MetroFramework.MetroColorStyle.Black
        Me.MetroCheckBox1.UseSelectable = True
        '
        'MetroTabPage2
        '
        Me.MetroTabPage2.Controls.Add(Me.status_error)
        Me.MetroTabPage2.Controls.Add(Me.status_ok)
        Me.MetroTabPage2.Controls.Add(Me.MetroButton6)
        Me.MetroTabPage2.Controls.Add(Me.MetroButton5)
        Me.MetroTabPage2.Controls.Add(Me.MetroButton4)
        Me.MetroTabPage2.Controls.Add(Me.MetroButton3)
        Me.MetroTabPage2.Controls.Add(Me.MetroButton2)
        Me.MetroTabPage2.Controls.Add(Me.MetroButton1)
        Me.MetroTabPage2.Controls.Add(Me.MetroLabel1)
        Me.MetroTabPage2.HorizontalScrollbarBarColor = True
        Me.MetroTabPage2.HorizontalScrollbarHighlightOnWheel = False
        Me.MetroTabPage2.HorizontalScrollbarSize = 10
        resources.ApplyResources(Me.MetroTabPage2, "MetroTabPage2")
        Me.MetroTabPage2.Name = "MetroTabPage2"
        Me.MetroTabPage2.VerticalScrollbarBarColor = True
        Me.MetroTabPage2.VerticalScrollbarHighlightOnWheel = False
        Me.MetroTabPage2.VerticalScrollbarSize = 10
        '
        'status_error
        '
        Me.status_error.BackColor = System.Drawing.Color.Transparent
        Me.status_error.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.DeletePrinter
        resources.ApplyResources(Me.status_error, "status_error")
        Me.status_error.Name = "status_error"
        Me.status_error.TabStop = False
        '
        'status_ok
        '
        Me.status_ok.BackColor = System.Drawing.Color.Transparent
        Me.status_ok.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.dialog_ok
        resources.ApplyResources(Me.status_ok, "status_ok")
        Me.status_ok.Name = "status_ok"
        Me.status_ok.TabStop = False
        '
        'MetroButton6
        '
        resources.ApplyResources(Me.MetroButton6, "MetroButton6")
        Me.MetroButton6.Name = "MetroButton6"
        Me.MetroButton6.UseSelectable = True
        '
        'MetroButton5
        '
        resources.ApplyResources(Me.MetroButton5, "MetroButton5")
        Me.MetroButton5.Name = "MetroButton5"
        Me.MetroButton5.UseSelectable = True
        '
        'MetroButton4
        '
        resources.ApplyResources(Me.MetroButton4, "MetroButton4")
        Me.MetroButton4.Name = "MetroButton4"
        Me.MetroButton4.UseSelectable = True
        '
        'MetroButton3
        '
        resources.ApplyResources(Me.MetroButton3, "MetroButton3")
        Me.MetroButton3.Name = "MetroButton3"
        Me.MetroButton3.UseSelectable = True
        '
        'MetroButton2
        '
        resources.ApplyResources(Me.MetroButton2, "MetroButton2")
        Me.MetroButton2.Name = "MetroButton2"
        Me.MetroButton2.UseSelectable = True
        '
        'MetroButton1
        '
        resources.ApplyResources(Me.MetroButton1, "MetroButton1")
        Me.MetroButton1.Name = "MetroButton1"
        Me.MetroButton1.UseSelectable = True
        '
        'MetroLabel1
        '
        Me.MetroLabel1.BackColor = System.Drawing.Color.Linen
        Me.MetroLabel1.ForeColor = System.Drawing.Color.Red
        resources.ApplyResources(Me.MetroLabel1, "MetroLabel1")
        Me.MetroLabel1.Name = "MetroLabel1"
        Me.MetroLabel1.UseCustomBackColor = True
        '
        'MetroButton7
        '
        resources.ApplyResources(Me.MetroButton7, "MetroButton7")
        Me.MetroButton7.Name = "MetroButton7"
        Me.MetroButton7.UseSelectable = True
        '
        'AppSettingsDlg
        '
        Me.AcceptButton = Me.MetroButton7
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle
        Me.Controls.Add(Me.MetroButton7)
        Me.Controls.Add(Me.MetroTabControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AppSettingsDlg"
        Me.Resizable = False
        Me.ShadowType = MetroFramework.Forms.MetroFormShadowType.None
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Style = MetroFramework.MetroColorStyle.Black
        Me.MetroTabControl1.ResumeLayout(False)
        Me.MetroTabPage1.ResumeLayout(False)
        Me.MetroTabPage1.PerformLayout()
        Me.MetroTabPage2.ResumeLayout(False)
        CType(Me.status_error, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.status_ok, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents MetroTabControl1 As Controls.MetroTabControl
    Friend WithEvents MetroTabPage1 As Controls.MetroTabPage
    Friend WithEvents MetroTabPage2 As Controls.MetroTabPage
    Friend WithEvents MetroCheckBox1 As Controls.MetroCheckBox
    Friend WithEvents MetroCheckBox2 As Controls.MetroCheckBox
    Friend WithEvents MetroCheckBox3 As Controls.MetroCheckBox
    Friend WithEvents MetroCheckBox4 As Controls.MetroCheckBox
    Friend WithEvents MetroCheckBox5 As Controls.MetroCheckBox
    Friend WithEvents MetroLabel1 As Controls.MetroLabel
    Friend WithEvents MetroButton1 As Controls.MetroButton
    Friend WithEvents MetroButton2 As Controls.MetroButton
    Friend WithEvents MetroButton3 As Controls.MetroButton
    Friend WithEvents MetroButton6 As Controls.MetroButton
    Friend WithEvents MetroButton5 As Controls.MetroButton
    Friend WithEvents MetroButton4 As Controls.MetroButton
    Friend WithEvents MetroButton7 As Controls.MetroButton
    Friend WithEvents status_error As PictureBox
    Friend WithEvents status_ok As PictureBox
End Class
