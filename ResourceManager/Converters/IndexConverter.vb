Imports System
Imports System.Globalization
Imports System.Windows.Controls
Imports System.Windows.Data

Namespace ResourceManager.Converters
    Friend Class IndexConverter
        Implements IValueConverter
        Public Function Convert(ByVal value As Object, ByVal TargetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim item = CType(value, ListViewItem)
            Dim listView As ListView = TryCast(ItemsControl.ItemsControlFromItemContainer(item), ListView)
            Dim index = listView.ItemContainerGenerator.IndexFromContainer(item) + 1
            Return index.ToString()
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
