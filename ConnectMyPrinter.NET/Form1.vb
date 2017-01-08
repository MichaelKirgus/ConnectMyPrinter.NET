Imports System.IO
Imports System.Management
Imports System.Printing
Imports System.Printing.IndexedProperties
Imports System.Xml.Serialization
Imports ConnectMyPrinterAppSettingsHandler
Imports ConnectMyPrinterEnumerationLib
Imports ConnectMyPrinterPrinterManageLib
Imports MetroFramework.Forms
Imports Microsoft.VisualBasic.ApplicationServices

Public Class Form1
    Public PrinterEnumerationService As New EnumeratePrinters
    Public PrinterManageService As New ManagePrinter
    Public PrinterDriverRemoverService As New PrinterDriverRemover
    Public PrinterQueuesACLLib As New ConnectMyPrinterACLHelperLib.HelperFunctions

    Public AppSettings As New AppSettingsClass
    Public AppSettingFile As String = "AppSettings.xml"
    Public ActionFileDir As String = "PrinterActionsElv"

    Public PrintQueues As New List(Of PrinterQueueInfo)
    Public LocalPrinters As New List(Of PrinterQueueInfo)
    Public LocalPrinterDrivers As New List(Of PrinterDriverInfo)
    Public PrintQueuesAutoComplete As New AutoCompleteStringCollection
    Public MyPrinters As New List(Of PrinterQueueInfo)

    Public MultipleSelectionEnabled As Boolean = False
    Public ComboBoxSelected As Boolean = False
    Public UserCanControlSpooler As Boolean = False

    Public IsExpanded As Boolean = False
    Public CollapsedHeight As Integer = 160
    Public OldHeight As Integer = 0

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'Evtl. Spuren löschen, um Fehlverhalten zu verhindern
            CleanOldElevationActionFiles()

            'Laden der Einstellungen
            If Not IO.File.Exists(AppSettingFile) Then
                SaveSettings(AppSettings, AppSettingFile)
            End If
            AppSettings = LoadSettings(AppSettingFile)
            SaveSettings(AppSettings, AppSettingFile)

            'GUI-Einstellungen setzen
            Me.Text = AppSettings.WindowTitle
            MetroLabel1.Text = AppSettings.UserInformation
            MetroLabel3.Text = AppSettings.AdditionalUserInformation
            Dim zz As New Size(Me.Width, AppSettings.StartWindowHeight)
            Me.Size = zz
            If Not AppSettings.CompanyLogoImagePath = "" Then
                PictureBox2.Image = Image.FromFile(AppSettings.CompanyLogoImagePath)
            End If
            If AppSettings.AllowUserToConnectToNotCollectedPrinter Then
                MetroButton1.Enabled = True
            End If
            MetroCheckBox1.Checked = AppSettings.DefaultPrinterOptionActivated
            If AppSettings.UsePrinterSavingFeature = False Then
                MetroTabControl1.TabPages.Remove(MetroTabControl1.TabPages(1))
            End If
            If AppSettings.ShowRestartPrinterQueueButton Then
                Button4.Visible = True
            End If
            'Fenstergröße und Modus setzen
            If AppSettings.StartInMinimalMode = True Then
                OldHeight = Me.Height
                Dim jj As New Size(0, 0)
                Me.MinimumSize = jj
                Me.Height = CollapsedHeight
                If AppSettings.AllowExpandMode Then
                    PictureBox3.Visible = True
                End If
                IsExpanded = False
                Me.Resizable = False
            Else
                PictureBox3.Image = My.Resources.decollapse_2
            End If
            If AppSettings.StartWindowPosition = AppSettingsClass.AppWindowPosition.CenterScreen Then
                Me.StartPosition = FormStartPosition.CenterScreen
            End If
            If AppSettings.StartWindowPosition = AppSettingsClass.AppWindowPosition.AtNotificationBar Then
                Dim mainscreenwidth As Integer
                Dim mainscreenheight As Integer
                mainscreenwidth = My.Computer.Screen.WorkingArea.Width
                mainscreenheight = My.Computer.Screen.WorkingArea.Height
                Dim ww As New Point(mainscreenwidth - Me.Width, mainscreenheight - Me.Height)
                Me.Location = ww
            End If
            If AppSettings.StartWindowPosition = AppSettingsClass.AppWindowPosition.AtStartMenu Then
                Dim mainscreenheight As Integer
                mainscreenheight = My.Computer.Screen.WorkingArea.Height
                Dim ww As New Point(0, mainscreenheight - Me.Height)
                Me.Location = ww
            End If
            Me.TopMost = AppSettings.ShowTopMost
            MetroCheckBox1.Visible = AppSettings.AllowUserToSetPrinterToDefaultByConnecting
            ComboBox1.MaxDropDownItems = AppSettings.AutoCompleteMaxVisibleItems
            MetroTabControl1.SelectedTab = MetroTabControl1.TabPages(0)
            ResetPrinterSearchField()

            'Seq. Prüfung der Drucker aktivieren/deaktivieren
            LocalPrinterChangeTimer.Enabled = AppSettings.AlwaysCheckForNewPrinters
            LocalPrinterChangeTimer.Interval = AppSettings.AlwaysCheckForNewPrintersInterval

            'Asynchrone Vorgänge starten
            LoadAllPrintersAsync.RunWorkerAsync()
            LoadAllLocalPrinters.RunWorkerAsync()

            'Gespeicherte Drucker laden
            If AppSettings.UsePrinterSavingFeature Then
                LoadMyPrinters()
                FillGUIWithSavedPrinters()
            End If

            'Prüfen, ob Benutzer die Berechtigung hat, den Spoolerdienst ohne Admin-Rechte zu steuern:
            If AppSettings.CheckUserSpoolerPermissions Then
                UserCanControlSpooler = PrinterQueuesACLLib.CheckForSpoolerPermission
            Else
                UserCanControlSpooler = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function AddSavedPrinter(ByVal Obj As PrinterQueueInfo, ByVal OverwriteDuplicate As Boolean) As Boolean
        Try
            'Prüfem, ob bereits ein Drucker mit diesem Namen existiert:
            For Each item As PrinterQueueInfo In MyPrinters
                If item.ShareName.ToLower = Obj.ShareName.ToLower Then
                    If OverwriteDuplicate Then
                        Try
                            MyPrinters.Remove(item)
                            Exit For
                        Catch ex As Exception
                        End Try
                    Else
                        Return False
                    End If
                End If
            Next

            'Hinzufügen
            MyPrinters.Add(Obj)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DeleteSavedPrinter(ByVal Obj As PrinterQueueInfo) As Boolean
        Try
            'Drucker löschen
            For Each item As PrinterQueueInfo In MyPrinters
                If item.ShareName.ToLower = Obj.ShareName.ToLower Then
                    Try
                        MyPrinters.Remove(item)
                        Exit For
                    Catch ex As Exception
                    End Try
                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function LoadMyPrinters() As Boolean
        Try
            Dim pp As New SavedPrinterEnumerationSerializer
            MyPrinters = pp.LoadMyPrinterCollectionFile(AppSettings.SavedPrintersProfileFile)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SaveMyPrinters() As Boolean
        Try
            Dim pp As New SavedPrinterEnumerationSerializer
            pp.SaveMyPrinterCollectionFile(MyPrinters, AppSettings.SavedPrintersProfileFile)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CleanOldElevationActionFiles() As Boolean
        Try
            Dim tmp As String
            Dim tmp2 As String = "C:\Windows\Temp"
            tmp = tmp2 & "\" & ActionFileDir

            If IO.Directory.Exists(tmp) Then
                My.Computer.FileSystem.DeleteDirectory(tmp, FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub ReloadLocalPrinters()
        Try
            If Not LoadAllLocalPrinters.IsBusy Then
                MetroProgressSpinner2.Visible = True
                Application.DoEvents()
                LoadAllLocalPrinters.RunWorkerAsync()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Function LoadLocalPrinters() As List(Of PrinterQueueInfo)
        'Diese Funktion lädt alle auf dem Computer (bzw. im aktuellen Benutzerprofil) vorhandenen Drucker.

        Try
            Dim hh As List(Of PrintQueueCollection)
            hh = PrinterEnumerationService.InternalLocalPrinterCollector(AppSettings.ShowLocalPrinters)

            Dim result As New List(Of PrinterQueueInfo)
            Dim DefaultPrinter As String = ""
            Dim printdoc As New System.Drawing.Printing.PrintDocument
            DefaultPrinter = printdoc.PrinterSettings.PrinterName
            If DefaultPrinter.StartsWith("\\") Then
                Try
                    DefaultPrinter = DefaultPrinter.Split("\")(3)
                Catch ex As Exception
                End Try
            End If

            Dim duplicatefinder As New List(Of PrinterQueueInfo)

            'Druckerwarteschlangen auflisten und Standarddrucker markieren
            For Each item As PrintQueueCollection In hh
                For Each pq As PrintQueue In item
                    Dim zz As New PrinterQueueInfo
                    Dim IsLocal As Boolean = False
                    zz.Server = pq.HostingPrintServer.Name
                    zz.Name = pq.Name
                    If pq.ShareName = "" Then
                        zz.ShareName = pq.Name
                        IsLocal = True
                    Else
                        zz.ShareName = pq.ShareName
                    End If
                    If (pq.ShareName = DefaultPrinter) Or (pq.Name = DefaultPrinter) Then
                        zz.DefaultPrinter = True
                    End If
                    If IsLocal Then
                        zz.Server = "Lokal"
                    End If
                    zz.State = pq.QueueStatus.ToString
                    zz.DriverName = pq.QueueDriver.Name

                    'Zugehörigen Treiber suchen
                    For index = 0 To LocalPrinterDrivers.Count - 1
                        If LocalPrinterDrivers(index).Name = pq.QueueDriver.Name Then
                            'Eigenschaften des Treibers mit dem Drucker zusammenfügen
                            zz.DriverVersion = LocalPrinterDrivers(index).Version
                            zz.Driver = LocalPrinterDrivers(index)
                        End If
                    Next

                    zz.Description = pq.Description
                    zz.Location = pq.Location

                    Dim isdup As Boolean = False
                    For index = 0 To duplicatefinder.Count - 1
                        If zz.Name = duplicatefinder(index).Name Then
                            isdup = True
                        End If
                    Next

                    If isdup = True Then
                        'Der Drucker ist bereits vorhanden und wurde durch eine andere Enumeration ermittelt.
                    Else
                        duplicatefinder.Add(zz)
                        result.Add(zz)
                    End If
                Next pq
            Next

            Return result
        Catch ex As Exception
            Return New List(Of PrinterQueueInfo)
        End Try
    End Function

    Public Function GenerateAutoCompleteList(ByVal Obj As List(Of PrinterQueueInfo)) As Boolean
        'Diese Funktion stellt eine zusätzliche Auflistung bereit, um in der ComboBox ein Autocomplete zu ermöglichen.

        Try
            For index = 0 To Obj.Count - 1
                PrintQueuesAutoComplete.Add(Obj(index).ShareName)
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function FillComboBox(ByVal Obj As AutoCompleteStringCollection) As Boolean
        'Diese Funktion füllt nun die ermittelten Drucker in die ComboBox.

        Try
            ComboBox1.Items.Clear()

            If Not Obj.Count = 0 Then
                For index = 0 To PrintQueues.Count - 1
                    ComboBox1.Items.Add(Obj(index))
                Next
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function LoadSettings(ByVal Filename As String) As AppSettingsClass
        'Diese Funktion lädt die Einstellungen der Anwendung

        Try
            Dim objStreamReader As New StreamReader(Filename)
            Dim p2 As New AppSettingsClass
            Dim x As New XmlSerializer(p2.GetType)
            p2 = x.Deserialize(objStreamReader)
            objStreamReader.Close()

            Return p2
        Catch ex As Exception
            Return New AppSettingsClass
        End Try
    End Function

    Public Function SaveSettings(ByVal Settingsx As AppSettingsClass, ByVal Filename As String) As Boolean
        'Diese Funktion speichert die Einstellungen der Anwendung

        Try
            Dim XML As New XmlSerializer(Settingsx.GetType)
            Dim FS As New FileStream(Filename, FileMode.Create)
            XML.Serialize(FS, Settingsx)
            FS.Close()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function LoadAllNetworkPrinters() As List(Of PrinterQueueInfo)
        Try
            Dim result As New List(Of PrinterQueueInfo)

            For Each item As PrintServerItem In AppSettings.PrintServers
                result.AddRange(GetPrinterQueries(item.PrintServerName))
            Next

            Return result
        Catch ex As Exception
            Return New List(Of PrinterQueueInfo)
        End Try
    End Function

    Public Function GetPrinterQueries(ByVal PrintServer As String) As List(Of PrinterQueueInfo)
        Try
            'Initialisierung des PrintServer-Objekts
            Dim myPrintServer As New PrintServer("\\" & PrintServer, PrintSystemDesiredAccess.EnumerateServer)

            Dim result As New List(Of PrinterQueueInfo)

            'Druckerwarteschlangen auflisten
            Dim myPrintQueues As PrintQueueCollection = myPrintServer.GetPrintQueues()
            For Each pq As PrintQueue In myPrintQueues
                If Not pq.ShareName = "" Then
                    Dim hh As New PrinterQueueInfo
                    hh.Server = pq.HostingPrintServer.Name
                    hh.ShareName = pq.ShareName
                    result.Add(hh)
                End If
            Next pq

            Return result
        Catch ex As Exception
            Return New List(Of PrinterQueueInfo)
        End Try
    End Function
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            MetroTextBox1.Text = ComboBox1.SelectedItem
            MetroLabel2.Text = "Drucker gefunden, bereit zum verbinden."
            MetroButton1.Enabled = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadAllPrintersAsync_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles LoadAllPrintersAsync.DoWork
        'Hier werden die Netzwerkdrucker von allen Printserver in einem asynchronen Thread geladen, um die GUI nicht zum einfrieren zu bringen.

        CheckForIllegalCrossThreadCalls = False
        Try
            'Netzwerkdrucker laden
            Dim _result As List(Of PrinterQueueInfo)
            _result = LoadAllNetworkPrinters()

            'Autocomplete aufbauen
            GenerateAutoCompleteList(_result)

            e.Result = _result
        Catch ex As Exception
            e.Result = New List(Of PrinterQueueInfo)
        End Try
    End Sub

    Private Sub LoadAllPrintersAsync_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadAllPrintersAsync.RunWorkerCompleted
        'Die Drucker wurden geladen
        CheckForIllegalCrossThreadCalls = False
        Try
            PrintQueues = CopyList(e.Result)
            ComboBox1.AutoCompleteCustomSource = PrintQueuesAutoComplete

            'Ladeanzeige entfernen
            MetroProgressSpinner1.Visible = False
            PictureBox1.Visible = True

            'Anzahl gefundener Drucker anzeigen
            ResetUserStatusInfo()
        Catch ex As Exception
        End Try
    End Sub

    Public Function CopyList(Of T)(oldList As List(Of T)) As List(Of T)

        'Serialize
        Dim xmlString As String = ""
        Dim string_writer As New StringWriter
        Dim xml_serializer As New XmlSerializer(GetType(List(Of T)))
        xml_serializer.Serialize(string_writer, oldList)
        xmlString = string_writer.ToString()

        'Deserialize
        Dim string_reader As New StringReader(xmlString)
        Dim newList As List(Of T)
        newList = DirectCast(xml_serializer.Deserialize(string_reader), List(Of T))
        string_reader.Close()

        Return newList
    End Function

    Private Sub MetroTextBox1_TextChanged(sender As Object, e As EventArgs) Handles MetroTextBox1.TextChanged

        MultipleSelectionEnabled = False
        Try
            ComboBox1.Text = MetroTextBox1.Text
            If AppSettings.AllowUserToConnectToNotCollectedPrinter Then
                MetroButton1.Enabled = False
            End If


            If AppSettings.AutoCompleteCountEnable Then
                If (MetroTextBox1.Text.Length >= AppSettings.AutoCompleteCount) Or (PrintQueuesAutoComplete.Count < AppSettings.AutoCompleteCount) Then
                    'Die Länge ist ausreichend, um performant zu suchen, oder es sind nicht mehr Elemente vorhanden
                Else
                    Exit Try
                End If
            End If

            Dim _result As AutoCompleteStringCollection
            _result = GetResults(MetroTextBox1.Text, AppSettings.SearchCount, AppSettings.IgnoreUpperLowerCase)

            FillComboBox(_result)

            Dim readytoconnect As Boolean = False

            If ComboBoxSelected = True Then
                readytoconnect = True
            End If

            If Not _result.Count = 1 Or _result.Count = 0 Then
                If Not ComboBoxSelected Then
                    ComboBox1.DroppedDown = True
                Else
                    ComboBox1.DroppedDown = False
                End If
                MetroLabel2.Text = "Mehrere Ergebnisse. Bitte Suche verfeinern."
                PictureBox1.Image = My.Resources.dialog_ok_3
                MultipleSelectionEnabled = True
            Else
                ComboBox1.DroppedDown = False
                MetroLabel2.Text = "Leider kein Drucker gefunden."
                PictureBox1.Image = My.Resources.edit_delete_4
            End If

            If _result.Count = 1 Then
                ComboBox1.DroppedDown = False
                readytoconnect = True
            End If

            If readytoconnect = True Then
                ComboBoxSelected = False
                MetroLabel2.Text = "Drucker gefunden, bereit zum verbinden."
                MetroButton1.Enabled = True
                MetroTextBox1.Text = ComboBox1.Items(0)
                ComboBox1.SelectedIndex = 0
                PictureBox1.Image = My.Resources.dialog_ok_3
                MetroButton1.Focus()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Function ConnectPrinter(ByVal PrinterShareName As String, ByVal PrinterCollection As List(Of PrinterQueueInfo), ByVal SetDefaultPrinter As Boolean, ByVal Silent As Boolean) As Boolean
        Try
            Dim matchprinters As New List(Of PrinterQueueInfo)
            For index = 0 To PrinterCollection.Count - 1
                If PrinterCollection(index).ShareName = PrinterShareName Then
                    matchprinters.Add(PrinterCollection(index))
                End If
            Next

            If matchprinters.Count > 1 Then
                If AppSettings.AskUserIfMultipleResults Then
                    'Der Drucker ist auf mehreren Printservern angelegt. Es muss dem Benutzer eine Auswahl an verfügbaren Printservern angezeigt werden.
                    Dim hh As New SelectFromMultipleServersDlg

                    For Each item As PrinterQueueInfo In matchprinters
                        hh.ListBox1.Items.Add(item.Server)
                    Next
                    hh.MetroLabel2.Text = PrinterShareName

                    hh.ShowDialog()

                    Dim correctprinter As PrinterQueueInfo
                    correctprinter = matchprinters(hh.ListBox1.SelectedIndex)

                    'Nun Auflistung löschen und nur den korrekten Drucker hinzufügen
                    matchprinters.Clear()
                    matchprinters.Add(correctprinter)
                Else
                    'Der Drucker ist auf mehrern Printservern angelegt. Der Server muss automatisch ermittelt werden.

                    Dim bestsrvindex As Integer = 0
                    Dim resultx As New List(Of PrintServerItem)
                    For Each item As PrinterQueueInfo In matchprinters
                        For Each item1 As PrintServerItem In AppSettings.PrintServers
                            If item.Server.ToLower = item1.PrintServerName.ToLower Then
                                If item1.Priority > bestsrvindex Then
                                    resultx.Add(item1)
                                    bestsrvindex = item1.Priority
                                End If
                            End If
                        Next
                    Next

                    Dim printserver As PrintServerItem
                    printserver = resultx(resultx.Count - 1)

                    matchprinters.Clear()

                    For Each item As PrinterQueueInfo In matchprinters
                        If item.Server.ToLower = printserver.PrintServerName.ToLower Then
                            matchprinters.Add(item)
                        End If
                    Next
                End If
            End If

            Dim qq As New MsgBoxResult
            If (AppSettings.AskUserIfConnectPrinter) Then
                Dim kk As New ConnectPrinterDlg
                kk.MetroLabel2.Text = "Möchten Sie den Drucker " & vbNewLine & matchprinters(0).Server & "\" & matchprinters(0).ShareName & " verbinden?"
                If Silent = False Then
                    kk.ShowDialog()
                    qq = kk.DialogResult
                Else
                    qq = MsgBoxResult.Yes
                End If
            Else
                qq = MsgBoxResult.Yes
            End If

            If qq = MsgBoxResult.Yes Then
                Shell("rundll32 printui.dll PrintUIEntry /dn /n" & matchprinters(0).Server & "\" & matchprinters(0).ShareName & " /q", AppWinStyle.Hide, True, 5000)
                Shell("rundll32 printui.dll PrintUIEntry /in /n" & matchprinters(0).Server & "\" & matchprinters(0).ShareName, AppWinStyle.Hide, True, 5000)

                If SetDefaultPrinter Then
                    PrinterManageService.SetDefaultPrinter(matchprinters(0))
                End If
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetResults(ByVal SearchText As String, ByVal SearchCount As Integer, ByVal IgnoreCase As Boolean) As AutoCompleteStringCollection
        Try
            Dim result As New AutoCompleteStringCollection
            Dim cnt As Integer = 0

            If IgnoreCase = True Then
                For index = 0 To PrintQueuesAutoComplete.Count - 1
                    If PrintQueuesAutoComplete(index).ToLower.Contains(SearchText.ToLower) Then
                        result.Add(PrintQueuesAutoComplete(index))
                    End If
                    If cnt = SearchCount Then
                        Exit For
                    End If
                    cnt += 1
                Next
            Else
                For index = 0 To PrintQueuesAutoComplete.Count - 1
                    If PrintQueuesAutoComplete(index).Contains(SearchText) Then
                        result.Add(PrintQueuesAutoComplete(index))
                    End If
                    If cnt = SearchCount Then
                        Exit For
                    End If
                    cnt += 1
                Next
            End If


            Return result
        Catch ex As Exception
            Return New AutoCompleteStringCollection
        End Try
    End Function

    Private Sub ComboBox1_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If MultipleSelectionEnabled = True Then
            MetroButton1.Enabled = True
        End If
        Try
            MetroTextBox1.Text = ComboBox1.SelectedItem
        Catch ex As Exception
        End Try
        If ComboBox1.Items.Count < 1 Then
            ComboBoxSelected = True
        End If
    End Sub

    Public Sub ResetPrinterSearchField()
        Try
            MetroTextBox1.Text = AppSettings.FixedPrefix
        Catch ex As Exception
        End Try
    End Sub

    Public Sub ResetUserStatusInfo()
        Try
            If AppSettings.ShowPrinterCountAfterSearch Then
                MetroLabel2.Text = PrintQueuesAutoComplete.Count & " Drucker gefunden."
            Else
                MetroLabel2.Text = "Drucker geladen, bitte suchen."
            End If

            PictureBox1.Image = My.Resources.dialog_ok_3
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MetroTextBox1_Click(sender As Object, e As EventArgs) Handles MetroTextBox1.Click
        ResetPrinterSearchField()
    End Sub

    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
        ConnectPrinter(MetroTextBox1.Text, PrintQueues, MetroCheckBox1.Checked, False)
        ReloadLocalPrinters()
    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        MetroTextBox1.Focus()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub LoadAllLocalPrinters_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles LoadAllLocalPrinters.DoWork
        Try
            Dim av_drivers As List(Of PrinterDriverInfo)
            av_drivers = PrinterEnumerationService.GetLocalPrinterDriversFromWMI(AppSettings.ShowLocalPrinters)

            LocalPrinterDrivers = CopyList(av_drivers)

            Dim result As List(Of PrinterQueueInfo)
            result = LoadLocalPrinters()

            e.Result = result
        Catch ex As Exception
            e.Result = New List(Of PrinterQueueInfo)
        End Try
    End Sub

    Public Function FillGUIWithLocalPrinters() As Boolean
        Try
            FlowLayoutPanel1.Visible = False
            FlowLayoutPanel1.Controls.Clear()

            For index = 0 To LocalPrinters.Count - 1
                Dim ll As New PrinterCtl
                ll.MetroLabel1Lbl.Text = LocalPrinters(index).ShareName
                ll.MetroLabel2Lbl.Text = LocalPrinters(index).Server
                If LocalPrinters(index).DefaultPrinter Then
                    ll.PictureBox1.Image = My.Resources.printer_standard
                End If
                ll.LocationLbl.Text = LocalPrinters(index).Location
                Try
                    ll.DescriptionLbl.Text = LocalPrinters(index).Description.Split(",")(1)
                Catch ex As Exception
                End Try
                ll.DriverLbl.Text = LocalPrinters(index).DriverName
                ll.DriverVersionLbl.Text = LocalPrinters(index).DriverVersion

                If LocalPrinters(index).State = "None" Then
                    ll.StateLbl.Text = "Bereit"
                Else
                    ll.StateLbl.Text = LocalPrinters(index).State
                End If

                ll.ConfigFileLbl.Text = LocalPrinters(index).Driver.ConfigFile
                ll.DriverPathLbl.Text = LocalPrinters(index).Driver.DriverPath
                ll.DataFileLbl.Text = LocalPrinters(index).Driver.DataFile

                ll.Tag = LocalPrinters(index)
                ll._parent = Me

                FlowLayoutPanel1.Controls.Add(ll)
                Application.DoEvents()
            Next

            FlowLayoutPanel1.Visible = True

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function FillGUIWithSavedPrinters() As Boolean
        Try
            FlowLayoutPanel2.Visible = False
            FlowLayoutPanel2.Controls.Clear()

            For index = 0 To MyPrinters.Count - 1
                Dim ll As New SavedPrinterCtl
                ll.MetroLabel1Lbl.Text = MyPrinters(index).ShareName
                ll.MetroLabel2Lbl.Text = MyPrinters(index).Server
                ll.LocationLbl.Text = MyPrinters(index).Location
                Try
                    ll.DescriptionLbl.Text = MyPrinters(index).Description.Split(",")(1)
                Catch ex As Exception
                End Try

                ll.Tag = MyPrinters(index)
                ll._parent = Me

                FlowLayoutPanel2.Controls.Add(ll)
            Next

            If MyPrinters.Count = 0 Then
                MetroLabel4.Visible = True
            Else
                MetroLabel4.Visible = False
            End If


            FlowLayoutPanel2.Visible = True

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub LoadAllLocalPrinters_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadAllLocalPrinters.RunWorkerCompleted
        Try
            Application.DoEvents()

            Dim ll As New List(Of PrinterQueueInfo)
            ll = CopyList(e.Result)

            LocalPrinters = ll

            'Druckerliste laden
            FillGUIWithLocalPrinters()
            Application.DoEvents()

            'Ladezustand zurücksetzen
            MetroProgressSpinner2.Visible = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MetroTabControl1_Validated(sender As Object, e As EventArgs) Handles MetroTabControl1.Validated
        MetroTabPage1.Visible = False
        MetroTabPage1.Visible = True
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ReloadLocalPrinters()
        If AppSettings.UsePrinterSavingFeature Then
            LoadMyPrinters()
            FillGUIWithSavedPrinters()
        End If
        MetroTabControl1.SelectedTab = MetroTabControl1.TabPages(0)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim hh As New AboutFrm
        hh.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim jj As New AppSettingsDlg
        jj._parent = Me
        jj.ShowDialog()
    End Sub

    Private Sub LocalPrinterChangeTimer_Tick(sender As Object, e As EventArgs) Handles LocalPrinterChangeTimer.Tick
        Try
            If (Application.OpenForms.Count = 2) And LoadAllLocalPrinters.IsBusy = False Then
                LocalPrinterIdleWorker.RunWorkerAsync()
                LocalPrinterChangeTimer.Stop()
            Else
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LocalPrinterIdleWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles LocalPrinterIdleWorker.DoWork
        Try
            Dim result As List(Of PrinterQueueInfo)
            result = LoadLocalPrinters()

            e.Result = result
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LocalPrinterIdleWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LocalPrinterIdleWorker.RunWorkerCompleted
        Try
            Dim jj As List(Of PrinterQueueInfo)
            jj = e.Result

            If LocalPrinters.Count = jj.Count Then
            Else
                ReloadLocalPrinters()
            End If

            LocalPrinterChangeTimer.Start()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        If IsExpanded Then
            OldHeight = Me.Height
            Dim gg As New Size(0, 0)
            Me.MinimumSize = gg
            Me.Height = CollapsedHeight
            Me.Resizable = False
            PictureBox3.Image = My.Resources.expandable
            IsExpanded = False
        Else
            Me.Height = Me.OldHeight
            Me.Resizable = True
            Dim jj As New Size(Me.Width, OldHeight)
            Me.MinimumSize = jj
            PictureBox3.Image = My.Resources.decollapse_2
            IsExpanded = True
        End If
    End Sub

    Private Sub PictureBox3_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox3.MouseEnter
        If IsExpanded Then
            PictureBox3.Image = My.Resources.decollapse
        Else
            PictureBox3.Image = My.Resources.expandable_2
        End If
    End Sub

    Private Sub PictureBox3_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox3.MouseLeave
        If IsExpanded Then
            PictureBox3.Image = My.Resources.decollapse_2
        Else
            PictureBox3.Image = My.Resources.expandable
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            Dim tt As New ProcessingDlg
            tt.Show(Me.Parent)
            Application.DoEvents()
            Dim hh As New ManagePrinter
            hh.RestartPrinterService()
            Application.DoEvents()
            tt.Close()
        Catch ex As Exception
        End Try
    End Sub
End Class
