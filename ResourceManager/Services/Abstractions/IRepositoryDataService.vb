Imports ResourceManager.Models
Imports System.Collections.Generic
Imports System.Threading.Tasks

Namespace ResourceManager.Services.Abstractions
    Public Interface IRepositoryDataService
        Inherits IDataService(Of Repository)
        Function UpdateTags(ByVal repo As Repository, ByVal tags As IList(Of Tag)) As Task(Of Integer)
    End Interface
End Namespace
