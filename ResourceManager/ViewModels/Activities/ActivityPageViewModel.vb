Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Security
Imports ResourceManager.Configuration
Imports ResourceManager.Helpers
Imports System.Collections.ObjectModel
Imports System.Linq
Imports Microsoft.Extensions.DependencyInjection

Namespace ResourceManager.ViewModels
    Public Class ActivityPageViewModel
        Inherits PageViewModel
        Public Sub New()

            PageTitle = "Activity"
            PageIcon = AssetManager.Instance.GetImage("Activity.png")
            PageColor = ActivityPageColor
            MyBase.HasPermission = AuthManager.User.IsInPermission(VIEW_ACTIVITY_PERMISSION)

            Dim tagsContent = Application.ServiceProvider.GetRequiredService(Of TagsContentViewModel)()
            tagsContent.PageColor = PageColor
            tagsContent.ActivityColumnWidth = 110

            MenuItems = New ObservableCollection(Of PageContentViewModel) From {
    Application.ServiceProvider.GetRequiredService(Of ActivityContentViewModel)(),
    tagsContent
}

            SelectedItem = MenuItems.FirstOrDefault()
        End Sub
    End Class
End Namespace
