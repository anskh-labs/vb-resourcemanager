Imports VBNetCore.Mvvm.Commands
Imports VBNetCore.Mvvm.Controls
Imports VBNetCore.Mvvm.ViewModels
Imports VBNetCore.Security
Imports ResourceManager.Models
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Input

Namespace ResourceManager.ViewModels
    Public Class RolePermissionsPopupViewModel
        Inherits PopupViewModelBase
        Public Sub New()
            AddCommand = New RelayCommand(Of Permission)(AddressOf AddPermission, Function(x) SelectedPermission IsNot Nothing)
            RemoveCommand = New RelayCommand(Of Permission)(AddressOf RemovePermission, Function(x) SelectedRolePermission IsNot Nothing)
            SaveCommand = New RelayCommand(AddressOf Save)
        End Sub

        Private Sub Save()
            Result = PopupResult.OK
            OnClose()
        End Sub

        Private Sub RemovePermission(ByVal perm As Permission)
            RolePermissions = RolePermissions.Where(Function(x) x.ID <> perm.ID).ToList()
            Permissions = Permissions.Append(perm).ToList()
        End Sub

        Private Sub AddPermission(ByVal perm As Permission)
            RolePermissions = RolePermissions.Append(perm).ToList()
            Permissions = Permissions.Where(Function(x) x.ID <> perm.ID).ToList()
        End Sub
        Public Property RolePermissions As IList(Of Permission)
            Get
                Return GetProperty(Of IList(Of Permission))()
            End Get
            Set(ByVal value As IList(Of Permission))
                SetProperty(value)
            End Set
        End Property
        Public Property SelectedRolePermission As Permission
            Get
                Return GetProperty(Of Permission)()
            End Get
            Set(ByVal value As Permission)
                SetProperty(value)
            End Set
        End Property
        Public Property Permissions As IList(Of Permission)
            Get
                Return GetProperty(Of IList(Of Permission))()
            End Get
            Set(ByVal value As IList(Of Permission))
                SetProperty(value)
            End Set
        End Property
        Public Property SelectedPermission As Permission
            Get
                Return GetProperty(Of Permission)()
            End Get
            Set(ByVal value As Permission)
                SetProperty(value)
            End Set
        End Property
        Public ReadOnly Property CanEditRole As Boolean
            Get
                Return AuthManager.User.IsInPermission("a_edit_role")
            End Get
        End Property
        Public ReadOnly Property AddCommand As ICommand
        Public ReadOnly Property RemoveCommand As ICommand
        Public ReadOnly Property SaveCommand As ICommand
    End Class
End Namespace
