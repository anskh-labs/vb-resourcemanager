Imports Microsoft.Extensions.Options
Imports VBNetCore.Cryptography
Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Mvvm.Commands
Imports VBNetCore.Mvvm.Controls
Imports VBNetCore.Mvvm.ViewModels
Imports VBNetCore.Validators
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions
Imports ResourceManager.Settings
Imports System
Imports System.Threading.Tasks
Imports System.Windows.Input

Namespace ResourceManager.ViewModels
    Public Class PermissionPopupViewModel
        Inherits PopupViewModelBase
        Private ReadOnly _dataServiceManager As IDataServiceManager
        Private ID As Integer
        Private isEdit As Boolean

        Public Sub New(ByVal serviceProvider As IServiceProvider, ByVal settings As IOptions(Of AppSettings), ByVal dataServiceManager As IDataServiceManager, ByVal windowManager As IWindowManager)
            _dataServiceManager = dataServiceManager
            isEdit = False

            AddValidationRule(Function() PermissionName, Function(text) TextValidator.Instance.Required(text), TextValidator.Instance.ErrorMessage("Required", "Permission Name"))
            AddValidationRule(Function() PermissionDescription, Function(text) TextValidator.Instance.Required(text), TextValidator.Instance.ErrorMessage("Required", "Permission Description"))
            SaveCommand = New AsyncRelayCommand(AddressOf OnSaveAsync, AddressOf CanSave)
        End Sub

        Private Function CanSave() As Boolean
            Return Not HasErrors
        End Function
        Public Sub SetPermission(ByVal perm As Permission)
            isEdit = True
            ID = perm.ID
            PermissionName = perm.Name
            PermissionDescription = perm.Description
        End Sub
        Private Async Function OnSaveAsync() As Task
            ValidateAllRules()

            If Not HasErrors Then
                Try
                    If isEdit Then
                        Dim editPerm = New Permission() With {
    .Name = PermissionName,
    .Description = PermissionDescription
}
                        Dim perm = Await _dataServiceManager.PermissionDataService.Update(ID, editPerm)
                        If perm IsNot Nothing Then
                            Result = PopupResult.OK
                        Else
                            Result = PopupResult.None
                        End If
                    Else
                        Dim perm = Await _dataServiceManager.PermissionDataService.Create(New Permission() With {
                            .Name = PermissionName,
                            .Description = PermissionDescription
                        })
                        If perm IsNot Nothing Then
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

        Public Property PermissionName As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                SetProperty(value)
            End Set
        End Property
        Public Property PermissionDescription As String
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
