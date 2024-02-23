Imports System.Diagnostics
Imports System.IO

Namespace VBNetCore.DatabaseToolkit.PostgreSQL
    Friend Class PostgreSQLToolkit
        Implements IPostgreSQLToolkit
        ''' <summary>
        ''' Backup a PostgreSQL database using pg_dump. Make sure the following appsettings.json properties
        ''' <seecref="ApplicationOptions.PostgreSQLHost"/>, <seecref="ApplicationOptions.PostgreSQLPort"/>,
        ''' and <seecref="ApplicationOptions.PostgreSQLUser"/> are set and match exactly what's in your pgpass.conf file (Windows).
        ''' </summary>
        ''' <paramname="databaseName">The name of the database on the Postgres server we're backing up.</param>
        ''' <paramname="localDatabasePath">The local file path to the .sql database file where the backup is saved.</param>
        Public Sub BackupDatabase(ByVal databaseName As String, ByVal localDatabasePath As String) Implements IPostgreSQLToolkit.BackupDatabase
            Dim process = New Process()
            Dim startInfo = New ProcessStartInfo()
            startInfo.FileName = Path.Combine("PostgreSQL", "postgresql-backup.bat")
            Dim host = String.Empty
            Dim port = String.Empty
            Dim user = String.Empty
            Dim database = databaseName
            Dim outputPath = localDatabasePath

            ' use pg_dump, specifying the host, port, user, database to back up, and the output path.
            ' the host, port, user, and database must be an exact match with what's inside your pgpass.conf (Windows)
            startInfo.Arguments = $"{host} {port} {user} {database} ""{outputPath}"""
            startInfo.CreateNoWindow = True
            startInfo.UseShellExecute = False
            process.StartInfo = startInfo
            process.Start()
            process.WaitForExit()
            process.Close()
        End Sub

        ''' <summary>
        ''' Restore a PostgreSQL database using pg_restore. Make sure the following appsettings.json properties
        ''' <seecref="ApplicationOptions.PostgreSQLHost"/>, <seecref="ApplicationOptions.PostgreSQLPort"/>,
        ''' and <seecref="ApplicationOptions.PostgreSQLUser"/> are set and match exactly what's in your pgpass.conf file (Windows).
        ''' </summary>
        ''' <paramname="databaseName">The name of the database on the Postgres server we're restoring.</param>
        ''' <paramname="localDatabasePath">The local file path to the .sql database file where we're restoring from.</param>
        Public Sub RestoreDatabase(ByVal databaseName As String, ByVal localDatabasePath As String) Implements IPostgreSQLToolkit.RestoreDatabase
            Dim process = New Process()
            Dim startInfo = New ProcessStartInfo()
            startInfo.FileName = Path.Combine("PostgreSQL", "postgresql-restore.bat")
            Dim host = String.Empty
            Dim port = String.Empty
            Dim user = String.Empty
            Dim database = databaseName
            Dim outputPath = localDatabasePath

            ' use pg_restore, specifying the host, port, user, database to restore, and the output path.
            ' the host, port, user, and database must be an exact match with what's inside your pgpass.conf (Windows)
            startInfo.Arguments = $"{host} {port} {user} {database} ""{outputPath}"""
            startInfo.CreateNoWindow = True
            startInfo.UseShellExecute = False
            process.StartInfo = startInfo
            process.Start()
            process.WaitForExit()
            process.Close()
        End Sub
    End Class
End Namespace
