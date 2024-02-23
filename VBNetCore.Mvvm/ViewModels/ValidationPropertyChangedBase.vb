Imports System.ComponentModel
Imports System.Linq.Expressions
Imports VBNetCore.Mvvm.Controls
Imports VBNetCore.Mvvm.Helpers

Namespace VBNetCore.Mvvm.ViewModels
    ''' <summary>
    ''' A base class for <seecref="PopupViewModel"/> providing validation.
    ''' </summary>
    Public MustInherit Class ValidationPropertyChangedBase
        Inherits PropertyChangedBase
        Implements INotifyDataErrorInfo

        Private ReadOnly _errors As New Dictionary(Of String, List(Of ValidationError))
        Private ReadOnly _validationRules As New Dictionary(Of String, List(Of ValidationRule))


        Private Class ValidationRule
            Public ValidationError As ValidationError = Nothing
            Public IsValid As Func(Of Boolean) = Nothing
        End Class

        ''' <summary>
        ''' Adds a rule for validation.
        ''' </summary>
        ''' <paramname="property">The property to validate.</param>
        ''' <paramname="validate">The function to validate the value of the property.</param>
        ''' <paramname="errorMsg">The error message if the validation fails.</param>
        ''' <paramname="errorlevel">The error level</param>
        ''' <typeparamname="TProperty">The type of the property to validate.</typeparam>
        Protected Sub AddValidationRule(Of TProperty)(ByVal [property] As Expression(Of Func(Of TProperty)), ByVal validate As Func(Of TProperty, Boolean), ByVal errorMsg As String, ByVal Optional errorlevel As ErrorLevel = ErrorLevel.Error)
            Dim propertyName = GetMemberName([property])

            Dim rule = New ValidationRule With {
                .ValidationError = New ValidationError(errorMsg, errorlevel),
                .IsValid = Function()
                               Dim val = [property].Compile()()
                               Return validate(val)
                           End Function
}

            Dim ruleSet As List(Of ValidationRule) = Nothing

            If Not Me._validationRules.TryGetValue(propertyName, ruleSet) Then
                ruleSet = New List(Of ValidationRule)()
                Me._validationRules.Add(propertyName, ruleSet)
            End If
            ruleSet.Add(rule)
        End Sub

        ''' <summary>
        ''' Gets the validation errors for a specified property or for the entire entity.
        ''' </summary>
        ''' <returns>
        ''' The validation errors for the property or entity.
        ''' </returns>
        ''' <paramname="propertyName">The name of the property to retrieve validation errors for;
        ''' or null or <seecref="F:System.String.Empty"/>, to retrieve entity-level errors.</param>
        Public Function GetErrors(ByVal propertyName As String) As IEnumerable Implements INotifyDataErrorInfo.GetErrors
            If String.IsNullOrEmpty(propertyName) Then
                propertyName = String.Empty
            End If
            Dim propertErrors As IEnumerable = Nothing

            If _errors.TryGetValue(propertyName, propertErrors) Then
                Return propertErrors
            End If
            Return Enumerable.Empty(Of ValidationError)()
        End Function

        ''' <summary>
        ''' Raises the <seecref="PropertyChangedBase.PropertyChanged"/> event.
        ''' </summary>
        ''' <paramname="propertyName">The name of the changed property.</param>
        Protected Overrides Sub OnPropertyChanged(ByVal Optional propertyName As String = Nothing)
            MyBase.OnPropertyChanged(propertyName)
            Dim ruleSet As List(Of ValidationRule) = Nothing

            If Equals(propertyName, Nothing) OrElse String.IsNullOrEmpty(propertyName) Then
                ValidateAllRules()
            Else

                If Me._validationRules.TryGetValue(propertyName, ruleSet) Then
                    For Each rule In ruleSet
                        If rule.IsValid() Then
                            RemoveError(propertyName, rule.ValidationError)
                        Else
                            AddError(propertyName, rule.ValidationError)
                        End If
                    Next
                End If
            End If
        End Sub

        ''' <summary>
        ''' Runs all validation rules.
        ''' </summary>
        ''' <returns><seelangword="true"/> if all rules are valid; otherwise, <seelangword="false"/>.</returns>
        Protected Overridable Function ValidateAllRules() As Boolean
            For Each ruleSet In Me._validationRules
                For Each rule In ruleSet.Value
                    If rule.IsValid() Then
                        RemoveError(ruleSet.Key, rule.ValidationError)
                    Else
                        AddError(ruleSet.Key, rule.ValidationError)
                    End If
                Next
            Next
            Return Not HasErrors
        End Function

        ''' <summary>
        ''' Gets a value that indicates whether the entity has validation errors.
        ''' </summary>
        ''' <returns>
        ''' true if the entity currently has validation errors; otherwise, false.
        ''' </returns>
        Public ReadOnly Property HasErrors As Boolean Implements INotifyDataErrorInfo.HasErrors
            Get
                Return Me._errors.Any()
            End Get
        End Property

        ''' <summary>
        ''' Occurs when the validation errors have changed for a property or for the entire entity.
        ''' </summary>
        Public Event ErrorsChanged As EventHandler(Of DataErrorsChangedEventArgs) Implements INotifyDataErrorInfo.ErrorsChanged

        ''' <summary>
        ''' Called when the validation state of a property has changed.
        ''' </summary>
        ''' <paramname="propertyName">The name of the validated property.</param>
        Protected Overridable Sub OnErrorsChanged(ByVal propertyName As String)
            RaiseEvent ErrorsChanged(Me, New DataErrorsChangedEventArgs(propertyName))
        End Sub

        ''' <summary>
        ''' Sets a property's error state.
        ''' </summary>
        ''' <paramname="isValid"><seelangword="true"/> if the property is valid; otherwise, <seelangword="false"/>.</param>
        ''' <paramname="property">The validated property.</param>
        ''' <paramname="validationError">The error message.</param>
        ''' <typeparamname="TProperty">The type of the property.</typeparam>
        Protected Sub Validate(Of TProperty)(ByVal isValid As Boolean, ByVal [property] As Expression(Of Func(Of TProperty)), ByVal validationError As ValidationError)
            Validate(isValid, GetMemberName([property]), validationError)
        End Sub

        ''' <summary>
        ''' Sets a property's error state.
        ''' </summary>
        ''' <paramname="isValid"><seelangword="true"/> if the property is valid; otherwise, <seelangword="false"/>.</param>
        ''' <paramname="propertyName">The name of the validated property.</param>
        ''' <paramname="validationError">The error message.</param>
        Protected Sub Validate(ByVal isValid As Boolean, ByVal propertyName As String, ByVal validationError As ValidationError)
            If isValid Then
                Me.RemoveError(propertyName, validationError)
            Else
                Me.AddError(propertyName, validationError)
            End If
        End Sub

        ''' <summary>
        ''' Adds a validation error message for a property.
        ''' </summary>
        ''' <paramname="property">The validated property.</param>
        ''' <paramname="validationError">The error message.</param>
        ''' <typeparamname="TProperty">The type of the validated property.</typeparam>
        Protected Sub AddError(Of TProperty)(ByVal [property] As Expression(Of Func(Of TProperty)), ByVal validationError As ValidationError)
            Me.AddError(GetMemberName([property]), validationError)
        End Sub

        ''' <summary>
        ''' Adds a validation error message for a property.
        ''' </summary>
        ''' <paramname="propertyName">The name of the validated property.</param>
        ''' <paramname="validationError">The error message.</param>
        Protected Sub AddError(ByVal propertyName As String, ByVal validationError As ValidationError)
            Dim safePropertyName = If(propertyName, String.Empty)
            Dim propertyErrors As List(Of ValidationError) = Nothing

            If Not Me._errors.TryGetValue(safePropertyName, propertyErrors) Then
                propertyErrors = New List(Of ValidationError)()
                Me._errors.Add(safePropertyName, propertyErrors)
            End If
            If Not propertyErrors.Contains(validationError) Then
                propertyErrors.Add(validationError)
                OnErrorsChanged(safePropertyName)
            End If
        End Sub

        ''' <summary>
        ''' Removes a validation error message for a property.
        ''' </summary>
        ''' <paramname="property">The validated property.</param>
        ''' <paramname="validationError">The error message.</param>
        ''' <typeparamname="TProperty">The type of the validated property.</typeparam>
        Protected Sub RemoveError(Of TProperty)(ByVal [property] As Expression(Of Func(Of TProperty)), ByVal validationError As ValidationError)
            Me.RemoveError(GetMemberName([property]), validationError)
        End Sub

        ''' <summary>
        ''' Removes a validation error message for a property.
        ''' </summary>
        ''' <paramname="propertyName">The name of the validated property.</param>
        ''' <paramname="validationError">The error message.</param>
        Protected Sub RemoveError(ByVal propertyName As String, ByVal validationError As ValidationError)
            Dim safePropertyName = If(propertyName, String.Empty)
            Dim propertyErrors As List(Of ValidationError) = Nothing

            If Me._errors.TryGetValue(safePropertyName, propertyErrors) Then
                If propertyErrors.Contains(validationError) Then
                    propertyErrors.Remove(validationError)
                    If Not propertyErrors.Any() Then
                        Me._errors.Remove(safePropertyName)
                    End If
                    OnErrorsChanged(safePropertyName)
                End If
            End If
        End Sub
    End Class
End Namespace
