
Imports Microsoft.Extensions.DependencyInjection

Namespace ResourceManager.ViewModels
    Public Class ViewModelLocator
        Public ReadOnly Property MainViewModel As MainViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of MainViewModel)()
            End Get
        End Property
        Public ReadOnly Property MainPageViewModel As MainPageViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of MainPageViewModel)()
            End Get
        End Property
        Public ReadOnly Property LoginPageViewModel As LoginPageViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of LoginPageViewModel)()
            End Get
        End Property
        Public ReadOnly Property UserPageViewModel As UserPageViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of UserPageViewModel)()
            End Get
        End Property
        Public ReadOnly Property UserContentViewModel As UserContentViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of UserContentViewModel)()
            End Get
        End Property
        Public ReadOnly Property RoleContentViewModel As RoleContentViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of RoleContentViewModel)()
            End Get
        End Property
        Public ReadOnly Property PermissionContentViewModel As PermissionContentViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of PermissionContentViewModel)()
            End Get
        End Property
        Public ReadOnly Property PermissionPopupViewModel As PermissionPopupViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of PermissionPopupViewModel)()
            End Get
        End Property
        Public ReadOnly Property PasswordPageViewModel As PasswordPageViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of PasswordPageViewModel)()
            End Get
        End Property
        Public ReadOnly Property PasswordContentViewModel As PasswordContentViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of PasswordContentViewModel)()
            End Get
        End Property
        Public ReadOnly Property PasswordPopupViewModel As PasswordPopupViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of PasswordPopupViewModel)()
            End Get
        End Property
        Public ReadOnly Property EbookPageViewModel As EbookPageViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of EbookPageViewModel)()
            End Get
        End Property
        Public ReadOnly Property EbookContentViewModel As EbookContentViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of EbookContentViewModel)()
            End Get
        End Property
        Public ReadOnly Property EbookPopupViewModel As EbookPopupViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of EbookPopupViewModel)()
            End Get
        End Property
        Public ReadOnly Property ActivityPageViewModel As ActivityPageViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of ActivityPageViewModel)()
            End Get
        End Property
        Public ReadOnly Property ActivityContentViewModel As ActivityContentViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of ActivityContentViewModel)()
            End Get
        End Property
        Public ReadOnly Property ActivityPopupViewModel As ActivityPopupViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of ActivityPopupViewModel)()
            End Get
        End Property
        Public ReadOnly Property ArticlePageViewModel As ArticlePageViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of ArticlePageViewModel)()
            End Get
        End Property
        Public ReadOnly Property ArticleContentViewModel As ArticleContentViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of ArticleContentViewModel)()
            End Get
        End Property
        Public ReadOnly Property ArticlePopupViewModel As ArticlePopupViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of ArticlePopupViewModel)()
            End Get
        End Property
        Public ReadOnly Property RepositoryPageViewModel As RepositoryPageViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of RepositoryPageViewModel)()
            End Get
        End Property
        Public ReadOnly Property RepositoryContentViewModel As RepositoryContentViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of RepositoryContentViewModel)()
            End Get
        End Property
        Public ReadOnly Property RepositoryPopupViewModel As RepositoryPopupViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of RepositoryPopupViewModel)()
            End Get
        End Property
        Public ReadOnly Property RepositoryOptionsContentViewModel As RepositoryOptionsContentViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of RepositoryOptionsContentViewModel)()
            End Get
        End Property
        Public ReadOnly Property FilterPopupViewModel As FilterPopupViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of FilterPopupViewModel)()
            End Get
        End Property
        Public ReadOnly Property TagsContentViewModel As TagsContentViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of TagsContentViewModel)()
            End Get
        End Property
        Public ReadOnly Property TagsPopupViewModel As TagsPopupViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of TagsPopupViewModel)()
            End Get
        End Property
        Public ReadOnly Property ToolsPageViewModel As ToolsPageViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of ToolsPageViewModel)()
            End Get
        End Property
        Public ReadOnly Property DatabaseContentViewModel As DatabaseContentViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of DatabaseContentViewModel)()
            End Get
        End Property
        Public ReadOnly Property EbookOptionsContentViewModel As EbookOptionsContentViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of EbookOptionsContentViewModel)()
            End Get
        End Property
        Public ReadOnly Property ArticleOptionsContentViewModel As ArticleOptionsContentViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of ArticleOptionsContentViewModel)()
            End Get
        End Property
        Public ReadOnly Property NoteContentViewModel As NoteContentViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of NoteContentViewModel)()
            End Get
        End Property
        Public ReadOnly Property NotePopupViewModel As NotePopupViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of NotePopupViewModel)()
            End Get
        End Property
        Public ReadOnly Property NotePageViewModel As NotePageViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of NotePageViewModel)()
            End Get
        End Property
    End Class
End Namespace
