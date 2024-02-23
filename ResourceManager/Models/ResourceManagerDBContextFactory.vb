Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Design
Imports Microsoft.Extensions.Options
Imports ResourceManager.Settings

Namespace ResourceManager.Models
    Public Class ResourceManagerDBContextFactory
        Implements IDesignTimeDbContextFactory(Of ResourceManagerDBContext)
        Private ReadOnly dbConnectionString As String
        Public Sub New(ByVal options As IOptionsMonitor(Of DBSettings))
            dbConnectionString = options.CurrentValue.Sqlite
        End Sub

        Public Function CreateDbContext(args() As String) As ResourceManagerDBContext Implements IDesignTimeDbContextFactory(Of ResourceManagerDBContext).CreateDbContext
            Dim Options = New DbContextOptionsBuilder(Of ResourceManagerDBContext)
            Options.EnableSensitiveDataLogging(True)
            Options.UseSqlite(Me.dbConnectionString)
            Return New ResourceManagerDBContext(Options.Options)
        End Function
    End Class
End Namespace
