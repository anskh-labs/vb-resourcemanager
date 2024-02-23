Imports VBNetCore.Mvvm.Commands
Imports VBNetCore.Mvvm.Controls
Imports ResourceManager.Configuration
Imports ResourceManager.Helpers
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows.Input
Imports Microsoft.Extensions.DependencyInjection

Namespace ResourceManager.ViewModels
    Public Class NoteContentViewModel
        Inherits PageContentWithListViewModel(Of Note)
        Private ReadOnly _dataServiceManager As IDataServiceManager
        Public Sub New(ByVal dataServiceManager As IDataServiceManager)
            MyBase.New(dataServiceManager.NoteDataService)
            _dataServiceManager = dataServiceManager

            MenuTitle = "Note"
            MenuIcon = AssetManager.Instance.GetImage("Note.png")
            Title = "Note Manager"
            PageColor = NotePageColor

            _add_permission_name = ACTION_ADD_NOTE
            _edit_permission_name = ACTION_EDIT_NOTE
            _delete_permission_name = ACTION_DEL_NOTE

            TagsCommand = New AsyncRelayCommand(Of Note)(AddressOf OnTagsAsync)

            MyBase.OnRefresh()
        End Sub

        Private Async Function OnTagsAsync(ByVal note As Note) As Task
            Dim vm = Application.ServiceProvider.GetRequiredService(Of TagsPopupViewModel)()
            vm.Caption = String.Format("Manage tag for {0}", note.GetCaption())
            Dim tags = Await _dataServiceManager.TagDataService.GetTagObjectForNoteID(note.ID)
            vm.Tags = tags.ToList()
            Dim popup = ShowPopup(vm)
            If popup.Result = PopupResult.OK Then
                Await _dataServiceManager.NoteDataService.UpdateTags(note, vm.Tags)
                MyBase.OnRefresh()
            End If
        End Function

        Protected Overrides Sub OnEdit(ByVal note As Note)
            Dim vm = Application.ServiceProvider.GetRequiredService(Of NotePopupViewModel)()
            vm.Caption = "Edit Note"
            vm.SetNote(note)
            Dim popup = ShowPopup(vm)
            If popup.Result = PopupResult.OK Then
                MyBase.OnRefresh()
            End If
        End Sub

        Protected Overrides Sub OnAdd()
            Dim vm = Application.ServiceProvider.GetRequiredService(Of NotePopupViewModel)()
            vm.Caption = "Add Note"
            Dim popup = ShowPopup(vm)
            If popup.Result = PopupResult.OK Then
                MyBase.OnRefresh()
            End If
        End Sub

        Protected Overrides Sub OnFilter()
            MyBase.Filter(FilterColumns.Note, "Filter Note")
        End Sub

        Public ReadOnly Property TagsCommand As ICommand
    End Class
End Namespace
