Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Mvvm.Commands
Imports VBNetCore.Mvvm.Controls
Imports VBNetCore.Mvvm.ViewModels
Imports VBNetCore.Security
Imports VBNetCore.Validators
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions
Imports System
Imports System.Threading.Tasks
Imports System.Windows.Input

Namespace ResourceManager.ViewModels
    Public Class NotePopupViewModel
        Inherits PopupViewModel
        Private windowManager As IWindowManager
        Private ReadOnly _dataServiceManager As IDataServiceManager
        Private ID As Integer
        Private isEdit As Boolean

        Public Sub New(ByVal dataServiceManager As IDataServiceManager, ByVal windowManager As IWindowManager)
            _dataServiceManager = dataServiceManager
            Me.windowManager = windowManager
            isEdit = False

            AddValidationRule(Function() Title, Function(x) TextValidator.Instance.Required(x), TextValidator.Instance.ErrorMessage("Required", NameOf(NotePopupViewModel.Title)))
            AddValidationRule(Function() Notes, Function(x) TextValidator.Instance.Required(x), TextValidator.Instance.ErrorMessage("Required", NameOf(NotePopupViewModel.Notes)))

            [Date] = Date.Now.Date

            SaveCommand = New AsyncRelayCommand(AddressOf OnSaveAsync, AddressOf CanSave)
        End Sub

        Private Function CanSave() As Boolean
            Return Not HasErrors
        End Function
        Public Sub SetNote(ByVal note As Note)
            isEdit = True
            ID = note.ID
            Title = note.Title
            [Date] = note.Date
            Notes = note.Notes
        End Sub
        Private Async Function OnSaveAsync() As Task
            ValidateAllRules()

            If Not HasErrors Then
                Try
                    Dim note = New Note() With {
    .Title = Title,
    .[Date] = [Date].Date,
    .Notes = Notes,
    .UserID = AuthManager.User.Identity.ID
}

                    If isEdit Then
                        Await _dataServiceManager.NoteDataService.Update(ID, note)

                        Result = PopupResult.OK
                    Else
                        Await _dataServiceManager.NoteDataService.Create(note)

                        Result = PopupResult.OK
                    End If
                Catch e As Exception
                    windowManager.ShowMessageBox(e.Message, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error)
                End Try
                OnClose()
            End If
        End Function

        Public Property Title As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                SetProperty(value)
            End Set
        End Property
        Public Property [Date] As Date
            Get
                Return GetProperty(Of Date)()
            End Get
            Set(ByVal value As Date)
                SetProperty(value)
            End Set
        End Property

        Public Property Notes As String
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
