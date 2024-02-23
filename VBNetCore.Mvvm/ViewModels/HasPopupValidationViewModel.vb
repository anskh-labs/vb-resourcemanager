Imports VBNetCore.Mvvm.Abstractions
Imports System
Imports System.Threading
Imports System.Windows.Threading

Namespace VBNetCore.Mvvm.ViewModels

    Public MustInherit Class HasPopupValidationViewModel
        Inherits ValidationPropertyChangedBase
        Implements IPopupable
        Public Property CurrentPopup As PopupViewModel Implements IPopupable.CurrentPopup
            Get
                Return GetProperty(Of PopupViewModel)()
            End Get
            Set(ByVal value As PopupViewModel)
                SetProperty(value)
            End Set
        End Property
        Public Property IsPopupVisible As Boolean Implements IPopupable.IsPopupVisible
            Get
                Return GetProperty(Of Boolean)()
            End Get
            Set(ByVal value As Boolean)
                SetProperty(value)
            End Set
        End Property
        Public Sub HidePopup() Implements IPopupable.HidePopup
            IsPopupVisible = False
            If CurrentPopup IsNot Nothing Then
                RemoveHandler CurrentPopup.CloseHandler, AddressOf PopupViewModel_CloseHandler
                CurrentPopup = Nothing
            End If
        End Sub

        Public Function ShowPoup(ByVal popupViewModel As PopupViewModel) As PopupViewModel Implements IPopupable.ShowPoup
            AddHandler popupViewModel.CloseHandler, AddressOf PopupViewModel_CloseHandler
            CurrentPopup = popupViewModel
            IsPopupVisible = True

            While IsPopupVisible
                If Dispatcher.CurrentDispatcher.HasShutdownStarted OrElse Dispatcher.CurrentDispatcher.HasShutdownFinished Then Exit While
                Call Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Background, New ThreadStart(Sub()
                                                                                                        End Sub))
                Thread.Sleep(20)
            End While

            Return popupViewModel
        End Function
        Private Sub PopupViewModel_CloseHandler(ByVal sender As Object, ByVal e As EventArgs)
            HidePopup()
        End Sub
    End Class
End Namespace
