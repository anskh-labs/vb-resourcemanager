Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Security
Imports ResourceManager.Configuration
Imports ResourceManager.Helpers
Imports System.Collections.ObjectModel
Imports System.Linq
Imports Microsoft.Extensions.DependencyInjection

Namespace ResourceManager.ViewModels
    Public Class UserPageViewModel
        Inherits PageViewModel
        Public Sub New()
            PageTitle = "User"
            PageIcon = AssetManager.Instance.GetImage("Users.png")
            PageColor = UserPageColor
            MyBase.HasPermission = AuthManager.User.IsInPermission(VIEW_USER_PERMISSION)

            MenuItems = New ObservableCollection(Of PageContentViewModel) From {
                Application.ServiceProvider.GetRequiredService(Of UserContentViewModel)(),
                Application.ServiceProvider.GetRequiredService(Of RoleContentViewModel)(),
                Application.ServiceProvider.GetRequiredService(Of PermissionContentViewModel)()
            }

            SelectedItem = MenuItems.FirstOrDefault()
        End Sub

    End Class
End Namespace
