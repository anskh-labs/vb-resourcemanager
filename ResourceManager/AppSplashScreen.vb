Imports Microsoft.Extensions.DependencyInjection
Imports ResourceManager.Settings
Imports ResourceManager.Views
Imports VBNetCore.Mvvm.Abstractions

Namespace ResourceManager
    Public Class AppSplashScreen
        Implements IDisposable
        Private productName As String = String.Empty
        Private productVersion As String = String.Empty
        Private _SplashScreenWindow As SplashScreenWindow = Nothing
        ''' 
        ''' 
        Private ReadOnly Property UiExecution As IUiExecution
            Get
                Return Application.ServiceProvider.GetRequiredService(Of IUiExecution)()
            End Get
        End Property
        Public Sub New(ByVal settings As AppSettings)
            productName = settings.AppName
            productVersion = settings.AppVersion
        End Sub
        Public Property IsVisible As Boolean
            Get
                Return Me._SplashScreenWindow IsNot Nothing
            End Get
            Set(ByVal value As Boolean)
                If value Then
                    InitializeSplashScreen()
                Else
                    DisposeSplashScreen()
                End If
            End Set
        End Property

        Public Property Message As String
            Get
                Return _SplashScreenWindow.Message
            End Get
            Set(ByVal value As String)
                SetSplashScreenMessage(If(value, String.Empty))
            End Set
        End Property
        Public Property Progress As Double
            Get
                Return Me._SplashScreenWindow.Progress
            End Get
            Set(ByVal value As Double)
                If value < 0 Then value = 0.0
                SetSplashScreenProgress(value)
            End Set
        End Property

        Private Sub SetSplashScreenProgress(ByVal progress As Double)
            UiExecution.Execute(Sub() Me._SplashScreenWindow.Progress = progress)
        End Sub

        Private Sub InitializeSplashScreen()

            UiExecution.Execute(Sub()
                                    Me._SplashScreenWindow = New SplashScreenWindow()
                                    Me._SplashScreenWindow._productName.Text = productName
                                    Me._SplashScreenWindow._productVersion.Text = productVersion
                                    Me._SplashScreenWindow._message.Text = "Loading.."
                                    Me._SplashScreenWindow.Show()
                                End Sub)
        End Sub

        Private Sub DisposeSplashScreen()
            UiExecution.Execute(Sub()
                                    If Me._SplashScreenWindow Is Nothing Then
                                        Return
                                    End If

                                    Me._SplashScreenWindow.Close()
                                    Me._SplashScreenWindow = Nothing
                                End Sub)
        End Sub
        Private Sub SetSplashScreenMessage(ByVal message As String)
            UiExecution.Execute(Sub() Me._SplashScreenWindow.Message = message)
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            DisposeSplashScreen()
        End Sub
    End Class
End Namespace
