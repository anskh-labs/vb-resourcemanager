Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata.Builders

Namespace ResourceManager.Models.Configurations
    Public Class ActivityConfiguration
        Implements IEntityTypeConfiguration(Of Activity)
        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of Activity)) Implements IEntityTypeConfiguration(Of Activity).Configure
            builder.ToTable("Activities")
            builder.HasKey(Function(x) x.ID)

            builder.Property(Function(x) x.ID).ValueGeneratedOnAdd()
            builder.[Property](Function(x) x.CreatedDate).HasDefaultValueSql("date('now','localtime')")
            builder.[Property](Function(x) x.ModifiedDate).HasDefaultValueSql("date('now','localtime')")
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Title), "nvarchar(255)").IsRequired()
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Date), "text").IsRequired()
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Duration), "int").IsRequired()
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.StartTime), "text").IsRequired()
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.EndTime), "text").IsRequired()
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Metric), "nvarchar(50)").IsRequired()
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Quantity), "int").IsRequired()
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Output), "nvarchar(255)").IsRequired(False)
            RelationalPropertyBuilderExtensions.HasColumnType(builder.Property(Function(x) x.Note), "nvarchar(255)").IsRequired(False)
            builder.HasIndex(CType(Function(x) New With {
                x.Date,
                x.Title
            }, Expressions.Expression(Of Func(Of Activity, Object)))).IsUnique()

            builder.HasOne(Function(x) x.User).WithMany(Function(x) x.Activities).HasForeignKey(Function(x) x.UserID)
        End Sub
    End Class
End Namespace
