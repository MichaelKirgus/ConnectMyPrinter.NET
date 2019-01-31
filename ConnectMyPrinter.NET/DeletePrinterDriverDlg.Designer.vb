Imports MetroFramework

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DeletePrinterDriverDlg))
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
        resources.ApplyResources(Me.MetroLabel1, "MetroLabel1")
        Me.MetroLabel1.Name = "MetroLabel1"
        '
        'MetroCheckBox1
        '
        resources.ApplyResources(Me.MetroCheckBox1, "MetroCheckBox1")
        Me.MetroCheckBox1.Checked = True
        Me.MetroCheckBox1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.MetroCheckBox1.Name = "MetroCheckBox1"
        Me.MetroCheckBox1.UseSelectable = True
        '
        'MetroCheckBox2
        '
        resources.ApplyResources(Me.MetroCheckBox2, "MetroCheckBox2")
        Me.MetroCheckBox2.Checked = True
        Me.MetroCheckBox2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.MetroCheckBox2.Name = "MetroCheckBox2"
        Me.MetroCheckBox2.UseSelectable = True
        '
        'MetroCheckBox3
        '
        resources.ApplyResources(Me.MetroCheckBox3, "MetroCheckBox3")
        Me.MetroCheckBox3.Checked = True
        Me.MetroCheckBox3.CheckState = System.Windows.Forms.CheckState.Checked
        Me.MetroCheckBox3.Name = "MetroCheckBox3"
        Me.MetroCheckBox3.UseSelectable = True
        '
        'MetroCheckBox4
        '
        resources.ApplyResources(Me.MetroCheckBox4, "MetroCheckBox4")
        Me.MetroCheckBox4.Checked = True
        Me.MetroCheckBox4.CheckState = System.Windows.Forms.CheckState.Checked
        Me.MetroCheckBox4.Name = "MetroCheckBox4"
        Me.MetroCheckBox4.UseSelectable = True
        '
        'MetroCheckBox5
        '
        resources.ApplyResources(Me.MetroCheckBox5, "MetroCheckBox5")
        Me.MetroCheckBox5.Checked = True
        Me.MetroCheckBox5.CheckState = System.Windows.Forms.CheckState.Checked
        Me.MetroCheckBox5.Name = "MetroCheckBox5"
        Me.MetroCheckBox5.UseSelectable = True
        '
        'MetroCheckBox6
        '
        resources.ApplyResources(Me.MetroCheckBox6, "MetroCheckBox6")
        Me.MetroCheckBox6.Checked = True
        Me.MetroCheckBox6.CheckState = System.Windows.Forms.CheckState.Checked
        Me.MetroCheckBox6.Name = "MetroCheckBox6"
        Me.MetroCheckBox6.UseSelectable = True
        '
        'MetroCheckBox7
        '
        resources.ApplyResources(Me.MetroCheckBox7, "MetroCheckBox7")
        Me.MetroCheckBox7.Checked = True
        Me.MetroCheckBox7.CheckState = System.Windows.Forms.CheckState.Checked
        Me.MetroCheckBox7.Name = "MetroCheckBox7"
        Me.MetroCheckBox7.UseSelectable = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.uac_shield1
        resources.ApplyResources(Me.PictureBox1, "PictureBox1")
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.uac_shield1
        resources.ApplyResources(Me.PictureBox2, "PictureBox2")
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.uac_shield1
        resources.ApplyResources(Me.PictureBox3, "PictureBox3")
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.TabStop = False
        '
        'MetroButton1
        '
        resources.ApplyResources(Me.MetroButton1, "MetroButton1")
        Me.MetroButton1.Name = "MetroButton1"
        Me.MetroButton1.UseSelectable = True
        '
        'DeletePrinterDriverDlg
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
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
        Me.ShadowType = MetroFramework.Forms.MetroFormShadowType.None
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Style = MetroFramework.MetroColorStyle.Black
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
