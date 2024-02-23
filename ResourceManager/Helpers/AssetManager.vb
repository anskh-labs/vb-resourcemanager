Imports System
Imports System.Windows.Media.Imaging

Namespace ResourceManager.Helpers
    Friend Class AssetManager
        Private Shared ReadOnly _instance As AssetManager = New AssetManager()
        Private Sub New()
        End Sub
        Public Shared ReadOnly Property Instance As AssetManager
            Get
                Return _instance
            End Get
        End Property
        Public Function GetImage(ByVal resourceName As String) As BitmapImage
            Dim image = New BitmapImage()

            Dim resourceLocation = String.Format("pack://application:,,,/ResourceManager;component/Resources/Assets/Images/{0}", resourceName)

            Try
                image.BeginInit()
                image.CacheOption = BitmapCacheOption.OnLoad
                image.CreateOptions = BitmapCreateOptions.IgnoreImageCache
                image.UriSource = New Uri(resourceLocation)
                image.EndInit()
            Catch e As Exception
                Trace.WriteLine(e.ToString())
            End Try

            Return image
        End Function
    End Class
End Namespace
