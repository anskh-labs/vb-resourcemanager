Imports Microsoft.EntityFrameworkCore.Metadata.Builders
Imports Microsoft.EntityFrameworkCore
Imports System
Imports System.Linq

Namespace ResourceManager.Models.Configurations
    Public Class RepositoryConfiguration
        Implements IEntityTypeConfiguration(Of Repository)
        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of Repository)) Implements IEntityTypeConfiguration(Of Repository).Configure
            builder.ToTable("Repositories")
            builder.HasKey(Function(x) x.ID)

            builder.Property(Function(x) x.ID).ValueGeneratedOnAdd()
            builder.[Property](Function(x) x.CreatedDate).HasDefaultValueSql("date('now','localtime')")
            builder.[Property](Function(x) x.ModifiedDate).HasDefaultValueSql("date('now','localtime')")
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Title), "nvarchar(255)").IsRequired()
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Filename), "nvarchar(255)").IsRequired()
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.FileSize), "int").IsRequired()
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.FileType), "nvarchar(50)").IsRequired()
            builder.HasIndex(CType(Function(x) x.Title, Expressions.Expression(Of Func(Of Repository, Object)))).IsUnique()
        End Sub
    End Class
End Namespace
