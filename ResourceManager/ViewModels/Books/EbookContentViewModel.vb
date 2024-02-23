Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Mvvm.Commands
Imports VBNetCore.Mvvm.Controls
Imports ResourceManager.Configuration
Imports ResourceManager.Helpers
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions
Imports System.Diagnostics
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows.Input
Imports Microsoft.Extensions.DependencyInjection

Namespace ResourceManager.ViewModels
    Public Class EbookContentViewModel
        Inherits PageContentWithListViewModel(Of Book)
        Private _dataServiceManager As IDataServiceManager
        Public Sub New(ByVal dataServiceManager As IDataServiceManager)
            MyBase.New(dataServiceManager.BookDataService)
            _dataServiceManager = dataServiceManager

            MenuTitle = "Ebook"
            MenuIcon = AssetManager.Instance.GetImage("Ebooks.png")
            Title = "Ebook Manager"
            PageColor = EbookPageColor

            _edit_permission_name = ACTION_EDIT_EBOOK
            _add_permission_name = ACTION_ADD_EBOOK
            _delete_permission_name = ACTION_DEL_EBOOK

            TagsCommand = New AsyncRelayCommand(Of Book)(AddressOf OnTagsAsync)
            OpenCommand = New RelayCommand(Of Book)(AddressOf OnOpen)

            MyBase.OnRefresh()
        End Sub

        Private Sub OnOpen(ByVal book As Book)
            Dim filename = book.FilePath
            If Not File.Exists(filename) Then
                ShowPopupMessage("File '" & filename & "' doesn't exists.", Application.Settings.AppName, PopupButton.OK, PopupImage.[Error])
                Return
            End If
            Dim process = New Process()
            Dim startInfo = New ProcessStartInfo()
            startInfo.FileName = "cmd.exe"

            ' sqlite needs to have two slashes instead of one for the path it uses inside sqlite3
            startInfo.Arguments = $"/c ""{filename}"""
            startInfo.CreateNoWindow = True
            startInfo.UseShellExecute = False
            process.StartInfo = startInfo
            process.Start()
            'process.WaitForExit();
            'process.Close();
            process.Dispose()
        End Sub

        Private Async Function OnTagsAsync(ByVal book As Book) As Task
            Dim vm = Application.ServiceProvider.GetRequiredService(Of TagsPopupViewModel)()
            vm.Caption = String.Format("Manage tag for {0}", book.GetCaption())
            Dim tags = Await _dataServiceManager.TagDataService.GetTagObjectForBookID(book.ID)
            vm.Tags = tags.ToList()
            Dim popup = ShowPopup(vm)
            If popup.Result = PopupResult.OK Then
                Await _dataServiceManager.BookDataService.UpdateTags(book, vm.Tags)
                MyBase.OnRefresh()
            End If
        End Function

        Protected Overrides Sub OnEdit(ByVal book As Book)
            Dim vm = Application.ServiceProvider.GetRequiredService(Of EbookPopupViewModel)()
            vm.Caption = "Edit Book"
            vm.SetBook(book)
            Dim popup = ShowPopup(vm)
            If popup.Result = PopupResult.OK Then
                MyBase.OnRefresh()
            End If
        End Sub

        Protected Overrides Sub OnAdd()
            Dim vm = Application.ServiceProvider.GetRequiredService(Of EbookPopupViewModel)()
            vm.Caption = "Add Book"
            Dim popup = ShowPopup(vm)
            If popup.Result = PopupResult.OK Then
                MyBase.OnRefresh()
            End If
        End Sub
        Protected Overrides Sub OnDelete(ByVal book As Book)
            Dim result = ShowPopupMessage("Delete " & book.GetCaption() & "", "Confirmation", PopupButton.YesNo, PopupImage.Question)
            If result = PopupResult.Yes Then
                _dataService.Delete(book.ID)
                Dim filename = Path.Combine(Application.Settings.ArticleSettings.FolderPath, book.Filename)
                If File.Exists(filename) Then File.Delete(filename)

                If File.Exists(book.Cover) Then File.Delete(book.Cover)
                MyBase.OnRefresh()
            End If
        End Sub

        Protected Overrides Sub OnFilter()
            MyBase.Filter(FilterColumns.Book, "Filter Book")
        End Sub

        Public ReadOnly Property TagsCommand As ICommand
        Public ReadOnly Property OpenCommand As ICommand
    End Class
End Namespace
