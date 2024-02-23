Imports ResourceManager.Models.Abstractions
Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace ResourceManager.Models
    Public Class Activity
        Inherits BaseEntity
        Implements ITags, IEquatable(Of Activity), IComparable(Of Activity)
        Public Property Title As String = String.Empty
        Public Property [Date] As Date
        Public Property StartTime As Date
        Public Property EndTime As Date
        Public Property Duration As Integer
        Public Property Metric As String = String.Empty
        Public Property Quantity As Integer
        Public Property Output As String = String.Empty
        Public Property Note As String = String.Empty
        Public Property UserID As Integer
        Public Property User As User
        Public Property ActivityTags As ICollection(Of ActivityTag)

        Public ReadOnly Property TagString As String Implements ITags.TagString
            Get
                Return String.Join(", ", ActivityTags.Select(Function(x) x.Tag.Name))
            End Get
        End Property

        Public Overrides Function GetCaption() As String
            Return String.Format("{0} with Title {1}", NameOf(Activity), Title)
        End Function

#Region "IEquatable<Activity>"
        Public Shadows Function Equals(ByVal other As Activity) As Boolean Implements IEquatable(Of Activity).Equals
            If ReferenceEquals(Nothing, other) Then Return False

            Return Title.Equals(other.Title) AndAlso [Date].Equals(other.Date)
        End Function
#End Region

#Region "IComparable<Activity>"
        Public Function CompareTo(ByVal other As Activity) As Integer Implements IComparable(Of Activity).CompareTo
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
