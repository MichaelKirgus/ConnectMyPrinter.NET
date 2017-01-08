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
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MetroTabControl1.SuspendLayout()
        Me.MetroTabPage1.SuspendLayout()
        Me.MetroTabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.printer_big
        Me.PictureBox1.Location = New System.Drawing.Point(23, 61)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(150, 150)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'MetroLabel1
        '
        Me.MetroLabel1.AutoSize = True
        Me.MetroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.MetroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Bold
        Me.MetroLabel1.Location = New System.Drawing.Point(179, 67)
        Me.MetroLabel1.Name = "MetroLabel1"
        Me.MetroLabel1.Size = New System.Drawing.Size(206, 25)
        Me.MetroLabel1.TabIndex = 1
        Me.MetroLabel1.Text = "ConnectMyPrinter.NET"
        '
        'MetroLabel2
        '
        Me.MetroLabel2.AutoSize = True
        Me.MetroLabel2.Location = New System.Drawing.Point(182, 92)
        Me.MetroLabel2.Name = "MetroLabel2"
        Me.MetroLabel2.Size = New System.Drawing.Size(357, 38)
        Me.MetroLabel2.TabIndex = 2
        Me.MetroLabel2.Text = "Copyright 2016 Michael Kirgus" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Tool zum einfachen Verwalten von Druckern unter Wi" &
    "ndows."
        '
        'MetroLabel3
        '
        Me.MetroLabel3.AutoSize = True
        Me.MetroLabel3.FontSize = MetroFramework.MetroLabelSize.Small
        Me.MetroLabel3.Location = New System.Drawing.Point(182, 145)
        Me.MetroLabel3.Name = "MetroLabel3"
        Me.MetroLabel3.Size = New System.Drawing.Size(321, 15)
        Me.MetroLabel3.TabIndex = 3
        Me.MetroLabel3.Text = "Diese Software verwendet das MetroFramework von Jens Thiel."
        '
        'MetroButton1
        '
        Me.MetroButton1.Location = New System.Drawing.Point(450, 491)
        Me.MetroButton1.Name = "MetroButton1"
        Me.MetroButton1.Size = New System.Drawing.Size(89, 23)
        Me.MetroButton1.TabIndex = 4
        Me.MetroButton1.Text = "Schließen"
        '
        'MetroTabControl1
        '
        Me.MetroTabControl1.Controls.Add(Me.MetroTabPage1)
        Me.MetroTabControl1.Controls.Add(Me.MetroTabPage2)
        Me.MetroTabControl1.Location = New System.Drawing.Point(23, 217)
        Me.MetroTabControl1.Name = "MetroTabControl1"
        Me.MetroTabControl1.SelectedIndex = 0
        Me.MetroTabControl1.Size = New System.Drawing.Size(511, 268)
        Me.MetroTabControl1.Style = MetroFramework.MetroColorStyle.Black
        Me.MetroTabControl1.TabIndex = 5
        '
        'MetroTabPage1
        '
        Me.MetroTabPage1.Controls.Add(Me.MetroTextBox1)
        Me.MetroTabPage1.HorizontalScrollbarBarColor = True
        Me.MetroTabPage1.Location = New System.Drawing.Point(4, 35)
        Me.MetroTabPage1.Name = "MetroTabPage1"
        Me.MetroTabPage1.Size = New System.Drawing.Size(503, 229)
        Me.MetroTabPage1.TabIndex = 0
        Me.MetroTabPage1.Text = "Programmlizenz"
        Me.MetroTabPage1.VerticalScrollbarBarColor = True
        '
        'MetroTextBox1
        '
        Me.MetroTextBox1.Location = New System.Drawing.Point(3, 3)
        Me.MetroTextBox1.Multiline = True
        Me.MetroTextBox1.Name = "MetroTextBox1"
        Me.MetroTextBox1.ReadOnly = True
        Me.MetroTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.MetroTextBox1.Size = New System.Drawing.Size(497, 223)
        Me.MetroTextBox1.TabIndex = 2
        Me.MetroTextBox1.Text = "#"
        '
        'MetroTabPage2
        '
        Me.MetroTabPage2.Controls.Add(Me.MetroTextBox2)
        Me.MetroTabPage2.HorizontalScrollbarBarColor = True
        Me.MetroTabPage2.Location = New System.Drawing.Point(4, 35)
        Me.MetroTabPage2.Name = "MetroTabPage2"
        Me.MetroTabPage2.Size = New System.Drawing.Size(503, 229)
        Me.MetroTabPage2.TabIndex = 1
        Me.MetroTabPage2.Text = "MetroFramework"
        Me.MetroTabPage2.VerticalScrollbarBarColor = True
        '
        'MetroTextBox2
        '
        Me.MetroTextBox2.Location = New System.Drawing.Point(3, 3)
        Me.MetroTextBox2.Multiline = True
        Me.MetroTextBox2.Name = "MetroTextBox2"
        Me.MetroTextBox2.ReadOnly = True
        Me.MetroTextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.MetroTextBox2.Size = New System.Drawing.Size(497, 223)
        Me.MetroTextBox2.TabIndex = 3
        Me.MetroTextBox2.Text = "#"
        '
        'AboutFrm
        '
        Me.AcceptButton = Me.MetroButton1
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle
        Me.ClientSize = New System.Drawing.Size(557, 537)
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
        Me.Text = "Über"
        Me.Theme = MetroFramework.MetroThemeStyle.Light
        Me.TopMost = True
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MetroTabControl1.ResumeLayout(False)
        Me.MetroTabPage1.ResumeLayout(False)
        Me.MetroTabPage2.ResumeLayout(False)
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
End Class
