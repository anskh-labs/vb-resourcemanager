Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Mvvm.Commands
Imports System.Collections.ObjectModel
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging

Namespace ResourceManager.ViewModels
    Public Class PageViewModel
        Inherits ViewModelBase


        Private _PageColor As System.Windows.Media.Brush, _PageIcon As System.Windows.Media.Imaging.BitmapImage, _HasPermission As Boolean, _MenuItems As System.Collections.ObjectModel.ObservableCollection(Of ResourceManager.ViewModels.PageContentViewModel)
        Public Sub New()
            SelectedPageCommand = New RelayCommand(Sub() mainVM.CurrentPage = Me)
            GoHomeCommand = New RelayCommand(AddressOf mainVM.GoHome)
        End Sub

        Public Property PageTitle As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                SetProperty(value)
            End Set
        End Property

        Public Property PageColor As Brush
            Get
                Return _PageColor
            End Get
            Protected Set(ByVal value As Brush)
                _PageColor = value
            End Set
        End Property

        Public Property PageIcon As BitmapImage
            Get
                Return _PageIcon
            End Get
            Protected Set(ByVal value As BitmapImage)
                _PageIcon = value
            End Set
        End Property

        Public Overridable Property HasPermission As Boolean
            Get
                Return _HasPermission
            End Get
            Protected Set(ByVal value As Boolean)
                _HasPermission = value
            End Set
        End Property

        Public ReadOnly Property SelectedPageCommand As ICommand
        Public ReadOnly Property GoHomeCommand As ICommand

        Public Property MenuItems As ObservableCollection(Of PageContentViewModel)
            Get
                Return _MenuItems
            End Get
            Protected Set(ByVal value As ObservableCollection(Of PageContentViewModel))
                _MenuItems = value
            End Set
        End Property
        Public Property SelectedItem As PageContentViewModel
            Get
                Return GetProperty(Of PageContentViewModel)()
            End Get
            Set(ByVal value As PageContentViewModel)
                SetProperty(value)
            End Set
        End Property
    End Class
End Namespace
