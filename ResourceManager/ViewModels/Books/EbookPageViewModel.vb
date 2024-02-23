Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Security
Imports ResourceManager.Configuration
Imports ResourceManager.Helpers
Imports System.Collections.ObjectModel
Imports System.Linq
Imports Microsoft.Extensions.DependencyInjection

Namespace ResourceManager.ViewModels
    Public Class EbookPageViewModel
        Inherits PageViewModel
        Public Sub New()

            PageTitle = "Ebook"
            PageIcon = AssetManager.Instance.GetImage("Ebooks.png")
            PageColor = EbookPageColor
            MyBase.HasPermission = AuthManager.User.IsInPermission(VIEW_EBOOK_PERMISSION)

            Dim tagsContent = Application.ServiceProvider.GetRequiredService(Of TagsContentViewModel)()
            tagsContent.PageColor = PageColor
            tagsContent.EbookColumnWidth = 100

            MenuItems = New ObservableCollection(Of PageContentViewModel) From {
    Application.ServiceProvider.GetRequiredService(Of EbookContentViewModel)(),
    tagsContent,
    Application.ServiceProvider.GetRequiredService(Of EbookOptionsContentViewModel)()
}

            SelectedItem = MenuItems.FirstOrDefault()
        End Sub
    End Class
End Namespace
