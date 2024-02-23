Imports Microsoft.EntityFrameworkCore.Design
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Hosting
Imports Microsoft.Extensions.Options
Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Mvvm.Helpers
Imports VBNetCore.Security
Imports ResourceManager.Configuration
Imports ResourceManager.Helpers
Imports ResourceManager.Models
Imports ResourceManager.Settings
Imports ResourceManager.ViewModels
Imports ResourceManager.Views
Imports System
Imports System.IO
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows
Imports Microsoft.EntityFrameworkCore
Imports System.ComponentModel

Namespace ResourceManager
    ''' <summary>
    ''' Interaction logic for Application.xaml
    ''' </summary>
    Public Class Application
        Inherits System.Windows.Application

        Private _TrayIcon As AppTrayIcon
        Private ReadOnly host As IHost
        Private WithEvents _MainWindow As MainView
        Private ReadOnly Property ContextFactory As IDesignTimeDbContextFactory(Of ResourceManagerDBContext)
            Get
                Return ServiceProvider.GetRequiredService(Of IDesignTimeDbContextFactory(Of ResourceManagerDBContext))()
            End Get
        End Property
        Private ReadOnly Property SystemIntegrationSettings As SystemIntegrationSettings
            Get
                Return ServiceProviderServiceExtensions.GetRequiredService(Of IOptionsMonitor(Of SystemIntegrationSettings))(ServiceProvider).CurrentValue
            End Get
        End Property
        Private ReadOnly Property UiExecution As IUiExecution
            Get
                Return ServiceProvider.GetRequiredService(Of IUiExecution)()
            End Get
        End Property
        Private SplashScreen As AppSplashScreen
        Public Shared ReadOnly Property Settings As AppSettings
            Get
                Return ServiceProviderServiceExtensions.GetRequiredService(Of IOptionsMonitor(Of AppSettings))(ServiceProvider).CurrentValue
            End Get
        End Property

        Public Property TrayIcon As AppTrayIcon
            Get
                Return _TrayIcon
            End Get
            Private Set(ByVal value As AppTrayIcon)
                _TrayIcon = value
            End Set
        End Property

        Public Shared Property ServiceProvider As IServiceProvider

        Public Sub New()
            host = HostFactory.Create()
            ServiceProvider = host.Services
        End Sub
        Private Async Function StartApplication() As Task
            InitializeSpashScreen(True)

            Await host.StartAsync()

            ' background process
            Await Task.Factory.StartNew(Sub()
                                            SplashScreen.Message = "Init database.."
                                            Dim i As Double = 0

                                            While i <= 40
                                                SplashScreen.Progress = i
                                                Thread.Sleep(20)
                                                i = i + 5
                                            End While
                                        End Sub)

            InitializeDatabase()

            Await Task.Factory.StartNew(Sub()
                                            SplashScreen.Message = "Init configuration.."
                                            Dim i As Double = 40

                                            While i <= 80
                                                SplashScreen.Progress = i
                                                Thread.Sleep(30)
                                                i = i + 5
                                            End While
                                        End Sub)

            InitializePrincipal()
            InitializeConfiguration()

            Await Task.Factory.StartNew(Sub()
                                            SplashScreen.Message = "Starting Application.."
                                            Dim i As Double = 80

                                            While i <= 100
                                                SplashScreen.Progress = i
                                                Thread.Sleep(30)
                                                i = i + 5
                                            End While
                                        End Sub)

            InitializeMainWindow()
            InitializeTrayIcon()

            SplashScreen.IsVisible = False

            If SystemIntegrationSettings.ShowTrayIcon Then
                TrayIcon.IsVisible = True
            End If

            If Not SystemIntegrationSettings.ShowTrayIcon OrElse Not SystemIntegrationSettings.MinimizeToTrayOnStartup Then
                ShowMainWindow()
            End If
        End Function

        Private Sub InitializeConfiguration()
            If Not Directory.Exists(Settings.EbookSettings.FolderPath) Then Directory.CreateDirectory(Settings.EbookSettings.FolderPath)
            If Not Directory.Exists(Settings.EbookSettings.CoverPath) Then Directory.CreateDirectory(Settings.EbookSettings.CoverPath)
            If Not Directory.Exists(Settings.ArticleSettings.FolderPath) Then Directory.CreateDirectory(Settings.ArticleSettings.FolderPath)
            If Not Directory.Exists(Settings.RepositorySettings.FolderPath) Then Directory.CreateDirectory(Settings.RepositorySettings.FolderPath)
        End Sub

        Private Sub InitializePrincipal()
            'ensure set UserPrincipal
            AuthManager.SetCurrentPrincipal()
        End Sub

        'ensure db created and migrated
        Private Sub InitializeDatabase()
            Dim context = ContextFactory.CreateDbContext(Nothing)
            Using context
                context.Database.EnsureCreated()
                context.Database.Migrate()
            End Using
        End Sub
        Private Sub InitializeSpashScreen(ByVal isSplashScreenVisible As Boolean)
            SplashScreen = New AppSplashScreen(Settings)
            SplashScreen.IsVisible = isSplashScreenVisible
        End Sub
        Private Sub InitializeMainWindow()
            UiExecution.Execute(Sub()
                                    Dim _viewLocator = ServiceProvider.GetRequiredService(Of ViewLocator)()
                                    _MainWindow = CType(_viewLocator.GetViewForViewModel(Of MainViewModel)(ServiceProvider), MainView)
                                    AddHandler _MainWindow.Closing, AddressOf MainWindowClosingEventHandler
                                    MyBase.MainWindow = _MainWindow
                                End Sub)
        End Sub
        Private Sub InitializeTrayIcon()
            TrayIcon = New AppTrayIcon()
        End Sub
        Private Sub ShowMainWindow()
            UiExecution.Execute(Sub()
                                    If MyBase.MainWindow.IsVisible Then
                                        If _MainWindow.WindowState = WindowState.Minimized Then
                                            _MainWindow.WindowState = WindowState.Normal
                                        End If

                                        MyBase.MainWindow.Activate()
                                    Else
                                        MyBase.MainWindow.Show()
                                    End If
                                End Sub)
        End Sub
        Private Sub MainWindowClosingEventHandler(ByVal sender As Object, ByVal e As ComponentModel.CancelEventArgs)
            If TrayIcon.IsVisible Then
                e.Cancel = True
                Dim mainVM = TryCast(MainWindow.DataContext, MainViewModel)
                If mainVM IsNot Nothing Then
                    AuthManager.User.Identity = New AnonymousIdentity()
                    mainVM.RefreshUI()
                End If
                UiExecution.Execute(AddressOf MyBase.MainWindow.Hide)
            End If
        End Sub
        Protected Overrides Async Sub OnStartup(ByVal e As StartupEventArgs)
            ' To prevent opening of a second app instance
            If Not CreateMutex() Then
                Call Current.Shutdown()
                Return
            End If

            MyBase.OnStartup(e)

            Await StartApplication()
        End Sub
        Protected Overrides Async Sub OnExit(ByVal e As ExitEventArgs)
            Await host.StopAsync(TimeSpan.FromSeconds(5))

            host.Dispose()
        End Sub
    End Class
End Namespace
