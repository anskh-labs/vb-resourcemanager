Imports ResourceManager.Models
Imports System.Collections.Generic
Imports System.Threading.Tasks

Namespace ResourceManager.Services.Abstractions
    Public Interface IBookDataService
        Inherits IDataService(Of Book)
        Function UpdateTags(ByVal book As Book, ByVal tags As IList(Of Tag)) As Task(Of Integer)
    End Interface
End Namespace
