Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata.Builders

Namespace ResourceManager.Models.Configurations
    Public Class ArticleConfiguration
        Implements IEntityTypeConfiguration(Of Article)
        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of Article)) Implements IEntityTypeConfiguration(Of Article).Configure
            builder.ToTable("Articles")
            builder.HasKey(Function(x) x.ID)

            builder.Property(Function(x) x.ID).ValueGeneratedOnAdd()
            builder.[Property](Function(x) x.CreatedDate).HasDefaultValueSql("date('now','localtime')")
            builder.[Property](Function(x) x.ModifiedDate).HasDefaultValueSql("date('now','localtime')")
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Title), "nvarchar(255)").IsRequired()
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Author), "nvarchar(255)").IsRequired()
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Filename), "nvarchar(255)").IsRequired()
            builder.HasIndex(CType(Function(x) x.Title, Expressions.Expression(Of Func(Of Article, Object)))).IsUnique()

        End Sub
    End Class
End Namespace
