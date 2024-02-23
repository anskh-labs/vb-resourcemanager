Imports ResourceManager.Models.Abstractions
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq

Namespace ResourceManager.Models
    Public Class Repository
        Inherits BaseEntity
        Implements ITags, IEquatable(Of Repository), IComparable(Of Repository)
        Public Property Title As String = String.Empty
        Public Property FileType As String = String.Empty
        Public Property Filename As String = String.Empty

        Public Property FileSize As Integer
        Public ReadOnly Property FilePath As String
            Get
                If Not String.IsNullOrEmpty(Filename) Then
                    Return Path.Combine(Application.Settings.RepositorySettings.FolderPath, Filename)
                End If
                Return Filename
            End Get
        End Property
        Public Property RepositoryTags As ICollection(Of RepositoryTag)

        Public ReadOnly Property TagString As String Implements ITags.TagString
            Get
                Return String.Join(", ", RepositoryTags.Select(Function(x) x.Tag.Name))
            End Get
        End Property
        Public Overrides Function GetCaption() As String
            Return String.Format("{0} with Title '{1}'", NameOf(Repository), Title)
        End Function
#Region "IEquatable<Repository>"
        Public Shadows Function Equals(ByVal other As Repository) As Boolean Implements IEquatable(Of Repository).Equals
            If ReferenceEquals(Nothing, other) Then Return False

            Return Title.Equals(other.Title)
        End Function
#End Region

#Region "IComparable<Repository>"
        Public Function CompareTo(ByVal other As Repository) As Integer Implements IComparable(Of Repository).CompareTo
            If ReferenceEquals(Nothing, other) Then Return -1

            Return Title.CompareTo(other.Title)
        End Function
#End Region
    End Class
End Namespace
