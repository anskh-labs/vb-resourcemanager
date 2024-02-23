Imports VBNetCore.Security
Imports ResourceManager.Configuration
Imports ResourceManager.Helpers
Imports System.Collections.ObjectModel
Imports System.Linq
Imports Microsoft.Extensions.DependencyInjection

Namespace ResourceManager.ViewModels
    Public Class PasswordPageViewModel
        Inherits PageViewModel
        Public Sub New()
            PageTitle = "Password"
            PageIcon = AssetManager.Instance.GetImage("Passwords.png")
            PageColor = PasswordPageColor
            MyBase.HasPermission = AuthManager.User.IsInPermission(VIEW_PASSWORD_PERMISSION)

            Dim tagsContent = Application.ServiceProvider.GetRequiredService(Of TagsContentViewModel)()
            tagsContent.PageColor = PageColor
            tagsContent.PasswordColumnWidth = 120

            MenuItems = New ObservableCollection(Of PageContentViewModel) From {
                Application.ServiceProvider.GetRequiredService(Of PasswordContentViewModel)(),
                tagsContent
            }

            SelectedItem = MenuItems.FirstOrDefault()

        End Sub
    End Class
End Namespace
