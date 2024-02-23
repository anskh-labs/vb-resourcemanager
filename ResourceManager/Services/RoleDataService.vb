Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Design
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions

Namespace ResourceManager.Services
    Public Class RoleDataService
        Inherits GenericDataService(Of Role)
        Implements IRoleDataService
        Public Sub New(ByVal contextFactory As IDesignTimeDbContextFactory(Of ResourceManagerDBContext))
            MyBase.New(contextFactory)
        End Sub
        Public Overrides Async Function GetAll() As Task(Of IList(Of Role)) Implements IDataService(Of Role).GetAll
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim entities = Await db.Roles.OrderBy(Function(x) x.Name).ToListAsync()
                Return entities
            End Using
        End Function
        Public Async Function GetRoleStringForUserID(ByVal id As Integer) As Task(Of IList(Of String)) Implements IRoleDataService.GetRoleStringForUserID
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim results = Await (From r In db.Roles Join ur In db.UserRoles
                                     On ur.RoleID Equals r.ID
                                     Where ur.UserID = id
                                     Select r.Name).ToListAsync()
                Return results
            End Using
        End Function
        Public Async Function GetRoleObjectForUserID(ByVal id As Integer) As Task(Of IList(Of Role)) Implements IRoleDataService.GetRoleObjectForUserID
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim results = Await (From r In db.Roles Join ur In db.UserRoles On r.ID Equals ur.RoleID
                                     Where ur.UserID = id
                                     Select r Order By r.Name).ToListAsync()
                Return results
            End Using
        End Function
        Public Async Function UpdatePermissions(ByVal role As Role, ByVal perms As IList(Of Permission)) As Task(Of Integer) Implements IRoleDataService.UpdatePermissions
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim r = db.Roles.Include(Function(x) x.RolePermissions).Single(Function(x) x.ID = role.ID)
                db.UpdateManyToMany(r.RolePermissions, perms.Select(Function(x) New RolePermission With {.PermissionID = x.ID, .RoleID = r.ID}), Function(x) x.PermissionID)
                Return Await db.SaveChangesAsync()
            End Using
        End Function
    End Class
End Namespace
