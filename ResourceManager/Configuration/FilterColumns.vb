Imports System.Collections.Generic

Namespace ResourceManager.Configuration
    Public Module FilterColumns
        Public User As IEnumerable(Of String) = {"Name", "AccountName"}
        Public Tag As IEnumerable(Of String) = {"Name", "PasswordCount", "BookCount", "ArticleCount", "ActivityCount", "RepositoryCount", "NoteCount"}
        Public Role As IEnumerable(Of String) = {"Name", "Description"}
        Public Permission As IEnumerable(Of String) = {"Name", "Description"}
        Public Book As IEnumerable(Of String) = {"Title", "Author", "Publisher", "Abstraction", "TagString"}
        Public Article As IEnumerable(Of String) = {"Title", "Author", "TagString"}
        Public Password As IEnumerable(Of String) = {"Name", "UserName", "Url", "Description", "TagString"}
        Public Repository As IEnumerable(Of String) = {"Title", "FileType", "FilePath", "FileSize", "TagString"}
        Public Activity As IEnumerable(Of String) = {"Title", "Date", "StartTime", "EndTime", "Metric", "Quantity", "Output", "Note", "TagString"}
        Public Note As IEnumerable(Of String) = {"Title", "Date", "Notes", "TagString"}
    End Module
End Namespace
