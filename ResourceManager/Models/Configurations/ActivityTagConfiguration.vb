Imports Microsoft.EntityFrameworkCore.Metadata.Builders
Imports Microsoft.EntityFrameworkCore

Namespace ResourceManager.Models.Configurations
    Public Class ActivityTagConfiguration
        Implements IEntityTypeConfiguration(Of ActivityTag)
        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of ActivityTag)) Implements IEntityTypeConfiguration(Of ActivityTag).Configure
            builder.ToTable("ActivityTags")
            builder.HasKey(Function(x) New With {x.ActivityID, x.TagID
            })

            builder.HasOne(Function(x) x.Activity).WithMany(Function(x) x.ActivityTags).HasForeignKey(Function(x) x.ActivityID)

            builder.HasOne(Function(x) x.Tag).WithMany(Function(x) x.ActivityTags).HasForeignKey(Function(x) x.TagID)

        End Sub
    End Class
End Namespace
