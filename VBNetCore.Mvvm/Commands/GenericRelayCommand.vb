Imports System
Imports System.Windows.Input

Namespace VBNetCore.Mvvm.Commands
    ''' <summary>
    ''' A generic command whose sole purpose is to relay its functionality to other
    ''' objects by invoking delegates. The default return value for the CanExecute
    ''' method is 'true'. This class allows you to accept command parameters in the
    ''' Execute and CanExecute callback methods.
    ''' </summary>
    ''' <typeparamname="T">The type of the command parameter.</typeparam>
    Public Class RelayCommand(Of T)
        Implements ICommand
        Private ReadOnly _execute As Action(Of T)
        Private ReadOnly _canExecute As Predicate(Of T)

        ''' <summary>
        ''' Initializes a new instance of the RelayCommand class.
        ''' </summary>
        ''' <paramname="execute">The execution logic.</param>
        ''' <paramname="canExecute">The execution status logic.</param>
        ''' <exceptioncref="ArgumentNullException">If the execute argument is null.</exception>
        Public Sub New(ByVal execute As Action(Of T), ByVal canExecute As Predicate(Of T))
            If execute Is Nothing Then Throw New ArgumentNullException(NameOf(execute))
            _execute = execute
            _canExecute = canExecute
        End Sub
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <paramname="execute"></param>
        Public Sub New(ByVal execute As Action(Of T))
            Me.New(execute, Nothing)
        End Sub

        ''' <summary>
        ''' Occurs when changes occur that affect whether the command should execute.
        ''' </summary>
        Public Custom Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged
            AddHandler(ByVal value As EventHandler)
                If _canExecute IsNot Nothing Then
                    AddHandler CommandManager.RequerySuggested, value
                End If
            End AddHandler

            RemoveHandler(ByVal value As EventHandler)
                If _canExecute IsNot Nothing Then
                    RemoveHandler CommandManager.RequerySuggested, value
                End If
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
            End RaiseEvent
        End Event

        ''' <summary>
        ''' Defines the method that determines whether the command can execute in its current state.
        ''' </summary>
        ''' <paramname="parameter">Data used by the command. If the command does not require data 
        ''' to be passed, this object can be set to a null reference</param>
        ''' <returns>true if this command can be executed; otherwise, false.</returns>
        Public Function CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
            Return _canExecute Is Nothing OrElse _canExecute(parameter)
        End Function

        ''' <summary>
        ''' Defines the method to be called when the command is invoked. 
        ''' </summary>
        ''' <paramname="parameter">Data used by the command. If the command does not require data 
        ''' to be passed, this object can be set to a null reference</param>
        Public Sub Execute(ByVal parameter As Object) Implements ICommand.Execute
            If CanExecute(parameter) Then
                _execute(CType(parameter, T))
            End If
        End Sub

    End Class

End Namespace
