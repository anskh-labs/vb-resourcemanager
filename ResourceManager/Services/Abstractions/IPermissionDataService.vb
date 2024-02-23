Imports ResourceManager.Models
Imports System.Collections.Generic
Imports System.Threading.Tasks

Namespace ResourceManager.Services.Abstractions
    Public Interface IPermissionDataService
        Inherits IDataService(Of Permission)
        Function GetPermissionForRoles(ByVal roles As IList(Of String)) As Task(Of IList(Of String))
        Function GetPermissionObjectForRoleID(ByVal iD As Integer) As Task(Of IList(Of Permission))
    End Interface
End Namespace
