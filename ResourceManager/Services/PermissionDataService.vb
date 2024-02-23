Imports System.Data
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Design
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions

Namespace ResourceManager.Services
    Public Class PermissionDataService
        Inherits GenericDataService(Of Permission)
        Implements IPermissionDataService
        Public Sub New(ByVal contextFactory As IDesignTimeDbContextFactory(Of ResourceManagerDBContext))
            MyBase.New(contextFactory)
        End Sub
        Public Overrides Async Function GetAll() As Task(Of IList(Of Permission)) Implements IDataService(Of Permission).GetAll
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim entities = Await db.Permissions.OrderBy(Function(x) x.Name).ToListAsync()
                Return entities
            End Using
        End Function
        Public Async Function GetPermissionForRoles(ByVal roles As IList(Of String)) As Task(Of IList(Of String)) Implements IPermissionDataService.GetPermissionForRoles
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim results = Await (From p In db.Permissions
                                     Join r In db.RolePermissions
                                         On r.PermissionID Equals p.ID
                                     Where roles.Contains(r.Role.Name)
                                     Select p.Name).Distinct().ToListAsync()
                Return results
            End Using
        End Function
        Public Async Function GetPermissionObjectForRoleID(ByVal id As Integer) As Task(Of IList(Of Permission)) Implements IPermissionDataService.GetPermissionObjectForRoleID
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim results = Await (From p In db.Permissions
                                     Join r In db.RolePermissions
                                         On p.ID Equals r.PermissionID
                                     Where r.RoleID = id
                                     Order By p.Name
                                     Select p).Distinct().ToListAsync
                Return results
            End Using
        End Function
    End Class
End Namespace
