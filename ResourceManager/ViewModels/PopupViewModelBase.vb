Imports Microsoft.Extensions.DependencyInjection
Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Mvvm.Controls
Imports VBNetCore.Mvvm.ViewModels

Namespace ResourceManager.ViewModels
    Public Class PopupViewModelBase
        Inherits PopupViewModel
        Protected ReadOnly Property popupManager As IPopupManager
            Get
                Return Application.ServiceProvider.GetRequiredService(Of IPopupManager)()
            End Get
        End Property
        Protected ReadOnly Property windowManager As IWindowManager
            Get
                Return Application.ServiceProvider.GetRequiredService(Of IWindowManager)()
            End Get
        End Property
        Protected ReadOnly Property mainVM As MainViewModel
            Get
                Return Application.ServiceProvider.GetRequiredService(Of MainViewModel)()
            End Get
        End Property
        Protected ReadOnly Property UiExecution As IUiExecution
            Get
                Return Application.ServiceProvider.GetRequiredService(Of IUiExecution)()
            End Get
        End Property
    End Class
End Namespace
