Imports ResourceManager.Models
Imports System.Collections.Generic
Imports System.Threading.Tasks

Namespace ResourceManager.Services.Abstractions
    Public Interface IUserDataService
        Inherits IDataService(Of User)
        Function UpdateRoles(ByVal user As User, ByVal roles As IList(Of Role)) As Task(Of Integer)
    End Interface
End Namespace
