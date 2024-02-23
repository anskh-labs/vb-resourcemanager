Imports VBNetCore.Security
Imports ResourceManager.Configuration
Imports ResourceManager.Helpers
Imports System.Collections.ObjectModel
Imports System.Linq
Imports Microsoft.Extensions.DependencyInjection

Namespace ResourceManager.ViewModels
    Public Class RepositoryPageViewModel
        Inherits PageViewModel
        Public Sub New()

            PageTitle = "Repository"
            PageIcon = AssetManager.Instance.GetImage("Repository.png")
            PageColor = RepositoryPageColor
            MyBase.HasPermission = AuthManager.User.IsInPermission(VIEW_REPOSITORY_PERMISSION)

            Dim tagsContent = Application.ServiceProvider.GetRequiredService(Of TagsContentViewModel)()
            tagsContent.PageColor = PageColor
            tagsContent.RepositoryColumnWidth = 130

            MenuItems = New ObservableCollection(Of PageContentViewModel) From {
    Application.ServiceProvider.GetRequiredService(Of RepositoryContentViewModel)(),
    tagsContent,
    Application.ServiceProvider.GetRequiredService(Of RepositoryOptionsContentViewModel)()
}

            SelectedItem = MenuItems.FirstOrDefault()
        End Sub
    End Class
End Namespace
