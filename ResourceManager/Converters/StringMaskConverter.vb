Imports System
Imports System.Globalization
Imports System.Windows.Data

Namespace ResourceManager.Converters
    Friend Class StringMaskConverter
        Implements IValueConverter
        Public Function Convert(ByVal value As Object, ByVal TargetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim val = String.Empty
            If value IsNot Nothing Then
                val = New String("*"c, value.ToString().Length)
            End If

            Return val
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
