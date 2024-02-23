Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Design
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions

Namespace ResourceManager.Services
    Public Class BookDataService
        Inherits GenericDataService(Of Book)
        Implements IBookDataService
        Public Sub New(ByVal contextFactory As IDesignTimeDbContextFactory(Of ResourceManagerDBContext))
            MyBase.New(contextFactory)
        End Sub
        Public Overrides Async Function GetAll() As Task(Of IList(Of Book)) Implements IDataService(Of Book).GetAll
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim entities = Await db.Books.Include(Function(x) x.BookTags).ThenInclude(Function(x) x.Tag).OrderBy(Function(x) x.Title).ToListAsync()
                Return entities
            End Using
        End Function
        Public Async Function UpdateTags(ByVal book As Book, ByVal tags As IList(Of Tag)) As Task(Of Integer) Implements IBookDataService.UpdateTags
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim p = db.Books.Include(Function(x) x.BookTags).Single(Function(x) x.ID = book.ID)
                db.UpdateManyToMany(p.BookTags, tags.Select(Function(x) New BookTag() With {.BookID = book.ID, .TagID = x.ID}), Function(x) x.TagID)
                Return Await db.SaveChangesAsync()
            End Using
        End Function
    End Class
End Namespace
