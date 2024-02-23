Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata.Builders

Namespace ResourceManager.Models.Configurations
    Public Class NoteConfiguration
        Implements IEntityTypeConfiguration(Of Note)
        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of Note)) Implements IEntityTypeConfiguration(Of Note).Configure
            builder.ToTable("Notes")
            builder.HasKey(Function(x) x.ID)

            builder.Property(Function(x) x.ID).ValueGeneratedOnAdd()
            builder.[Property](Function(x) x.CreatedDate).HasDefaultValueSql("date('now','localtime')")
            builder.[Property](Function(x) x.ModifiedDate).HasDefaultValueSql("date('now','localtime')")
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Title), "nvarchar(255)").IsRequired()
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Date), "text").IsRequired()
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Notes), "nvarchar(500)").IsRequired()
            builder.HasIndex(CType(Function(x) New With {
                x.Date,
                x.Title
            }, Expressions.Expression(Of Func(Of Note, Object)))).IsUnique()

            builder.HasOne(Function(x) x.User).WithMany(Function(x) x.Notes).HasForeignKey(Function(x) x.UserID)
        End Sub
    End Class
End Namespace
