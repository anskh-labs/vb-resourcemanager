Imports VBNetCore.Cryptography
Imports VBNetCore.Mvvm.Commands
Imports VBNetCore.Mvvm.Controls
Imports VBNetCore.Mvvm.ViewModels
Imports VBNetCore.Validators
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions
Imports System
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows.Input

Namespace ResourceManager.ViewModels
    Public Class UserPopupViewModel
        Inherits PopupViewModelBase
        Private ReadOnly _userDataService As IUserDataService
        Private ID As Integer
        Private isEdit As Boolean

        Public Sub New(ByVal userDataService As IUserDataService)
            _userDataService = userDataService
            isEdit = False

            AddValidationRule(Function() RealName, Function(x) TextValidator.Instance.Required(x), TextValidator.Instance.ErrorMessage("Required", "Real Name"))
            AddValidationRule(Function() Username, Function(x) TextValidator.Instance.Required(x), TextValidator.Instance.ErrorMessage("Required", "Username"))
            AddValidationRule(Function() Password, Function(x) TextValidator.Instance.Required(x), TextValidator.Instance.ErrorMessage("Required", "Password"))
            AddValidationRule(Function() RepeatPassword, Function(x) TextValidator.Instance.Required(x), TextValidator.Instance.ErrorMessage("Required", "Repeat Password"))
            AddValidationRule(Function() RepeatPassword, Function(x) Equals(Password, RepeatPassword), "Repeat Password must be equal to Password")
            SaveCommand = New AsyncRelayCommand(AddressOf OnSaveAsync, AddressOf CanSave)
        End Sub

        Private Function CanSave() As Boolean
            Return Not HasErrors
        End Function
        Public Sub SetUser(ByVal user As User)
            isEdit = True
            ID = user.ID
            RealName = user.Name
            Username = user.AccountName
            Password = EncryptionManager.Instance.DecryptString(user.Password, EncryptionManager.KEY)
            RepeatPassword = Password
        End Sub
        Private Async Function OnSaveAsync() As Task
            ValidateAllRules()

            If Not HasErrors Then
                Try
                    If isEdit Then
                        Dim editUser = New User() With {
    .Name = RealName,
    .AccountName = Username,
    .Password = EncryptionManager.Instance.EncryptString(Password, EncryptionManager.KEY)
}
                        Dim user = Await _userDataService.Update(ID, editUser)
                        If user IsNot Nothing Then
                            Result = PopupResult.OK
                        Else
                            Result = PopupResult.None
                        End If
                    Else
                        Dim existUser = (Await _userDataService.GetWhere(Function(x) x.Name.Equals(RealName) OrElse x.AccountName.Equals(Username))).SingleOrDefault()
                        If existUser IsNot Nothing Then

                        End If

                        Dim user = Await _userDataService.Create(New User() With {
                            .Name = RealName,
                            .AccountName = Username,
                            .Password = EncryptionManager.Instance.EncryptString(Password, EncryptionManager.KEY)
                        })
                        If user IsNot Nothing Then
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

        Public Property RealName As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                SetProperty(value)
            End Set
        End Property
        Public Property Username As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                SetProperty(value)
            End Set
        End Property
        Public Property Password As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                SetProperty(value)
            End Set
        End Property
        Public Property RepeatPassword As String
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
