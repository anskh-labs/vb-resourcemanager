Namespace ResourceManager.Settings
    Public Class AppSettings
        Public Const SectionName As String = "AppSettings"
        Public Property AppName As String
        Public Property AppVersion As String
        Public Property AppLanguage As String
        Public Property EbookSettings As EbookSettings
        Public Property ArticleSettings As ArticleSettings
        Public Property ActivitySettings As ActivitySettings
        Public Property RepositorySettings As RepositorySettings
    End Class
    Public Class EbookSettings
        Public Const SectionName As String = "EbookSettings"
        Public Property FolderPath As String
        Public Property SupportExtensions As String
        Public Property CoverPath As String
        Public Property CoverFileExtensions As String
        Public Property DeleteSourceFile As Boolean
    End Class
    Public Class ArticleSettings
        Public Const SectionName As String = "ArticleSettings"
        Public Property FolderPath As String
        Public Property SupportExtensions As String
        Public Property DeleteSourceFile As Boolean
    End Class
    Public Class ActivitySettings
        Public Const SectionName As String = "ActivitySettings"

    End Class
    Public Class RepositorySettings
        Public Const SectionName As String = "RepositorySettings"
        Public Property FolderPath As String
        Public Property SupportExtensions As String
        Public Property DeleteSourceFile As Boolean
    End Class
End Namespace
