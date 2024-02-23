Imports Microsoft.Extensions.DependencyInjection
Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Mvvm.Controls
Imports VBNetCore.Mvvm.ViewModels
Imports System

Namespace VBNetCore.Mvvm.Helpers
    Public Class PopupManager
        Implements IPopupManager
        Private ReadOnly _serviceProvider As IServiceProvider
        Private _popupMessageViewModel As PopupMessageViewModel
        Public Sub New(ByVal serviceProvider As IServiceProvider, ByVal popupMessageViewModel As PopupMessageViewModel)
            _serviceProvider = serviceProvider
            _popupMessageViewModel = If(popupMessageViewModel, _serviceProvider.GetRequiredService(Of PopupMessageViewModel)())
        End Sub

        Public Function ShowPopup(Of T As PopupViewModel)(ByVal popupViewModel As T, ByVal owner As IPopupable) As T Implements IPopupManager.ShowPopup
            If owner IsNot Nothing AndAlso TypeOf owner Is IPopupable Then
                Return TryCast(owner.ShowPoup(popupViewModel), T)
            End If
            Return Nothing
        End Function

        Public Function ShowPopupMessage(ByVal message As String, ByVal caption As String, ByVal owner As IPopupable, ByVal Optional popupButton As PopupButton = PopupButton.OK, ByVal Optional popupImage As PopupImage = PopupImage.None) As PopupResult Implements IPopupManager.ShowPopupMessage
            If owner IsNot Nothing AndAlso TypeOf owner Is IPopupable Then
                _popupMessageViewModel.Message = message
                _popupMessageViewModel.Caption = caption
                _popupMessageViewModel.Button = popupButton
                _popupMessageViewModel.Image = popupImage

                Dim popup = owner.ShowPoup(_popupMessageViewModel)
                If popup IsNot Nothing Then Return popup.Result

            End If
            Return PopupResult.None
        End Function
    End Class
End Namespace
