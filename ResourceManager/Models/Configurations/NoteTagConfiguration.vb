Imports Microsoft.EntityFrameworkCore.Metadata.Builders
Imports Microsoft.EntityFrameworkCore

Namespace ResourceManager.Models.Configurations
    Public Class NoteTagConfiguration
        Implements IEntityTypeConfiguration(Of NoteTag)
        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of NoteTag)) Implements IEntityTypeConfiguration(Of NoteTag).Configure
            builder.ToTable("NoteTags")
            builder.HasKey(Function(x) New With {x.NoteID, x.TagID
            })

            builder.HasOne(Function(x) x.Note).WithMany(Function(x) x.NoteTags).HasForeignKey(Function(x) x.NoteID)

            builder.HasOne(Function(x) x.Tag).WithMany(Function(x) x.NoteTags).HasForeignKey(Function(x) x.TagID)

        End Sub
    End Class
End Namespace
