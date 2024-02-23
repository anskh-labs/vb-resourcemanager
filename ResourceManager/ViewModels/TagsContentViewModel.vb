Imports ResourceManager.Configuration
Imports ResourceManager.Helpers
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions
Imports System.Linq

Namespace ResourceManager.ViewModels
    Public Class TagsContentViewModel
        Inherits PageContentWithListViewModel(Of Tag)
        Public Sub New(ByVal dataServiceManager As IDataServiceManager)
            MyBase.New(dataServiceManager.TagDataService)
            MenuTitle = "Tag"
            MenuIcon = AssetManager.Instance.GetImage("Tag.png")
            Title = "Tag Manager"
            PageColor = DefaultPageColor

            _add_permission_name = ACTION_ADD_TAG
            _edit_permission_name = ACTION_EDIT_TAG
            _delete_permission_name = ACTION_DEL_TAG

            PasswordColumnWidth = 0
            EbookColumnWidth = 0
            ArticleColumnWidth = 0
            ActivityColumnWidth = 0
            RepositoryColumnWidth = 0
            NoteColumnWidth = 0
        End Sub
        Protected Overrides Async Sub OnRefresh()
            _filter = FilterManager.Create(Of Tag)()
            If PasswordColumnWidth > 0 Then
                _viewPager = New ViewPager(Of Tag)(Await _dataService.GetWhere(Function(x) x.PasswordTags.Count > 0), SelectedPageItem, SelectedRowsPerPage)
            ElseIf EbookColumnWidth > 0 Then
                _viewPager = New ViewPager(Of Tag)(Await _dataService.GetWhere(Function(x) x.BookTags.Count > 0), SelectedPageItem, SelectedRowsPerPage)
            ElseIf ArticleColumnWidth > 0 Then
                _viewPager = New ViewPager(Of Tag)(Await _dataService.GetWhere(Function(x) x.ArticleTags.Count > 0), SelectedPageItem, SelectedRowsPerPage)
            ElseIf ActivityColumnWidth > 0 Then
                _viewPager = New ViewPager(Of Tag)(Await _dataService.GetWhere(Function(x) x.ActivityTags.Count > 0), SelectedPageItem, SelectedRowsPerPage)
            ElseIf RepositoryColumnWidth > 0 Then
                _viewPager = New ViewPager(Of Tag)(Await _dataService.GetWhere(Function(x) x.RepositoryTags.Count > 0), SelectedPageItem, SelectedRowsPerPage)
            ElseIf NoteColumnWidth > 0 Then
                _viewPager = New ViewPager(Of Tag)(Await _dataService.GetWhere(Function(x) x.NoteTags.Count > 0), SelectedPageItem, SelectedRowsPerPage)
            End If
            ContentData = _viewPager.ViewContent
            PageItems = _viewPager.PageItems
            If SelectedPageItem > PageItems.Count() Then SelectedPageItem = PageItems.Count()
            PageInfo = String.Format("Page {0} of {1}", SelectedPageItem, PageItems.Count())
        End Sub
        Protected Overrides Sub OnFilter()
            MyBase.Filter(FilterColumns.Tag, "Filter Tag")
        End Sub

        Public Property PasswordColumnWidth As Double
            Get
                Return GetProperty(Of Double)()
            End Get
            Set(ByVal value As Double)
                If SetProperty(value) Then
                    If value > 0 Then OnRefresh()
                End If
            End Set
        End Property
        Public Property ArticleColumnWidth As Double
            Get
                Return GetProperty(Of Double)()
            End Get
            Set(ByVal value As Double)
                If SetProperty(value) Then
                    If value > 0 Then OnRefresh()
                End If
            End Set
        End Property
        Public Property EbookColumnWidth As Double
            Get
                Return GetProperty(Of Double)()
            End Get
            Set(ByVal value As Double)
                If SetProperty(value) Then
                    If value > 0 Then OnRefresh()
                End If
            End Set
        End Property
        Public Property RepositoryColumnWidth As Double
            Get
                Return GetProperty(Of Double)()
            End Get
            Set(ByVal value As Double)
                If SetProperty(value) Then
                    If value > 0 Then OnRefresh()
                End If
            End Set
        End Property
        Public Property ActivityColumnWidth As Double
            Get
                Return GetProperty(Of Double)()
            End Get
            Set(ByVal value As Double)
                If SetProperty(value) Then
                    If value > 0 Then OnRefresh()
                End If
            End Set
        End Property
        Public Property NoteColumnWidth As Double
            Get
                Return GetProperty(Of Double)()
            End Get
            Set(ByVal value As Double)
                If SetProperty(value) Then
                    If value > 0 Then OnRefresh()
                End If
            End Set
        End Property
    End Class
End Namespace
