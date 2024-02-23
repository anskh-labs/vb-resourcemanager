Namespace VBNetCore.Mvvm.Abstractions
    ' <summary>
    ''' This interface can be implemented by view models, which want to be notified when
    ''' the corresponding view is about to be closed.
    ''' </summary>
    Public Interface IOnClosingHandler
        ''' <summary>
        ''' This method is called when the corresponding view closes.
        ''' </summary>
        ''' <remarks>
        ''' When the corresponding view is a <seecref="Window"/>, this method is called when <seecref="Window.OnClosing"/>
        ''' is raised; otherwise, when <seecref="FrameworkElement.Unloaded"/> is raised.
        ''' </remarks>
        ''' <remarks>If you want to intercept when the corresponding window is about to be closed,
        ''' use <seecref="ICancelableOnClosingHandler"/>.</remarks>
        Sub OnClosing()
    End Interface
End Namespace
