Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata.Builders

Namespace ResourceManager.Models.Configurations
    Public Class PasswordConfiguration
        Implements IEntityTypeConfiguration(Of Password)
        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of Password)) Implements IEntityTypeConfiguration(Of Password).Configure
            builder.ToTable("Passwords")
            builder.HasKey(Function(x) x.ID)

            builder.Property(Function(x) x.ID).ValueGeneratedOnAdd()
            builder.[Property](Function(x) x.CreatedDate).HasDefaultValueSql("date('now','localtime')")
            builder.[Property](Function(x) x.ModifiedDate).HasDefaultValueSql("date('now','localtime')")
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Name), "nvarchar(100)").IsRequired()
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Username), "nvarchar(50)").IsRequired()
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Pass), "nvarchar(100)").IsRequired()
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Url), "nvarchar(100)").IsRequired(False)
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Description), "nvarchar(255)").IsRequired(False)
            builder.HasIndex(CType(Function(x) x.Name, Expressions.Expression(Of Func(Of Password, Object)))).IsUnique()

            builder.HasOne(Function(x) x.User).WithMany(Function(x) x.Passwords).HasForeignKey(Function(x) x.UserID)

        End Sub
    End Class
End Namespace
