Imports VBNetCore.Mvvm.Commands
Imports VBNetCore.Mvvm.Controls
Imports System.Windows.Input

Namespace VBNetCore.Mvvm.ViewModels
    Public Class PopupMessageViewModel
        Inherits PopupViewModel

        Public Sub New()
            MyBase.New()
            OKCommand = New RelayCommand(AddressOf OnOK)
            YesCommand = New RelayCommand(AddressOf OnYes)
            NoCommand = New RelayCommand(AddressOf OnNo)
        End Sub
        Private Sub OnOK()
            Result = PopupResult.OK
            OnClose()
        End Sub
        Private Sub OnYes()
            Result = PopupResult.Yes
            OnClose()
        End Sub
        Private Sub OnNo()
            Result = PopupResult.No
            OnClose()
        End Sub

        Public Property Message As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                SetProperty(value)
            End Set
        End Property
        Public Property Image As PopupImage
            Get
                Return GetProperty(Of PopupImage)()
            End Get
            Set(ByVal value As PopupImage)
                SetProperty(value)
            End Set
        End Property
        Public Property Button As PopupButton
            Get
                Return GetProperty(Of PopupButton)()
            End Get
            Set(ByVal value As PopupButton)
                SetProperty(value)
            End Set
        End Property

        Public ReadOnly Property OKCommand As ICommand
        Public ReadOnly Property YesCommand As ICommand
        Public ReadOnly Property NoCommand As ICommand
    End Class
End Namespace
