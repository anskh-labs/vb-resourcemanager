
Namespace VBNetCore.DatabaseToolkit.SQLite
    Public Interface ISQLiteToolkit
        ''' <summary>
        ''' Backup a sqlite database using sqlite3 and the .backup dot command.
        ''' </summary>
        ''' <paramname="databaseName">This is the database you connect to including extension.</param>
        ''' <paramname="localDatabasePath">The path where the backup is stored including extension.</param>
        ''' <returns></returns>
        Function BackupDatabase(ByVal databaseName As String, ByVal localDatabasePath As String) As (Integer, String)

        ''' <summary>
        ''' Restore a sqlite database using sqlite3 and the .restore dot command.
        ''' </summary>
        ''' <paramname="databaseName">This is the database you connect to including extension.</param>
        ''' <paramname="localDatabasePath">The path where the backup is stored including extension.</param>
        ''' <returns></returns>
        Function RestoreDatabase(ByVal databaseName As String, ByVal localDatabasePath As String) As (Integer, String)
    End Interface
End Namespace
