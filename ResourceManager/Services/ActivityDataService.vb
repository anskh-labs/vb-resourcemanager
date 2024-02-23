Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Design
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions

Namespace ResourceManager.Services
    Public Class ActivityDataService
        Inherits GenericDataService(Of Activity)
        Implements IActivityDataService
        Public Sub New(ByVal contextFactory As IDesignTimeDbContextFactory(Of ResourceManagerDBContext))
            MyBase.New(contextFactory)
        End Sub
        Public Overrides Async Function GetAll() As Task(Of IList(Of Activity)) Implements IDataService(Of Activity).GetAll
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim entities = Await db.Activities.Include(Function(x) x.ActivityTags).ThenInclude(Function(x) x.Tag).OrderByDescending(Function(x) x.Date).ToListAsync()
                Return entities
            End Using
        End Function
        Public Async Function UpdateTags(ByVal activity As Activity, ByVal tags As IList(Of Tag)) As Task(Of Integer) Implements IActivityDataService.UpdateTags
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim p = db.Activities.Include(Function(x) x.ActivityTags).Single(Function(x) x.ID = activity.ID)
                db.UpdateManyToMany(p.ActivityTags, tags.Select(Function(x) New ActivityTag() With {.ActivityID = activity.ID, .TagID = x.ID}), Function(x) x.TagID)
                Return Await db.SaveChangesAsync()
            End Using
        End Function
    End Class
End Namespace
