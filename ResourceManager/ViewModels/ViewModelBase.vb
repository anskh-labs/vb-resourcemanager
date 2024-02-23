Imports Microsoft.Extensions.DependencyInjection
Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Mvvm.Controls
Imports VBNetCore.Mvvm.ViewModels

Namespace ResourceManager.ViewModels
    Public Class ViewModelBase
        Inherits ValidationPropertyChangedBase
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

        Public Function ShowPopup(Of T As PopupViewModel)(ByVal popupViewModel As T) As T
            Return popupManager.ShowPopup(popupViewModel, mainVM)
        End Function
        Public Function ShowPopupMessage(ByVal message As String, ByVal caption As String, ByVal Optional popupButton As PopupButton = PopupButton.OK, ByVal Optional popupImage As PopupImage = PopupImage.None) As PopupResult
            Return popupManager.ShowPopupMessage(message, caption, mainVM, popupButton, popupImage)
        End Function
    End Class
End Namespace
