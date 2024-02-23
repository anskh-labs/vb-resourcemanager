Namespace VBNetCore.Mvvm.Abstractions
    ''' <summary>
    ''' This interface can be implemented by view models, which want to be notified when
    ''' the corresponding window is about to be closed.
    ''' </summary>
    Public Interface ICancelableOnClosingHandler
        ''' <summary>
        ''' This method is called when the corresponding view's <seecref="Window.Closing"/> event was raised.
        ''' </summary>
        ''' <returns><seelandword="true"/> the the window can be closed; otherwise, <seelangword="false"/>.</returns>
        Function OnClosing() As Boolean
    End Interface
End Namespace
