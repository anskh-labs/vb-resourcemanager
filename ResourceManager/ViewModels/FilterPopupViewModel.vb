Imports VBNetCore.Mvvm.Commands
Imports VBNetCore.Mvvm.Controls
Imports VBNetCore.Mvvm.ViewModels
Imports VBNetCore.Validators
Imports ResourceManager.Helpers
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Input

Namespace ResourceManager.ViewModels
    Public Class FilterPopupViewModel
        Inherits PopupViewModelBase
        Private _FilterParams As ResourceManager.Helpers.FilterParams
        Public Sub New()
            AddValidationRule(Function() SelectedColumn, Function(x) TextValidator.Instance.Required(x), TextValidator.Instance.ErrorMessage("Required", "Filter column"))
            AddValidationRule(Function() SelectedFilterOption, Function(x) [Enum].IsDefined(GetType(FilterOptions), x), TextValidator.Instance.ErrorMessage("Required", "Filter option"))
            AddValidationRule(Function() FilterValue, AddressOf ValidateFilterValue, TextValidator.Instance.ErrorMessage("Required", "Filter value"))

            FilterOptions = [Enum].GetValues(GetType(FilterOptions)).Cast(Of FilterOptions)()

            OKCommand = New RelayCommand(AddressOf OnOk, AddressOf CanOk)
        End Sub

        Private Function ValidateFilterValue(ByVal x As String) As Boolean
            If IsFilterValueEnabled Then
                Return TextValidator.Instance.Required(x)
            End If

            Return True
        End Function

        Public Property Columns As IEnumerable(Of String)
            Get
                Return GetProperty(Of IEnumerable(Of String))()
            End Get
            Set(ByVal value As IEnumerable(Of String))
                SetProperty(value)
            End Set
        End Property
        Public Property SelectedColumn As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                SetProperty(value)
            End Set
        End Property
        Public Property IsFilterValueEnabled As Boolean
            Get
                Return GetProperty(Of Boolean)()
            End Get
            Set(ByVal value As Boolean)
                SetProperty(value)
            End Set
        End Property
        Public Property FilterOptions As IEnumerable(Of FilterOptions)
            Get
                Return GetProperty(Of IEnumerable(Of FilterOptions))()
            End Get
            Set(ByVal value As IEnumerable(Of FilterOptions))
                SetProperty(value)
            End Set
        End Property
        Public Property SelectedFilterOption As FilterOptions
            Get
                Return GetProperty(Of FilterOptions)()
            End Get
            Set(ByVal value As FilterOptions)
                If SetProperty(value) Then
                    If value = Helpers.FilterOptions.IsEmpty OrElse value = Helpers.FilterOptions.IsNotEmpty Then
                        IsFilterValueEnabled = False
                    Else
                        IsFilterValueEnabled = True
                    End If
                    OnPropertyChanged(NameOf(FilterPopupViewModel.FilterValue))
                End If
            End Set
        End Property
        Public Property FilterValue As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                SetProperty(value)
            End Set
        End Property

        Public Property FilterParams As FilterParams
            Get
                Return _FilterParams
            End Get
            Private Set(ByVal value As FilterParams)
                _FilterParams = value
            End Set
        End Property
        Private Sub OnOk()
            ValidateAllRules()
            If Not HasErrors Then
                FilterParams = New FilterParams() With {
    .ColumnName = SelectedColumn,
    .FilterOption = SelectedFilterOption,
    .FilterValue = FilterValue
}
                Result = PopupResult.OK
                OnClose()
            End If
        End Sub

        Private Function CanOk() As Boolean
            Return Not HasErrors
        End Function

        Public ReadOnly Property OKCommand As ICommand
    End Class
End Namespace
