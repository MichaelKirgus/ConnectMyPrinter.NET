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
        Me.MetroLabel1Lbl.Location = New System.Drawing.Point(44, 1)
        Me.MetroLabel1Lbl.Name = "MetroLabel1Lbl"
        Me.MetroLabel1Lbl.Size = New System.Drawing.Size(284, 23)
        Me.MetroLabel1Lbl.TabIndex = 1
        Me.MetroLabel1Lbl.Text = "<PrinterName>"
        '
        'MetroLabel2Lbl
        '
        Me.MetroLabel2Lbl.BackColor = System.Drawing.Color.White
        Me.MetroLabel2Lbl.CustomBackground = True
        Me.MetroLabel2Lbl.FontSize = MetroFramework.MetroLabelSize.Small
        Me.MetroLabel2Lbl.Location = New System.Drawing.Point(48, 24)
        Me.MetroLabel2Lbl.Name = "MetroLabel2Lbl"
        Me.MetroLabel2Lbl.Size = New System.Drawing.Size(280, 19)
        Me.MetroLabel2Lbl.TabIndex = 2
        Me.MetroLabel2Lbl.Text = "<PrinterServer>"
        '
        'MetroLabel6Lbl
        '
        Me.MetroLabel6Lbl.AutoSize = True
        Me.MetroLabel6Lbl.BackColor = System.Drawing.Color.White
        Me.MetroLabel6Lbl.CustomBackground = True
        Me.MetroLabel6Lbl.Location = New System.Drawing.Point(3, 53)
        Me.MetroLabel6Lbl.Name = "MetroLabel6Lbl"
        Me.MetroLabel6Lbl.Size = New System.Drawing.Size(63, 19)
        Me.MetroLabel6Lbl.TabIndex = 17
        Me.MetroLabel6Lbl.Text = "Standort:"
        '
        'MetroLabel7Lbl
        '
        Me.MetroLabel7Lbl.AutoSize = True
        Me.MetroLabel7Lbl.BackColor = System.Drawing.Color.White
        Me.MetroLabel7Lbl.CustomBackground = True
        Me.MetroLabel7Lbl.Location = New System.Drawing.Point(3, 76)
        Me.MetroLabel7Lbl.Name = "MetroLabel7Lbl"
        Me.MetroLabel7Lbl.Size = New System.Drawing.Size(90, 19)
        Me.MetroLabel7Lbl.TabIndex = 18
        Me.MetroLabel7Lbl.Text = "Beschreibung:"
        '
        'LocationLbl
        '
        Me.LocationLbl.AutoSize = True
        Me.LocationLbl.BackColor = System.Drawing.Color.White
        Me.LocationLbl.CustomBackground = True
        Me.LocationLbl.Location = New System.Drawing.Point(107, 53)
        Me.LocationLbl.Name = "LocationLbl"
        Me.LocationLbl.Size = New System.Drawing.Size(93, 19)
        Me.LocationLbl.TabIndex = 19
        Me.LocationLbl.Text = "<LocationLbl>"
        '
        'DescriptionLbl
        '
        Me.DescriptionLbl.AutoSize = True
        Me.DescriptionLbl.BackColor = System.Drawing.Color.White
        Me.DescriptionLbl.CustomBackground = True
        Me.DescriptionLbl.Location = New System.Drawing.Point(107, 76)
        Me.DescriptionLbl.Name = "DescriptionLbl"
        Me.DescriptionLbl.Size = New System.Drawing.Size(109, 19)
        Me.DescriptionLbl.TabIndex = 20
        Me.DescriptionLbl.Text = "<DescriptionLbl>"
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox2.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.expandable
        Me.PictureBox2.Location = New System.Drawing.Point(336, 7)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(28, 28)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 13
        Me.PictureBox2.TabStop = False
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.DeletePrinter2
        Me.Button1.Location = New System.Drawing.Point(367, 0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(22, 22)
        Me.Button1.TabIndex = 12
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.plus_bw
        Me.Button2.Location = New System.Drawing.Point(367, 20)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(22, 22)
        Me.Button2.TabIndex = 11
        Me.Button2.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.printer_nonstandard
        Me.PictureBox1.Location = New System.Drawing.Point(3, 4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(35, 35)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'SavedPrinterCtl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
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
        Me.Size = New System.Drawing.Size(391, 42)
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
