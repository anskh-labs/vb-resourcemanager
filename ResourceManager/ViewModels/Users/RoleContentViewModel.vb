Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Mvvm.Commands
Imports VBNetCore.Mvvm.Controls
Imports ResourceManager.Configuration
Imports ResourceManager.Helpers
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions
Imports System
Imports System.Linq
Imports System.Windows.Input
Imports Microsoft.Extensions.DependencyInjection

Namespace ResourceManager.ViewModels
    Public Class RoleContentViewModel
        Inherits PageContentWithListViewModel(Of Role)
        Private _dataServiceManager As IDataServiceManager
        Public Sub New(ByVal dataServiceManager As IDataServiceManager)
            MyBase.New(dataServiceManager.RoleDataService)
            _dataServiceManager = dataServiceManager

            MenuTitle = "Roles"
            MenuIcon = AssetManager.Instance.GetImage("Roles.png")
            Title = "Role Manager"
            PageColor = UserPageColor

            _edit_permission_name = ACTION_EDIT_ROLE
            _add_permission_name = ACTION_ADD_ROLE
            _delete_permission_name = ACTION_DEL_ROLE

            RolePermissionsCommand = New RelayCommand(Of Role)(AddressOf RolePermissionsAsync)

            MyBase.OnRefresh()
        End Sub

        Private Async Sub RolePermissionsAsync(ByVal role As Role)
            Dim vm = Application.ServiceProvider.GetRequiredService(Of RolePermissionsPopupViewModel)()
            vm.Caption = "Manage Permission for role with name '" & role.Name & "'"
            Dim perms = Await _dataServiceManager.PermissionDataService.GetAll()
            Dim rolePerms = Await _dataServiceManager.PermissionDataService.GetPermissionObjectForRoleID(role.ID)
            For Each perm In rolePerms
                perms.Remove(perms.First(Function(x) x.ID = perm.ID))
            Next
            vm.Permissions = perms
            vm.RolePermissions = rolePerms
            Dim popup = ShowPopup(vm)
            If popup IsNot Nothing AndAlso popup.Result = PopupResult.OK Then
                Try
                    Dim res = Await _dataServiceManager.RoleDataService.UpdatePermissions(role, popup.RolePermissions)
                Catch e As Exception
                    windowManager.ShowMessageBox(e.Message, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error)
                End Try
                MyBase.OnRefresh()
            End If
        End Sub

        Protected Overrides Sub OnEdit(ByVal role As Role)
            Dim vm = Application.ServiceProvider.GetRequiredService(Of RolePopupViewModel)()
            vm.Caption = "Edit Role"
            vm.SetRole(role)
            Dim popup = ShowPopup(vm)
            If popup.Result = PopupResult.OK Then
                MyBase.OnRefresh()
            End If
        End Sub

        Protected Overrides Sub OnAdd()
            Dim vm = Application.ServiceProvider.GetRequiredService(Of RolePopupViewModel)()
            vm.Caption = "Add Role"
            Dim popup = ShowPopup(vm)
            If popup.Result = PopupResult.OK Then
                MyBase.OnRefresh()
            End If
        End Sub
        Protected Overrides Sub OnFilter()
            MyBase.Filter(FilterColumns.Role, "Filter Role")
        End Sub
        Public ReadOnly Property RolePermissionsCommand As ICommand

    End Class
End Namespace
