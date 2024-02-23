Imports Microsoft.EntityFrameworkCore.Metadata.Builders
Imports Microsoft.EntityFrameworkCore

Namespace ResourceManager.Models.Configurations
    Public Class RepositoryTagConfiguration
        Implements IEntityTypeConfiguration(Of RepositoryTag)
        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of RepositoryTag)) Implements IEntityTypeConfiguration(Of RepositoryTag).Configure
            builder.ToTable("RepositoryTags")
            builder.HasKey(Function(x) New With {x.RepositoryID, x.TagID
            })

            builder.HasOne(Function(x) x.Repository).WithMany(Function(x) x.RepositoryTags).HasForeignKey(Function(x) x.RepositoryID)

            builder.HasOne(Function(x) x.Tag).WithMany(Function(x) x.RepositoryTags).HasForeignKey(Function(x) x.TagID)

        End Sub
    End Class
End Namespace
