Imports VBNetCore.Mvvm.Commands
Imports VBNetCore.Mvvm.Controls
Imports VBNetCore.Mvvm.ViewModels
Imports VBNetCore.Security
Imports ResourceManager.Models
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Input

Namespace ResourceManager.ViewModels
    Public Class UserRolesPopupViewModel
        Inherits PopupViewModelBase
        Public Sub New()
            AddCommand = New RelayCommand(Of Role)(AddressOf AddRole, Function(x) SelectedRole IsNot Nothing)
            RemoveCommand = New RelayCommand(Of Role)(AddressOf RemoveRole, Function(x) SelectedUserRole IsNot Nothing)
            SaveCommand = New RelayCommand(AddressOf Save)
        End Sub

        Private Sub Save()
            Result = PopupResult.OK
            OnClose()
        End Sub

        Private Sub RemoveRole(ByVal role As Role)
            UserRoles = UserRoles.Where(Function(x) x.ID <> role.ID).ToList()
            Roles = Roles.Append(role).ToList()
        End Sub

        Private Sub AddRole(ByVal role As Role)
            UserRoles = UserRoles.Append(role).ToList()
            Roles = Roles.Where(Function(x) x.ID <> role.ID).ToList()
        End Sub
        Public Property UserRoles As IList(Of Role)
            Get
                Return GetProperty(Of IList(Of Role))()
            End Get
            Set(ByVal value As IList(Of Role))
                SetProperty(value)
            End Set
        End Property
        Public Property SelectedUserRole As Role
            Get
                Return GetProperty(Of Role)()
            End Get
            Set(ByVal value As Role)
                SetProperty(value)
            End Set
        End Property
        Public Property Roles As IList(Of Role)
            Get
                Return GetProperty(Of IList(Of Role))()
            End Get
            Set(ByVal value As IList(Of Role))
                SetProperty(value)
            End Set
        End Property
        Public Property SelectedRole As Role
            Get
                Return GetProperty(Of Role)()
            End Get
            Set(ByVal value As Role)
                SetProperty(value)
            End Set
        End Property
        Public ReadOnly Property CanEditUser As Boolean
            Get
                Return AuthManager.User.IsInPermission("a_edit_user")
            End Get
        End Property
        Public ReadOnly Property AddCommand As ICommand
        Public ReadOnly Property RemoveCommand As ICommand
        Public ReadOnly Property SaveCommand As ICommand
    End Class
End Namespace
