Imports VBNetCore.Mvvm.Commands
Imports VBNetCore.Mvvm.Controls
Imports VBNetCore.Validators
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions
Imports System
Imports System.Threading.Tasks
Imports System.Windows.Input

Namespace ResourceManager.ViewModels
    Public Class RolePopupViewModel
        Inherits PopupViewModelBase
        Private ReadOnly _dataServiceManager As IDataServiceManager
        Private ID As Integer
        Private isEdit As Boolean
        Public Sub New(ByVal dataServiceManager As IDataServiceManager)
            _dataServiceManager = dataServiceManager
            isEdit = False

            AddValidationRule(Function() RoleName, Function(text) TextValidator.Instance.Required(text), TextValidator.Instance.ErrorMessage("Required", "Role Name"))
            AddValidationRule(Function() RoleDescription, Function(text) TextValidator.Instance.Required(text), TextValidator.Instance.ErrorMessage("Required", "Role Description"))
            SaveCommand = New AsyncRelayCommand(AddressOf OnSaveAsync, AddressOf CanSave)
        End Sub

        Private Function CanSave() As Boolean
            Return Not HasErrors
        End Function
        Public Sub SetRole(ByVal role As Role)
            isEdit = True
            ID = role.ID
            RoleName = role.Name
            RoleDescription = role.Description
        End Sub
        Private Async Function OnSaveAsync() As Task
            ValidateAllRules()

            If Not HasErrors Then
                Try
                    If isEdit Then
                        Dim editRole = New Role() With {
    .Name = RoleName,
    .Description = RoleDescription
}
                        Dim role = Await _dataServiceManager.RoleDataService.Update(ID, editRole)
                        If role IsNot Nothing Then
                            Result = PopupResult.OK
                        Else
                            Result = PopupResult.None
                        End If
                    Else
                        Dim role = Await _dataServiceManager.RoleDataService.Create(New Role() With {
                            .Name = RoleName,
                            .Description = RoleDescription
                        })
                        If role IsNot Nothing Then
                            Result = PopupResult.OK
                        Else
                            Result = PopupResult.None
                        End If
                    End If
                Catch e As Exception
                    windowManager.ShowMessageBox(e.Message, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error)
                End Try
                OnClose()
            End If
        End Function

        Public Property RoleName As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                SetProperty(value)
            End Set
        End Property
        Public Property RoleDescription As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                SetProperty(value)
            End Set
        End Property

        Public ReadOnly Property SaveCommand As ICommand
    End Class
End Namespace
