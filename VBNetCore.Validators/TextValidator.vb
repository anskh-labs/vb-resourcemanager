Imports System.Collections.Generic

Namespace VBNetCore.Validators
    Public Class TextValidator
        Private ReadOnly _errorMessages As Dictionary(Of String, String) = New Dictionary(Of String, String)()
        Private Shared ReadOnly _instance As TextValidator = New TextValidator()
        Private Sub New()
            _errorMessages.Add("Required", "{0} Can not be empty.")
        End Sub
        Public Shared ReadOnly Property Instance As TextValidator
            Get
                Return _instance
            End Get
        End Property
        Public Function Required(ByVal Text As String) As Boolean
            If String.IsNullOrEmpty(Text) OrElse String.IsNullOrWhiteSpace(Text) OrElse Equals(Text, String.Empty) OrElse Equals(Text, " ") Then
                Return False
            Else
                Return True
            End If
        End Function
        Public Function ErrorMessage(ByVal ruleName As String, ByVal propertyName As String) As String
            Return If(_errorMessages.ContainsKey(ruleName), String.Format(_errorMessages(ruleName), propertyName), String.Empty)
        End Function
    End Class
End Namespace
