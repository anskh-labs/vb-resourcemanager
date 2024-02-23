Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace ResourceManager.Helpers
    Public Class ViewPager(Of T As Class)
        Private _SelectedPageItem As Integer
        Private _totalRecords As Integer
        Private _totalPages As Integer
        Private _pageSize As Integer
        Public Sub New(ByVal contents As IEnumerable(Of T), ByVal Optional selectedPageItem As Integer = 1, ByVal Optional selectedRowsPerPage As String = "10")
            Me.Contents = contents
            ViewContent = [Goto](selectedPageItem, selectedRowsPerPage)
        End Sub
        Public Function [Goto](ByVal Optional selectedPageItem As Integer = 1, ByVal Optional selectedRowsPerPage As String = "10") As IEnumerable(Of T)
            _totalRecords = Contents.Count()
            Me.SelectedPageItem = selectedPageItem
            _pageSize = If(selectedRowsPerPage.Equals("all"), If(_totalRecords < 1, 1, _totalRecords), Convert.ToInt32(selectedRowsPerPage))
            _totalPages = If(_totalRecords < 1, 1, _totalRecords / _pageSize)
            If _totalRecords Mod _pageSize > 0 Then _totalPages += 1
            Dim pageItems As IList(Of Integer) = New List(Of Integer)()
            For i = 1 To _totalPages
                pageItems.Add(i)
            Next
            Me.PageItems = pageItems
            If Me.SelectedPageItem > _totalPages Then
                Me.SelectedPageItem = _totalPages
            End If
            Return Contents.Skip((Me.SelectedPageItem - 1) * _pageSize).Take(_pageSize)
        End Function

        Public Property SelectedPageItem As Integer
            Get
                Return _SelectedPageItem
            End Get
            Private Set(ByVal value As Integer)
                _SelectedPageItem = value
            End Set
        End Property
        Public Property PageItems As IEnumerable(Of Integer) = New List(Of Integer)()
        Public Property ViewContent As IEnumerable(Of T)
        Public ReadOnly Property Contents As IEnumerable(Of T)
    End Class
End Namespace
