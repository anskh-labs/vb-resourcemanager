Imports ResourceManager.Models
Imports System.Collections.Generic
Imports System.Threading.Tasks

Namespace ResourceManager.Services.Abstractions
    Public Interface INoteDataService
        Inherits IDataService(Of Note)
        Function UpdateTags(ByVal note As Note, ByVal tags As IList(Of Tag)) As Task(Of Integer)
    End Interface
End Namespace
