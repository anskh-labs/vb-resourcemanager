Imports ResourceManager.Models.Abstractions
Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace ResourceManager.Models
    Public Class Password
        Inherits BaseEntity
        Implements IItem, ITags, IEquatable(Of Password), IComparable(Of Password)
        Public Property Name As String = String.Empty Implements IItem.Name
        Public Property Username As String = String.Empty
        Public Property Pass As String = String.Empty
        Public Property Url As String = String.Empty
        Public Property Description As String = String.Empty

        Public Property UserID As Integer
        Public Property User As User
        Public Property PasswordTags As ICollection(Of PasswordTag)

        Public ReadOnly Property TagString As String Implements ITags.TagString
            Get
                Return String.Join(", ", PasswordTags.Select(Function(x) x.Tag.Name))
            End Get
        End Property
        Public Overrides Function GetCaption() As String
            Return String.Format("{0} with name '{1}'", NameOf(Password), Name)
        End Function

#Region "IEquatable<Password>"
        Public Shadows Function Equals(ByVal other As Password) As Boolean Implements IEquatable(Of Password).Equals
            If ReferenceEquals(Nothing, other) Then Return False

            Return Name.Equals(other.Name)
        End Function
#End Region

#Region "IComparable<Password>"
        Public Function CompareTo(ByVal other As Password) As Integer Implements IComparable(Of Password).CompareTo
            If ReferenceEquals(Nothing, other) Then Return -1

            Return Name.CompareTo(other.Name)
        End Function
#End Region
    End Class
End Namespace
