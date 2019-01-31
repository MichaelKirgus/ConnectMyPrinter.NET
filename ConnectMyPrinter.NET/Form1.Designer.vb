Imports MetroFramework

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.LoadAllPrintersAsync = New System.ComponentModel.BackgroundWorker()
        Me.ComboBox1 = New MetroFramework.Controls.MetroComboBox()
        Me.MetroLabel1 = New MetroFramework.Controls.MetroLabel()
        Me.MetroButton1 = New MetroFramework.Controls.MetroButton()
        Me.MetroTextBox1 = New MetroFramework.Controls.MetroTextBox()
        Me.MetroLabel2 = New MetroFramework.Controls.MetroLabel()
        Me.MetroProgressSpinner1 = New MetroFramework.Controls.MetroProgressSpinner()
        Me.MetroCheckBox1 = New MetroFramework.Controls.MetroCheckBox()
        Me.MetroLabel3 = New MetroFramework.Controls.MetroLabel()
        Me.MetroTabControl1 = New MetroFramework.Controls.MetroTabControl()
        Me.MetroTabPage1 = New MetroFramework.Controls.MetroTabPage()
        Me.MetroPanel1 = New MetroFramework.Controls.MetroPanel()
        Me.MetroProgressSpinner2 = New MetroFramework.Controls.MetroProgressSpinner()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.MetroTabPage2 = New MetroFramework.Controls.MetroTabPage()
        Me.MetroPanel2 = New MetroFramework.Controls.MetroPanel()
        Me.MetroLabel4 = New MetroFramework.Controls.MetroLabel()
        Me.MetroProgressSpinner3 = New MetroFramework.Controls.MetroProgressSpinner()
        Me.FlowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel()
        Me.LoadAllLocalPrinters = New System.ComponentModel.BackgroundWorker()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DruckerTreiberTreiberpaketEntfernenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DruckertreiberTreiberpaketEntfernenAdminToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WindowsDruckverwaltungÖffnenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WindowsDruckverwaltungÖffnenAdminToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AnwendungseinstellungenBearbeitenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AnwendungseinstellungenBearbeitenAdminToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.LocalPrinterIdleWorker = New System.ComponentModel.BackgroundWorker()
        Me.LocalPrinterChangeTimer = New System.Windows.Forms.Timer(Me.components)
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.ToolTip2 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Button5 = New System.Windows.Forms.Button()
        Me.RestartPrinterService = New System.ComponentModel.BackgroundWorker()
        Me.AdditionalInfoRTF = New System.Windows.Forms.RichTextBox()
        Me.MetroLabel5 = New MetroFramework.Controls.MetroLabel()
        Me.MetroTabControl1.SuspendLayout()
        Me.MetroTabPage1.SuspendLayout()
        Me.MetroPanel1.SuspendLayout()
        Me.MetroTabPage2.SuspendLayout()
        Me.MetroPanel2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LoadAllPrintersAsync
        '
        Me.LoadAllPrintersAsync.WorkerReportsProgress = True
        Me.LoadAllPrintersAsync.WorkerSupportsCancellation = True
        '
        'ComboBox1
        '
        Me.ComboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.ComboBox1.FormattingEnabled = True
        resources.ApplyResources(Me.ComboBox1, "ComboBox1")
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Style = MetroFramework.MetroColorStyle.Black
        '
        'MetroLabel1
        '
        Me.MetroLabel1.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.MetroLabel1, "MetroLabel1")
        Me.MetroLabel1.Name = "MetroLabel1"
        '
        'MetroButton1
        '
        resources.ApplyResources(Me.MetroButton1, "MetroButton1")
        Me.MetroButton1.Name = "MetroButton1"
        Me.ToolTip2.SetToolTip(Me.MetroButton1, resources.GetString("MetroButton1.ToolTip"))
        '
        'MetroTextBox1
        '
        Me.MetroTextBox1.FontSize = MetroFramework.MetroTextBoxSize.Medium
        resources.ApplyResources(Me.MetroTextBox1, "MetroTextBox1")
        Me.MetroTextBox1.Name = "MetroTextBox1"
        Me.MetroTextBox1.PromptText = "Druckerbezeichnung eingeben..."
        Me.MetroTextBox1.Style = MetroFramework.MetroColorStyle.Black
        Me.MetroTextBox1.TabIndex = 0
        Me.ToolTip2.SetToolTip(Me.MetroTextBox1, resources.GetString("MetroTextBox1.ToolTip"))
        '
        'MetroLabel2
        '
        resources.ApplyResources(Me.MetroLabel2, "MetroLabel2")
        Me.MetroLabel2.FontSize = MetroFramework.MetroLabelSize.Small
        Me.MetroLabel2.Name = "MetroLabel2"
        '
        'MetroProgressSpinner1
        '
        resources.ApplyResources(Me.MetroProgressSpinner1, "MetroProgressSpinner1")
        Me.MetroProgressSpinner1.Maximum = 100
        Me.MetroProgressSpinner1.Name = "MetroProgressSpinner1"
        Me.MetroProgressSpinner1.Style = MetroFramework.MetroColorStyle.Black
        '
        'MetroCheckBox1
        '
        resources.ApplyResources(Me.MetroCheckBox1, "MetroCheckBox1")
        Me.MetroCheckBox1.Name = "MetroCheckBox1"
        Me.MetroCheckBox1.Style = MetroFramework.MetroColorStyle.Black
        Me.ToolTip2.SetToolTip(Me.MetroCheckBox1, resources.GetString("MetroCheckBox1.ToolTip"))
        '
        'MetroLabel3
        '
        Me.MetroLabel3.FontSize = MetroFramework.MetroLabelSize.Small
        resources.ApplyResources(Me.MetroLabel3, "MetroLabel3")
        Me.MetroLabel3.Name = "MetroLabel3"
        '
        'MetroTabControl1
        '
        resources.ApplyResources(Me.MetroTabControl1, "MetroTabControl1")
        Me.MetroTabControl1.Controls.Add(Me.MetroTabPage1)
        Me.MetroTabControl1.Controls.Add(Me.MetroTabPage2)
        Me.MetroTabControl1.HotTrack = True
        Me.MetroTabControl1.Name = "MetroTabControl1"
        Me.MetroTabControl1.SelectedIndex = 0
        Me.MetroTabControl1.Style = MetroFramework.MetroColorStyle.Black
        Me.MetroTabControl1.UseStyleColors = True
        '
        'MetroTabPage1
        '
        Me.MetroTabPage1.BackColor = System.Drawing.Color.White
        Me.MetroTabPage1.Controls.Add(Me.MetroPanel1)
        Me.MetroTabPage1.HorizontalScrollbarBarColor = True
        Me.MetroTabPage1.HorizontalScrollbarSize = 0
        resources.ApplyResources(Me.MetroTabPage1, "MetroTabPage1")
        Me.MetroTabPage1.Name = "MetroTabPage1"
        Me.MetroTabPage1.Style = MetroFramework.MetroColorStyle.White
        Me.MetroTabPage1.Theme = MetroFramework.MetroThemeStyle.Light
        Me.MetroTabPage1.VerticalScrollbarBarColor = True
        Me.MetroTabPage1.VerticalScrollbarSize = 0
        '
        'MetroPanel1
        '
        Me.MetroPanel1.Controls.Add(Me.MetroProgressSpinner2)
        Me.MetroPanel1.Controls.Add(Me.FlowLayoutPanel1)
        resources.ApplyResources(Me.MetroPanel1, "MetroPanel1")
        Me.MetroPanel1.HorizontalScrollbarBarColor = False
        Me.MetroPanel1.HorizontalScrollbarHighlightOnWheel = False
        Me.MetroPanel1.HorizontalScrollbarSize = 10
        Me.MetroPanel1.Name = "MetroPanel1"
        Me.MetroPanel1.VerticalScrollbarBarColor = True
        Me.MetroPanel1.VerticalScrollbarHighlightOnWheel = False
        Me.MetroPanel1.VerticalScrollbarSize = 10
        '
        'MetroProgressSpinner2
        '
        resources.ApplyResources(Me.MetroProgressSpinner2, "MetroProgressSpinner2")
        Me.MetroProgressSpinner2.Maximum = 100
        Me.MetroProgressSpinner2.Name = "MetroProgressSpinner2"
        Me.MetroProgressSpinner2.Style = MetroFramework.MetroColorStyle.Black
        '
        'FlowLayoutPanel1
        '
        resources.ApplyResources(Me.FlowLayoutPanel1, "FlowLayoutPanel1")
        Me.FlowLayoutPanel1.BackColor = System.Drawing.Color.White
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        '
        'MetroTabPage2
        '
        Me.MetroTabPage2.BackColor = System.Drawing.Color.White
        Me.MetroTabPage2.Controls.Add(Me.MetroPanel2)
        Me.MetroTabPage2.HorizontalScrollbarBarColor = True
        Me.MetroTabPage2.HorizontalScrollbarSize = 0
        resources.ApplyResources(Me.MetroTabPage2, "MetroTabPage2")
        Me.MetroTabPage2.Name = "MetroTabPage2"
        Me.MetroTabPage2.VerticalScrollbarBarColor = True
        Me.MetroTabPage2.VerticalScrollbarSize = 0
        '
        'MetroPanel2
        '
        Me.MetroPanel2.Controls.Add(Me.MetroLabel4)
        Me.MetroPanel2.Controls.Add(Me.MetroProgressSpinner3)
        Me.MetroPanel2.Controls.Add(Me.FlowLayoutPanel2)
        resources.ApplyResources(Me.MetroPanel2, "MetroPanel2")
        Me.MetroPanel2.HorizontalScrollbarBarColor = False
        Me.MetroPanel2.HorizontalScrollbarHighlightOnWheel = False
        Me.MetroPanel2.HorizontalScrollbarSize = 10
        Me.MetroPanel2.Name = "MetroPanel2"
        Me.MetroPanel2.VerticalScrollbarBarColor = True
        Me.MetroPanel2.VerticalScrollbarHighlightOnWheel = False
        Me.MetroPanel2.VerticalScrollbarSize = 10
        '
        'MetroLabel4
        '
        resources.ApplyResources(Me.MetroLabel4, "MetroLabel4")
        Me.MetroLabel4.Name = "MetroLabel4"
        '
        'MetroProgressSpinner3
        '
        resources.ApplyResources(Me.MetroProgressSpinner3, "MetroProgressSpinner3")
        Me.MetroProgressSpinner3.Maximum = 100
        Me.MetroProgressSpinner3.Name = "MetroProgressSpinner3"
        Me.MetroProgressSpinner3.Speed = 2.0!
        Me.MetroProgressSpinner3.Style = MetroFramework.MetroColorStyle.Black
        '
        'FlowLayoutPanel2
        '
        resources.ApplyResources(Me.FlowLayoutPanel2, "FlowLayoutPanel2")
        Me.FlowLayoutPanel2.BackColor = System.Drawing.Color.White
        Me.FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        '
        'LoadAllLocalPrinters
        '
        Me.LoadAllLocalPrinters.WorkerReportsProgress = True
        Me.LoadAllLocalPrinters.WorkerSupportsCancellation = True
        '
        'PictureBox2
        '
        Me.PictureBox2.ContextMenuStrip = Me.ContextMenuStrip1
        resources.ApplyResources(Me.PictureBox2, "PictureBox2")
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.TabStop = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.BackColor = System.Drawing.Color.White
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DruckerTreiberTreiberpaketEntfernenToolStripMenuItem, Me.DruckertreiberTreiberpaketEntfernenAdminToolStripMenuItem, Me.WindowsDruckverwaltungÖffnenToolStripMenuItem, Me.WindowsDruckverwaltungÖffnenAdminToolStripMenuItem, Me.AnwendungseinstellungenBearbeitenToolStripMenuItem, Me.AnwendungseinstellungenBearbeitenAdminToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ContextMenuStrip1.ShowImageMargin = False
        resources.ApplyResources(Me.ContextMenuStrip1, "ContextMenuStrip1")
        '
        'DruckerTreiberTreiberpaketEntfernenToolStripMenuItem
        '
        Me.DruckerTreiberTreiberpaketEntfernenToolStripMenuItem.Name = "DruckerTreiberTreiberpaketEntfernenToolStripMenuItem"
        resources.ApplyResources(Me.DruckerTreiberTreiberpaketEntfernenToolStripMenuItem, "DruckerTreiberTreiberpaketEntfernenToolStripMenuItem")
        '
        'DruckertreiberTreiberpaketEntfernenAdminToolStripMenuItem
        '
        Me.DruckertreiberTreiberpaketEntfernenAdminToolStripMenuItem.Name = "DruckertreiberTreiberpaketEntfernenAdminToolStripMenuItem"
        resources.ApplyResources(Me.DruckertreiberTreiberpaketEntfernenAdminToolStripMenuItem, "DruckertreiberTreiberpaketEntfernenAdminToolStripMenuItem")
        '
        'WindowsDruckverwaltungÖffnenToolStripMenuItem
        '
        Me.WindowsDruckverwaltungÖffnenToolStripMenuItem.Name = "WindowsDruckverwaltungÖffnenToolStripMenuItem"
        resources.ApplyResources(Me.WindowsDruckverwaltungÖffnenToolStripMenuItem, "WindowsDruckverwaltungÖffnenToolStripMenuItem")
        '
        'WindowsDruckverwaltungÖffnenAdminToolStripMenuItem
        '
        Me.WindowsDruckverwaltungÖffnenAdminToolStripMenuItem.Name = "WindowsDruckverwaltungÖffnenAdminToolStripMenuItem"
        resources.ApplyResources(Me.WindowsDruckverwaltungÖffnenAdminToolStripMenuItem, "WindowsDruckverwaltungÖffnenAdminToolStripMenuItem")
        '
        'AnwendungseinstellungenBearbeitenToolStripMenuItem
        '
        Me.AnwendungseinstellungenBearbeitenToolStripMenuItem.Name = "AnwendungseinstellungenBearbeitenToolStripMenuItem"
        resources.ApplyResources(Me.AnwendungseinstellungenBearbeitenToolStripMenuItem, "AnwendungseinstellungenBearbeitenToolStripMenuItem")
        '
        'AnwendungseinstellungenBearbeitenAdminToolStripMenuItem
        '
        Me.AnwendungseinstellungenBearbeitenAdminToolStripMenuItem.Name = "AnwendungseinstellungenBearbeitenAdminToolStripMenuItem"
        resources.ApplyResources(Me.AnwendungseinstellungenBearbeitenAdminToolStripMenuItem, "AnwendungseinstellungenBearbeitenAdminToolStripMenuItem")
        '
        'Button2
        '
        resources.ApplyResources(Me.Button2, "Button2")
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.settings_small
        Me.Button2.Name = "Button2"
        Me.ToolTip2.SetToolTip(Me.Button2, resources.GetString("Button2.ToolTip"))
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.ForeColor = System.Drawing.Color.Transparent
        Me.Button1.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.info_small
        Me.Button1.Name = "Button1"
        Me.ToolTip2.SetToolTip(Me.Button1, resources.GetString("Button1.ToolTip"))
        Me.Button1.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.dialog_ok_3
        resources.ApplyResources(Me.PictureBox1, "PictureBox1")
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.TabStop = False
        '
        'Button3
        '
        resources.ApplyResources(Me.Button3, "Button3")
        Me.Button3.ForeColor = System.Drawing.Color.White
        Me.Button3.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.refresh16
        Me.Button3.Name = "Button3"
        Me.ToolTip2.SetToolTip(Me.Button3, resources.GetString("Button3.ToolTip"))
        Me.Button3.UseVisualStyleBackColor = True
        '
        'LocalPrinterIdleWorker
        '
        '
        'LocalPrinterChangeTimer
        '
        Me.LocalPrinterChangeTimer.Enabled = True
        Me.LocalPrinterChangeTimer.Interval = 2000
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox3.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.expandable
        resources.ApplyResources(Me.PictureBox3, "PictureBox3")
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.TabStop = False
        '
        'Button4
        '
        resources.ApplyResources(Me.Button4, "Button4")
        Me.Button4.ForeColor = System.Drawing.Color.Transparent
        Me.Button4.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.restart_printerq
        Me.Button4.Name = "Button4"
        Me.ToolTip2.SetToolTip(Me.Button4, resources.GetString("Button4.ToolTip"))
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        resources.ApplyResources(Me.Button5, "Button5")
        Me.Button5.ForeColor = System.Drawing.Color.White
        Me.Button5.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.Find_VS
        Me.Button5.Name = "Button5"
        Me.ToolTip2.SetToolTip(Me.Button5, resources.GetString("Button5.ToolTip"))
        Me.Button5.UseVisualStyleBackColor = True
        '
        'RestartPrinterService
        '
        Me.RestartPrinterService.WorkerReportsProgress = True
        Me.RestartPrinterService.WorkerSupportsCancellation = True
        '
        'AdditionalInfoRTF
        '
        resources.ApplyResources(Me.AdditionalInfoRTF, "AdditionalInfoRTF")
        Me.AdditionalInfoRTF.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.AdditionalInfoRTF.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.AdditionalInfoRTF.Name = "AdditionalInfoRTF"
        '
        'MetroLabel5
        '
        Me.MetroLabel5.BackColor = System.Drawing.SystemColors.Control
        Me.MetroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Regular
        resources.ApplyResources(Me.MetroLabel5, "MetroLabel5")
        Me.MetroLabel5.Name = "MetroLabel5"
        '
        'Form1
        '
        Me.AcceptButton = Me.MetroButton1
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = Forms.MetroFormBorderStyle.FixedSingle
        Me.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Controls.Add(Me.MetroLabel5)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.MetroTabControl1)
        Me.Controls.Add(Me.MetroLabel3)
        Me.Controls.Add(Me.MetroLabel1)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.MetroCheckBox1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.MetroProgressSpinner1)
        Me.Controls.Add(Me.MetroLabel2)
        Me.Controls.Add(Me.MetroTextBox1)
        Me.Controls.Add(Me.MetroButton1)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.AdditionalInfoRTF)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Style = MetroFramework.MetroColorStyle.Black
        Me.Theme = MetroFramework.MetroThemeStyle.Light
        Me.MetroTabControl1.ResumeLayout(False)
        Me.MetroTabPage1.ResumeLayout(False)
        Me.MetroPanel1.ResumeLayout(False)
        Me.MetroTabPage2.ResumeLayout(False)
        Me.MetroPanel2.ResumeLayout(False)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboBox1 As MetroFramework.Controls.MetroComboBox
    Friend WithEvents MetroLabel1 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroButton1 As MetroFramework.Controls.MetroButton
    Friend WithEvents MetroTextBox1 As MetroFramework.Controls.MetroTextBox
    Friend WithEvents MetroLabel2 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroProgressSpinner1 As MetroFramework.Controls.MetroProgressSpinner
    Friend WithEvents PictureBox1 As PictureBox
    Public WithEvents LoadAllPrintersAsync As System.ComponentModel.BackgroundWorker
    Friend WithEvents MetroCheckBox1 As MetroFramework.Controls.MetroCheckBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents MetroLabel3 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroTabControl1 As MetroFramework.Controls.MetroTabControl
    Friend WithEvents MetroTabPage1 As Controls.MetroTabPage
    Friend WithEvents MetroTabPage2 As Controls.MetroTabPage
    Friend WithEvents MetroProgressSpinner2 As Controls.MetroProgressSpinner
    Friend WithEvents LoadAllLocalPrinters As System.ComponentModel.BackgroundWorker
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents MetroPanel1 As Controls.MetroPanel
    Friend WithEvents Button3 As Button
    Friend WithEvents MetroPanel2 As Controls.MetroPanel
    Friend WithEvents FlowLayoutPanel2 As FlowLayoutPanel
    Friend WithEvents MetroProgressSpinner3 As Controls.MetroProgressSpinner
    Friend WithEvents MetroLabel4 As Controls.MetroLabel
    Friend WithEvents LocalPrinterIdleWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents LocalPrinterChangeTimer As Timer
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents Button4 As Button
    Friend WithEvents ToolTip2 As ToolTip
    Friend WithEvents RestartPrinterService As System.ComponentModel.BackgroundWorker
    Friend WithEvents Button5 As Button
    Friend WithEvents AdditionalInfoRTF As RichTextBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents DruckerTreiberTreiberpaketEntfernenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DruckertreiberTreiberpaketEntfernenAdminToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents WindowsDruckverwaltungÖffnenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents WindowsDruckverwaltungÖffnenAdminToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AnwendungseinstellungenBearbeitenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AnwendungseinstellungenBearbeitenAdminToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MetroLabel5 As Controls.MetroLabel
End Class
