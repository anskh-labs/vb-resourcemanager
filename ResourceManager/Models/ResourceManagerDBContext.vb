Imports Microsoft.EntityFrameworkCore
Imports ResourceManager.Models.Configurations
Imports System.Linq
Imports System.Threading
Imports System.Threading.Tasks

Namespace ResourceManager.Models
    Public Class ResourceManagerDBContext
        Inherits DbContext
        Public Sub New(ByVal options As DbContextOptions)
            MyBase.New(options)
        End Sub

        Public Property Users As DbSet(Of User)
        Public Property Tags As DbSet(Of Tag)
        Public Property Roles As DbSet(Of Role)
        Public Property Permissions As DbSet(Of Permission)
        Public Property RolePermissions As DbSet(Of RolePermission)
        Public Property Passwords As DbSet(Of Password)
        Public Property PasswordTags As DbSet(Of PasswordTag)
        Public Property UserRoles As DbSet(Of UserRole)
        Public Property Books As DbSet(Of Book)
        Public Property BookTags As DbSet(Of BookTag)
        Public Property Articles As DbSet(Of Article)
        Public Property ArticleTags As DbSet(Of ArticleTag)
        Public Property Activities As DbSet(Of Activity)
        Public Property ActivityTags As DbSet(Of ActivityTag)
        Public Property Repositories As DbSet(Of Repository)
        Public Property RepositoryTags As DbSet(Of RepositoryTag)
        Public Property Notes As DbSet(Of Note)
        Public Property NoteTags As DbSet(Of NoteTag)
        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As ModelBuilder)
            modelBuilder.ApplyConfiguration(New UserConfiguration())
            modelBuilder.ApplyConfiguration(New RoleConfiguration())
            modelBuilder.ApplyConfiguration(New TagConfiguration())
            modelBuilder.ApplyConfiguration(New PermissionConfiguration())
            modelBuilder.ApplyConfiguration(New RolePermissionConfiguration())
            modelBuilder.ApplyConfiguration(New UserRoleConfiguration())
            modelBuilder.ApplyConfiguration(New PasswordConfiguration())
            modelBuilder.ApplyConfiguration(New PasswordTagConfiguration())
            modelBuilder.ApplyConfiguration(New BookConfiguration())
            modelBuilder.ApplyConfiguration(New BookTagConfiguration())
            modelBuilder.ApplyConfiguration(New ActivityConfiguration())
            modelBuilder.ApplyConfiguration(New ActivityTagConfiguration())
            modelBuilder.ApplyConfiguration(New ArticleConfiguration())
            modelBuilder.ApplyConfiguration(New ArticleTagConfiguration())
            modelBuilder.ApplyConfiguration(New RepositoryConfiguration())
            modelBuilder.ApplyConfiguration(New RepositoryTagConfiguration())
            modelBuilder.ApplyConfiguration(New NoteConfiguration())
            modelBuilder.ApplyConfiguration(New NoteTagConfiguration())
        End Sub
        Public Overrides Function SaveChanges() As Integer
            Dim entries = MyBase.ChangeTracker.Entries().Where(Function(e) TypeOf e.Entity Is BaseEntity AndAlso (e.State = EntityState.Added OrElse e.State = EntityState.Modified))

            For Each entityEntry In entries
                CType(entityEntry.Entity, BaseEntity).ModifiedDate = Date.Now

                If entityEntry.State = EntityState.Added Then
                    CType(entityEntry.Entity, BaseEntity).CreatedDate = Date.Now
                End If
            Next

            Return MyBase.SaveChanges()
        End Function

        Public Overrides Function SaveChangesAsync(ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of Integer)
            Dim entries = MyBase.ChangeTracker.Entries().Where(Function(e) TypeOf e.Entity Is BaseEntity AndAlso (e.State = EntityState.Added OrElse e.State = EntityState.Modified))

            For Each entityEntry In entries
                CType(entityEntry.Entity, BaseEntity).ModifiedDate = Date.Now

                If entityEntry.State = EntityState.Added Then
                    CType(entityEntry.Entity, BaseEntity).CreatedDate = Date.Now
                End If
            Next

            Return MyBase.SaveChangesAsync(cancellationToken)
        End Function
    End Class
End Namespace
