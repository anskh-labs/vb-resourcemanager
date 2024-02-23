Imports VBNetCore.Mvvm.Abstractions
Imports System
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Threading

Namespace VBNetCore.Mvvm.Helpers
    Public Class UiExecution
        Implements IUiExecution
        Public Sub Execute(ByVal action As Action, ByVal Optional priority As DispatcherPriority = DispatcherPriority.DataBind) Implements IUiExecution.Execute
            Dim dispatcher = If(Application.Current.Dispatcher, Threading.Dispatcher.CurrentDispatcher)
            dispatcher.Invoke(action, priority)
        End Sub

        Public Function ExecuteAsync(ByVal action As Action, ByVal Optional priority As DispatcherPriority = DispatcherPriority.DataBind) As Task Implements IUiExecution.ExecuteAsync
            Dim dispatcher = If(Application.Current.Dispatcher, Threading.Dispatcher.CurrentDispatcher)
            Return dispatcher.InvokeAsync(action, priority).Task
        End Function
    End Class
End Namespace
