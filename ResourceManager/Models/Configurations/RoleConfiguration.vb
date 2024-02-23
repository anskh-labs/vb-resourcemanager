Imports Microsoft.EntityFrameworkCore.Metadata.Builders
Imports Microsoft.EntityFrameworkCore
Imports ResourceManager.Configuration

Namespace ResourceManager.Models.Configurations
    Public Class RoleConfiguration
        Implements IEntityTypeConfiguration(Of Role)
        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of Role)) Implements IEntityTypeConfiguration(Of Role).Configure
            builder.ToTable("Roles")
            builder.HasKey(Function(x) x.ID)

            builder.Property(Function(x) x.ID).ValueGeneratedOnAdd()
            builder.[Property](Function(x) x.CreatedDate).HasDefaultValueSql("date('now','localtime')")
            builder.[Property](Function(x) x.ModifiedDate).HasDefaultValueSql("date('now','localtime')")
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Name), "nvarchar(100)").IsRequired()
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Description), "nvarchar(255)").IsRequired()
            builder.HasIndex(CType(Function(x) x.Name, Expressions.Expression(Of Func(Of Role, Object)))).IsUnique()

            builder.HasData(DesignData.Instance.Roles)
        End Sub
    End Class
End Namespace
