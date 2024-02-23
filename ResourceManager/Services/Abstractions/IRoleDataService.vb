Imports ResourceManager.Models
Imports System.Collections.Generic
Imports System.Threading.Tasks

Namespace ResourceManager.Services.Abstractions
    Public Interface IRoleDataService
        Inherits IDataService(Of Role)
        Function GetRoleStringForUserID(ByVal iD As Integer) As Task(Of IList(Of String))
        Function GetRoleObjectForUserID(ByVal iD As Integer) As Task(Of IList(Of Role))
        Function UpdatePermissions(ByVal role As Role, ByVal permissions As IList(Of Permission)) As Task(Of Integer)
    End Interface
End Namespace
