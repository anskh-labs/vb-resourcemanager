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
    Public Class ActivityPopupViewModel
        Inherits PopupViewModelBase
        Private ReadOnly _dataServiceManager As IDataServiceManager
        Private ID As Integer
        Private isEdit As Boolean

        Public Sub New(ByVal dataServiceManager As IDataServiceManager, ByVal windowManager As IWindowManager)
            _dataServiceManager = dataServiceManager
            isEdit = False

            AddValidationRule(Function() Title, Function(x) TextValidator.Instance.Required(x), TextValidator.Instance.ErrorMessage("Required", NameOf(ActivityPopupViewModel.Title)))
            AddValidationRule(Function() Metric, Function(x) TextValidator.Instance.Required(x), TextValidator.Instance.ErrorMessage("Required", NameOf(ActivityPopupViewModel.Metric)))
            AddValidationRule(Function() EndTime, Function() Duration > 0, NameOf(ActivityPopupViewModel.EndTime) & " must greater than start time.")
            AddValidationRule(Function() Quantity, Function() Quantity > 0, NameOf(ActivityPopupViewModel.Quantity) & " must greater than 0.")

            Dim now = Date.Now
            [Date] = now.Date
            StartTime = New DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0)
            EndTime = New DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0)

            SaveCommand = New AsyncRelayCommand(AddressOf OnSaveAsync, AddressOf CanSave)
        End Sub

        Private Function CanSave() As Boolean
            Return Not HasErrors
        End Function
        Public Sub SetActivity(ByVal activity As Activity)
            isEdit = True
            ID = activity.ID
            Title = activity.Title
            Duration = activity.Duration
            Quantity = activity.Quantity
            Metric = activity.Metric
            [Date] = activity.Date
            StartTime = activity.StartTime
            EndTime = activity.EndTime
            Output = activity.Output
            Note = activity.Note
        End Sub
        Private Async Function OnSaveAsync() As Task
            ValidateAllRules()

            If Not HasErrors Then
                Try
                    Dim act = New Activity() With {
    .Title = Title,
    .Duration = Duration,
    .Quantity = Quantity,
    .Metric = Metric,
    .[Date] = [Date].Date,
    .StartTime = StartTime,
    .EndTime = EndTime,
    .Output = Output,
    .Note = Note,
    .UserID = AuthManager.User.Identity.ID
}

                    If isEdit Then
                        Await _dataServiceManager.ActivityDataService.Update(ID, act)

                        Result = PopupResult.OK
                    Else
                        Await _dataServiceManager.ActivityDataService.Create(act)

                        Result = PopupResult.OK
                    End If
                Catch ex As Exception
                    windowManager.ShowMessageBox(ex.Message, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error)
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
        Public Property Quantity As Integer
            Get
                Return GetProperty(Of Integer)()
            End Get
            Set(ByVal value As Integer)
                SetProperty(value)
            End Set
        End Property
        Public Property Metric As String
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
                If SetProperty(value) Then
                    StartTime = New DateTime(value.Year, value.Month, value.Day, StartTime.Hour, StartTime.Minute, 0)
                    EndTime = New DateTime(value.Year, value.Month, value.Day, EndTime.Hour, EndTime.Minute, 0)
                End If
            End Set
        End Property
        Public Property Duration As Integer
            Get
                Return GetProperty(Of Integer)()
            End Get
            Set(ByVal value As Integer)
                SetProperty(value)
            End Set
        End Property
        Public Property StartTime As Date
            Get
                Return GetProperty(Of Date)()
            End Get
            Set(ByVal value As Date)
                If SetProperty(value) Then
                    Duration = CInt((EndTime - StartTime).TotalMinutes)
                End If
            End Set
        End Property
        Public Property EndTime As Date
            Get
                Return GetProperty(Of Date)()
            End Get
            Set(ByVal value As Date)
                If SetProperty(value) Then
                    Duration = CInt((EndTime - StartTime).TotalMinutes)
                End If
            End Set
        End Property
        Public Property Output As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                SetProperty(value)
            End Set
        End Property
        Public Property Note As String
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
