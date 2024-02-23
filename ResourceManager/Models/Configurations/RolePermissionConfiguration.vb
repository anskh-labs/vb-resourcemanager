Imports Microsoft.EntityFrameworkCore.Metadata.Builders
Imports Microsoft.EntityFrameworkCore
Imports ResourceManager.Configuration

Namespace ResourceManager.Models.Configurations
    Public Class RolePermissionConfiguration
        Implements IEntityTypeConfiguration(Of RolePermission)
        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of RolePermission)) Implements IEntityTypeConfiguration(Of RolePermission).Configure
            builder.ToTable("RolePermissions")
            builder.HasKey(Function(x) New With {x.RoleID, x.PermissionID
            })

            builder.HasOne(Function(x) x.Permission).WithMany(Function(x) x.RolePermissions).HasForeignKey(Function(x) x.PermissionID)

            builder.HasOne(Function(x) x.Role).WithMany(Function(x) x.RolePermissions).HasForeignKey(Function(x) x.RoleID)

            builder.HasData(DesignData.Instance.RolePermissions)
        End Sub
    End Class
End Namespace
