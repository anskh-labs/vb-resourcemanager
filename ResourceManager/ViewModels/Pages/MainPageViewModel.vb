Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Mvvm.Commands
Imports VBNetCore.Security
Imports System.Collections.ObjectModel
Imports System.Windows.Input
Imports Microsoft.Extensions.DependencyInjection

Namespace ResourceManager.ViewModels
    Public Class MainPageViewModel
        Inherits ViewModelBase
        Public Sub New()
            Pages = New ObservableCollection(Of PageViewModel) From {
                Application.ServiceProvider.GetRequiredService(Of UserPageViewModel)(),
                Application.ServiceProvider.GetRequiredService(Of PasswordPageViewModel)(),
                Application.ServiceProvider.GetRequiredService(Of EbookPageViewModel)(),
                Application.ServiceProvider.GetRequiredService(Of RepositoryPageViewModel)(),
                Application.ServiceProvider.GetRequiredService(Of ArticlePageViewModel)(),
                Application.ServiceProvider.GetRequiredService(Of ActivityPageViewModel)(),
                Application.ServiceProvider.GetRequiredService(Of NotePageViewModel)(),
                Application.ServiceProvider.GetRequiredService(Of ToolsPageViewModel)()
            }

            LogoutCommand = New RelayCommand(AddressOf Logout)
        End Sub

        Private Sub Logout()
            AuthManager.User.Identity = New AnonymousIdentity()
            mainVM.RefreshUI()
        End Sub

        Public ReadOnly Property LoginUser As String
            Get
                Return String.Format(" Logout [{0}]", AuthManager.User.Identity.Name)
            End Get
        End Property
        Public ReadOnly Property LogoutCommand As ICommand
        Public ReadOnly Property Pages As ObservableCollection(Of PageViewModel)
    End Class
End Namespace
