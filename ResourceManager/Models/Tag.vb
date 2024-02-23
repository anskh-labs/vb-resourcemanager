Imports ResourceManager.Models.Abstractions
Imports System.Collections.Generic

Namespace ResourceManager.Models
    Public Class Tag
        Inherits BaseEntity
        Implements IItem
        Public Property Name As String = String.Empty Implements IItem.Name
        Public Overridable Property PasswordTags As ICollection(Of PasswordTag)
        Public Overridable Property BookTags As ICollection(Of BookTag)
        Public Overridable Property ArticleTags As ICollection(Of ArticleTag)
        Public Overridable Property ActivityTags As ICollection(Of ActivityTag)
        Public Overridable Property RepositoryTags As ICollection(Of RepositoryTag)
        Public Overridable Property NoteTags As ICollection(Of NoteTag)

        Public ReadOnly Property PasswordCount As Integer
            Get
                Return PasswordTags.Count
            End Get
        End Property
        Public ReadOnly Property BookCount As Integer
            Get
                Return BookTags.Count
            End Get
        End Property
        Public ReadOnly Property ArticleCount As Integer
            Get
                Return ArticleTags.Count
            End Get
        End Property
        Public ReadOnly Property ActivityCount As Integer
            Get
                Return ActivityTags.Count
            End Get
        End Property
        Public ReadOnly Property RepositoryCount As Integer
            Get
                Return RepositoryTags.Count
            End Get
        End Property
        Public ReadOnly Property NoteCount As Integer
            Get
                Return NoteTags.Count
            End Get
        End Property

        Public Overrides Function GetCaption() As String
            Return String.Format("{0} with Name '{1}'", NameOf(Tag), Name)
        End Function
    End Class
End Namespace
