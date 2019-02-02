Imports MetroFramework

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PrinterCtl))
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
        Me.DruckerEntfernenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DruckerEntfernenlokalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DruckerNeuInstallierenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StandardeinstellungenLöschenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.DruckereigenschaftenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DruckereinstellungenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.DruckereinstellungenExportierenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DruckereinstellungenImportierenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProfildateiErstellenverbindenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProfildateioErstellenentfernenUndVerbindenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog2 = New System.Windows.Forms.SaveFileDialog()
        Me.ProfildateiVersendenverbindenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProfildateiVersendenentfernenUndVerbindenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MetroLabel1Lbl
        '
        Me.MetroLabel1Lbl.BackColor = System.Drawing.Color.White
        Me.MetroLabel1Lbl.FontSize = MetroFramework.MetroLabelSize.Tall
        resources.ApplyResources(Me.MetroLabel1Lbl, "MetroLabel1Lbl")
        Me.MetroLabel1Lbl.Name = "MetroLabel1Lbl"
        Me.ToolTip1.SetToolTip(Me.MetroLabel1Lbl, resources.GetString("MetroLabel1Lbl.ToolTip"))
        Me.MetroLabel1Lbl.UseCustomBackColor = True
        '
        'MetroLabel2Lbl
        '
        Me.MetroLabel2Lbl.BackColor = System.Drawing.Color.White
        Me.MetroLabel2Lbl.FontSize = MetroFramework.MetroLabelSize.Small
        resources.ApplyResources(Me.MetroLabel2Lbl, "MetroLabel2Lbl")
        Me.MetroLabel2Lbl.Name = "MetroLabel2Lbl"
        Me.ToolTip1.SetToolTip(Me.MetroLabel2Lbl, resources.GetString("MetroLabel2Lbl.ToolTip"))
        Me.MetroLabel2Lbl.UseCustomBackColor = True
        '
        'MetroLabel3Lbl
        '
        resources.ApplyResources(Me.MetroLabel3Lbl, "MetroLabel3Lbl")
        Me.MetroLabel3Lbl.BackColor = System.Drawing.Color.White
        Me.MetroLabel3Lbl.Name = "MetroLabel3Lbl"
        Me.MetroLabel3Lbl.UseCustomBackColor = True
        '
        'MetroLabel4Lbl
        '
        resources.ApplyResources(Me.MetroLabel4Lbl, "MetroLabel4Lbl")
        Me.MetroLabel4Lbl.BackColor = System.Drawing.Color.White
        Me.MetroLabel4Lbl.Name = "MetroLabel4Lbl"
        Me.MetroLabel4Lbl.UseCustomBackColor = True
        '
        'MetroLabel5Lbl
        '
        resources.ApplyResources(Me.MetroLabel5Lbl, "MetroLabel5Lbl")
        Me.MetroLabel5Lbl.BackColor = System.Drawing.Color.White
        Me.MetroLabel5Lbl.Name = "MetroLabel5Lbl"
        Me.MetroLabel5Lbl.UseCustomBackColor = True
        '
        'MetroLabel6Lbl
        '
        resources.ApplyResources(Me.MetroLabel6Lbl, "MetroLabel6Lbl")
        Me.MetroLabel6Lbl.BackColor = System.Drawing.Color.White
        Me.MetroLabel6Lbl.Name = "MetroLabel6Lbl"
        Me.MetroLabel6Lbl.UseCustomBackColor = True
        '
        'MetroLabel7Lbl
        '
        resources.ApplyResources(Me.MetroLabel7Lbl, "MetroLabel7Lbl")
        Me.MetroLabel7Lbl.BackColor = System.Drawing.Color.White
        Me.MetroLabel7Lbl.Name = "MetroLabel7Lbl"
        Me.MetroLabel7Lbl.UseCustomBackColor = True
        '
        'LocationLbl
        '
        resources.ApplyResources(Me.LocationLbl, "LocationLbl")
        Me.LocationLbl.BackColor = System.Drawing.Color.White
        Me.LocationLbl.Name = "LocationLbl"
        Me.LocationLbl.UseCustomBackColor = True
        '
        'DescriptionLbl
        '
        resources.ApplyResources(Me.DescriptionLbl, "DescriptionLbl")
        Me.DescriptionLbl.BackColor = System.Drawing.Color.White
        Me.DescriptionLbl.Name = "DescriptionLbl"
        Me.DescriptionLbl.UseCustomBackColor = True
        '
        'DriverLbl
        '
        resources.ApplyResources(Me.DriverLbl, "DriverLbl")
        Me.DriverLbl.BackColor = System.Drawing.Color.White
        Me.DriverLbl.Name = "DriverLbl"
        Me.DriverLbl.UseCustomBackColor = True
        '
        'DriverVersionLbl
        '
        resources.ApplyResources(Me.DriverVersionLbl, "DriverVersionLbl")
        Me.DriverVersionLbl.BackColor = System.Drawing.Color.White
        Me.DriverVersionLbl.Name = "DriverVersionLbl"
        Me.DriverVersionLbl.UseCustomBackColor = True
        '
        'StateLbl
        '
        resources.ApplyResources(Me.StateLbl, "StateLbl")
        Me.StateLbl.BackColor = System.Drawing.Color.White
        Me.StateLbl.Name = "StateLbl"
        Me.StateLbl.UseCustomBackColor = True
        '
        'DataFileLbl
        '
        Me.DataFileLbl.BackColor = System.Drawing.Color.White
        Me.DataFileLbl.FontSize = MetroFramework.MetroLabelSize.Small
        Me.DataFileLbl.LabelMode = MetroFramework.Controls.MetroLabelMode.Selectable
        resources.ApplyResources(Me.DataFileLbl, "DataFileLbl")
        Me.DataFileLbl.Name = "DataFileLbl"
        Me.DataFileLbl.UseCustomBackColor = True
        '
        'MetroLabel10Lbl
        '
        resources.ApplyResources(Me.MetroLabel10Lbl, "MetroLabel10Lbl")
        Me.MetroLabel10Lbl.BackColor = System.Drawing.Color.White
        Me.MetroLabel10Lbl.FontSize = MetroFramework.MetroLabelSize.Small
        Me.MetroLabel10Lbl.Name = "MetroLabel10Lbl"
        Me.MetroLabel10Lbl.UseCustomBackColor = True
        '
        'ConfigFileLbl
        '
        Me.ConfigFileLbl.BackColor = System.Drawing.Color.White
        Me.ConfigFileLbl.FontSize = MetroFramework.MetroLabelSize.Small
        Me.ConfigFileLbl.LabelMode = MetroFramework.Controls.MetroLabelMode.Selectable
        resources.ApplyResources(Me.ConfigFileLbl, "ConfigFileLbl")
        Me.ConfigFileLbl.Name = "ConfigFileLbl"
        Me.ConfigFileLbl.UseCustomBackColor = True
        '
        'MetroLabel12Lbl
        '
        resources.ApplyResources(Me.MetroLabel12Lbl, "MetroLabel12Lbl")
        Me.MetroLabel12Lbl.BackColor = System.Drawing.Color.White
        Me.MetroLabel12Lbl.FontSize = MetroFramework.MetroLabelSize.Small
        Me.MetroLabel12Lbl.Name = "MetroLabel12Lbl"
        Me.MetroLabel12Lbl.UseCustomBackColor = True
        '
        'DriverPathLbl
        '
        Me.DriverPathLbl.BackColor = System.Drawing.Color.White
        Me.DriverPathLbl.FontSize = MetroFramework.MetroLabelSize.Small
        Me.DriverPathLbl.LabelMode = MetroFramework.Controls.MetroLabelMode.Selectable
        resources.ApplyResources(Me.DriverPathLbl, "DriverPathLbl")
        Me.DriverPathLbl.Name = "DriverPathLbl"
        Me.DriverPathLbl.UseCustomBackColor = True
        '
        'MetroLabel13Lbl
        '
        resources.ApplyResources(Me.MetroLabel13Lbl, "MetroLabel13Lbl")
        Me.MetroLabel13Lbl.BackColor = System.Drawing.Color.White
        Me.MetroLabel13Lbl.FontSize = MetroFramework.MetroLabelSize.Small
        Me.MetroLabel13Lbl.Name = "MetroLabel13Lbl"
        Me.MetroLabel13Lbl.UseCustomBackColor = True
        '
        'MetroButton1
        '
        resources.ApplyResources(Me.MetroButton1, "MetroButton1")
        Me.MetroButton1.Name = "MetroButton1"
        Me.MetroButton1.UseSelectable = True
        '
        'MetroButton2
        '
        resources.ApplyResources(Me.MetroButton2, "MetroButton2")
        Me.MetroButton2.Name = "MetroButton2"
        Me.MetroButton2.UseSelectable = True
        '
        'MetroButton3
        '
        resources.ApplyResources(Me.MetroButton3, "MetroButton3")
        Me.MetroButton3.Name = "MetroButton3"
        Me.MetroButton3.UseSelectable = True
        '
        'MetroButton4
        '
        resources.ApplyResources(Me.MetroButton4, "MetroButton4")
        Me.MetroButton4.Name = "MetroButton4"
        Me.MetroButton4.UseSelectable = True
        '
        'MetroButton5
        '
        resources.ApplyResources(Me.MetroButton5, "MetroButton5")
        Me.MetroButton5.Name = "MetroButton5"
        Me.MetroButton5.UseSelectable = True
        '
        'MetroButton6
        '
        resources.ApplyResources(Me.MetroButton6, "MetroButton6")
        Me.MetroButton6.Name = "MetroButton6"
        Me.MetroButton6.UseSelectable = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.BackColor = System.Drawing.Color.White
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AlsStandarddruckerFestlegenToolStripMenuItem, Me.ToolStripSeparator1, Me.DruckerImProfilSpeichernToolStripMenuItem, Me.ToolStripSeparator2, Me.DruckerEntfernenToolStripMenuItem, Me.DruckerEntfernenlokalToolStripMenuItem, Me.DruckerNeuInstallierenToolStripMenuItem, Me.StandardeinstellungenLöschenToolStripMenuItem, Me.ToolStripSeparator3, Me.DruckereigenschaftenToolStripMenuItem, Me.DruckereinstellungenToolStripMenuItem, Me.ToolStripSeparator4, Me.DruckereinstellungenExportierenToolStripMenuItem, Me.DruckereinstellungenImportierenToolStripMenuItem, Me.ProfildateiErstellenverbindenToolStripMenuItem, Me.ProfildateioErstellenentfernenUndVerbindenToolStripMenuItem, Me.ProfildateiVersendenverbindenToolStripMenuItem, Me.ProfildateiVersendenentfernenUndVerbindenToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        resources.ApplyResources(Me.ContextMenuStrip1, "ContextMenuStrip1")
        '
        'AlsStandarddruckerFestlegenToolStripMenuItem
        '
        Me.AlsStandarddruckerFestlegenToolStripMenuItem.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.dialog_ok
        Me.AlsStandarddruckerFestlegenToolStripMenuItem.Name = "AlsStandarddruckerFestlegenToolStripMenuItem"
        resources.ApplyResources(Me.AlsStandarddruckerFestlegenToolStripMenuItem, "AlsStandarddruckerFestlegenToolStripMenuItem")
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        resources.ApplyResources(Me.ToolStripSeparator1, "ToolStripSeparator1")
        '
        'DruckerImProfilSpeichernToolStripMenuItem
        '
        Me.DruckerImProfilSpeichernToolStripMenuItem.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.floppy_nocustom
        Me.DruckerImProfilSpeichernToolStripMenuItem.Name = "DruckerImProfilSpeichernToolStripMenuItem"
        resources.ApplyResources(Me.DruckerImProfilSpeichernToolStripMenuItem, "DruckerImProfilSpeichernToolStripMenuItem")
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        resources.ApplyResources(Me.ToolStripSeparator2, "ToolStripSeparator2")
        '
        'DruckerEntfernenToolStripMenuItem
        '
        Me.DruckerEntfernenToolStripMenuItem.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.DeletePrinter
        Me.DruckerEntfernenToolStripMenuItem.Name = "DruckerEntfernenToolStripMenuItem"
        resources.ApplyResources(Me.DruckerEntfernenToolStripMenuItem, "DruckerEntfernenToolStripMenuItem")
        '
        'DruckerEntfernenlokalToolStripMenuItem
        '
        Me.DruckerEntfernenlokalToolStripMenuItem.Name = "DruckerEntfernenlokalToolStripMenuItem"
        resources.ApplyResources(Me.DruckerEntfernenlokalToolStripMenuItem, "DruckerEntfernenlokalToolStripMenuItem")
        '
        'DruckerNeuInstallierenToolStripMenuItem
        '
        Me.DruckerNeuInstallierenToolStripMenuItem.Name = "DruckerNeuInstallierenToolStripMenuItem"
        resources.ApplyResources(Me.DruckerNeuInstallierenToolStripMenuItem, "DruckerNeuInstallierenToolStripMenuItem")
        '
        'StandardeinstellungenLöschenToolStripMenuItem
        '
        Me.StandardeinstellungenLöschenToolStripMenuItem.Name = "StandardeinstellungenLöschenToolStripMenuItem"
        resources.ApplyResources(Me.StandardeinstellungenLöschenToolStripMenuItem, "StandardeinstellungenLöschenToolStripMenuItem")
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        resources.ApplyResources(Me.ToolStripSeparator3, "ToolStripSeparator3")
        '
        'DruckereigenschaftenToolStripMenuItem
        '
        Me.DruckereigenschaftenToolStripMenuItem.Name = "DruckereigenschaftenToolStripMenuItem"
        resources.ApplyResources(Me.DruckereigenschaftenToolStripMenuItem, "DruckereigenschaftenToolStripMenuItem")
        '
        'DruckereinstellungenToolStripMenuItem
        '
        Me.DruckereinstellungenToolStripMenuItem.Name = "DruckereinstellungenToolStripMenuItem"
        resources.ApplyResources(Me.DruckereinstellungenToolStripMenuItem, "DruckereinstellungenToolStripMenuItem")
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        resources.ApplyResources(Me.ToolStripSeparator4, "ToolStripSeparator4")
        '
        'DruckereinstellungenExportierenToolStripMenuItem
        '
        Me.DruckereinstellungenExportierenToolStripMenuItem.Name = "DruckereinstellungenExportierenToolStripMenuItem"
        resources.ApplyResources(Me.DruckereinstellungenExportierenToolStripMenuItem, "DruckereinstellungenExportierenToolStripMenuItem")
        '
        'DruckereinstellungenImportierenToolStripMenuItem
        '
        Me.DruckereinstellungenImportierenToolStripMenuItem.Name = "DruckereinstellungenImportierenToolStripMenuItem"
        resources.ApplyResources(Me.DruckereinstellungenImportierenToolStripMenuItem, "DruckereinstellungenImportierenToolStripMenuItem")
        '
        'ProfildateiErstellenverbindenToolStripMenuItem
        '
        Me.ProfildateiErstellenverbindenToolStripMenuItem.Name = "ProfildateiErstellenverbindenToolStripMenuItem"
        resources.ApplyResources(Me.ProfildateiErstellenverbindenToolStripMenuItem, "ProfildateiErstellenverbindenToolStripMenuItem")
        '
        'ProfildateioErstellenentfernenUndVerbindenToolStripMenuItem
        '
        Me.ProfildateioErstellenentfernenUndVerbindenToolStripMenuItem.Name = "ProfildateioErstellenentfernenUndVerbindenToolStripMenuItem"
        resources.ApplyResources(Me.ProfildateioErstellenentfernenUndVerbindenToolStripMenuItem, "ProfildateioErstellenentfernenUndVerbindenToolStripMenuItem")
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox2.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.expandable
        resources.ApplyResources(Me.PictureBox2, "PictureBox2")
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox2, resources.GetString("PictureBox2.ToolTip"))
        '
        'Button1
        '
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.DeletePrinter2
        Me.Button1.Name = "Button1"
        Me.ToolTip1.SetToolTip(Me.Button1, resources.GetString("Button1.ToolTip"))
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        resources.ApplyResources(Me.Button2, "Button2")
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.settings_small
        Me.Button2.Name = "Button2"
        Me.ToolTip1.SetToolTip(Me.Button2, resources.GetString("Button2.ToolTip"))
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
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.DefaultExt = "dat"
        resources.ApplyResources(Me.SaveFileDialog1, "SaveFileDialog1")
        Me.SaveFileDialog1.RestoreDirectory = True
        '
        'OpenFileDialog1
        '
        resources.ApplyResources(Me.OpenFileDialog1, "OpenFileDialog1")
        Me.OpenFileDialog1.RestoreDirectory = True
        '
        'SaveFileDialog2
        '
        Me.SaveFileDialog2.DefaultExt = "prpr"
        resources.ApplyResources(Me.SaveFileDialog2, "SaveFileDialog2")
        '
        'ProfildateiVersendenverbindenToolStripMenuItem
        '
        Me.ProfildateiVersendenverbindenToolStripMenuItem.Name = "ProfildateiVersendenverbindenToolStripMenuItem"
        resources.ApplyResources(Me.ProfildateiVersendenverbindenToolStripMenuItem, "ProfildateiVersendenverbindenToolStripMenuItem")
        '
        'ProfildateiVersendenentfernenUndVerbindenToolStripMenuItem
        '
        Me.ProfildateiVersendenentfernenUndVerbindenToolStripMenuItem.Name = "ProfildateiVersendenentfernenUndVerbindenToolStripMenuItem"
        resources.ApplyResources(Me.ProfildateiVersendenentfernenUndVerbindenToolStripMenuItem, "ProfildateiVersendenentfernenUndVerbindenToolStripMenuItem")
        '
        'PrinterCtl
        '
        resources.ApplyResources(Me, "$this")
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
        Me.Name = "PrinterCtl"
        Me.UseCustomBackColor = True
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
    Friend WithEvents DruckerEntfernenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents DruckereigenschaftenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DruckereinstellungenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents DruckerNeuInstallierenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DruckerEntfernenlokalToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProfildateiErstellenverbindenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProfildateioErstellenentfernenUndVerbindenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveFileDialog2 As SaveFileDialog
    Friend WithEvents ProfildateiVersendenverbindenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProfildateiVersendenentfernenUndVerbindenToolStripMenuItem As ToolStripMenuItem
End Class
