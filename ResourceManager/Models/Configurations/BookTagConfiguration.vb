Imports Microsoft.EntityFrameworkCore.Metadata.Builders
Imports Microsoft.EntityFrameworkCore

Namespace ResourceManager.Models.Configurations
    Public Class BookTagConfiguration
        Implements IEntityTypeConfiguration(Of BookTag)
        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of BookTag)) Implements IEntityTypeConfiguration(Of BookTag).Configure
            builder.ToTable("BookTags")
            builder.HasKey(Function(x) New With {x.BookID, x.TagID
            })

            builder.HasOne(Function(x) x.Book).WithMany(Function(x) x.BookTags).HasForeignKey(Function(x) x.BookID)

            builder.HasOne(Function(x) x.Tag).WithMany(Function(x) x.BookTags).HasForeignKey(Function(x) x.TagID)

        End Sub
    End Class
End Namespace
