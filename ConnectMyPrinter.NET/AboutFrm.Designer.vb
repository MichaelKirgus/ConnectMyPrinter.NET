<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AboutFrm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AboutFrm))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.MetroLabel1 = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel2 = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel3 = New MetroFramework.Controls.MetroLabel()
        Me.MetroButton1 = New MetroFramework.Controls.MetroButton()
        Me.MetroTabControl1 = New MetroFramework.Controls.MetroTabControl()
        Me.MetroTabPage1 = New MetroFramework.Controls.MetroTabPage()
        Me.MetroTextBox1 = New MetroFramework.Controls.MetroTextBox()
        Me.MetroTabPage2 = New MetroFramework.Controls.MetroTabPage()
        Me.MetroTextBox2 = New MetroFramework.Controls.MetroTextBox()
        Me.MetroTabPage3 = New MetroFramework.Controls.MetroTabPage()
        Me.MetroTextBox3 = New MetroFramework.Controls.MetroTextBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MetroTabControl1.SuspendLayout()
        Me.MetroTabPage1.SuspendLayout()
        Me.MetroTabPage2.SuspendLayout()
        Me.MetroTabPage3.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.printer_big
        resources.ApplyResources(Me.PictureBox1, "PictureBox1")
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.TabStop = False
        '
        'MetroLabel1
        '
        resources.ApplyResources(Me.MetroLabel1, "MetroLabel1")
        Me.MetroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.MetroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Bold
        Me.MetroLabel1.Name = "MetroLabel1"
        '
        'MetroLabel2
        '
        resources.ApplyResources(Me.MetroLabel2, "MetroLabel2")
        Me.MetroLabel2.Name = "MetroLabel2"
        '
        'MetroLabel3
        '
        resources.ApplyResources(Me.MetroLabel3, "MetroLabel3")
        Me.MetroLabel3.FontSize = MetroFramework.MetroLabelSize.Small
        Me.MetroLabel3.Name = "MetroLabel3"
        '
        'MetroButton1
        '
        resources.ApplyResources(Me.MetroButton1, "MetroButton1")
        Me.MetroButton1.Name = "MetroButton1"
        '
        'MetroTabControl1
        '
        Me.MetroTabControl1.Controls.Add(Me.MetroTabPage1)
        Me.MetroTabControl1.Controls.Add(Me.MetroTabPage3)
        Me.MetroTabControl1.Controls.Add(Me.MetroTabPage2)
        resources.ApplyResources(Me.MetroTabControl1, "MetroTabControl1")
        Me.MetroTabControl1.Name = "MetroTabControl1"
        Me.MetroTabControl1.SelectedIndex = 2
        Me.MetroTabControl1.Style = MetroFramework.MetroColorStyle.Black
        '
        'MetroTabPage1
        '
        Me.MetroTabPage1.Controls.Add(Me.MetroTextBox1)
        Me.MetroTabPage1.HorizontalScrollbarBarColor = True
        resources.ApplyResources(Me.MetroTabPage1, "MetroTabPage1")
        Me.MetroTabPage1.Name = "MetroTabPage1"
        Me.MetroTabPage1.VerticalScrollbarBarColor = True
        '
        'MetroTextBox1
        '
        resources.ApplyResources(Me.MetroTextBox1, "MetroTextBox1")
        Me.MetroTextBox1.Multiline = True
        Me.MetroTextBox1.Name = "MetroTextBox1"
        Me.MetroTextBox1.ReadOnly = True
        Me.MetroTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        '
        'MetroTabPage2
        '
        Me.MetroTabPage2.Controls.Add(Me.MetroTextBox2)
        Me.MetroTabPage2.HorizontalScrollbarBarColor = True
        resources.ApplyResources(Me.MetroTabPage2, "MetroTabPage2")
        Me.MetroTabPage2.Name = "MetroTabPage2"
        Me.MetroTabPage2.VerticalScrollbarBarColor = True
        '
        'MetroTextBox2
        '
        resources.ApplyResources(Me.MetroTextBox2, "MetroTextBox2")
        Me.MetroTextBox2.Multiline = True
        Me.MetroTextBox2.Name = "MetroTextBox2"
        Me.MetroTextBox2.ReadOnly = True
        Me.MetroTextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both
        '
        'MetroTabPage3
        '
        Me.MetroTabPage3.Controls.Add(Me.MetroTextBox3)
        Me.MetroTabPage3.HorizontalScrollbarBarColor = True
        resources.ApplyResources(Me.MetroTabPage3, "MetroTabPage3")
        Me.MetroTabPage3.Name = "MetroTabPage3"
        Me.MetroTabPage3.VerticalScrollbarBarColor = True
        '
        'MetroTextBox3
        '
        resources.ApplyResources(Me.MetroTextBox3, "MetroTextBox3")
        Me.MetroTextBox3.Multiline = True
        Me.MetroTextBox3.Name = "MetroTextBox3"
        Me.MetroTextBox3.ReadOnly = True
        Me.MetroTextBox3.ScrollBars = System.Windows.Forms.ScrollBars.Both
        '
        'AboutFrm
        '
        Me.AcceptButton = Me.MetroButton1
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle
        Me.Controls.Add(Me.MetroTabControl1)
        Me.Controls.Add(Me.MetroButton1)
        Me.Controls.Add(Me.MetroLabel3)
        Me.Controls.Add(Me.MetroLabel2)
        Me.Controls.Add(Me.MetroLabel1)
        Me.Controls.Add(Me.PictureBox1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AboutFrm"
        Me.Resizable = False
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Style = MetroFramework.MetroColorStyle.Black
        Me.Theme = MetroFramework.MetroThemeStyle.Light
        Me.TopMost = True
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MetroTabControl1.ResumeLayout(False)
        Me.MetroTabPage1.ResumeLayout(False)
        Me.MetroTabPage2.ResumeLayout(False)
        Me.MetroTabPage3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents MetroLabel1 As Controls.MetroLabel
    Friend WithEvents MetroLabel2 As Controls.MetroLabel
    Friend WithEvents MetroLabel3 As Controls.MetroLabel
    Friend WithEvents MetroButton1 As Controls.MetroButton
    Friend WithEvents MetroTabControl1 As Controls.MetroTabControl
    Friend WithEvents MetroTabPage1 As Controls.MetroTabPage
    Friend WithEvents MetroTabPage2 As Controls.MetroTabPage
    Friend WithEvents MetroTextBox1 As Controls.MetroTextBox
    Friend WithEvents MetroTextBox2 As Controls.MetroTextBox
    Friend WithEvents MetroTabPage3 As Controls.MetroTabPage
    Friend WithEvents MetroTextBox3 As Controls.MetroTextBox
End Class
