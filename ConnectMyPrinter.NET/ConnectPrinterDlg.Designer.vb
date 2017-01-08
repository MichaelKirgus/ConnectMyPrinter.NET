<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ConnectPrinterDlg
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
        Me.MetroLabel2 = New MetroFramework.Controls.MetroLabel()
        Me.MetroButton1 = New MetroFramework.Controls.MetroButton()
        Me.MetroButton2 = New MetroFramework.Controls.MetroButton()
        Me.SuspendLayout()
        '
        'MetroLabel2
        '
        Me.MetroLabel2.Location = New System.Drawing.Point(23, 60)
        Me.MetroLabel2.Name = "MetroLabel2"
        Me.MetroLabel2.Size = New System.Drawing.Size(381, 64)
        Me.MetroLabel2.TabIndex = 1
        Me.MetroLabel2.Text = "<Message>"
        '
        'MetroButton1
        '
        Me.MetroButton1.Location = New System.Drawing.Point(218, 127)
        Me.MetroButton1.Name = "MetroButton1"
        Me.MetroButton1.Size = New System.Drawing.Size(90, 23)
        Me.MetroButton1.TabIndex = 2
        Me.MetroButton1.Text = "Ja"
        '
        'MetroButton2
        '
        Me.MetroButton2.Location = New System.Drawing.Point(314, 127)
        Me.MetroButton2.Name = "MetroButton2"
        Me.MetroButton2.Size = New System.Drawing.Size(90, 23)
        Me.MetroButton2.TabIndex = 3
        Me.MetroButton2.Text = "Nein"
        '
        'ConnectPrinterDlg
        '
        Me.AcceptButton = Me.MetroButton1
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle
        Me.ClientSize = New System.Drawing.Size(427, 168)
        Me.Controls.Add(Me.MetroButton2)
        Me.Controls.Add(Me.MetroButton1)
        Me.Controls.Add(Me.MetroLabel2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ConnectPrinterDlg"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Style = MetroFramework.MetroColorStyle.Black
        Me.Text = "Drucker verbinden?"
        Me.Theme = MetroFramework.MetroThemeStyle.Light
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents MetroLabel2 As Controls.MetroLabel
    Friend WithEvents MetroButton1 As Controls.MetroButton
    Friend WithEvents MetroButton2 As Controls.MetroButton
End Class
