<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectFromMultipleServersDlg
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectFromMultipleServersDlg))
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.MetroButton1 = New MetroFramework.Controls.MetroButton()
        Me.MetroLabel1 = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel2 = New MetroFramework.Controls.MetroLabel()
        Me.SuspendLayout()
        '
        'ListBox1
        '
        resources.ApplyResources(Me.ListBox1, "ListBox1")
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Name = "ListBox1"
        '
        'MetroButton1
        '
        resources.ApplyResources(Me.MetroButton1, "MetroButton1")
        Me.MetroButton1.Name = "MetroButton1"
        '
        'MetroLabel1
        '
        resources.ApplyResources(Me.MetroLabel1, "MetroLabel1")
        Me.MetroLabel1.Name = "MetroLabel1"
        '
        'MetroLabel2
        '
        resources.ApplyResources(Me.MetroLabel2, "MetroLabel2")
        Me.MetroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Bold
        Me.MetroLabel2.Name = "MetroLabel2"
        '
        'SelectFromMultipleServersDlg
        '
        Me.AcceptButton = Me.MetroButton1
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.MetroLabel2)
        Me.Controls.Add(Me.MetroLabel1)
        Me.Controls.Add(Me.MetroButton1)
        Me.Controls.Add(Me.ListBox1)
        Me.Name = "SelectFromMultipleServersDlg"
        Me.Resizable = False
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Style = MetroFramework.MetroColorStyle.Black
        Me.Theme = MetroFramework.MetroThemeStyle.Light
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents MetroButton1 As Controls.MetroButton
    Friend WithEvents MetroLabel1 As Controls.MetroLabel
    Friend WithEvents MetroLabel2 As Controls.MetroLabel
End Class
