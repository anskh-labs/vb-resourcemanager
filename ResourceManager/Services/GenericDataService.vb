Imports System.Linq.Expressions
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Design
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions

Namespace ResourceManager.Services
    Public Class GenericDataService(Of T As BaseEntity)
        Implements IDataService(Of T)
        Protected dbFactory As IDesignTimeDbContextFactory(Of ResourceManagerDBContext)

        Public Sub New(ByVal contextFactory As IDesignTimeDbContextFactory(Of ResourceManagerDBContext))
            dbFactory = contextFactory
        End Sub

        Public Overridable Async Function CountAll() As Task(Of Integer) Implements IDataService(Of T).CountAll
            Dim db = Me.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim count = Await db.Set(Of T).CountAsync()
                Return count
            End Using
        End Function

        Public Overridable Async Function CountWhere(ByVal predicate As Expression(Of Func(Of T, Boolean))) As Task(Of Integer) Implements IDataService(Of T).CountWhere
            Dim db = Me.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim count = Await db.Set(Of T).CountAsync(predicate)
                Return count
            End Using
        End Function

        Public Overridable Async Function Create(ByVal entity As T) As Task(Of T) Implements IDataService(Of T).Create
            Dim db = Me.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim result = Await db.Set(Of T).AddAsync(entity)
                Await db.SaveChangesAsync
                Return result.Entity
            End Using
        End Function

        Public Overridable Async Function Delete(ByVal id As Integer) As Task(Of Boolean) Implements IDataService(Of T).Delete
            Dim db = Me.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim entity = Await db.Set(Of T).FirstOrDefaultAsync(Function(e) e.ID = id)
                If entity IsNot Nothing Then
                    db.Set(Of T).Remove(entity)
                    Await db.SaveChangesAsync
                    Return True
                End If
                Return False
            End Using
        End Function

        Public Overridable Async Function SingleOrDefault(ByVal predicate As Expression(Of Func(Of T, Boolean))) As Task(Of T) Implements IDataService(Of T).SingleOrDefault
            Dim db = Me.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim entity = Await db.Set(Of T).SingleOrDefaultAsync(predicate)
                Return entity
            End Using
        End Function

        Public Overridable Async Function FirstOrDefault(ByVal predicate As Expression(Of Func(Of T, Boolean))) As Task(Of T) Implements IDataService(Of T).FirstOrDefault
            Dim db = Me.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim entity = Await db.Set(Of T).FirstOrDefaultAsync(predicate)
                Return entity
            End Using
        End Function

        Public Overridable Async Function [Get](ByVal id As Integer) As Task(Of T) Implements IDataService(Of T).Get
            Dim db = Me.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim entity = Await db.Set(Of T).FirstAsync(Function(e) e.ID = id)
                Return entity
            End Using
        End Function

        Public Overridable Async Function GetAll() As Task(Of IList(Of T)) Implements IDataService(Of T).GetAll
            Dim db = Me.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim entities = Await db.Set(Of T).AsNoTracking().ToListAsync()
                Return entities
            End Using
        End Function

        Public Overridable Async Function GetWhere(ByVal predicate As Expression(Of Func(Of T, Boolean))) As Task(Of IList(Of T)) Implements IDataService(Of T).GetWhere
            Dim db = Me.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim entities = Await db.Set(Of T).Where(predicate).ToListAsync()
                Return entities
            End Using
        End Function

        Public Overridable Async Function GetWithRawSql(ByVal query As String, ParamArray parameters As Object()) As Task(Of IList(Of T)) Implements IDataService(Of T).GetWithRawSql
            Dim db = Me.dbFactory.CreateDbContext(Nothing)
            Using db
                Dim entities = Await db.Set(Of T).FromSqlRaw(query, parameters).ToListAsync()
                Return entities
            End Using
        End Function

        Public Overridable Async Function Update(ByVal id As Integer, ByVal entity As T) As Task(Of T) Implements IDataService(Of T).Update
            Dim db = Me.dbFactory.CreateDbContext(Nothing)
            Using db
                entity.ID = id
                db.Set(Of T).Update(entity)
                Await db.SaveChangesAsync
                Return entity
            End Using
        End Function
    End Class
End Namespace
