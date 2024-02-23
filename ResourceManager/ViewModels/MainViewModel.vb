Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Mvvm.ViewModels
Imports VBNetCore.Security
Imports System.Threading.Tasks
Imports Microsoft.Extensions.DependencyInjection

Namespace ResourceManager.ViewModels
    Public Class MainViewModel
        Inherits HasPopupViewModel
        Implements IOnLoadedHandler, IPopupable
#Region "IOnLoadedHandler"
        Public Function OnLoadedAsync() As Task Implements IOnLoadedHandler.OnLoadedAsync
            RefreshUI()
            Return Task.FromResult(True)
        End Function
#End Region
        Public Sub RefreshUI()
            If AuthManager.User.Identity.IsAuthenticated Then
                CurrentPage = MainPage
            Else
                CurrentPage = LoginPage
            End If
        End Sub

        Friend Sub GoHome()
            CurrentPage = MainPage
        End Sub

        Public Property CurrentPage As ViewModelBase
            Get
                Return GetProperty(Of ViewModelBase)()
            End Get
            Set(ByVal value As ViewModelBase)
                SetProperty(value)
            End Set
        End Property
        Private ReadOnly Property LoginPage As ViewModelBase
            Get
                Return Application.ServiceProvider.GetRequiredService(Of LoginPageViewModel)()
            End Get
        End Property
        Private ReadOnly Property MainPage As ViewModelBase
            Get
                Return Application.ServiceProvider.GetRequiredService(Of MainPageViewModel)()
            End Get
        End Property
    End Class
End Namespace
