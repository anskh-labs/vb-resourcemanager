Imports Microsoft.EntityFrameworkCore.Metadata.Builders
Imports Microsoft.EntityFrameworkCore
Imports System
Imports System.Linq

Namespace ResourceManager.Models.Configurations
    Public Class BookConfiguration
        Implements IEntityTypeConfiguration(Of Book)
        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of Book)) Implements IEntityTypeConfiguration(Of Book).Configure
            builder.ToTable("Books")
            builder.HasKey(Function(x) x.ID)

            builder.Property(Function(x) x.ID).ValueGeneratedOnAdd()
            builder.[Property](Function(x) x.CreatedDate).HasDefaultValueSql("date('now','localtime')")
            builder.[Property](Function(x) x.ModifiedDate).HasDefaultValueSql("date('now','localtime')")
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Title), "nvarchar(255)").IsRequired()
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Author), "nvarchar(255)").IsRequired()
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Publisher), "nvarchar(100)").IsRequired()
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Filename), "nvarchar(255)").IsRequired()
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Cover), "nvarchar(255)").IsRequired(False)
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Abstraction), "nvarchar(255)").IsRequired(False)
            builder.HasIndex(CType(Function(x) x.Title, Expressions.Expression(Of Func(Of Book, Object)))).IsUnique()
        End Sub
    End Class
End Namespace
