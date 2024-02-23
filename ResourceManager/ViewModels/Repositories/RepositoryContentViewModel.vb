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
    Public Class RepositoryContentViewModel
        Inherits PageContentWithListViewModel(Of Repository)
        Private ReadOnly _dataServiceManager As IDataServiceManager
        Public Sub New(ByVal dataServiceManager As IDataServiceManager)
            MyBase.New(dataServiceManager.RepositoryDataService)
            _dataServiceManager = dataServiceManager
            MenuTitle = "Repository"
            MenuIcon = AssetManager.Instance.GetImage("Repository.png")
            Title = "Repository Manager"
            PageColor = RepositoryPageColor

            _edit_permission_name = ACTION_EDIT_REPOSITORY
            _add_permission_name = ACTION_ADD_REPOSITORY
            _delete_permission_name = ACTION_DEL_REPOSITORY

            TagsCommand = New AsyncRelayCommand(Of Repository)(AddressOf OnTagsAsync)
            ExploreCommand = New RelayCommand(Of Repository)(AddressOf OnExplore)

            MyBase.OnRefresh()
        End Sub

        Private Sub OnExplore(ByVal repo As Repository)
            Dim filename = repo.FilePath

            If Not File.Exists(filename) Then
                ShowPopupMessage("File '" & filename & "' doesn't exists.", Application.Settings.AppName, PopupButton.OK, PopupImage.[Error])
                Return
            End If
            Dim process = New Process()
            Dim startInfo = New ProcessStartInfo()
            startInfo.FileName = "explorer.exe"

            ' sqlite needs to have two slashes instead of one for the path it uses inside sqlite3
            startInfo.Arguments = $"/select,""{filename}"""
            startInfo.CreateNoWindow = True
            startInfo.UseShellExecute = False
            process.StartInfo = startInfo
            process.Start()
            process.WaitForExit()
            process.Close()
        End Sub

        Private Async Function OnTagsAsync(ByVal repo As Repository) As Task
            Dim vm = Application.ServiceProvider.GetRequiredService(Of TagsPopupViewModel)()
            vm.Caption = String.Format("Manage tag for {0}", repo.GetCaption())
            Dim tags = Await _dataServiceManager.TagDataService.GetTagObjectForRepositoryID(repo.ID)
            vm.Tags = tags.ToList()
            Dim popup = ShowPopup(vm)
            If popup.Result = PopupResult.OK Then
                Await _dataServiceManager.RepositoryDataService.UpdateTags(repo, vm.Tags)
                MyBase.OnRefresh()
            End If
        End Function


        Protected Overrides Sub OnEdit(ByVal repo As Repository)
            Dim vm = Application.ServiceProvider.GetRequiredService(Of RepositoryPopupViewModel)()
            vm.Caption = "Edit Repository"
            vm.SetRepo(repo)
            Dim popup = ShowPopup(vm)
            If popup.Result = PopupResult.OK Then
                MyBase.OnRefresh()
            End If
        End Sub

        Protected Overrides Sub OnAdd()
            Dim vm = Application.ServiceProvider.GetRequiredService(Of RepositoryPopupViewModel)()
            vm.Caption = "Add Repository"
            Dim popup = ShowPopup(vm)
            If popup.Result = PopupResult.OK Then
                MyBase.OnRefresh()
            End If
        End Sub
        Protected Overrides Sub OnFilter()
            MyBase.Filter(FilterColumns.Repository, "Filter Repository")
        End Sub

        Public ReadOnly Property TagsCommand As ICommand
        Public ReadOnly Property ExploreCommand As ICommand
    End Class
End Namespace
