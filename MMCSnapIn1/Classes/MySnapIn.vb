Imports System
Imports System.IO
Imports System.Text
Imports System.Collections.Generic
Imports System.Runtime.Serialization.Formatters.Binary
Imports Microsoft.ManagementConsole

Namespace Microsoft.ManagementConsole.SnapIns
    <SnapInSettings(MySnapIn.Guid, Vendor:=MySnapIn.Vendor, Description:=MySnapIn.Description, DisplayName:=MySnapIn.DisplayName)>
    Public Class MySnapIn
        Inherits SnapIn

        Public Const Guid As String = "{7728f349-f3c1-44f6-a6c7-79a3e489911d}"
        Public Const DisplayName As String = "MMCSnapIn1"
        Public Const Description As String = "MMCSnapIn1 for MMC 3.0"
        Public Const Vendor As String = ""
        Public Property PersistenceData As List(Of Object)

        Public Sub New()
        End Sub

        Protected Overrides Sub OnInitialize()
            MyBase.OnInitialize()
            Me.RootNode = New ScopeNode(New Guid(MyScopeNode.Guid), False)
            Me.RootNode.DisplayName = "MMCSnapIn1"
        End Sub

        Protected Overrides Function OnShowInitializationWizard() As Boolean
            Return MyBase.OnShowInitializationWizard()
        End Function

        Protected Overrides Sub OnShutdown(ByVal status As AsyncStatus)
            MyBase.OnShutdown(status)
        End Sub

        Protected Overrides Sub OnLoadCustomData(ByVal status As AsyncStatus, ByVal persistenceData As Byte())
            Using memoryStream As MemoryStream = New MemoryStream()
                memoryStream.Write(persistenceData, 0, persistenceData.Length)
                memoryStream.Seek(0, SeekOrigin.Begin)
                Dim binaryFormatter As BinaryFormatter = New BinaryFormatter()
                Me.PersistenceData = CType(binaryFormatter.Deserialize(memoryStream), List(Of Object))
            End Using
        End Sub

        Protected Overrides Function OnSaveCustomData(ByVal status As SyncStatus) As Byte()
            If Me.PersistenceData Is Nothing Then
                Me.PersistenceData = New List(Of Object)()
            End If

            Using memoryStream As MemoryStream = New MemoryStream()
                Dim binaryFormatter As BinaryFormatter = New BinaryFormatter()
                binaryFormatter.Serialize(memoryStream, Me.PersistenceData)
                Return memoryStream.ToArray()
            End Using
        End Function
    End Class
End Namespace
