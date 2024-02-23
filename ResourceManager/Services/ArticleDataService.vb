Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Design
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions
Imports System.Collections.Generic
Imports System.Threading.Tasks

Namespace ResourceManager.Services
    Public Class ArticleDataService
        Inherits GenericDataService(Of Article)
        Implements IArticleDataService
        Public Sub New(ByVal contextFactory As IDesignTimeDbContextFactory(Of ResourceManagerDBContext))
            MyBase.New(contextFactory)
        End Sub
        Public Overrides Async Function GetAll() As Task(Of IList(Of Article)) Implements IDataService(Of Article).GetAll
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim entities = Await db.Articles.Include(Function(x) x.ArticleTags).ThenInclude(Function(x) x.Tag).OrderBy(Function(x) x.Title).ToListAsync()
                Return entities
            End Using
        End Function
        Public Async Function UpdateTags(ByVal article As Article, ByVal tags As IList(Of Tag)) As Task(Of Integer) Implements IArticleDataService.UpdateTags
            Dim db = MyBase.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim p = db.Articles.Include(Function(x) x.ArticleTags).Single(Function(x) x.ID = article.ID)
                db.UpdateManyToMany(p.ArticleTags, tags.Select(Function(x) New ArticleTag() With {.ArticleID = article.ID, .TagID = x.ID}), Function(x) x.TagID)
                Return Await db.SaveChangesAsync()
            End Using
        End Function
    End Class
End Namespace
