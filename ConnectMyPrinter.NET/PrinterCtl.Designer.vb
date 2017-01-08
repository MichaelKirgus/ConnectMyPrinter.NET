<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PrinterCtl
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
        Me.components = New System.ComponentModel.Container()
        Me.MetroLabel1Lbl = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel2Lbl = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel3Lbl = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel4Lbl = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel5Lbl = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel6Lbl = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel7Lbl = New MetroFramework.Controls.MetroLabel()
        Me.LocationLbl = New MetroFramework.Controls.MetroLabel()
        Me.DescriptionLbl = New MetroFramework.Controls.MetroLabel()
        Me.DriverLbl = New MetroFramework.Controls.MetroLabel()
        Me.DriverVersionLbl = New MetroFramework.Controls.MetroLabel()
        Me.StateLbl = New MetroFramework.Controls.MetroLabel()
        Me.DataFileLbl = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel10Lbl = New MetroFramework.Controls.MetroLabel()
        Me.ConfigFileLbl = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel12Lbl = New MetroFramework.Controls.MetroLabel()
        Me.DriverPathLbl = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel13Lbl = New MetroFramework.Controls.MetroLabel()
        Me.MetroButton1 = New MetroFramework.Controls.MetroButton()
        Me.MetroButton2 = New MetroFramework.Controls.MetroButton()
        Me.MetroButton3 = New MetroFramework.Controls.MetroButton()
        Me.MetroButton4 = New MetroFramework.Controls.MetroButton()
        Me.MetroButton5 = New MetroFramework.Controls.MetroButton()
        Me.MetroButton6 = New MetroFramework.Controls.MetroButton()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AlsStandarddruckerFestlegenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.DruckerImProfilSpeichernToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.StandardeinstellungenLöschenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.DruckereinstellungenExportierenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DruckereinstellungenImportierenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ContextMenuStrip1.SuspendLayout()
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
        'MetroLabel3Lbl
        '
        Me.MetroLabel3Lbl.AutoSize = True
        Me.MetroLabel3Lbl.BackColor = System.Drawing.Color.White
        Me.MetroLabel3Lbl.CustomBackground = True
        Me.MetroLabel3Lbl.Location = New System.Drawing.Point(2, 98)
        Me.MetroLabel3Lbl.Name = "MetroLabel3Lbl"
        Me.MetroLabel3Lbl.Size = New System.Drawing.Size(85, 19)
        Me.MetroLabel3Lbl.TabIndex = 14
        Me.MetroLabel3Lbl.Text = "Treibername:"
        '
        'MetroLabel4Lbl
        '
        Me.MetroLabel4Lbl.AutoSize = True
        Me.MetroLabel4Lbl.BackColor = System.Drawing.Color.White
        Me.MetroLabel4Lbl.CustomBackground = True
        Me.MetroLabel4Lbl.Location = New System.Drawing.Point(2, 121)
        Me.MetroLabel4Lbl.Name = "MetroLabel4Lbl"
        Me.MetroLabel4Lbl.Size = New System.Drawing.Size(94, 19)
        Me.MetroLabel4Lbl.TabIndex = 15
        Me.MetroLabel4Lbl.Text = "Treiberversion:"
        '
        'MetroLabel5Lbl
        '
        Me.MetroLabel5Lbl.AutoSize = True
        Me.MetroLabel5Lbl.BackColor = System.Drawing.Color.White
        Me.MetroLabel5Lbl.CustomBackground = True
        Me.MetroLabel5Lbl.Location = New System.Drawing.Point(2, 144)
        Me.MetroLabel5Lbl.Name = "MetroLabel5Lbl"
        Me.MetroLabel5Lbl.Size = New System.Drawing.Size(83, 19)
        Me.MetroLabel5Lbl.TabIndex = 16
        Me.MetroLabel5Lbl.Text = "Gerätestatus:"
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
        'DriverLbl
        '
        Me.DriverLbl.AutoSize = True
        Me.DriverLbl.BackColor = System.Drawing.Color.White
        Me.DriverLbl.CustomBackground = True
        Me.DriverLbl.Location = New System.Drawing.Point(107, 98)
        Me.DriverLbl.Name = "DriverLbl"
        Me.DriverLbl.Size = New System.Drawing.Size(79, 19)
        Me.DriverLbl.TabIndex = 21
        Me.DriverLbl.Text = "<DriverLbl>"
        '
        'DriverVersionLbl
        '
        Me.DriverVersionLbl.AutoSize = True
        Me.DriverVersionLbl.BackColor = System.Drawing.Color.White
        Me.DriverVersionLbl.CustomBackground = True
        Me.DriverVersionLbl.Location = New System.Drawing.Point(107, 121)
        Me.DriverVersionLbl.Name = "DriverVersionLbl"
        Me.DriverVersionLbl.Size = New System.Drawing.Size(121, 19)
        Me.DriverVersionLbl.TabIndex = 22
        Me.DriverVersionLbl.Text = "<DriverVersionLbl>"
        '
        'StateLbl
        '
        Me.StateLbl.AutoSize = True
        Me.StateLbl.BackColor = System.Drawing.Color.White
        Me.StateLbl.CustomBackground = True
        Me.StateLbl.Location = New System.Drawing.Point(107, 144)
        Me.StateLbl.Name = "StateLbl"
        Me.StateLbl.Size = New System.Drawing.Size(73, 19)
        Me.StateLbl.TabIndex = 23
        Me.StateLbl.Text = "<StateLbl>"
        '
        'DataFileLbl
        '
        Me.DataFileLbl.BackColor = System.Drawing.Color.White
        Me.DataFileLbl.CustomBackground = True
        Me.DataFileLbl.FontSize = MetroFramework.MetroLabelSize.Small
        Me.DataFileLbl.LabelMode = MetroFramework.Controls.MetroLabelMode.Selectable
        Me.DataFileLbl.Location = New System.Drawing.Point(108, 172)
        Me.DataFileLbl.Name = "DataFileLbl"
        Me.DataFileLbl.Size = New System.Drawing.Size(74, 15)
        Me.DataFileLbl.TabIndex = 27
        Me.DataFileLbl.Text = "<DatafileLbl>"
        '
        'MetroLabel10Lbl
        '
        Me.MetroLabel10Lbl.AutoSize = True
        Me.MetroLabel10Lbl.BackColor = System.Drawing.Color.White
        Me.MetroLabel10Lbl.CustomBackground = True
        Me.MetroLabel10Lbl.FontSize = MetroFramework.MetroLabelSize.Small
        Me.MetroLabel10Lbl.Location = New System.Drawing.Point(4, 171)
        Me.MetroLabel10Lbl.Name = "MetroLabel10Lbl"
        Me.MetroLabel10Lbl.Size = New System.Drawing.Size(65, 15)
        Me.MetroLabel10Lbl.TabIndex = 26
        Me.MetroLabel10Lbl.Text = "Datendatei:"
        '
        'ConfigFileLbl
        '
        Me.ConfigFileLbl.BackColor = System.Drawing.Color.White
        Me.ConfigFileLbl.CustomBackground = True
        Me.ConfigFileLbl.FontSize = MetroFramework.MetroLabelSize.Small
        Me.ConfigFileLbl.LabelMode = MetroFramework.Controls.MetroLabelMode.Selectable
        Me.ConfigFileLbl.Location = New System.Drawing.Point(108, 190)
        Me.ConfigFileLbl.Name = "ConfigFileLbl"
        Me.ConfigFileLbl.Size = New System.Drawing.Size(85, 15)
        Me.ConfigFileLbl.TabIndex = 29
        Me.ConfigFileLbl.Text = "<ConfigFileLbl>"
        '
        'MetroLabel12Lbl
        '
        Me.MetroLabel12Lbl.AutoSize = True
        Me.MetroLabel12Lbl.BackColor = System.Drawing.Color.White
        Me.MetroLabel12Lbl.CustomBackground = True
        Me.MetroLabel12Lbl.FontSize = MetroFramework.MetroLabelSize.Small
        Me.MetroLabel12Lbl.Location = New System.Drawing.Point(4, 190)
        Me.MetroLabel12Lbl.Name = "MetroLabel12Lbl"
        Me.MetroLabel12Lbl.Size = New System.Drawing.Size(94, 15)
        Me.MetroLabel12Lbl.TabIndex = 28
        Me.MetroLabel12Lbl.Text = "Konfigurationsfile:"
        '
        'DriverPathLbl
        '
        Me.DriverPathLbl.BackColor = System.Drawing.Color.White
        Me.DriverPathLbl.CustomBackground = True
        Me.DriverPathLbl.FontSize = MetroFramework.MetroLabelSize.Small
        Me.DriverPathLbl.LabelMode = MetroFramework.Controls.MetroLabelMode.Selectable
        Me.DriverPathLbl.Location = New System.Drawing.Point(108, 208)
        Me.DriverPathLbl.Name = "DriverPathLbl"
        Me.DriverPathLbl.Size = New System.Drawing.Size(85, 15)
        Me.DriverPathLbl.TabIndex = 31
        Me.DriverPathLbl.Text = "<DriverPathLbl>"
        '
        'MetroLabel13Lbl
        '
        Me.MetroLabel13Lbl.AutoSize = True
        Me.MetroLabel13Lbl.BackColor = System.Drawing.Color.White
        Me.MetroLabel13Lbl.CustomBackground = True
        Me.MetroLabel13Lbl.FontSize = MetroFramework.MetroLabelSize.Small
        Me.MetroLabel13Lbl.Location = New System.Drawing.Point(4, 208)
        Me.MetroLabel13Lbl.Name = "MetroLabel13Lbl"
        Me.MetroLabel13Lbl.Size = New System.Drawing.Size(66, 15)
        Me.MetroLabel13Lbl.TabIndex = 30
        Me.MetroLabel13Lbl.Text = "Treiberpfad:"
        '
        'MetroButton1
        '
        Me.MetroButton1.Location = New System.Drawing.Point(3, 235)
        Me.MetroButton1.Name = "MetroButton1"
        Me.MetroButton1.Size = New System.Drawing.Size(183, 23)
        Me.MetroButton1.TabIndex = 32
        Me.MetroButton1.Text = "Testseite drucken"
        '
        'MetroButton2
        '
        Me.MetroButton2.Location = New System.Drawing.Point(192, 235)
        Me.MetroButton2.Name = "MetroButton2"
        Me.MetroButton2.Size = New System.Drawing.Size(196, 23)
        Me.MetroButton2.TabIndex = 33
        Me.MetroButton2.Text = "Druckerwarteschlange öffnen"
        '
        'MetroButton3
        '
        Me.MetroButton3.Location = New System.Drawing.Point(3, 261)
        Me.MetroButton3.Name = "MetroButton3"
        Me.MetroButton3.Size = New System.Drawing.Size(183, 23)
        Me.MetroButton3.TabIndex = 34
        Me.MetroButton3.Text = "Druckerwarteschlange leeren"
        '
        'MetroButton4
        '
        Me.MetroButton4.Location = New System.Drawing.Point(192, 261)
        Me.MetroButton4.Name = "MetroButton4"
        Me.MetroButton4.Size = New System.Drawing.Size(196, 23)
        Me.MetroButton4.TabIndex = 35
        Me.MetroButton4.Text = "Drucker+ Treiber löschen"
        '
        'MetroButton5
        '
        Me.MetroButton5.Location = New System.Drawing.Point(3, 287)
        Me.MetroButton5.Name = "MetroButton5"
        Me.MetroButton5.Size = New System.Drawing.Size(183, 23)
        Me.MetroButton5.TabIndex = 36
        Me.MetroButton5.Text = "Drucker neu installieren"
        '
        'MetroButton6
        '
        Me.MetroButton6.Location = New System.Drawing.Point(192, 287)
        Me.MetroButton6.Name = "MetroButton6"
        Me.MetroButton6.Size = New System.Drawing.Size(196, 23)
        Me.MetroButton6.TabIndex = 37
        Me.MetroButton6.Text = "Druckerinformationen neu ermitteln"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.BackColor = System.Drawing.Color.White
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AlsStandarddruckerFestlegenToolStripMenuItem, Me.ToolStripSeparator1, Me.DruckerImProfilSpeichernToolStripMenuItem, Me.ToolStripSeparator2, Me.StandardeinstellungenLöschenToolStripMenuItem, Me.DruckereinstellungenExportierenToolStripMenuItem, Me.DruckereinstellungenImportierenToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(269, 166)
        '
        'AlsStandarddruckerFestlegenToolStripMenuItem
        '
        Me.AlsStandarddruckerFestlegenToolStripMenuItem.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.dialog_ok
        Me.AlsStandarddruckerFestlegenToolStripMenuItem.Name = "AlsStandarddruckerFestlegenToolStripMenuItem"
        Me.AlsStandarddruckerFestlegenToolStripMenuItem.Size = New System.Drawing.Size(268, 30)
        Me.AlsStandarddruckerFestlegenToolStripMenuItem.Text = "Als Standarddrucker festlegen"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(265, 6)
        '
        'DruckerImProfilSpeichernToolStripMenuItem
        '
        Me.DruckerImProfilSpeichernToolStripMenuItem.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.floppy_nocustom
        Me.DruckerImProfilSpeichernToolStripMenuItem.Name = "DruckerImProfilSpeichernToolStripMenuItem"
        Me.DruckerImProfilSpeichernToolStripMenuItem.Size = New System.Drawing.Size(268, 30)
        Me.DruckerImProfilSpeichernToolStripMenuItem.Text = "Drucker im Profil speichern"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(265, 6)
        '
        'StandardeinstellungenLöschenToolStripMenuItem
        '
        Me.StandardeinstellungenLöschenToolStripMenuItem.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.DeletePrinter
        Me.StandardeinstellungenLöschenToolStripMenuItem.Name = "StandardeinstellungenLöschenToolStripMenuItem"
        Me.StandardeinstellungenLöschenToolStripMenuItem.Size = New System.Drawing.Size(268, 30)
        Me.StandardeinstellungenLöschenToolStripMenuItem.Text = "Standardeinstellungen löschen"
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
        Me.ToolTip1.SetToolTip(Me.Button1, "Drucker entfernen")
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.settings_small
        Me.Button2.Location = New System.Drawing.Point(367, 20)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(22, 22)
        Me.Button2.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.Button2, "Druckereinstellungen")
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
        'DruckereinstellungenExportierenToolStripMenuItem
        '
        Me.DruckereinstellungenExportierenToolStripMenuItem.Name = "DruckereinstellungenExportierenToolStripMenuItem"
        Me.DruckereinstellungenExportierenToolStripMenuItem.Size = New System.Drawing.Size(268, 30)
        Me.DruckereinstellungenExportierenToolStripMenuItem.Text = "Druckereinstellungen exportieren..."
        Me.DruckereinstellungenExportierenToolStripMenuItem.Visible = False
        '
        'DruckereinstellungenImportierenToolStripMenuItem
        '
        Me.DruckereinstellungenImportierenToolStripMenuItem.Name = "DruckereinstellungenImportierenToolStripMenuItem"
        Me.DruckereinstellungenImportierenToolStripMenuItem.Size = New System.Drawing.Size(268, 30)
        Me.DruckereinstellungenImportierenToolStripMenuItem.Text = "Druckereinstellungen importieren..."
        Me.DruckereinstellungenImportierenToolStripMenuItem.Visible = False
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.DefaultExt = "dat"
        Me.SaveFileDialog1.Filter = "Druckereinstellungen (*.dat)|*.dat|Alle Dateien|*.*"
        Me.SaveFileDialog1.RestoreDirectory = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "Druckereinstellungen (*.dat)|*.dat|Alle Dateien|*.*"
        Me.OpenFileDialog1.RestoreDirectory = True
        '
        'PrinterCtl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Controls.Add(Me.MetroButton6)
        Me.Controls.Add(Me.MetroButton5)
        Me.Controls.Add(Me.MetroButton4)
        Me.Controls.Add(Me.MetroButton3)
        Me.Controls.Add(Me.MetroButton2)
        Me.Controls.Add(Me.MetroButton1)
        Me.Controls.Add(Me.DriverPathLbl)
        Me.Controls.Add(Me.MetroLabel13Lbl)
        Me.Controls.Add(Me.ConfigFileLbl)
        Me.Controls.Add(Me.MetroLabel12Lbl)
        Me.Controls.Add(Me.DataFileLbl)
        Me.Controls.Add(Me.MetroLabel10Lbl)
        Me.Controls.Add(Me.StateLbl)
        Me.Controls.Add(Me.DriverVersionLbl)
        Me.Controls.Add(Me.DriverLbl)
        Me.Controls.Add(Me.DescriptionLbl)
        Me.Controls.Add(Me.LocationLbl)
        Me.Controls.Add(Me.MetroLabel7Lbl)
        Me.Controls.Add(Me.MetroLabel6Lbl)
        Me.Controls.Add(Me.MetroLabel5Lbl)
        Me.Controls.Add(Me.MetroLabel4Lbl)
        Me.Controls.Add(Me.MetroLabel3Lbl)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.MetroLabel2Lbl)
        Me.Controls.Add(Me.MetroLabel1Lbl)
        Me.Controls.Add(Me.PictureBox1)
        Me.CustomBackground = True
        Me.Name = "PrinterCtl"
        Me.Size = New System.Drawing.Size(391, 42)
        Me.ContextMenuStrip1.ResumeLayout(False)
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
    Friend WithEvents MetroLabel3Lbl As Controls.MetroLabel
    Friend WithEvents MetroLabel4Lbl As Controls.MetroLabel
    Friend WithEvents MetroLabel5Lbl As Controls.MetroLabel
    Friend WithEvents MetroLabel6Lbl As Controls.MetroLabel
    Friend WithEvents MetroLabel7Lbl As Controls.MetroLabel
    Friend WithEvents LocationLbl As Controls.MetroLabel
    Friend WithEvents DescriptionLbl As Controls.MetroLabel
    Friend WithEvents DriverLbl As Controls.MetroLabel
    Friend WithEvents DriverVersionLbl As Controls.MetroLabel
    Friend WithEvents StateLbl As Controls.MetroLabel
    Friend WithEvents DataFileLbl As Controls.MetroLabel
    Friend WithEvents MetroLabel10Lbl As Controls.MetroLabel
    Friend WithEvents ConfigFileLbl As Controls.MetroLabel
    Friend WithEvents MetroLabel12Lbl As Controls.MetroLabel
    Friend WithEvents DriverPathLbl As Controls.MetroLabel
    Friend WithEvents MetroLabel13Lbl As Controls.MetroLabel
    Friend WithEvents MetroButton1 As Controls.MetroButton
    Friend WithEvents MetroButton2 As Controls.MetroButton
    Friend WithEvents MetroButton3 As Controls.MetroButton
    Friend WithEvents MetroButton4 As Controls.MetroButton
    Friend WithEvents MetroButton5 As Controls.MetroButton
    Friend WithEvents MetroButton6 As Controls.MetroButton
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents AlsStandarddruckerFestlegenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents DruckerImProfilSpeichernToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents StandardeinstellungenLöschenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents DruckereinstellungenExportierenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DruckereinstellungenImportierenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
End Class
