Imports System.Linq.Expressions
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Design
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions

Namespace ResourceManager.Services
    Public Class TagDataService
        Inherits GenericDataService(Of Tag)
        Implements ITagDataService
        Public Sub New(ByVal contextFactory As IDesignTimeDbContextFactory(Of ResourceManagerDBContext))
            MyBase.New(contextFactory)
        End Sub
        Public Overrides Async Function GetAll() As Task(Of IList(Of Tag)) Implements IDataService(Of Tag).GetAll
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim entities = Await db.Tags.Include(Function(x) x.PasswordTags) _
                    .Include(Function(x) x.RepositoryTags) _
                    .Include(Function(x) x.ActivityTags) _
                    .Include(Function(x) x.ArticleTags) _
                    .Include(Function(x) x.BookTags) _
                    .Include(Function(x) x.NoteTags) _
                    .AsNoTracking _
                    .OrderBy(Function(x) x.Name).ToListAsync
                Return entities
            End Using
        End Function
        Public Overrides Async Function GetWhere(ByVal predicate As Expression(Of Func(Of Tag, Boolean))) As Task(Of IList(Of Tag)) Implements IDataService(Of Tag).GetWhere
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim entities = Await db.Tags.Include(Function(x) x.PasswordTags) _
                    .Include(Function(x) x.RepositoryTags) _
                    .Include(Function(x) x.ActivityTags) _
                    .Include(Function(x) x.ArticleTags) _
                    .Include(Function(x) x.BookTags) _
                    .Include(Function(x) x.NoteTags) _
                    .AsNoTracking _
                    .Where(predicate) _
                    .OrderBy(Function(x) x.Name).ToListAsync
                Return entities
            End Using
        End Function
        Public Async Function GetTagObjectForPasswordID(ByVal id As Integer) As Task(Of IList(Of Tag)) Implements ITagDataService.GetTagObjectForPasswordID
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim results = Await (From t In db.Tags Join ur In db.PasswordTags
                                                           On t.ID Equals ur.TagID
                                     Where ur.PasswordID = id
                                     Order By t.Name
                                     Select t).ToListAsync
                Return results
            End Using
        End Function
        Public Async Function GetTagObjectForBookID(ByVal id As Integer) As Task(Of IList(Of Tag)) Implements ITagDataService.GetTagObjectForBookID
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim results = Await (From t In db.Tags Join ur In db.BookTags
                                                           On t.ID Equals ur.TagID
                                     Where ur.BookID = id
                                     Order By t.Name
                                     Select t).ToListAsync()
                Return results
            End Using
        End Function
        Public Async Function GetTagObjectForArticleID(ByVal id As Integer) As Task(Of IList(Of Tag)) Implements ITagDataService.GetTagObjectForArticleID
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim results = Await (From t In db.Tags Join ur In db.ArticleTags
                                                           On t.ID Equals ur.TagID
                                     Where ur.ArticleID = id
                                     Order By t.Name
                                     Select t).ToListAsync
                Return results
            End Using
        End Function
        Public Async Function GetTagObjectForActivityID(ByVal id As Integer) As Task(Of IList(Of Tag)) Implements ITagDataService.GetTagObjectForActivityID
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim results = Await (From t In db.Tags Join ur In db.ActivityTags
                                                           On t.ID Equals ur.TagID
                                     Where ur.ActivityID = id
                                     Order By t.Name
                                     Select t).ToListAsync
                Return results
            End Using
        End Function
        Public Async Function GetTagObjectForRepositoryID(ByVal id As Integer) As Task(Of IList(Of Tag)) Implements ITagDataService.GetTagObjectForRepositoryID
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim results = Await (From t In db.Tags Join ur In db.RepositoryTags
                                                           On t.ID Equals ur.TagID
                                     Where ur.RepositoryID = id
                                     Order By t.Name
                                     Select t).ToListAsync
                Return results
            End Using
        End Function
        Public Async Function GetTagObjectForNoteID(ByVal id As Integer) As Task(Of IList(Of Tag)) Implements ITagDataService.GetTagObjectForNoteID
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim results = Await (From t In db.Tags Join ur In db.NoteTags
                                                           On t.ID Equals ur.TagID
                                     Where ur.NoteID = id
                                     Order By t.Name
                                     Select t).ToListAsync
                Return results
            End Using
        End Function
    End Class
End Namespace
