Namespace VBNetCore.DatabaseToolkit.PostgreSQL
    Public Interface IPostgreSQLToolkit
        ''' <summary>
        ''' Restore a PostgreSQL database using pg_restore. Make sure the following appsettings.json properties
        ''' <seecref="ApplicationOptions.PostgreSQLHost"/>, <seecref="ApplicationOptions.PostgreSQLPort"/>,
        ''' and <seecref="ApplicationOptions.PostgreSQLUser"/> are set and match exactly what's in your pgpass.conf file (Windows).
        ''' </summary>
        ''' <paramname="databaseName">The name of the database on the Postgres server we're restoring.</param>
        ''' <paramname="localDatabasePath">The local file path to the .sql database file where we're restoring from.</param>
        Sub RestoreDatabase(ByVal databaseName As String, ByVal localDatabasePath As String)

        ''' <summary>
        ''' Backup a PostgreSQL database using pg_dump. Make sure the following appsettings.json properties
        ''' <seecref="ApplicationOptions.PostgreSQLHost"/>, <seecref="ApplicationOptions.PostgreSQLPort"/>,
        ''' and <seecref="ApplicationOptions.PostgreSQLUser"/> are set and match exactly what's in your pgpass.conf file (Windows).
        ''' </summary>
        ''' <paramname="databaseName">The name of the database on the Postgres server we're backing up.</param>
        ''' <paramname="localDatabasePath">The local file path to the .sql database file where the backup is saved.</param>
        Sub BackupDatabase(ByVal databaseName As String, ByVal localDatabasePath As String)
    End Interface
End Namespace
