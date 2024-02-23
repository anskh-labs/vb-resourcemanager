Imports System.Windows

Namespace ResourceManager.Views
    ''' <summary>
    ''' Interaction logic for SplashScreenWindow.xaml
    ''' </summary>
    Public Partial Class SplashScreenWindow
        Inherits Window
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Public Property Message As String
            Get
                Return Me._message.Text
            End Get
            Set(ByVal value As String)
                Me._message.Text = value
            End Set

        End Property
        Public Property Progress As Double
            Get
                Return Me._progressBar.Value
            End Get
            Set(ByVal value As Double)
                Me._progressBar.Value = value
            End Set

        End Property
    End Class
End Namespace
