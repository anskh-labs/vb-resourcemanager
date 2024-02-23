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
    Public Class UserContentViewModel
        Inherits PageContentWithListViewModel(Of User)
        Private ReadOnly _dataServiceManager As IDataServiceManager

        Public Sub New(ByVal dataServiceManager As IDataServiceManager)
            MyBase.New(dataServiceManager.UserDataService)
            _dataServiceManager = dataServiceManager

            MenuTitle = "User"
            MenuIcon = AssetManager.Instance.GetImage("Users.png")
            Title = "User Manager"
            PageColor = UserPageColor
            RolesUserCommand = New RelayCommand(Of User)(AddressOf RolesUser)

            _add_permission_name = ACTION_ADD_USER
            _edit_permission_name = ACTION_EDIT_USER
            _delete_permission_name = ACTION_DEL_USER

            MyBase.OnRefresh()
        End Sub

        Private Async Sub RolesUser(ByVal user As User)
            Dim vm = Application.ServiceProvider.GetRequiredService(Of UserRolesPopupViewModel)()
            vm.Caption = "Manage Role for user with account name '" & user.AccountName & "'"
            Dim roles = Await _dataServiceManager.RoleDataService.GetAll()
            Dim userRoles = Await _dataServiceManager.RoleDataService.GetRoleObjectForUserID(user.ID)
            For Each role In userRoles
                roles.Remove(roles.First(Function(x) x.ID = role.ID))
            Next
            vm.Roles = roles
            vm.UserRoles = userRoles
            Dim popup = ShowPopup(vm)
            If popup IsNot Nothing AndAlso popup.Result = PopupResult.OK Then
                Try
                    Dim res = Await _dataServiceManager.UserDataService.UpdateRoles(user, popup.UserRoles)
                Catch e As Exception
                    windowManager.ShowMessageBox(e.Message, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error)
                End Try
                MyBase.OnRefresh()
            End If
        End Sub

        Protected Overrides Sub OnEdit(ByVal user As User)
            Dim vm = Application.ServiceProvider.GetRequiredService(Of UserPopupViewModel)()
            vm.Caption = "Edit User"
            vm.SetUser(user)
            Dim popup = ShowPopup(vm)
            If popup.Result = PopupResult.OK Then
                MyBase.OnRefresh()
            End If
        End Sub

        Protected Overrides Sub OnAdd()
            Dim vm = Application.ServiceProvider.GetRequiredService(Of UserPopupViewModel)()
            vm.Caption = "Add User"
            Dim popup = ShowPopup(vm)
            If popup.Result = PopupResult.OK Then
                MyBase.OnRefresh()
            End If
        End Sub
        Protected Overrides Sub OnFilter()
            MyBase.Filter(FilterColumns.User, "Filter User")
        End Sub
        Public ReadOnly Property RolesUserCommand As ICommand
    End Class
End Namespace
