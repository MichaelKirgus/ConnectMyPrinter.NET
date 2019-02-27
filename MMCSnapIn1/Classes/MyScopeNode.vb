Imports System
Imports System.IO
Imports System.Text
Imports System.Collections.Generic
Imports System.Runtime.Serialization.Formatters.Binary
Imports Microsoft.ManagementConsole

Namespace Microsoft.ManagementConsole.SnapIns
    <NodeType(MyScopeNode.Guid, Description:=MyScopeNode.Description)>
    Public Class MyScopeNode
        Inherits ScopeNode

        Public Const Guid As String = "{144fed39-54f9-4c08-94a9-5296f7f0ddf2}"
        Public Const Description As String = "MMCSnapIn1 for MMC3.0"

        Public Sub New()
        End Sub
    End Class
End Namespace
