Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Logging
Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Mvvm.Controls
Imports VBNetCore.Mvvm.Extensions
Imports System
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Controls
Imports System.ComponentModel
Imports System.Net.Sockets

Namespace VBNetCore.Mvvm.Helpers
    ''' <summary>
    ''' Provides methods to get a view instance for a given view model.
    ''' </summary>
    Public Class ViewLocator
        Private ReadOnly _serviceProvider As IServiceProvider
        Private ReadOnly _viewModelRegistry As ViewModelRegistry
        Private ReadOnly _logger As ILogger(Of ViewLocator)

        ''' <summary>
        ''' Creates a new <seecref="ViewLocator"/> instance.
        ''' </summary>
        ''' <paramname="serviceProvider">The service provider.</param>
        ''' <paramname="logger">The logger.</param>
        Public Sub New(ByVal serviceProvider As IServiceProvider, ByVal logger As ILogger(Of ViewLocator))
            _serviceProvider = serviceProvider
            _viewModelRegistry = serviceProvider.GetRequiredService(Of ViewModelRegistry)()
            _logger = logger
        End Sub

        ''' <summary>
        ''' Gets the view for the passed view model.
        ''' </summary>
        ''' <paramname="serviceProvider">The optional service provider. If <seelangname="null"/>, the instance passed in the
        ''' constructor will be used.</param>
        ''' <typeparamname="TViewModel"></typeparam>
        ''' <returns>The view matching the view model.</returns>
        ''' <exceptioncref="InvalidOperationException">Thrown when the view cannot be found in the IoC container.</exception>
        ''' <remarks>
        ''' <para>
        ''' If <typeparamrefname="TViewModel"/> implements <seecref="IOnLoadedHandler"/> or <seecref="IOnClosingHandler"/>,
        ''' this method will register the view's corresponding events and call <seecref="IOnLoadedHandler.OnLoadedAsync"/>
        ''' and <seecref="IOnClosingHandler.OnClosing"/> respectively when those events are raised.
        ''' </para>
        ''' <para>
        ''' Don't call <strong>InitializeComponent()</strong> in your view's constructor yourself! If your view contains
        ''' a method called InitializeComponent, this method will call it automatically via reflection.
        ''' This allows the user of the library to remove the code-behind of her/his XAML files.
        ''' </para>
        ''' </remarks>
        Public Function GetViewForViewModel(Of TViewModel As Class)(ByVal Optional serviceProvider As IServiceProvider = Nothing) As Object
            If serviceProvider Is Nothing Then serviceProvider = _serviceProvider
            Dim viewModel As TViewModel = serviceProvider.GetRequiredService(Of TViewModel)
            Return GetViewForViewModel(viewModel, serviceProvider)
        End Function

        ''' <summary>
        ''' Gets the view for the passed view model.
        ''' </summary>
        ''' <paramname="viewModel">The view model for which a view should be returned.</param>
        ''' <paramname="serviceProvider">The optional service provider. If <seelangname="null"/>, the instance passed in the
        ''' constructor will be used.</param>
        ''' <returns>The view matching the view model.</returns>
        ''' <exceptioncref="InvalidOperationException">Thrown when the view cannot be found in the IoC container.</exception>
        ''' <remarks>
        ''' <para>
        ''' If the <paramrefname="viewModel"/> implements <seecref="IOnLoadedHandler"/> or <seecref="IOnClosingHandler"/>,
        ''' this method will register the view's corresponding events and call <seecref="IOnLoadedHandler.OnLoadedAsync"/>
        ''' and <seecref="IOnClosingHandler.OnClosing"/> respectively when those events are raised.
        ''' </para>
        ''' <para>
        ''' Don't call <strong>InitializeComponent()</strong> in your view's constructor yourself! If your view contains
        ''' a method called InitializeComponent, this method will call it automatically via reflection.
        ''' This allows the user of the library to remove the code-behind of her/his XAML files.
        ''' </para>
        ''' </remarks>
        Public Function GetViewForViewModel(ByVal viewModel As Object, ByVal Optional serviceProvider As IServiceProvider = Nothing) As Object
            _logger.LogDebug($"View for view model {viewModel.GetType()} requested")
            Dim viewType As Type = Nothing

            If Not _viewModelRegistry.ViewModelTypeToViewType.TryGetValue(viewModel.GetType(), viewType) Then
                _logger.LogError($"Could not find view for view model type {viewModel.GetType()}")
                Throw New InvalidOperationException("No View found for ViewModel of type " & viewModel.GetType().ToString())
            End If

            Dim view = _serviceProvider.GetRequiredService(viewType)
            _logger.LogDebug($"Resolved to instance of {view.GetType()}")

            Dim dependencyObject As DependencyObject = TryCast(view, DependencyObject)
            If dependencyObject IsNot Nothing Then
                If serviceProvider Is Nothing Then serviceProvider = _serviceProvider
                SetServiceProvider(dependencyObject, serviceProvider)
            End If

            Dim frameworkElement As FrameworkElement = TryCast(view, FrameworkElement)

            If frameworkElement IsNot Nothing Then
                AttachHandler(frameworkElement, viewModel)
                frameworkElement.DataContext = viewModel
            End If

            InitializeComponent(view)

            Return view
        End Function

        Private Shared Sub AttachHandler(ByVal view As FrameworkElement, ByVal viewModel As Object)

            If TypeOf viewModel Is IOnLoadedHandler Then
                Dim onLoadedHandler = TryCast(viewModel, IOnLoadedHandler)
                Dim OnLoadEvent As RoutedEventHandler = Async Sub(sender As Object, args As RoutedEventArgs)
                                                            RemoveHandler view.Loaded, OnLoadEvent
                                                            Await onLoadedHandler.OnLoadedAsync
                                                        End Sub
                AddHandler view.Loaded, OnLoadEvent
            End If

            If TypeOf viewModel Is IOnClosingHandler Then
                Dim OnCLosingHandler = TryCast(viewModel, IOnClosingHandler)
                If TypeOf view Is Window Then
                    Dim window = TryCast(view, Window)
                    Dim OnClosingEvent As [Delegate] = Sub(sender As Object, args As CancelEventArgs)
                                                           RemoveHandler window.Closing, OnClosingEvent
                                                           OnCLosingHandler.OnClosing()
                                                       End Sub
                    AddHandler window.Closing, OnClosingEvent
                Else
                    Dim OnUnloadedEvent As [Delegate] = Sub(sender As Object, args As RoutedEventArgs)
                                                            RemoveHandler view.Unloaded, OnUnloadedEvent
                                                            OnCLosingHandler.OnClosing()
                                                        End Sub
                    AddHandler view.Unloaded, OnUnloadedEvent
                End If
            End If

            If TypeOf viewModel Is ICancelableOnClosingHandler Then
                Dim CancelableOnClosingHandlersingHandler = TryCast(viewModel, ICancelableOnClosingHandler)
                If TypeOf view IsNot Window Then
                    Throw New ArgumentException("If a view model implements ICancelableOnClosingHandler, the corresponding view must be a window.")
                Else
                    Dim window = TryCast(view, Window)
                    Dim OnClosingEvent As [Delegate] = Sub(sender As Object, args As CancelEventArgs)
                                                           args.Cancel = CancelableOnClosingHandlersingHandler.OnClosing
                                                       End Sub
                    AddHandler window.Closing, OnClosingEvent
                    Dim OnClosedEvent As [Delegate] = Sub(sender As Object, args As CancelEventArgs)
                                                          RemoveHandler window.Closing, OnClosingEvent
                                                          RemoveHandler window.Closed, OnClosedEvent
                                                      End Sub
                    AddHandler window.Closed, OnClosedEvent
                End If
            End If
            If TypeOf viewModel Is IPopupable Then
                Dim viewControl = TryCast(view, ContentControl)
                viewControl.Content = New PopupControl With {.Content = viewControl.Content}
            End If

        End Sub

        Private Shared Sub InitializeComponent(ByVal element As Object)
            Dim method = element.GetType().GetMethod("InitializeComponent", BindingFlags.Instance Or BindingFlags.Public)
            method.Invoke(element, Nothing)
        End Sub

    End Class
End Namespace
