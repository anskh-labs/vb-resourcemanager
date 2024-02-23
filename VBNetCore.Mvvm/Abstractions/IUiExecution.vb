Imports System
Imports System.Threading.Tasks
Imports System.Windows.Threading

Namespace VBNetCore.Mvvm.Abstractions
    ''' <summary>
    ''' Provides an method to execute an action in the dispatcher thread.
    ''' </summary>
    Public Interface IUiExecution
        ''' <summary>
        ''' Executes the passed action in the dispatcher thread.
        ''' </summary>
        ''' <paramname="action">The action to execute.</param>
        ''' <paramname="priority">Dispatcher priority</param>
        Sub Execute(ByVal action As Action, ByVal Optional priority As DispatcherPriority = DispatcherPriority.DataBind)

        ''' <summary>
        ''' Executes the passed action in the dispatcher thread asynchronously.
        ''' </summary>
        ''' <paramname="action">The action to execute.</param>
        ''' /// <paramname="priority">Dispatcher priority</param>
        Function ExecuteAsync(ByVal action As Action, ByVal Optional priority As DispatcherPriority = DispatcherPriority.DataBind) As Task
    End Interface
End Namespace
