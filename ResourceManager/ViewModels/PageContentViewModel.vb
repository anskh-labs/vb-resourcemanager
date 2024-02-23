Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Mvvm.Commands
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging

Namespace ResourceManager.ViewModels
    Public Class PageContentViewModel
        Inherits ViewModelBase
        Private _MenuTitle As String, _MenuIcon As BitmapImage, _Title As String
        Public Sub New()
            SelectedMenuCommand = New RelayCommand(Sub() CType(mainVM.CurrentPage, PageViewModel).SelectedItem = Me)
        End Sub
        Public ReadOnly Property SelectedMenuCommand As ICommand

        Public Property MenuTitle As String
            Get
                Return _MenuTitle
            End Get
            Protected Set(ByVal value As String)
                _MenuTitle = value
            End Set
        End Property

        Public Property MenuIcon As BitmapImage
            Get
                Return _MenuIcon
            End Get
            Protected Set(ByVal value As BitmapImage)
                _MenuIcon = value
            End Set
        End Property

        Public Property Title As String
            Get
                Return _Title
            End Get
            Protected Set(ByVal value As String)
                _Title = value
            End Set
        End Property
        Public Property PageColor As Brush
    End Class
End Namespace
