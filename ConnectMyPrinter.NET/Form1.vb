'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports System.Globalization
Imports System.IO
Imports System.Management
Imports System.Printing
Imports System.Printing.IndexedProperties
Imports System.Xml.Serialization
Imports ConnectMyPrinterAppSettingsHandler
Imports ConnectMyPrinterEnumerationLib
Imports ConnectMyPrinterLanguageHelper
Imports ConnectMyPrinterPrinterManageLib
Imports MetroFramework.Forms
Imports Microsoft.VisualBasic.ApplicationServices

Public Class Form1
    Public MLangHelper As New LanguageApplyHelper
    Public MCultureInf As CultureInfo = CultureInfo.CurrentUICulture

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
    Public PrinterCollectReturnState As Integer = 0

    Public IsExpanded As Boolean = False
    Public CollapsedHeight As Integer = 160
    Public OldHeight As Integer = 0

    Public _Log As New ConnectMyPrinterLog.Logging

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'Evtl. Spuren löschen, um Fehlverhalten zu verhindern
            CleanOldElevationActionFiles()

            'Korrekte AppSettings-Datei laden
            Dim MUIHelper As New MUISettingsHandler
            AppSettingFile = MUIHelper.GetAppSettingsFilePath(True)

            'Befehlszeilenparameter prüfen
            For Each argument In My.Application.CommandLineArgs
                If argument.StartsWith("/SETTINGS|") Then
                    AppSettingFile = argument.Split("|")(1)
                End If
            Next

            'Laden der Einstellungen (im Programmverzeichnis oder über Befehlszeile)
            If Not IO.File.Exists(AppSettingFile) Then
                SaveSettings(AppSettings, AppSettingFile)
            End If
            AppSettings = LoadSettings(AppSettingFile)
            SaveSettings(AppSettings, AppSettingFile)

            'Ggf. Logging aktivieren
            _Log.Enable = AppSettings.EnableLogging
            _Log.LogFile = Environment.ExpandEnvironmentVariables(AppSettings.LogFile)
            _Log.WriteLogSystemInfo()

            'GUI-Einstellungen setzen
            _Log.Write(ConnectMyPrinterLog.Logging.LogType.Information, Me, "Lade GUI-Einstellungen", Err)
            Me.Text = AppSettings.WindowTitle
            MetroLabel1.Text = AppSettings.UserInformation
            MetroLabel3.Text = AppSettings.AdditionalUserInformation
            CustomSupportInfoLbl.Text = AppSettings.AdditionalSupportBottomInformation
            Dim zz As New Size(Me.Width, AppSettings.StartWindowHeight)
            Me.Size = zz

            'Bild aus Bilddatei oder aus Base64-String laden
            If Not Environment.ExpandEnvironmentVariables(AppSettings.CompanyLogoImagePath) = "" Then
                Try
                    PictureBox2.Image = Image.FromFile(Environment.ExpandEnvironmentVariables(AppSettings.CompanyLogoImagePath))
                Catch ex As Exception
                End Try
            End If
            If Not AppSettings.CompanyLogoImageBase64 = "" Then
                Try
                    Dim tmp As Image
                    Dim ByteArray
                    ByteArray = ConvertBase64ToByteArray(AppSettings.CompanyLogoImageBase64)
                    tmp = convertbytetoimage(ByteArray)
                    PictureBox2.Image = tmp
                Catch ex As Exception
                End Try
            End If

            'Style setzen
            Me.Style = AppSettings.WindowStyle
            ComboBox1.Style = AppSettings.ComboxBoxStyle
            MetroTabControl1.Style = AppSettings.TabControlStyle
            MetroButton1.Style = AppSettings.ButtonControlStyle
            MetroProgressSpinner1.Style = AppSettings.SpinnerControlStyle
            MetroProgressSpinner2.Style = AppSettings.SpinnerControlStyle
            MetroProgressSpinner3.Style = AppSettings.SpinnerControlStyle

            'Watermark in Textbox in korrekter Sprache laden:
            MetroTextBox1.WaterMark = MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "TextBoxWatermarkStr", "")

            'Verhindern, dass die Farbe des ausgewählten Elements im ComboBox-Element die gesamte Liste füllt:
            ComboBox1.Refresh()

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
            If AppSettings.ShowAdvancedPrinterListButton Then
                Button5.Visible = True
            End If
            'Kontextmenü ausblenden, wenn keine erweiterten Verknüpfungen genutzt
            If (AppSettings.ShowPrintManagementCenterEntry = False And AppSettings.ShowForceDeletePrinterEntry = False) Then
                Me.ContextMenuStrip = Nothing
                PictureBox2.ContextMenuStrip = Nothing
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

            'Muss das Eingabefeld verdeckt werden, da erst nach der Suche der Drucker ein Wert eingegeben werden darf?:
            If AppSettings.AllowUserToEnterPrinternameBeforeSearchFinished Then
                MetroLabel5.Visible = False
            End If

            'Zusätzliche RTF-Info für Druckernamen laden
            If Not AppSettings.AdditionalUserHelpInformationRTF = "" Then
                AdditionalInfoRTF.LoadFile(AppSettings.AdditionalUserHelpInformationRTF)
            End If

            'Seq. Prüfung der Drucker aktivieren/deaktivieren
            _Log.Write(ConnectMyPrinterLog.Logging.LogType.Information, Me, "Seq. Prüfung der Drucker aktivieren/deaktivieren", Err)
            LocalPrinterChangeTimer.Enabled = AppSettings.AlwaysCheckForNewPrinters
            LocalPrinterChangeTimer.Interval = AppSettings.AlwaysCheckForNewPrintersInterval

            'Asynchrone Vorgänge starten
            _Log.Write(ConnectMyPrinterLog.Logging.LogType.Information, Me, "Asynchrone Vorgänge starten", Err)
            LoadAllPrintersAsync.RunWorkerAsync()
            LoadAllLocalPrinters.RunWorkerAsync()

            'Gespeicherte Drucker laden
            _Log.Write(ConnectMyPrinterLog.Logging.LogType.Information, Me, "Gespeicherte Drucker laden", Err)
            If AppSettings.UsePrinterSavingFeature Then
                LoadMyPrinters()
                FillGUIWithSavedPrinters()
            End If

            'Prüfen, ob Benutzer die Berechtigung hat, den Spoolerdienst ohne Admin-Rechte zu steuern:
            _Log.Write(ConnectMyPrinterLog.Logging.LogType.Information, Me, "Prüfe ACLs des Spoolers", Err)
            If AppSettings.CheckUserSpoolerPermissions Then
                UserCanControlSpooler = PrinterQueuesACLLib.CheckForSpoolerPermission
            Else
                UserCanControlSpooler = True
            End If

            'Prüfen, ob das Textfeld den Fokus haben soll:
            If AppSettings.FocusPrinternameFieldAtStart Then
                Me.FocusMe()
                MetroTextBox1.TabIndex = 0
                Application.DoEvents()
                SendKeys.Send("{TAB}")
            End If
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
        End Try
    End Sub

    Public Function ConvertImageToBase64String() As String
        Using ms As New MemoryStream()
            PictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png) 'We load the image from first PictureBox in the MemoryStream
            Dim obyte = ms.ToArray() 'We transform it to byte array..

            Return Convert.ToBase64String(obyte) 'We then convert the byte array to base 64 string.
        End Using
    End Function

    Public Function ConvertBase64ToByteArray(base64 As String) As Byte()
        Return Convert.FromBase64String(base64) 'Convert the base64 back to byte array.
    End Function

    Private Function convertbytetoimage(ByVal BA As Byte())
        Dim ms As MemoryStream = New MemoryStream(BA)
        Dim image = System.Drawing.Image.FromStream(ms)
        Return image
    End Function

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
            _Log.Write(ConnectMyPrinterLog.Logging.LogType.Information, Me, "Füge Drucker zu den gespeicherten Druckern hinzu", Err)
            MyPrinters.Add(Obj)

            Return True
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
            Return False
        End Try
    End Function

    Public Function DeleteSavedPrinter(ByVal Obj As PrinterQueueInfo) As Boolean
        Try
            _Log.Write(ConnectMyPrinterLog.Logging.LogType.Information, Me, "Lösche gespeicherten Drucker", Err)
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
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
            Return False
        End Try
    End Function

    Public Function LoadMyPrinters() As Boolean
        Try
            _Log.Write(ConnectMyPrinterLog.Logging.LogType.Information, Me, "Lade verfügbare Drucker", Err)
            Dim pp As New SavedPrinterEnumerationSerializer
            MyPrinters = pp.LoadMyPrinterCollectionFile(AppSettings.SavedPrintersProfileFile)

            Return True
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
            Return False
        End Try
    End Function

    Public Function SaveMyPrinters() As Boolean
        Try
            _Log.Write(ConnectMyPrinterLog.Logging.LogType.Information, Me, "Serialisiere gespeicherte Drucker", Err)
            Dim pp As New SavedPrinterEnumerationSerializer
            pp.SaveMyPrinterCollectionFile(MyPrinters, AppSettings.SavedPrintersProfileFile)

            Return True
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
            Return False
        End Try
    End Function

    Public Function CleanOldElevationActionFiles() As Boolean
        Try
            _Log.Write(ConnectMyPrinterLog.Logging.LogType.Information, Me, "Lösche Dateien des UAC-Helpers", Err)

            Dim tmp As String
            Dim tmp2 As String = "C:\Windows\Temp"
            tmp = tmp2 & "\" & ActionFileDir

            If IO.Directory.Exists(tmp) Then
                My.Computer.FileSystem.DeleteDirectory(tmp, FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If

            Return True
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
            Return False
        End Try
    End Function

    Public Sub ReloadLocalPrinters()
        Button3.Enabled = False
        Try
            If Not LoadAllLocalPrinters.IsBusy Then
                If AppSettings.ShowProgressCircleOnEvents Then
                    MetroProgressSpinner2.Visible = True
                End If
                Application.DoEvents()
                LoadAllLocalPrinters.RunWorkerAsync()
            End If
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
        End Try
    End Sub

    Public Function LoadLocalPrinters(Optional ByVal GetLocalPrinters As Boolean = False) As List(Of PrinterQueueInfo)
        'Diese Funktion lädt alle auf dem Computer (bzw. im aktuellen Benutzerprofil) vorhandenen Drucker.

        _Log.Write(ConnectMyPrinterLog.Logging.LogType.Information, Me, "Lade alle Drucker von aktuellem Benutzer", Err)
        Try
            Dim hh As List(Of PrintQueueCollection)

            If GetLocalPrinters = False Then
                hh = PrinterEnumerationService.InternalLocalPrinterCollector(AppSettings.ShowLocalPrinters)
            Else
                hh = PrinterEnumerationService.InternalLocalPrinterCollector(True)
            End If

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
                        zz.Server = MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "LocalPrintServerStr", "Local")
                    End If
                    zz.State = String.Join(",", pq.QueueStatus)
                    zz.DriverName = pq.QueueDriver.Name

                    'Zugehörigen Treiber suchen
                    For index = 0 To LocalPrinterDrivers.Count - 1
                        If LocalPrinterDrivers(index).Name = pq.QueueDriver.Name Then
                            'Eigenschaften des Treibers mit dem Drucker zusammenfügen
                            zz.DriverVersion = LocalPrinterDrivers(index).Version
                            zz.Driver = LocalPrinterDrivers(index)
                        End If
                    Next

                    zz.Description = pq.Comment
                    zz.Location = pq.Location

                    Dim isdup As Boolean = False
                    For index = 0 To duplicatefinder.Count - 1
                        If zz.Name = duplicatefinder(index).Name Then
                            isdup = True
                        End If
                    Next

                    'Prüfen, ob der Drucker auf der Hidden-Liste steht:
                    For index = 0 To AppSettings.HiddenPrinterList.Count - 1
                        If zz.ShareName.Contains(AppSettings.HiddenPrinterList(index)) Then
                            isdup = True
                        End If
                    Next

                    If isdup = True Then
                        'Der Drucker ist bereits vorhanden/steht auf Hidden-Liste und wurde durch eine andere Enumeration ermittelt.
                    Else
                        duplicatefinder.Add(zz)
                        result.Add(zz)
                    End If
                Next pq
            Next

            Return result
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
            Return New List(Of PrinterQueueInfo)
        End Try
    End Function

    Public Function GenerateAutoCompleteList(ByVal Obj As List(Of PrinterQueueInfo)) As Boolean
        'Diese Funktion stellt eine zusätzliche Auflistung bereit, um in der ComboBox ein Autocomplete zu ermöglichen.

        Try
            PrintQueuesAutoComplete.Clear()

            'Auf Duplikate in der Liste prüfen und ggf. entfernen
            If AppSettings.PrintServers.Count > 1 Then
                Obj = RemoveDupItems(Obj)
            End If

            'Liste generieren
            For index = 0 To Obj.Count - 1
                PrintQueuesAutoComplete.Add(Obj(index).ShareName)
            Next

            Return True
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
            Return False
        End Try
    End Function

    Public Function RemoveDupItems(ByVal Obj As List(Of PrinterQueueInfo)) As List(Of PrinterQueueInfo)
        Try
            Dim nodupcollection As New List(Of PrinterQueueInfo)

            For ind1 = 0 To Obj.Count - 1
                Dim additm As Boolean = True
                For ind2 = 0 To nodupcollection.Count - 1
                    If Obj(ind1).ShareName.ToLower = nodupcollection(ind2).ShareName.ToLower Then
                        additm = False
                    End If
                Next

                If additm Then
                    nodupcollection.Add(Obj(ind1))
                End If
            Next

            Return nodupcollection
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
            Return Obj
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
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
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
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
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
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
            Return False
        End Try
    End Function

    Public Function LoadAllNetworkPrinters() As List(Of PrinterQueueInfo)
        Try
            Dim result As New List(Of PrinterQueueInfo)

            Dim timeout As New Stopwatch
            If Not AppSettings.MaxPrinterCollectTime = 0 Then
                timeout.Start()
            End If

            For Each item As PrintServerItem In AppSettings.PrintServers
                If item.RequestPing Then
                    Try
                        If My.Computer.Network.Ping(item.PrintServerName, item.PingTimeout) Then
                            result.AddRange(GetPrinterQueries(item.PrintServerName))
                        Else
                            PrinterCollectReturnState = 2
                            If AppSettings.CancelCollectionOnPrintServerNotAvailable Then
                                If timeout.IsRunning Then
                                    timeout.Stop()
                                End If
                                Exit For
                            End If
                        End If
                    Catch ex As Exception
                        'Eine Ping Exception ist aufgetreten, Host oder Netzwerk nicht erreichbar...
                        PrinterCollectReturnState = 2
                        If AppSettings.CancelCollectionOnPrintServerNotAvailable Then
                            If timeout.IsRunning Then
                                timeout.Stop()
                            End If
                            Exit For
                        End If
                    End Try

                Else
                    result.AddRange(GetPrinterQueries(item.PrintServerName))
                End If

                If timeout.IsRunning Then
                    If timeout.ElapsedMilliseconds > AppSettings.MaxPrinterCollectTime Then
                        PrinterCollectReturnState = 1
                        timeout.Stop()
                        Exit For
                    End If
                End If
            Next

            Return result
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
            Return New List(Of PrinterQueueInfo)
        End Try
    End Function

    Public Function GetPrinterQueries(ByVal PrintServer As String) As List(Of PrinterQueueInfo)
        Try
            _Log.Write(ConnectMyPrinterLog.Logging.LogType.Information, Me, "Suche und lade alle Drucker im Netzwerk", Err)

            'Initialisierung des PrintServer-Objekts
            Dim myPrintServer As New PrintServer("\\" & PrintServer, PrintSystemDesiredAccess.EnumerateServer)

            Dim result As New List(Of PrinterQueueInfo)

            'Druckerwarteschlangen auflisten
            Dim myPrintQueues As PrintQueueCollection = myPrintServer.GetPrintQueues()
            If AppSettings.CollectAdditionalInformation Then
                For Each pq As PrintQueue In myPrintQueues
                    If Not pq.ShareName = "" Then
                        Dim hh As New PrinterQueueInfo
                        hh.Server = pq.HostingPrintServer.Name
                        hh.ShareName = pq.ShareName
                        Try
                            hh.Description = pq.Comment
                            hh.Location = pq.Location
                            hh.State = pq.QueueStatus.ToString
                        Catch ex As Exception
                        End Try
                        Try
                            hh.DriverName = pq.QueueDriver.Name
                        Catch ex As Exception
                        End Try
                        result.Add(hh)
                    End If
                Next pq
            Else
                For Each pq As PrintQueue In myPrintQueues
                    If Not pq.ShareName = "" Then
                        Dim hh As New PrinterQueueInfo
                        hh.Server = pq.HostingPrintServer.Name
                        hh.ShareName = pq.ShareName
                        result.Add(hh)
                    End If
                Next pq
            End If

            Return result
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
            Return New List(Of PrinterQueueInfo)
        End Try
    End Function
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            MetroTextBox1.Text = ComboBox1.SelectedItem
            MetroLabel2.Text = MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ExactPrinterFoundPrinterSearchText", "")
            MetroButton1.Enabled = True
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
        End Try
    End Sub

    Private Sub LoadAllPrintersAsync_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles LoadAllPrintersAsync.DoWork
        'Hier werden die Netzwerkdrucker von allen Printserver in einem asynchronen Thread geladen, um die GUI nicht zum einfrieren zu bringen.

        CheckForIllegalCrossThreadCalls = False
        Try
            Dim WasCachedContentLoaded As Boolean = False

            Dim oo As New SavedPrinterEnumerationSerializer
            Dim _cacheresult As List(Of PrinterQueueInfo) = Nothing
            'Prüfen, ob kurzfristig eine Cache-Datei geladen werden soll:
            If AppSettings.FirstLoadCachedPrinters Then
                _cacheresult = oo.LoadMyPrinterCollectionFile(Environment.ExpandEnvironmentVariables(AppSettings.CacheFoundPrintersFilepath))
                GenerateAutoCompleteList(_cacheresult)
                PrintQueues = CopyList(e.Result)
                ComboBox1.AutoCompleteCustomSource = PrintQueuesAutoComplete
                If Not _cacheresult.Count = 0 Then
                    WasCachedContentLoaded = True
                End If
            Else
                _cacheresult = oo.LoadMyPrinterCollectionFile(Environment.ExpandEnvironmentVariables(AppSettings.CacheFoundPrintersFilepath))
                If Not _cacheresult.Count = 0 Then
                    WasCachedContentLoaded = True
                End If
            End If

            'Netzwerkdrucker laden
            Dim _result As List(Of PrinterQueueInfo)
            _result = LoadAllNetworkPrinters()

            'Prüfen, ob die zwischengeladenen Ergebnisse beibehalten werden sollen:
            If Not PrinterCollectReturnState = 0 Then
                'Die Suche wurde nicht abgeschlossen, bzw. nicht vollständig abgeschlossen. 
                If AppSettings.FirstLoadCachedPrinters And WasCachedContentLoaded Then
                    'Es wurden Ergebnisse zwischengeladen und werden endgültig verwendet:
                    GenerateAutoCompleteList(_cacheresult)
                    e.Result = _cacheresult
                    Exit Try
                End If
                If (AppSettings.FirstLoadCachedPrinters = False) And (WasCachedContentLoaded) Then
                    'Es wurden Ergebnisse zwischengeladen, allerdings noch nicht angezeigt.
                    'Verwende diese Elemente nun als Ergebnisse:
                    GenerateAutoCompleteList(_cacheresult)
                    e.Result = _cacheresult
                    Exit Try
                End If
            End If

            'Es werden Live-Ergebnisse angezeigt:
            'Ergebnis ggf. in Cache-File speichern:
            If Not Environment.ExpandEnvironmentVariables(AppSettings.CacheFoundPrintersFilepath) = "" Then
                If Not _result.Count = 0 Then
                    oo.SaveMyPrinterCollectionFile(_result, Environment.ExpandEnvironmentVariables(AppSettings.CacheFoundPrintersFilepath))
                End If
            End If

            'Autocomplete aufbauen
            GenerateAutoCompleteList(_result)

            e.Result = _result
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
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

            'Alle Drucker auflisten ermöglichen
            Button5.Enabled = True

            'Ggf. das Eingeben von Druckernamen ermöglichen
            MetroLabel5.Visible = False

            'Anzahl gefundener Drucker anzeigen
            ResetUserStatusInfo()
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
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

    Public Sub ClickOnCompanyLogo(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Try
            If Not AppSettings.CompanyLogoImageClickURL = "" Then
                Dim hh As New Process
                hh.StartInfo.FileName = AppSettings.CompanyLogoImageClickURL
                hh.Start()
            End If
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
        End Try
    End Sub

    Private Sub MetroTextBox1_TextChanged(sender As Object, e As EventArgs) Handles MetroTextBox1.TextChanged

        MultipleSelectionEnabled = False
        Try
            ComboBox1.Text = MetroTextBox1.Text
            If AppSettings.AllowUserToConnectToNotCollectedPrinter Then
                MetroButton1.Enabled = False
            End If

            'Prüfen auf Admin-Eingabe
            If MetroTextBox1.Text.StartsWith("=") Then
                'Es wird eine Admin-Funktion aufgerufen:
                If MetroTextBox1.Text.ToLower = "=clean" Then
                    Shell("ConnectMyPrinterClean.exe")
                    MetroTextBox1.BackColor = Color.LightGreen
                    MetroTextBox1.Text = ""
                End If
                If MetroTextBox1.Text.ToLower = "=settings" Then
                    Shell("ConnectMyPrinterSettingsConsole.exe")
                    MetroTextBox1.BackColor = Color.LightGreen
                    MetroTextBox1.Text = ""
                End If
                If MetroTextBox1.Text.ToLower = "=list" Then
                    Button5.PerformClick()
                    MetroTextBox1.BackColor = Color.LightGreen
                    MetroTextBox1.Text = ""
                End If
                If MetroTextBox1.Text.ToLower = "=restart" Then
                    Dim ActionLib As New ConnectMyPrinterPrinterManageLib.ManagePrinter
                    Dim ElevationHelper As New ElevationHelperClass
                    If AppSettings.PrinterSpoolerRestartNeedElevation Or (UserCanControlSpooler = False) Then
                        ElevationHelper.GenerateActionFile("RestartPrinterService", New ConnectMyPrinterEnumerationLib.PrinterQueueInfo, Me, New PrinterCtl)
                        ElevationHelper.StartElevatedActions(Me, Nothing)
                    Else
                        ActionLib.RestartPrinterService()
                    End If
                    MetroTextBox1.BackColor = Color.LightGreen
                    MetroTextBox1.Text = ""
                End If
            Else
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
                        Cursor.Current = Cursors.Default
                        ComboBox1.DroppedDown = True
                    Else
                        ComboBox1.DroppedDown = False
                    End If
                    MetroLabel2.Text = MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "MultipleResultsPrinterSearchText", "")
                    'MetroLabel2.Text = "Mehrere Ergebnisse. Bitte Suche verfeinern."
                    PictureBox1.Image = My.Resources.dialog_ok_3
                    MultipleSelectionEnabled = True
                Else
                    ComboBox1.DroppedDown = False
                    MetroLabel2.Text = MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "NoResultsPrinterSearchText", "")
                    'MetroLabel2.Text = "Leider kein Drucker gefunden."
                    PictureBox1.Image = My.Resources.edit_delete_4
                End If

                If _result.Count = 1 Then
                    ComboBox1.DroppedDown = False
                    readytoconnect = True
                End If

                If readytoconnect = True Then
                    ComboBoxSelected = False
                    MetroLabel2.Text = MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ExactPrinterFoundPrinterSearchText", "")
                    'MetroLabel2.Text = "Drucker gefunden, bereit zum verbinden."
                    MetroButton1.Enabled = True
                    MetroTextBox1.Text = ComboBox1.Items(0)
                    ComboBox1.SelectedIndex = 0
                    PictureBox1.Image = My.Resources.dialog_ok_3
                    MetroButton1.Focus()
                    If AppSettings.AutoConnectPrinterIfExactResult Then
                        MetroButton1.PerformClick()
                    End If
                End If
            End If


        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
        End Try
    End Sub

    Public Function ConnectPrinter(ByVal PrinterShareName As String, ByVal PrinterCollection As List(Of PrinterQueueInfo), ByVal SetDefaultPrinter As Boolean, ByVal Silent As Boolean) As Boolean
        Try
            _Log.Write(ConnectMyPrinterLog.Logging.LogType.Information, Me, "Drucker " & PrinterShareName & " verbinden", Err)

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
                    hh._parent = Me

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
                    'Der Drucker ist auf mehreren Printservern angelegt. Der Server muss automatisch ermittelt werden.

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

                    Dim bestprinter As New List(Of PrinterQueueInfo)
                    For Each item As PrinterQueueInfo In matchprinters
                        If item.Server.ToLower = printserver.PrintServerName.ToLower Then
                            bestprinter.Add(item)
                        End If
                    Next

                    matchprinters.Clear()
                    matchprinters.Add(bestprinter(0))
                End If
            End If

            Dim qq As New MsgBoxResult
            If (AppSettings.AskUserIfConnectPrinter) Then
                Dim kk As New ConnectPrinterDlg
                kk._parent = Me

                kk.MetroLabel2.Text = MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "AskUserForPrinterConnectionPart1", "") & vbNewLine & matchprinters(0).Server & "\" &
                    matchprinters(0).ShareName & MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "AskUserForPrinterConnectionPart2", "")
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

                'Drucker verbinden
                Shell("rundll32 printui.dll PrintUIEntry /dn /n " & matchprinters(0).Server & "\" & matchprinters(0).ShareName & " /q", AppWinStyle.Hide, True, AppSettings.MaxPrinterAPIShellTimout)
                Shell("rundll32 printui.dll PrintUIEntry /in /n " & matchprinters(0).Server & "\" & matchprinters(0).ShareName, AppWinStyle.Hide, True, AppSettings.MaxPrinterAPIShellTimout)

                If SetDefaultPrinter Then
                    PrinterManageService.SetDefaultPrinter(matchprinters(0))
                End If
            End If

            If AppSettings.ShowDriverNotifications And qq = MsgBoxResult.Yes Then
                If Silent = False Then
                    'Prüfen auf Hinweise zum Drucker bzw. Druckertreiber
                    Dim notifydrv As New ConnectMyPrinterDriverNotifications.CheckDriverNotifications
                    Dim result1 As String
                    result1 = notifydrv.CheckForNotifications(matchprinters(0).DriverName, AppSettings.DriverNotifications)

                    If Not result1 = "" Then
                        'Es wurde eine RTF-Datei zurückgegeben, diese wird nun angezeigt
                        Dim uu As New DriverNotificationRtfViewerDlg
                        uu._parent = Me
                        uu.Show()
                        uu.RichTextBox1.LoadFile(result1)
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
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
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
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

    Public Sub ResetPrinterSearchField(Optional ByVal ShowPrintServerStats As Boolean = False)
        Try
            MetroTextBox1.Text = AppSettings.FixedPrefix
            If ShowPrintServerStats Then
                If AppSettings.ShowPrinterCountAfterSearch Then
                    MetroLabel2.Text = PrintQueuesAutoComplete.Count & MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "PrinterCollectCountText", "")
                Else
                    MetroLabel2.Text = PrintQueuesAutoComplete.Count & MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "PrinterCollectFinishedText", "")
                End If
            End If
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
        End Try
    End Sub

    Public Sub ResetUserStatusInfo()
        Try
            If PrinterCollectReturnState = 1 Then
                MetroLabel2.Text = MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "PrinterCollectNotFinishedTimeout", "")
                PictureBox1.Image = My.Resources.dialog_error
            End If
            If PrinterCollectReturnState = 2 Then
                MetroLabel2.Text = MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "PrinterCollectNotFinishedServerNotAv", "")
                PictureBox1.Image = My.Resources.dialog_error
            End If
            If PrinterCollectReturnState = 0 Then
                If AppSettings.ShowPrinterCountAfterSearch Then
                    MetroLabel2.Text = PrintQueuesAutoComplete.Count & MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "PrinterCollectCountText", "")
                Else
                    MetroLabel2.Text = PrintQueuesAutoComplete.Count & MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "PrinterCollectFinishedText", "")
                End If

                PictureBox1.Image = My.Resources.dialog_ok_3
            End If

        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
        End Try
    End Sub

    Private Sub MetroTextBox1_Click(sender As Object, e As EventArgs) Handles MetroTextBox1.Click
        ResetPrinterSearchField(True)
        If AppSettings.ShowAdditionalUserHelpOnTextFieldClick Then
            HandleMouseEnterTextfield()
        End If
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
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
            e.Result = New List(Of PrinterQueueInfo)
        End Try
    End Sub

    Public Function FillGUIWithLocalPrinters() As Boolean
        Try
            _Log.Write(ConnectMyPrinterLog.Logging.LogType.Information, Me, "GUI mit Druckern füllen", Err)
            FlowLayoutPanel1.Visible = False
            FlowLayoutPanel1.Controls.Clear()

            For index = 0 To LocalPrinters.Count - 1
                Dim ll As New PrinterCtl
                ll.MetroLabel1Lbl.Text = LocalPrinters(index).ShareName
                ll.MetroLabel2Lbl.Text = LocalPrinters(index).Server

                If LocalPrinters(index).DefaultPrinter Then
                    Dim isok As Boolean = True
                    If LocalPrinters(index).State.Contains("Error") Or LocalPrinters(index).State.Contains("PaperJam") Then
                        ll.PictureBox1.Image = My.Resources.printer_error_standard_2
                        isok = False
                    End If
                    If LocalPrinters(index).State.Contains("Offline") Or LocalPrinters(index).State.Contains("Paused") Then
                        ll.PictureBox1.Image = My.Resources.printer_offline_standard_2
                        isok = False
                    End If
                    If LocalPrinters(index).State.Contains("None") Or LocalPrinters(index).State.Contains("Printing") Or LocalPrinters(index).State.Contains("Processing") Then
                        ll.PictureBox1.Image = My.Resources.printer_online_standard_2
                        isok = False
                    End If
                    If isok = True Then
                        ll.PictureBox1.Image = My.Resources.printer_nostat_default_new_2
                    End If
                Else
                    Dim isok As Boolean = True
                    If LocalPrinters(index).State.Contains("Error") Or LocalPrinters(index).State.Contains("PaperJam") Then
                        ll.PictureBox1.Image = My.Resources.printer_error_nonstandard_2
                        isok = False
                    End If
                    If LocalPrinters(index).State.Contains("Offline") Or LocalPrinters(index).State.Contains("Paused") Then
                        ll.PictureBox1.Image = My.Resources.printer_offline_nonstandard_2
                        isok = False
                    End If
                    If LocalPrinters(index).State.Contains("None") Or LocalPrinters(index).State.Contains("Printing") Or LocalPrinters(index).State.Contains("Processing") Then
                        ll.PictureBox1.Image = My.Resources.printer_online_nonstandard_2
                        isok = False
                    End If
                    If isok = True Then
                        ll.PictureBox1.Image = My.Resources.printer_nostat_nodefault_new_2
                    End If
                End If

                ll.LocationLbl.Text = LocalPrinters(index).Location

                Try
                    ll.DescriptionLbl.Text = LocalPrinters(index).Description
                Catch ex As Exception
                End Try
                ll.DriverLbl.Text = LocalPrinters(index).DriverName
                ll.DriverVersionLbl.Text = LocalPrinters(index).DriverVersion

                If LocalPrinters(index).State = "None" Then
                    ll.StateLbl.Text = MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "PrinterReadyText", "")
                Else
                    ll.StateLbl.Text = LocalPrinters(index).State
                End If

                ll.ConfigFileLbl.Text = LocalPrinters(index).Driver.ConfigFile
                ll.DriverPathLbl.Text = LocalPrinters(index).Driver.DriverPath
                ll.DataFileLbl.Text = LocalPrinters(index).Driver.DataFile

                ll.Tag = LocalPrinters(index)
                ll._parent = Me

                If LocalPrinters(index).Server = MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "LocalPrintServerStr", "Local") Then
                    If Not AppSettings.AllowUserDeleteLocalPrinter Then
                        ll.Button1.Enabled = False
                        ll.MetroButton4.Enabled = False
                        ll.MetroButton5.Enabled = False
                        ll.DruckerEntfernenToolStripMenuItem.Enabled = False
                        ll.DruckerNeuInstallierenToolStripMenuItem.Enabled = False
                    End If
                End If

                FlowLayoutPanel1.Controls.Add(ll)
                If AppSettings.ShowDefaultPrinterAlwaysOnTop Then
                    If LocalPrinters(index).DefaultPrinter Then
                        FlowLayoutPanel1.Controls.SetChildIndex(ll, 0)
                    End If
                End If
                Application.DoEvents()
            Next

            FlowLayoutPanel1.Visible = True

            Return True
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
            Return False
        End Try
    End Function

    Public Function FillGUIWithSavedPrinters() As Boolean
        Try
            _Log.Write(ConnectMyPrinterLog.Logging.LogType.Information, Me, "GUI mit gespeicherten Druckern füllen", Err)

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
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
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
            Button3.Enabled = True
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
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
        hh._parentFrm = Me
        hh.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim jj As New AppSettingsDlg
        jj._parent = Me
        jj.ShowDialog()
    End Sub

    Private Sub LocalPrinterChangeTimer_Tick(sender As Object, e As EventArgs) Handles LocalPrinterChangeTimer.Tick
        Try
            If (Application.OpenForms.Count = 1 Or Application.OpenForms.Count = 2) And LoadAllLocalPrinters.IsBusy = False Then
                LocalPrinterIdleWorker.RunWorkerAsync()
                LocalPrinterChangeTimer.Stop()
            Else
            End If
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
        End Try
    End Sub

    Private Sub LocalPrinterIdleWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles LocalPrinterIdleWorker.DoWork
        Try
            Dim result As List(Of PrinterQueueInfo)
            result = LoadLocalPrinters()

            e.Result = result
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
        End Try
    End Sub

    Private Sub LocalPrinterIdleWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LocalPrinterIdleWorker.RunWorkerCompleted
        Try
            Dim jj As List(Of PrinterQueueInfo)
            jj = e.Result

            'Was war der alte Standarddrucker?
            Dim olddef As String = ""
            For index = 0 To jj.Count - 1
                If LocalPrinters(index).DefaultPrinter Then
                    olddef = LocalPrinters(index).ShareName
                    Exit For
                End If
            Next

            'Was ist jetzt der Standarddrucker?
            Dim newdef As String = ""
            For index = 0 To jj.Count - 1
                If jj(index).DefaultPrinter Then
                    newdef = jj(index).ShareName
                    Exit For
                End If
            Next

            If (LocalPrinters.Count = jj.Count) And (newdef = olddef) Then
            Else
                ReloadLocalPrinters()
            End If

            LocalPrinterChangeTimer.Start()
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
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
            tt._parent = Me
            If Not RestartPrinterService.IsBusy = True Then
                RestartPrinterService.RunWorkerAsync(tt)
                tt.Show(Me.Parent)
                Application.DoEvents()
            End If
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
        End Try
    End Sub

    Private Sub RestartPrinterService_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles RestartPrinterService.DoWork
        Try
            Dim hh As New ManagePrinter
            hh.RestartPrinterService()
            e.Result = e.Argument
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
        End Try
    End Sub

    Private Sub RestartPrinterService_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles RestartPrinterService.RunWorkerCompleted
        Try
            Dim tt As ProcessingDlg
            tt = e.Result
            Application.DoEvents()
            tt.Close()
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            Dim qq As New AdvancedPrinterViewDlg
            qq._parent = Me
            qq._AllPrinters = PrintQueues
            qq.Show()
        Catch ex As Exception
        End Try
    End Sub

    Public Sub HandleMouseEnterTextfield()
        If Not AppSettings.AdditionalUserHelpInformationRTF = "" Then
            AdditionalInfoRTF.Visible = True
            MetroTabControl1.Visible = False
            Button3.Visible = False
        End If
    End Sub

    Public Sub HandleMouseLeaveTextfield()
        If Not AppSettings.AdditionalUserHelpInformationRTF = "" Then
            AdditionalInfoRTF.Visible = False
            MetroTabControl1.Visible = True
            Button3.Visible = True
        End If
    End Sub

    Private Sub Form1_MouseEnter(sender As Object, e As EventArgs) Handles MyBase.MouseEnter
        If AppSettings.ShowAdditionalUserHelpOnWindowMouseEnter Then
            HandleMouseEnterTextfield()
        End If
        If AppSettings.ShowAdditionalUserHelpOnTextFieldClick Then
            HandleMouseLeaveTextfield()
        End If
    End Sub

    Private Sub Form1_MouseLeave(sender As Object, e As EventArgs) Handles MyBase.MouseLeave
        If AppSettings.ShowAdditionalUserHelpOnWindowMouseEnter Then
            HandleMouseLeaveTextfield()
        End If
    End Sub

    Public Function OpenForceDriverDeleteApp(ByVal AsAdmin As Boolean) As Boolean
        Try
            If AsAdmin = True Then
                Dim dd As New ConnectMyPrinterElevationLib.ElevationHelper
                dd.RunAppAsAdmin("ConnectMyPrinterForceDelete.exe")
            Else
                Shell("ConnectMyPrinterForceDelete.exe", AppWinStyle.NormalFocus)
            End If

            Return True
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
            Return False
        End Try
    End Function

    Public Function OpenWindowsPrintManagerApp(ByVal AsAdmin As Boolean) As Boolean
        Try
            If AsAdmin = True Then
                Dim dd As New ConnectMyPrinterElevationLib.ElevationHelper
                dd.RunAppAsAdmin("cmd", "", "/C start mmc C:\Windows\system32\printmanagement.msc")
            Else
                Dim kk As New Process
                kk.StartInfo.FileName = "cmd"
                kk.StartInfo.Arguments = "/C start mmc C:\Windows\system32\printmanagement.msc"
                kk.Start()
            End If

            Return True
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
            Return False
        End Try
    End Function

    Public Function OpenSettingsConsoleApp(ByVal AsAdmin As Boolean) As Boolean
        Try
            If AsAdmin = True Then
                Dim dd As New ConnectMyPrinterElevationLib.ElevationHelper
                dd.RunAppAsAdmin("ConnectMyPrinterSettingsConsole.exe")
            Else
                Shell("ConnectMyPrinterSettingsConsole.exe", AppWinStyle.NormalFocus)
            End If

            Return True
        Catch ex As Exception
            _Log.Write(ConnectMyPrinterLog.Logging.LogType._Error, Me, MLangHelper.GetCultureString("ConnectMyPrinter.NET.TranslatedStrings", GetType(Form1), MCultureInf, "ErrorLogStr", "Error"), Err)
            Return False
        End Try
    End Function

    Private Sub DruckerTreiberTreiberpaketEntfernenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DruckerTreiberTreiberpaketEntfernenToolStripMenuItem.Click
        OpenForceDriverDeleteApp(False)
    End Sub

    Private Sub DruckertreiberTreiberpaketEntfernenAdminToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DruckertreiberTreiberpaketEntfernenAdminToolStripMenuItem.Click
        OpenForceDriverDeleteApp(True)
    End Sub

    Private Sub WindowsDruckverwaltungÖffnenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WindowsDruckverwaltungÖffnenToolStripMenuItem.Click
        OpenWindowsPrintManagerApp(False)
    End Sub

    Private Sub WindowsDruckverwaltungÖffnenAdminToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WindowsDruckverwaltungÖffnenAdminToolStripMenuItem.Click
        OpenWindowsPrintManagerApp(True)
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If AppSettings.ShowForceDeletePrinterEntry = False Then
            DruckerTreiberTreiberpaketEntfernenToolStripMenuItem.Visible = False
            DruckertreiberTreiberpaketEntfernenAdminToolStripMenuItem.Visible = False
        End If
        If AppSettings.ShowPrintManagementCenterEntry = False Then
            WindowsDruckverwaltungÖffnenToolStripMenuItem.Visible = False
            WindowsDruckverwaltungÖffnenAdminToolStripMenuItem.Visible = False
        End If
        If AppSettings.ShowAppSettingsConsoleEntry = False Then
            AnwendungseinstellungenBearbeitenToolStripMenuItem.Visible = False
            AnwendungseinstellungenBearbeitenAdminToolStripMenuItem.Visible = False
        End If
    End Sub

    Private Sub AnwendungseinstellungenBearbeitenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AnwendungseinstellungenBearbeitenToolStripMenuItem.Click
        OpenSettingsConsoleApp(False)
    End Sub

    Private Sub AnwendungseinstellungenBearbeitenAdminToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AnwendungseinstellungenBearbeitenAdminToolStripMenuItem.Click
        OpenSettingsConsoleApp(True)
    End Sub

    Private Sub FlowLayoutPanel1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles FlowLayoutPanel1.PreviewKeyDown
        Try
            If e.KeyCode = Keys.Up Then
                ScrollUp(FlowLayoutPanel1, False)
            End If
            If e.KeyCode = Keys.Down Then
                ScrollUp(FlowLayoutPanel1, False)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Function ScrollUp(ByVal ScrollCtl As ScrollableControl, ByVal IsLargeChange As Boolean) As Boolean
        Try
            Dim changeAmount As Integer

            If (IsLargeChange = False) Then
                changeAmount = ScrollCtl.VerticalScroll.SmallChange * 3
            Else
                changeAmount = ScrollCtl.VerticalScroll.LargeChange
            End If

            Dim currentPosition As Integer = ScrollCtl.VerticalScroll.Value

            If ((currentPosition - changeAmount) > ScrollCtl.VerticalScroll.Minimum) Then
                ScrollCtl.VerticalScroll.Value -= changeAmount
            Else
                ScrollCtl.VerticalScroll.Value = ScrollCtl.VerticalScroll.Minimum
            End If

            ScrollCtl.PerformLayout()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
