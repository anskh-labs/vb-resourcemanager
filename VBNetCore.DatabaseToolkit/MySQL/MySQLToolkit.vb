Imports System.Diagnostics
Imports System.IO

Namespace VBNetCore.DatabaseToolkit.MySQL
    Friend Class MySQLToolkit
        Implements IMySQLToolkit

        ''' <summary>
        ''' Backup a MySQL database using mysqldump. Make sure the appsettings.json properties
        ''' <seecref="ApplicationOptions.MySqlDefaultsFilePath"/> points to your my.ini (Windows) file
        ''' and <seecref="ApplicationOptions.MySqlDumpPath"/> points to your mysqldump.exe file.
        ''' </summary>
        ''' <paramname="databaseName">The name of the database on the MySQL server.</param>
        ''' <paramname="localDatabasePath">The local path to the .sql database file where the backup will be saved.</param>
        Public Sub BackupDatabase(ByVal databaseName As String, ByVal localDatabasePath As String) Implements IMySQLToolkit.BackupDatabase
            Dim process = New Process()
            Dim startInfo = New ProcessStartInfo()

            ' execute from a bat file allows the use of > and < in the arguments
            startInfo.FileName = Path.Combine("MySQL", "mysql-backup.bat")

            ' call mysqldump, specifying the defaults config to use, which database to back up, and where to save the back up
            'startInfo.Arguments = $@"""{options.Value.MySqlDumpPath}"" ""{options.Value.MySqlDefaultsFilePath}"" {databaseName} ""{localDatabasePath}""";
            startInfo.CreateNoWindow = True
            startInfo.UseShellExecute = False
            process.StartInfo = startInfo
            process.Start()
            process.WaitForExit()
            process.Close()
        End Sub

        ''' <summary>
        ''' Restore a MySQL database using mysql. Make sure the appsettings.json properties
        ''' <seecref="ApplicationOptions.MySqlDefaultsFilePath"/> points to your my.ini (Windows) file.
        ''' </summary>
        ''' <paramname="databaseName">The name of the database on the MySQL server to restore.</param>
        ''' <paramname="localDatabasePath">The local path to the .sql database file that's being restored.</param>
        Public Sub RestoreDatabase(ByVal databaseName As String, ByVal localDatabasePath As String) Implements IMySQLToolkit.RestoreDatabase
            Dim process = New Process()
            Dim startInfo = New ProcessStartInfo()

            ' execute from a bat file allows the use of > and < in the arguments
            startInfo.FileName = Path.Combine("MySQL", "mysql-restore.bat")

            ' call mysql, specify the defaults config file, the database you're restoring, and the local path to the database you want to restore
            'startInfo.Arguments = $@"""{options.Value.MySqlDefaultsFilePath}"" {databaseName} ""{localDatabasePath}""";
            startInfo.CreateNoWindow = True
            startInfo.UseShellExecute = False
            process.StartInfo = startInfo
            process.Start()
            process.WaitForExit()
            process.Close()
        End Sub
    End Class
End Namespace
