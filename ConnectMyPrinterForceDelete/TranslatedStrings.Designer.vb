﻿'------------------------------------------------------------------------------
' <auto-generated>
'     Dieser Code wurde von einem Tool generiert.
'     Laufzeitversion:4.0.30319.42000
'
'     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
'     der Code erneut generiert wird.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'Diese Klasse wurde von der StronglyTypedResourceBuilder automatisch generiert
    '-Klasse über ein Tool wie ResGen oder Visual Studio automatisch generiert.
    'Um einen Member hinzuzufügen oder zu entfernen, bearbeiten Sie die .ResX-Datei und führen dann ResGen
    'mit der /str-Option erneut aus, oder Sie erstellen Ihr VS-Projekt neu.
    '''<summary>
    '''  Eine stark typisierte Ressourcenklasse zum Suchen von lokalisierten Zeichenfolgen usw.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Class TranslatedStrings
        
        Private Shared resourceMan As Global.System.Resources.ResourceManager
        
        Private Shared resourceCulture As Global.System.Globalization.CultureInfo
        
        <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")>  _
        Friend Sub New()
            MyBase.New
        End Sub
        
        '''<summary>
        '''  Gibt die zwischengespeicherte ResourceManager-Instanz zurück, die von dieser Klasse verwendet wird.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Shared ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("ConnectMyPrinterForceDelete.TranslatedStrings", GetType(TranslatedStrings).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Überschreibt die CurrentUICulture-Eigenschaft des aktuellen Threads für alle
        '''  Ressourcenzuordnungen, die diese stark typisierte Ressourcenklasse verwenden.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Shared Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Alle Benutzer ähnelt.
        '''</summary>
        Friend Shared ReadOnly Property AllUsersListStr() As String
            Get
                Return ResourceManager.GetString("AllUsersListStr", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die verbunden ähnelt.
        '''</summary>
        Friend Shared ReadOnly Property ConnectedListStr() As String
            Get
                Return ResourceManager.GetString("ConnectedListStr", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die [Treiberpaket] (Von keinem Drucker genutzt) ähnelt.
        '''</summary>
        Friend Shared ReadOnly Property DriverPacketNotUsedStr() As String
            Get
                Return ResourceManager.GetString("DriverPacketNotUsedStr", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die [Treiberpaket]  ähnelt.
        '''</summary>
        Friend Shared ReadOnly Property DriverPacketStr() As String
            Get
                Return ResourceManager.GetString("DriverPacketStr", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die [Treiber]  ähnelt.
        '''</summary>
        Friend Shared ReadOnly Property DriverStr() As String
            Get
                Return ResourceManager.GetString("DriverStr", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die lokal installiert ähnelt.
        '''</summary>
        Friend Shared ReadOnly Property LocalInstalledStr() As String
            Get
                Return ResourceManager.GetString("LocalInstalledStr", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Die Anwendung wird ohne Admin-Rechte ausgeführt. Drucker können nur für den aktuellen Benutzer gelöscht werden. Dies kann zu Problemen beim Löschen Treiberpaketen sowie Treibern führen. ähnelt.
        '''</summary>
        Friend Shared ReadOnly Property StartWithoutAdministrativeRightsStr() As String
            Get
                Return ResourceManager.GetString("StartWithoutAdministrativeRightsStr", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Benutzer mit normalen Benutzerrechten dürfen diese Anwendung nicht starten. ähnelt.
        '''</summary>
        Friend Shared ReadOnly Property UserWithoutAdministrativeRightsDenyStr() As String
            Get
                Return ResourceManager.GetString("UserWithoutAdministrativeRightsDenyStr", resourceCulture)
            End Get
        End Property
    End Class
End Namespace
