Imports Microsoft.EntityFrameworkCore.Metadata.Builders
Imports Microsoft.EntityFrameworkCore

Namespace ResourceManager.Models.Configurations
    Public Class ArticleTagConfiguration
        Implements IEntityTypeConfiguration(Of ArticleTag)
        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of ArticleTag)) Implements IEntityTypeConfiguration(Of ArticleTag).Configure
            builder.ToTable("ArticleTags")
            builder.HasKey(Function(x) New With {x.ArticleID, x.TagID
            })

            builder.HasOne(Function(x) x.Article).WithMany(Function(x) x.ArticleTags).HasForeignKey(Function(x) x.ArticleID)

            builder.HasOne(Function(x) x.Tag).WithMany(Function(x) x.ArticleTags).HasForeignKey(Function(x) x.TagID)

        End Sub
    End Class
End Namespace
