Imports System.Threading.Tasks

Namespace VBNetCore.Mvvm.Abstractions
    ''' <summary>
    ''' This interface can be implemented by view models, which want to be notified when
    ''' the corresponding view was loaded.
    ''' </summary>
    Public Interface IOnLoadedHandler
        ''' <summary>
        ''' This method is called when the corresponding view's <seecref="Window.Closing"/> or
        ''' <seecref="FrameworkElement.Unloaded"/> event was raised.
        ''' </summary>
        ''' <returns></returns>
        Function OnLoadedAsync() As Task
    End Interface
End Namespace
