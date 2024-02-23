Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata.Builders

Namespace ResourceManager.Models.Configurations
    Public Class TagConfiguration
        Implements IEntityTypeConfiguration(Of Tag)
        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of Tag)) Implements IEntityTypeConfiguration(Of Tag).Configure
            builder.ToTable("Tags")
            builder.HasKey(Function(x) x.ID)

            builder.Property(Function(x) x.ID).ValueGeneratedOnAdd()
            builder.[Property](Function(x) x.CreatedDate).HasDefaultValueSql("date('now', 'localtime')")
            builder.[Property](Function(x) x.ModifiedDate).HasDefaultValueSql("date('now', 'localtime')")
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Name), "nvarchar(100)").IsRequired()
            builder.HasIndex(CType(Function(x) x.Name, Expressions.Expression(Of Func(Of Tag, Object)))).IsUnique()
        End Sub
    End Class
End Namespace
