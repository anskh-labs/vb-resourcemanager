Imports Microsoft.Extensions.DependencyInjection
Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Mvvm.Controls
Imports VBNetCore.Mvvm.Helpers
Imports VBNetCore.Mvvm.ViewModels
Imports System.Linq
Imports System.Windows
Imports System.Runtime.CompilerServices

Namespace VBNetCore.Mvvm.Extensions
    ''' <summary>
    ''' IServiceCollection extension methods for common scenarios.
    ''' </summary>
    Public Module ServiceCollectionExtension
        ''' <summary>
        ''' Adds required services to the given service collection.
        ''' </summary>
        ''' <paramname="services">The service collection.</param>
        ''' <returns>A reference to this instance after the operation has completed.</returns>
        <Extension()>
        Public Function AddMvvm(ByVal services As IServiceCollection) As IServiceCollection
            services.AddSingleton(Of ViewLocator)()
            services.AddSingleton(Of IWindowManager, WindowManager)()
            services.AddSingleton(Of IPopupManager, PopupManager)()
            services.AddSingleton(Of IUiExecution, UiExecution)()
            services.AddMvvmSingleton(Of PopupMessageViewModel, PopupMessageView)()
            Return services
        End Function

        ''' <summary>
        ''' Adds a transient viewmodel and its corresponding view to the service collection.
        ''' </summary>
        ''' <paramname="services">The <seecref="IServiceCollection"/> to add the types to.</param>
        ''' <typeparamname="TViewModel">The type of the viewmodel to add.</typeparam>
        ''' <typeparamname="TView">The type of the view to add.</typeparam>
        ''' <returns>A reference to this instance after the operation has completed.</returns>
        <Extension()>
        Public Function AddMvvmTransient(Of TViewModel As Class, TView As FrameworkElement)(ByVal services As IServiceCollection) As IServiceCollection
            services.AddTransient(Of TViewModel)().AddTransient(Of TView)()
            services.GetViewModelRegistry().ViewModelTypeToViewType.Add(GetType(TViewModel), GetType(TView))

            Return services
        End Function

        ''' <summary>
        ''' Adds a singleton viewmodel and its corresponding view to the service collection.
        ''' </summary>
        ''' <paramname="services">The <seecref="IServiceCollection"/> to add the types to.</param>
        ''' <typeparamname="TViewModel">The type of the viewmodel to add.</typeparam>
        ''' <typeparamname="TView">The type of the view to add.</typeparam>
        ''' <returns>A reference to this instance after the operation has completed.</returns>
        <Extension()>
        Public Function AddMvvmSingleton(Of TViewModel As Class, TView As FrameworkElement)(ByVal services As IServiceCollection) As IServiceCollection
            services.AddSingleton(Of TViewModel)().AddTransient(Of TView)()
            services.GetViewModelRegistry().ViewModelTypeToViewType.Add(GetType(TViewModel), GetType(TView))

            Return services
        End Function

        <Extension()>
        Private Function GetViewModelRegistry(ByVal services As IServiceCollection) As ViewModelRegistry
            Dim registry As ViewModelRegistry
            Dim descriptor = services.FirstOrDefault(Function(x) x.ServiceType = GetType(ViewModelRegistry))
            If descriptor IsNot Nothing Then
                registry = CType(descriptor.ImplementationInstance, ViewModelRegistry)
            Else
                registry = New ViewModelRegistry()
                services.AddSingleton(registry)
            End If

            Return registry
        End Function
    End Module
End Namespace
