Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata.Builders
Imports ResourceManager.Configuration

Namespace ResourceManager.Models.Configurations
    Public Class PermissionConfiguration
        Implements IEntityTypeConfiguration(Of Permission)
        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of Permission)) Implements IEntityTypeConfiguration(Of Permission).Configure
            builder.ToTable("Permissions")
            builder.HasKey(Function(x) x.ID)

            builder.Property(Function(x) x.ID).ValueGeneratedOnAdd()
            builder.[Property](Function(x) x.CreatedDate).HasDefaultValueSql("date('now','localtime')")
            builder.[Property](Function(x) x.ModifiedDate).HasDefaultValueSql("date('now','localtime')")
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Name), "nvarchar(100)").IsRequired()
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Description), "nvarchar(255)").IsRequired()
            builder.HasIndex(CType(Function(x) x.Name, Expressions.Expression(Of Func(Of Permission, Object)))).IsUnique()

            builder.HasData(DesignData.Instance.Permissions)
        End Sub
    End Class
End Namespace
