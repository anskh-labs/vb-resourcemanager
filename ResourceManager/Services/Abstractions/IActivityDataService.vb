Imports ResourceManager.Models
Imports System.Collections.Generic
Imports System.Threading.Tasks

Namespace ResourceManager.Services.Abstractions
    Public Interface IActivityDataService
        Inherits IDataService(Of Activity)
        Function UpdateTags(ByVal activity As Activity, ByVal tags As IList(Of Tag)) As Task(Of Integer)
    End Interface
End Namespace
