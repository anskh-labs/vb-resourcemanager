Imports Microsoft.Extensions.DependencyInjection
Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Mvvm.Extensions
Imports System
Imports System.Windows

Namespace VBNetCore.Mvvm.Helpers
    Public Class WindowManager
        Implements IWindowManager
        Private ReadOnly _serviceProvider As IServiceProvider
        Private ReadOnly _viewLocator As ViewLocator

        Public Sub New(ByVal serviceProvider As IServiceProvider, ByVal viewLocator As ViewLocator)
            _serviceProvider = serviceProvider
            _viewLocator = viewLocator
        End Sub

        Public Function ShowWindow(Of TViewModel As Class)(ByVal Optional owningWindow As Window = Nothing, ByVal Optional scope As IServiceScope = Nothing) As Window Implements IWindowManager.ShowWindow
            Dim serviceProvider = If(scope.ServiceProvider, If(If(owningWindow IsNot Nothing, ServiceProviderPropertyExtension.GetServiceProvider(owningWindow), Nothing), _serviceProvider))

            Dim window = CType(_viewLocator.GetViewForViewModel(Of TViewModel)(serviceProvider), Window)
            window.Owner = owningWindow
            window.Show()
            Return window
        End Function

        Public Function ShowWindow(ByVal viewModel As Object, ByVal Optional owningWindow As Window = Nothing, ByVal Optional scope As IServiceScope = Nothing) As Window Implements IWindowManager.ShowWindow
            Dim serviceProvider = If(scope.ServiceProvider, If(If(owningWindow IsNot Nothing, ServiceProviderPropertyExtension.GetServiceProvider(owningWindow), Nothing), _serviceProvider))

            Dim window = CType(_viewLocator.GetViewForViewModel(viewModel, serviceProvider), Window)
            window.Owner = owningWindow
            window.Show()
            Return window
        End Function

        Public Function ShowDialog(Of TViewModel As Class)(ByVal Optional owningWindow As Window = Nothing, ByVal Optional scope As IServiceScope = Nothing) As (Boolean, TViewModel) Implements IWindowManager.ShowDialog
            Dim serviceProvider = If(scope.ServiceProvider, If(If(owningWindow IsNot Nothing, ServiceProviderPropertyExtension.GetServiceProvider(owningWindow), Nothing), _serviceProvider))

            Dim viewModel = serviceProvider.GetRequiredService(Of TViewModel)()

            Dim window = CType(_viewLocator.GetViewForViewModel(viewModel, serviceProvider), Window)
            window.Owner = owningWindow
            Dim result = window.ShowDialog()
            Return (result, viewModel)
        End Function

        Public Function ShowMessageBox(ByVal messageBoxText As String, ByVal Optional button As MessageBoxButton = MessageBoxButton.OK, ByVal Optional icon As MessageBoxImage = MessageBoxImage.None) As MessageBoxResult Implements IWindowManager.ShowMessageBox
            Dim title = Application.Current.MainWindow.Title
            Return MessageBox.Show(messageBoxText, title, button, icon)
        End Function

        Public Sub ShutdownApplication(ByVal Optional exitCode As Integer = 0) Implements IWindowManager.ShutdownApplication
            If Application.Current Is Nothing Then Throw New InvalidOperationException("There's no application to shut down.")

            Application.Current.Shutdown(exitCode)
        End Sub
    End Class
End Namespace
