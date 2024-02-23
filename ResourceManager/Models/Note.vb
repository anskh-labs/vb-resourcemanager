Imports ResourceManager.Models.Abstractions
Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace ResourceManager.Models
    Public Class Note
        Inherits BaseEntity
        Implements ITags, IEquatable(Of Note), IComparable(Of Note)
        Public Property Title As String = String.Empty
        Public Property [Date] As Date
        Public Property Notes As String = String.Empty
        Public Property UserID As Integer
        Public Property User As User
        Public Property NoteTags As ICollection(Of NoteTag)

        Public ReadOnly Property TagString As String Implements ITags.TagString
            Get
                Return String.Join(", ", NoteTags.Select(Function(x) x.Tag.Name))
            End Get
        End Property

        Public Overrides Function GetCaption() As String
            Return String.Format("{0} with Title {1}", NameOf(Note), Title)
        End Function

#Region "IEquatable<Note>"
        Public Shadows Function Equals(ByVal other As Note) As Boolean Implements IEquatable(Of Note).Equals
            If ReferenceEquals(Nothing, other) Then Return False

            Return Title.Equals(other.Title) AndAlso [Date].Equals(other.Date)
        End Function
#End Region

#Region "IComparable<Note>"
        Public Function CompareTo(ByVal other As Note) As Integer Implements IComparable(Of Note).CompareTo
            If ReferenceEquals(Nothing, other) Then Return -1
            Dim result = [Date].CompareTo(other.Date)
            If result = 0 Then
                Return Title.CompareTo(other.Title)
            End If
            Return result
        End Function
#End Region
    End Class
End Namespace
