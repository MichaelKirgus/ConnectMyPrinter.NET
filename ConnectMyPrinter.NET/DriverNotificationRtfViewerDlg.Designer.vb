<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DriverNotificationRtfViewerDlg
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
        Me.MetroButton2 = New MetroFramework.Controls.MetroButton()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'MetroButton2
        '
        Me.MetroButton2.Location = New System.Drawing.Point(350, 329)
        Me.MetroButton2.Name = "MetroButton2"
        Me.MetroButton2.Size = New System.Drawing.Size(90, 23)
        Me.MetroButton2.TabIndex = 3
        Me.MetroButton2.Text = "&OK"
        '
        'RichTextBox1
        '
        Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox1.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.RichTextBox1.EnableAutoDragDrop = True
        Me.RichTextBox1.Location = New System.Drawing.Point(23, 63)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.ReadOnly = True
        Me.RichTextBox1.Size = New System.Drawing.Size(418, 260)
        Me.RichTextBox1.TabIndex = 4
        Me.RichTextBox1.Text = ""
        '
        'DriverNotificationRtfViewerDlg
        '
        Me.AcceptButton = Me.MetroButton2
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle
        Me.ClientSize = New System.Drawing.Size(459, 369)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.MetroButton2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DriverNotificationRtfViewerDlg"
        Me.Resizable = False
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Style = MetroFramework.MetroColorStyle.Black
        Me.Text = "Infos zum Drucker"
        Me.Theme = MetroFramework.MetroThemeStyle.Light
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MetroButton2 As Controls.MetroButton
    Friend WithEvents RichTextBox1 As RichTextBox
End Class
