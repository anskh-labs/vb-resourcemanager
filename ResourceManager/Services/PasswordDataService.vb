Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Design
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions

Namespace ResourceManager.Services
    Public Class PasswordDataService
        Inherits GenericDataService(Of Password)
        Implements IPasswordDataService
        Public Sub New(ByVal contextFactory As IDesignTimeDbContextFactory(Of ResourceManagerDBContext))
            MyBase.New(contextFactory)
        End Sub
        Public Overrides Async Function GetAll() As Task(Of IList(Of Password)) Implements IDataService(Of Password).GetAll
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim entities = Await db.Passwords.Include(Function(x) x.PasswordTags).ThenInclude(Function(x) x.Tag).OrderBy(Function(x) x.Name).ToListAsync()
                Return entities
            End Using
        End Function
        Public Async Function UpdateTags(ByVal pass As Password, ByVal tags As IList(Of Tag)) As Task(Of Integer) Implements IPasswordDataService.UpdateTags
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim p = db.Passwords.Include(Function(x) x.PasswordTags).Single(Function(x) x.ID = pass.ID)
                db.UpdateManyToMany(p.PasswordTags, tags.Select(Function(x) New PasswordTag() With {.PasswordID = pass.ID, .TagID = x.ID}), Function(x) x.TagID)
                Return Await db.SaveChangesAsync()
            End Using
        End Function
    End Class
End Namespace
