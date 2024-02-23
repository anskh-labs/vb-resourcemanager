Imports VBNetCore.Mvvm.Controls
Imports ResourceManager.Configuration
Imports ResourceManager.Helpers
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions
Imports System.Windows.Input
Imports Microsoft.Extensions.DependencyInjection

Namespace ResourceManager.ViewModels
    Public Class PermissionContentViewModel
        Inherits PageContentWithListViewModel(Of Permission)

        Public Sub New(ByVal dataService As IPermissionDataService)
            MyBase.New(dataService)
            MenuTitle = "Permissions"
            MenuIcon = AssetManager.Instance.GetImage("Permissions.png")
            Title = "Permission Manager"
            PageColor = UserPageColor

            _edit_permission_name = ACTION_EDIT_PERMISSION
            _add_permission_name = ACTION_ADD_PERMISSION
            _delete_permission_name = ACTION_DEL_PERMISSION

            MyBase.OnRefresh()
        End Sub
        Protected Overrides Sub OnAdd()
            Dim vm = Application.ServiceProvider.GetRequiredService(Of PermissionPopupViewModel)()
            vm.Caption = "Add Permission"
            Dim popup = ShowPopup(vm)
            If popup.Result = PopupResult.OK Then
                MyBase.OnRefresh()
            End If
        End Sub

        Protected Overrides Sub OnEdit(ByVal perm As Permission)
            Dim vm = Application.ServiceProvider.GetRequiredService(Of PermissionPopupViewModel)()
            vm.Caption = "Edit Permission"
            vm.SetPermission(perm)
            Dim popup = ShowPopup(vm)
            If popup.Result = PopupResult.OK Then
                MyBase.OnRefresh()
            End If
        End Sub

        Protected Overrides Sub OnFilter()
            MyBase.Filter(FilterColumns.Permission, "Filter Permission")
        End Sub
        Public ReadOnly Property RolePermissionsCommand As ICommand
    End Class
End Namespace
