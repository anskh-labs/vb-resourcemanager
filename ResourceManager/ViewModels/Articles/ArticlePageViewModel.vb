Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Security
Imports ResourceManager.Configuration
Imports ResourceManager.Helpers
Imports System.Collections.ObjectModel
Imports System.Linq
Imports Microsoft.Extensions.DependencyInjection

Namespace ResourceManager.ViewModels
    Public Class ArticlePageViewModel
        Inherits PageViewModel
        Public Sub New()

            PageTitle = "Article"
            PageIcon = AssetManager.Instance.GetImage("Articles.png")
            PageColor = ArticlePageColor
            MyBase.HasPermission = AuthManager.User.IsInPermission(VIEW_ARTICLE_PERMISSION)

            Dim tagsContent = Application.ServiceProvider.GetRequiredService(Of TagsContentViewModel)()
            tagsContent.PageColor = PageColor
            tagsContent.ArticleColumnWidth = 100

            MenuItems = New ObservableCollection(Of PageContentViewModel) From {
    Application.ServiceProvider.GetRequiredService(Of ArticleContentViewModel)(),
    tagsContent,
    Application.ServiceProvider.GetRequiredService(Of ArticleOptionsContentViewModel)()
}

            SelectedItem = MenuItems.FirstOrDefault()
        End Sub
    End Class
End Namespace
