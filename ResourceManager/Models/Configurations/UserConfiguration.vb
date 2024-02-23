Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata.Builders
Imports ResourceManager.Configuration

Namespace ResourceManager.Models.Configurations
    Public Class UserConfiguration
        Implements IEntityTypeConfiguration(Of User)
        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of User)) Implements IEntityTypeConfiguration(Of User).Configure
            builder.ToTable("Users")
            builder.HasKey(Function(x) x.ID)

            builder.Property(Function(x) x.ID).ValueGeneratedOnAdd()
            builder.[Property](Function(x) x.CreatedDate).HasDefaultValueSql("date('now','localtime')")
            builder.[Property](Function(x) x.ModifiedDate).HasDefaultValueSql("date('now','localtime')")
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Name), "nvarchar(100)").IsRequired()
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.AccountName), "nvarchar(50)").IsRequired()
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Password), "nvarchar(100)").IsRequired()
            builder.HasIndex(CType(Function(x) x.Name, Expressions.Expression(Of Func(Of User, Object)))).IsUnique()
            builder.HasIndex(CType(Function(x) x.AccountName, Expressions.Expression(Of Func(Of User, Object)))).IsUnique()

            builder.HasData(DesignData.Instance.Users)
        End Sub
    End Class
End Namespace
