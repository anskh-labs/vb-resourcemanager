Imports System.Windows.Input
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions
Imports VBNetCore.Mvvm.Commands
Imports VBNetCore.Mvvm.Controls
Imports VBNetCore.Validators

Namespace ResourceManager.ViewModels
    Public Class TagsPopupViewModel
        Inherits PopupViewModelBase
        Private ReadOnly _tagDataService As ITagDataService
        Public Sub New(ByVal tagDataService As ITagDataService)
            AddValidationRule(Function() Name, Function(x) TextValidator.Instance.Required(x), TextValidator.Instance.ErrorMessage("Required", "Tag Name"))

            _tagDataService = tagDataService

            OKCommand = New RelayCommand(AddressOf OnOk)
            AddCommand = New RelayCommand(AddressOf OnAdd, AddressOf CanAdd)
            RemoveCommand = New RelayCommand(Of Tag)(AddressOf OnRemove)
        End Sub

        Private Sub OnRemove(ByVal tag As Tag)
            Tags = Tags.Where(Function(x) Not Equals(x.Name, tag.Name)).ToList()
        End Sub
        Public Property Name As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                SetProperty(value)
            End Set
        End Property

        Public Property Tags As IList(Of Tag)
            Get
                Return GetProperty(Of IList(Of Tag))()
            End Get
            Set(ByVal value As IList(Of Tag))
                SetProperty(value)
            End Set
        End Property
        Private Sub OnOk()
            Result = PopupResult.OK
            OnClose()
        End Sub
        Private Async Sub OnAdd()
            ValidateAllRules()
            If Not HasErrors Then
                Try
                    Dim tags = Me.Tags
                    Dim tag = Await _tagDataService.SingleOrDefault(Function(x) x.Name.Equals(Name))
                    If tag Is Nothing Then
                        tag = Await _tagDataService.Create(New Tag With {.Name = Me.Name})
                    End If
                    If tags.SingleOrDefault(Function(x) x.Name.Equals(Name)) Is Nothing Then
                        tags.Add(tag)
                    End If
                    Me.Tags = tags.ToList()
                Catch e As Exception
                    windowManager.ShowMessageBox(e.Message, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error)
                End Try
            End If
        End Sub

        Private Function CanAdd() As Boolean
            Return Not HasErrors
        End Function

        Public ReadOnly Property OKCommand As ICommand
        Public ReadOnly Property AddCommand As ICommand
        Public ReadOnly Property RemoveCommand As ICommand
    End Class
End Namespace
