Imports ResourceManager.Models
Imports System.Collections.Generic
Imports System.Threading.Tasks

Namespace ResourceManager.Services.Abstractions
    Public Interface ITagDataService
        Inherits IDataService(Of Tag)
        Function GetTagObjectForPasswordID(ByVal id As Integer) As Task(Of IList(Of Tag))
        Function GetTagObjectForBookID(ByVal id As Integer) As Task(Of IList(Of Tag))
        Function GetTagObjectForArticleID(ByVal id As Integer) As Task(Of IList(Of Tag))
        Function GetTagObjectForRepositoryID(ByVal id As Integer) As Task(Of IList(Of Tag))
        Function GetTagObjectForActivityID(ByVal id As Integer) As Task(Of IList(Of Tag))
        Function GetTagObjectForNoteID(ByVal id As Integer) As Task(Of IList(Of Tag))
    End Interface
End Namespace
