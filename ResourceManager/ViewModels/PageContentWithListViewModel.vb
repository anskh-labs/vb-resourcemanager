Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Mvvm.Commands
Imports VBNetCore.Mvvm.Controls
Imports VBNetCore.Security
Imports ResourceManager.Helpers
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Input
Imports Microsoft.Extensions.DependencyInjection

Namespace ResourceManager.ViewModels
    Public MustInherit Class PageContentWithListViewModel(Of tT As BaseEntity)
        Inherits PageContentViewModel

        Private _AddCommand As System.Windows.Input.ICommand, _FilterCommand As System.Windows.Input.ICommand, _RefreshCommand As System.Windows.Input.ICommand, _EditCommand As System.Windows.Input.ICommand, _DeleteCommand As System.Windows.Input.ICommand, _FirstCommand As System.Windows.Input.ICommand, _PrevCommand As System.Windows.Input.ICommand, _NextCommand As System.Windows.Input.ICommand, _LastCommand As System.Windows.Input.ICommand
        Protected _viewPager As ViewPager(Of tT)
        Protected _filter As Filter(Of tT)
        Protected _dataService As IDataService(Of tT)
        Protected _add_permission_name As String
        Protected _delete_permission_name As String
        Protected _edit_permission_name As String
        Public Sub New(ByVal dataService As IDataService(Of tT))
            _dataService = dataService

            RefreshCommand = New RelayCommand(AddressOf OnRefresh)
            FilterCommand = New RelayCommand(AddressOf OnFilter)
            DeleteCommand = New RelayCommand(Of tT)(AddressOf OnDelete)
            AddCommand = New RelayCommand(AddressOf OnAdd)
            EditCommand = New RelayCommand(Of tT)(AddressOf OnEdit)
            FirstCommand = New RelayCommand(AddressOf OnFirst)
            PrevCommand = New RelayCommand(AddressOf OnPrev)
            NextCommand = New RelayCommand(AddressOf OnNext)
            LastCommand = New RelayCommand(AddressOf OnLast)

            RowsPerPage = New List(Of String)(New String() {"25", "50", "all"})
            PageItems = New List(Of Integer)(New Integer() {1})
            SelectedRowsPerPage = RowsPerPage.First()
            SelectedPageItem = PageItems.First()
        End Sub
        Protected Sub OnLast()
            If SelectedPageItem < PageItems.Count() Then
                SelectedPageItem = PageItems.Count()
            End If
        End Sub
        Protected Sub OnNext()
            If SelectedPageItem < PageItems.Count() Then
                SelectedPageItem += 1
            End If
        End Sub
        Protected Sub OnPrev()
            If SelectedPageItem > 1 Then
                SelectedPageItem -= 1
            End If
        End Sub
        Protected Sub OnFirst()
            If SelectedPageItem > 1 Then
                SelectedPageItem = 1
            End If
        End Sub
        Protected Overridable Sub OnEdit(ByVal t As tT)
        End Sub
        Protected Overridable Sub OnAdd()
        End Sub
        Protected MustOverride Sub OnFilter()
        Protected Overridable Sub Filter(ByVal columns As IEnumerable(Of String), ByVal caption As String)
            Dim vm = Application.ServiceProvider.GetRequiredService(Of FilterPopupViewModel)()
            vm.Caption = caption
            vm.Columns = columns
            Dim popup = ShowPopup(vm)
            If popup.Result = PopupResult.OK Then
                _viewPager = New ViewPager(Of tT)(_filter.FilteredData(vm.FilterParams, _viewPager.Contents), SelectedPageItem, SelectedRowsPerPage)
                SelectedPageItem = _viewPager.SelectedPageItem
                ContentData = _viewPager.ViewContent
                PageItems = _viewPager.PageItems
                PageInfo = String.Format("Page {0} of {1}", SelectedPageItem, PageItems.Count())
            End If
        End Sub

        Protected Overridable Sub OnDelete(ByVal t As tT)
            Dim result = ShowPopupMessage("Delete " & t.GetCaption() & "", "Confirmation", PopupButton.YesNo, PopupImage.Question)
            If result = PopupResult.Yes Then
                _dataService.Delete(t.ID)
                OnRefresh()
            End If
        End Sub
        Protected Overridable Async Sub OnRefresh()
            _filter = Create(Of tT)()
            _viewPager = New ViewPager(Of tT)(Await _dataService.GetAll(), SelectedPageItem, SelectedRowsPerPage)
            ContentData = _viewPager.ViewContent
            PageItems = _viewPager.PageItems
            If SelectedPageItem > PageItems.Count() Then SelectedPageItem = PageItems.Count()
            PageInfo = String.Format("Page {0} of {1}", SelectedPageItem, PageItems.Count())
        End Sub

        Public Property PageItems As IEnumerable(Of Integer)
            Get
                Return GetProperty(Of IEnumerable(Of Integer))()
            End Get
            Set(ByVal value As IEnumerable(Of Integer))
                SetProperty(value)
            End Set
        End Property
        Public Property SelectedPageItem As Integer
            Get
                Return GetProperty(Of Integer)()
            End Get
            Set(ByVal value As Integer)
                If SetProperty(value) Then
                    If _viewPager IsNot Nothing Then
                        ContentData = _viewPager.Goto(value, SelectedRowsPerPage)
                        PageInfo = String.Format("Page {0} of {1}", SelectedPageItem, PageItems.Count())
                    End If
                End If
            End Set
        End Property
        Public Property RowsPerPage As IList(Of String)
            Get
                Return GetProperty(Of IList(Of String))()
            End Get
            Set(ByVal value As IList(Of String))
                SetProperty(value)
            End Set
        End Property
        Public Property SelectedRowsPerPage As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                If SetProperty(value) Then
                    If _viewPager IsNot Nothing Then
                        ContentData = _viewPager.Goto(SelectedPageItem, value)
                        PageItems = _viewPager.PageItems
                        If SelectedPageItem > PageItems.Count() Then SelectedPageItem = PageItems.Count()
                        PageInfo = String.Format("Page {0} of {1}", SelectedPageItem, PageItems.Count())
                    End If
                End If
            End Set
        End Property
        Public Property ContentData As IEnumerable(Of tT)
            Get
                Return GetProperty(Of IEnumerable(Of tT))()
            End Get
            Set(ByVal value As IEnumerable(Of tT))
                SetProperty(value)
            End Set
        End Property
        Public Property SelectedData As tT
            Get
                Return GetProperty(Of tT)()
            End Get
            Set(ByVal value As tT)
                SetProperty(value)
            End Set
        End Property
        Public Property PageInfo As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                SetProperty(value)
            End Set
        End Property
        Public ReadOnly Property CanAdd As Boolean
            Get
                Return AuthManager.User.IsInPermission(_add_permission_name)
            End Get
        End Property

        Public ReadOnly Property CanEdit As Boolean
            Get
                Return AuthManager.User.IsInPermission(_edit_permission_name)
            End Get
        End Property

        Public ReadOnly Property CanDelete As Boolean
            Get
                Return AuthManager.User.IsInPermission(_delete_permission_name)
            End Get
        End Property

        Public Property AddCommand As ICommand
            Get
                Return _AddCommand
            End Get
            Protected Set(ByVal value As ICommand)
                _AddCommand = value
            End Set
        End Property

        Public Property FilterCommand As ICommand
            Get
                Return _FilterCommand
            End Get
            Protected Set(ByVal value As ICommand)
                _FilterCommand = value
            End Set
        End Property

        Public Property RefreshCommand As ICommand
            Get
                Return _RefreshCommand
            End Get
            Protected Set(ByVal value As ICommand)
                _RefreshCommand = value
            End Set
        End Property

        Public Property EditCommand As ICommand
            Get
                Return _EditCommand
            End Get
            Protected Set(ByVal value As ICommand)
                _EditCommand = value
            End Set
        End Property

        Public Property DeleteCommand As ICommand
            Get
                Return _DeleteCommand
            End Get
            Protected Set(ByVal value As ICommand)
                _DeleteCommand = value
            End Set
        End Property

        Public Property FirstCommand As ICommand
            Get
                Return _FirstCommand
            End Get
            Protected Set(ByVal value As ICommand)
                _FirstCommand = value
            End Set
        End Property

        Public Property PrevCommand As ICommand
            Get
                Return _PrevCommand
            End Get
            Protected Set(ByVal value As ICommand)
                _PrevCommand = value
            End Set
        End Property

        Public Property NextCommand As ICommand
            Get
                Return _NextCommand
            End Get
            Protected Set(ByVal value As ICommand)
                _NextCommand = value
            End Set
        End Property

        Public Property LastCommand As ICommand
            Get
                Return _LastCommand
            End Get
            Protected Set(ByVal value As ICommand)
                _LastCommand = value
            End Set
        End Property
    End Class
End Namespace
