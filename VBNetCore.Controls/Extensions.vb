Imports System.Windows
Imports System.Windows.Media

Namespace VBNetCore.Controls
    Public Module Extensions
        ''' <summary>
        ''' Finds a Child of a given item in the visual tree. 
        ''' </summary>
        ''' <paramname="parent">A direct parent of the queried item.</param>
        ''' <typeparamname="T">The type of the queried item.</typeparam>
        ''' <paramname="childName">x:Name or Name of child. </param>
        ''' <returns>The first parent item that matches the submitted type parameter. 
        ''' If not matching item can be found, 
        ''' a null parent is being returned.</returns>
        Public Function FindChild(Of T As DependencyObject)(ByVal parent As DependencyObject, ByVal childName As String) As T
            ' Confirm parent and childName are valid. 
            If parent Is Nothing Then Return Nothing

            Dim foundChild As T = Nothing

            Dim childrenCount = VisualTreeHelper.GetChildrenCount(parent)
            For i = 0 To childrenCount - 1
                Dim child = VisualTreeHelper.GetChild(parent, i)
                ' If the child is not of the request child type child
                Dim childType As T = TryCast(child, T)
                If childType Is Nothing Then
                    ' recursively drill down the tree
                    foundChild = FindChild(Of T)(child, childName)

                    ' If the child is found, break so we do not overwrite the found child. 
                    If foundChild IsNot Nothing Then Exit For
                ElseIf Not String.IsNullOrEmpty(childName) Then
                    Dim frameworkElement = TryCast(child, FrameworkElement)
                    ' If the child's name is set for search
                    If frameworkElement IsNot Nothing AndAlso Equals(frameworkElement.Name, childName) Then
                        ' if the child's name is of the request name
                        foundChild = CType(child, T)
                        Exit For
                    End If
                Else
                    ' child element found.
                    foundChild = CType(child, T)
                    Exit For
                End If
            Next

            Return foundChild
        End Function
    End Module
End Namespace
