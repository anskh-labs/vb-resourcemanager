Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Design
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions

Namespace ResourceManager.Services
    Public Class UserDataService
        Inherits GenericDataService(Of User)
        Implements IUserDataService
        Public Sub New(ByVal contextFactory As IDesignTimeDbContextFactory(Of ResourceManagerDBContext))
            MyBase.New(contextFactory)
        End Sub
        Public Overrides Async Function GetAll() As Task(Of IList(Of User)) Implements IDataService(Of User).GetAll
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim entities = Await db.Users.OrderBy(Function(x) x.Name).ToListAsync()
                Return entities
            End Using
        End Function
        Public Async Function UpdateRoles(ByVal user As User, ByVal roles As IList(Of Role)) As Task(Of Integer) Implements IUserDataService.UpdateRoles
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim u = db.Users.Include(Function(x) x.UserRoles).Single(Function(x) x.ID = user.ID)
                db.UpdateManyToMany(u.UserRoles, roles.Select(Function(x) New UserRole With {.RoleID = x.ID, .UserID = u.ID}), Function(x) x.RoleID)
                Return Await db.SaveChangesAsync()
            End Using
        End Function
    End Class
End Namespace
