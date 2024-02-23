Imports System.Windows
Imports System.Windows.Media
Imports System.Runtime.CompilerServices

Namespace VBNetCore.Mvvm.Extensions
    Public Module DependencyObjectExtension
        ''' <summary>
        ''' From MahApps project
        ''' This method is an alternative to WPF's
        ''' <seecref="VisualTreeHelper.GetParent"/> method, which also supports content elements. Keep in mind that for content element,
        ''' this method falls back to the logical tree of the element!
        ''' </summary>
        ''' <paramname="child">The item to be processed.</param>
        ''' <returns>The submitted item's parent, if available. Otherwise
        ''' null.</returns>
        <Extension()>
        Public Function GetParentObject(ByVal child As DependencyObject) As DependencyObject
            If child Is Nothing Then
                Return Nothing
            End If

            ' handle content elements separately
            Dim contentElement As ContentElement = TryCast(child, ContentElement)

            If contentElement IsNot Nothing Then
                Dim parent = ContentOperations.GetParent(contentElement)
                If parent IsNot Nothing Then
                    Return parent
                Else
                    Dim fce As FrameworkContentElement = TryCast(contentElement, FrameworkContentElement)
                    If fce IsNot Nothing Then
                        Return fce.Parent
                    Else
                        Return Nothing
                    End If
                End If


            End If

            Dim childParent = VisualTreeHelper.GetParent(child)
            If childParent IsNot Nothing Then
                Return childParent
            End If

            ' also try searching for parent in framework elements (such as DockPanel, etc)
            Dim frameworkElement As FrameworkElement = TryCast(child, FrameworkElement)

            If frameworkElement IsNot Nothing Then
                Dim parent = frameworkElement.Parent
                If parent IsNot Nothing Then
                    Return parent
                End If
            End If

            Return Nothing
        End Function

    End Module
End Namespace
