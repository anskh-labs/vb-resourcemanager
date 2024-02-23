Imports System
Imports System.Linq.Expressions
Imports System.Reflection

Namespace VBNetCore.Mvvm.Helpers
    ''' <summary>
    ''' Extension methods for <seecref="Expression"/>s.
    ''' </summary>
    Public Module ExpressionHelper
        ''' <summary>
        ''' Gets the name of a class' property.
        ''' </summary>
        ''' <paramname="property">The property.</param>
        ''' <typeparamname="TProperty">The type of the property.</typeparam>
        ''' <returns>The name of the property.</returns>
        Public Function GetMemberName(Of TProperty)(ByVal [property] As Expression(Of Func(Of TProperty))) As String
            Return GetMemberInfo([property]).Name
        End Function

        ''' <summary>
        ''' Gets the name of a class' property.
        ''' </summary>
        ''' <paramname="property">The property.</param>
        ''' <typeparamname="TEntity">The type of the class owning the property.</typeparam>
        ''' <typeparamname="TProperty">The type of the property.</typeparam>
        ''' <returns>The name of the property.</returns>
        Public Function GetMemberName(Of TEntity, TProperty)(ByVal [property] As Expression(Of Func(Of TEntity, TProperty))) As String
            Return GetMemberInfo([property]).Name
        End Function

        Private Function GetMemberInfo(ByVal expression As Expression) As MemberInfo
            Dim lambdaExpression = CType(expression, LambdaExpression)
            Dim unaryExpression As UnaryExpression = TryCast(lambdaExpression.Body, UnaryExpression)
            If unaryExpression IsNot Nothing Then
                Return CType(unaryExpression.Operand, MemberExpression).Member
            Else
                Return CType(lambdaExpression.Body, MemberExpression).Member
            End If
        End Function

    End Module
End Namespace
