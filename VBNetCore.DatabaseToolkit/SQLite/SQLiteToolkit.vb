Imports System.Diagnostics

Namespace VBNetCore.DatabaseToolkit.SQLite
    Public Class SQLiteToolkit
        Implements ISQLiteToolkit
        ''' <summary>
        ''' Backup a sqlite database using sqlite3 and the .backup dot command.
        ''' </summary>
        ''' <paramname="databaseName">This is the database you connect to including extension.</param>
        ''' <paramname="localDatabasePath">The path where the backup is stored including extension.</param>
        ''' <returns></returns>
        Public Function BackupDatabase(ByVal databaseName As String, ByVal localDatabasePath As String) As (Integer, String) Implements ISQLiteToolkit.BackupDatabase
            Dim process = New Process()
            Dim startInfo = New ProcessStartInfo()
            startInfo.FileName = "cmd.exe"

            ' sqlite needs to have two slashes instead of one for the path it uses inside sqlite3
            startInfo.Arguments = $"/c sqlite3 ""{databaseName}"" "".backup ""{localDatabasePath.Replace("\", "\\")}"""""
            startInfo.RedirectStandardOutput = True
            startInfo.CreateNoWindow = True
            startInfo.WindowStyle = ProcessWindowStyle.Hidden
            startInfo.UseShellExecute = False
            process.StartInfo = startInfo
            process.Start()
            process.WaitForExit()
            Dim statusCode = process.ExitCode
            Dim output As String = process.StandardOutput.ReadToEnd()
            process.Close()

            Return (statusCode, output)
        End Function

        ''' <summary>
        ''' Restore a sqlite database using sqlite3 and the .restore dot command.
        ''' </summary>
        ''' <paramname="databaseName">This is the database you connect to including extension.</param>
        ''' <paramname="localDatabasePath">The path where the backup is stored including extension.</param>
        ''' <returns></returns>
        Public Function RestoreDatabase(ByVal databaseName As String, ByVal localDatabasePath As String) As (Integer, String) Implements ISQLiteToolkit.RestoreDatabase
            Dim process = New Process()
            Dim startInfo = New ProcessStartInfo()
            startInfo.FileName = "cmd.exe"

            ' sqlite needs to have two slashes instead of one for the path it uses inside sqlite3
            startInfo.Arguments = $"/c sqlite3 ""{databaseName}"" "".restore ""{localDatabasePath.Replace("\", "\\")}"""""
            startInfo.RedirectStandardOutput = True
            startInfo.CreateNoWindow = True
            startInfo.WindowStyle = ProcessWindowStyle.Hidden
            startInfo.UseShellExecute = False
            process.StartInfo = startInfo
            process.Start()
            process.WaitForExit()
            Dim statusCode = process.ExitCode
            Dim output As String = process.StandardOutput.ReadToEnd()
            process.Close()

            Return (statusCode, output)
        End Function
    End Class
End Namespace
