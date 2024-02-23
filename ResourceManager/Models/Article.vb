Imports ResourceManager.Models.Abstractions
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq

Namespace ResourceManager.Models
    Public Class Article
        Inherits BaseEntity
        Implements ITags, IEquatable(Of Article), IComparable(Of Article)
        Public Property Title As String = String.Empty
        Public Property Author As String = String.Empty
        Public Property Filename As String = String.Empty
        Public ReadOnly Property FilePath As String
            Get
                If Not String.IsNullOrEmpty(Filename) Then
                    Return Path.Combine(Application.Settings.ArticleSettings.FolderPath, Filename)
                End If
                Return Filename
            End Get
        End Property
        Public Overrides Function GetCaption() As String
            Return String.Format("{0} with title '{1}'", NameOf(Article), Title)
        End Function
        Public Property ArticleTags As ICollection(Of ArticleTag)

        Public ReadOnly Property TagString As String Implements ITags.TagString
            Get
                Return String.Join(",", ArticleTags.Select(Function(x) x.Tag.Name))
            End Get
        End Property
#Region "IEquatable<Article>"
        Public Shadows Function Equals(ByVal other As Article) As Boolean Implements IEquatable(Of Article).Equals
            If ReferenceEquals(Nothing, other) Then Return False

            Return Title.Equals(other.Title)
        End Function
#End Region

#Region "IComparable<Article>"
        Public Function CompareTo(ByVal other As Article) As Integer Implements IComparable(Of Article).CompareTo
            If ReferenceEquals(Nothing, other) Then Return -1

            Return Title.CompareTo(other.Title)
        End Function
#End Region
    End Class
End Namespace
