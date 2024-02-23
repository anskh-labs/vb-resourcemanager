Imports Microsoft.EntityFrameworkCore.Metadata.Builders
Imports Microsoft.EntityFrameworkCore
Imports ResourceManager.Configuration

Namespace ResourceManager.Models.Configurations
    Public Class UserRoleConfiguration
        Implements IEntityTypeConfiguration(Of UserRole)
        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of UserRole)) Implements IEntityTypeConfiguration(Of UserRole).Configure
            builder.ToTable("UserRoles")
            builder.HasKey(Function(x) New With {x.UserID, x.RoleID
            })

            builder.HasOne(Function(x) x.User).WithMany(Function(x) x.UserRoles).HasForeignKey(Function(x) x.UserID)

            builder.HasOne(Function(x) x.Role).WithMany(Function(x) x.UserRoles).HasForeignKey(Function(x) x.RoleID)

            builder.HasData(DesignData.Instance.UserRoles)
        End Sub
    End Class
End Namespace
