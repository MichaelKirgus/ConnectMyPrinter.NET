<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DeletePrinterDriverDlg
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
        Me.MetroLabel1 = New MetroFramework.Controls.MetroLabel()
        Me.MetroCheckBox1 = New MetroFramework.Controls.MetroCheckBox()
        Me.MetroCheckBox2 = New MetroFramework.Controls.MetroCheckBox()
        Me.MetroCheckBox3 = New MetroFramework.Controls.MetroCheckBox()
        Me.MetroCheckBox4 = New MetroFramework.Controls.MetroCheckBox()
        Me.MetroCheckBox5 = New MetroFramework.Controls.MetroCheckBox()
        Me.MetroCheckBox6 = New MetroFramework.Controls.MetroCheckBox()
        Me.MetroCheckBox7 = New MetroFramework.Controls.MetroCheckBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.MetroButton1 = New MetroFramework.Controls.MetroButton()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MetroLabel1
        '
        Me.MetroLabel1.AutoSize = True
        Me.MetroLabel1.Location = New System.Drawing.Point(23, 60)
        Me.MetroLabel1.Name = "MetroLabel1"
        Me.MetroLabel1.Size = New System.Drawing.Size(354, 19)
        Me.MetroLabel1.TabIndex = 0
        Me.MetroLabel1.Text = "Bitte wählen Sie, welche Aktionen ausgeführt werden sollen:"
        '
        'MetroCheckBox1
        '
        Me.MetroCheckBox1.AutoSize = True
        Me.MetroCheckBox1.Checked = True
        Me.MetroCheckBox1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.MetroCheckBox1.Location = New System.Drawing.Point(50, 97)
        Me.MetroCheckBox1.Name = "MetroCheckBox1"
        Me.MetroCheckBox1.Size = New System.Drawing.Size(294, 15)
        Me.MetroCheckBox1.TabIndex = 1
        Me.MetroCheckBox1.Text = "Druckerwarteschlange mit Benutzerrechten löschen"
        Me.MetroCheckBox1.UseVisualStyleBackColor = True
        '
        'MetroCheckBox2
        '
        Me.MetroCheckBox2.AutoSize = True
        Me.MetroCheckBox2.Checked = True
        Me.MetroCheckBox2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.MetroCheckBox2.Location = New System.Drawing.Point(50, 118)
        Me.MetroCheckBox2.Name = "MetroCheckBox2"
        Me.MetroCheckBox2.Size = New System.Drawing.Size(345, 15)
        Me.MetroCheckBox2.TabIndex = 2
        Me.MetroCheckBox2.Text = "Druckerwarteschlange mit erhöhten Benutzerrechten löschen"
        Me.MetroCheckBox2.UseVisualStyleBackColor = True
        '
        'MetroCheckBox3
        '
        Me.MetroCheckBox3.AutoSize = True
        Me.MetroCheckBox3.Checked = True
        Me.MetroCheckBox3.CheckState = System.Windows.Forms.CheckState.Checked
        Me.MetroCheckBox3.Location = New System.Drawing.Point(50, 139)
        Me.MetroCheckBox3.Name = "MetroCheckBox3"
        Me.MetroCheckBox3.Size = New System.Drawing.Size(135, 15)
        Me.MetroCheckBox3.TabIndex = 3
        Me.MetroCheckBox3.Text = "Drucker zurücksetzen"
        Me.MetroCheckBox3.UseVisualStyleBackColor = True
        '
        'MetroCheckBox4
        '
        Me.MetroCheckBox4.AutoSize = True
        Me.MetroCheckBox4.Checked = True
        Me.MetroCheckBox4.CheckState = System.Windows.Forms.CheckState.Checked
        Me.MetroCheckBox4.Location = New System.Drawing.Point(50, 160)
        Me.MetroCheckBox4.Name = "MetroCheckBox4"
        Me.MetroCheckBox4.Size = New System.Drawing.Size(179, 15)
        Me.MetroCheckBox4.TabIndex = 4
        Me.MetroCheckBox4.Text = "Druckereinstellungen löschen"
        Me.MetroCheckBox4.UseVisualStyleBackColor = True
        '
        'MetroCheckBox5
        '
        Me.MetroCheckBox5.AutoSize = True
        Me.MetroCheckBox5.Checked = True
        Me.MetroCheckBox5.CheckState = System.Windows.Forms.CheckState.Checked
        Me.MetroCheckBox5.Location = New System.Drawing.Point(50, 181)
        Me.MetroCheckBox5.Name = "MetroCheckBox5"
        Me.MetroCheckBox5.Size = New System.Drawing.Size(118, 15)
        Me.MetroCheckBox5.TabIndex = 5
        Me.MetroCheckBox5.Text = "Drucker entfernen"
        Me.MetroCheckBox5.UseVisualStyleBackColor = True
        '
        'MetroCheckBox6
        '
        Me.MetroCheckBox6.AutoSize = True
        Me.MetroCheckBox6.Checked = True
        Me.MetroCheckBox6.CheckState = System.Windows.Forms.CheckState.Checked
        Me.MetroCheckBox6.Location = New System.Drawing.Point(50, 202)
        Me.MetroCheckBox6.Name = "MetroCheckBox6"
        Me.MetroCheckBox6.Size = New System.Drawing.Size(303, 15)
        Me.MetroCheckBox6.TabIndex = 6
        Me.MetroCheckBox6.Text = "Druckertreiber mit erhöhten Benutzerrechten löschen"
        Me.MetroCheckBox6.UseVisualStyleBackColor = True
        '
        'MetroCheckBox7
        '
        Me.MetroCheckBox7.AutoSize = True
        Me.MetroCheckBox7.Checked = True
        Me.MetroCheckBox7.CheckState = System.Windows.Forms.CheckState.Checked
        Me.MetroCheckBox7.Location = New System.Drawing.Point(50, 223)
        Me.MetroCheckBox7.Name = "MetroCheckBox7"
        Me.MetroCheckBox7.Size = New System.Drawing.Size(320, 15)
        Me.MetroCheckBox7.TabIndex = 7
        Me.MetroCheckBox7.Text = "Treiber manuell mit erhöhten Benutzerrechten entfernen"
        Me.MetroCheckBox7.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.uac_shield1
        Me.PictureBox1.Location = New System.Drawing.Point(28, 117)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox1.TabIndex = 8
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.uac_shield1
        Me.PictureBox2.Location = New System.Drawing.Point(28, 201)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox2.TabIndex = 9
        Me.PictureBox2.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.uac_shield1
        Me.PictureBox3.Location = New System.Drawing.Point(28, 222)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox3.TabIndex = 10
        Me.PictureBox3.TabStop = False
        '
        'MetroButton1
        '
        Me.MetroButton1.Location = New System.Drawing.Point(278, 271)
        Me.MetroButton1.Name = "MetroButton1"
        Me.MetroButton1.Size = New System.Drawing.Size(145, 23)
        Me.MetroButton1.TabIndex = 11
        Me.MetroButton1.Text = "Druckertreiber löschen"
        '
        'DeletePrinterDriverDlg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(446, 317)
        Me.Controls.Add(Me.MetroButton1)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.MetroCheckBox7)
        Me.Controls.Add(Me.MetroCheckBox6)
        Me.Controls.Add(Me.MetroCheckBox5)
        Me.Controls.Add(Me.MetroCheckBox4)
        Me.Controls.Add(Me.MetroCheckBox3)
        Me.Controls.Add(Me.MetroCheckBox2)
        Me.Controls.Add(Me.MetroCheckBox1)
        Me.Controls.Add(Me.MetroLabel1)
        Me.Name = "DeletePrinterDriverDlg"
        Me.Resizable = False
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Style = MetroFramework.MetroColorStyle.Black
        Me.Text = "Druckertreiber löschen"
        Me.Theme = MetroFramework.MetroThemeStyle.Light
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MetroLabel1 As Controls.MetroLabel
    Friend WithEvents MetroCheckBox1 As Controls.MetroCheckBox
    Friend WithEvents MetroCheckBox2 As Controls.MetroCheckBox
    Friend WithEvents MetroCheckBox3 As Controls.MetroCheckBox
    Friend WithEvents MetroCheckBox4 As Controls.MetroCheckBox
    Friend WithEvents MetroCheckBox5 As Controls.MetroCheckBox
    Friend WithEvents MetroCheckBox6 As Controls.MetroCheckBox
    Friend WithEvents MetroCheckBox7 As Controls.MetroCheckBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents MetroButton1 As Controls.MetroButton
End Class
