Imports ResourceManager.Models.Abstractions
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq

Namespace ResourceManager.Models
    Public Class Book
        Inherits BaseEntity
        Implements ITags, IEquatable(Of Book), IComparable(Of Book)
        Public Property Title As String = String.Empty
        Public Property Author As String = String.Empty
        Public Property Publisher As String = String.Empty
        Public Property Filename As String = String.Empty
        Public Property Cover As String = String.Empty
        Public Property Abstraction As String = String.Empty
        Public Property BookTags As ICollection(Of BookTag)

        Public ReadOnly Property TagString As String Implements ITags.TagString
            Get
                Return String.Join(", ", BookTags.Select(Function(x) x.Tag.Name))
            End Get
        End Property
        Public ReadOnly Property FilePath As String
            Get
                If Not String.IsNullOrEmpty(Filename) Then
                    Return Path.Combine(Application.Settings.EbookSettings.FolderPath, Filename)
                End If
                Return Filename
            End Get
        End Property
        Public ReadOnly Property CoverPath As String
            Get
                If Not String.IsNullOrEmpty(Cover) Then
                    Return Path.Combine(Application.Settings.EbookSettings.CoverPath, Cover)
                End If
                Return Cover
            End Get
        End Property
        Public Overrides Function GetCaption() As String
            Return String.Format("{0} with title '{1}'", NameOf(Book), Title)
        End Function

#Region "IEquatable<Book>"
        Public Shadows Function Equals(ByVal other As Book) As Boolean Implements IEquatable(Of Book).Equals
            If ReferenceEquals(Nothing, other) Then Return False

            Return Title.Equals(other.Title)
        End Function
#End Region

#Region "IComparable<Book>"
        Public Function CompareTo(ByVal other As Book) As Integer Implements IComparable(Of Book).CompareTo
            If ReferenceEquals(Nothing, other) Then Return -1

            Return Title.CompareTo(other.Title)
        End Function
#End Region
    End Class
End Namespace
