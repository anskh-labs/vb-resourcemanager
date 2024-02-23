Imports Microsoft.EntityFrameworkCore.Metadata.Builders
Imports Microsoft.EntityFrameworkCore

Namespace ResourceManager.Models.Configurations
    Public Class PasswordTagConfiguration
        Implements IEntityTypeConfiguration(Of PasswordTag)
        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of PasswordTag)) Implements IEntityTypeConfiguration(Of PasswordTag).Configure
            builder.ToTable("PasswordTags")
            builder.HasKey(Function(x) New With {x.PasswordID, x.TagID
            })

            builder.HasOne(Function(x) x.Password).WithMany(Function(x) x.PasswordTags).HasForeignKey(Function(x) x.PasswordID)

            builder.HasOne(Function(x) x.Tag).WithMany(Function(x) x.PasswordTags).HasForeignKey(Function(x) x.TagID)

        End Sub
    End Class
End Namespace
