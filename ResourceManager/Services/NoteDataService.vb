Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Design
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions

Namespace ResourceManager.Services
    Public Class NoteDataService
        Inherits GenericDataService(Of Note)
        Implements INoteDataService
        Public Sub New(ByVal contextFactory As IDesignTimeDbContextFactory(Of ResourceManagerDBContext))
            MyBase.New(contextFactory)
        End Sub
        Public Overrides Async Function GetAll() As Task(Of IList(Of Note)) Implements IDataService(Of Note).GetAll
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim entities = Await db.Notes.Include(Function(x) x.NoteTags).ThenInclude(Function(x) x.Tag).OrderByDescending(Function(x) x.Date).ToListAsync()
                Return entities
            End Using
        End Function
        Public Async Function UpdateTags(ByVal note As Note, ByVal tags As IList(Of Tag)) As Task(Of Integer) Implements INoteDataService.UpdateTags
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim p = db.Notes.Include(Function(x) x.NoteTags).Single(Function(x) x.ID = note.ID)
                db.UpdateManyToMany(p.NoteTags, tags.Select(Function(x) New NoteTag() With {.NoteID = note.ID, .TagID = x.ID}), Function(x) x.TagID)
                Return Await db.SaveChangesAsync()
            End Using
        End Function
    End Class
End Namespace
