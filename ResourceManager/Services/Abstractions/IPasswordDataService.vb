Imports ResourceManager.Models
Imports System.Collections.Generic
Imports System.Threading.Tasks

Namespace ResourceManager.Services.Abstractions
    Public Interface IPasswordDataService
        Inherits IDataService(Of Password)
        Function UpdateTags(ByVal pass As Password, ByVal tags As IList(Of Tag)) As Task(Of Integer)
    End Interface
End Namespace
