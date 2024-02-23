Imports VBNetCore.Cryptography
Imports VBNetCore.Mvvm.Commands
Imports VBNetCore.Mvvm.Controls
Imports VBNetCore.Security
Imports VBNetCore.Validators
Imports ResourceManager.Helpers
Imports ResourceManager.Services.Abstractions
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows.Input
Imports System.Windows.Media.Imaging

Namespace ResourceManager.ViewModels
    Public Class LoginPageViewModel
        Inherits ViewModelBase
        Private ReadOnly _dataServiceManager As IDataServiceManager
        Public Sub New(ByVal dataServiceManager As IDataServiceManager)
            _dataServiceManager = dataServiceManager
            AddValidationRule(Function() Username, Function(x) TextValidator.Instance.Required(x), TextValidator.Instance.ErrorMessage("Required", "Username"))
            AddValidationRule(Function() Password, Function(x) TextValidator.Instance.Required(x), TextValidator.Instance.ErrorMessage("Required", "Password"))
            LoginCommand = New AsyncRelayCommand(AddressOf LoginAsync, AddressOf CanLogin)
            ShowPasswordCommand = New AsyncRelayCommand(AddressOf ShowPassword)
            HidePasswordCommand = New AsyncRelayCommand(AddressOf HidePasssword)
            ImageEye = AssetManager.Instance.GetImage("Hide.png")
            IsHidePassword = True
        End Sub

        Private Function HidePasssword() As Task
            ImageEye = AssetManager.Instance.GetImage("Hide.png")
            IsHidePassword = True

            Return Task.CompletedTask
        End Function

        Private Function ShowPassword() As Task
            ImageEye = AssetManager.Instance.GetImage("Show.png")
            IsHidePassword = False

            Return Task.CompletedTask
        End Function
        Private Function CanLogin() As Boolean
            Return Not HasErrors
        End Function
        Public ReadOnly Property LoginCommand As ICommand
        Public ReadOnly Property HidePasswordCommand As ICommand
        Public ReadOnly Property ShowPasswordCommand As ICommand
        Private Async Function LoginAsync() As Task
            ValidateAllRules()

            If Not HasErrors Then
                Dim loginUser = Await _dataServiceManager.UserDataService.FirstOrDefault(Function(x) Equals(x.AccountName, Username))

                If loginUser IsNot Nothing Then
                    If EncryptionManager.Instance.PasswordVerify(loginUser.Password, Password) Then

                        Dim resultRoles = Await _dataServiceManager.RoleDataService.GetRoleStringForUserID(loginUser.ID)
                        Dim resultPermissions = Await _dataServiceManager.PermissionDataService.GetPermissionForRoles(resultRoles)

                        AuthManager.User.Identity = New UserIdentity(loginUser.ID, loginUser.AccountName, resultRoles.ToArray(), resultPermissions.ToArray())

                        mainVM.RefreshUI()

                        Username = String.Empty
                        Password = String.Empty
                    Else
                        ShowPopupMessage("Username or password invalid.", Application.Settings.AppName, PopupButton.OK, PopupImage.[Error])
                    End If
                Else
                    ShowPopupMessage("Username not found.", Application.Settings.AppName, PopupButton.OK, PopupImage.[Error])
                End If
            End If
        End Function

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
                If SetProperty(value) Then
                    OnPropertyChanged(Function() PasswordNotEmpty)
                End If
            End Set
        End Property
        Public Property ImageEye As BitmapImage
            Get
                Return GetProperty(Of BitmapImage)()
            End Get
            Set(ByVal value As BitmapImage)
                SetProperty(value)
            End Set
        End Property
        Public Property IsHidePassword As Boolean
            Get
                Return GetProperty(Of Boolean)()
            End Get
            Set(ByVal value As Boolean)
                If SetProperty(value) Then
                    OnPropertyChanged(Function() IsShowPassword)
                End If
            End Set
        End Property
        Public ReadOnly Property IsShowPassword As Boolean
            Get
                Return Not IsHidePassword
            End Get
        End Property
        Public ReadOnly Property PasswordNotEmpty As Boolean
            Get
                If Password Is Nothing Then Return False
                Return Password.Length > 0
            End Get
        End Property
    End Class
End Namespace
