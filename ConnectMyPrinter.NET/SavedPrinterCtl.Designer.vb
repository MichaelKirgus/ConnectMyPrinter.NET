<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SavedPrinterCtl
    Inherits MetroFramework.Controls.MetroUserControl

    'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SavedPrinterCtl))
        Me.MetroLabel1Lbl = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel2Lbl = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel6Lbl = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel7Lbl = New MetroFramework.Controls.MetroLabel()
        Me.LocationLbl = New MetroFramework.Controls.MetroLabel()
        Me.DescriptionLbl = New MetroFramework.Controls.MetroLabel()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MetroLabel1Lbl
        '
        Me.MetroLabel1Lbl.BackColor = System.Drawing.Color.White
        Me.MetroLabel1Lbl.CustomBackground = True
        Me.MetroLabel1Lbl.FontSize = MetroFramework.MetroLabelSize.Tall
        resources.ApplyResources(Me.MetroLabel1Lbl, "MetroLabel1Lbl")
        Me.MetroLabel1Lbl.Name = "MetroLabel1Lbl"
        '
        'MetroLabel2Lbl
        '
        Me.MetroLabel2Lbl.BackColor = System.Drawing.Color.White
        Me.MetroLabel2Lbl.CustomBackground = True
        Me.MetroLabel2Lbl.FontSize = MetroFramework.MetroLabelSize.Small
        resources.ApplyResources(Me.MetroLabel2Lbl, "MetroLabel2Lbl")
        Me.MetroLabel2Lbl.Name = "MetroLabel2Lbl"
        '
        'MetroLabel6Lbl
        '
        resources.ApplyResources(Me.MetroLabel6Lbl, "MetroLabel6Lbl")
        Me.MetroLabel6Lbl.BackColor = System.Drawing.Color.White
        Me.MetroLabel6Lbl.CustomBackground = True
        Me.MetroLabel6Lbl.Name = "MetroLabel6Lbl"
        '
        'MetroLabel7Lbl
        '
        resources.ApplyResources(Me.MetroLabel7Lbl, "MetroLabel7Lbl")
        Me.MetroLabel7Lbl.BackColor = System.Drawing.Color.White
        Me.MetroLabel7Lbl.CustomBackground = True
        Me.MetroLabel7Lbl.Name = "MetroLabel7Lbl"
        '
        'LocationLbl
        '
        resources.ApplyResources(Me.LocationLbl, "LocationLbl")
        Me.LocationLbl.BackColor = System.Drawing.Color.White
        Me.LocationLbl.CustomBackground = True
        Me.LocationLbl.Name = "LocationLbl"
        '
        'DescriptionLbl
        '
        resources.ApplyResources(Me.DescriptionLbl, "DescriptionLbl")
        Me.DescriptionLbl.BackColor = System.Drawing.Color.White
        Me.DescriptionLbl.CustomBackground = True
        Me.DescriptionLbl.Name = "DescriptionLbl"
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox2.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.expandable
        resources.ApplyResources(Me.PictureBox2, "PictureBox2")
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.TabStop = False
        '
        'Button1
        '
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.DeletePrinter2
        Me.Button1.Name = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        resources.ApplyResources(Me.Button2, "Button2")
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.plus_bw
        Me.Button2.Name = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.printer_nonstandard
        resources.ApplyResources(Me.PictureBox1, "PictureBox1")
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.TabStop = False
        '
        'SavedPrinterCtl
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.DescriptionLbl)
        Me.Controls.Add(Me.LocationLbl)
        Me.Controls.Add(Me.MetroLabel7Lbl)
        Me.Controls.Add(Me.MetroLabel6Lbl)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.MetroLabel2Lbl)
        Me.Controls.Add(Me.MetroLabel1Lbl)
        Me.Controls.Add(Me.PictureBox1)
        Me.CustomBackground = True
        Me.Name = "SavedPrinterCtl"
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents MetroLabel1Lbl As Controls.MetroLabel
    Friend WithEvents MetroLabel2Lbl As Controls.MetroLabel
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents MetroLabel6Lbl As Controls.MetroLabel
    Friend WithEvents MetroLabel7Lbl As Controls.MetroLabel
    Friend WithEvents LocationLbl As Controls.MetroLabel
    Friend WithEvents DescriptionLbl As Controls.MetroLabel
End Class
