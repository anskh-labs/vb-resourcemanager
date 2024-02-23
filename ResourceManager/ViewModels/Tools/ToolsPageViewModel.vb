Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Security
Imports ResourceManager.Configuration
Imports ResourceManager.Helpers
Imports System.Collections.ObjectModel
Imports System.Linq
Imports Microsoft.Extensions.DependencyInjection

Namespace ResourceManager.ViewModels
    Public Class ToolsPageViewModel
        Inherits PageViewModel
        Public Sub New()

            PageTitle = "Tools"
            PageIcon = AssetManager.Instance.GetImage("Tools.png")
            PageColor = ToolsPageColor
            MyBase.HasPermission = AuthManager.User.IsInPermission(VIEW_TOOLS_PERMISSION)

            MenuItems = New ObservableCollection(Of PageContentViewModel) From {
    Application.ServiceProvider.GetRequiredService(Of DatabaseContentViewModel)()
}
            SelectedItem = MenuItems.FirstOrDefault()
        End Sub
    End Class
End Namespace
