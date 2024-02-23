Imports ResourceManager.Models.Abstractions
Imports System.Collections.Generic

Namespace ResourceManager.Models
    Public Class Permission
        Inherits BaseEntity
        Implements IItem
        Public Property Name As String = String.Empty Implements IItem.Name
        Public Property Description As String = String.Empty
        Public Overridable Property RolePermissions As ICollection(Of RolePermission)
        Public Overrides Function GetCaption() As String
            Return String.Format("{0} with name '{1}'", NameOf(Permission), Name)
        End Function
    End Class
End Namespace
