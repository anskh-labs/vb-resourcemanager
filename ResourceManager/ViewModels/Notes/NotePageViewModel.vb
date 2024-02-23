Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Security
Imports ResourceManager.Configuration
Imports ResourceManager.Helpers
Imports System.Collections.ObjectModel
Imports System.Linq
Imports Microsoft.Extensions.DependencyInjection

Namespace ResourceManager.ViewModels
    Public Class NotePageViewModel
        Inherits PageViewModel
        Public Sub New()

            PageTitle = "Note"
            PageIcon = AssetManager.Instance.GetImage("Note.png")
            PageColor = NotePageColor
            MyBase.HasPermission = AuthManager.User.IsInPermission(VIEW_NOTE_PERMISSION)

            Dim tagsContent = Application.ServiceProvider.GetRequiredService(Of TagsContentViewModel)()
            tagsContent.PageColor = PageColor
            tagsContent.NoteColumnWidth = 110

            MenuItems = New ObservableCollection(Of PageContentViewModel) From {
    Application.ServiceProvider.GetRequiredService(Of NoteContentViewModel)(),
    tagsContent
}

            SelectedItem = MenuItems.FirstOrDefault()
        End Sub
    End Class
End Namespace
