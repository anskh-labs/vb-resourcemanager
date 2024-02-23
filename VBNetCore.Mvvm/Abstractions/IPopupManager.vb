Imports VBNetCore.Mvvm.Controls
Imports VBNetCore.Mvvm.ViewModels

Namespace VBNetCore.Mvvm.Abstractions
    Public Interface IPopupManager
        Function ShowPopup(Of T As PopupViewModel)(ByVal popupViewModel As T, ByVal owner As IPopupable) As T
        Function ShowPopupMessage(ByVal message As String, ByVal caption As String, ByVal owner As IPopupable, ByVal Optional popupButton As PopupButton = PopupButton.OK, ByVal Optional popupImage As PopupImage = PopupImage.None) As PopupResult
    End Interface
End Namespace
