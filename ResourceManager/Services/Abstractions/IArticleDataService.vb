Imports ResourceManager.Models
Imports System.Collections.Generic
Imports System.Threading.Tasks

Namespace ResourceManager.Services.Abstractions
    Public Interface IArticleDataService
        Inherits IDataService(Of Article)
        Function UpdateTags(ByVal article As Article, ByVal tags As IList(Of Tag)) As Task(Of Integer)
    End Interface
End Namespace
