
Namespace VBNetCore.DatabaseToolkit.MySQL
    Public Interface IMySQLToolkit
        ''' <summary>
        ''' Restore a MySQL database using mysql. Make sure the appsettings.json properties
        ''' <seecref="ApplicationOptions.MySqlDefaultsFilePath"/> points to your my.ini (Windows) file.
        ''' </summary>
        ''' <paramname="databaseName">The name of the database on the MySQL server to restore.</param>
        ''' <paramname="localDatabasePath">The local path to the .sql database file that's being restored.</param>
        Sub RestoreDatabase(ByVal databaseName As String, ByVal localDatabasePath As String)

        ''' <summary>
        ''' Backup a MySQL database using mysqldump. Make sure the appsettings.json properties
        ''' <seecref="ApplicationOptions.MySqlDefaultsFilePath"/> points to your my.ini (Windows) file
        ''' and <seecref="ApplicationOptions.MySqlDumpPath"/> points to your mysqldump.exe file.
        ''' </summary>
        ''' <paramname="databaseName">The name of the database on the MySQL server.</param>
        ''' <paramname="localDatabasePath">The local path to the .sql database file where the backup will be saved.</param>
        Sub BackupDatabase(ByVal databaseName As String, ByVal localDatabasePath As String)
    End Interface
End Namespace
