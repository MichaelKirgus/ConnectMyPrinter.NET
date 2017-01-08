<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.LocalPrinterIdleWorker = New System.ComponentModel.BackgroundWorker()
        Me.LocalPrinterChangeTimer = New System.Windows.Forms.Timer(Me.components)
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MetroTabControl1.SuspendLayout()
        Me.MetroTabPage1.SuspendLayout()
        Me.MetroPanel1.SuspendLayout()
        Me.MetroTabPage2.SuspendLayout()
        Me.MetroPanel2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ComboBox1.ItemHeight = 23
        Me.ComboBox1.Location = New System.Drawing.Point(23, 93)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(298, 29)
        Me.ComboBox1.Style = MetroFramework.MetroColorStyle.Black
        Me.ComboBox1.TabIndex = 0
        '
        'MetroLabel1
        '
        Me.MetroLabel1.BackColor = System.Drawing.Color.Transparent
        Me.MetroLabel1.Location = New System.Drawing.Point(23, 52)
        Me.MetroLabel1.Name = "MetroLabel1"
        Me.MetroLabel1.Size = New System.Drawing.Size(298, 19)
        Me.MetroLabel1.TabIndex = 1
        Me.MetroLabel1.Text = "Bitte geben Sie einen Druckernamen ein."
        '
        'MetroButton1
        '
        Me.MetroButton1.Enabled = False
        Me.MetroButton1.Location = New System.Drawing.Point(334, 93)
        Me.MetroButton1.Name = "MetroButton1"
        Me.MetroButton1.Size = New System.Drawing.Size(118, 29)
        Me.MetroButton1.TabIndex = 3
        Me.MetroButton1.Text = "Drucker verbinden"
        Me.ToolTip1.SetToolTip(Me.MetroButton1, "Verbindet einen Drucker.")
        '
        'MetroTextBox1
        '
        Me.MetroTextBox1.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.MetroTextBox1.Location = New System.Drawing.Point(23, 93)
        Me.MetroTextBox1.Name = "MetroTextBox1"
        Me.MetroTextBox1.PromptText = "Druckerbezeichnung eingeben..."
        Me.MetroTextBox1.Size = New System.Drawing.Size(304, 29)
        Me.MetroTextBox1.Style = MetroFramework.MetroColorStyle.Black
        Me.MetroTextBox1.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.MetroTextBox1, "Geben Sie hier den Namen des Druckers an. Wenn genügend Zeichen eingegeben worden" &
        " sind, dann werden Vorschläge angezeigt.")
        '
        'MetroLabel2
        '
        Me.MetroLabel2.AutoSize = True
        Me.MetroLabel2.FontSize = MetroFramework.MetroLabelSize.Small
        Me.MetroLabel2.Location = New System.Drawing.Point(49, 127)
        Me.MetroLabel2.Name = "MetroLabel2"
        Me.MetroLabel2.Size = New System.Drawing.Size(184, 15)
        Me.MetroLabel2.TabIndex = 5
        Me.MetroLabel2.Text = "Suche nach Druckern im Netzwerk..."
        '
        'MetroProgressSpinner1
        '
        Me.MetroProgressSpinner1.Location = New System.Drawing.Point(23, 124)
        Me.MetroProgressSpinner1.Maximum = 100
        Me.MetroProgressSpinner1.Name = "MetroProgressSpinner1"
        Me.MetroProgressSpinner1.Size = New System.Drawing.Size(20, 20)
        Me.MetroProgressSpinner1.Style = MetroFramework.MetroColorStyle.Black
        Me.MetroProgressSpinner1.TabIndex = 6
        '
        'MetroCheckBox1
        '
        Me.MetroCheckBox1.AutoSize = True
        Me.MetroCheckBox1.Location = New System.Drawing.Point(334, 127)
        Me.MetroCheckBox1.Name = "MetroCheckBox1"
        Me.MetroCheckBox1.Size = New System.Drawing.Size(110, 15)
        Me.MetroCheckBox1.Style = MetroFramework.MetroColorStyle.Black
        Me.MetroCheckBox1.TabIndex = 8
        Me.MetroCheckBox1.Text = "Standarddrucker"
        Me.ToolTip1.SetToolTip(Me.MetroCheckBox1, "Aktivieren Sie diese Option, um den neuen Drucker auch als Standarddrucker festzu" &
        "legen.")
        Me.MetroCheckBox1.UseVisualStyleBackColor = True
        '
        'MetroLabel3
        '
        Me.MetroLabel3.FontSize = MetroFramework.MetroLabelSize.Small
        Me.MetroLabel3.Location = New System.Drawing.Point(24, 71)
        Me.MetroLabel3.Name = "MetroLabel3"
        Me.MetroLabel3.Size = New System.Drawing.Size(298, 15)
        Me.MetroLabel3.TabIndex = 12
        '
        'MetroTabControl1
        '
        Me.MetroTabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MetroTabControl1.Controls.Add(Me.MetroTabPage1)
        Me.MetroTabControl1.Controls.Add(Me.MetroTabPage2)
        Me.MetroTabControl1.HotTrack = True
        Me.MetroTabControl1.Location = New System.Drawing.Point(23, 164)
        Me.MetroTabControl1.Name = "MetroTabControl1"
        Me.MetroTabControl1.SelectedIndex = 0
        Me.MetroTabControl1.Size = New System.Drawing.Size(429, 438)
        Me.MetroTabControl1.Style = MetroFramework.MetroColorStyle.Black
        Me.MetroTabControl1.TabIndex = 14
        Me.MetroTabControl1.UseStyleColors = True
        '
        'MetroTabPage1
        '
        Me.MetroTabPage1.BackColor = System.Drawing.Color.White
        Me.MetroTabPage1.Controls.Add(Me.MetroPanel1)
        Me.MetroTabPage1.HorizontalScrollbarBarColor = True
        Me.MetroTabPage1.HorizontalScrollbarSize = 0
        Me.MetroTabPage1.Location = New System.Drawing.Point(4, 35)
        Me.MetroTabPage1.Name = "MetroTabPage1"
        Me.MetroTabPage1.Size = New System.Drawing.Size(421, 399)
        Me.MetroTabPage1.Style = MetroFramework.MetroColorStyle.White
        Me.MetroTabPage1.TabIndex = 0
        Me.MetroTabPage1.Text = "Vorhandene Drucker"
        Me.MetroTabPage1.Theme = MetroFramework.MetroThemeStyle.Light
        Me.MetroTabPage1.VerticalScrollbarBarColor = True
        Me.MetroTabPage1.VerticalScrollbarSize = 0
        '
        'MetroPanel1
        '
        Me.MetroPanel1.Controls.Add(Me.MetroProgressSpinner2)
        Me.MetroPanel1.Controls.Add(Me.FlowLayoutPanel1)
        Me.MetroPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MetroPanel1.HorizontalScrollbarBarColor = False
        Me.MetroPanel1.HorizontalScrollbarHighlightOnWheel = False
        Me.MetroPanel1.HorizontalScrollbarSize = 10
        Me.MetroPanel1.Location = New System.Drawing.Point(0, 0)
        Me.MetroPanel1.Name = "MetroPanel1"
        Me.MetroPanel1.Size = New System.Drawing.Size(421, 399)
        Me.MetroPanel1.TabIndex = 5
        Me.MetroPanel1.VerticalScrollbarBarColor = True
        Me.MetroPanel1.VerticalScrollbarHighlightOnWheel = False
        Me.MetroPanel1.VerticalScrollbarSize = 10
        '
        'MetroProgressSpinner2
        '
        Me.MetroProgressSpinner2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MetroProgressSpinner2.Location = New System.Drawing.Point(150, 139)
        Me.MetroProgressSpinner2.Margin = New System.Windows.Forms.Padding(0)
        Me.MetroProgressSpinner2.Maximum = 100
        Me.MetroProgressSpinner2.Name = "MetroProgressSpinner2"
        Me.MetroProgressSpinner2.Size = New System.Drawing.Size(120, 120)
        Me.MetroProgressSpinner2.Style = MetroFramework.MetroColorStyle.Black
        Me.MetroProgressSpinner2.TabIndex = 3
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FlowLayoutPanel1.AutoScroll = True
        Me.FlowLayoutPanel1.BackColor = System.Drawing.Color.White
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(415, 393)
        Me.FlowLayoutPanel1.TabIndex = 4
        '
        'MetroTabPage2
        '
        Me.MetroTabPage2.BackColor = System.Drawing.Color.White
        Me.MetroTabPage2.Controls.Add(Me.MetroPanel2)
        Me.MetroTabPage2.HorizontalScrollbarBarColor = True
        Me.MetroTabPage2.HorizontalScrollbarSize = 0
        Me.MetroTabPage2.Location = New System.Drawing.Point(4, 35)
        Me.MetroTabPage2.Name = "MetroTabPage2"
        Me.MetroTabPage2.Size = New System.Drawing.Size(421, 399)
        Me.MetroTabPage2.TabIndex = 1
        Me.MetroTabPage2.Text = "Gespeicherte Drucker"
        Me.MetroTabPage2.VerticalScrollbarBarColor = True
        Me.MetroTabPage2.VerticalScrollbarSize = 0
        '
        'MetroPanel2
        '
        Me.MetroPanel2.Controls.Add(Me.MetroLabel4)
        Me.MetroPanel2.Controls.Add(Me.MetroProgressSpinner3)
        Me.MetroPanel2.Controls.Add(Me.FlowLayoutPanel2)
        Me.MetroPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MetroPanel2.HorizontalScrollbarBarColor = False
        Me.MetroPanel2.HorizontalScrollbarHighlightOnWheel = False
        Me.MetroPanel2.HorizontalScrollbarSize = 10
        Me.MetroPanel2.Location = New System.Drawing.Point(0, 0)
        Me.MetroPanel2.Name = "MetroPanel2"
        Me.MetroPanel2.Size = New System.Drawing.Size(421, 399)
        Me.MetroPanel2.TabIndex = 6
        Me.MetroPanel2.VerticalScrollbarBarColor = True
        Me.MetroPanel2.VerticalScrollbarHighlightOnWheel = False
        Me.MetroPanel2.VerticalScrollbarSize = 10
        '
        'MetroLabel4
        '
        Me.MetroLabel4.Location = New System.Drawing.Point(3, 3)
        Me.MetroLabel4.Name = "MetroLabel4"
        Me.MetroLabel4.Size = New System.Drawing.Size(414, 386)
        Me.MetroLabel4.TabIndex = 4
        Me.MetroLabel4.Text = "Sie haben keine Drucker gespeichert."
        '
        'MetroProgressSpinner3
        '
        Me.MetroProgressSpinner3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MetroProgressSpinner3.Location = New System.Drawing.Point(0, 0)
        Me.MetroProgressSpinner3.Margin = New System.Windows.Forms.Padding(0)
        Me.MetroProgressSpinner3.Maximum = 100
        Me.MetroProgressSpinner3.Name = "MetroProgressSpinner3"
        Me.MetroProgressSpinner3.Size = New System.Drawing.Size(408, 338)
        Me.MetroProgressSpinner3.Speed = 2.0!
        Me.MetroProgressSpinner3.Style = MetroFramework.MetroColorStyle.Black
        Me.MetroProgressSpinner3.TabIndex = 3
        '
        'FlowLayoutPanel2
        '
        Me.FlowLayoutPanel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FlowLayoutPanel2.AutoScroll = True
        Me.FlowLayoutPanel2.BackColor = System.Drawing.Color.White
        Me.FlowLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        Me.FlowLayoutPanel2.Size = New System.Drawing.Size(415, 393)
        Me.FlowLayoutPanel2.TabIndex = 4
        '
        'LoadAllLocalPrinters
        '
        Me.LoadAllLocalPrinters.WorkerReportsProgress = True
        Me.LoadAllLocalPrinters.WorkerSupportsCancellation = True
        '
        'PictureBox2
        '
        Me.PictureBox2.Location = New System.Drawing.Point(334, 43)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(118, 44)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 11
        Me.PictureBox2.TabStop = False
        '
        'Button2
        '
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.settings_small
        Me.Button2.Location = New System.Drawing.Point(389, 5)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(24, 21)
        Me.Button2.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.Button2, "Anwendungseinstellungen/Administratoreinstellungen")
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ForeColor = System.Drawing.Color.Transparent
        Me.Button1.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.info_small
        Me.Button1.Location = New System.Drawing.Point(367, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(24, 21)
        Me.Button1.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.Button1, "Anwendungsinformationen/Lizenz anzeigen")
        Me.Button1.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.dialog_ok_3
        Me.PictureBox1.Location = New System.Drawing.Point(23, 124)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(20, 20)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        Me.PictureBox1.Visible = False
        '
        'Button3
        '
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.ForeColor = System.Drawing.Color.White
        Me.Button3.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.refresh16
        Me.Button3.Location = New System.Drawing.Point(423, 173)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(22, 21)
        Me.Button3.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.Button3, "Ansicht aktualisieren")
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
        Me.PictureBox3.Location = New System.Drawing.Point(298, 94)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(28, 27)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 16
        Me.PictureBox3.TabStop = False
        Me.PictureBox3.Visible = False
        '
        'Button4
        '
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.ForeColor = System.Drawing.Color.Transparent
        Me.Button4.Image = Global.ConnectMyPrinter.NET.My.Resources.Resources.restart_printerq
        Me.Button4.Location = New System.Drawing.Point(346, 5)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(24, 21)
        Me.Button4.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.Button4, "Druckerwarteschlange neu starten")
        Me.Button4.UseVisualStyleBackColor = True
        Me.Button4.Visible = False
        '
        'Form1
        '
        Me.AcceptButton = Me.MetroButton1
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle
        Me.ClientSize = New System.Drawing.Size(470, 620)
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
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(-800, -800)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(470, 1800)
        Me.MinimumSize = New System.Drawing.Size(470, 613)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Style = MetroFramework.MetroColorStyle.Black
        Me.Text = "Netzwerkdrucker verbinden"
        Me.Theme = MetroFramework.MetroThemeStyle.Light
        Me.MetroTabControl1.ResumeLayout(False)
        Me.MetroTabPage1.ResumeLayout(False)
        Me.MetroPanel1.ResumeLayout(False)
        Me.MetroTabPage2.ResumeLayout(False)
        Me.MetroPanel2.ResumeLayout(False)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents MetroTabControl1 As Controls.MetroTabControl
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
    Friend WithEvents ToolTip1 As ToolTip
End Class
