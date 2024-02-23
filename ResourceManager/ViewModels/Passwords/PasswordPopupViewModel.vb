Imports VBNetCore.Mvvm.Commands
Imports VBNetCore.Mvvm.Controls
Imports VBNetCore.Security
Imports VBNetCore.Validators
Imports ResourceManager.Helpers
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions
Imports System
Imports System.Threading.Tasks
Imports System.Windows.Input
Imports System.Windows.Media.Imaging
Imports System.Windows

Namespace ResourceManager.ViewModels
    Public Class PasswordPopupViewModel
        Inherits PopupViewModelBase
        Private ReadOnly _dataServiceManager As IDataServiceManager
        Private ID As Integer
        Private isEdit As Boolean

        Public Sub New(ByVal dataServiceManager As IDataServiceManager)
            _dataServiceManager = dataServiceManager
            isEdit = False

            AddValidationRule(Function() Name, Function(x) TextValidator.Instance.Required(x), TextValidator.Instance.ErrorMessage("Required", NameOf(PasswordPopupViewModel.Name)))
            AddValidationRule(Function() Username, Function(x) TextValidator.Instance.Required(x), TextValidator.Instance.ErrorMessage("Required", NameOf(PasswordPopupViewModel.Username)))
            AddValidationRule(Function() Pass, Function(x) TextValidator.Instance.Required(x), TextValidator.Instance.ErrorMessage("Required", NameOf(PasswordPopupViewModel.Pass)))

            SaveCommand = New AsyncRelayCommand(AddressOf OnSaveAsync, AddressOf CanSave)
            CopyCommand = New RelayCommand(AddressOf OnCopy, AddressOf CanCopy)

            ShowPasswordCommand = New AsyncRelayCommand(AddressOf ShowPassword)
            HidePasswordCommand = New AsyncRelayCommand(AddressOf HidePasssword)
            ImageEye = AssetManager.Instance.GetImage("Hide.png")

            IsHidePassword = True
        End Sub

        Private Sub OnCopy()
            Clipboard.SetText(Me.Pass)
            windowManager.ShowMessageBox("Text copied!")
        End Sub

        Private Function CanCopy() As Boolean
            Return Pass.Length > 0
        End Function

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
                Return Pass.Length > 0
            End Get
        End Property
        Private Function CanSave() As Boolean
            Return Not HasErrors
        End Function
        Public Sub SetPassword(ByVal pass As Password)
            isEdit = True

            ID = pass.ID
            Name = pass.Name
            Username = pass.Username
            Me.Pass = pass.Pass
            Url = pass.Url
            Description = pass.Description
        End Sub
        Private Async Function OnSaveAsync() As Task
            ValidateAllRules()

            If Not HasErrors Then
                Try
                    If isEdit Then
                        Dim editPassword = New Password() With {
    .Name = Name,
    .Username = Username,
    .Pass = Me.Pass,
    .Url = Url,
    .Description = Description,
    .UserID = AuthManager.User.Identity.ID
}
                        Dim pass = Await _dataServiceManager.PasswordDataService.Update(ID, editPassword)
                        Result = If(pass IsNot Nothing, PopupResult.OK, PopupResult.None)
                    Else
                        Dim pass = Await _dataServiceManager.PasswordDataService.Create(New Password() With {
                            .Name = Name,
                            .Username = Username,
                            .Pass = Me.Pass,
                            .Url = Url,
                            .Description = Description,
                            .UserID = AuthManager.User.Identity.ID
                        })
                        Result = If(pass IsNot Nothing, PopupResult.OK, PopupResult.None)
                    End If
                Catch e As Exception
                    windowManager.ShowMessageBox(e.Message, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error)
                End Try
                OnClose()
            End If
        End Function

        Public Property Name As String
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
        Public Property Pass As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                If SetProperty(value) Then
                    OnPropertyChanged(Function() PasswordNotEmpty)
                End If
            End Set
        End Property
        Public Property Url As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                SetProperty(value)
            End Set
        End Property
        Public Property Description As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                SetProperty(value)
            End Set
        End Property
        Public ReadOnly Property SaveCommand As ICommand
        Public ReadOnly Property HidePasswordCommand As ICommand
        Public ReadOnly Property ShowPasswordCommand As ICommand
        Public ReadOnly Property CopyCommand As ICommand
    End Class
End Namespace
