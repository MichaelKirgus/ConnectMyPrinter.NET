<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AppSettingsDlg
    Inherits MetroFramework.Forms.MetroForm

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MetroTabControl1 = New MetroFramework.Controls.MetroTabControl()
        Me.MetroTabPage1 = New MetroFramework.Controls.MetroTabPage()
        Me.MetroCheckBox5 = New MetroFramework.Controls.MetroCheckBox()
        Me.MetroCheckBox4 = New MetroFramework.Controls.MetroCheckBox()
        Me.MetroCheckBox3 = New MetroFramework.Controls.MetroCheckBox()
        Me.MetroCheckBox2 = New MetroFramework.Controls.MetroCheckBox()
        Me.MetroCheckBox1 = New MetroFramework.Controls.MetroCheckBox()
        Me.MetroTabPage2 = New MetroFramework.Controls.MetroTabPage()
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
        Me.SuspendLayout()
        '
        'MetroTabControl1
        '
        Me.MetroTabControl1.Controls.Add(Me.MetroTabPage1)
        Me.MetroTabControl1.Controls.Add(Me.MetroTabPage2)
        Me.MetroTabControl1.Location = New System.Drawing.Point(23, 63)
        Me.MetroTabControl1.Name = "MetroTabControl1"
        Me.MetroTabControl1.SelectedIndex = 0
        Me.MetroTabControl1.Size = New System.Drawing.Size(449, 304)
        Me.MetroTabControl1.Style = MetroFramework.MetroColorStyle.Black
        Me.MetroTabControl1.TabIndex = 0
        '
        'MetroTabPage1
        '
        Me.MetroTabPage1.Controls.Add(Me.MetroCheckBox5)
        Me.MetroTabPage1.Controls.Add(Me.MetroCheckBox4)
        Me.MetroTabPage1.Controls.Add(Me.MetroCheckBox3)
        Me.MetroTabPage1.Controls.Add(Me.MetroCheckBox2)
        Me.MetroTabPage1.Controls.Add(Me.MetroCheckBox1)
        Me.MetroTabPage1.HorizontalScrollbarBarColor = True
        Me.MetroTabPage1.Location = New System.Drawing.Point(4, 35)
        Me.MetroTabPage1.Name = "MetroTabPage1"
        Me.MetroTabPage1.Size = New System.Drawing.Size(441, 265)
        Me.MetroTabPage1.TabIndex = 0
        Me.MetroTabPage1.Text = "Anwendungsverhalten"
        Me.MetroTabPage1.VerticalScrollbarBarColor = True
        '
        'MetroCheckBox5
        '
        Me.MetroCheckBox5.AutoSize = True
        Me.MetroCheckBox5.Location = New System.Drawing.Point(11, 109)
        Me.MetroCheckBox5.Name = "MetroCheckBox5"
        Me.MetroCheckBox5.Size = New System.Drawing.Size(408, 15)
        Me.MetroCheckBox5.Style = MetroFramework.MetroColorStyle.Black
        Me.MetroCheckBox5.TabIndex = 6
        Me.MetroCheckBox5.Text = "Unbenutzte Treiberpakete bei dem Löschen eines Druckertreibers löschen"
        Me.MetroCheckBox5.UseVisualStyleBackColor = True
        '
        'MetroCheckBox4
        '
        Me.MetroCheckBox4.AutoSize = True
        Me.MetroCheckBox4.Location = New System.Drawing.Point(11, 88)
        Me.MetroCheckBox4.Name = "MetroCheckBox4"
        Me.MetroCheckBox4.Size = New System.Drawing.Size(381, 15)
        Me.MetroCheckBox4.Style = MetroFramework.MetroColorStyle.Black
        Me.MetroCheckBox4.TabIndex = 5
        Me.MetroCheckBox4.Text = "Unbenutzte Treiberpakete bei dem Entfernen eines Druckers löschen"
        Me.MetroCheckBox4.UseVisualStyleBackColor = True
        '
        'MetroCheckBox3
        '
        Me.MetroCheckBox3.AutoSize = True
        Me.MetroCheckBox3.Location = New System.Drawing.Point(11, 56)
        Me.MetroCheckBox3.Name = "MetroCheckBox3"
        Me.MetroCheckBox3.Size = New System.Drawing.Size(363, 15)
        Me.MetroCheckBox3.Style = MetroFramework.MetroColorStyle.Black
        Me.MetroCheckBox3.TabIndex = 4
        Me.MetroCheckBox3.Text = "Abfrage, wenn ein Drucker auf mehreren Printservern vorhanden"
        Me.MetroCheckBox3.UseVisualStyleBackColor = True
        '
        'MetroCheckBox2
        '
        Me.MetroCheckBox2.AutoSize = True
        Me.MetroCheckBox2.Location = New System.Drawing.Point(11, 35)
        Me.MetroCheckBox2.Name = "MetroCheckBox2"
        Me.MetroCheckBox2.Size = New System.Drawing.Size(327, 15)
        Me.MetroCheckBox2.Style = MetroFramework.MetroColorStyle.Black
        Me.MetroCheckBox2.TabIndex = 3
        Me.MetroCheckBox2.Text = "Sicherheitsabfrage vor dem Löschen eines Druckertreibers"
        Me.MetroCheckBox2.UseVisualStyleBackColor = True
        '
        'MetroCheckBox1
        '
        Me.MetroCheckBox1.AutoSize = True
        Me.MetroCheckBox1.Location = New System.Drawing.Point(11, 14)
        Me.MetroCheckBox1.Name = "MetroCheckBox1"
        Me.MetroCheckBox1.Size = New System.Drawing.Size(245, 15)
        Me.MetroCheckBox1.Style = MetroFramework.MetroColorStyle.Black
        Me.MetroCheckBox1.TabIndex = 2
        Me.MetroCheckBox1.Text = "Sicherheitsabfrage vor Druckerverbindung"
        Me.MetroCheckBox1.UseVisualStyleBackColor = True
        '
        'MetroTabPage2
        '
        Me.MetroTabPage2.Controls.Add(Me.MetroButton6)
        Me.MetroTabPage2.Controls.Add(Me.MetroButton5)
        Me.MetroTabPage2.Controls.Add(Me.MetroButton4)
        Me.MetroTabPage2.Controls.Add(Me.MetroButton3)
        Me.MetroTabPage2.Controls.Add(Me.MetroButton2)
        Me.MetroTabPage2.Controls.Add(Me.MetroButton1)
        Me.MetroTabPage2.Controls.Add(Me.MetroLabel1)
        Me.MetroTabPage2.HorizontalScrollbarBarColor = True
        Me.MetroTabPage2.Location = New System.Drawing.Point(4, 35)
        Me.MetroTabPage2.Name = "MetroTabPage2"
        Me.MetroTabPage2.Size = New System.Drawing.Size(441, 265)
        Me.MetroTabPage2.TabIndex = 1
        Me.MetroTabPage2.Text = "Erweitert"
        Me.MetroTabPage2.VerticalScrollbarBarColor = True
        '
        'MetroButton6
        '
        Me.MetroButton6.Location = New System.Drawing.Point(83, 70)
        Me.MetroButton6.Name = "MetroButton6"
        Me.MetroButton6.Size = New System.Drawing.Size(277, 23)
        Me.MetroButton6.TabIndex = 8
        Me.MetroButton6.Text = "Druckerwarteschlange starten"
        '
        'MetroButton5
        '
        Me.MetroButton5.Location = New System.Drawing.Point(83, 41)
        Me.MetroButton5.Name = "MetroButton5"
        Me.MetroButton5.Size = New System.Drawing.Size(277, 23)
        Me.MetroButton5.TabIndex = 7
        Me.MetroButton5.Text = "Druckerwarteschlange stoppen"
        '
        'MetroButton4
        '
        Me.MetroButton4.Location = New System.Drawing.Point(83, 12)
        Me.MetroButton4.Name = "MetroButton4"
        Me.MetroButton4.Size = New System.Drawing.Size(277, 23)
        Me.MetroButton4.TabIndex = 6
        Me.MetroButton4.Text = "Druckerwarteschlange neu starten"
        '
        'MetroButton3
        '
        Me.MetroButton3.Location = New System.Drawing.Point(83, 232)
        Me.MetroButton3.Name = "MetroButton3"
        Me.MetroButton3.Size = New System.Drawing.Size(277, 23)
        Me.MetroButton3.TabIndex = 5
        Me.MetroButton3.Text = "Löschen der Druckerwarteschlange erzwingen"
        '
        'MetroButton2
        '
        Me.MetroButton2.Location = New System.Drawing.Point(83, 203)
        Me.MetroButton2.Name = "MetroButton2"
        Me.MetroButton2.Size = New System.Drawing.Size(277, 23)
        Me.MetroButton2.TabIndex = 4
        Me.MetroButton2.Text = "Unbenutzte Treiberpakete löschen"
        '
        'MetroButton1
        '
        Me.MetroButton1.Location = New System.Drawing.Point(83, 174)
        Me.MetroButton1.Name = "MetroButton1"
        Me.MetroButton1.Size = New System.Drawing.Size(277, 23)
        Me.MetroButton1.TabIndex = 3
        Me.MetroButton1.Text = "Alle Drucker + Treiber + Treiberpakete löschen"
        '
        'MetroLabel1
        '
        Me.MetroLabel1.BackColor = System.Drawing.Color.Linen
        Me.MetroLabel1.CustomBackground = True
        Me.MetroLabel1.CustomForeColor = True
        Me.MetroLabel1.ForeColor = System.Drawing.Color.Red
        Me.MetroLabel1.Location = New System.Drawing.Point(0, 123)
        Me.MetroLabel1.Name = "MetroLabel1"
        Me.MetroLabel1.Size = New System.Drawing.Size(441, 142)
        Me.MetroLabel1.TabIndex = 2
        Me.MetroLabel1.Text = "Achtung: Diese Funktionen sollten nur von einem " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Administrator ausgeführt werden" &
    "."
        '
        'MetroButton7
        '
        Me.MetroButton7.Location = New System.Drawing.Point(368, 375)
        Me.MetroButton7.Name = "MetroButton7"
        Me.MetroButton7.Size = New System.Drawing.Size(104, 23)
        Me.MetroButton7.TabIndex = 1
        Me.MetroButton7.Text = "&OK"
        '
        'AppSettingsDlg
        '
        Me.AcceptButton = Me.MetroButton7
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle
        Me.ClientSize = New System.Drawing.Size(495, 419)
        Me.Controls.Add(Me.MetroButton7)
        Me.Controls.Add(Me.MetroTabControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AppSettingsDlg"
        Me.Resizable = False
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Style = MetroFramework.MetroColorStyle.Black
        Me.Text = "Einstellungen"
        Me.Theme = MetroFramework.MetroThemeStyle.Light
        Me.MetroTabControl1.ResumeLayout(False)
        Me.MetroTabPage1.ResumeLayout(False)
        Me.MetroTabPage1.PerformLayout()
        Me.MetroTabPage2.ResumeLayout(False)
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
End Class
