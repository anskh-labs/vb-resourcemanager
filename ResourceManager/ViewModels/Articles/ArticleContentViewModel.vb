Imports VBNetCore.Mvvm.Commands
Imports VBNetCore.Mvvm.Controls
Imports ResourceManager.Configuration
Imports ResourceManager.Helpers
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions
Imports System
Imports System.Diagnostics
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows.Input
Imports Microsoft.Extensions.DependencyInjection

Namespace ResourceManager.ViewModels
    Public Class ArticleContentViewModel
        Inherits PageContentWithListViewModel(Of Article)
        Private _dataServiceManager As IDataServiceManager
        Public Sub New(ByVal dataServiceManager As IDataServiceManager)
            MyBase.New(dataServiceManager.ArticleDataService)
            _dataServiceManager = dataServiceManager

            MenuTitle = "Article"
            MenuIcon = AssetManager.Instance.GetImage("Articles.png")
            Title = "Article Manager"
            PageColor = ArticlePageColor

            _add_permission_name = ACTION_ADD_ARTICLE
            _edit_permission_name = ACTION_EDIT_ARTICLE
            _delete_permission_name = ACTION_DEL_ARTICLE

            TagsCommand = New AsyncRelayCommand(Of Article)(AddressOf OnTagsAsync)
            OpenCommand = New RelayCommand(Of Article)(AddressOf OnOpen)

            MyBase.OnRefresh()
        End Sub

        Private Sub OnOpen(ByVal article As Article)
            Dim filename = article.FilePath

            If Not File.Exists(filename) Then
                ShowPopupMessage("File '" & filename & "' doesn't exists.", Application.Settings.AppName, PopupButton.OK, PopupImage.[Error])
                Return
            End If
            Try
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
            Catch ex As Exception
                ShowPopupMessage(ex.Message, Application.Settings.AppName, PopupButton.OK, PopupImage.[Error])
            End Try
        End Sub

        Private Async Function OnTagsAsync(ByVal article As Article) As Task
            Dim vm = Application.ServiceProvider.GetRequiredService(Of TagsPopupViewModel)()
            vm.Caption = String.Format("Manage tag for {0}", article.GetCaption())
            Dim tags = Await _dataServiceManager.TagDataService.GetTagObjectForArticleID(article.ID)
            vm.Tags = tags.ToList()
            Dim popup = ShowPopup(vm)
            If popup.Result = PopupResult.OK Then
                Try
                    Await _dataServiceManager.ArticleDataService.UpdateTags(article, vm.Tags)
                Catch ex As Exception
                    ShowPopupMessage(ex.Message, Application.Settings.AppName, PopupButton.OK, PopupImage.[Error])
                End Try
                MyBase.OnRefresh()
            End If
        End Function

        Protected Overrides Sub OnEdit(ByVal article As Article)
            Dim vm = Application.ServiceProvider.GetRequiredService(Of ArticlePopupViewModel)()
            vm.Caption = "Edit Article"
            vm.SetArticle(article)
            Dim popup = ShowPopup(vm)
            If popup.Result = PopupResult.OK Then
                MyBase.OnRefresh()
            End If
        End Sub

        Protected Overrides Sub OnAdd()
            Dim vm = Application.ServiceProvider.GetRequiredService(Of ArticlePopupViewModel)()
            vm.Caption = "Add Article"
            Dim popup = ShowPopup(vm)
            If popup.Result = PopupResult.OK Then
                MyBase.OnRefresh()
            End If
        End Sub

        Protected Overrides Sub OnFilter()
            MyBase.Filter(FilterColumns.Article, "Filter Article")
        End Sub

        Public ReadOnly Property TagsCommand As ICommand
        Public ReadOnly Property OpenCommand As ICommand
    End Class
End Namespace
