Imports VBNetCore.Mvvm.ViewModels

Namespace VBNetCore.Mvvm.Abstractions
    Public Interface IPopupable
        Function ShowPoup(ByVal popupViewModel As PopupViewModel) As PopupViewModel
        Sub HidePopup()
        Property CurrentPopup As PopupViewModel
        Property IsPopupVisible As Boolean
    End Interface
End Namespace
