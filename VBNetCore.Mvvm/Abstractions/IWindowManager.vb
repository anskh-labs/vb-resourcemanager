Imports System.Windows
Imports Microsoft.Extensions.DependencyInjection

Namespace VBNetCore.Mvvm.Abstractions
    ''' <summary>
    ''' Declares methods to show windows.
    ''' </summary>
    Public Interface IWindowManager
        ''' <summary>
        ''' Shows a window for a given view model type.
        ''' </summary>
        ''' <paramname="owningWindow">An optional owner for the new window.</param>
        ''' <paramname="scope">Optional IoC scope for the window.</param>
        ''' <typeparamname="TViewModel">The type of the view model.</typeparam>
        ''' <returns>The window.</returns>
        Function ShowWindow(Of TViewModel As Class)(ByVal Optional owningWindow As Window = Nothing, ByVal Optional scope As IServiceScope = Nothing) As Window

        ''' <summary>
        ''' Shows a window for a given view model object.
        ''' </summary>
        ''' <paramname="viewModel">The view model for the window to be displayed.</param>
        ''' <paramname="owningWindow">An optional owner for the new window.</param>
        ''' <paramname="scope">Optional IoC scope for the window.</param>
        ''' <returns>The window.</returns>
        Function ShowWindow(ByVal viewModel As Object, ByVal Optional owningWindow As Window = Nothing, ByVal Optional scope As IServiceScope = Nothing) As Window

        ''' <summary>
        ''' Shows a window for a given view model type as a Dialog.
        ''' </summary>
        ''' <paramname="owningWindow">An optional owner for the new window.</param>
        ''' <paramname="scope">Optional IoC scope for the window.</param>
        ''' <typeparamname="TViewModel">The type of the view model.</typeparam>
        ''' <returns>A tuple composed of the result of <seecref="Window.ShowDialog()">ShowDialog</see> and the view model.</returns>
        Function ShowDialog(Of TViewModel As Class)(ByVal Optional owningWindow As Window = Nothing, ByVal Optional scope As IServiceScope = Nothing) As (Boolean, TViewModel)

        ''' <summary>
        ''' Displays a message box.
        ''' </summary>
        ''' <paramname="messageBoxText">A <seecref="String"/> that specifies the text to display.</param>
        ''' <paramname="button">A <seecref="MessageBoxButton"/> value that specifies which button or buttons to display</param>
        ''' <paramname="icon">A <seecref="MessageBoxImage"/> value that specifies the icon to display.</param>
        ''' <returns>A <seecref="MessageBoxResult"/> value that specifies which message box button is clicked by the user.</returns>
        Function ShowMessageBox(ByVal messageBoxText As String, ByVal Optional button As MessageBoxButton = MessageBoxButton.OK, ByVal Optional icon As MessageBoxImage = MessageBoxImage.None) As MessageBoxResult

        ''' <summary>
        ''' Shuts down an application that returns the specified exit code to the operating system.
        ''' </summary>
        ''' <paramname="exitCode">An integer exit code for an application. The default exit code is 0.</param>
        Sub ShutdownApplication(ByVal Optional exitCode As Integer = 0)
    End Interface
End Namespace
