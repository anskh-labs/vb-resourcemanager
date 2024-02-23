Imports VBNetCore.Mvvm.Commands
Imports VBNetCore.Mvvm.Controls
Imports System
Imports System.Windows.Input

Namespace VBNetCore.Mvvm.ViewModels
    Public MustInherit Class PopupViewModel
        Inherits ValidationPropertyChangedBase
        Public Event CloseHandler As EventHandler
        Public Sub New()
            CloseCommand = New RelayCommand(AddressOf OnClose)
        End Sub
        Protected Sub OnClose()
            RaiseEvent CloseHandler(Me, EventArgs.Empty)
        End Sub
        Public Property Result As PopupResult
            Get
                Return GetProperty(Of PopupResult)()
            End Get
            Protected Set(ByVal value As PopupResult)
                SetProperty(value)
            End Set
        End Property
        Public Property Caption As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                SetProperty(value)
            End Set
        End Property
        Public ReadOnly Property CloseCommand As ICommand
    End Class
End Namespace
