Imports ResourceManager.Models.Abstractions
Imports System.Collections.Generic

Namespace ResourceManager.Models
    Public Class Role
        Inherits BaseEntity
        Implements IItem
        Public Property Name As String Implements IItem.Name
        Public Property Description As String
        Public Overridable Property UserRoles As ICollection(Of UserRole)
        Public Overridable Property RolePermissions As ICollection(Of RolePermission)
        Public Overrides Function GetCaption() As String
            Return String.Format("{0} with Name '{1}'", NameOf(Role), Name)
        End Function

    End Class
End Namespace
