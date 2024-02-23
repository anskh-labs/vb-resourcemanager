Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Design
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions

Namespace ResourceManager.Services
    Friend Class RepositoryDataService
        Inherits GenericDataService(Of Repository)
        Implements IRepositoryDataService
        Public Sub New(ByVal contextFactory As IDesignTimeDbContextFactory(Of ResourceManagerDBContext))
            MyBase.New(contextFactory)
        End Sub
        Public Overrides Async Function GetAll() As Task(Of IList(Of Repository)) Implements IDataService(Of Repository).GetAll
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim entities = Await db.Repositories.Include(Function(x) x.RepositoryTags).ThenInclude(Function(x) x.Tag).OrderBy(Function(x) x.Title).ToListAsync()
                Return entities
            End Using
        End Function
        Public Async Function UpdateTags(ByVal repo As Repository, ByVal tags As IList(Of Tag)) As Task(Of Integer) Implements IRepositoryDataService.UpdateTags
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim p = db.Repositories.Include(Function(x) x.RepositoryTags).Single(Function(x) x.ID = repo.ID)
                db.UpdateManyToMany(p.RepositoryTags, tags.Select(Function(x) New RepositoryTag() With {.RepositoryID = repo.ID, .TagID = x.ID}), Function(x) x.TagID)
                Return Await db.SaveChangesAsync()
            End Using
        End Function
    End Class
End Namespace
