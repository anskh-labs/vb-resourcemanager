Imports ResourceManager.Models.Abstractions
Imports System.Collections.Generic

Namespace ResourceManager.Models
    Public Class User
        Inherits BaseEntity
        Implements IItem
        Public Property Name As String = String.Empty Implements IItem.Name
        Public Property AccountName As String = String.Empty
        Public Property Password As String = String.Empty
        Public Overridable Property UserRoles As ICollection(Of UserRole)
        Public Overridable Property Passwords As ICollection(Of Password)
        Public Overridable Property Activities As ICollection(Of Activity)
        Public Overridable Property Notes As ICollection(Of Note)
        Public Overrides Function GetCaption() As String
            Return String.Format("{0} with Name '{1}'", NameOf(User), Name)
        End Function

    End Class
End Namespace
