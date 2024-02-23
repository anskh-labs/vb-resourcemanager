Imports ResourceManager.Helpers
Imports System
Imports System.Globalization
Imports System.IO
Imports System.Windows.Data
Imports System.Windows.Media.Imaging

Namespace ResourceManager.Converters
    Friend Class StringToImageConverter
        Implements IValueConverter
        Public Function Convert(ByVal value As Object, ByVal TargetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim filename As String = value.ToString()
            If value IsNot Nothing AndAlso Not String.IsNullOrEmpty(filename) AndAlso File.Exists(filename) Then
                Dim memoryStream As MemoryStream = New MemoryStream(File.ReadAllBytes(filename))
                Dim imageSource = New BitmapImage()
                imageSource.BeginInit()
                imageSource.CacheOption = BitmapCacheOption.OnLoad
                imageSource.StreamSource = memoryStream
                imageSource.EndInit()

                Return imageSource
            Else
                Return AssetManager.Instance.GetImage("no_cover.png")
            End If
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
