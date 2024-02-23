Imports System.ComponentModel
Imports System.Linq.Expressions
Imports System.Runtime.CompilerServices
Imports System.Windows.Input
Imports VBNetCore.Mvvm.Helpers

Namespace VBNetCore.Mvvm.ViewModels
    ''' <summary>
    ''' Base class implementing <seecref="INotifyPropertyChanged"/>.
    ''' </summary>
    Public MustInherit Class PropertyChangedBase
        Implements INotifyPropertyChanged
        ''' <summary>
        ''' Occurs when a property value changes.
        ''' </summary>
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        ''' <summary>
        ''' Storage for fields
        ''' </summary>
        Protected _properties As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()

        ''' <summary>
        ''' Notifies clients that all properties may have changed.
        ''' </summary>
        ''' <remarks>
        ''' This method raises the <seecref="PropertyChanged"/> event with <seecref="String.Empty"/> as the property name.
        ''' </remarks>
        Protected Sub Refresh()
            Call CommandManager.InvalidateRequerySuggested()
            Me.OnPropertyChanged(String.Empty)
        End Sub

        ''' <summary>
        ''' Raises the <seecref="PropertyChanged"/> event.
        ''' </summary>
        ''' <paramname="propertyName">The name of the changed property.</param>
        Protected Overridable Sub OnPropertyChanged(ByVal Optional propertyName As String = Nothing)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

        ''' <summary>
        ''' Raises the <seecref="PropertyChanged"/> event.
        ''' </summary>
        ''' <paramname="property">The changed property.</param>
        ''' <typeparamname="TProperty">The type of the changed property.</typeparam>
        Protected Sub OnPropertyChanged(Of TProperty)(ByVal [property] As Expression(Of Func(Of TProperty)))
            Me.OnPropertyChanged(GetMemberName([property]))
        End Sub

        ''' <summary>
        ''' Set field value and Raises the <seecref="PropertyChanged"/> event.
        ''' </summary>
        ''' <typeparamname="TProperty"></typeparam>
        ''' <paramname="value"></param>
        ''' <paramname="propertyName"></param>
        ''' <returns></returns>
        Protected Overridable Function SetProperty(Of TProperty)(ByVal value As TProperty, <CallerMemberName> ByVal Optional propertyName As String = Nothing) As Boolean
            If EqualityComparer(Of TProperty).Default.Equals(GetProperty(Of TProperty)(propertyName), value) Then Return False
            If Not String.IsNullOrEmpty(propertyName) Then
                _properties(propertyName) = value
                OnPropertyChanged(propertyName)

                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Set field value and Raises the <seecref="PropertyChanged"/> event. 
        ''' </summary>
        ''' <typeparamname="TProperty"></typeparam>
        ''' <paramname="value"></param>
        ''' <paramname="property"></param>
        ''' <returns></returns>
        Protected Overridable Function SetProperty(Of TProperty)(ByVal value As TProperty, ByVal [property] As Expression(Of Func(Of TProperty))) As Boolean
            Return SetProperty(Of TProperty)(value, GetMemberName([property]))
        End Function

        ''' <summary>
        ''' Gets the value of a property
        ''' </summary>
        ''' <typeparamname="TProperty"></typeparam>
        ''' <paramname="propertyName"></param>
        ''' <returns></returns>
        Protected Overridable Function GetProperty(Of TProperty)(<CallerMemberName> ByVal Optional propertyName As String = Nothing) As TProperty
            If Not String.IsNullOrEmpty(propertyName) Then
                Dim value As Object = Nothing
                If _properties.TryGetValue(propertyName, value) Then Return If(value Is Nothing, Nothing, CType(value, TProperty))
            End If
            Return Nothing
        End Function

        ''' <summary>
        ''' Gets the value of a property
        ''' </summary>
        ''' <typeparamname="TProperty"></typeparam>
        ''' <paramname="property"></param>
        ''' <returns></returns>
        Protected Overridable Function GetProperty(Of TProperty)(ByVal [property] As Expression(Of Func(Of TProperty))) As TProperty
            Return GetProperty(Of TProperty)(GetMemberName([property]))
        End Function

    End Class
End Namespace
