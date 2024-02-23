Imports VBNetCore.Mvvm.Commands
Imports VBNetCore.Mvvm.Controls
Imports ResourceManager.Configuration
Imports ResourceManager.Helpers
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions
Imports System
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows.Input
Imports Microsoft.Extensions.DependencyInjection

Namespace ResourceManager.ViewModels
    Public Class ActivityContentViewModel
        Inherits PageContentWithListViewModel(Of Activity)
        Private ReadOnly _dataServiceManager As IDataServiceManager
        Public Sub New(ByVal dataServiceManager As IDataServiceManager)
            MyBase.New(dataServiceManager.ActivityDataService)
            _dataServiceManager = dataServiceManager

            MenuTitle = "Activity"
            MenuIcon = AssetManager.Instance.GetImage("Activity.png")
            Title = "Activity Manager"
            PageColor = ActivityPageColor

            _add_permission_name = ACTION_ADD_ACTIVITY
            _edit_permission_name = ACTION_EDIT_ACTIVITY
            _delete_permission_name = ACTION_DEL_ACTIVITY

            TagsCommand = New AsyncRelayCommand(Of Activity)(AddressOf OnTagsAsync)

            MyBase.OnRefresh()
        End Sub

        Private Async Function OnTagsAsync(ByVal activity As Activity) As Task
            Dim vm = Application.ServiceProvider.GetRequiredService(Of TagsPopupViewModel)()
            vm.Caption = String.Format("Manage tag for {0}", activity.GetCaption())
            Dim tags = Await _dataServiceManager.TagDataService.GetTagObjectForActivityID(activity.ID)
            vm.Tags = tags.ToList()
            Dim popup = ShowPopup(vm)
            If popup.Result = PopupResult.OK Then
                Try
                    Await _dataServiceManager.ActivityDataService.UpdateTags(activity, vm.Tags)
                Catch ex As Exception
                    ShowPopupMessage(ex.Message, Application.Settings.AppName, PopupButton.OK, PopupImage.[Error])
                End Try
                MyBase.OnRefresh()
            End If
        End Function

        Protected Overrides Sub OnEdit(ByVal activity As Activity)
            Dim vm = Application.ServiceProvider.GetRequiredService(Of ActivityPopupViewModel)()
            vm.Caption = "Edit Activity"
            vm.SetActivity(activity)
            Dim popup = ShowPopup(vm)
            If popup.Result = PopupResult.OK Then
                MyBase.OnRefresh()
            End If
        End Sub

        Protected Overrides Sub OnAdd()
            Dim vm = Application.ServiceProvider.GetRequiredService(Of ActivityPopupViewModel)()
            vm.Caption = "Add Activity"
            Dim popup = ShowPopup(vm)
            If popup.Result = PopupResult.OK Then
                MyBase.OnRefresh()
            End If
        End Sub

        Protected Overrides Sub OnFilter()
            MyBase.Filter(FilterColumns.Activity, "Filter Activity")
        End Sub

        Public ReadOnly Property TagsCommand As ICommand
    End Class
End Namespace
