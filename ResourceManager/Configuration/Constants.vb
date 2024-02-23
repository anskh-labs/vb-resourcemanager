Imports System.Windows.Media

Namespace ResourceManager.Configuration
    Public Module Constants
        Public Const JsonFileName As String = "appsettings.json"
        Public Const DbFileName As String = "ResourceManager.db"

        Public ReadOnly UserPageColor As Brush = New SolidColorBrush(Color.FromRgb(0, 51, 238))
        Public ReadOnly PasswordPageColor As Brush = New SolidColorBrush(Color.FromRgb(4, 149, 84))
        Public ReadOnly RepositoryPageColor As Brush = New SolidColorBrush(Color.FromRgb(247, 99, 12))
        Public ReadOnly EbookPageColor As Brush = New SolidColorBrush(Color.FromRgb(232, 17, 25))
        Public ReadOnly ArticlePageColor As Brush = New SolidColorBrush(Color.FromRgb(191, 0, 119))
        Public ReadOnly ActivityPageColor As Brush = New SolidColorBrush(Color.FromRgb(77, 74, 142))
        Public ReadOnly NotePageColor As Brush = New SolidColorBrush(Color.FromRgb(34, 119, 170))
        Public ReadOnly ToolsPageColor As Brush = New SolidColorBrush(Color.FromRgb(74, 84, 89))
        Public ReadOnly DefaultPageColor As Brush = New SolidColorBrush(Color.FromRgb(80, 80, 80))

        Public Const VIEW_USER_PERMISSION As String = "v_user"
        Public Const VIEW_PASSWORD_PERMISSION As String = "v_password"
        Public Const VIEW_REPOSITORY_PERMISSION As String = "v_repository"
        Public Const VIEW_EBOOK_PERMISSION As String = "v_ebook"
        Public Const VIEW_ARTICLE_PERMISSION As String = "v_article"
        Public Const VIEW_ACTIVITY_PERMISSION As String = "v_activity"
        Public Const VIEW_NOTE_PERMISSION As String = "v_note"
        Public Const VIEW_TOOLS_PERMISSION As String = "v_tools"

        Public Const ACTION_ADD_USER As String = "a_add_user"
        Public Const ACTION_EDIT_USER As String = "a_edit_user"
        Public Const ACTION_DEL_USER As String = "a_del_user"

        Public Const ACTION_ADD_ROLE As String = "a_add_role"
        Public Const ACTION_EDIT_ROLE As String = "a_edit_role"
        Public Const ACTION_DEL_ROLE As String = "a_del_role"

        Public Const ACTION_ADD_PERMISSION As String = "a_add_perm"
        Public Const ACTION_EDIT_PERMISSION As String = "a_edit_perm"
        Public Const ACTION_DEL_PERMISSION As String = "a_del_perm"

        Public Const ACTION_ADD_PASSWORD As String = "a_add_password"
        Public Const ACTION_EDIT_PASSWORD As String = "a_edit_password"
        Public Const ACTION_DEL_PASSWORD As String = "a_del_password"

        Public Const ACTION_ADD_TAG As String = "a_add_tag"
        Public Const ACTION_EDIT_TAG As String = "a_edit_tag"
        Public Const ACTION_DEL_TAG As String = "a_del_tag"

        Public Const ACTION_ADD_REPOSITORY As String = "a_add_repository"
        Public Const ACTION_EDIT_REPOSITORY As String = "a_edit_repository"
        Public Const ACTION_DEL_REPOSITORY As String = "a_del_repository"

        Public Const ACTION_ADD_EBOOK As String = "a_add_ebook"
        Public Const ACTION_EDIT_EBOOK As String = "a_edit_ebook"
        Public Const ACTION_DEL_EBOOK As String = "a_del_ebook"

        Public Const ACTION_ADD_ARTICLE As String = "a_add_article"
        Public Const ACTION_EDIT_ARTICLE As String = "a_edit_article"
        Public Const ACTION_DEL_ARTICLE As String = "a_del_article"

        Public Const ACTION_ADD_ACTIVITY As String = "a_add_activity"
        Public Const ACTION_EDIT_ACTIVITY As String = "a_edit_activity"
        Public Const ACTION_DEL_ACTIVITY As String = "a_del_activity"

        Public Const ACTION_ADD_NOTE As String = "a_add_note"
        Public Const ACTION_EDIT_NOTE As String = "a_edit_note"
        Public Const ACTION_DEL_NOTE As String = "a_del_note"
    End Module
End Namespace
